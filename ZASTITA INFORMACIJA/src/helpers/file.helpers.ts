import * as fs from "fs";
import {
  doubleTranspositionDecrypt,
  doubleTranspositionEncrypt,
  a52Decrypt,
  a52Encrypt,
  cfbDecrypt,
  cfbEncrypt,
} from "./algorithm.helpers";

export function ensureDirectoryExists(dirPath: string) {
  if (!fs.existsSync(dirPath)) {
    fs.mkdirSync(dirPath, { recursive: true });
  }
}

export function encryptFile(data: Buffer, algorithm: string): Buffer {
  switch (algorithm) {
    case "doubleTransposition":
      return doubleTranspositionEncrypt(data);
    case "a52":
      return a52Encrypt(data);
    case "cfb":
      return cfbEncrypt(data);
    default:
      throw new Error(`Unsupported algorithm: ${algorithm}`);
  }
}

export function decryptFile(data: Buffer, algorithm: string): Buffer {
  switch (algorithm) {
    case "doubleTransposition":
      return doubleTranspositionDecrypt(data);
    case "a52":
      return a52Decrypt(data);
    case "cfb":
      return cfbDecrypt(data);
    default:
      throw new Error(`Unsupported algorithm: ${algorithm}`);
  }
}
