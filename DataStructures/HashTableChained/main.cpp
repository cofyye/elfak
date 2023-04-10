#include <iostream>

#include "ChainedHashTable.h"

using namespace std;

int main()
{
  	ChainedHashTable table(10);
  	char index[] = "18724";
  	char name[] = "Filip Lakicevic";
  	HashObject o(index, name);
  	table.insert(o);
  	char index2[] = "18999";
  	char name2[] = "Strukture Podataka";
  	HashObject o1(index2, name2);
  	HashObject o2(index, name);
  	table.insert(o1);
  	table.insert(o2);
  	table.insert(o1);
  	table.insert(o1);
  	table.insert(o2);
  	table.withdraw(index);
  	table.withdraw(index2);
  	table.print();

    return 0;
}
