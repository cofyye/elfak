#pragma once

#include <string>

#include "Subject.h"
#include "Blanket.h"

class GenerateBlanket {
private:
	Subject* subject;
	int questions;
	int tasks;
	Array<Blanket>* blankets;

	int random(int min, int max);
	bool inList(int* arr, int counter, int value);
	std::string generateQuestions();
	std::string generateTasks();
	void putIntoFile();
public:
	GenerateBlanket(Subject* subject, int questions, int tasks);
	~GenerateBlanket();
	void print();
	std::string generate();
};