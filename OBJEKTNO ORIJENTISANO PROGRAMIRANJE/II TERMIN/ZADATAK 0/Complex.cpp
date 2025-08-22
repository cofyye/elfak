#include <iostream>
#include <bits/stdc++.h>
#include "Complex.h"

using namespace std;

Complex::Complex() {
  this->realni = 0;
  this->imaginarni = 0;
}

Complex::Complex(double realni, double imaginarni) {
  this->realni = realni;
  this->imaginarni = imaginarni;
}

void Complex::unos() {
  double realni, imaginarni;
  
  cout << "Unesite realni broj : ";
  cin >> realni;

  cout << "Unesite imaginarni broj : ";
  cin >> imaginarni;
  
  this->realni = realni;
  this->imaginarni = imaginarni;
}

void Complex::prikaz() {
  cout << "Realni broj je : " << this->realni << endl;
  cout << "Imaginarni broj je : " << this->imaginarni << endl;
}

Complex Complex::saberi(const Complex& cpx) {
  Complex temp;
  temp.realni = this->realni + cpx.realni;
  temp.imaginarni = this->imaginarni + cpx.imaginarni;
  
  return temp;
}

Complex Complex::oduzmi(const Complex& cpx) {
  Complex temp;
  temp.realni = this->realni - cpx.realni;
  temp.imaginarni = this->imaginarni - cpx.imaginarni;
  
  return temp;
}

bool Complex::operator==(const Complex& b) const {
  
  if(this->realni == b.realni && this->imaginarni == b.imaginarni) {
    return true;
  } else {
    return false;
  }
  
}

Complex& Complex::operator=(const Complex& a) {
  if(this != &a) {
    this->realni = a.realni;
    this->imaginarni = a.imaginarni;
  }
  
  return *this;
}

bool Complex::operator<(const Complex& b) const {
  double moduo1 = sqrt(pow(this->realni, 2) + pow(this->imaginarni, 2));
  double moduo2 = sqrt(pow(b.realni, 2) + pow(b.imaginarni, 2));
  
  return moduo1 < moduo2;
}

ostream& operator<<(ostream& cout, const Complex& a) {
  cout << "Realni broj je : " << a.realni << endl;
  cout << "Imaginarni broj je : " << a.imaginarni << endl;
  return cout;
}
