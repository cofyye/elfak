import { A52 } from "../algorithms/a52";
import { DoubleTransposition } from "../algorithms/doubleTransposition";
import { CFB } from "../algorithms/cfb";
import { ApplicationState } from "../classes/ApplicationState";
import { MD5 } from "../algorithms/md5";

export function a52Encrypt(data: Buffer): Buffer {
  const a52 = new A52();
  const defaultFrame = 0x21;

  // Convert encrypt secret key to Uint8Array
  const keyArray = new Uint8Array(
    Buffer.from(ApplicationState.encryptSecretKey)
  );

  const dataArray = new Uint8Array(data);
  const encrypted = a52.encrypt(dataArray, keyArray, defaultFrame);
  return Buffer.from(encrypted);
}

export function a52Decrypt(data: Buffer): Buffer {
  const a52 = new A52();
  const defaultFrame = 0x21;

  // Convert decrypt secret key to Uint8Array
  const keyArray = new Uint8Array(
    Buffer.from(ApplicationState.decryptSecretKey)
  );

  const dataArray = new Uint8Array(data);
  const decrypted = a52.decrypt(dataArray, keyArray, defaultFrame);
  return Buffer.from(decrypted);
}

export function doubleTranspositionEncrypt(data: Buffer): Buffer {
  const dt = new DoubleTransposition();

  // Convert buffer to string
  const message = data.toString("utf8");

  // Use encrypt secret key as transposition key
  const key = ApplicationState.encryptSecretKey;

  // Encrypt the message
  const encrypted = dt.encrypt(message, key);

  // Convert back to buffer
  return Buffer.from(encrypted, "utf8");
}

export function doubleTranspositionDecrypt(data: Buffer): Buffer {
  const dt = new DoubleTransposition();

  // Convert buffer to string
  const cipher = data.toString("utf8");

  // Use decrypt secret key as transposition key
  const key = ApplicationState.decryptSecretKey;

  // Decrypt the message
  const decrypted = dt.decrypt(cipher, key);

  // Convert back to buffer
  return Buffer.from(decrypted, "utf8");
}

export function cfbEncrypt(data: Buffer): Buffer {
  const cfb = new CFB();

  return cfb.encrypt(data);
}

export function cfbDecrypt(data: Buffer): Buffer {
  const cfb = new CFB();

  return cfb.decrypt(data);
}

export function calculateMD5(data: Buffer): string {
  return MD5.hash(data);
}
