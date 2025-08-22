#pragma once
#include <string>

using namespace std;

class Command {
  protected:
    string title;
    char* value;
  public:
    Command(string title = "Command", string value = "Command Value");
    virtual ~Command();
    virtual void execute();
    //virtual void edit(string title, string value) {}; UZ DODATAK
};
