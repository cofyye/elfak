import * as net from "net";
import * as fs from "fs";
import {
  a52Encrypt,
  doubleTranspositionEncrypt,
  cfbEncrypt,
  calculateMD5,
} from "../helpers/algorithm.helpers";

export class TcpClient {
  private client: net.Socket | null = null;

  private encryptFile(data: Buffer, algorithm: string): Buffer {
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

  public connectToServer(host: string, port: number): Promise<void> {
    return new Promise((resolve, reject) => {
      this.client = new net.Socket();

      this.client.on("connect", () => {
        resolve();
      });

      this.client.on("error", (error) => {
        reject(error);
      });

      this.client.connect(port, host);
    });
  }

  public sendFile(filePath: string, algorithm: string = "cfb"): Promise<void> {
    return new Promise((resolve, reject) => {
      if (!this.client) {
        reject(new Error("Not connected to server"));
        return;
      }

      try {
        const fileContent = fs.readFileSync(filePath);
        const fileName = filePath.split(/[\\/]/).pop() || "unknown";

        // Calculate hash of original file
        const hash = calculateMD5(fileContent);

        // Encrypt the file using specified algorithm
        const encryptedData = this.encryptFile(fileContent, algorithm);

        // Create header following the specified format:
        // fileName (string) + fileSize (long) + hashLength (int) + hash (bytes)
        const fileNameBuffer = Buffer.from(fileName);
        const hashBuffer = Buffer.from(hash);

        // Calculate header size
        const headerSize =
          4 +
          fileNameBuffer.length + // fileName length + fileName
          8 + // fileSize (long - 64 bit)
          4 +
          hashBuffer.length; // hashLength + hash

        const header = Buffer.alloc(headerSize);
        let offset = 0;

        // Write fileName (string)
        header.writeUInt32LE(fileNameBuffer.length, offset);
        offset += 4;
        fileNameBuffer.copy(header, offset);
        offset += fileNameBuffer.length;

        // Write fileSize (long)
        header.writeBigUInt64LE(BigInt(encryptedData.length), offset);
        offset += 8;

        // Write hashLength (int) + hash (bytes)
        header.writeUInt32LE(hashBuffer.length, offset);
        offset += 4;
        hashBuffer.copy(header, offset);

        // Send header and encrypted file data
        if (!this.client) {
          reject(new Error("Client disconnected"));
          return;
        }

        this.client.write(header, (err) => {
          if (err) {
            reject(err);
            return;
          }

          if (!this.client) {
            reject(new Error("Client disconnected"));
            return;
          }

          this.client.write(encryptedData, (err) => {
            if (err) {
              reject(err);
              return;
            }
            resolve();
          });
        });
      } catch (error) {
        reject(error);
      }
    });
  }

  public disconnectFromServer(): void {
    if (this.client) {
      this.client.end();
      this.client = null;
    }
  }
}

// Export singleton instance
export const tcpClient = new TcpClient();
