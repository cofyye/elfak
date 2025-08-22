#pragma once

#include "HashObject.h"
#include "LinkedListNode.h"

class LinkedList {
  private:
    LinkedListNode* head;
    int size;
    
  public:
    LinkedList();
    ~LinkedList();
    void insertToHead(HashObject obj);
    void insertToTail(HashObject obj);
    void deleteFromHead();
    void deleteFromTail();
    void deleteFromHeadFirst(int n);
    void deleteFromTailFirst(int n);
    void deleteNode(char* key);
    void print();
    
    bool isEmpty();
    LinkedListNode* getHead();
    HashObject getFromHead();
    HashObject getFromTail();
};