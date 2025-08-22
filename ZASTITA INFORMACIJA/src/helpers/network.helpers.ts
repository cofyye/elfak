export async function getLocalIPAddress(): Promise<string> {
  return new Promise((resolve) => {
    const { networkInterfaces } = require("os");
    const nets = networkInterfaces();

    for (const name of Object.keys(nets)) {
      for (const net of nets[name]) {
        // Skip over non-IPv4 and internal (i.e. 127.0.0.1) addresses
        if (net.family === "IPv4" && !net.internal) {
          resolve(net.address);
          return;
        }
      }
    }
    resolve("127.0.0.1");
  });
}
