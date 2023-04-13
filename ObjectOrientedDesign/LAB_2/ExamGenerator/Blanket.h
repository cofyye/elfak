#pragma once

#include "Area.h"

class Blanket {
private:
	Area* area;
	QuestionTask* qt;
public:
	Blanket();
	Blanket(Area* area, QuestionTask* qt);
	~Blanket();
	inline Area* getArea() {
		return this->area;
	}
	inline QuestionTask* getQT() {
		return this->qt;
	}
};