#include"ChainedScatterTable.h"

void main()
{
	ChainedScatterTable tablica(64);
	ChainedScatterObject o("2907982750051", "Nikola Davidovic");
	tablica.insert(o);
	ChainedScatterObject o1("2706985730058", "Pera Peric");
	ChainedScatterObject o2("2907982750051", "Nikola Davidovic");
	tablica.insert(o2);
	tablica.insert(o2);
	tablica.insert(o1);
	tablica.insert(o1);
	tablica.insert(o2);
	tablica.print();
}