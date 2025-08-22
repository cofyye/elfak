import { app, BrowserWindow } from "electron";
import path from "path";
import { ApplicationState } from "./src/classes/ApplicationState";
import { registerIpcHandlers } from "./src/handlers/ipc.handlers";

function createWindow() {
  ApplicationState.mainWindow = new BrowserWindow({
    width: 1400,
    height: 900,
    webPreferences: {
      nodeIntegration: true,
      contextIsolation: false,
      devTools: false,
    },
    autoHideMenuBar: true,
    titleBarStyle: "default",
  });

  ApplicationState.mainWindow.loadFile(
    path.join(__dirname, "../src/gui/index.html")
  );

  ApplicationState.mainWindow.on("closed", () => {
    ApplicationState.mainWindow = null;
  });
}

app.whenReady().then(() => {
  registerIpcHandlers();
  createWindow();

  app.on("activate", () => {
    if (ApplicationState.mainWindow === null) {
      createWindow();
    }
  });
});

app.on("window-all-closed", () => {
  if (process.platform !== "darwin") {
    app.quit();
  }
});
