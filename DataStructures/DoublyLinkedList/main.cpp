#include <iostream>
#include "DoublyLinkedList.h"

using namespace std;

int main()
{
    DoublyLinkedList* list = new DoublyLinkedList();
  
    list->insertToHead(2);
    list->insertToHead(5);
    list->insertToHead(10);
    list->insertToHead(43);
    list->insertToTail(101);
    list->insertToTail(110);
    list->insertToTail(111);
    list->insertToTail(352);
    
    list->deleteFromHeadFirst(2);
    list->deleteFromTailFirst(2);
    
    cout << "Number from head is : " << list->getFromHead() << endl;
    cout << "Number from tail is : " << list->getFromTail() << endl;
 
    list->deleteNode(2);
  
    list->print();
    
    delete list;

    return 0;
}
