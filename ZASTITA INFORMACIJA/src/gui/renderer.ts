const { ipcRenderer } = require("electron");

interface SelectedFile {
  path: string;
  name: string;
}

// UI Elements
const logElement = document.getElementById("log") as HTMLDivElement;
const chooseFileButton = document.getElementById(
  "chooseFile"
) as HTMLButtonElement;
const chooseFileText = document.getElementById(
  "chooseFileText"
) as HTMLSpanElement;
const encryptFileStatus = document.getElementById(
  "encryptFileStatus"
) as HTMLDivElement;
const encryptButton = document.getElementById("encrypt") as HTMLButtonElement;
const algorithmSelect = document.getElementById(
  "algorithm"
) as HTMLSelectElement;

const chooseEncryptedFileButton = document.getElementById(
  "chooseEncryptedFile"
) as HTMLButtonElement;
const chooseEncryptedFileText = document.getElementById(
  "chooseEncryptedFileText"
) as HTMLSpanElement;
const decryptFileStatus = document.getElementById(
  "decryptFileStatus"
) as HTMLDivElement;
const decryptButton = document.getElementById("decrypt") as HTMLButtonElement;
const decryptAlgorithmSelect = document.getElementById(
  "decryptAlgorithmSelect"
) as HTMLSelectElement;

const fswToggle = document.getElementById("fswToggle") as HTMLInputElement;
const fswToggleLabel = document.getElementById(
  "fswToggleLabel"
) as HTMLSpanElement;
const targetDirDisplay = document.getElementById(
  "targetDirDisplay"
) as HTMLSpanElement;
const encryptedDirDisplay = document.getElementById(
  "encryptedDirDisplay"
) as HTMLSpanElement;
const selectTargetDir = document.getElementById(
  "selectTargetDir"
) as HTMLButtonElement;
const selectEncryptedDir = document.getElementById(
  "selectEncryptedDir"
) as HTMLButtonElement;
const fswAlgorithmSelect = document.getElementById(
  "fswAlgorithmSelect"
) as HTMLSelectElement;

// TCP Elements
const tcpClientSection = document.getElementById(
  "tcpClientSection"
) as HTMLDivElement;
const tcpServerSection = document.getElementById(
  "tcpServerSection"
) as HTMLDivElement;
const clientHostInput = document.getElementById(
  "clientHostInput"
) as HTMLInputElement;
const clientPortInput = document.getElementById(
  "clientPortInput"
) as HTMLInputElement;
const serverHostDisplay = document.getElementById(
  "serverHostDisplay"
) as HTMLInputElement;
const serverPortInput = document.getElementById(
  "serverPortInput"
) as HTMLInputElement;
const chooseTcpFile = document.getElementById(
  "chooseTcpFile"
) as HTMLButtonElement;
const chooseTcpFileText = document.getElementById(
  "chooseTcpFileText"
) as HTMLSpanElement;
const tcpFileStatus = document.getElementById(
  "tcpFileStatus"
) as HTMLDivElement;
const tcpAlgorithmSelect = document.getElementById(
  "tcpAlgorithmSelect"
) as HTMLSelectElement;
const sendFileBtn = document.getElementById("sendFileBtn") as HTMLButtonElement;
const startServerBtn = document.getElementById(
  "startServerBtn"
) as HTMLButtonElement;
const stopServerBtn = document.getElementById(
  "stopServerBtn"
) as HTMLButtonElement;
const serverAlgorithmSelect = document.getElementById(
  "serverAlgorithmSelect"
) as HTMLSelectElement;

// Secret Key Elements
const encryptSecretKeyInput = document.getElementById(
  "encryptSecretKeyInput"
) as HTMLInputElement;
const generateEncryptKeyBtn = document.getElementById(
  "generateEncryptKey"
) as HTMLButtonElement;
const decryptSecretKeyInput = document.getElementById(
  "decryptSecretKeyInput"
) as HTMLInputElement;

let selectedFile: SelectedFile | null = null;
let selectedEncryptedFile: SelectedFile | null = null;
let selectedTcpFile: SelectedFile | null = null;

type LogType = "info" | "success" | "error" | "warning";

// Logging function
function log(message: string, type: LogType = "info"): void {
  const entry = document.createElement("div");
  entry.className = `log-entry ${type}`;
  entry.textContent = `[${new Date().toLocaleTimeString()}] ${message}`;
  logElement.appendChild(entry);
  logElement.scrollTop = logElement.scrollHeight;
}

