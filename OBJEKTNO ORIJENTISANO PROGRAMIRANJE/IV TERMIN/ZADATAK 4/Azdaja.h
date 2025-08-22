#pragma once
#include <fstream>
#include "PutMora.h"
#include "PutGora.h"
#include "PutMocvara.h"
#include "PutPlanina.h"

class Azdaja {
private:
    PutMora nizMora[777];
    PutGora nizGora[777];
    PutMocvara nizMocvara[777];
    PutPlanina nizPlanina[777];
    ifstream more;
    ifstream gora;
    ifstream mocvara;
    ifstream planina;
    ofstream sTezine;
    ofstream sLepote;
public:
    Azdaja();
    Azdaja(char* more, char* gora, char* mocvara, char* planina, char* sTezine, char* sLepote);
    ~Azdaja();
    void uzmiMore();
    void uzmiGoru();
    void uzmiMocvaru();
    void uzmiPlaninu();
    void sortirajTezinu();
    void sortirajLepotu();
    void upisiKTezine();
    void upisiKLepote();
};
