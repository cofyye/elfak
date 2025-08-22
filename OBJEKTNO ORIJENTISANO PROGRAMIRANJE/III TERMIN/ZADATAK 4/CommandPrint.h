#pragma once
#include "Command.h"

class CommandPrint : public Command {
  public:
    CommandPrint();
    ~CommandPrint();
    void execute();
};
