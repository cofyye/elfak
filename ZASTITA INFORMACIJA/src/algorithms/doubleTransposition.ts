/*
 * Double Transposition Cipher
 */

export class DoubleTransposition {
  constructor() {}

  private makeMatrix(message: string, key: string): string[][] {
    const cols = key.length;
    const rows = Math.ceil(message.length / cols);
    const matrix: string[][] = Array.from({ length: rows }, () =>
      Array(cols).fill("-")
    );

    let k = 0;
    for (let i = 0; i < rows; i++) {
      for (let j = 0; j < cols; j++) {
        if (k < message.length) {
          matrix[i][j] = message[k++];
        }
      }
    }
    return matrix;
  }

  private permuteColumns(matrix: string[][], key: string): string {
    const order = this.getColumnOrder(key);
    let result = "";

    for (const idx of order) {
      for (let i = 0; i < matrix.length; i++) {
        result += matrix[i][idx];
      }
    }
    return result;
  }

  private getColumnOrder(key: string): number[] {
    const arr = key.split("").map((ch, idx) => ({ ch, idx }));
    arr.sort((a, b) => (a.ch < b.ch ? -1 : a.ch > b.ch ? 1 : a.idx - b.idx));
    return arr.map((x) => x.idx);
  }

  private inversePermute(cipher: string, key: string): string {
    const cols = key.length;
    const rows = Math.ceil(cipher.length / cols);
    const order = this.getColumnOrder(key);

    const matrix: string[][] = Array.from({ length: rows }, () =>
      Array(cols).fill("")
    );

    let k = 0;
    for (const idx of order) {
      for (let r = 0; r < rows; r++) {
        if (k < cipher.length) {
          matrix[r][idx] = cipher[k++];
        }
      }
    }

    let result = "";
    for (let r = 0; r < rows; r++) {
      for (let c = 0; c < cols; c++) {
        result += matrix[r][c];
      }
    }
    return result.replace(/-+$/g, "");
  }

  public encrypt(message: string, key: string): string {
    // First transposition
    const m1 = this.makeMatrix(message, key);
    const firstPass = this.permuteColumns(m1, key);

    // Second transposition
    const m2 = this.makeMatrix(firstPass, key);
    const secondPass = this.permuteColumns(m2, key);

    return secondPass;
  }

  public decrypt(cipher: string, key: string): string {
    // Inverse of second transposition
    const firstInverse = this.inversePermute(cipher, key);
    const original = this.inversePermute(firstInverse, key);

    return original;
  }
}
