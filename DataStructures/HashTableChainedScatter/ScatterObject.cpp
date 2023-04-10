#include "ScatterObject.h"

ScatterObject::ScatterObject() : HashObject() {
  this->status = 0;
}

ScatterObject::ScatterObject(char* key, char* value) : HashObject(key, value) {
  this->status = 0;
}

ScatterObject::ScatterObject(const ScatterObject& obj) : HashObject(obj) {
  this->status = obj.status;
}

ScatterObject& ScatterObject::operator=(const ScatterObject& obj) {
  if(this != &obj) {
      (HashObject&)(*this) = obj;
      this->status = obj.status;
  }
  
  return *this;
}