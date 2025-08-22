#pragma once

#include "DoublyLinkedListNode.h"

class DoublyLinkedList {
  private:
    DoublyLinkedListNode* head;
    int size;
    
  public:
    DoublyLinkedList();
    ~DoublyLinkedList();
    void insertToHead(int num);
    void insertToTail(int num);
    void deleteFromHead();
    void deleteFromTail();
    void deleteFromHeadFirst(int n);
    void deleteFromTailFirst(int n);
    void deleteNode(int num);
    void print();
    void printReverse();
    
    int getFromHead();
    int getFromTail();
};