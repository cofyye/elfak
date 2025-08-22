#include <iostream>
#include <string.h>
#include <math.h>
#include "EksponencijalnaFunkcija.h"

EksponencijalnaFunkcija::EksponencijalnaFunkcija() : Funkcija() {};
EksponencijalnaFunkcija::EksponencijalnaFunkcija(int k, int n, string naziv) : Funkcija(k, n) {
    this->naziv = new char[50];
    strcpy(this->naziv, naziv.c_str());
}

EksponencijalnaFunkcija::~EksponencijalnaFunkcija() {};

void EksponencijalnaFunkcija::prikaziFunkciju() {
    cout << "Naziv : " << this->naziv << endl;
    cout << "K : " << this->k << endl;
    cout << "N : " << this->n << endl;
}

double EksponencijalnaFunkcija::odrediX(int x) {
    return pow(this->k, (x + this->n));
}
