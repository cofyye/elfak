import * as net from "net";
import * as fs from "fs";
import * as path from "path";
import * as os from "os";
import {
  a52Decrypt,
  doubleTranspositionDecrypt,
  cfbDecrypt,
  calculateMD5,
} from "../helpers/algorithm.helpers";
import { ApplicationState } from "../classes/ApplicationState";

const TCP_SERVER_DIR = path.join(os.homedir(), "Desktop", "tcp_server_files");

interface FileTransfer {
  filename: string;
  filesize: number;
  hashLength: number;
  hash: string;
  algorithm: string;
  data: Buffer;
  receivedSize: number;
}

export class TcpServer {
  private server: net.Server | null = null;

  private decryptFileData(data: Buffer, algorithm: string): Buffer {
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

  public start(port: number = 3000): net.Server {
    this.server = net.createServer();

    this.server.on("connection", (socket) => {
      const clientInfo = `Client connected: ${socket.remoteAddress}`;

      ApplicationState.mainWindow?.webContents.send("log-message", {
        message: clientInfo,
        type: "info",
      });

      let currentTransfer: FileTransfer | null = null;
      let headerBuffer = Buffer.alloc(0);

      socket.on("data", (data) => {
        try {
          if (!currentTransfer) {
            // Accumulate header data
            headerBuffer = Buffer.concat([headerBuffer, data]);

            // Try to parse header format: fileName (string) + fileSize (long) + hashLength (int) + hash (bytes)
            if (headerBuffer.length >= 16) {
              let offset = 0;

              // Read filename length and filename
              const filenameLength = headerBuffer.readUInt32LE(offset);
              offset += 4;

              if (headerBuffer.length >= offset + filenameLength + 8 + 4) {
                const filename = headerBuffer
                  .slice(offset, offset + filenameLength)
                  .toString();
                offset += filenameLength;

                // Read file size (long - 64 bit)
                const filesize = Number(headerBuffer.readBigUInt64LE(offset));
                offset += 8;

                // Read hash length
                const hashLength = headerBuffer.readUInt32LE(offset);
                offset += 4;

                if (headerBuffer.length >= offset + hashLength) {
                  const hash = headerBuffer
                    .slice(offset, offset + hashLength)
                    .toString();
                  offset += hashLength;

                  // Use server algorithm from ApplicationState
                  const algorithm = ApplicationState.serverAlgorithm;

                  currentTransfer = {
                    filename,
                    filesize,
                    hashLength,
                    hash,
                    algorithm,
                    data: Buffer.alloc(0),
                    receivedSize: 0,
                  };

                  // Process remaining data as file content
                  const remainingData = headerBuffer.slice(offset);
                  if (remainingData.length > 0) {
                    handleFileData(remainingData);
                  }

                  headerBuffer = Buffer.alloc(0);
                }
              }
            }
          } else {
            handleFileData(data);
          }
        } catch (error) {
          const errorMsg = `Error processing data: ${error}`;
          ApplicationState.mainWindow?.webContents.send("log-message", {
            message: errorMsg,
            type: "error",
          });
          socket.end();
        }
      });

      const handleFileData = (data: Buffer) => {
        if (!currentTransfer) return;

        currentTransfer.data = Buffer.concat([currentTransfer.data, data]);
        currentTransfer.receivedSize += data.length;

        if (currentTransfer.receivedSize >= currentTransfer.filesize) {
          const success = this.processCompletedTransfer(currentTransfer);
          if (success) {
            const successMsg = `File transfer completed successfully: ${currentTransfer.filename}`;
            ApplicationState.mainWindow?.webContents.send("log-message", {
              message: successMsg,
              type: "success",
            });
          } else {
            const errorMsg = `File transfer failed for: ${currentTransfer.filename}`;
            ApplicationState.mainWindow?.webContents.send("log-message", {
              message: errorMsg,
              type: "error",
            });
          }
          currentTransfer = null;
        }
      };

      socket.on("end", () => {
        const disconnectMsg = "Client disconnected";
        ApplicationState.mainWindow?.webContents.send("log-message", {
          message: disconnectMsg,
          type: "info",
        });
      });

      socket.on("error", (err) => {
        const socketErrorMsg = `Socket error: ${err}`;
        ApplicationState.mainWindow?.webContents.send("log-message", {
          message: socketErrorMsg,
          type: "error",
        });
      });
    });

    this.server.listen(port, () => {
      const listenMsg = `Server listening on port ${port}`;
      ApplicationState.mainWindow?.webContents.send("log-message", {
        message: listenMsg,
        type: "info",
      });
    });

    return this.server;
  }

  public stop(): void {
    if (this.server) {
      this.server.close();
      this.server = null;
    }
  }

  private processCompletedTransfer(transfer: FileTransfer): boolean {
    // Ensure TCP server directory exists
    if (!fs.existsSync(TCP_SERVER_DIR)) {
      fs.mkdirSync(TCP_SERVER_DIR, { recursive: true });
    }

    // Use the specified algorithm from the transfer
    try {
      const tryMsg = `Decrypting with specified algorithm: ${transfer.algorithm}`;
      ApplicationState.mainWindow?.webContents.send("log-message", {
        message: tryMsg,
        type: "info",
      });

      const decryptedData = this.decryptFileData(
        transfer.data,
        transfer.algorithm
      );
      const calculatedHash = calculateMD5(decryptedData);

      if (calculatedHash === transfer.hash) {
        const successMsg = `Hash verification successful with algorithm: ${transfer.algorithm}`;
        ApplicationState.mainWindow?.webContents.send("log-message", {
          message: successMsg,
          type: "success",
        });

        const outputPath = path.join(TCP_SERVER_DIR, transfer.filename);
        fs.writeFileSync(outputPath, decryptedData);

        const savedMsg = `File decrypted and saved: ${outputPath}`;
        ApplicationState.mainWindow?.webContents.send("log-message", {
          message: savedMsg,
          type: "success",
        });

        return true;
      } else {
        const failMsg = `Hash verification failed for algorithm: ${transfer.algorithm}`;
        ApplicationState.mainWindow?.webContents.send("log-message", {
          message: failMsg,
          type: "error",
        });
      }
    } catch (error) {
      const errorMsg = `Decryption failed with algorithm ${transfer.algorithm}: ${error}`;
      ApplicationState.mainWindow?.webContents.send("log-message", {
        message: errorMsg,
        type: "error",
      });
    }

    const finalErrorMsg = `Failed to decrypt file with algorithm ${transfer.algorithm} - file may be corrupted or keys may be incorrect`;
    ApplicationState.mainWindow?.webContents.send("log-message", {
      message: finalErrorMsg,
      type: "error",
    });

    return false;
  }
}

// Export singleton instance
export const tcpServer = new TcpServer();
