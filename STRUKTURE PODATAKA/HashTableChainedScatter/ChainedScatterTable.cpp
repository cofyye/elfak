#include <iostream>
#include <string>

#include "ChainedScatterTable.h"

using namespace std;

ChainedScatterTable::ChainedScatterTable(unsigned int len) {
  this->size = len;
  this->count = 0;
  this->size2 = len;
  this->array = new ChainedScatterObject[2 * this->size];
  for(unsigned int i = 0; i < 2 * this->size - 1; i++) {
    this->array[i].next = i + 1;
  }
  this->array[2 * this->size - 1].next = 0;
}

ChainedScatterTable::~ChainedScatterTable() {
  if(this->array != nullptr) {
    delete[] this->array;
  }
}

void ChainedScatterTable::insert(ChainedScatterObject obj) {
  if(this->count == this->size) {
    string error = "Table space is full.";
    throw error;
  }
  
  unsigned int index = this->hashCode(obj.getKey());
  if(this->array[index].status == 2) {
    while(this->array[index].status == 2 && this->array[index].next != -1 && this->array[index].next == 0) {
      index = this->array[index].next;
    }
    if(this->array[index].next == 0) {
      string error = "Collision space is full.";
      throw error;
    }
    
    this->array[index].next = this->size2;
    index = this->size2;
    this->size2 = this->array[index].next;
  }
  
  this->array[index] = obj;
  this->array[index].status = 2;
  this->array[index].next = -1;
  this->count += 1;
}

ChainedScatterObject ChainedScatterTable::find(char* key) {
  unsigned int index = this->hashCode(key);
  
  while(index != -1) {
    if(!this->array[index].isEqualKey(key)) {
      index = this->array[index].next;
    } else {
      return this->array[index];
    }
  }
  return ChainedScatterObject();
}

void ChainedScatterTable::print() {
  	for (unsigned int i = 0; i < 2 * this->size; i++) {
		if (i == this->size) {
			cout << "---------------------------------------\n";
		}
		cout << i << " ";
		this->array[i].print();
	}
}