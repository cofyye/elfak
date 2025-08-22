#pragma once

#include "Letter.h"

class LetterS : public Letter {
public:
	LetterS(char c) : Letter(c) {}
	void power(Game& game);
};