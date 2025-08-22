#include "PutPlanina.h"

PutPlanina::PutPlanina(float P, float H, string naziv) : Put(naziv) {
    this->P = P;
    this->H = H;
}

PutPlanina::~PutPlanina() {};

float PutPlanina::odrediKTezine() {
    this->kTezine = this->P * this->H / (this->P + this->H);
    return this->kTezine;
}

float PutPlanina::odrediKLepote() {
    this->kLepote = (1 - this->P) * this->H;
    return this->kLepote;
}

float PutPlanina::odrediParametre() {
    return this->P + this->H;
}
