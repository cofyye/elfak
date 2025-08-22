#pragma once
#include <string>
#include <iostream>

using namespace std;

class E3Point {
    private:
        int X;
        int Y;
        int Z;
        string name;
    public:
        E3Point();
        E3Point(int X, int Y, int Z, string name);
        ~E3Point();

        E3Point& operator=(const E3Point& e3p);
        E3Point operator+(const E3Point& e3p);
        friend ostream& operator<<(ostream& cout, E3Point& e3p);
        friend istream& operator>>(istream& cin, E3Point& e3p);
};
