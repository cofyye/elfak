#include <iostream>
#include "Funkcija.h"
#include "KvadratnaFunkcija.h"
#include "EksponencijalnaFunkcija.h"

using namespace std;

int main()
{
    int trBroj = 0;
    Funkcija* niz[2019];

    for(int i = 0; i < 673; i++, trBroj++) {
        niz[trBroj] = new Funkcija(7,9);
    }

    for(int i = 0; i < 673; i++, trBroj++) {
        niz[trBroj] = new KvadratnaFunkcija(6, 9, "Kvadratna");
    }

    for(int i = 0; i < 673; i++, trBroj++) {
        niz[trBroj] = new EksponencijalnaFunkcija(6, 9, "Eksponencijalna");
    }

    for(int i = 0; i < trBroj; i++) {
        niz[i]->prikaziFunkciju();
    }

    trBroj = 0;

    Funkcija* func1 = new Funkcija(2, 3);
    Funkcija* func2 = new KvadratnaFunkcija(4, 5, "Aca Pi*ka");
    Funkcija* func3 = new EksponencijalnaFunkcija(6, 7, "Mataaaa");

    int x;
    cout << "Unesite X : ";
    cin >> x;

    double fun1 = func1->odrediX(x);
    double fun2 = func2->odrediX(x);
    double fun3 = func3->odrediX(x);

    cout << fun1 << endl;
    cout << fun2 << endl;
    cout << fun3 << endl;

    if(fun1 < fun2) {
        if(fun1 < fun3) {
            cout << fun1 << endl;
        } else if(fun2 < fun3) {
            cout << fun2 << endl;
        } else {
            cout << fun3 << endl;
        }
    } else {
        if(fun2 < fun3) {
            cout << fun2 << endl;
        } else if(fun1 < fun3) {
            cout << fun1 << endl;
        } else {
            cout << fun3 << endl;
        }
    }

    delete func1;
    delete func2;
    delete func3;

    return 0;
}
