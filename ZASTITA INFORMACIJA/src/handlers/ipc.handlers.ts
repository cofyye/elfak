import { dialog, ipcMain } from "electron";
import { ApplicationState } from "../classes/ApplicationState";
import path from "path";
import fs from "fs";
import {
  decryptFile,
  encryptFile,
  ensureDirectoryExists,
} from "../helpers/file.helpers";
import { startFileWatcher } from "../helpers/fsw.helpers";
import { TcpServer } from "../tcp/tcp-server";
import { getLocalIPAddress } from "../helpers/network.helpers";
import { generateRandomKey, setCustomKey } from "../helpers/key.helpers";
import { tcpClient } from "../tcp/tcp-client";

export function registerIpcHandlers() {
  console.log("IPC handlers registered");

  ipcMain.handle("choose-file", async () => {
    if (!ApplicationState.mainWindow) return null;

    const result = await dialog.showOpenDialog(ApplicationState.mainWindow, {
      properties: ["openFile"],
      filters: [
        { name: "Text Files", extensions: ["txt"] },
        { name: "All Files", extensions: ["*"] },
      ],
    });

    if (result.canceled || result.filePaths.length === 0) {
      return null;
    }

    const filePath = result.filePaths[0];
    const fileName = path.basename(filePath);
    const fileExtension = path.extname(fileName).toLowerCase();

    // Check if file is .txt
    if (fileExtension !== ".txt") {
      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("log-message", {
          message: `Manual encryption only accepts .txt files. Selected: ${fileName}`,
          type: "error",
        });
      }
      return null;
    }

    return {
      path: filePath,
      name: fileName,
    };
  });

  ipcMain.handle("choose-directory", async (_, title: string) => {
    if (!ApplicationState.mainWindow) return null;

    const result = await dialog.showOpenDialog(ApplicationState.mainWindow, {
      properties: ["openDirectory"],
      title: title,
    });

    if (result.canceled || result.filePaths.length === 0) {
      return null;
    }

    return result.filePaths[0];
  });

  ipcMain.handle("get-config", async () => {
    return {
      fswEnabled: ApplicationState.fswEnabled,
      targetDirectory: ApplicationState.targetDirectory,
      encryptedDirectory: ApplicationState.encryptedDirectory,
      fswAlgorithm: ApplicationState.fswAlgorithm,
      serverAlgorithm: ApplicationState.serverAlgorithm,
      tcpHost: ApplicationState.tcpHost,
      tcpPort: ApplicationState.tcpPort,
      encryptSecretKey: ApplicationState.encryptSecretKey,
      decryptSecretKey: ApplicationState.decryptSecretKey,
      algorithms: ["doubleTransposition", "a52", "cfb"],
    };
  });

  // Secret Key Management handlers
  ipcMain.handle("get-encrypt-secret-key", async () => {
    return ApplicationState.encryptSecretKey;
  });

  ipcMain.handle("get-decrypt-secret-key", async () => {
    return ApplicationState.decryptSecretKey;
  });

  ipcMain.handle("set-encrypt-secret-key", async (_, customKey: string) => {
    ApplicationState.encryptSecretKey = setCustomKey(customKey);

    return ApplicationState.encryptSecretKey;
  });

  ipcMain.handle("set-decrypt-secret-key", async (_, customKey: string) => {
    ApplicationState.decryptSecretKey = setCustomKey(customKey);

    return ApplicationState.decryptSecretKey;
  });

  ipcMain.handle("generate-new-encrypt-key", async () => {
    const newKey = generateRandomKey();
    ApplicationState.encryptSecretKey = newKey;

    return newKey;
  });

  ipcMain.handle(
    "encrypt-file",
    async (_, filePath: string, algorithm: string) => {
      if (!ApplicationState.encryptedDirectory) {
        throw new Error(
          "Encrypted directory not set. Please configure directories first."
        );
      }

      const fileContent = fs.readFileSync(filePath);
      const encryptedData = encryptFile(fileContent, algorithm);

      ensureDirectoryExists(ApplicationState.encryptedDirectory);

      const fileName = path.basename(filePath);
      const encryptedFilePath = path.join(
        ApplicationState.encryptedDirectory,
        `${fileName}.encrypted`
      );

      fs.writeFileSync(encryptedFilePath, encryptedData);

      // Log successful manual encryption
      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("log-message", {
          message: `Manual encryption completed: ${fileName} using ${algorithm} algorithm`,
          type: "success",
        });
      }

      return {
        outputPath: encryptedFilePath,
      };
    }
  );

  ipcMain.handle("choose-encrypted-file", async () => {
    if (!ApplicationState.mainWindow) return null;

    const result = await dialog.showOpenDialog(ApplicationState.mainWindow, {
      properties: ["openFile"],
      filters: [
        { name: "Encrypted Files", extensions: ["encrypted"] },
        { name: "All Files", extensions: ["*"] },
      ],
      defaultPath: ApplicationState.encryptedDirectory,
    });

    if (result.canceled || result.filePaths.length === 0) {
      return null;
    }

    const filePath = result.filePaths[0];
    const fileName = path.basename(filePath);
    const fileExtension = path.extname(fileName).toLowerCase();

    // Check if file is .encrypted
    if (fileExtension !== ".encrypted") {
      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("log-message", {
          message: `Decryption only accepts .encrypted files. Selected: ${fileName}`,
          type: "error",
        });
      }
      return null;
    }

    return {
      path: filePath,
      name: fileName,
    };
  });

  ipcMain.handle(
    "decrypt-file",
    async (_, filePath: string, algorithm: string) => {
      const encryptedData = fs.readFileSync(filePath);
      const decryptedData = decryptFile(encryptedData, algorithm);

      // Create decrypted directory
      const decryptedDir = path.join(
        path.dirname(ApplicationState.encryptedDirectory),
        "decrypted_files"
      );
      ensureDirectoryExists(decryptedDir);

      // Get original filename (remove .encrypted extension)
      const originalFileName = path
        .basename(filePath)
        .replace(".encrypted", "");

      const outputPath = path.join(decryptedDir, originalFileName);
      fs.writeFileSync(outputPath, decryptedData);

      return {
        outputPath: outputPath,
      };
    }
  );

  ipcMain.handle("set-target-directory", async (_, dirPath: string) => {
    ApplicationState.targetDirectory = dirPath;

    if (ApplicationState.fswEnabled) {
      startFileWatcher();
      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("log-message", {
          message: "File System Watcher restarted with new target directory",
          type: "info",
        });
      }
    }
    return { success: true };
  });

  ipcMain.handle("set-encrypted-directory", async (_, dirPath: string) => {
    ApplicationState.encryptedDirectory = dirPath;
    ensureDirectoryExists(ApplicationState.encryptedDirectory);

    return { success: true };
  });

  ipcMain.handle("toggle-fsw", async (_, enabled: boolean) => {
    if (
      enabled &&
      (!ApplicationState.targetDirectory ||
        !ApplicationState.encryptedDirectory)
    ) {
      const errorMsg =
        "Cannot enable FSW: Target and encrypted directories must be configured first";

      return {
        enabled: false,
        error: errorMsg,
      };
    }

    // Check if TCP server is running when trying to disable FSW
    if (!enabled && ApplicationState.tcpServer) {
      const errorMsg =
        "Cannot disable FSW: TCP Server is running. Please stop the TCP Server first.";

      return {
        enabled: ApplicationState.fswEnabled,
        error: errorMsg,
      };
    }

    // Only change the state if we passed all checks
    ApplicationState.fswEnabled = enabled;

    if (enabled) {
      startFileWatcher();
    } else {
      if (ApplicationState.fileWatcher) {
        ApplicationState.fileWatcher.close();
        ApplicationState.fileWatcher = null;
      }
    }

    return {
      enabled: ApplicationState.fswEnabled,
    };
  });

  ipcMain.handle("get-fsw-status", async () => {
    return {
      enabled: ApplicationState.fswEnabled,
      targetDirectory: ApplicationState.targetDirectory,
      encryptedDirectory: ApplicationState.encryptedDirectory,
    };
  });

  ipcMain.handle("set-fsw-algorithm", async (_, algorithm: string) => {
    ApplicationState.fswAlgorithm = algorithm;
    return true;
  });

  ipcMain.handle("set-server-algorithm", async (_, algorithm: string) => {
    ApplicationState.serverAlgorithm = algorithm;
    return true;
  });

  // TCP Settings handlers
  ipcMain.handle("set-tcp-config", async (_, host: string, port: number) => {
    ApplicationState.tcpHost = host;
    ApplicationState.tcpPort = port;

    if (ApplicationState.mainWindow) {
      ApplicationState.mainWindow.webContents.send("log-message", {
        message: `TCP config set to: ${host}:${port}`,
        type: "info",
      });
    }

    return true;
  });

  ipcMain.handle("set-tcp-host", async (_, host: string) => {
    ApplicationState.tcpHost = host;

    if (ApplicationState.mainWindow) {
      ApplicationState.mainWindow.webContents.send("log-message", {
        message: `TCP host set to: ${host}`,
        type: "info",
      });
    }

    return true;
  });

  ipcMain.handle("set-tcp-port", async (_, port: number) => {
    ApplicationState.tcpPort = port;

    if (ApplicationState.mainWindow) {
      ApplicationState.mainWindow.webContents.send("log-message", {
        message: `TCP port set to: ${port}`,
        type: "info",
      });
    }

    return true;
  });

  // TCP Connection handlers
  ipcMain.handle("start-tcp-server", async (_, port?: number) => {
    try {
      const serverPort = port || ApplicationState.tcpPort;

      if (ApplicationState.tcpServer) {
        if (ApplicationState.mainWindow) {
          ApplicationState.mainWindow.webContents.send("log-message", {
            message: "TCP server is already running",
            type: "warning",
          });
        }
        return { success: false };
      }

      ApplicationState.tcpServer = new TcpServer();
      ApplicationState.tcpServer.start(serverPort);

      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("log-message", {
          message: `TCP server started on port ${serverPort}`,
          type: "success",
        });
      }

      return { success: true };
    } catch (error) {
      const errorMessage =
        error instanceof Error ? error.message : String(error);

      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("log-message", {
          message: `Failed to start TCP server: ${errorMessage}`,
          type: "error",
        });
      }

      return { success: false };
    }
  });

  ipcMain.handle("stop-tcp-server", async () => {
    try {
      if (!ApplicationState.tcpServer) {
        if (ApplicationState.mainWindow) {
          ApplicationState.mainWindow.webContents.send("log-message", {
            message: "TCP server is not running",
            type: "warning",
          });
        }
        return { success: false };
      }

      ApplicationState.tcpServer.stop();
      ApplicationState.tcpServer = null;

      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("log-message", {
          message: "TCP server stopped",
          type: "info",
        });
      }

      return { success: true };
    } catch (error) {
      const errorMessage =
        error instanceof Error ? error.message : String(error);

      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("log-message", {
          message: `Failed to stop TCP server: ${errorMessage}`,
          type: "error",
        });
      }

      return { success: false };
    }
  });

  ipcMain.handle("get-local-ip", async () => {
    return getLocalIPAddress();
  });

  ipcMain.handle(
    "send-tcp-file",
    async (
      _,
      host: string,
      port: number,
      filePath: string,
      algorithm: string
    ) => {
      try {
        if (ApplicationState.mainWindow) {
          ApplicationState.mainWindow.webContents.send("log-message", {
            message: `Connecting to ${host}:${port}...`,
            type: "info",
          });
        }

        await tcpClient.connectToServer(host, port);

        if (ApplicationState.mainWindow) {
          ApplicationState.mainWindow.webContents.send("log-message", {
            message: `Connected! Sending file: ${path.basename(filePath)}`,
            type: "info",
          });
        }

        await tcpClient.sendFile(filePath, algorithm);

        if (ApplicationState.mainWindow) {
          ApplicationState.mainWindow.webContents.send("log-message", {
            message: `File sent successfully using ${algorithm} algorithm`,
            type: "success",
          });
        }

        // Disconnect after sending
        tcpClient.disconnectFromServer();

        return { success: true };
      } catch (error) {
        const errorMessage =
          error instanceof Error ? error.message : String(error);

        if (ApplicationState.mainWindow && errorMessage) {
          ApplicationState.mainWindow.webContents.send("log-message", {
            message: `Failed to send file: ${errorMessage}`,
            type: "error",
          });
        }

        return { success: false };
      }
    }
  );
}
