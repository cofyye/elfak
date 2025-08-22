#include "PutGora.h"

PutGora::PutGora(float P, float D, float V, float B, string naziv) : Put(naziv) {
    this->P = P;
    this->D = D;
    this->V = V <= 1 && V >= 0.01 ? V : 1;
    this->B = B;
}

PutGora::~PutGora() {};

float PutGora::odrediKTezine() {
    this->kTezine = this->B * this->D / (this->D + this->P + this->V + this->D / this->P);
    return this->kTezine;
}

float PutGora::odrediKLepote() {
    this->kLepote = (1 - this->B) * this->V;
    return this->kLepote;
}

float PutGora::odrediParametre() {
    return this->P + this->D + this->V + this->B;
}
