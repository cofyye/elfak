#include <iostream>
#include <fstream>
#include <math.h>
#include "Complex.h"

using namespace std;

Complex::Complex(double real, double imag) {
  this->real = real;
  this->imag = imag;
}

Complex::Complex(const Complex& cpx) {
  this->real = cpx.real;
  this->imag = cpx.imag;
}

void Complex::input() {
  double real, imag;

  cout << "Unesite realni deo : ";
  cin >> real;

  cout << "Unesite imaginarni deo : ";
  cin >> imag;

  this->real = real;
  this->imag = imag;
}

void Complex::print() {
  cout << "Realni deo je : " << this->real << endl;
  cout << "Imaginarni deo je : " << this->imag << endl;
}

Complex Complex::add(const Complex& cpx) {
  Complex temp;
  temp.real = this->real + cpx.real;
  temp.imag = this->imag + cpx.imag;
  return temp;
}

Complex Complex::sub(const Complex& cpx) {
  Complex temp;
  temp.real = this->real - cpx.real;
  temp.imag = this->imag - cpx.imag;
  return temp;
}

double Complex::mod() {
  double moduo = sqrt(pow(this->real, 2) + pow(this->imag, 2));
  return moduo;
}

bool Complex::comparisonByModuo(Complex& cpx1, Complex& cpx2, int type) {
    double moduo1 = cpx1.mod();
    double moduo2 = cpx2.mod();

    if(type == 0) {
        return moduo1 < moduo2;
    } else if(type == 1) {
        return moduo1 > moduo2;
    } else return false;

}

Complex& Complex::operator=(const Complex& cpx) {
    if(this != &cpx) {
        this->real = cpx.real;
        this->imag = cpx.imag;
    }

    return *this;
}

ostream& operator<<(ostream& cout, Complex& cpx) {
    cpx.print();
    return cout;
}

ofstream& operator<<(ofstream& fout, Complex& cpx) {
    fout << "Realni deo je : " << cpx.real << endl;
    fout << "Imaginarni deo je : " << cpx.imag << endl;
    return fout;
}
