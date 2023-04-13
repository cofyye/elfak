#pragma once

#include "Area.h"

#include <string>

class QuestionTask {
private:
	std::string name;
	std::string type;
	int counter;
public:
	QuestionTask();
	QuestionTask(std::string name, std::string type);
	inline void addCounterByOne() {
		this->counter += 1;
	}
	inline std::string getName() {
		return this->name;
	}
	inline std::string getType() {
		return this->type;
	}
	inline int getCounter() {
		return this->counter;
	}
};