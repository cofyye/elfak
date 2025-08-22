#include <iostream>
#include "LinkedList.h"
#include "LinkedListNode.h"

using namespace std;

LinkedList::LinkedList() {
  this->head = nullptr;
  this->size = 0;
}

LinkedList::~LinkedList() {
  while(this->head != nullptr) {
    this->deleteFromHead();
  }
}

void LinkedList::insertToHead(int num) {
  LinkedListNode* lln = new LinkedListNode(num, this->head);
  this->head = lln;
  this->size += 1;
}

void LinkedList::insertToTail(int num) {
  LinkedListNode* tmp = this->head;
  while(tmp->next != nullptr) {
    tmp = tmp->next;
  }
  
  LinkedListNode* lln = new LinkedListNode(num, nullptr);
  tmp->next = lln;
  this->size += 1;
}

void LinkedList::deleteFromHead() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  LinkedListNode* tmp = this->head;
  this->head = this->head->next;
  delete tmp;
  this->size -= 1;
}

void LinkedList::deleteFromTail() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  if(this->head->next == nullptr) {
    this->deleteFromHead();
    return;
  }
  
  LinkedListNode* tmp = this->head;
  LinkedListNode* prev = nullptr;
  
  while(tmp->next != nullptr) {
    prev = tmp;
    tmp = tmp->next;
  }
  
  prev->next = nullptr;
  delete tmp;
  this->size -= 1;
}

void LinkedList::deleteFromHeadFirst(int n) {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  if(n < 0) {
    cout << "Passed number must greater than zero." << endl;
    return;
  }
  
  if(this->size < n) {
    cout << "List size is less than passed number." << endl;
    return;
  }
  
  for(int i = 0; i < n; i++) {
    LinkedListNode* tmp = this->head;
    this->head = tmp->next;
    delete tmp;
    this->size -= 1;
  }
}

void LinkedList::deleteFromTailFirst(int n) {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  if(n < 0) {
    cout << "Passed number must greater than zero." << endl;
    return;
  }
  
  if(this->size < n) {
    cout << "List size is less than passed number." << endl;
    return;
  }
  
  if(this->size == n) {
    this->deleteFromHeadFirst(n);
    return;
  }
  
  for(int i = 0; i < n; i++) {
    LinkedListNode* tmp = this->head;
    LinkedListNode* prev = nullptr;
    
    while(tmp->next != nullptr) {
      prev = tmp;
      tmp = tmp->next;
    }
    prev->next = nullptr;
    delete tmp;
    this->size -= 1;
  }
}

void LinkedList::deleteNode(int num) {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  LinkedListNode* current = this->head;
  LinkedListNode* trail = nullptr;
  
  while(current != nullptr) {
    if(current->num == num) {
      break;
    } else {
      trail = current;
      current = current->next;
    }
  }
  if(current == nullptr) {
    cout << "Element not found." << endl;
    return;
  }
  if(this->head == current) {
    this->head = this->head->next;
  } else {
    trail->next = current->next;
  }
  
  delete current;
  this->size -= 1;
}

int LinkedList::getFromHead() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return rand();
  }
  
  return this->head->num;
}

int LinkedList::getFromTail() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return rand();
  }
  
  LinkedListNode* tmp = this->head;
  
  while(tmp->next != nullptr) {
    tmp = tmp->next;
  }
  
  return tmp->num;
}

void LinkedList::print() {
  
  LinkedListNode* tmp;
  tmp = this->head;
  
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  while(tmp != nullptr) {
    cout << tmp->num << endl;
    tmp = tmp->next;
  }
}