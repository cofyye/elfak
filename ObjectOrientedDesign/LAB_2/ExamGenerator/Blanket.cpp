#include "Blanket.h"

Blanket::Blanket() {
	// Initialization
	this->area = nullptr;
	this->qt = nullptr;
}

Blanket::Blanket(Area* area, QuestionTask* qt) {
	// Initialization
	this->area = area;
	this->qt = qt;
}

Blanket::~Blanket() {}