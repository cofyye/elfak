#include"vektor.h"
#include<iostream>
#include <fstream>
using namespace std;
int main() {
	
	Vektor* v1 = new Vektor(5);
	Vektor* v2 = new Vektor(5);

	ifstream ul1("tacke1.txt"), ul2("tacke2.txt"), ul4("tacke3.txt"), ul5("tacke4.txt");

	if (ul1.good()) {
		ul1 >> *v1;
		ul1.close();
	}

	
	
	if (ul2.good()) {
		ul2 >> *v2;
		ul2.close();
	}
	ofstream ul("tacke11.txt");
	if (ul.good()) {
		ul << *v2;
		ul.close();
	}

	

	Vektor* v3 = saberiel(*v1, *v2);

	Vektor* v4 = new Vektor(5);
	Vektor* v5 = new Vektor(5);

	if (ul4.good()) {
		ul4 >> *v4;
		ul4.close();
	}
	if (ul5.good()) {
		ul5 >> *v5;
		ul5.close();
	}

	Vektor* v6 = new Vektor(5);
	for (int i = 0; i < v4->Getvel(); i++) {
		Tacka t1 = v4->Vraca_konst(i);
		Tacka t2 = v5->Vraca_konst(i);
		Tacka t3(t1.getX() * t2.getX(), t1.getY() * t2.getY(), t1.getZ() * t2.getZ());
		v6->Postavlja_el(i, t3);
	}

	Vektor* v7 = new Vektor(5);
	const double n = 3.3;
	for (int i = 0; i < 5; i++) {
		Tacka t1 = v5->Vraca_konst(i);
		Tacka t(t1.getX() + n, t1.getY() + n, t1.getZ() + n);
		v7->Postavlja_el(i, t);
	}

	Vektor* v8 = new Vektor(5);
	const double m = 0.33;
	for (int i = 0; i < 5; i++) {
		Tacka t1 = v5->Vraca_konst(i);
		v8->Postavlja_el(i, t1.mnoziskalarom(m));
	}

	v1->izbacidupliuredi();
	v2->rotiralevo();

	ofstream iz("tacke5.txt");
	if (iz.good()) {
		iz << *v1;
		iz.close();
	}
	

	delete v1;
	delete v2;
	delete v3;
	delete v4;
	delete v5;
	delete v6;
	delete v7;
	delete v8;
	
	return 0;




	/*Vektor vektor(n);

	cout << "Unesite elemente niza" << endl;
	for (int i = 0; i < n; i++) {
		int j;
		cin >> j;
		vektor.Postavlja_el(i, j);
	}
	cout << "Elementi niza su:" << endl;
	for (int i = 0; i < n; i++) {
		cout << vektor.Vraca_konst(i) << " ";
	}
	cout << endl;

	vektor.sortiraj_neopadajuci();
	cout << "Sortirani niz:" << endl;
	for (int i = 0; i < n; i++) {
		cout << vektor.Vraca_konst(i) << " ";
	}
	cout << endl;

 Vektor* niz1;
	niz1 = new Vektor(n);
	cout << "Unesite elemente novokreiranog niza" << endl;
	for (int i = 0; i < n; i++) {
		int j;
		cin >> j;
		niz1->Postavlja_el(i, j);
	}
	cout << "Ovo su elementi novokreiranog niza" << endl;
	for (int i = 0; i < n; i++) {
		cout << niz1->Vraca_konst(i) << " ";
	}
	cout << endl;
	
	niz1->sortiraj_neopadajuci();
	cout << "Sortirani niz:" << endl;
	for (int i = 0; i < n; i++) {
		cout << niz1->Vraca_konst(i) << " ";
	}
	cout << endl;

	

	cout << "Unesite broj elemenata niza" << endl;
	cin >> n;
	
	Vektor niz2(n);
	
	cout << "Unesite novi niz:" << endl;
	for (int i = 0; i < n; i++) {
		int j;
		cin >> j;
		niz2.Postavlja_el(i, j);
	}
	cout << endl;
	cout << "Skalarni proizvod dva vektora je:"<<" ";
	cout << niz1->skalarni_proizvod(niz2);
	cout << endl;

	cout << "Indeks elementa cija je vrednost najbliza srednjoj vrednosti:" << " ";
	cout<<niz1->vraca_indeks();
	cout << endl;

	Vektor a(5);
	Vektor b(5);
	cin >> a;
	cin >> b;
	cout << a.skalarni_proizvod(b);



	

	
	
    delete niz1;
	*/

	
}