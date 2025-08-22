#include <iostream>
#include <string>
#include <sstream>
#include "Azdaja.h"

using namespace std;

Azdaja::Azdaja() {
    this->more.open("more.txt");
    this->gora.open("gora.txt");
    this->mocvara.open("mocvara.txt");
    this->planina.open("planina.txt");
    this->sTezine.open("tezina.txt");
    this->sLepote.open("lepota.txt");
}

Azdaja::Azdaja(char* more, char* gora, char* mocvara, char* planina, char* sTezine, char* sLepote) {
    this->more.open(more);
    this->gora.open(gora);
    this->mocvara.open(mocvara);
    this->planina.open(planina);
    this->sTezine.open(sTezine);
    this->sLepote.open(sLepote);
}

Azdaja::~Azdaja() {
    this->more.close();
    this->gora.close();
    this->mocvara.close();
    this->planina.close();
    this->sTezine.close();
    this->sLepote.close();
}

void Azdaja::uzmiMore() {
    string line;
    int i = 1, index = 0, MSV;
    float P, S;

    if(this->more.is_open()) {
        while(getline(this->more, line))  {
            stringstream ss;
            if(i == 1) {
                ss << line;
                ss >> P;
            } else if(i == 2) {
                ss << line;
                ss >> S;
            } else if(i == 3) {
                ss << line;
                ss >> MSV;
            }

            i++;

            if(i > 3) {
                stringstream ss;
                string str;
                ss << (index+1);
                ss >> str;
                string naziv = "More " + str;
                PutMora more(P, S, MSV, naziv);
                this->nizMora[index] = more;
                index++;
                i = 1;
            }
        }
    } else {
        exit(1);
    }
}

void Azdaja::uzmiGoru() {
    string line;
    int i = 1, index = 0;
    float P, D, V, B;

    if(this->gora.is_open()) {
        while(getline(this->gora, line))  {
            stringstream ss;
            if(i == 1) {
                ss << line;
                ss >> P;
            } else if(i == 2) {
                ss << line;
                ss >> D;
            } else if(i == 3) {
                ss << line;
                ss >> V;
            } else if(i == 4) {
                ss << line;
                ss >> B;
            }

            i++;

            if(i > 4) {
                stringstream ss;
                string str;
                ss << (index+1);
                ss >> str;
                string naziv = "Gora " + str;
                PutGora gora(P, D, V, B, naziv);
                this->nizGora[index] = gora;
                index++;
                i = 1;
            }
        }
    } else {
        exit(1);
    }
}

void Azdaja::uzmiMocvaru() {
    string line;
    int i = 1, index = 0;
    float P, Z;

    if(this->mocvara.is_open()) {
        while(getline(this->mocvara, line))  {
            stringstream ss;
            if(i == 1) {
                ss << line;
                ss >> P;
            } else if(i == 2) {
                ss << line;
                ss >> Z;
            }

            i++;

            if(i > 2) {
                stringstream ss;
                string str;
                ss << (index+1);
                ss >> str;
                string naziv = "Mocvara " + str;
                PutMocvara mocvara(static_cast<PutMora>(this->nizMora[index]), P, Z, naziv);
                this->nizMocvara[index] = mocvara;
                index++;
                i = 1;
            }
        }
    } else {
        exit(1);
    }
}

void Azdaja::uzmiPlaninu() {
    string line;
    int i = 1, index = 0;
    float P, H;

    if(this->planina.is_open()) {
        while(getline(this->planina, line))  {
            stringstream ss;
            if(i == 1) {
                ss << line;
                ss >> P;
            } else if(i == 2) {
                ss << line;
                ss >> H;
            }

            i++;

            if(i > 2) {
                stringstream ss;
                string str;
                ss << (index+1);
                ss >> str;
                string naziv = "Planina " + str;
                PutPlanina planina(P, H, naziv);
                this->nizPlanina[index] = planina;
                index++;
                i = 1;
            }
        }
    } else {
        exit(1);
    }
}

