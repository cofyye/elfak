#include <iostream>

#include "ChainedHashTable.h"
#include "LinkedListNode.h"

using namespace std;

ChainedHashTable::ChainedHashTable(unsigned int len) {
  this->size = len;
  this->count = 0;
  this->array = new LinkedList[this->size];
}

ChainedHashTable::~ChainedHashTable() {
  for(unsigned int i = 0; i < this->size; i++) {
    while(!this->array[i].isEmpty()) {
      this->array[i].deleteFromHead();
    } 
  }
  this->size = 0;
  this->count = 0;
  delete[] this->array;
}

void ChainedHashTable::insert(HashObject obj) {
  this->array[this->hashCode(obj.getKey())].insertToHead(obj);
}

HashObject ChainedHashTable::find(char* key) {
  unsigned int index = this->hashCode(key);
  LinkedListNode* node = this->array[index].getHead();
  
  while(node != nullptr && !node->getNode().isEqualKey(key)) {
    node = node->getNextNode();
  }
  
  if(node == nullptr) {
    return HashObject();
  } else {
    return node->getNode();
  }
}

void ChainedHashTable::withdraw(HashObject obj) {
  this->array[this->hashCode(obj.getKey())].deleteNode(obj.getKey());
  this->count -= 1;
}

void ChainedHashTable::withdraw(char* key) {
  HashObject obj = this->find(key);
  this->withdraw(obj);
}

void ChainedHashTable::print() {
  for(unsigned int i = 0; i < this->size; i++) {
    cout << "Index : " << i << " -> "; 
    LinkedListNode* node = this->array[i].getHead();
    while(node != nullptr) {
      node->print();
      node = node->getNextNode();
      cout << " | ";
    }
    if(node == nullptr) cout << "\n";
    delete node;
  }
}