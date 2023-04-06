#pragma once

#include "Letter.h"

class LetterP : public Letter {
public:
	LetterP(char c) : Letter(c) {}
	void power(Game& game);
};