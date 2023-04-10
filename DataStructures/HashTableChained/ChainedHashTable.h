#pragma once

#include "HashTable.h"
#include "LinkedList.h"

class ChainedHashTable : public HashTable {
  private:
    LinkedList* array;
  public:
    ChainedHashTable(unsigned int len);
    ~ChainedHashTable();
    void insert(HashObject obj);
    void withdraw(HashObject obj);
    void withdraw(char* key);
    void print();
    HashObject find(char *key);
};