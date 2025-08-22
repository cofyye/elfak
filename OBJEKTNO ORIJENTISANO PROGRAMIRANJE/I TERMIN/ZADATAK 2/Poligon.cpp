#include <iostream>
#include <cmath>
#include "Poligon.h"

using namespace std;

Poligon::Poligon() {
  this->brojTemena = 0;
  this->x = new int[this->brojTemena];
  this->y = new int[this->brojTemena];
}

Poligon::Poligon(int brojTemena) {
  this->brojTemena = brojTemena;
  this->x = new int[this->brojTemena];
  this->y = new int[this->brojTemena];
}

Poligon::~Poligon() {
  if(this->x != 0 && this->y != 0) {
    delete[] this->x;
    delete[] this->y;
  }
}

double Poligon::izracunajObim() {
  double obim = 0;
  
  for(int i = 0; i < this->brojTemena-1; i++) {
	    obim += sqrt((this->x[i] - this->x[i+1]) * (this->x[i] - this->x[i+1]) + (this->y[i] - this->y[i+1]) * (this->y[i] - this->y[i+1]));
  }
  
  return obim;
}

void Poligon::ucitaj() {
  int temp = 0;
  for(int i = 0; i < this->brojTemena; i++) {
    cout << "Unesite X#" << i+1 << " : ";
    cin >> temp;
    this->x[i] = temp;
  }
  for(int i = 0; i < this->brojTemena; i++) {
    cout << "Unesite Y#" << i+1 << " : ";
    cin >> temp;
    this->y[i] = temp;
  }
}

void Poligon::najudaljenijeTacke(int &x1, int &y1, int &x2, int &y2) {
	double najduzeRastojanje = 0;
	for (int i = 0; i < this->brojTemena-1; i++) {
		for (int j = i + 1; j < this->brojTemena; j++)
		{
			double novoRastojanje = sqrt((this->x[i] - this->x[j]) * (this->x[i] - this->x[j]) + (this->y[i] - this->y[j]) * (this->y[i] - this->y[j]));
			if (novoRastojanje > najduzeRastojanje) {
				najduzeRastojanje = novoRastojanje;
				x1 = this->x[i];
				y1 = this->y[i];
				x2 = this->x[j];
				y2 = this->y[j];
			}
		}
	}
}

void Poligon::prikazi() {
  for(int i = 0; i < this->brojTemena; i++) {
    cout << "Teme #" << i+1 << " : " << this->x[i] << " " << this->y[i] << endl;
  }
}

void Poligon::izbaci() {
  
  if(this->brojTemena-1 <= 0) return;
  
  this->brojTemena -= 1;
  int *novoX = new int[this->brojTemena];
  int *novoY = new int[this->brojTemena];
  
  for(int i = 0; i < this->brojTemena; i++) {
    novoX[i] = this->x[i];
    novoY[i] = this->y[i];
  }
  
  delete[] this->x;
  delete[] this->y;
  
  this->x = novoX;
  this->y = novoY;
}
