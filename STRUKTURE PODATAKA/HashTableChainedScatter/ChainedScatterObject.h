#pragma once

#include "ScatterObject.h"

class ChainedScatterObject : public ScatterObject {
  public:
    unsigned int next;
    ChainedScatterObject();
    ChainedScatterObject(char* key, char* value);
    ChainedScatterObject(char* key, char* value, unsigned int n);
    ChainedScatterObject(const ChainedScatterObject& obj);
    ChainedScatterObject& operator=(const ChainedScatterObject& obj);
    void print();
};