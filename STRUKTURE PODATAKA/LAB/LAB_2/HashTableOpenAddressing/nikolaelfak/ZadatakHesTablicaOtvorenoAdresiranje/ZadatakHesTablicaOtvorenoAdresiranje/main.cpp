#include"OpenScatterTable.h"

void main()
{
	OpenScatterTable tablica(64);
	ScatterObject o("2907982750052", "Nikola Davidovic");
	tablica.insert(o);
	ScatterObject o1("2706985730058", "Pera Peric");
	ScatterObject o2("2907982750052", "Nikola Davidovic");
	tablica.insert(o2);
	tablica.insert(o2);
	tablica.insert(o1);
	tablica.print();
	ScatterObject p = tablica.find("2907982750052");
	p.print();
}