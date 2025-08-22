#pragma once
#include <iostream>
#include "HashObject.h"
using namespace std;
class LListNode
{
public:
	HashObject info;
	LListNode* next;
	LListNode();
	LListNode(HashObject i);
	LListNode(HashObject i, LListNode* n);
	void print();
	bool isEqual(char* key);
	~LListNode();
};