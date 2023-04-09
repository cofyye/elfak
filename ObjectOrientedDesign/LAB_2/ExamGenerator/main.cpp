#include <iostream>
#include <fstream>

#include "Subject.h"
#include "Subjects.h"

using namespace std;

int main() {
	try {
		// Initialization of a Subjects class
		Subjects* subjects = new Subjects();

		// Initialization of a file
		ifstream* subjectsFile = new ifstream("subjects.txt");

		// Checker for file if successfully opened
		if (!subjectsFile->is_open()) {
			// Define error
			string error = "\u001b[36mFile \u001b[33msubjects.txt\u001b[36m is not found !\u001b[0m";
			// Throw error
			throw error;
		}

		string line = ""; // Initialization of a empty string subject

		// Load all subjects
		while (getline(*subjectsFile, line)) {
			// Initialization of a new subject
			Subject* subject = new Subject(line);
			// Push subject into array
			subjects->push(subject);
		}

		subjects->loadAllAssets();

		subjects->printSubjects();
		subjects->printAreas();


		// Calling destructors
		delete subjectsFile;
		delete subjects;
	}
	catch (string error) {
		cout << "\u001b[31m[ERROR] " << error << '\n';
	}

	return 0;
}