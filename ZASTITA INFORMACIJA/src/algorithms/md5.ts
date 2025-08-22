export class MD5 {
  private static readonly S: number[] = [
    7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 5, 9, 14, 20, 5,
    9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11,
    16, 23, 4, 11, 16, 23, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10,
    15, 21,
  ];

  private static readonly K: number[] = new Array(64)
    .fill(0)
    .map((_, i) => Math.floor(Math.abs(Math.sin(i + 1)) * Math.pow(2, 32)));

  private static leftRotate(x: number, c: number): number {
    return ((x << c) | (x >>> (32 - c))) >>> 0;
  }

  private static toHexString(num: number): string {
    const hex = num.toString(16);
    return "00000000".substr(hex.length) + hex;
  }

  public static hash(buffer: Buffer): string {
    // Initialize variables
    let a0 = 0x67452301;
    let b0 = 0xefcdab89;
    let c0 = 0x98badcfe;
    let d0 = 0x10325476;

    // Pre-processing
    const originalLength = buffer.length * 8; // length in bits
    const remainder = buffer.length % 64;
    const paddingLength = remainder < 56 ? 56 - remainder : 120 - remainder;

    // Create new buffer with padding
    const paddedBuffer = Buffer.alloc(buffer.length + paddingLength + 8);
    buffer.copy(paddedBuffer);
    paddedBuffer[buffer.length] = 0x80; // append 1 bit followed by zeros

    // Append original length in bits as little-endian 64-bit number
    paddedBuffer.writeUInt32LE(originalLength >>> 0, paddedBuffer.length - 8);
    paddedBuffer.writeUInt32LE(
      (originalLength / Math.pow(2, 32)) >>> 0,
      paddedBuffer.length - 4
    );

    // Process each 512-bit chunk
    for (let i = 0; i < paddedBuffer.length; i += 64) {
      const chunk = paddedBuffer.slice(i, i + 64);
      const M = new Array(16);
      for (let j = 0; j < 16; j++) {
        M[j] = chunk.readUInt32LE(j * 4);
      }

      let A = a0;
      let B = b0;
      let C = c0;
      let D = d0;

      // Main loop
      for (let j = 0; j < 64; j++) {
        let F: number, g: number;

        if (j < 16) {
          F = (B & C) | (~B & D);
          g = j;
        } else if (j < 32) {
          F = (D & B) | (~D & C);
          g = (5 * j + 1) % 16;
        } else if (j < 48) {
          F = B ^ C ^ D;
          g = (3 * j + 5) % 16;
        } else {
          F = C ^ (B | ~D);
          g = (7 * j) % 16;
        }

        const temp = D;
        D = C;
        C = B;
        B =
          (B + MD5.leftRotate((A + F + MD5.K[j] + M[g]) >>> 0, MD5.S[j])) >>> 0;
        A = temp;
      }

      a0 = (a0 + A) >>> 0;
      b0 = (b0 + B) >>> 0;
      c0 = (c0 + C) >>> 0;
      d0 = (d0 + D) >>> 0;
    }

    // Convert to hex string
    return (
      MD5.toHexString(a0) +
      MD5.toHexString(b0) +
      MD5.toHexString(c0) +
      MD5.toHexString(d0)
    );
  }
}
