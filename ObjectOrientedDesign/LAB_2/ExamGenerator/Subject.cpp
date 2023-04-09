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

		if (this->name == subjectName) {
			Area* area = new Area(areaName);
			this->areas->push(area);
		}
	}
	// END : Load Areas

	// Closing all files
	areasFile->close();
	questionsFile->close();
	tasksFile->close();

	// Calling destructors
	delete areasFile;
	delete questionsFile;
	delete tasksFile;
}