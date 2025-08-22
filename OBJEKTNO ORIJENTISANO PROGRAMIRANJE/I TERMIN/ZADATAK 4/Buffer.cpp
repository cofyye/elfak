#include <iostream>
#include "Buffer.h"

using namespace std;

Buffer::Buffer() {
    this->size = 0;
}

Buffer::Buffer(int size) {
    this->size = size;
    this->arr = new int [this->size];
}

Buffer::Buffer(const Buffer& buf) {
  this->numOfEls = buf.numOfEls;
  this->arr = new int [this->numOfEls];
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

void Buffer::push(int data) {
  if(this->numOfEls < this->size) {
    this->arr[this->numOfEls] = data;
    this->numOfEls++;
  } else {
    cout << "Greska" << endl;
  }
}

int Buffer::pop() {
  if(this->numOfEls > 0) {
    int data = this->arr[this->numOfEls-1];
    this->arr[this->numOfEls-1] = 0;
    this->numOfEls--;
    return data;
  } else {
    cout << "Greska" << endl;
    return -1;
  }
}

void Buffer::print() {
    for(int i = 0; i < this->numOfEls; i++) {
        cout << this->arr[i] << " ";
    }
}

void Buffer::duplicate() {
    this->size *= 2;
    int *arr2 = new int[this->size];

    for(int i = 0; i < this->numOfEls; i++) {
        arr2[i] = this->arr[i];
    }

    delete[] this->arr;
    this->arr = arr2;
}

void Buffer::removee() {
    int *arr2 = new int[this->numOfEls];

    for(int i = 0; i < this->numOfEls; i++) {
        arr2[i] = this->arr[i];
    }

    delete[] this->arr;
    this->arr = arr2;
}
