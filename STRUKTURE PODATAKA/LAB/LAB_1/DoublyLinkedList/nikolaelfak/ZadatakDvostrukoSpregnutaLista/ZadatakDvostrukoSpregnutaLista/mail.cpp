#include"DList.h"

void main()
{
	DList *list = new DList();
	list->addToHead(6);
	list->addToHead(3);
	list->addToHead(2);
	list->addToHead(7);
	list->addToHead(1);
	list->addToHead(9);
	list->addToHead(4);

	list->printAll();

	delete list;
}