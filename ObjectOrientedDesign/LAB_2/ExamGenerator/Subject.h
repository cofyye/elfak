#pragma once

#include <string>

#include "Area.h"
#include "Array.h"

class Subject {
private:
	std::string name;
	Array<Area>* areas;

	void loadAllAssets();

	friend class Subjects;
public:
	Subject();
	Subject(std::string name);
	inline std::string getName() {
		return this->name;
	}
};