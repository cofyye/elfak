#include <iostream>
#include "Student.h"

using namespace std;

Student::Student(int maxBrIspita) {
    this->maxBrIspita = maxBrIspita;
	this->nizOcena = new int[this->maxBrIspita];
	this->ime = nullptr;
	this->prezime = nullptr;
}

Student::~Student()
{
	if (this->ime != nullptr)
	{
		delete[] this->ime;
	}
	if (this->prezime != nullptr)
	{
		delete[] this->prezime;
	}
	if (this->nizOcena != nullptr)
	{
		delete[] this->nizOcena;
	}
}

void Student::DodajOcenu(int ocena)
{
	if (this->maxBrIspita > this->brPolIspita && ocena >= 5 && ocena <= 10)
	{
		this->nizOcena[this->brPolIspita] = ocena;
		this->brPolIspita++;
	}
}

void Student::Ucitaj() {
    
    if(this->ime != nullptr) delete[] this->ime;
    if(this->prezime != nullptr) delete[] this->prezime;
    
    char* ime = new char[50];
    char* prezime = new char[50];
    int brIndeksa = 0;
    
    cout << "Unesite ime : ";
    fgets(ime, 50, stdin);
    
    cout << "Unesite prezime : ";
    fgets(prezime, 50, stdin);
    
    cout << "Unesite broj indeksa : ";
    cin >> brIndeksa;
    
    char ch;
    ch = cin.get();
    
    this->ime = ime;
    this->prezime = prezime;
    this->brojIndeksa = brIndeksa;
}

void Student::setIme(char* ime) {
    if(this->ime != nullptr) delete[] this->ime;
    
    this->ime = ime;
}

void Student::setPrezime(char* prezime) {
    if(this->prezime != nullptr) delete[] this->prezime;
    
    this->prezime = prezime;
}

void Student::PrikaziOcene() {
    int pom = 0;
    int pomNiz[this->brPolIspita];
    
    for(int i = 0; i < this->brPolIspita; i++) {
        pomNiz[i] = this->nizOcena[i];
    }
    
    for (int i = 0; i < this->brPolIspita; i++) {
        for (int j = i + 1; j < this->brPolIspita; j++) {
            if (pomNiz[i] < pomNiz[j]) {
                pom = pomNiz[i];
                pomNiz[i] = pomNiz[j];
                pomNiz[j] = pom;
            }
        }
    }
    cout << "Ocene : ";
    for(int i = 0; i < this->brPolIspita; i++) {
        cout << pomNiz[i] << " ";
    }
}

void Student::PrikaziPodatke() {
    cout << "Ime : " << this->ime;
    cout << "Prezime : " << this->prezime;
    cout << "Broj Indeksa : " << this->brojIndeksa<<endl;
}

