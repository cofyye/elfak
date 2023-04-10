#pragma once

class HashTable {
  protected:
    unsigned int size; // size of table
    unsigned int count; // current element(s) in table
    
    unsigned int hashCode(char* key); // generate a hash code with passed key
  
  public:
    inline unsigned int getLength() {
      return this->count;
    }
};