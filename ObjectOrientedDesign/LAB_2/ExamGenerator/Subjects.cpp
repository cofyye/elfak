#include <iostream>
#include <windows.h>

#include "Subjects.h"

#define SLEEP_TIME_1 500
#define SLEEP_TIME_2 800

using namespace std;

Subjects::Subjects() {
	// Initialization
	this->currentEl = 0;
	this->arr = new Subject*[this->currentEl];
}

Subjects::~Subjects() {
	// Destructor
	if (this->arr != 0) {
		delete[] this->arr;
	}
	this->currentEl = 0;
}

void Subjects::push(Subject* element) {
	// Change size of current elements to one more
	this->currentEl += 1;

	// Initialization of a new array
	Subject** newArr = new Subject*[this->currentEl];

	// Check if size of current elements greater than one
	if (this->currentEl > 1) {
		// Copy old elements into new array
		for (int i = 0; i < this->currentEl - 1; i++) {
			newArr[i] = this->arr[i];
		}
	}
	
	// Push element into array
	newArr[this->currentEl - 1] = element;

	// Delete old array
	delete[] this->arr;

	// Initialization of a old array to new one
	this->arr = newArr;
}

void Subjects::printSubjects() {
	// Print list of all subjects
	cout << "\u001b[32m[OUTPUT] \u001b[36mList of all subjects [\u001b[33m" << this->getNumberOfElements() << "\u001b[36m]\u001b[0m \n";
	Sleep(SLEEP_TIME_2);
	for (int i = 0; i < this->currentEl; i++) {
		cout << "\u001b[32m[OUTPUT] \u001b[36mSubject name : \u001b[33m" << this->arr[i]->getName() << "\u001b[0m \n";
		Sleep(SLEEP_TIME_1);
	}
}

void Subjects::printAreas() {
	// Print list of all areas from all subjects
	for (int j = 0; j < this->currentEl; j++) {
		cout << "\u001b[32m[OUTPUT] \u001b[36mList of all areas [\u001b[33m" << this->arr[j]->areas->getNumberOfElements() << "\u001b[36m] from subject [\u001b[33m" << this->arr[j]->name << "\u001b[36m] \u001b[0m \n";
		Sleep(SLEEP_TIME_2);
		for (int i = 0; i < this->arr[j]->areas->getNumberOfElements(); i++) {
			cout << "\u001b[32m[OUTPUT] \u001b[36mArea name : \u001b[33m" << this->arr[j]->areas->getElement(i)->getName() << "\u001b[0m \n";
			Sleep(SLEEP_TIME_1);
		}
	}
}


Subject Subjects::pop() {
	// If list is empty, return empty subject
	if (this->currentEl < 1) {
		return Subject();
	}

	// Initialization of a new array
	Subject** newArr = new Subject*[this->currentEl];
	
	// Initialization of a element who will be removed
	Subject temp = *this->arr[this->currentEl-1];

	// Remove element from tail
	for (int i = 0; i < this->currentEl-1; i++) {
		newArr[i] = this->arr[i];
	}

	// Delete old array
	delete[] this->arr;

	// Decrease current element for one
	this->currentEl -= 1;
	// Initialization of a old array to new one
	this->arr = newArr;
	
	// Return removed element
	return temp;
}

Subject Subjects::shift() {
	// If list is empty, return empty subject
	if (this->currentEl < 1) {
		return Subject();
	}

	// Initialization of a new array
	Subject** newArr = new Subject * [this->currentEl];

	// Initialization of a element who will be removed
	Subject temp = *this->arr[0];

	// Remove element from head
	for (int i = 1, j = 0; i < this->currentEl; i++, j++) {
		newArr[j] = this->arr[i];
	}

	// Delete old array
	delete[] this->arr;

	// Decrease current element for one
	this->currentEl -= 1;
	// Initialization of a old array to new one
	this->arr = newArr;

	// Return removed element
	return temp;
}

Subject Subjects::remove(int index) {
	// If list is empty, return empty subject
	if (this->currentEl < 1) {
		return Subject();
	}

	// Check if index is correct
	if (index < 0 || index >= this->currentEl) {
		return Subject();
	}

	// Initialization of a new array
	Subject** newArr = new Subject * [this->currentEl+1];

	// Initialization of a element who will be removed
	Subject temp = *this->arr[index];

	// Remove element with specific index
	int j = 0;
	for (int i = 0; i < index; i++, j++) {
		newArr[j] = this->arr[i];
	}
	for (int i = index + 1; i < this->currentEl; i++, j++) {
		newArr[j] = this->arr[i];
	}

	// Delete old array
	delete[] this->arr;

	// Decrease current element for one
	this->currentEl -= 1;
	// Initialization of a old array to new one
	this->arr = newArr;

	// Return removed element
	return temp;
}

void Subjects::loadAllAssets() {
	// Load all assets for each subject like areas, questions, tasks etc etc
	for (int i = 0; i < this->currentEl; i++) {
		this->arr[i]->loadAllAssets();
	}
}