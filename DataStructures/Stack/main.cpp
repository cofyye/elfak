#include <iostream>
#include <string>
#include "Stack.h"

using namespace std;

int main()
{
    try {
      Stack* stack = new Stack(3);
      
      stack->push(1);
      stack->push(2);
      stack->push(3);
      
      cout << "Pop element : " << stack->pop() << endl;
      cout << "Number of elements : " << stack->numberOfElements() << endl;
      cout << "Stack is empty : " << stack->isEmpty() << endl;
      cout << "Stack top element : " << stack->getTop() << endl;
      
      stack->print();
      
      delete stack; 
    } catch(string error) {
      cout << error << endl;
    }

    return 0;
}
