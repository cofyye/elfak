#include <string>
#include <iostream> 

#include "OpenScatterTable.h"

using namespace std;

int main()
{
  try {
  	OpenScatterTable table(11);
  	char index[] = "18724";
  	char name[] = "Filip Lakicevic";
  	char index2[] = "18999";
  	char name2[] = "Strukture Podataka";  	
  	ScatterObject o(index, name);
  	table.insert(o);
  	ScatterObject o1(index2, name2);
  	ScatterObject o2(index, name);
  	table.insert(o2);
  	table.insert(o2);
  	table.insert(o1);
  	table.print();
  } catch (string error) {
    cout << "[ERROR] " << error << "\n";
  }
	
	return 0;
}