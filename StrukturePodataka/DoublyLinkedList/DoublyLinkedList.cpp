#include "DoublyLinkedList.h"
#include <iostream>

using namespace std;

DoublyLinkedList::DoublyLinkedList() {
  this->head = nullptr;
  this->size = 0;
}

DoublyLinkedList::~DoublyLinkedList() {
  while(this->head->next != nullptr) {
    this->deleteFromHead();
    this->head = this->head->next;
  }
}

void DoublyLinkedList::insertToHead(int num) {
  DoublyLinkedListNode* el = new DoublyLinkedListNode(num, nullptr, this->head);
  this->head = el;
  if(this->head->next != nullptr) {
    this->head->next->prev = this->head;
  }
  
  this->size += 1;
}

void DoublyLinkedList::insertToTail(int num) {
  DoublyLinkedListNode* tmp = this->head;
  while(tmp->next != nullptr) {
    tmp = tmp->next;
  }
  
  DoublyLinkedListNode* el = new DoublyLinkedListNode(num, tmp, nullptr);
  tmp->next = el;
  this->size += 1;
} 

void DoublyLinkedList::printReverse() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  DoublyLinkedListNode* tmp = this->head;
  while(tmp->next != nullptr) {
    tmp = tmp->next;
  }
  
  DoublyLinkedListNode* tmp2 = tmp;
  while(tmp2 != nullptr) {
    cout << tmp2->num << endl;
    tmp2 = tmp2->prev;
  }
}

void DoublyLinkedList::print() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  DoublyLinkedListNode* tmp = this->head;
  while(tmp != nullptr) {
    cout << tmp->num << endl;
    tmp = tmp->next;
  }
}

void DoublyLinkedList::deleteFromHead() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  DoublyLinkedListNode* tmp = this->head;
  this->head = this->head->next;
  this->head->prev = nullptr;
  
  delete tmp;
  this->size -= 1;
}

void DoublyLinkedList::deleteFromTail() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  DoublyLinkedListNode* tmp = this->head;
  
  while(tmp->next != nullptr) {
    tmp = tmp->next;
  }
  
  DoublyLinkedListNode* tmp2 = tmp->prev;
  tmp2->next = nullptr;
  
  delete tmp;
  this->size -= 1;
}

void DoublyLinkedList::deleteFromHeadFirst(int n) {
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
    this->deleteFromHead();
  }
}

void DoublyLinkedList::deleteFromTailFirst(int n) {
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
    this->deleteFromTail();
  }
}

int DoublyLinkedList::getFromHead() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return rand();
  }
  
  return this->head->num;
}

int DoublyLinkedList::getFromTail() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return rand();
  }
  
  DoublyLinkedListNode* tmp = this->head;
  
  while(tmp->next != nullptr) {
    tmp = tmp->next;
  }
  
  return tmp->num;
}

void DoublyLinkedList::deleteNode(int num) {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  DoublyLinkedListNode* tmp = this->head;
  DoublyLinkedListNode* trail = nullptr;
  
  while(tmp != nullptr) {
    if(tmp->num == num) {
      trail = tmp;
      break;
    } else {
      tmp = tmp->next;
    }
  }
  
  if(trail == nullptr) {
    cout << "Element not found." << endl;
    return;
  }
  
  if(this->head == trail) {
    this->deleteFromHead();
    return;
  }

  DoublyLinkedListNode* prev = trail->prev;
  
  prev->next = trail->next;
  delete trail;
  this->size -= 1;
}

