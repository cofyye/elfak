#include <iostream>
#include <string.h>
#include <math.h>
#include "KvadratnaFunkcija.h"

KvadratnaFunkcija::KvadratnaFunkcija() : Funkcija() {};
KvadratnaFunkcija::KvadratnaFunkcija(int k, int n, string naziv) : Funkcija(k, n) {
    this->naziv = new char[50];
    strcpy(this->naziv, naziv.c_str());
}

KvadratnaFunkcija::~KvadratnaFunkcija() {};

void KvadratnaFunkcija::prikaziFunkciju() {
    cout << "Naziv : " << this->naziv << endl;
    cout << "K : " << this->k << endl;
    cout << "N : " << this->n << endl;
}

double KvadratnaFunkcija::odrediX(int x) {
    return pow((x+this->n), this->k);
}
