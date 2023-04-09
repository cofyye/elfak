#pragma once

#include "Area.h"

#include <string>

class QuestionTask {
private:
	std::string name;
	std::string type;
public:
	QuestionTask();
	QuestionTask(std::string name, std::string type);
	inline std::string getName() {
		return this->name;
	}
	inline std::string getType() {
		return this->type;
	}
};