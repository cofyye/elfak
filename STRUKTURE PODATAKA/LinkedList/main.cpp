#include <iostream>
#include "LinkedList.h"

using namespace std;

int main()
{
    LinkedList* ll = new LinkedList();
    
    ll->insertToHead(5);
    ll->insertToHead(32);
    ll->insertToHead(12);
    ll->insertToHead(434);
    ll->insertToHead(69);
    ll->insertToTail(101);
    ll->insertToTail(707);
    ll->insertToTail(555);
    ll->insertToTail(482);
    ll->insertToTail(10);
    
    ll->print();
    
    cout << "Get from head : " << ll->getFromHead() << endl;
    cout << "Get from tail : " << ll->getFromTail() << endl;
    
    cout << "====================" << endl;
    
    ll->deleteNode(12);
    ll->deleteFromHead();
    ll->deleteFromTail();
    ll->deleteFromHeadFirst(2);
    ll->deleteFromTailFirst(2);
    
    ll->print();
    
    delete ll;
    
    return 0;
}
