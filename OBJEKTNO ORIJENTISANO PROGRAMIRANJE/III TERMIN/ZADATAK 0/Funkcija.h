#pragma once

class Funkcija {
protected:
    int k;
    int n;
    char* naziv;

public:
    Funkcija();
    Funkcija(int k, int n);
    virtual ~Funkcija();
    virtual double odrediX(int x);
    virtual void prikaziFunkciju();
};
