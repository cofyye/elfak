#include "LinkedListNode.h"

LinkedListNode::LinkedListNode() {
  this->info = HashObject();
  this->next = nullptr;
}

LinkedListNode::LinkedListNode(HashObject obj) {
  this->info = obj;
  this->next = nullptr;
}

LinkedListNode::LinkedListNode(HashObject obj, LinkedListNode* lln) {
  this->info = obj;
  this->next = lln;
}

void LinkedListNode::print() {
  this->info.print();
}

LinkedListNode* LinkedListNode::getNextNode() {
  if(this->next != nullptr) {
    return this->next;
  } else {
    return nullptr;
  }
}

HashObject LinkedListNode::getNode() {
  return this->info;
}