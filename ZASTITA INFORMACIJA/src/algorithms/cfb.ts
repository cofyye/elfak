import { a52Encrypt } from "../helpers/algorithm.helpers";

const BLOCK_SIZE = 16; // 128 bits

export class CFB {
  private iv: Buffer = Buffer.alloc(BLOCK_SIZE, 1); // Default IV filled with ones

  constructor() {}

  encrypt(data: Buffer): Buffer {
    let previousBlock = this.iv;
    const encrypted = Buffer.alloc(data.length + BLOCK_SIZE);

    // Save IV at the start of the encrypted data
    this.iv.copy(encrypted, 0);

    for (let i = 0; i < data.length; i += BLOCK_SIZE) {
      const encryptedBlock = a52Encrypt(previousBlock);

      const blockSize = Math.min(BLOCK_SIZE, data.length - i);
      const cipherBlock = Buffer.alloc(blockSize);

      for (let j = 0; j < blockSize; j++) {
        cipherBlock[j] = encryptedBlock[j] ^ data[i + j];
      }

      cipherBlock.copy(encrypted, i + BLOCK_SIZE);
      previousBlock = cipherBlock; // update
    }

    return encrypted;
  }

  decrypt(data: Buffer): Buffer {
    let previousBlock = data.slice(0, BLOCK_SIZE);
    const encryptedData = data.slice(BLOCK_SIZE);
    const decrypted = Buffer.alloc(encryptedData.length);

    for (let i = 0; i < encryptedData.length; i += BLOCK_SIZE) {
      const encryptedBlock = a52Encrypt(previousBlock);

      const blockSize = Math.min(BLOCK_SIZE, encryptedData.length - i);
      const plainBlock = Buffer.alloc(blockSize);

      for (let j = 0; j < blockSize; j++) {
        plainBlock[j] = encryptedBlock[j] ^ encryptedData[i + j];
      }

      plainBlock.copy(decrypted, i);
      previousBlock = encryptedData.slice(i, i + blockSize); // update
    }

    return decrypted;
  }
}
