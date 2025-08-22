#include <string>
#include <iostream> 

#include "ChainedScatterTable.h"

using namespace std;

int main()
{
  try {
  	ChainedScatterTable table(10);
  	char index[] = "18724";
  	char name[] = "Filip Lakicevic";
  	ChainedScatterObject o(index, name);
  	table.insert(o);
  	char index2[] = "18999";
  	char name2[] = "Strukture Podataka";
  	ChainedScatterObject o1(index2, name2);
  	ChainedScatterObject o2(index, name);
  	table.insert(o2);
  	table.insert(o2);
  	table.insert(o1);
  	table.insert(o1);
  	table.insert(o2);
  	table.print(); 
  } catch (string error) {
    cout << "[ERROR] " << error << "\n";
  }
	
	return 0;
}