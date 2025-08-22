#pragma once
#include"HashObject.h"
class HashTable
{
protected:
	unsigned int m; // velicina tablice
	unsigned int count;  //broj elemenata u tablici
protected:
	unsigned int h(char* key);

	// primarna transformacija 
	unsigned int f(char * key);
	unsigned int g(unsigned int key);
public:
	unsigned int getLength() { return m; }
	
};

