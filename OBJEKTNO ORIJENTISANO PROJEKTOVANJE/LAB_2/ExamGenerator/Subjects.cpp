#include <iostream>
#include <windows.h>

#include "Subjects.h"

#define SLEEP_TIME_1 500
#define SLEEP_TIME_2 800

using namespace std;

Subjects::Subjects() {
	// Initialization
	this->arr = new Array<Subject>[1];
}

Subjects::~Subjects() {
	// Destructor
	if (this->arr != 0) {
		delete[] this->arr;
	}
}

void Subjects::printSubjects() {
	// Print list of all subjects
	cout << "\u001b[32m[OUTPUT] \u001b[36mList of all subjects [\u001b[33m" << this->arr->getNumberOfElements() << "\u001b[36m]\u001b[0m \n";
	Sleep(SLEEP_TIME_2);
	for (int i = 0; i < this->arr->getNumberOfElements(); i++) {
		cout << "\u001b[32m[OUTPUT] \u001b[36mSubject name : \u001b[33m" << this->arr->getElement(i)->getName() << "\u001b[0m \n";
		Sleep(SLEEP_TIME_1);
	}
}

void Subjects::printAreas() {
	// Print list of all areas from all subjects
	for (int j = 0; j < this->arr->getNumberOfElements(); j++) {
		cout << "\u001b[32m[OUTPUT] \u001b[36mList of all areas [\u001b[33m" << this->arr->getElement(j)->areas->getNumberOfElements() << "\u001b[36m] from subject [\u001b[33m" << this->arr->getElement(j)->name << "\u001b[36m] \u001b[0m \n";
		Sleep(SLEEP_TIME_2);
		for (int i = 0; i < this->arr->getElement(j)->areas->getNumberOfElements(); i++) {
			cout << "\t\u001b[32m[OUTPUT] \u001b[36mArea name : \u001b[33m" << this->arr->getElement(j)->areas->getElement(i)->getName() << "\u001b[0m \n";
			Sleep(SLEEP_TIME_1);
		}
	}
}

void Subjects::printQTs(string type) {
	// Print all questions or tasks of all areas of all subjects
	// Loop through all subjects
	for (int i = 0; i < this->arr->getNumberOfElements(); i++) {
		// Set wType from type to print correct value
		string wType = type == "questions" || type == "tasks" ? type : "questions and tasks";
		// For each subject, loop through all his areas
		for (int j = 0; j < this->arr->getElement(i)->areas->getNumberOfElements(); j++) {
			// Print info about subject and area
			cout << "\u001b[32m[OUTPUT] \u001b[36mList of all " << wType << " [\u001b[33m" << this->arr->getElement(i)->areas->getElement(j)->getNumberOfElements(wType) << "\u001b[36m] from area [\u001b[33m" << this->arr->getElement(i)->areas->getElement(j)->getName() << "\u001b[36m] and subject [\u001b[33m" << this->arr->getElement(i)->getName() << "\u001b[36m] \u001b[0m \n";
			Sleep(SLEEP_TIME_2);
			// If wType is tasks, must fix next problem
			// Starting index must set from end of questions type
			int startIndex = wType == "tasks" ? this->arr->getElement(i)->areas->getElement(j)->getNumberOfElements("questions") : 0;
			int endIndex = wType == "questions" ? this->arr->getElement(i)->areas->getElement(j)->getNumberOfElements("questions") : this->arr->getElement(i)->areas->getElement(j)->getNumberOfElements("both");
			// For each area, loop through all his questions and tasks
			for (int k = startIndex; k < endIndex; k++) {
				// Print info about subject, area and questions or tasks or both depends on wType
				cout << "\t\u001b[32m[OUTPUT] \u001b[36m" << this->arr->getElement(i)->areas->getElement(j)->getElement(k)->getType() << " name : \u001b[33m" << this->arr->getElement(i)->areas->getElement(j)->getElement(k)->getName() << " \u001b[0m \n";
				Sleep(SLEEP_TIME_1);
			}
		}
	}
}

void Subjects::printQTsSpecific(Area* area, string type) {
	// Print all questions or tasks of all areas of subject
	
	// Set wType from type to print correct value
	string wType = type == "questions" || type == "tasks" ? type : "questions and tasks";

	// Print info about subject and area
	cout << "\u001b[32m[OUTPUT] \u001b[36mList of all " << wType << " [\u001b[33m" << area->getNumberOfElements(wType) << "\u001b[36m] from area [\u001b[33m" << area->getName() << "\u001b[36m] and subject [\u001b[33m" << area->getName() << "\u001b[36m] \u001b[0m \n";
	Sleep(SLEEP_TIME_2);
	// If wType is tasks, must fix next problem
	// Starting index must set from end of questions type
	int startIndex = wType == "tasks" ? area->getNumberOfElements("questions") : 0;
	int endIndex = wType == "questions" ? area->getNumberOfElements("questions") : area->getNumberOfElements("both");
	// Loop through all his questions and tasks
	for (int i = startIndex; i < endIndex; i++) {
		// Print info about area and questions or tasks or both depends on wType
		cout << "\t\u001b[32m[OUTPUT] \u001b[36m" << area->getElement(i)->getType() << " name : \u001b[33m" << area->getElement(i)->getName() << " \u001b[0m \n";
		Sleep(SLEEP_TIME_1);
	}
}

void Subjects::loadAllAssets() {
	// Load all assets for each subject like areas, questions, tasks etc etc
	for (int i = 0; i < this->arr->getNumberOfElements(); i++) {
		this->arr->getElement(i)->loadAllAssets();
	}
}

void Subjects::push(Subject* subject) {
	// Push subject into array;
	this->arr->push(subject);
}

Subject* Subjects::getElement(int index) {
	// Return a element of this index
	return this->arr->getElement(index);
}