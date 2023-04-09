#include <iostream>
#include <fstream>
#include <conio.h>

#include "Subject.h"
#include "Subjects.h"

using namespace std;

void printMainMenu() {
	// Main Menu
	cout << "\u001b[36m################## \u001b[32mEXAM GENERATOR \u001b[36m##################\u001b[0m\n\n";
	cout << "\u001b[36m[\u001b[32m1\u001b[36m] Generate a blanket \u001b[0m\n";
	cout << "\u001b[36m[\u001b[32m2\u001b[36m] Print all subjects \u001b[0m\n";
	cout << "\u001b[36m[\u001b[32m3\u001b[36m] Print all areas \u001b[0m\n";
	cout << "\u001b[36m[\u001b[32m4\u001b[36m] Print all questions or tasks or both \u001b[0m\n";
	cout << "\u001b[36m[\u001b[32m5\u001b[36m] Print specific subject \u001b[0m\n";
	cout << "\u001b[36m[\u001b[32m6\u001b[36m] Print history of generated blankets \u001b[0m\n\n";
	cout << "\u001b[36m[\u001b[32m9\u001b[36m] Quit from ExamGenerator \u001b[0m\n";
	cout << "\n\u001b[36m#####################################################\u001b[0m\n\n";
}

void mainMenu(Subjects* subjects) {
	// Initialization of a isActivated for detecting exit from program
	bool isActivated = true;

	// While loop until program end with number 9
	while (isActivated) {
		printMainMenu();
		int choice;
		char enterKey;
		cout << "\u001b[32m[OUTPUT] \u001b[36mYour choice : \u001b[0m";
		cin >> choice;

		// Handler for main menu
		switch(choice) {
		case 2:
			// Print subjects and back to main menu after enter
			system("cls");
			subjects->printSubjects();
			cout << "\n\u001b[32m[OUTPUT] \u001b[36mPress any key to back to main menu... \u001b[0m";
			// Keyboard hit, detect user pressing a key
			_kbhit();
			// After detecting, abort writes from user and get a last char from user
			enterKey = _getch();
			// Clear a console
			system("cls");
			break;
		case 3:
			// Print areas and back to main menu after enter
			system("cls");
			subjects->printAreas();
			cout << "\n\u001b[32m[OUTPUT] \u001b[36mPress any key to back to main menu... \u001b[0m";
			// Keyboard hit, detect user pressing a key
			_kbhit();
			// After detecting, abort writes from user and get a last char from user
			enterKey = _getch();
			// Clear a console
			system("cls");
			break;
		case 4:
			// Clear a console
			system("cls");

			// While is true until break
			while (true) {
				// Initialization of a subChoice used for sub menu
				int subChoice;

				// Print sub menu
				cout << "\u001b[36m[\u001b[32m1\u001b[36m] Print all questions and tasks \u001b[0m\n";
				cout << "\u001b[36m[\u001b[32m2\u001b[36m] Print all questions \u001b[0m\n";
				cout << "\u001b[36m[\u001b[32m3\u001b[36m] Print all tasks \u001b[0m\n";

				cout << "\n\u001b[36m[\u001b[32m9\u001b[36m] Back to main menu \u001b[0m\n";

				cout << "\n\u001b[32m[OUTPUT] \u001b[36mYour choice : \u001b[0m";

				// Get a subChoice
				cin >> subChoice;

				// Clear a console
				system("cls");

				// Detect user subChoice
				if (subChoice == 1) {
					subjects->printQTs("both");
				}
				else if (subChoice == 2) {
					subjects->printQTs("questions");
				}
				else if (subChoice == 3) {
					subjects->printQTs("tasks");
				}
				else if (subChoice == 9) {
					// If user choice is 9, then break this loop and continue the program
					break;
				}
				else {
					// Continue the loop without executing codes below 
					continue;
				}

				cout << "\n\u001b[32m[OUTPUT] \u001b[36mPress any key to back to menu... \u001b[0m";
				// Keyboard hit, detect user pressing a key
				_kbhit();
				// After detecting, abort writes from user and get a last char from user
				enterKey = _getch();
				// Clear a console
				system("cls");
			}

			// Clear a console
			system("cls");
			break;
		case 9:
			// If user choice is 9, then set isActivated to false to out from loop
			isActivated = false;
			// Clear a console
			system("cls");
			break;
		default:
			// Clear a console
			system("cls");
			break;
		}
	}

	// This will be execute when user exit from loop with choice number 9
	cout << "\u001b[32m[OUTPUT] \u001b[36mGoodbye, thanks for using \u001b[32mExamGenerator \u001b[0m\n";
}

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

		// Load all assets for subjects
		subjects->loadAllAssets();

		// Execute a main menu
		mainMenu(subjects);

		// Calling destructors
		delete subjectsFile;
		delete subjects;
	}
	catch (string error) {
		// If catch some error, then print this
		cout << "\u001b[31m[ERROR] " << error << '\n';
	}

	return 0;
}