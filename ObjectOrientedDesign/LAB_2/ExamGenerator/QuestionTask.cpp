#include "QuestionTask.h"

using namespace std;

QuestionTask::QuestionTask() {
	this->name = "";
	this->type = "";
}

QuestionTask::QuestionTask(string name, string type) {
	this->name = name;
	this->type = type == "Question" || type == "Task" ? type : "";
}