void Azdaja::sortirajTezinu() {
    const int SIZE = 777;
    PutMora nizPutMora[SIZE];
    PutGora nizPutGora[SIZE];
    PutMocvara nizPutMocvara[SIZE];
    PutPlanina nizPutPlanina[SIZE];
    int index = 0;

    for(int i = 0; i < SIZE; i++, index++) {
        this->nizMora[i].odrediKTezine();
        nizPutMora[index] = this->nizMora[i];
    }

    index = 0;

    for(int i = 0; i < SIZE; i++, index++) {
        this->nizGora[i].odrediKTezine();
        nizPutGora[index] = this->nizGora[i];
    }

    index = 0;

    for(int i = 0; i < SIZE; i++, index++) {
        this->nizMocvara[i].odrediKTezine();
        nizPutMocvara[index] = this->nizMocvara[i];
    }

    index = 0;

    for(int i = 0; i < SIZE; i++, index++) {
        this->nizPlanina[i].odrediKTezine();
        nizPutPlanina[index] = this->nizPlanina[i];
    }

    for (int i = 0; i < SIZE; i++) {
        for (int j = i + 1; j < SIZE; j++) {
            if (this->nizMora[i].kTezine < this->nizMora[j].kTezine) {
                PutMora temp = this->nizMora[i];
                this->nizMora[i] = this->nizMora[j];
                this->nizMora[j] = temp;
            }
        }
    }

    this->sTezine << "----------------------------- MORE -----------------------------" << endl << endl;;

    for(int i = 0; i < SIZE; i++) {
        this->sTezine << "| Naziv : " << this->nizMora[i].naziv << " | P = " << this->nizMora[i].P << " | S = " << this->nizMora[i].S << " | MSV = " << this->nizMora[i].MSV << " | kTezine = " << this->nizMora[i].kTezine << " |" << endl;
    }

    for (int i = 0; i < SIZE; i++) {
        for (int j = i + 1; j < SIZE; j++) {
            if (this->nizGora[i].kTezine < this->nizGora[j].kTezine) {
                PutGora temp = this->nizGora[i];
                this->nizGora[i] = this->nizGora[j];
                this->nizGora[j] = temp;
            }
        }
    }
    this->sTezine << endl;
    this->sTezine << "----------------------------------------------------------------" << endl << endl;
    this->sTezine << "----------------------------- GORA -----------------------------" << endl << endl;

    for(int i = 0; i < SIZE; i++) {
        this->sTezine << "| Naziv : " << this->nizGora[i].naziv << " | P = " << this->nizGora[i].P << " | D = " << this->nizGora[i].D << " | V = " << this->nizGora[i].V << " | B = " << this->nizGora[i].B << " | kTezine = " << this->nizGora[i].kTezine << " |" << endl;
    }
    this->sTezine << endl;
    this->sTezine << "----------------------------------------------------------------" << endl << endl;


    for (int i = 0; i < SIZE; i++) {
        for (int j = i + 1; j < SIZE; j++) {
            if (this->nizMocvara[i].kTezine < this->nizMocvara[j].kTezine) {
                PutMocvara temp = this->nizMocvara[i];
                this->nizMocvara[i] = this->nizMocvara[j];
                this->nizMocvara[j] = temp;
            }
        }
    }

    this->sTezine << "--------------------------- MOCVARA ----------------------------" << endl << endl;

    for(int i = 0; i < SIZE; i++) {
        this->sTezine << "| Naziv : " << this->nizMocvara[i].naziv << " | P = " << this->nizMocvara[i].P << " | Z = " << this->nizMocvara[i].Z << " | S = " << this->nizMocvara[i].S << " | kTezine = " << this->nizMocvara[i].kTezine << " |" << endl;
    }
    this->sTezine << endl;
    this->sTezine << "----------------------------------------------------------------" << endl << endl;

    for (int i = 0; i < SIZE; i++) {
        for (int j = i + 1; j < SIZE; j++) {
            if (this->nizPlanina[i].kTezine < this->nizPlanina[j].kTezine) {
                PutPlanina temp = this->nizPlanina[i];
                this->nizPlanina[i] = this->nizPlanina[j];
                this->nizPlanina[j] = temp;
            }
        }
    }

    this->sTezine << "--------------------------- PLANINA ----------------------------" << endl << endl;

    for(int i = 0; i < SIZE; i++) {
        this->sTezine << "| Naziv : " << this->nizPlanina[i].naziv << " | P = " << this->nizPlanina[i].P << " | H = " << this->nizPlanina[i].H << " | kTezine = " << this->nizPlanina[i].kTezine << " |" << endl;
    }
    this->sTezine << endl;
    this->sTezine << "----------------------------------------------------------------" << endl << endl;
}

