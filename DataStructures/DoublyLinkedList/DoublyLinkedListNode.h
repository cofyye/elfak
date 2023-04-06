#pragma once

class DoublyLinkedListNode {
  private:
    int num;
    DoublyLinkedListNode* prev;
    DoublyLinkedListNode* next;
    
    DoublyLinkedListNode();
    DoublyLinkedListNode(int num);
    DoublyLinkedListNode(int num, DoublyLinkedListNode* prev, DoublyLinkedListNode* next);
    
    friend class DoublyLinkedList;
};