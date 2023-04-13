#pragma once

#include "Array.h"
#include "Subject.h"

class Subjects {
private:
	Array<Subject>* arr;
public:
	Subjects();
	~Subjects();
	void printSubjects();
	void printAreas();
	void printQTs(std::string type);
	void printQTsSpecific(Area* area, std::string type);
	void loadAllAssets();
	void push(Subject* subject);
	Subject* getElement(int index);
	inline int getNumberOfElements() {
		return this->arr->getNumberOfElements();
	}
};