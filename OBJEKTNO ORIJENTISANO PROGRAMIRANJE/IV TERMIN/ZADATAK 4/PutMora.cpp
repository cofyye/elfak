#include "PutMora.h"

PutMora::PutMora(float P, float S, int MSV, string naziv) : Put(naziv) {
    this->P = P;
    this->S = S;
    this->MSV = MSV >= 0 && MSV <= 12 ? MSV : 1;
}

PutMora::~PutMora() {};

float PutMora::odrediKTezine() {
    this->kTezine = this->MSV * this->P / (this->odrediKLepote() - 1);
    return this->kTezine;
}

float PutMora::odrediKLepote() {
    this->kLepote = this->MSV * this->S / this->P;
    return this->kLepote;
}

float PutMora::odrediParametre() {
    return this->P + this->S + this->MSV;
}
