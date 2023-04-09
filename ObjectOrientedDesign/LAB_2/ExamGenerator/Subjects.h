#pragma once

#include "Subject.h"

class Subjects {
private:
	int currentEl;
	Subject** arr;
public:
	Subjects();
	~Subjects();
	void push(Subject* element);
	void printSubjects();
	void printAreas();
	void loadAllAssets();
	Subject pop();
	Subject shift();
	Subject remove(int index);

	inline int getNumberOfElements() {
		return this->currentEl;
	}
};