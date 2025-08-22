#pragma once
#include <string>
#include "Put.h"
#include "PutMora.h"

class PutMocvara : public Put {
protected:
    float P;
    float Z;
    float S;
    friend class Azdaja;
public:
    PutMocvara() {};
    PutMocvara(PutMora* putmora, float P = 1, float Z = 1, string naziv = "Nema naziva za mocvaru");
    PutMocvara(PutMora putmora, float P = 1, float Z = 1, string naziv = "Nema naziva za mocvaru");
    ~PutMocvara();
    float odrediKTezine();
    float odrediKLepote();
    float odrediParametre();
};
