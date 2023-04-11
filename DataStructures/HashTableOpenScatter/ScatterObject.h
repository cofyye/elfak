#pragma once

#include "HashObject.h"

class ScatterObject : public HashObject {
  public:
    int status; // 0 - free, 1 - deleted, 2 - busy
    ScatterObject();
    ScatterObject(char* key, char* value);
    ScatterObject(const ScatterObject& obj);
    ScatterObject& operator=(const ScatterObject& obj);
};