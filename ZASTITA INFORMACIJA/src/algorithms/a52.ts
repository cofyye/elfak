/**
 * A5/2 Stream Cipher
 */

export class A52 {
  // Masks for the shift registers
  private static readonly R1MASK = 0x07ffff; // 19 bits, numbered 0..18
  private static readonly R2MASK = 0x3fffff; // 22 bits, numbered 0..21
  private static readonly R3MASK = 0x7fffff; // 23 bits, numbered 0..22
  private static readonly R4MASK = 0x01ffff; // 17 bits, numbered 0..16

  // A bit of R4 that controls each of the shift registers
  private static readonly R4TAP1 = 0x000400; // bit 10
  private static readonly R4TAP2 = 0x000008; // bit 3
  private static readonly R4TAP3 = 0x000080; // bit 7

  // Feedback taps, for clocking the shift registers
  private static readonly R1TAPS = 0x072000; // bits 18,17,16,13
  private static readonly R2TAPS = 0x300000; // bits 21,20
  private static readonly R3TAPS = 0x700080; // bits 22,21,20,7
  private static readonly R4TAPS = 0x010800; // bits 16,11

  // The shift registers
  private R1 = 0;
  private R2 = 0;
  private R3 = 0;
  private R4 = 0;

  private delaybit = 0;

  constructor() {}

  /**
   * Calculate the parity of a 32-bit word, i.e. the sum of its bits modulo 2
   */
  private parity(x: number): number {
    x ^= x >> 16;
    x ^= x >> 8;
    x ^= x >> 4;
    x ^= x >> 2;
    x ^= x >> 1;
    return x & 1;
  }

  /**
   * Clock one shift register
   */
  private clockone(
    reg: number,
    mask: number,
    taps: number,
    loadedBit: number
  ): number {
    const t = reg & taps;
    reg = (reg << 1) & mask;
    reg |= this.parity(t);
    reg |= loadedBit;
    return reg;
  }

  /**
   * Return 1 if at least two of the parameter words are non-zero
   */
  private majority(w1: number, w2: number, w3: number): number {
    const sum = (w1 !== 0 ? 1 : 0) + (w2 !== 0 ? 1 : 0) + (w3 !== 0 ? 1 : 0);
    return sum >= 2 ? 1 : 0;
  }

  /**
   * Clock the shift registers
   */
  private clock(allP: number, loaded: number): void {
    const maj = this.majority(
      this.R4 & A52.R4TAP1,
      this.R4 & A52.R4TAP2,
      this.R4 & A52.R4TAP3
    );

    if (allP || ((this.R4 & A52.R4TAP1) !== 0) === (maj === 1)) {
      this.R1 = this.clockone(this.R1, A52.R1MASK, A52.R1TAPS, loaded << 15);
    }
    if (allP || ((this.R4 & A52.R4TAP2) !== 0) === (maj === 1)) {
      this.R2 = this.clockone(this.R2, A52.R2MASK, A52.R2TAPS, loaded << 16);
    }
    if (allP || ((this.R4 & A52.R4TAP3) !== 0) === (maj === 1)) {
      this.R3 = this.clockone(this.R3, A52.R3MASK, A52.R3TAPS, loaded << 18);
    }
    this.R4 = this.clockone(this.R4, A52.R4MASK, A52.R4TAPS, loaded << 10);
  }

  /**
   * Generate an output bit from the current state
   */
  private getbit(): number {
    const topbits =
      ((this.R1 >> 18) ^ (this.R2 >> 21) ^ (this.R3 >> 22)) & 0x01;

    const nowbit = this.delaybit;
    this.delaybit =
      topbits ^
      this.majority(this.R1 & 0x8000, ~this.R1 & 0x4000, this.R1 & 0x1000) ^
      this.majority(~this.R2 & 0x10000, this.R2 & 0x2000, this.R2 & 0x200) ^
      this.majority(this.R3 & 0x40000, this.R3 & 0x10000, ~this.R3 & 0x2000);
    return nowbit;
  }

  /**
   * Do the A5 key setup. This routine accepts a 64-bit key and a 22-bit frame number.
   */
  private keysetup(key: Uint8Array, frame: number): void {
    // Zero out the shift registers
    this.R1 = this.R2 = this.R3 = 0;
    this.R4 = 0;

    // Load the key into the shift registers
    for (let i = 0; i < 64; i++) {
      this.clock(1, 0); // always clock
      const keybit = (key[Math.floor(i / 8)] >> (i & 7)) & 1; // The i-th bit of the key
      this.R1 ^= keybit;
      this.R2 ^= keybit;
      this.R3 ^= keybit;
      this.R4 ^= keybit;
    }

    // For A5/2, signal when the last bit is being clocked in
    for (let i = 0; i < 22; i++) {
      this.clock(1, i === 21 ? 1 : 0); // always clock
      const framebit = (frame >> i) & 1; // The i-th bit of the frame #
      this.R1 ^= framebit;
      this.R2 ^= framebit;
      this.R3 ^= framebit;
      this.R4 ^= framebit;
    }

    // Run the shift registers for 100 clocks to mix the keying material and frame number
    for (let i = 0; i < 100; i++) {
      this.clock(0, 0);
    }

    // For A5/2, we have to load the delayed output bit
    this.getbit();
  }

  /**
   * Generate output. We generate 228 bits of keystream output.
   * The first 114 bits is for the A->B frame; the next 114 bits is for the B->A frame.
   */
  private run(
    key: Uint8Array,
    frame: number
  ): { AtoBkeystream: Uint8Array; BtoAkeystream: Uint8Array } {
    const AtoBkeystream = new Uint8Array(15);
    const BtoAkeystream = new Uint8Array(15);

    this.keysetup(key, frame);

    // Zero out the output buffers
    AtoBkeystream.fill(0);
    BtoAkeystream.fill(0);

    // Generate 114 bits of keystream for the A->B direction. Store it, MSB first.
    for (let i = 0; i < 114; i++) {
      this.clock(0, 0);
      AtoBkeystream[Math.floor(i / 8)] |= this.getbit() << (7 - (i & 7));
    }

    // Generate 114 bits of keystream for the B->A direction. Store it, MSB first.
    for (let i = 0; i < 114; i++) {
      this.clock(0, 0);
      BtoAkeystream[Math.floor(i / 8)] |= this.getbit() << (7 - (i & 7));
    }

    return { AtoBkeystream, BtoAkeystream };
  }

  /**
   * Generate keystream for arbitrary length data
   */
  private generateKeystream(
    key: Uint8Array,
    frame: number,
    lengthBytes: number
  ): Uint8Array {
    this.keysetup(key, frame);
    const keystream = new Uint8Array(lengthBytes);

    for (let i = 0; i < lengthBytes * 8; i++) {
      this.clock(0, 0);
      const bit = this.getbit();
      keystream[Math.floor(i / 8)] |= bit << (7 - (i & 7));
    }

    return keystream;
  }

  /**
   * Encrypt data using A5/2 algorithm
   */
  public encrypt(data: Uint8Array, key: Uint8Array, frame: number): Uint8Array {
    const keystream = this.generateKeystream(key, frame, data.length);
    const result = new Uint8Array(data.length);

    for (let i = 0; i < data.length; i++) {
      result[i] = data[i] ^ keystream[i];
    }

    return result;
  }

  /**
   * Decrypt data using A5/2 algorithm (same as encrypt for stream cipher)
   */
  public decrypt(
    encryptedData: Uint8Array,
    key: Uint8Array,
    frame: number
  ): Uint8Array {
    return this.encrypt(encryptedData, key, frame);
  }
}
