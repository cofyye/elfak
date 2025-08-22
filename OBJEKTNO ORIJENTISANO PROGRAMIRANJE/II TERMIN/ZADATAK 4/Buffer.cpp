#include <iostream>
#include <fstream>
#include "Buffer.h"
#include "Complex.h"

using namespace std;

Buffer::Buffer() {
    this->size = 0;
    this->numOfEls = 0;
    this->arr = new Complex[this->size];
}

Buffer::Buffer(int size) {
    this->size = size;
    this->numOfEls = 0;
    this->arr = new Complex [this->size];
}

Buffer::Buffer(const Buffer& buf) {
    this->size = buf.size;
    this->numOfEls = buf.numOfEls;
    this->arr = new Complex [this->numOfEls];
    for(int i = 0; i < buf.numOfEls; i++) {
      this->arr[i] = buf.arr[i];
    }
}

Buffer::~Buffer() {
    if (this->arr != 0) {
      delete[] this->arr;
    }
    this->arr = 0;
}

void Buffer::push(const Complex& cpx) {
    if(this->numOfEls < this->size) {
      this->arr[this->numOfEls] = cpx;
      this->numOfEls++;
    } else {
      cout << "Greska" << endl;
    }
}

Complex Buffer::pop() {
    if(this->numOfEls > 0) {
      Complex data = this->arr[this->numOfEls-1];
      this->arr[this->numOfEls-1] = 0;
      this->numOfEls--;
      return data;
    } else {
      cout << "Greska" << endl;
      return Complex(0, 0);
    }
}

void Buffer::print() {
    for(int i = 0; i < this->numOfEls; i++) {
        cout << this->arr[i];
    }
}

void Buffer::duplicate() {
    this->size *= 2;
    Complex *arr2 = new Complex[this->size];

    for(int i = 0; i < this->numOfEls; i++) {
        arr2[i] = this->arr[i];
    }

    delete[] this->arr;
    this->arr = arr2;
}

void Buffer::removee() {
    Complex *arr2 = new Complex[this->numOfEls];

    for(int i = 0; i < this->numOfEls; i++) {
        arr2[i] = this->arr[i];
    }

    delete[] this->arr;
    this->arr = arr2;
}

void Buffer::append(const Buffer& buf) {
    int i, j;
    this->numOfEls += buf.numOfEls;
    Complex *arr2 = new Complex[this->numOfEls];

    for(i = 0; i < this->numOfEls - buf.numOfEls; i++) {
        arr2[i] = this->arr[i];
    }

    for(i = this->numOfEls - buf.numOfEls, j = 0; i < this->numOfEls; i++, j++) {
        arr2[i] = buf.arr[j];
    }

    delete[] this->arr;
    this->arr = arr2;
}

void Buffer::popEl(int index) {

    if(index >= this->numOfEls && index < 0) return;

    Complex *arr2 = new Complex[this->numOfEls-1];

    for(int i = 0, j = 0; i < this->numOfEls; i++, j++) {
        if(index != i) {
          arr2[j] = this->arr[i];
        } else {
          j--;
        }
    }

    this->numOfEls -= 1;

    delete[] this->arr;
    this->arr = arr2;
}

void Buffer::popUnder(int index) {

    if(index >= this->numOfEls && index < 0) return;

    Complex *arr2 = new Complex[this->numOfEls-1];

    for(int i = index, j = 0; i < this->numOfEls; i++, j++) {
        arr2[j] = this->arr[i];
    }

    this->numOfEls -= index;

    delete[] this->arr;
    this->arr = arr2;
}

void Buffer::average() {

  int avgReal = 0;
  int avgImag = 0;

  for(int i = 0; i < this->numOfEls; i++) {
    avgReal += this->arr[i].real;
    avgImag += this->arr[i].imag;
  }

  avgReal /= this->numOfEls;
  avgImag /= this->numOfEls;

  cout << "Prosecna vrednost realnog dela je : " << avgReal << endl;
  cout << "Prosecna vrednost imaginarnog dela je : " << avgImag << endl;

}

ostream& operator<<(ostream& cout, Buffer& buf) {
    buf.print();
    return cout;
}

ofstream& operator<<(ofstream& fout, Buffer& buf) {
    for(int i = 0; i < buf.numOfEls; i++) {
        fout << buf.arr[i];
    }
    return fout;
}

Buffer& Buffer::operator=(const Buffer& buf) {
    if(this != &buf) {
      this->size = buf.size;
      this->numOfEls = buf.numOfEls;
      this->arr = new Complex [this->numOfEls];
      for(int i = 0; i < buf.numOfEls; i++) {
        this->arr[i] = buf.arr[i];
      }
    }
    return *this;
}

