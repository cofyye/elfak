#pragma once
#include <string>
#include "Put.h"

using namespace std;

class PutGora : public Put {
protected:
    float P;
    float D;
    float V;
    float B;
    friend class Azdaja;
public:
    PutGora(float P = 1, float D = 1, float V = 1, float B = 0.01, string naziv = "Nema naziva za goru");
    ~PutGora();
    float odrediKTezine();
    float odrediKLepote();
    float odrediParametre();
};
