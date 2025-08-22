#pragma once

#include "HashObject.h"

class LinkedListNode {
  private:
    HashObject info;
    LinkedListNode* next;
    
    LinkedListNode();
    LinkedListNode(HashObject obj);
    LinkedListNode(HashObject obj, LinkedListNode* lln);
    
    friend class LinkedList;
  public:
    void print();
    LinkedListNode* getNextNode();
    HashObject getNode();
};