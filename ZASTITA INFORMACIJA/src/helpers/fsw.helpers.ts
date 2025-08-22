import path from "path";
import { ApplicationState } from "../classes/ApplicationState";
import { encryptFile, ensureDirectoryExists } from "./file.helpers";
import fs from "fs";
import chokidar from "chokidar";

export function startFileWatcher() {
  if (ApplicationState.fileWatcher) {
    ApplicationState.fileWatcher.close();
  }

  if (!ApplicationState.fswEnabled || !ApplicationState.targetDirectory) {
    return;
  }

  ensureDirectoryExists(ApplicationState.targetDirectory);
  ensureDirectoryExists(ApplicationState.encryptedDirectory);

  // Log FSW activation and scan existing files
  const activationMsg = "File System Watcher activated - scanning directory...";
  if (ApplicationState.mainWindow) {
    ApplicationState.mainWindow.webContents.send("log-message", {
      message: activationMsg,
      type: "info",
    });
  }

  // Scan and catalog existing files
  try {
    if (fs.existsSync(ApplicationState.targetDirectory)) {
      const items = fs.readdirSync(ApplicationState.targetDirectory);
      let fileCount = 0;
      let folderCount = 0;

      items.forEach((item) => {
        const itemPath = path.join(ApplicationState.targetDirectory, item);
        try {
          const stats = fs.statSync(itemPath);

          if (stats.isDirectory()) {
            folderCount++;
          } else if (path.extname(item).toLowerCase() === ".txt") {
            fileCount++;
          } else {
            // non .txt file found
          }
        } catch (error) {
          ApplicationState.mainWindow?.webContents.send("log-message", {
            message: `Error checking item ${item}: ${String(error)}`,
            type: "error",
          });
        }
      });

      const scanMsg = `Scan complete: Found ${fileCount} .txt files, ${folderCount} folders, ${
        items.length - fileCount - folderCount
      } other files`;
      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("log-message", {
          message: scanMsg,
          type: "info",
        });
      }
    }
  } catch (error) {
    if (ApplicationState.mainWindow) {
      ApplicationState.mainWindow.webContents.send("log-message", {
        message: `Error scanning directory: ${String(error)}`,
        type: "error",
      });
    }
  }

  ApplicationState.fileWatcher = chokidar.watch(
    ApplicationState.targetDirectory,
    {
      ignored: /(^|[\/\\])\../,
      persistent: true,
      ignoreInitial: false,
    }
  );

  ApplicationState.fileWatcher.on("add", async (filePath) => {
    try {
      const fileName = path.basename(filePath);
      const stats = fs.statSync(filePath);

      // Check if it's a directory
      if (stats.isDirectory()) {
        const errorMsg = `Removing folder and all contents: ${fileName}`;
        ApplicationState.mainWindow?.webContents.send("log-message", {
          message: errorMsg,
          type: "warning",
        });

        try {
          fs.rmSync(filePath, { recursive: true, force: true });
          const successMsg = `Folder and all contents removed: ${fileName}`;

          if (ApplicationState.mainWindow) {
            ApplicationState.mainWindow.webContents.send("log-message", {
              message: successMsg,
              type: "warning",
            });
          }
        } catch (removeError) {
          if (ApplicationState.mainWindow) {
            ApplicationState.mainWindow.webContents.send("log-message", {
              message: `Error removing folder: ${fileName}`,
              type: "error",
            });
          }
        }
        return;
      }

      const fileExtension = path.extname(fileName).toLowerCase();

      // Check if file is .txt
      if (fileExtension !== ".txt") {
        const errorMsg = `Removing non-.txt file: ${fileName}`;
        if (ApplicationState.mainWindow) {
          ApplicationState.mainWindow.webContents.send("log-message", {
            message: errorMsg,
            type: "warning",
          });
        }

        try {
          fs.unlinkSync(filePath);
          if (ApplicationState.mainWindow) {
            ApplicationState.mainWindow.webContents.send("log-message", {
              message: `Removed file: ${fileName}`,
              type: "warning",
            });
          }
        } catch (removeError) {
          if (ApplicationState.mainWindow) {
            ApplicationState.mainWindow.webContents.send("log-message", {
              message: `Error removing file: ${fileName}`,
              type: "error",
            });
          }
        }

        return;
      }

      // Log encryption start
      const encryptMsg = `Encrypting file: ${fileName}`;
      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("log-message", {
          message: encryptMsg,
          type: "info",
        });
      }

      const fileContent = fs.readFileSync(filePath);

      // Use configured FSW algorithm
      const encryptedData = encryptFile(
        fileContent,
        ApplicationState.fswAlgorithm
      );

      const encryptedFilePath = path.join(
        ApplicationState.encryptedDirectory,
        `${fileName}.encrypted`
      );

      fs.writeFileSync(encryptedFilePath, encryptedData);

      // Notify renderer about new file
      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("file-encrypted", {
          originalName: fileName,
          encryptedPath: encryptedFilePath,
        });

        ApplicationState.mainWindow.webContents.send("log-message", {
          message: `File automatically encrypted: ${fileName} using ${ApplicationState.fswAlgorithm} algorithm`,
          type: "success",
        });
      }
    } catch (error) {
      const errorMsg = `Error processing file: ${error}`;

      if (ApplicationState.mainWindow) {
        ApplicationState.mainWindow.webContents.send("log-message", {
          message: errorMsg,
          type: "error",
        });
      }
    }
  });

  ApplicationState.fileWatcher.on("error", (error) => {
    if (ApplicationState.mainWindow) {
      ApplicationState.mainWindow.webContents.send("log-message", {
        message: `File watcher error: ${String(error)}`,
        type: "error",
      });
    }
  });
}

export function stopFileWatcher() {
  if (ApplicationState.fileWatcher) {
    ApplicationState.fileWatcher.close();
    ApplicationState.fileWatcher = null;
  }
}
