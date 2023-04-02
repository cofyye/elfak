#pragma once

#include <string>
#include "Letter.h"

using namespace std;

class Letter; // this is fix for ***

class Game {
private:
	Letter*** mat; // board
	int size; // size of board
	int* playerDice; // player dice
	int playerCounterH = 0; // player counter for letter H
	int playerCounterX = 0; // player counter for letter X
	int playerK; // how much player is need to pass circle
	int playerCurrentK = 1; // current progress of passing circle
	int playerX = 0; // player position X
	int playerY = 0; // player position Y
	int playerLastX = 0; // player latest X position
	int playerLastY = 0; // player latest Y position
	int playerDirection = 0; // 0 is forward, 1 is left
	int playerLetH = 0; // How much times player get H letter
	int playerLetX = 0; // How much times player get X letter
	int playerLetP = 0; // If 0, k not count to player, if 1, k is count to player
	bool isPlaying = false; // Detect when player is playing 
	void progressRollDice(); // progress of rolling dice
	void printBoard(); // print board
	void printStar(); // print star
	void setPosition(); // set position
	void progressPlayer(); // progres of player movement
	void nextMove(); // player next move position
	void rollDice(); // roll dice and get random number from 1 to 6

	// friend classes
	friend class Letter1;
	friend class LetterH;
	friend class LetterO;
	friend class LetterP;
	friend class LetterS;
	friend class LetterT;
	friend class LetterX;
public:
	Game(string file);
	~Game();
	void start(); // start the game
};