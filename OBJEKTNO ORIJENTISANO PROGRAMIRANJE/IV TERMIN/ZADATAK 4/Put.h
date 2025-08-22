#pragma once

#include <string>

using namespace std;

class Put {
protected:
    float kTezine;
    float kLepote;
    string naziv;
    friend class Azdaja;
public:
    Put(string naziv = "Nema naziva");
    virtual ~Put();
    virtual float odrediKTezine() = 0;
    virtual float odrediKLepote() = 0;
    virtual float odrediParametre() = 0;
};
