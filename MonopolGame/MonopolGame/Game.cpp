#pragma once

#define sleepTime1 1000
#define sleepTime2 700
#define sleepTime3 150

#include <string>
#include <fstream>
#include <iostream>
#include <thread>
#include <windows.h>
#include "Game.h"
#include "LetterFactory.h"

using namespace std;

Game::Game(string file) {
	cout << "\u001b[32m[INFO] \u001b[36mLoading game resources";
	Sleep(sleepTime1);
	cout << "\r\u001b[32m[INFO] \u001b[36mLoading game resources.";
	Sleep(sleepTime1);
	cout << "\r\u001b[32m[INFO] \u001b[36mLoading game resources..";
	Sleep(sleepTime1);
	cout << "\r\u001b[32m[INFO] \u001b[36mLoading game resources...";
	Sleep(sleepTime1);
	cout << "\r\u001b[32m[INFO] \u001b[36mLoading game resources..\x20\b";
	Sleep(sleepTime2);
	cout << "\r\u001b[32m[INFO] \u001b[36mLoading game resources..\u001b[0m" << endl;

	ifstream* input = new ifstream(file);
	char c;
	this->playerK = 1;
	this->playerDice = new int(0);
	this->playerCounterH = 0;
	this->playerCounterX = 0;

	if (!input->is_open()) {
		string error = "\u001b[36mFile is not found !\u001b[0m";
		throw error;
	}

	*input >> this->size;

	this->mat = new Letter**[this->size];
	for (int i = 0; i < this->size; i++) {
		this->mat[i] = new Letter*[this->size];
	}

	for (int i = 0; i < this->size; i++) {
		for (int j = 0; j < this->size; j++) {
			*input >> c;
			this->mat[i][j] = LetterFactory::loadLetter(c);
		}
	}

	input->close();

	cout << "\u001b[32m[INFO] \u001b[36mGame resources is loaded !\u001b[0m" << endl;
	Sleep(sleepTime2);

	delete input;
}

Game::~Game() {
	if (this->mat != 0) {
		for (int i = 0; i < this->size; i++) {
			for (int j = 0; j < this->size; j++) {
				delete[] this->mat[i][j];
			}
			delete[] this->mat[i];
		}
		delete[] this->mat;
	}

	delete this->playerDice;
}

void Game::nextMove() {
	if (this->playerX == 0 && this->playerY == 0) {
			this->playerX += 1;
	} else if (this->playerX == 11 && (this->playerY == 0 || this->playerY < 7) && this->playerDirection) {
		this->playerY += 1;
	} else if ((this->playerX >= 11 && this->playerX < this->size-1) && this->playerY == 7 && this->playerDirection) {
		this->playerX += 1;
	} else if (this->playerX == this->size-1 && (this->playerY == 7 || this->playerY < this->size-1) && this->playerDirection) {
		this->playerY += 1;
	} else if (this->playerX == 7 && (this->playerY == this->size-1 || this->playerY > 12) && this->playerDirection) {
		this->playerY -= 1;
	} else if (this->playerX <= 7 && this->playerX > 0 && this->playerY == 12 && this->playerDirection) {
		this->playerX -= 1;
	} else if (this->playerX == 0 && (this->playerY == 12 || this->playerY > 0) && this->playerDirection) {
		this->playerY -= 1;
	} else if (this->playerX < this->size - 1 && this->playerY == 0) {
		this->playerX += 1;
	} else if (this->playerX == this->size - 1 && this->playerY < this->size - 1) {
		this->playerY += 1;
	} else if (this->playerX > 0 && this->playerY == this->size - 1) {
		this->playerX -= 1;
	} else {
		this->playerY -= 1;
	}
}

void Game::progressRollDice() {
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), { 0, 2 });
	cout << "\r\u001b[32m[OUTPUT] \u001b[36mRolling dice.\x20\x20\x20\b\b\b";
	Sleep(sleepTime2);
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), { 0, 2 });
	cout << "\r\u001b[32m[OUTPUT] \u001b[36mRolling dice..";
	Sleep(sleepTime2);
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), { 0, 2 });
	cout << "\r\u001b[32m[OUTPUT] \u001b[36mRolling dice...";
	Sleep(sleepTime2);
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), { 0, 2 });
	this->rollDice();
	cout << "\r\u001b[32m[OUTPUT] \u001b[36mDice number is " << *this->playerDice << "\u001b[0m" << endl << endl;
	Sleep(sleepTime1);
}

