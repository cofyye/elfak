#include <iostream>

#include "ChainedScatterObject.h"

using namespace std;

ChainedScatterObject::ChainedScatterObject() : ScatterObject() {
  this->next = -1;
}

ChainedScatterObject::ChainedScatterObject(char* key, char* value) : ScatterObject(key, value) {
  this->next = -1;
}

ChainedScatterObject::ChainedScatterObject(char* key, char* value, unsigned int n) : ScatterObject(key, value) {
  this->next = n;
}

ChainedScatterObject::ChainedScatterObject(const ChainedScatterObject& obj) : ScatterObject(obj) {
  this->next = obj.next;
}

ChainedScatterObject& ChainedScatterObject::operator=(const ChainedScatterObject& obj) {
  if(this != &obj) {
    (ScatterObject&)(*this) = obj;
    this->next = obj.next;
  }
  
  return *this;
}

void ChainedScatterObject::print() {
  cout << "[" << this->key << "] " << this->value;
}