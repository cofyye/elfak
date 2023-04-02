#pragma once

#include "Letter.h"

class LetterO : public Letter {
public:
	LetterO(char c) : Letter(c) {}
	void power(Game& game);
};