void Game::printBoard() {
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), { 0, 4 });
	for (int i = 0; i < this->size; i++) {
		for (int j = 0; j < this->size; j++) {
			if (this->mat[i][j]->getChar() == '0') {
				cout << ' ' << " ";
			}
			else {
				cout << this->mat[i][j]->getChar() << " ";
			}
		}
		cout << endl;
	}
}

void Game::printStar() {
	cout << "*\b";
}

void Game::setPosition() {
	if (this->playerX >= 0 && this->playerY == 0 && this->playerX <= this->size-1) {
		SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), { (short)(this->playerY), (short)(this->playerX + 4) });
	}
	else {
		SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), { (short)(this->playerY * 2), (short)(this->playerX + 4) });
	}
}

void Game::rollDice() {
	*this->playerDice = 1 + rand() % ((6 + 1) - 1);
}

void Game::progressPlayer() {
	this->playerLastX = this->playerX;
	this->playerLastY = this->playerY;
	this->setPosition();
	this->isPlaying = true;
	this->playerCounterH = 0;
	while (this->isPlaying) {
		this->printStar();
		Sleep(sleepTime3);
		cout << this->mat[this->playerX][this->playerY]->getChar() << "\b";
		Sleep(sleepTime3);
		this->printStar();
		Sleep(sleepTime3);
		cout << this->mat[this->playerX][this->playerY]->getChar();
		Sleep(sleepTime3);

		if (this->playerLetX) {
			this->mat[this->playerX][this->playerY]->power(*this);
		}

		for (int i = 0; i < *this->playerDice; i++) {
			this->nextMove();
			this->setPosition();
			this->printStar();
			Sleep(sleepTime3);
			cout << this->mat[this->playerX][this->playerY]->getChar() << "\b";
			Sleep(sleepTime3);
			this->printStar();
			Sleep(sleepTime3);
			cout << this->mat[this->playerX][this->playerY]->getChar();
			Sleep(sleepTime3);

			if (i + 1 == *this->playerDice) {
				cout << "\b";
			}
		}
		this->mat[this->playerX][this->playerY]->power(*this);
		if (!this->playerLetP) this->printStar();
		if (!this->playerLetH) {
			this->isPlaying = false;
		}
		else {
			this->playerLetH = false;
		}
		if (this->mat[this->playerX][this->playerY]->getChar() != 'X') {
			this->playerCounterX = 0;
			this->playerLetX = false;
		}
	}
}

void Game::start() {
	cout << "\u001b[32m[INFO] \u001b[36mGame is ready for playing !\u001b[0m" << endl;
	cout << "\u001b[32m[INPUT] \u001b[36mHow much times player must pass whole circle? \u001b[0m";
	cin >> this->playerK;
	cout << "\u001b[32m[INFO] \u001b[36mPlayer must pass \u001b[0m" << this->playerK << " \u001b[36mtimes whole circle !\u001b[0m" << endl;
	Sleep(sleepTime2);
	system("cls");
	Sleep(sleepTime2);
	cout << "\u001b[32m[INFO] \u001b[36mGame is started. Enjoy !\u001b[0m" << endl << endl;
	Sleep(sleepTime3);

	this->printBoard();

	while(this->playerCurrentK <= this->playerK) {
		this->progressRollDice();
		this->progressPlayer();
		if (this->playerLastX == 0 && this->playerLastY > 0 && this->playerX > 0 && this->playerY == 0 && !this->playerLetP) {
			this->playerCurrentK += 1;
		}

		if (this->playerLetP) this->playerLetP = false;
	}
	Sleep(sleepTime2);
	system("cls");
	cout << "\u001b[32m[INFO] \u001b[36mYou pass all circles. Thanks for playing this game !\u001b[0m" << endl;
}
