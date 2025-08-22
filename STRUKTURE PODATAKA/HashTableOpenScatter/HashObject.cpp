#include <iostream>

#include "HashObject.h"

using namespace std;

HashObject::HashObject() {
  this->key = nullptr;
  this->value = nullptr;
}

HashObject::HashObject(char* key, char* value) {
  this->key = new char[strlen(key) + 1];
  this->value = new char[strlen(value) + 1];
  
  strcpy_s(this->key, strlen(key) + 1, key);
  strcpy_s(this->value, strlen(value) + 1, value);
}

HashObject::HashObject(const HashObject& obj) {
  this->key = new char[strlen(obj.key) + 1];
  this->value = new char[strlen(obj.value) + 1];
  
  strcpy_s(this->key, strlen(obj.key) + 1, obj.key);
  strcpy_s(this->value, strlen(obj.value) + 1, obj.value);
}

HashObject::~HashObject() {
  if(this->key != nullptr) delete[] this->key;
  if(this->value != nullptr) delete[] this->value;
}

HashObject& HashObject::operator=(const HashObject& obj) {
  if(this != &obj) {
    if(this->key != nullptr) delete[] this->key;
    if(this->value != nullptr) delete[] this->value;
  
    this->key = new char[strlen(obj.key) + 1];
    this->value = new char[strlen(obj.value) + 1];
  
    strcpy_s(this->key, strlen(obj.key) + 1, obj.key);
    strcpy_s(this->value, strlen(obj.value) + 1, obj.value);
  }
  
  return *this;
}

void HashObject::deleteValue() {
  if(this->value != nullptr) delete[] this->value;
}

void HashObject::print() {
    if (this->key != nullptr && this->value != nullptr) {
     cout << "[" << this->key << "] " << this->value;
    }
}
