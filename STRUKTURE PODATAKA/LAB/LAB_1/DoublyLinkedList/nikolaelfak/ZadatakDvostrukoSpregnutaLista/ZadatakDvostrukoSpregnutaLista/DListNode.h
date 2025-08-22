#pragma once
#include <iostream>
using namespace std;
class DListNode
{
public:
	int info;
	DListNode *next, *prev;
	DListNode();
	DListNode(int el);
	DListNode(int el, DListNode *p, DListNode *n);
	~DListNode();
	int print() { return info; }
	bool isEqual(int el) { return el == info; }
};

