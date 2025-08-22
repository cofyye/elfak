#include <iostream>
#include "CommandSave.h"

CommandSave::CommandSave() : Command("Save", "Save Value") {};
CommandSave::~CommandSave() {};

void CommandSave::execute() {
  cout << "Title : " << Command::title << endl;
  cout << "Value : " << Command::value << endl;
}
