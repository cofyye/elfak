#include "E3Point.h"
#include <string>

using namespace std;

E3Point::E3Point() {
    this->X = 0;
    this->Y = 0;
    this->Z = 0;
    this->name = "No Name";
}

E3Point::E3Point(int X, int Y, int Z, string name) {
    this->X = X;
    this->Y = Y;
    this->Z = Z;
    this->name = name;
}

E3Point::~E3Point() {}

E3Point& E3Point::operator=(const E3Point& e3p) {
    if(this != &e3p) {
        this->X = e3p.X;
        this->Y = e3p.Y;
        this->Z = e3p.Z;
        this->name = e3p.name;
        return *this;
    } else {
        cout << "[INFO] Error !" << endl;
        return *this;
    }
}

E3Point E3Point::operator+(const E3Point& e3p) {
    E3Point temp;
    temp.X = this->X + e3p.X;
    temp.Y = this->Y + e3p.Y;
    temp.Z = this->Z + e3p.Z;
    temp.name = this->name + " " + e3p.name;

    return temp;
}

ostream& operator<<(ostream& cout, E3Point& e3p) {
    cout << endl;
    cout << "==== " << e3p.name << " ====" << endl;
    cout << "==> X : " << e3p.X << " <==" << endl;
    cout << "==> Y : " << e3p.Y << " <==" << endl;
    cout << "==> Z : " << e3p.Z << " <==" << endl;
    cout << "=================" << endl;

    return cout;
}

istream& operator>>(istream& cin, E3Point& e3p) {
    string name;
    cout << "Unesite X : ";
    cin >> e3p.X;
    cout << "Unesite Y : ";
    cin >> e3p.Y;
    cout << "Unesite Z : ";
    cin >> e3p.Z;
    cout << "Unesite naziv : ";
    cin.ignore();
    getline(cin, name);
    e3p.name = name;
    return cin;
}