// UI Status Update Functions
function updateEncryptFileStatus(file: SelectedFile | null): void {
  if (file) {
    chooseFileText.textContent = file.name;
    chooseFileText.parentElement?.classList.add("has-file");
    encryptFileStatus.className = "file-status selected";
    encryptFileStatus.innerHTML = "<span>✅ File selected</span>";
    encryptButton.disabled = false;
  } else {
    chooseFileText.textContent = "Click to choose file...";
    chooseFileText.parentElement?.classList.remove("has-file");
    encryptFileStatus.className = "file-status empty";
    encryptFileStatus.innerHTML = "<span>⚪ No file selected</span>";
    encryptButton.disabled = true;
  }
}

function updateDecryptFileStatus(file: SelectedFile | null): void {
  if (file) {
    chooseEncryptedFileText.textContent = file.name;
    chooseEncryptedFileText.parentElement?.classList.add("has-file");
    decryptFileStatus.className = "file-status selected";
    decryptFileStatus.innerHTML = "<span>✅ File selected</span>";
    decryptButton.disabled = false;
  } else {
    chooseEncryptedFileText.textContent = "Click to choose encrypted file...";
    chooseEncryptedFileText.parentElement?.classList.remove("has-file");
    decryptFileStatus.className = "file-status empty";
    decryptFileStatus.innerHTML = "<span>⚪ No file selected</span>";
    decryptButton.disabled = true;
  }
}

function updateTcpFileStatus(file: SelectedFile | null): void {
  if (file) {
    chooseTcpFileText.textContent = file.name;
    chooseTcpFileText.parentElement?.classList.add("has-file");
    tcpFileStatus.className = "file-status selected";
    tcpFileStatus.innerHTML = "<span>✅ File selected</span>";
    sendFileBtn.disabled = false;
  } else {
    chooseTcpFileText.textContent = "Click to choose file...";
    chooseTcpFileText.parentElement?.classList.remove("has-file");
    tcpFileStatus.className = "file-status empty";
    tcpFileStatus.innerHTML = "<span>⚪ No file selected</span>";
    sendFileBtn.disabled = true;
  }
}

// File selection
chooseFileButton.addEventListener("click", async () => {
  try {
    const result = await ipcRenderer.invoke("choose-file");
    if (result) {
      selectedFile = result;
      updateEncryptFileStatus(selectedFile);
      log(`Selected file: ${result.name}`, "info");
    }
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    log(`Error selecting file: ${errorMessage}`, "error");
  }
});

// Encryption
encryptButton.addEventListener("click", async () => {
  if (!selectedFile) {
    log("Please select a file first", "error");
    return;
  }

  // Check if directories are configured
  try {
    const status = await ipcRenderer.invoke("get-fsw-status");
    if (!status.encryptedDirectory) {
      log(
        "Encrypted directory not configured. Please configure directories first.",
        "error"
      );
      return;
    }
  } catch (error) {
    log("Error checking configuration", "error");
    return;
  }

  try {
    const result = await ipcRenderer.invoke(
      "encrypt-file",
      selectedFile.path,
      algorithmSelect.value
    );
    log(`File encrypted successfully: ${result.outputPath}`, "success");
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    // Extract just the main error message without the full stack
    const cleanError = errorMessage.includes("Error invoking remote method")
      ? errorMessage.split(": ").slice(-1)[0]
      : errorMessage;
    log(`Encryption failed: ${cleanError}`, "error");
  }
});

// Encrypted file selection
chooseEncryptedFileButton.addEventListener("click", async () => {
  try {
    const result = await ipcRenderer.invoke("choose-encrypted-file");
    if (result) {
      selectedEncryptedFile = result;
      updateDecryptFileStatus(selectedEncryptedFile);
      log(`Selected encrypted file: ${result.name}`, "info");
    }
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    log(`Error selecting encrypted file: ${errorMessage}`, "error");
  }
});

// Decryption
decryptButton.addEventListener("click", async () => {
  if (!selectedEncryptedFile) {
    log("Please select an encrypted file first", "error");
    return;
  }

  try {
    const algorithm = decryptAlgorithmSelect.value;
    const result = await ipcRenderer.invoke(
      "decrypt-file",
      selectedEncryptedFile.path,
      algorithm
    );
    log(`Using ${algorithm} algorithm for decryption`, "info");

    log(`File decrypted successfully: ${result.outputPath}`, "success");
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    // Extract just the main error message without the full stack
    const cleanError = errorMessage.includes("Error invoking remote method")
      ? errorMessage.split(": ").slice(-1)[0]
      : errorMessage;
    log(`Decryption failed: ${cleanError}`, "error");
  }
});

