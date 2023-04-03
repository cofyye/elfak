#include "LinkedListNode.h"

LinkedListNode::LinkedListNode() {
  this->num = 0;
  this->next = nullptr;
}

LinkedListNode::LinkedListNode(int num) {
  this->num = num;
  this->next = nullptr;
}

LinkedListNode::LinkedListNode(int num, LinkedListNode* lln) {
  this->num = num;
  this->next = lln;
}