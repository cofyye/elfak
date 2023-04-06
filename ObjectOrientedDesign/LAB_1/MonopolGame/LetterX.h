#pragma once

#include "Letter.h"

class LetterX : public Letter {
public:
	LetterX(char c) : Letter(c) {}
	void power(Game& game);
};