// FSW Toggle
fswToggle.addEventListener("change", async () => {
  try {
    const result = await ipcRenderer.invoke("toggle-fsw", fswToggle.checked);

    if (result.error) {
      log(result.error, "error");
      // Reset toggle if there was an error
      fswToggle.checked = false;
      fswToggleLabel.textContent = "Disabled";
      tcpClientSection.classList.add("hidden");
      tcpServerSection.classList.add("hidden");
      return;
    }

    log(
      `File System Watcher ${result.enabled ? "enabled" : "disabled"}`,
      result.enabled ? "success" : "info"
    );
    fswToggleLabel.textContent = result.enabled ? "Enabled" : "Disabled";

    // Show/hide TCP sections based on FSW status
    if (result.enabled) {
      tcpClientSection.classList.remove("hidden");
      tcpServerSection.classList.remove("hidden");
    } else {
      tcpClientSection.classList.add("hidden");
      tcpServerSection.classList.add("hidden");
    }

    await updateFSWStatus();
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    log(`Error toggling File System Watcher: ${errorMessage}`, "error");
    // Reset toggle if there was an error
    fswToggle.checked = false;
    fswToggleLabel.textContent = "Disabled";
  }
});

// Directory selection
selectTargetDir.addEventListener("click", async () => {
  log("Selecting target directory...", "info");
  try {
    const result = await ipcRenderer.invoke(
      "choose-directory",
      "Choose Target Directory"
    );
    if (result) {
      await ipcRenderer.invoke("set-target-directory", result);
      log(`Target directory set to: ${result}`, "info");
      await updateFSWStatus();
    }
  } catch (error) {
    log("Error selecting target directory", "error");
  }
});

selectEncryptedDir.addEventListener("click", async () => {
  log("Selecting encrypted directory...", "info");
  try {
    const result = await ipcRenderer.invoke(
      "choose-directory",
      "Choose Encrypted Files Directory"
    );
    if (result) {
      await ipcRenderer.invoke("set-encrypted-directory", result);
      log(`Encrypted directory set to: ${result}`, "info");
      await updateFSWStatus();
    }
  } catch (error) {
    log("Error selecting encrypted directory", "error");
  }
});

// FSW Algorithm selection
fswAlgorithmSelect.addEventListener("change", async () => {
  try {
    const selectedAlgorithm = fswAlgorithmSelect.value;
    await ipcRenderer.invoke("set-fsw-algorithm", selectedAlgorithm);
    log(`FSW algorithm set to: ${selectedAlgorithm}`, "info");
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    log(`Error setting FSW algorithm: ${errorMessage}`, "error");
  }
});

// Server Algorithm selection
serverAlgorithmSelect.addEventListener("change", async () => {
  try {
    const selectedAlgorithm = serverAlgorithmSelect.value;
    await ipcRenderer.invoke("set-server-algorithm", selectedAlgorithm);
    log(`Server algorithm set to: ${selectedAlgorithm}`, "info");
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    log(`Error setting server algorithm: ${errorMessage}`, "error");
  }
});

// Update FSW status
async function updateFSWStatus() {
  try {
    const status = await ipcRenderer.invoke("get-fsw-status");
    const config = await ipcRenderer.invoke("get-config");

    // Update display elements
    targetDirDisplay.textContent = status.targetDirectory || "Not set";
    encryptedDirDisplay.textContent = status.encryptedDirectory || "Not set";

    fswToggle.checked = status.enabled;
    fswToggleLabel.textContent = status.enabled ? "Enabled" : "Disabled";

    // Show/hide TCP sections based on FSW status
    if (status.enabled) {
      tcpClientSection.classList.remove("hidden");
      tcpServerSection.classList.remove("hidden");
    } else {
      tcpClientSection.classList.add("hidden");
      tcpServerSection.classList.add("hidden");
    }

    // Update FSW algorithm select
    if (config.fswAlgorithm) {
      fswAlgorithmSelect.value = config.fswAlgorithm;
    }

    // Update server algorithm select
    if (config.serverAlgorithm) {
      serverAlgorithmSelect.value = config.serverAlgorithm;
    }

    // Update TCP client settings
    if (config.tcpHost) {
      clientHostInput.value = config.tcpHost;
    }
    if (config.tcpPort) {
      clientPortInput.value = config.tcpPort.toString();
      serverPortInput.value = config.tcpPort.toString();
    }

    // Update secret key inputs
    if (config.encryptSecretKey) {
      encryptSecretKeyInput.value = config.encryptSecretKey;
    }
    if (config.decryptSecretKey) {
      decryptSecretKeyInput.value = config.decryptSecretKey;
    }
  } catch (error) {
    log("Error getting FSW status", "error");
  }
}

// Listen for file encryption events from main process
ipcRenderer.on("file-encrypted", (_: any, data: any) => {
  log(`File automatically encrypted: ${data.originalName}`, "success");
});

