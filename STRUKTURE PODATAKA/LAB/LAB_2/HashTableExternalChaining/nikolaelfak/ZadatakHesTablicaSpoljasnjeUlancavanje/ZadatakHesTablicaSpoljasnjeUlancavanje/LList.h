#pragma once
#pragma once
#include "LListNode.h"
class LList
{
protected:
	LListNode* head;
public:
	LList();
	~LList();
	bool isEmpty();
	void addToHead(HashObject el);
	void addToTail(HashObject el);
	HashObject deleteFromHead();
	HashObject deleteFromTail();
	LListNode* findNodePtr(char* key);
	LListNode* getHead();
	LListNode* getNext(LListNode* ptr);
	HashObject getHeadEl();
	HashObject getNextEl(char* key);
	void printAll();
	bool isInList(char* key);
	void deleteEl(char* key);
	LListNode* removeNodeFromHead();
};

