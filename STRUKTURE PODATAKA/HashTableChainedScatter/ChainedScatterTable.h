#pragma once

#include "HashTable.h"
#include "ChainedScatterObject.h"

class ChainedScatterTable : public HashTable {
  private:
    ChainedScatterObject* array;
    unsigned int size2;
  public:
    ChainedScatterTable(unsigned int len);
    ~ChainedScatterTable();
    void print();
    void insert(ChainedScatterObject obj);
    ChainedScatterObject find(char* key);
};