#include "LetterH.h"

void LetterH::power(Game& game) {
	game.setPlayerLetH(true);
	int valueH = game.getPlayerCounterH() + 1;
	game.setPlayerCounterH(valueH);
	int valueDice = game.getPlayerDice() * game.getPlayerCounterH();
	game.setPlayerDice(valueDice);
}