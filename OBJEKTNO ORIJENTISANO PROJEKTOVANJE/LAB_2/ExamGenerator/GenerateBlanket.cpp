#include <iostream>
#include <fstream>

#include "GenerateBlanket.h"

#define MAX_ATTEMPTS 10

using namespace std;

GenerateBlanket::GenerateBlanket(Subject* subject, int questions, int tasks) {
	// Initialization
	this->subject = subject;
	this->questions = questions;
	this->tasks = tasks;
	this->blankets = new Array<Blanket>[this->questions + this->tasks];
}

GenerateBlanket::~GenerateBlanket() {
	// Destructor
	if (this->blankets != nullptr) {
		delete[] this->blankets;
	}
}

int GenerateBlanket::random(int min, int max) {
	static bool first = true;
	if (first)
	{
		srand(time_t(NULL));
		first = false;
	}

	// Return random number between min and max
	return min + rand() % ((max + 1) - min);
}

bool GenerateBlanket::inList(int* arr, int counter, int value) {
	// Initialization
	bool isInList = false;

	// Check if value is in list
	for (int i = 0; i < counter; i++) {
		if (arr[i] == value) {
			isInList = true;
			break;
		}
	}

	// Return boolean 
	return isInList;
}

string GenerateBlanket::generateQuestions() {
	// Initialization
	int counter = 0;
	int* generatedIndex = new int[this->questions];
	
	// Loop X times for get a random question
	for (int i = 0; i < this->questions; i++) {
		// Generate area index
		int getRandomAreaIndex = (this->random(1, this->subject->getNumberOfElements())) - 1;
		int attempts = 0;

		// Get random area index until don't get different index or fill max attempts
		while (this->inList(generatedIndex, counter, getRandomAreaIndex) && attempts <= MAX_ATTEMPTS) {
			getRandomAreaIndex = (this->random(1, this->subject->getNumberOfElements())) - 1;
			attempts += 1;
		}

		// If it exits while loop for attempts, return an error
		if (attempts > MAX_ATTEMPTS) {
			string response = "\u001b[38;5;196m[ERROR] \u001b[38;5;9mToo much attempts \u001b[0m";
			return response;
		}

		// Check if have enough questions and areas for generate questions
		if (this->questions > this->subject->getNumberOfElements() || this->subject->getArea(getRandomAreaIndex)->getNumberOfElements("questions") < 1) {
			string response = "\u001b[38;5;196m[ERROR] \u001b[38;5;9mNot enough number of questions. Please add more questions or reduce a generate questions number \u001b[0m";
			return response;

		}

		// Add random area index into generated index
		generatedIndex[counter] = getRandomAreaIndex;
		counter++;

		// Get random question index
		int getRandomQuestionIndex = (this->random(1, this->subject->getArea(getRandomAreaIndex)->getNumberOfElements("questions"))) - 1;

		// Add blanket into list
		this->blankets->push(new Blanket(this->subject->getArea(getRandomAreaIndex), this->subject->getArea(getRandomAreaIndex)->getElement(getRandomQuestionIndex)));
	}

	// Return success response
	string response = "";
	return response;
}

