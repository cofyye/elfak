#pragma once
class Stack
{
protected:
	char* array; 	// polje elemenata
	int size;	// veličina polja
	int top;	// indeks vršnog elementa
public:
	Stack(int nsize);
	bool isEmpty();
	int numberOfElements();
	~Stack();
	void push(int element); 
	char pop(); 
	char getTop();
};