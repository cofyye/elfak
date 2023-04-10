#pragma once

#include <cstring>

class HashObject {
  private:
    char* key;
    char* value;
  public:
    HashObject();
    HashObject(char* key, char* value);
    HashObject(const HashObject& obj);
    ~HashObject();
    void deleteValue();
    void print();
    HashObject& operator=(const HashObject& obj);
    inline bool operator==(const HashObject& obj) {
      return (std::strcmp(this->key, obj.key) == 0) && (std::strcmp(this->value, obj.value) == 0);
    }
    inline bool isEqualKey(char* key) {
      return std::strcmp(this->key, key) == 0;
    }
    inline char* getKey() {
      return this->key;
    }
    inline char* getValue() {
      return this->value;
    }
};