#include <iostream>
#include <string>
#include "Stack.h"

using namespace std;

Stack::Stack(int size) {
  this->size = size;
  this->array = new int[this->size];
  this->top = -1;
}

Stack::~Stack() {
  if(this->array != 0) {
    delete[] this->array;
  }
}

void Stack::push(int el) {
  if(this->top+1 > this->size-1) {
    string error = "Cannot push element - Stack overflow!";
    throw error;
  }
  this->array[this->top+1] = el;
  this->top += 1;
}

int Stack::pop() {
  if(this->isEmpty()) {
    string error = "Cannot pop element - Stack underflow!";
    throw error;
  }
  
  int tmp = this->array[this->top];
  this->top -= 1;
  return tmp;
}

bool Stack::isEmpty() {
  return this->top == -1;
}

int Stack::numberOfElements() {
  return this->top + 1;
}

int Stack::getTop() {
  if(this->isEmpty()) {
    string error = "Cannot get top element - Stack underflow!";
    throw error;
  }
  
  return this->array[this->top];
}

void Stack::print() {
  
  if(this->isEmpty()) return;
  
  for(int i = 0; i < this->top+1; i++) {
    cout << this->array[i] << " ";
  }
  cout << endl;
}

