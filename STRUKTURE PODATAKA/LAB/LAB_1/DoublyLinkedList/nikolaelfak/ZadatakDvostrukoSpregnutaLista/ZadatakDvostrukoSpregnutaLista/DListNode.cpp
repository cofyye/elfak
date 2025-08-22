#include "DListNode.h"


DListNode::DListNode()
{
	prev = NULL; next = NULL;
}

DListNode::DListNode(int el)
{ 
	info = el; 
	prev = NULL; 
	next = NULL; 
}

DListNode::DListNode(int el, DListNode *p, DListNode *n)
{
	info = el;
	next = n; 
	prev = p;
}

DListNode::~DListNode()
{
}
