#include "LListNode.h"


LListNode::LListNode()
{
	next = NULL;
}

LListNode::LListNode(HashObject i)
{
	info = i;
	next = NULL;
}

LListNode::LListNode(HashObject i, LListNode* n)
{
	info = i;
	next = n;
}

void LListNode::print()
{
	return info.print();
}

bool LListNode::isEqual(char* key)
{
	return this->info.isEqualKey(key);
}

LListNode::~LListNode()
{
}
