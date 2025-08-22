#pragma once
#include <iostream>
#include <fstream>
#include "Complex.h"

using namespace std;

class Buffer {
    private:
        Complex *arr;
        int size;
        int numOfEls;
    public:
        Buffer();
        Buffer(int size);
        Buffer(const Buffer& buf);
        ~Buffer();
        inline int getNumOfEls() {
            return this->numOfEls;
        }
        void push(const Complex& cpx);
        Complex pop();
        void print();
        void duplicate();
        void removee();
        void popEl(int index);
        void popUnder(int index);
        void append(const Buffer& buf);
        void average();

        Buffer& operator=(const Buffer& buf);
        friend ostream& operator<<( ostream& cout, Buffer& buf);
        friend ofstream& operator<<( ofstream& fout, Buffer& buf);
};

