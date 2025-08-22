#pragma once

class HashTable {
  protected:
    unsigned int size; // size of a table
    unsigned int count; // current element(s) in a table
    
    unsigned int hashCode(char* key); // generate a hash
  
  public:
    inline unsigned int getLength() {
      return this->count;
    }
};