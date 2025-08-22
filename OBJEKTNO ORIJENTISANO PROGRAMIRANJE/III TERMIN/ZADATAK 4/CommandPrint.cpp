#include <iostream>
#include "CommandPrint.h"

CommandPrint::CommandPrint() : Command("Print", "Print Value") {};
CommandPrint::~CommandPrint() {};

void CommandPrint::execute() {
  cout << "Title : " << Command::title << endl;
  cout << "Value : " << Command::value << endl;
}
