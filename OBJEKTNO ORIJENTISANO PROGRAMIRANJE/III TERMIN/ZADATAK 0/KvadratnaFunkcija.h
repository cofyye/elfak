#pragma once
#include <string>
#include "Funkcija.h"

using namespace std;

class KvadratnaFunkcija : public Funkcija {

public:
    KvadratnaFunkcija();
    KvadratnaFunkcija(int k, int n, string naziv);
    ~KvadratnaFunkcija();
    double odrediX(int x);
    void prikaziFunkciju();
};
