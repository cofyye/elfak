#include "DoublyLinkedListNode.h"

DoublyLinkedListNode::DoublyLinkedListNode() {
  this->num = 0;
  this->prev = nullptr;
  this->next = nullptr;
}

DoublyLinkedListNode::DoublyLinkedListNode(int num) {
  this->num = num;
  this->prev = nullptr;
  this->next = nullptr;
}

DoublyLinkedListNode::DoublyLinkedListNode(int num, DoublyLinkedListNode* prev, DoublyLinkedListNode* next) {
  this->num = num;
  this->prev = prev;
  this->next = next;
}