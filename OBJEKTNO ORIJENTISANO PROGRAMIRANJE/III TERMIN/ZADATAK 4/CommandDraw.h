#pragma once
#include "Command.h"

class CommandDraw : public Command {
  public:
    CommandDraw();
    ~CommandDraw();
    void execute();
};
