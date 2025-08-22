#include <fstream>
#include <iostream>
#include <boost/algorithm/string.hpp>
#include "Subject.h"

using namespace std;

Subject::Subject() {
	// Initialization
	this->name = "";
	this->areas = new Array<Area>();
}

Subject::Subject(string name) {
	// Initialization
	this->name = name;
	this->areas = new Array<Area>();
}

Area* Subject::getArea(int index) {
	return this->areas->getElement(index);
}

 void Subject::loadAllAssets() {
	// Initialization of a files
	ifstream* areasFile = new ifstream("areas.txt");
	ifstream* questionsFile = new ifstream("questions.txt");
	ifstream* tasksFile = new ifstream("tasks.txt");

	// START : Checker for all files if successfully opened
	if (!areasFile->is_open()) {
		// Define error
		string error = "\u001b[36mFile \u001b[33mareas.txt\u001b[36m is not found !\u001b[0m";
		// Throw error
		throw error;
	}

	if (!questionsFile->is_open()) {
		// Define error
		string error = "\u001b[36mFile \u001b[33mquestions.txt\u001b[36m is not found !\u001b[0m";
		// Throw error
		throw error;
	}

	if (!tasksFile->is_open()) {
		// Define error
		string error = "\u001b[36mFile \u001b[33mtasks.txt\u001b[36m is not found !\u001b[0m";
		// Throw error
		throw error;
	}
	// END : Checker for all files if successfully opened

	// START : Load Areas
	string line;
	while (getline(*areasFile, line)) {
		// Separate area and subject with delimiter ~
		string areaName = line.substr(0, line.find("~"));
		string subjectName = line.substr(line.find("~") + 1, line.length());

		// Trim right and left whitespaces
		boost::algorithm::trim(areaName);
		boost::algorithm::trim(subjectName);

		// If current name of subject equal subject name from file, then add it into list
		if (this->name == subjectName) {
			Area* area = new Area(areaName);
			this->areas->push(area);
		}
	}
	// END : Load Areas

	// START : Load Questions
	while (getline(*questionsFile, line)) {
		// Separate question, area and subject with delimiter ~
		string questionName = line.substr(0, line.find("~"));
		string rest = line.substr(line.find("~") + 1, line.length());
		string areaName = rest.substr(0, rest.find("~"));
		string subjectName = rest.substr(rest.find("~") + 1, rest.length());

		// Trim right and left whitespaces
		boost::algorithm::trim(questionName);
		boost::algorithm::trim(areaName);
		boost::algorithm::trim(subjectName);

		// If current name of subject equal subject name from file, then go into block
		if (this->name == subjectName) {
			// Loop through all areas and then add question to specific area
			for (int i = 0; i < this->areas->getNumberOfElements(); i++) {
				if (this->areas->getElement(i)->getName() == areaName) {
					QuestionTask* questionTask = new QuestionTask(questionName, "Question");
					this->areas->getElement(i)->addQuestionTask(questionTask);
					break;
				}
			}
		}

	}
	// END : Load Questions

	// START : Load Tasks
	while (getline(*tasksFile, line)) {
		// Separate task, area and subject with delimiter ~
		string taskName = line.substr(0, line.find("~"));
		string rest = line.substr(line.find("~") + 1, line.length());
		string areaName = rest.substr(0, rest.find("~"));
		string subjectName = rest.substr(rest.find("~") + 1, rest.length());

		// Trim right and left whitespaces
		boost::algorithm::trim(taskName);
		boost::algorithm::trim(areaName);
		boost::algorithm::trim(subjectName);

		// If current name of subject equal subject name from file, then go into block
		if (this->name == subjectName) {
			// Loop through all areas and then add task to specific area
			for (int i = 0; i < this->areas->getNumberOfElements(); i++) {
				if (this->areas->getElement(i)->getName() == areaName) {
					QuestionTask* questionTask = new QuestionTask(taskName, "Task");
					this->areas->getElement(i)->addQuestionTask(questionTask);
					break;
				}
			}
		}

	}
	// END : Load Tasks

	// Closing all files
	areasFile->close();
	questionsFile->close();
	tasksFile->close();

	// Calling destructors
	delete areasFile;
	delete questionsFile;
	delete tasksFile;
}