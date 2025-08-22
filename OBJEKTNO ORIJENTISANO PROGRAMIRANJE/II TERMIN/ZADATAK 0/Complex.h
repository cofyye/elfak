#pragma once

#include <iostream>

using namespace std;

class Complex {
  private:
    double realni;
    double imaginarni;
    friend class Skup;
  public:
    Complex();
    Complex(double realni, double imaginarni);
    void unos();
    void prikaz();
    Complex saberi(const Complex& cpx);
    Complex oduzmi(const Complex& cpx);
    
    bool operator==(const Complex& b) const;
    bool operator<(const Complex& b) const;
    friend ostream& operator<<(ostream& cout, const Complex& a);
    Complex& operator=(const Complex& b);
};