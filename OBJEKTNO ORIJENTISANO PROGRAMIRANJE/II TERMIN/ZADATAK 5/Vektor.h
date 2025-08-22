#include "Tacka.h"
#include <iostream>
using namespace std;

class Vektor {
private:
	int n;
	Tacka* niz;

public:

	Vektor();
	Vektor(int velicina);
	Vektor(const Vektor& v);
	~Vektor();

	void Postavlja_el(const int k, const Tacka el) {
		this->niz[k]=el;
	}


	 Tacka Vraca_konst (int k) const{
		return this ->niz[k];
	}

	 int Getvel() const { return this->n; }
	 void printniz();


	 friend Vektor* saberiel( const Vektor& v1, const Vektor& v2);

	 void izbacidupliuredi();
	 void rotiradesno();
	 void rotiralevo();

	 Vektor& operator=(const Vektor& v);

	 friend istream& operator>>(istream& ulaz, Vektor& v);
	 friend ostream& operator<<(ostream& izlaz, const Vektor& v);

	

	

	

	
	




};