void Azdaja::sortirajLepotu() {
    const int SIZE = 777;
    PutMora nizPutMora[SIZE];
    PutGora nizPutGora[SIZE];
    PutMocvara nizPutMocvara[SIZE];
    PutPlanina nizPutPlanina[SIZE];
    int index = 0;

    for(int i = 0; i < SIZE; i++, index++) {
        this->nizMora[i].odrediKLepote();
        nizPutMora[index] = this->nizMora[i];
    }

    index = 0;

    for(int i = 0; i < SIZE; i++, index++) {
        this->nizGora[i].odrediKLepote();
        nizPutGora[index] = this->nizGora[i];
    }

    index = 0;

    for(int i = 0; i < SIZE; i++, index++) {
        this->nizMocvara[i].odrediKLepote();
        nizPutMocvara[index] = this->nizMocvara[i];
    }

    index = 0;

    for(int i = 0; i < SIZE; i++, index++) {
        this->nizPlanina[i].odrediKLepote();
        nizPutPlanina[index] = this->nizPlanina[i];
    }

    for (int i = 0; i < SIZE; i++) {
        for (int j = i + 1; j < SIZE; j++) {
            if (this->nizMora[i].kLepote < this->nizMora[j].kLepote) {
                PutMora temp = this->nizMora[i];
                this->nizMora[i] = this->nizMora[j];
                this->nizMora[j] = temp;
            }
        }
    }

    this->sLepote << "----------------------------- MORE -----------------------------" << endl << endl;;

    for(int i = 0; i < SIZE; i++) {
        this->sLepote << "| Naziv : " << this->nizMora[i].naziv << " | P = " << this->nizMora[i].P << " | S = " << this->nizMora[i].S << " | MSV = " << this->nizMora[i].MSV << " | kLepote = " << this->nizMora[i].kLepote << " |" << endl;
    }

    for (int i = 0; i < SIZE; i++) {
        for (int j = i + 1; j < SIZE; j++) {
            if (this->nizGora[i].kLepote < this->nizGora[j].kLepote) {
                PutGora temp = this->nizGora[i];
                this->nizGora[i] = this->nizGora[j];
                this->nizGora[j] = temp;
            }
        }
    }
    this->sLepote << endl;
    this->sLepote << "----------------------------------------------------------------" << endl << endl;
    this->sLepote << "----------------------------- GORA -----------------------------" << endl << endl;

    for(int i = 0; i < SIZE; i++) {
        this->sLepote << "| Naziv : " << this->nizGora[i].naziv << " | P = " << this->nizGora[i].P << " | D = " << this->nizGora[i].D << " | V = " << this->nizGora[i].V << " | B = " << this->nizGora[i].B << " | kLepote = " << this->nizGora[i].kLepote << " |" << endl;
    }
    this->sLepote << endl;
    this->sLepote << "----------------------------------------------------------------" << endl << endl;


    for (int i = 0; i < SIZE; i++) {
        for (int j = i + 1; j < SIZE; j++) {
            if (this->nizMocvara[i].kLepote < this->nizMocvara[j].kLepote) {
                PutMocvara temp = this->nizMocvara[i];
                this->nizMocvara[i] = this->nizMocvara[j];
                this->nizMocvara[j] = temp;
            }
        }
    }

    this->sLepote << "--------------------------- MOCVARA ----------------------------" << endl << endl;

    for(int i = 0; i < SIZE; i++) {
        this->sLepote << "| Naziv : " << this->nizMocvara[i].naziv << " | P = " << this->nizMocvara[i].P << " | Z = " << this->nizMocvara[i].Z << " | S = " << this->nizMocvara[i].S << " | kLepote = " << this->nizMocvara[i].kLepote << " |" << endl;
    }
    this->sLepote << endl;
    this->sLepote << "----------------------------------------------------------------" << endl << endl;

    for (int i = 0; i < SIZE; i++) {
        for (int j = i + 1; j < SIZE; j++) {
            if (this->nizPlanina[i].kLepote < this->nizPlanina[j].kLepote) {
                PutPlanina temp = this->nizPlanina[i];
                this->nizPlanina[i] = this->nizPlanina[j];
                this->nizPlanina[j] = temp;
            }
        }
    }

    this->sLepote << "--------------------------- PLANINA ----------------------------" << endl << endl;

    for(int i = 0; i < SIZE; i++) {
        this->sLepote << "| Naziv : " << this->nizPlanina[i].naziv << " | P = " << this->nizPlanina[i].P << " | H = " << this->nizPlanina[i].H << " | kLepote = " << this->nizPlanina[i].kLepote << " |" << endl;
    }
    this->sLepote << endl;
    this->sLepote << "----------------------------------------------------------------" << endl << endl;

}

