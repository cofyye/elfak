#pragma once

#include "Letter.h"

class LetterH : public Letter {
public:
	LetterH(char c) : Letter(c) {}
	void power(Game& game);
};