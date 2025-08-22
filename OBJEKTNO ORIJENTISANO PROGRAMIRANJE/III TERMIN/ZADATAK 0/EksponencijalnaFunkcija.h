#pragma once
#include <string>
#include "Funkcija.h"

using namespace std;

class EksponencijalnaFunkcija : public Funkcija {

public:
    EksponencijalnaFunkcija();
    EksponencijalnaFunkcija(int k, int n, string naziv);
    ~EksponencijalnaFunkcija();
    double odrediX(int x);
    void prikaziFunkciju();
};
