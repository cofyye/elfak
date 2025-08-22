#include <iostream>
#include "Poligon.h"

using namespace std;

int main()
{
    Poligon p1(3);
    
    p1.ucitaj();
    
    double obim = p1.izracunajObim();
    
    cout << "Obim : " << obim << endl;
    
    int x1, x2, y1, y2;
    
    p1.najudaljenijeTacke(x1, y1, x2, y2);
    
    cout << x1 << " : " << y1 << " - " << x2 << " : " << y2 << endl;
    
    Poligon *p2 = new Poligon(3);
    p2->ucitaj();
    p2->prikazi();
    p2->izbaci();
    p2->prikazi();
    
    delete p2;
    
    return 0;
}