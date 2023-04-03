#pragma once

class LinkedListNode {
  private:
    int num;
    LinkedListNode* next;
    
    LinkedListNode();
    LinkedListNode(int num);
    LinkedListNode(int num, LinkedListNode* lln);
    
    friend class LinkedList;
};