#pragma once
#include"ChainedHashTable.h"

void main()
{
	ChainedHashTable tablica(64);
	char jmbg[] = "2907982750051";
	char ime[] = "Nikola Davidovic";
	HashObject o(jmbg, ime);
	tablica.insert(o);
	char jmbg2[] = "2706985730058";
	char ime2[] = "Pera Peric";
	HashObject o1(jmbg2, ime2);
	HashObject o2(jmbg, ime);
	tablica.insert(o2);
	tablica.insert(o2);
	tablica.insert(o1);
	tablica.insert(o1);
	tablica.insert(o2);
	tablica.print();
}