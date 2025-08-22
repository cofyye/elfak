import { BrowserWindow } from "electron";
import * as chokidar from "chokidar";
import { generateRandomKey } from "../helpers/key.helpers";
import { TcpServer } from "../tcp/tcp-server";

export class ApplicationState {
  static mainWindow: BrowserWindow | null = null;

  static fswEnabled = false;
  static targetDirectory = "";
  static encryptedDirectory = "";
  static fswAlgorithm = "cfb"; // Default algorithm for FSW
  static serverAlgorithm = "cfb"; // Default algorithm for TCP Server
  static tcpHost = "127.0.0.1";
  static tcpPort = 3000;
  static fileWatcher: chokidar.FSWatcher | null = null;
  static tcpServer: TcpServer | null = null;

  // Secret keys for encryption and decryption algorithms
  static encryptSecretKey = generateRandomKey(); // Initialize with random key for encryption
  static decryptSecretKey = generateRandomKey(); // Initialize with random key for decryption
}
