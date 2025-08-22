import crypto from "crypto";

export function generateRandomKey(length: number = 16): string {
  const bytes = Math.ceil(length / 2);
  return crypto.randomBytes(bytes).toString("hex").substring(0, length);
}

export function setCustomKey(customKey: string): string {
  // Trim whitespace
  const trimmedKey = customKey.trim();

  // If empty, generate a random key
  if (trimmedKey.length === 0) {
    return generateRandomKey();
  }

  return trimmedKey;
}
