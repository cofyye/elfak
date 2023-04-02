#pragma once

#include "Letter.h"

class LetterT : public Letter {
public:
	LetterT(char c) : Letter(c) {}
	void power(Game& game);
};