// Listen for log messages from main process
ipcRenderer.on("log-message", (_: any, data: any) => {
  log(data.message, data.type);
});

// TCP Event Listeners
clientHostInput.addEventListener("change", async () => {
  try {
    const port = parseInt(clientPortInput.value) || 3000;
    await ipcRenderer.invoke("set-tcp-config", clientHostInput.value, port);
  } catch (error) {
    log("Error setting TCP host", "error");
  }
});

clientPortInput.addEventListener("change", async () => {
  try {
    const port = parseInt(clientPortInput.value);
    if (port >= 1024 && port <= 65535) {
      await ipcRenderer.invoke("set-tcp-config", clientHostInput.value, port);
    } else {
      log("Port must be between 1024 and 65535", "error");
      clientPortInput.value = "3000";
    }
  } catch (error) {
    log("Error setting TCP port", "error");
  }
});

serverPortInput.addEventListener("change", async () => {
  // This updates the port for the server, but doesn't need to call IPC since server starts with this value
  const port = parseInt(serverPortInput.value);
  if (port < 1024 || port > 65535) {
    log("Port must be between 1024 and 65535", "error");
    serverPortInput.value = "3000";
  }
});

chooseTcpFile.addEventListener("click", async () => {
  try {
    const result = await ipcRenderer.invoke("choose-file");
    if (result) {
      selectedTcpFile = result;
      updateTcpFileStatus(selectedTcpFile);
      log(`TCP file selected: ${result.name}`, "info");
    }
  } catch (error) {
    log("Error selecting TCP file", "error");
  }
});

sendFileBtn.addEventListener("click", async () => {
  if (!selectedTcpFile) {
    log("Please select a file first", "error");
    return;
  }

  const host = clientHostInput.value.trim();
  const port = parseInt(clientPortInput.value);

  if (!host) {
    log("Please enter a target host", "error");
    return;
  }

  if (port < 1024 || port > 65535) {
    log("Please enter a valid port (1024-65535)", "error");
    return;
  }

  try {
    await ipcRenderer.invoke(
      "send-tcp-file",
      host,
      port,
      selectedTcpFile.path,
      tcpAlgorithmSelect.value
    );
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    log(`TCP send error: ${errorMessage}`, "error");
  }
});

startServerBtn.addEventListener("click", async () => {
  const port = parseInt(serverPortInput.value);

  if (port < 1024 || port > 65535) {
    log("Please enter a valid port (1024-65535)", "error");
    return;
  }

  try {
    const result = await ipcRenderer.invoke("start-tcp-server", port);

    if (result.success) {
      startServerBtn.classList.add("hidden");
      stopServerBtn.classList.remove("hidden");
    }
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    log(`Failed to start server: ${errorMessage}`, "error");
  }
});

stopServerBtn.addEventListener("click", async () => {
  try {
    const result = await ipcRenderer.invoke("stop-tcp-server");

    if (result.success) {
      stopServerBtn.classList.add("hidden");
      startServerBtn.classList.remove("hidden");
    }
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : String(error);
    log(`Failed to stop server: ${errorMessage}`, "error");
  }
});

// Secret Key Event Listeners
encryptSecretKeyInput.addEventListener("change", async () => {
  const newKey = encryptSecretKeyInput.value.trim();
  try {
    await ipcRenderer.invoke("set-encrypt-secret-key", newKey);
    log("Encrypt secret key updated", "success");
  } catch (error) {
    log("Error setting encrypt secret key", "error");
  }
});

decryptSecretKeyInput.addEventListener("change", async () => {
  const newKey = decryptSecretKeyInput.value.trim();
  try {
    await ipcRenderer.invoke("set-decrypt-secret-key", newKey);
    log("Decrypt secret key updated", "success");
  } catch (error) {
    log("Error setting decrypt secret key", "error");
  }
});

generateEncryptKeyBtn.addEventListener("click", async () => {
  try {
    const newKey = await ipcRenderer.invoke("generate-new-encrypt-key");
    encryptSecretKeyInput.value = newKey;
    log("New random encrypt key generated", "success");
  } catch (error) {
    log("Error generating new encrypt key", "error");
  }
});

// Initialize app
document.addEventListener("DOMContentLoaded", async () => {
  try {
    await updateFSWStatus();
    log("File Encryption Suite initialized", "success");

    // Get local IP address for server display
    try {
      const localIP = await ipcRenderer.invoke("get-local-ip");
      serverHostDisplay.value = localIP;
    } catch (error) {
      serverHostDisplay.value = "127.0.0.1";
    }

    // Initialize file status
    updateEncryptFileStatus(null);
    updateDecryptFileStatus(null);
    updateTcpFileStatus(null);
  } catch (error) {
    log("Error initializing application", "error");
  }
});
