#pragma once
#include <string>
#include "Put.h"

using namespace std;

class PutPlanina : public Put {
protected:
    float P;
    float H;
    friend class Azdaja;
public:
    PutPlanina(float P = 1, float H = 1, string naziv = "Nema naziva za planinu");
    ~PutPlanina();
    float odrediKTezine();
    float odrediKLepote();
    float odrediParametre();
};

