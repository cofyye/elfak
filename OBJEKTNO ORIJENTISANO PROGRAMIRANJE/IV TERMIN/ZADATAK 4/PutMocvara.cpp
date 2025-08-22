#include "PutMocvara.h"

PutMocvara::PutMocvara(PutMora* putmora, float P, float Z, string naziv) : Put(naziv) {
    this->P = P;
    this->Z = Z >= 0 && Z <= 0.99 ? Z : 0.99;
    this->S = putmora->S;
}

PutMocvara::PutMocvara(PutMora putmora, float P, float Z, string naziv) : Put(naziv) {
    this->P = P;
    this->Z = Z >= 0 && Z <= 0.99 ? Z : 0.99;
    this->S = putmora.S;
}

PutMocvara::~PutMocvara() {};

float PutMocvara::odrediKTezine() {
    this->kTezine = (1 - this->Z) * (1 - 1 / (this->S * this->P));
    return this->kTezine;
}

float PutMocvara::odrediKLepote() {
    this->kLepote = (1 - this->odrediKTezine());
    return this->kLepote;
}

float PutMocvara::odrediParametre() {
    return this->P + this->Z + this->S;
}
