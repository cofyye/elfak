#pragma once
#include "Command.h"

class CommandSave : public Command {
  public:
    CommandSave();
    ~CommandSave();
    void execute();
};
