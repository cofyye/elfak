#pragma once

#include "LinkedListNode.h"

class LinkedList {
  private:
    LinkedListNode* head;
    int size;
    
  public:
    LinkedList();
    ~LinkedList();
    void insertToHead(int num);
    void insertToTail(int num);
    void deleteFromHead();
    void deleteFromTail();
    void deleteFromHeadFirst(int n);
    void deleteFromTailFirst(int n);
    void deleteNode(int num);
    void print();
    
    int getFromHead();
    int getFromTail();
};