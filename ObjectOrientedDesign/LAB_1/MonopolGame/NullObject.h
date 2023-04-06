#pragma once

#include "Letter.h"

class NullObject : public Letter {
public:
	NullObject(char c) : Letter(c) {};
	void power(Game& game) {};
};