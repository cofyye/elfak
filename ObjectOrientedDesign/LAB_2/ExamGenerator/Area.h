#pragma once

#include <string>

#include "QuestionTask.h"
#include "Array.h"

class QuestionTask;

class Area {
private:
	std::string name;
	Array<QuestionTask>* qt;
public:
	Area();
	Area(std::string name);
	void addQuestionTask(QuestionTask* element);
	int getNumberOfElements(std::string type);
	QuestionTask* getElement(int index);

	inline std::string getName() {
		return this->name;
	}
};