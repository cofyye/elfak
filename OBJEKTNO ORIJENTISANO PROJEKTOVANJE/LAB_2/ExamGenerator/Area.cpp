#include "Area.h"

using namespace std;

Area::Area() {
	// Initialization
	this->name = "";
	this->qt = new Array<QuestionTask>();
}

Area::Area(string name) {
	// Initialization
	this->name = name;
	this->qt = new Array<QuestionTask>();
}

void Area::addQuestionTask(QuestionTask* element) {
	// Push QuestionTask class into list
	this->qt->push(element);
}

int Area::getNumberOfElements(string type) {
	// Initialization of string wType
	string wType;
	
	// Handle values
	if (type == "questions") {
		wType = "Question";
	}
	else if (type == "tasks") {
		wType = "Task";
	}
	else {
		wType = "both";
	}

	// If wType is both, then return all questions and tasks
	if (wType == "both") return this->qt->getNumberOfElements();

	// Initialization of a counter
	int counter = 0;
	
	// For loop for getting a number of questions or tasks
	for (int i = 0; i < this->qt->getNumberOfElements(); i++) {
		if (wType == this->qt->getElement(i)->getType()) {
			counter += 1;
		}
	}

	// Return a that value
	return counter;
}

QuestionTask* Area::getElement(int index) {
	// Get element of a QuestionTask class from area
	return this->qt->getElement(index);
}