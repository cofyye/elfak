#include <iostream>
#include "CommandDraw.h"

CommandDraw::CommandDraw() : Command("Draw", "Draw Value") {};
CommandDraw::~CommandDraw() {};

void CommandDraw::execute() {
  cout << "Title : " << Command::title << endl;
  cout << "Value : " << Command::value << endl;
}
