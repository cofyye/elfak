#include <iostream>
#include <fstream>
#include <sstream>
#include "Array.h"
#include "E3Point.h"

using namespace std;

int main()
{
    //Definisanje streamova za fajlove
    ifstream f_int_1("int_1.txt");
    ifstream f_int_2("int_2.txt");

    //Definisanje Array klase sa tipom INT
    Array<int>* arr1 = new Array<int>(5);
    Array<int>* arr2 = new Array<int>(8);

    //Definisanje Array klase sa tipom klase E3Point
    Array<E3Point>* arr3 = new Array<E3Point>(3);
    Array<E3Point>* arr4 = new Array<E3Point>(5);

    //Definisanje string line koji nam sluzi za cuvanje vrednosti iz fajla jedne linije
    string line;

    //Provera da li je fajl otvoren
    if(!f_int_1.is_open()) {
        cout << "[INFO] File int_1.txt is not opened !" << endl;
        exit(1);
    }

    //Provera da li je fajl otvoren
    if(!f_int_2.is_open()) {
        cout << "[INFO] File int_2.txt is not opened !" << endl;
        exit(1);
    }

    //Citanje iz fajla int_1.txt
    while(getline(f_int_1, line)) {
        stringstream ss; //Definisanje stringstreama koji sluzi da iz tipa string pretvori u drugi tip [int, float...]
        int num; //Definisanje promenljive num koji je tipa int;
        ss << line; //U ss(stringstream) upisujemo string koju vrednost ima npr 55
        ss >> num; //SS konvertuje string 55 u int 55
        arr1->addElem(num); //U arr1 ubacujemo element tipa int
    }

    //Citanje iz fajla int_2.txt
    while(getline(f_int_2, line)) {
        stringstream ss; //Definisanje stringstreama koji sluzi da iz tipa string pretvori u drugi tip [int, float...]
        int num; //Definisanje promenljive num koji je tipa int;
        ss << line; //U ss(stringstream) upisujemo string koju vrednost ima npr 55
        ss >> num; //SS konvertuje string 55 u int 55
        arr2->addElem(num); //U arr2 ubacujemo element tipa int
    }

    //Definisanje potrebnih E3Point klasa
    E3Point* e3p1 = new E3Point();
    E3Point* e3p2 = new E3Point();
    E3Point* e3p3 = new E3Point();
    E3Point* e3p4 = new E3Point();
    E3Point* e3p5 = new E3Point();
    E3Point* e3p6 = new E3Point();
    E3Point* e3p7 = new E3Point();
    E3Point* e3p8 = new E3Point();

    //Ubacivanje vrednosti za sve gore definisane klase
    cin >> *e3p1;
    arr3->addElem(*e3p1);
    cin >> *e3p2;
    arr3->addElem(*e3p2);
    cin >> *e3p3;
    arr3->addElem(*e3p3);
    cin >> *e3p4;
    arr4->addElem(*e3p4);
    cin >> *e3p5;
    arr4->addElem(*e3p5);
    cin >> *e3p6;
    arr4->addElem(*e3p6);
    cin >> *e3p7;
    arr4->addElem(*e3p7);
    cin >> *e3p8;
    arr4->addElem(*e3p8);

    Array<int>* arr5 = new Array<int>(); //Definisanje arr5 i pozivanje default konstruktora
    *arr5 = *arr2; //Koriscenje overloading= operatora za Array tipa int
    Array<int>* arr6 = *arr1 + *arr2; //Koriscenje overloading+ operatora za Array tipa int

    cout << "Size arr5 : " << arr5->getSize() << endl; //Ispisivanje velicine niza za arr5
    arr5->print(); //Ispisivanje vrednosti niza za arr5

    cout << "Size arr6 : " << arr6->getSize() << endl; //Ispisivanje velicine niza za arr6
    arr6->print(); //Ispisivanje vrednosti niza za arr6

    Array<E3Point>* arr7 = new Array<E3Point>(); //Definisanje arr7 i pozivanje default konstruktora
    *arr7 = *arr3; //Koriscenje overloading= operatora za Array tipa E3Point
    Array<E3Point>* arr8 = *arr3 + *arr4; //Koriscenje overloading+ operatora za Array tipa E3Point

    cout << "Size arr7 : " << arr7->getSize() << endl; //Ispisivanje velicine niza za arr7
    arr7->print(); //Ispisivanje vrednosti niza za arr7

    cout << "Size arr8 : " << arr8->getSize() << endl; //Ispisivanje velicine niza za arr8
    arr8->print(); //Ispisivanje vrednosti niza za arr8

    //Oslobadjanje memorije
    delete arr1;
    delete arr2;
    delete arr3;
    delete arr4;
    delete arr5;
    delete arr6;
    delete arr7;
    delete arr8;

    delete e3p1;
    delete e3p2;
    delete e3p3;
    delete e3p4;
    delete e3p5;
    delete e3p6;
    delete e3p7;
    delete e3p8;

    f_int_1.close(); //Zatvaranje fajla int_1.txt
    f_int_2.close(); //Zatvaranje fajla int_2.txt

    return 0;
}
