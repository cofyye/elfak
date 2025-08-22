#pragma once
#include"DListNode.h"
class DList
{
protected:
	DListNode *head;
public:
	DList();
	~DList();
	bool isEmpty() { return head == NULL; }
	void printAll();
	DListNode* findNodePtr(int el);
	DListNode* getHead() { return head; }
	int getHeadEl();
	int getNextEl(int el);
	int getPreviousEl(int el);
	bool isInList(int el);
	void deleteEl(int el);
	void addToHead(int el);
	void addToTail(int el);
	int deleteFromHead();
	int deleteFromTail();

};

