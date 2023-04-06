#include <iostream>
#include <string>
#include "Game.h"

using namespace std;

int main() {

	try {
		Game* game = new Game("file.txt");

		game->start();

		delete game;
	}
	catch (string error) {
		system("cls");
		cout << "\u001b[31m[ERROR] " << error << endl;
	}
	catch (...) {
		system("cls");
		cout << "\u001b[31m[ERROR] \u001b[36mUnknown Error !\u001b[0m" << endl;
	}

	return 0;
}