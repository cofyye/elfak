#include <iostream>
#include "Student.h"

using namespace std;

int main()
{
    Student Pera(10);
    Pera.Ucitaj();
    Pera.PrikaziPodatke();
    
    char* pNovoIme = new char[50];
    cout << "Unesite novo ime : ";
    fgets(pNovoIme, 50, stdin);
    
    char* pNovoPrezime = new char[50];
    cout << "Unesite novo prezime : ";
    fgets(pNovoPrezime, 50, stdin);
    
    Pera.setIme(pNovoIme);
    Pera.setPrezime(pNovoPrezime);
    
    cout << endl;
    Pera.DodajOcenu(5);
    Pera.DodajOcenu(7);
    Pera.DodajOcenu(6);
    Pera.PrikaziOcene();
    Pera.PrikaziPodatke();
    
    Student* Dika = new Student(10);    
    
    Dika->Ucitaj();
    Dika->DodajOcenu(8);
    Dika->DodajOcenu(9);
    Dika->DodajOcenu(10);
    Dika->PrikaziOcene();
    Dika->PrikaziPodatke();

    delete Dika;

    return 0;
}
