#include "DList.h"



DList::DList()
{
	head = NULL;
}


DList::~DList()
{
	while (!isEmpty())
	{
		int tmp = deleteFromHead();
	}
}

DListNode* DList::findNodePtr(int el)
{
	if (isEmpty())
		throw new exception("List is empty");
	DListNode *ret = head;
	while (ret != NULL && ret->info != el)
	{
		ret = ret->next;
	}
	return ret;
}

int DList::getHeadEl()
{
	if (isEmpty())
		throw new exception("List is empty");
	return head->info;
}

int DList::getNextEl(int el)
{
	if (isEmpty())
		throw new exception("List is empty");
	DListNode* tmp = findNodePtr(el);
	if (tmp == NULL)
		throw new exception("Node doesn't exist");
	if (tmp->next == NULL)
		throw new exception("Element doesn't have next node");
	return tmp->next->info;
}

int DList::getPreviousEl(int el)
{
	if (isEmpty())
		throw new exception("List is empty");
	DListNode* tmp = findNodePtr(el);
	if (tmp == NULL)
		throw new exception("Node doesn't exist");
	if (tmp->prev == NULL)
		throw new exception("Element doesn't have previous node");
	return tmp->prev->info;
}

bool DList::isInList(int el)
{
	if (isEmpty())
		return false;
	DListNode* tmp = findNodePtr(el);
	if (tmp == NULL)
		return false;
	else
		return true;
}

void DList::deleteEl(int el)
{
	if (!isEmpty())
		if (head->isEqual(el))
		{
			DListNode *tmp = head;
			head = head->next;
			head->prev = NULL;
			delete tmp;
		}
		else
		{
			DListNode *tmp;
			for (tmp = head->next; tmp != NULL && !(tmp->isEqual(el)); tmp = tmp->next);
			if (tmp != NULL)
			{
				tmp->prev->next = tmp->next;
				tmp->next->prev = tmp->prev;
				delete tmp;
			}
		}
}

void DList::addToHead(int el)
{
	head = new DListNode(el, NULL, head);
	if(head->next!=NULL)
		head->next->prev = head;
}

void DList::addToTail(int el)
{
	if (!isEmpty())
	{
		DListNode* tmp = head;
		while (tmp->next != NULL)
			tmp = tmp->next;
		tmp->next = new DListNode(el, tmp, NULL);
	}
	else
		addToHead(el);
}

int DList::deleteFromHead()
{
	if (isEmpty())
		throw new exception("List is empty");
	int el = head->info;
	DListNode *tmp = head;
	head = head->next;
	if(head!=NULL)
		head->prev = NULL;
	delete tmp;
	return el;
}

int DList::deleteFromTail()
{
	if (isEmpty())
		throw new exception("List is empty");
	int el;
	if (head->next == NULL)
	{
		return deleteFromHead();
	}
	else
	{
		DListNode *tmp = head;
		while (tmp->next != NULL)
		{
			tmp = tmp->next;
		}
		el = tmp->info;
		tmp->prev->next = NULL;
		delete tmp;
	}
	return el;
}

void DList::printAll()
{
	DListNode *tmp = head;
	while (tmp != NULL)
	{
		cout << tmp->print() << " ";
		tmp = tmp->next;
	}
	cout << endl;
}