string GenerateBlanket::generateTasks() {
	// Initialization
	int counter = 0;
	int* generatedIndex = new int[this->tasks];

	// Loop X times for get a random task
	for (int i = 0; i < this->tasks; i++) {
		// Generate area index
		int getRandomAreaIndex = (this->random(1, this->subject->getNumberOfElements())) - 1;
		int attempts = 0;

		// Get random area index until don't get different index or fill max attempts
		while (this->inList(generatedIndex, counter, getRandomAreaIndex) && attempts <= MAX_ATTEMPTS) {
			getRandomAreaIndex = (this->random(1, this->subject->getNumberOfElements())) - 1;
			attempts += 1;
		}

		// If it exits while loop for attempts, return an error
		if (attempts > MAX_ATTEMPTS) {
			string response = "\u001b[38;5;196m[ERROR] \u001b[38;5;9mToo much attempts \u001b[0m";
			return response;
		}

		// Check if have enough tasks and areas for generate tasks
		if (this->tasks > this->subject->getNumberOfElements() || this->subject->getArea(getRandomAreaIndex)->getNumberOfElements("tasks") < 1) {
			string response = "\u001b[38;5;196m[ERROR] \u001b[38;5;9mNot enough number of tasks. Please add more tasks or reduce a generate tasks number \u001b[0m";
			return response;

		}

		// Add random area index into generated index
		generatedIndex[counter] = getRandomAreaIndex;
		counter++;

		// Get random task index
		int getRandomTasksIndex = (this->random(this->subject->getArea(getRandomAreaIndex)->getNumberOfElements("questions") + 1, this->subject->getArea(getRandomAreaIndex)->getNumberOfElements("both"))) - 1;

		// Add blanket into list
		this->blankets->push(new Blanket(this->subject->getArea(getRandomAreaIndex), this->subject->getArea(getRandomAreaIndex)->getElement(getRandomTasksIndex)));
	}

	// Return success response
	string response = "";
	return response;
}

string GenerateBlanket::generate() {
	// Check if user want to generate a questions
	if (this->questions > 0) {
		string response = this->generateQuestions();
		if (response != "") {
			return response;
		}
	}

	// Check if user want to generate a tasks
	if (this->tasks > 0) {
		string response = this->generateTasks();
		if (response != "") {
			return response;
		}
	}

	// Return error if program for some reason didn't generate blanket
	if (this->blankets->getNumberOfElements() < 1) {
		string response = "\u001b[38;5;196m[ERROR] \u001b[38;5;9mError while generating blanket \u001b[0m";
		return response;
	}

	// Put blanket into file if successfully generated
	this->putIntoFile();

	// Return success response
	string success = "";
	return success;
}

void GenerateBlanket::print() {
	// Print blanket
	cout << "\u001b[32m[OUTPUT] \u001b[36mGenerated blanket is for subject [\u001b[33m" << this->subject->getName() << "\u001b[36m] \u001b[0m\n";
	cout << "\u001b[32m[OUTPUT] \u001b[36mThis blanket have [\u001b[33m" << this->questions << "\u001b[36m] question(s) and [\u001b[33m" << this->tasks << "\u001b[36m] task(s) \u001b[0m\n";
	for (int i = 0; i < this->blankets->getNumberOfElements(); i++) {
		cout << "\u001b[32m[OUTPUT] \u001b[36m" << this->blankets->getElement(i)->getQT()->getType() << " name : \u001b[33m" << this->blankets->getElement(i)->getQT()->getName() << "\u001b[36m from area [\u001b[33m" << this->blankets->getElement(i)->getArea()->getName() << "\u001b[36m]\u001b[0m\n";
	}
}

void GenerateBlanket::putIntoFile() {
	// Add generated blanket into file
	ofstream* historyFile = new ofstream("history.txt", std::ios::app);

	// Check if history.txt successfully opened
	if (!historyFile->is_open()) {
		// Define error
		string error = "\u001b[36mFile \u001b[33mhistory.txt\u001b[36m is not found !\u001b[0m";
		// Throw error
		throw error;
	}
	
	// Append into file
	*historyFile << "\nGenerated blanket is for subject [";
	*historyFile << this->subject->getName();
	*historyFile << "]\n";
	*historyFile << "This blanket have [" << this->questions << "] question(s) and [";
	*historyFile << this->tasks << "] task(s)\n";
	for (int i = 0; i < this->blankets->getNumberOfElements(); i++) {
		*historyFile << this->blankets->getElement(i)->getQT()->getType() << " name : " << this->blankets->getElement(i)->getQT()->getName() << " from area [" << this->blankets->getElement(i)->getArea()->getName() << "]";
		this->blankets->getElement(i)->getQT()->addCounterByOne();
		*historyFile << " Generated " << this->blankets->getElement(i)->getQT()->getCounter() << " time(s)\n";
	}
	*historyFile << "-------------------------------------------------";

	// Close file
	historyFile->close();

	// Calling destructor
	delete historyFile;
}