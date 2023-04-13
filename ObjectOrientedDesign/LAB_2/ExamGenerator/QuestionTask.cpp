#include "QuestionTask.h"

using namespace std;

QuestionTask::QuestionTask() {
	// Initialization
	this->name = "";
	this->type = "";
	this->counter = 0;
}

QuestionTask::QuestionTask(string name, string type) {
	// Initialization
	this->name = name;
	this->type = type == "Question" || type == "Task" ? type : "";
	this->counter = 0;
}