#include <cstring>
#include <iostream>
#include "HashTable.h"

using namespace std;

unsigned int HashTable::hashCode(char* key) {
  // Moduo hash method : Example -> key % size = index (8 % 5 = 3)
  unsigned int hash = 0;
  for(unsigned int i = 0; i < strlen(key); i++) {
    hash += key[i];
  }
  
  return hash % this->size;
}