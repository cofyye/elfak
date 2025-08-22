#pragma once

#include "HashTable.h"
#include "ScatterObject.h"

class OpenScatterTable : public HashTable {
  private:
    ScatterObject* array;
  public:
    OpenScatterTable(unsigned int len);
    ~OpenScatterTable();
    unsigned int findUnoccupied(ScatterObject obj);
    unsigned int findIndex(char* key);
    ScatterObject find(char* key);
    void print();
    void insert(ScatterObject obj);
    void withdraw(ScatterObject obj);
    unsigned int c(unsigned int i);
};