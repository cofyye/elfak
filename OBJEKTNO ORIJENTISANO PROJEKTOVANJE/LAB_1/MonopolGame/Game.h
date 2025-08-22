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
	bool playerDirection = false; // false is forward, true is left
	bool playerLetH = false; // How much times player get H letter
	bool playerLetX = false; // How much times player get X letter
	bool playerLetP = false; // If 0, k not count to player, if 1, k is count to player
	bool isPlaying = false; // Detect when player is playing 
	void progressRollDice(); // progress of rolling dice
	void printBoard(); // print board
	void printStar(); // print star
	void setPosition(); // set position
	void progressPlayer(); // progres of player movement
	void nextMove(); // player next move position
	void rollDice(); // roll dice and get random number from 1 to 6
public:
	Game(string file);
	~Game();
	void start(); // start the game

	// Setters
	inline void setPlayerLetH(bool status) {
		this->playerLetH = status;
	}
	inline void setPlayerLetX(bool status) {
		this->playerLetX = status;
	}
	inline void setPlayerLetP(bool status) {
		this->playerLetP = status;
	}
	inline void setPlayerDice(int value) {
		*this->playerDice = value;
	}
	inline void setPlayerCounterH(int value) {
		this->playerCounterH = value;
	}
	inline void setPlayerCounterX(int value) {
		this->playerCounterX = value;
	}
	inline void setPlayerCurrentK(int value) {
		this->playerCurrentK = value;
	}
	inline void setPlayerX(int value) {
		this->playerX = value;
	}
	inline void setPlayerY(int value) {
		this->playerY = value;
	}
	inline void setPlayerDirection(bool status) {
		this->playerDirection = status;
	}

	// Getters
	inline int getPlayerDice() {
		return *this->playerDice;
	}
	inline bool getPlayerLetH() {
		return this->playerLetH;
	}
	inline bool getPlayerLetX() {
		return this->playerLetX;
	}
	inline bool getPlayerLetP() {
		return this->playerLetP;
	}
	inline bool getPlayerDirection() {
		return this->playerDirection;
	}
	inline int getPlayerCounterH() {
		return this->playerCounterH;
	}
	inline int getPlayerCounterX() {
		return this->playerCounterX;
	}
	inline int getPlayerCurrentK() {
		return this->playerCurrentK;
	}
	inline int getPlayerX() {
		return this->playerX;
	}
	inline int getPlayerY() {
		return this->playerY;
	}
};