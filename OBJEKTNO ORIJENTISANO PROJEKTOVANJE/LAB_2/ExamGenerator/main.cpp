#include <iostream>
#include <fstream>
#include <conio.h>
#include <windows.h>

#include "Subject.h"
#include "Subjects.h"
#include "GenerateBlanket.h"

#define SLEEP_TIME_1 500
#define SHOW_ELEMENTS 10

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
	cout << "\u001b[36m[\u001b[32m0\u001b[36m] Quit from ExamGenerator \u001b[0m\n";
	cout << "\n\u001b[36m####################################################\u001b[0m\n\n";
}

void runCase6() {
	// Initialization of a history file
	ifstream* historyFile = new ifstream("history.txt");

	// Check if file successfully opened
	if (!historyFile->is_open()) {
		// Define error
		string error = "\u001b[36mFile \u001b[33mhistory.txt\u001b[36m is not found !\u001b[0m";
		// Throw error
		throw error;
	}

	string line;
	while (getline(*historyFile, line)) {
		cout << "\u001b[36m" << line << "\n";
	}

	// Close file
	historyFile->close();
	// Calling destructor
	delete historyFile;
}

void printQTSubMenuAll(Subjects* subjects) {
	// Initialization of a enterKey
	char enterKey;

	// While is true until break
	while (true) {
		// Initialization of a subChoice used for sub menu
		int subChoice;

		// Print sub menu
		cout << "\u001b[36m[\u001b[32m1\u001b[36m] Print all questions and tasks \u001b[0m\n";
		cout << "\u001b[36m[\u001b[32m2\u001b[36m] Print all questions \u001b[0m\n";
		cout << "\u001b[36m[\u001b[32m3\u001b[36m] Print all tasks \u001b[0m\n";

		cout << "\n\u001b[36m[\u001b[32m0\u001b[36m] Back to main menu \u001b[0m\n";

		cout << "\n\u001b[32m[OUTPUT] \u001b[36mYour choice : \u001b[0m";

		// Get a subChoice
		cin >> subChoice;

		// Clear a console
		std::system("cls");

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
		else if (subChoice == 0) {
			// If user choice is 0, then break this loop and continue the program
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
		std::system("cls");
	}
}

void printQTSubMenuSpecific(Subjects* subjects, Area* area) {
	// Initialization of a enterKey
	char enterKey;

	// While is true until break
	while (true) {
		// Clear a console
		std::system("cls");

		// Initialization of a subChoice used for sub menu
		int subChoice;

		// Print sub menu
		cout << "\u001b[36m[\u001b[32m1\u001b[36m] Print all questions and tasks for [\u001b[33m" << area->getName() <<"\u001b[36m] \u001b[0m\n";
		cout << "\u001b[36m[\u001b[32m2\u001b[36m] Print all questions for [\u001b[33m" << area->getName() << "\u001b[36m] \u001b[0m\n";
		cout << "\u001b[36m[\u001b[32m3\u001b[36m] Print all tasks for [\u001b[33m" << area->getName() << "\u001b[36m] \u001b[0m\n";

		cout << "\n\u001b[36m[\u001b[32m0\u001b[36m] Back to main menu \u001b[0m\n";

		cout << "\n\u001b[32m[OUTPUT] \u001b[36mYour choice : \u001b[0m";

		// Get a subChoice
		cin >> subChoice;

		// Clear a console
		std::system("cls");

		// Detect user subChoice
		if (subChoice == 1) {
			subjects->printQTsSpecific(area, "both");
		}
		else if (subChoice == 2) {
			subjects->printQTsSpecific(area, "questions");
		}
		else if (subChoice == 3) {
			subjects->printQTsSpecific(area, "tasks");
		}
		else if (subChoice == 0) {
			// If user choice is 0, then break this loop and continue the program
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
		std::system("cls");
	}
}

void mainMenu(Subjects* subjects) {
	// Initialization of a isActivated for detecting exit from program
	bool isActivated = true;

	// While loop until program end with number 0
	while (isActivated) {
		printMainMenu();
		int choice;
		char enterKey;
		cout << "\u001b[32m[OUTPUT] \u001b[36mYour choice : \u001b[0m";
		cin >> choice;

		// Handler for main menu
		switch(choice) {
		case 1:
			// While is true until break
			while (true) {
				// Clear a console
				std::system("cls");

				// Initialization of a subChoice used for sub menu
				int subChoice;

				// Print all subjects
				cout << "\u001b[36mChoose subject to generate a blanket\u001b[0m\n\n";
				for (int i = 0; i < subjects->getNumberOfElements(); i++) {
					cout << "\u001b[36m[\u001b[32m" << i + 1 << "\u001b[36m] " << subjects->getElement(i)->getName() << "\u001b[0m\n";

					// Sleep process on every SHOW_ELEMENTS elements
					if ((i + 1) % SHOW_ELEMENTS == 0) {
						Sleep(SLEEP_TIME_1);
					}
				}

				// Print back to main menu
				cout << "\n\u001b[36m[\u001b[32m0\u001b[36m] Back to main menu \u001b[0m\n";

				// Print your choice
				cout << "\n\u001b[32m[OUTPUT] \u001b[36mYour choice : \u001b[0m";

				// Get a subChoice
				cin >> subChoice;

				if (subChoice > 0 && subChoice <= subjects->getNumberOfElements()) {
					// Clear a console
					std::system("cls");

					// Print selected subject
					cout << "\u001b[32m[OUTPUT] \u001b[36mSelected subject [\u001b[33m" << subjects->getElement(subChoice-1)->getName() << "\u001b[36m]\u001b[0m\n";

					// Initialization
					int questions = 0;
					int tasks = 0;

					// Print how much user want questions for blanket
					cout << "\u001b[32m[OUTPUT] \u001b[36mHow much you want questions in blanket : \u001b[0m";
					cin >> questions;

					// Print how much user want tasks for blanket
					cout << "\u001b[32m[OUTPUT] \u001b[36mHow much you want tasks in blanket : \u001b[0m";
					cin >> tasks;

					// Generate a blanket
					GenerateBlanket* generateBlanket = new GenerateBlanket(subjects->getElement(subChoice - 1), questions, tasks);

					string response = generateBlanket->generate();

					// Clear a console
					std::system("cls");

					if (response == "") {
						generateBlanket->print();
					}
					else {
						cout << response << "\n";
					}

					cout << "\n\u001b[32m[OUTPUT] \u001b[36mPress any key to back to main menu... \u001b[0m";
					// Keyboard hit, detect user pressing a key
					_kbhit();
					// After detecting, abort writes from user and get a last char from user
					enterKey = _getch();
					// Clear a console
					std::system("cls");
					delete generateBlanket;
					// Continue the loop without executing codes below 
					continue;
				} else if (subChoice == 0) {
					// If user choice is 0, then break this loop and continue the program
					break;
				}
				else {
					// Continue the loop without executing codes below 
					continue;
				}
			}

			// Clear a console
			std::system("cls");
			break;

		case 2:
			// Print subjects and back to main menu after enter
			std::system("cls");
			subjects->printSubjects();
			cout << "\n\u001b[32m[OUTPUT] \u001b[36mPress any key to back to main menu... \u001b[0m";
			// Keyboard hit, detect user pressing a key
			_kbhit();
			// After detecting, abort writes from user and get a last char from user
			enterKey = _getch();
			// Clear a console
			std::system("cls");
			break;
		case 3:
			// Print areas and back to main menu after enter
			std::system("cls");
			subjects->printAreas();
			cout << "\n\u001b[32m[OUTPUT] \u001b[36mPress any key to back to main menu... \u001b[0m";
			// Keyboard hit, detect user pressing a key
			_kbhit();
			// After detecting, abort writes from user and get a last char from user
			enterKey = _getch();
			// Clear a console
			std::system("cls");
			break;
		case 4:
			// Clear a console
			std::system("cls");

			// Print a subMenu
			printQTSubMenuAll(subjects);

			// Clear a console
			std::system("cls");
			break;
		case 5:
			// While is true until break
			while (true) {
				// Clear a console
				std::system("cls");

				// Initialization of a subChoice used for sub menu
				int subChoice;

				// Print all subjects
				for (int i = 0; i < subjects->getNumberOfElements(); i++) {
					cout << "\u001b[36m[\u001b[32m" << i + 1 << "\u001b[36m] " << subjects->getElement(i)->getName() << "\u001b[0m\n";

					// Sleep process on every SHOW_ELEMENTS elements
					if ((i + 1) % SHOW_ELEMENTS == 0) {
						Sleep(SLEEP_TIME_1);
					}
				}

				// Print back to main menu
				cout << "\n\u001b[36m[\u001b[32m0\u001b[36m] Back to main menu \u001b[0m\n";

				// Print your choice
				cout << "\n\u001b[32m[OUTPUT] \u001b[36mYour choice : \u001b[0m";

				// Get a subChoice
				cin >> subChoice;

				if (subChoice > 0 && subChoice <= subjects->getNumberOfElements()) {
					// Clear a console
					std::system("cls");

					// While is true until break
					while (true) {
						// Clear a console
						std::system("cls");

						// Print all areas for this subject
						for (int i = 0; i < subjects->getElement(subChoice-1)->getNumberOfElements(); i++) {
							cout << "\u001b[36m[\u001b[32m" << i + 1 << "\u001b[36m] " << subjects->getElement(subChoice-1)->getArea(i)->getName() << "\u001b[0m\n";
							// Sleep process on every SHOW_ELEMENTS elements
							if ((i + 1) % SHOW_ELEMENTS == 0) {
								Sleep(SLEEP_TIME_1);
							}
						}

						// Initialization of a sub2Choice used for sub menu
						int sub2Choice;

						// Print back to main menu
						cout << "\n\u001b[36m[\u001b[32m0\u001b[36m] Back to main menu \u001b[0m\n";

						// Print your choice
						cout << "\n\u001b[32m[OUTPUT] \u001b[36mYour choice : \u001b[0m";

						// Get a subChoice
						cin >> sub2Choice;

						if (sub2Choice > 0 && sub2Choice <= subjects->getElement(subChoice - 1)->getNumberOfElements()) {
							printQTSubMenuSpecific(subjects, subjects->getElement(subChoice-1)->getArea(sub2Choice-1));
						}
						else if (sub2Choice == 0) {
							// If user choice is 0, then break this loop and continue the program
							break;
						}
						else {
							// Continue the loop without executing codes below 
							continue;
						}
					}
				}
				else if(subChoice == 0) {
					// If user choice is 0, then break this loop and continue the program
					break;
				}
				else {
					// Continue the loop without executing codes below 
					continue;
				}
			}
			// Clear a console
			std::system("cls");
			break;
		case 6:
			// Clear a console
			std::system("cls");

			runCase6();

			cout << "\n\u001b[32m[OUTPUT] \u001b[36mPress any key to back to main menu... \u001b[0m";
			// Keyboard hit, detect user pressing a key
			_kbhit();
			// After detecting, abort writes from user and get a last char from user
			enterKey = _getch();
			// Clear a console
			std::system("cls");
			break;
		case 0:
			// If user choice is 9, then set isActivated to false to out from loop
			isActivated = false;
			// Clear a console
			std::system("cls");
			break;
		default:
			// Clear a console
			std::system("cls");
			break;
		}
	}

	// This will be execute when user exit from loop with choice number 0
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