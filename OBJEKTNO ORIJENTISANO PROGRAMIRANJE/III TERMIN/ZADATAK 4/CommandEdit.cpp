#include "CommandEdit.h"
#include <iostream>
#include <string.h>

using namespace std;

CommandEdit::CommandEdit() : Command("Edit", "Edit Value") {};

CommandEdit::~CommandEdit() {};

void CommandEdit::execute() {
  cout << "Title : " << Command::title << endl;
  cout << "Value : " << Command::value << endl;
}

void CommandEdit::edit(string title, string value) {
    this->title = title;
    delete[] this->value;
    this->value = new char[30];
    strcpy(this->value, value.c_str());
}
