#include <string>
#include <iostream>
#include "OpenScatterTable.h"

using namespace std;

OpenScatterTable::OpenScatterTable(unsigned int len) {
  this->size = len;
  this->count = 0;
  this->array = new ScatterObject[this->size];
}

OpenScatterTable::~OpenScatterTable() {
  if(this->array != nullptr) delete[] this->array;
}

unsigned int OpenScatterTable::findUnoccupied(ScatterObject obj) {
  unsigned int hashIndex = this->hashCode(obj.getKey());
  unsigned int index = hashIndex;
  if(this->array[index].status < 2) {
    return index;
  }
  
  unsigned int i = 1;
  do {
    index = (hashIndex + this->c(i)) % this->size;
    if(this->array[index].status < 2) {
      return index;
    } else {
      i++;
    }
  } while(index != hashIndex && i < this->size);
  string error = "Table space is full.";
  throw error;
}

unsigned int OpenScatterTable::findIndex(char* key) {
  unsigned int hashIndex = this->hashCode(key);
  if(this->array[hashIndex].isEqualKey(key)) {
    return hashIndex;
  }
  unsigned int i = 1;
  unsigned int index = hashIndex;
  do {
    index = (hashIndex + this->c(i)) % this->size;
    if(this->array[index].isEqualKey(key)) {
      return index;
    } else {
      i++;
    }
  } while(index != hashIndex && i < this->size);
  
  return -1;
}

ScatterObject OpenScatterTable::find(char* key) {
  return ScatterObject();
} 

void OpenScatterTable::insert(ScatterObject obj) {
  if(this->count == this->size) {
    string error = "Table space is full.";
    throw error;
  }
  
  unsigned int offset = this->findUnoccupied(obj);
  this->array[offset] = obj;
  this->array[offset].status = 2;
  this->count += 1;
}

unsigned int OpenScatterTable::c(unsigned int i) {
  return i*i;
}

void OpenScatterTable::withdraw(ScatterObject obj) {
    if (this->count == 0) {
        string error = "Table is empty.";
        throw error;
    }

    unsigned int index = this->findIndex(obj.getKey());

    if (index == -1) {
        string error = "Element not found.";
        throw error;
    }
    
    this->array[index].status = 1;
    this->array[index].deleteValue();
    this->count -= 1;
}

void OpenScatterTable::print() {
    for (unsigned int i = 0; i < this->size; i++) {
        cout << i << " ";
        this->array[i].print();
        cout << "\n";
    }
}
