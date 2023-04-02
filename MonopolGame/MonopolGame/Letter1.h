#pragma once

#include "Letter.h"

class Letter1 : public Letter {
public:
	Letter1(char c) : Letter(c) {}
	void power(Game& game);
};