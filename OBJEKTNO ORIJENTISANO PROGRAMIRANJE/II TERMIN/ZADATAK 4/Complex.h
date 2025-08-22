#pragma once
#include <fstream>
#include <iostream>
using namespace std;

class Complex {
    double real;
    double imag;
    friend class Buffer;
  public:
    Complex(double real = 0.0, double imag = 0.0);
    Complex(const Complex& cpx);
    void input();
    void print();
    inline double getReal() { return this->real; };
    inline double getImag() { return this->imag; };
    Complex add(const Complex& cpx);
    Complex sub(const Complex& cpx);
    double mod();
    bool comparisonByModuo(Complex& cpx1, Complex& cpx2, int type);

    Complex& operator=(const Complex& cpx);
    friend ostream& operator<<( ostream& cout, Complex& cpx);
    friend ofstream& operator<<( ofstream& fout, Complex& cpx);
};
