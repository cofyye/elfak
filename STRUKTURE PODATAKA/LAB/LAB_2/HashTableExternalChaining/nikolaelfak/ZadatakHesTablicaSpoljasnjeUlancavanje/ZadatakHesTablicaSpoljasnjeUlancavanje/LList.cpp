#include "LList.h"


LList::LList()
{
	head = NULL;
}


LList::~LList()
{
	while (!isEmpty())
	{
		HashObject tmp = deleteFromHead();
	}
}


bool LList::isEmpty()
{
	return head == NULL;
}

void LList::addToHead(HashObject el)
{
	head = new LListNode(el, head);
}

void LList::addToTail(HashObject el)
{
	if (!isEmpty())
	{
		LListNode* tmp = head;
		while (tmp->next != NULL)
			tmp = tmp->next;
		tmp->next = new LListNode(el);
	}
	else
		addToHead(el);
}

HashObject LList::deleteFromHead()
{
	if (isEmpty())
		throw new exception("List is empty");
	HashObject el = head->info;
	LListNode* tmp = head;
	head = head->next;
	delete tmp;
	return el;
}

HashObject LList::deleteFromTail()
{
	if (isEmpty())
		throw new exception("List is empty");
	HashObject el;
	if (head->next == NULL)
	{
		el = head->info;
		delete head;
		head = NULL;
	}
	else
	{
		LListNode* prev = head;
		LListNode* tmp = head;
		while (tmp->next != NULL)
		{
			prev = tmp;
			tmp = tmp->next;
		}
		el = tmp->info;
		delete tmp;
		prev->next = NULL;
	}
	return el;
}

LListNode* LList::findNodePtr(char* key)
{
	if (isEmpty())
		throw new exception("List is empty");
	LListNode* ret = head;
	while (ret != NULL && !ret->info.isEqualKey(key))
	{
		ret = ret->next;
	}
	return ret;
}

LListNode* LList::getHead()
{
	return head;
}

LListNode* LList::getNext(LListNode* ptr)
{
	if (isEmpty())
		throw new exception("List is empty");
	return ptr->next;
}

HashObject LList::getHeadEl()
{
	if (isEmpty())
		throw new exception("List is empty");
	return head->info;
}

HashObject LList::getNextEl(char* key)
{
	if (isEmpty())
		throw new exception("List is empty");
	LListNode* tmp = findNodePtr(key);
	if (tmp == NULL)
		throw new exception("Node doesn't exist");
	if (tmp->next == NULL)
		throw new exception("Element doesn't have next node");
	return tmp->next->info;
}

void LList::printAll()
{
	LListNode* tmp = head;
	while (tmp != NULL)
	{
		tmp->print();
		cout << " ";
		tmp = tmp->next;
	}
	cout << endl;
}

bool LList::isInList(char* key)
{
	if (isEmpty())
		return false;
	LListNode* tmp = findNodePtr(key);
	if (tmp == NULL)
		return false;
	else
		return true;
}

void LList::deleteEl(char* key)
{
	if (!isEmpty())
		if (head->isEqual(key))
		{
			LListNode* tmp = head;
			head = head->next;
			delete tmp;
		}
		else
		{
			LListNode* pred, * tmp;
			for (pred = head, tmp = head->next; tmp != NULL && !(tmp->isEqual(key));
				pred = pred->next, tmp = tmp->next);
			if (tmp != NULL)
			{
				pred->next = tmp->next;
				delete tmp;
			}
		}
}

LListNode* LList::removeNodeFromHead()
{
	if (isEmpty())
		return NULL;
	LListNode* tmp = head;
	head = head->next;
	return tmp;
}

