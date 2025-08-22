#pragma once
#include <string>
#include "Command.h"

using namespace std;

class CommandEdit : public Command {
public:
    CommandEdit();
    ~CommandEdit();
    void execute();
    void edit(string title, string value);
};
