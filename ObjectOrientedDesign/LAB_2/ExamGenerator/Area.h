#pragma once

#include <string>

class Area {
private:
	std::string name;
public:
	Area();
	Area(std::string name);
	inline std::string getName() {
		return this->name;
	}
};