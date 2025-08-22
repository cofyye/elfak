#pragma once
#include <iostream>
#include "Complex.h"

class Skup 
{
private:
	int brojElemenata;
	Complex* niz;
public:
	Skup()									//DEFAULT KONSTRUKTOR,STAVLJA NA NULU VREDNOST BROJA ELEMENATA
	{
		brojElemenata = 0;
		niz = NULL;
	}

	Skup(int e)								//SLUZI KAO NEKA VRSTA SETERA ZA BROJ ELEMENATA NIZA
	{
		brojElemenata = e;
		niz = new Complex[brojElemenata];		//U INDEKSU MOZE DA STOJI e,MOZE DA STOJI I brojElemenata
	}

  Skup(const Skup& skup) {
    this->brojElemenata = skup.brojElemenata;
    this->niz = new Complex[this->brojElemenata];
    for(int i = 0; i < this->brojElemenata; i++) {
      this->niz[i] = skup.niz[i];
    }
  }

	~Skup()									//U DEKONSTRUKTORU SE BRISE SVE STO JE DINAMICKO
	{
		delete[]niz;
		niz = NULL;
	}

	inline int getBrojElemenata()			//GETER FUNKCIJA
	{
		return brojElemenata;
	}


	void izbaciDuplikat();
	bool ispitajElement(const Complex& a);
	void ulaz();
	void urediNerastuce();
	void izlaz();
	
	Skup razlika(const Skup& a);
	Skup unija(const Skup& a);
	Skup presek(const Skup& a);
};


