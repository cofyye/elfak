#include "Vektor.h"
#include <math.h>
using namespace std; 


Vektor::Vektor() {
	this->n = 0;
	this->niz = new Tacka[0];
	
}
Vektor::Vektor(int velicina) {
	this -> n = velicina;
	this -> niz = new Tacka[this -> n];
}
Vektor :: ~Vektor() {
	if (niz != 0)
		delete[] niz;
	
}

Vektor::Vektor(const Vektor& v) {
	this->n = v.n;
	this->niz = new Tacka[this->n];
	for (int i = 0; i < this->n; i++)
		niz[i].printTacka();
}

Vektor* saberiel(const Vektor& v1, const Vektor& v2) {
	if (v1.n != v2.n)
		return 0;
	int vel = v1.n;
	Vektor* v = new Vektor(vel);
	for (int i = 0; i < vel; i++)
		v->Postavlja_el(i, v1.niz[i].saberi(v2.niz[i]));
	return v;
}

void Vektor :: izbacidupliuredi() 
{
		Tacka t1, t2;
		for (int i = 0; i < n - 1; i++) {
			t1 = niz[i];
			for (int j = i + 1; j < n; j++) {
				t2 = niz[j];
				if (t1.getX() == t2.getX() && t1.getY() == t2.getY() && t1.getZ() == t2.getZ()) {
					this->n = this->n - 1;
					for (int k = j; k < n; k++) {
						niz[k] = niz[k + 1];
					}
				}
			}
		}

		if (this->n < 2)
			return;
		for (int i = 0; i < this->n; i++) {
			double min = niz[i].getR();
			int j = i;
			for (int k = i + 1; k < this->n; k++) {
				if (min > niz[k].getR()) {
					min = niz[k].getR();
					j = k;
				}
			}
			Tacka tt = niz[j];
			niz[j] = niz[i];
			niz[i] = tt;
		}

}


void Vektor::rotiradesno() {
	Tacka tt = niz[n - 1];
	for (int i = n-1; i >= 1; i--)
		niz[i] = niz[i - 1];
	niz[0] = tt;

}

void Vektor::rotiralevo() {
	Tacka tt = niz[0];
	for (int i = 0; i < n - 1; i++)
		niz[i] = niz[i + 1];
	niz[n - 1] = tt;
}

Vektor& Vektor::operator=(const Vektor& v) {
	if (this != &v) {
		this->n = v.n;
		delete[] niz;

		niz = new Tacka[n];
		for (int i = 0; i < n; i++)
			niz[i] = v.niz[i];
	}
	return* this;
}

istream& operator>>(istream& ulaz, Vektor& v) {
	double xx, yy, zz;
	for (int i = 0; i < v.n; i++) {
		ulaz >> xx >> yy >> zz;
		(v.niz[i]).SetTacka(xx, yy, zz);
	}
	return ulaz;
}


ostream& operator<<(ostream& izlaz, const Vektor& v) {
	izlaz << endl;
	for (int i = 0; i < v.n; i++) {
		Tacka t = v.niz[i];
		izlaz << t.getX() << " " << t.getY() << " " << t.getZ() << endl;
	}
	izlaz << endl;
	return izlaz;
}





//void Vektor::sortiraj_neopadajuci() {
//	for (int i = 0; i < this -> n - 1; i++) {
//		for (int j = i + 1; j < this -> n; j++) {
//			if (this -> niz[i] >= this -> niz[j]) {
//				int pom = this -> niz[i];
//				this -> niz[i] = this -> niz[j];
//				this -> niz[j] = pom;
//			}
//		}
//	}
//}
//
//
//int Vektor::skalarni_proizvod(const Vektor& b) {
//	if (this -> n != b.n)
//		return 0;
//	
//	int sk = 0;
//	for (int i = 0; i < b.n; i++) {
//			sk += this->niz[i] * b.niz[i];
//	}
//	return sk;
//	
//	
//}
//
//int Vektor::vraca_indeks() {
//	float s = 0;
//	float sr;
//	int R = niz[0];
//	int ind = 0;
//	for (int i = 0; i < this->n; i++) {
//		s = s + niz[i];
//
//	}
//	sr = s / this->n;
//	for (int i = 0; i < this->n; i++) {
//		if (abs(sr - niz[i]) < R) {
//			R = abs(sr - niz[i]);
//			ind = i;
//		}
//	}
//	return ind;
//}
//	
//
	
//

	
