#pragma once
#include <iostream>
using namespace std;
class LListNode
{
public:
	int info;
	LListNode *next;
	LListNode();
	LListNode(int i);
	LListNode(int i, LListNode* n);
	int print();
	bool isEqual(int el);
	~LListNode();
};

