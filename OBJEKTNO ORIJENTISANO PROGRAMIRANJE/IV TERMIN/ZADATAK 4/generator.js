// OVO JE GENERATOR ZA RANDOM BROJEVE ZA SVAKI SEGMENT PUTA. MORA DA SE IMA NODEJS KAKO BI SE OVO POKRENULO

const fs = require("fs");

// more
for (i = 0; i < 777; i++) {
  fs.appendFileSync(
    "more.txt",
    Math.floor(Math.random() * 1000 + 1).toString() + "\n"
  );
  fs.appendFileSync(
    "more.txt",
    Math.floor(Math.random() * 1000 + 1).toString() + "\n"
  );
  fs.appendFileSync(
    "more.txt",
    Math.floor(Math.random() * 12).toString() + "\n"
  );
}

// gora
for (i = 0; i < 777; i++) {
  fs.appendFileSync(
    "gora.txt",
    Math.floor(Math.random() * 1000 + 1).toString() + "\n"
  );
  fs.appendFileSync(
    "gora.txt",
    Math.floor(Math.random() * 1000 + 1).toString() + "\n"
  );
  fs.appendFileSync(
    "gora.txt",
    Math.floor(Math.random() * 1000 + 1).toString() + "\n"
  );
  fs.appendFileSync("gora.txt", (Math.random() * 1 + 0.01).toFixed(2) + "\n");
}

//mocvara
for (i = 0; i < 777; i++) {
  fs.appendFileSync(
    "mocvara.txt",
    Math.floor(Math.random() * 1000 + 1).toString() + "\n"
  );
  fs.appendFileSync("mocvara.txt", (Math.random() * 0.99).toFixed(2) + "\n");
}

//planina
for (i = 0; i < 777; i++) {
  fs.appendFileSync(
    "planina.txt",
    Math.floor(Math.random() * 1000 + 1).toString() + "\n"
  );
  fs.appendFileSync(
    "planina.txt",
    Math.floor(Math.random() * 1000 + 1).toString() + "\n"
  );
}
