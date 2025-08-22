#include <cstring>

#include "HashTable.h"

unsigned int HashTable::hashCode(char* key) {
  // Moduo method hash table : Example => key % N = index (8 % 5 = 3)
  unsigned int hash = 0;
  
  for(unsigned int i = 0; i < std::strlen(key); i++) {
    hash += key[i];
  }
    
  return hash % this->size;
}