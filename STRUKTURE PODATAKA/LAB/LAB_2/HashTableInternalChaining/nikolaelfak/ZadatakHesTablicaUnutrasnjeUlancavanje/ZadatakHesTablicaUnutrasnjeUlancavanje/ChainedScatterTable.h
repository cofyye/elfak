#pragma once
#include "HashTable.h"
#include "ChainedScatterObject.h"
class ChainedScatterTable :
	public HashTable
{
protected: 
	ChainedScatterObject* array;
	unsigned int lrmp;
public:
	ChainedScatterTable(unsigned int length);
	~ChainedScatterTable();
	void insert(ChainedScatterObject obj);
	void print();
	ChainedScatterObject find(char* key);
};

