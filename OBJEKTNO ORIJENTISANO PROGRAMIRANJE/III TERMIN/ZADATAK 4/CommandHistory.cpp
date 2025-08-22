#include <iostream>
#include "CommandHistory.h"
#include "Command.h"
#include "CommandDraw.h"
#include "CommandSave.h"
#include "CommandPrint.h"

using namespace std;

CommandHistory::CommandHistory() {
    this->currentEl = 0;
}

CommandHistory::~CommandHistory() {}

void CommandHistory::create(int x) {

    if(this->currentEl < 2019) {
        if(x == 1) {
            this->cmdArr[this->currentEl] = new CommandDraw();
            this->currentEl++;
        } else if(x == 2) {
            this->cmdArr[this->currentEl] = new CommandSave();
            this->currentEl++;
        } else if(x == 3) {
            this->cmdArr[this->currentEl] = new CommandPrint();;
            this->currentEl++;
        } else {
            this->cmdArr[this->currentEl] = new Command();;
            this->currentEl++;
        }
    } else {

        delete this->cmdArr[0];

        if(x == 1) {
            this->cmdArr[0] = new CommandDraw();
        } else if(x == 2) {
            this->cmdArr[0] = new CommandSave();
        } else if(x == 3) {
            this->cmdArr[0] = new CommandPrint();
        } else {
            this->cmdArr[0] = new Command();
        }
    }
}

void CommandHistory::execute() {
    this->cmdArr[this->currentEl-1]->execute();
}

void CommandHistory::undo() {
    if(this->currentEl > 0) {
        delete this->cmdArr[this->currentEl-1];
        this->currentEl--;
    }
}

void CommandHistory::executioner() {
    for(int i = this->currentEl-1; i >= 0; i--) {
        this->cmdArr[i]->execute();
        cout << endl;
        this->undo();
    }
}
