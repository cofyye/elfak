#include "skup.h"
#include <iostream>

using namespace std;

int main()
{
	Skup *skup1 = new Skup(3);

	skup1->ulaz();
	skup1->izlaz();	
	
// 	Complex a;
// 	a.unos();


// 	if (skup1->ispitajElement(a))
// 	{
// 		cout << "Ovaj element postoji u skupu\n";
// 	}
// 	else
// 		cout << "Element ne postoji u skupu\n";

// 	Complex a;
// 	a.unos();
// 	if (skup1->ispitajElement(a))
// 	{
// 		cout << "Ovaj element postoji u skupu\n";
// 	}
// 	else
// 		cout << "Element ne postoji u skupu\n";
// 	//

// 	Complex a;
// 	a.unos();
// 	if (skup1->ispitajElement(a))
// 	{
// 		cout << "Ovaj element postoji u skupu\n";
// 	}
// 	else
// 		cout << "Element ne postoji u skupu\n";

	Skup *skup2 = new Skup(2);
	skup2->ulaz();

// 	skup2->urediNerastuce();
	skup2->izlaz();
	
	cout << "NEXT" << endl;
	
	Skup *unija = new Skup(skup1->unija(*skup2));
	unija->izlaz();
	Skup *razlika = new Skup(skup1->razlika(*skup2));
	razlika->izlaz();
	Skup *presek = new Skup(skup1->presek(*skup2));
	presek->izlaz();
 
// 	cout << "Pre izbacivanja duplikata Skup2 ima " << skup2->getBrojElemenata() << " elemenata\n";


// 	skup2->izbaciDuplikat();
// 	skup2->izlaz();

// 	cout << "Posle izbacivanja duplikata Skup2 ima " << skup2->getBrojElemenata() << " elemenata\n";


  // Skup c = skup1.razlika(skup2);
  // c.izlaz();

	delete skup1;				//POZVAN DESTRUKTOR
	delete skup2;				//POZVAN DESTRUKTOR
	delete razlika;				//POZVAN DESTRUKTOR
	delete unija;				//POZVAN DESTRUKTOR
	delete presek;				//POZVAN DESTRUKTOR

}


