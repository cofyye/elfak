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

void LinkedList::insertToHead(HashObject obj) {
  LinkedListNode* lln = new LinkedListNode(obj, this->head);
  this->head = lln;
  this->size += 1;
}

void LinkedList::insertToTail(HashObject obj) {
  LinkedListNode* tmp = this->head;
  while(tmp->next != nullptr) {
    tmp = tmp->next;
  }
  
  LinkedListNode* lln = new LinkedListNode(obj, nullptr);
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

void LinkedList::deleteNode(char* key) {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  LinkedListNode* current = this->head;
  LinkedListNode* trail = nullptr;
  
  while(current != nullptr) {
    if(strcmp(current->info.getKey(), key) == 0) {
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

HashObject LinkedList::getFromHead() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return HashObject();
  }
  
  return this->head->info;
}

LinkedListNode* LinkedList::getHead() {
  return this->head;
}

HashObject LinkedList::getFromTail() {
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return HashObject();
  }
  
  LinkedListNode* tmp = this->head;
  
  while(tmp->next != nullptr) {
    tmp = tmp->next;
  }
  
  return tmp->info;
}

bool LinkedList::isEmpty() {
  return this->head != nullptr || this->size == 0;
}

void LinkedList::print() {
  
  LinkedListNode* tmp;
  tmp = this->head;
  
  if(this->head == nullptr) {
    cout << "List is empty." << endl;
    return;
  }
  
  while(tmp != nullptr) {
    tmp->info.print();
    tmp = tmp->next;
  }
}