#pragma once

#include "Game.h"

class Letter {
private:
	char c;

	friend class Game;
public:
	Letter(char c);
	virtual void power(Game& game) = 0;

	// Getters
	inline char getChar() {
		return this->c;
	}
};