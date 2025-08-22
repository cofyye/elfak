#include <iostream>
#include <string.h>
#include "Funkcija.h"

using namespace std;

Funkcija::Funkcija() {
    this->k = 1;
    this->n = 0;
    this->naziv = new char[50];
    strcpy(this->naziv, "Linearna funkcija");
}

Funkcija::Funkcija(int k, int n) {
    this->k = k;
    this->n = n;
    this->naziv = new char[50];
    strcpy(this->naziv, "Bezimena funkcija");
}

Funkcija::~Funkcija() {
    if(this->naziv != 0) {
        delete[] this->naziv;
    }
};

void Funkcija::prikaziFunkciju(){
    cout << "Naziv : " << this->naziv<< endl;
    cout << "K : " << this->k << endl;
    cout << "N : " << this->n << endl;
};

double Funkcija::odrediX(int x) {
    return this->k*x+this->n;
}
