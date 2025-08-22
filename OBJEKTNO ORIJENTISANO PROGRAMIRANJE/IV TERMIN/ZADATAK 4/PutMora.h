#pragma once
#include <string>
#include "Put.h"

class PutMora : public Put {
protected:
    float P;
    float S;
    int MSV;
    friend class PutMocvara;
    friend class Azdaja;
public:
    PutMora(float P = 1, float S = 1, int MSV = 1, string naziv = "Nema naziva za more");
    ~PutMora();
    float odrediKTezine();
    float odrediKLepote();
    float odrediParametre();
};
