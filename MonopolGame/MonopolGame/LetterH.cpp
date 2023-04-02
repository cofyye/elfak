#include "LetterH.h"

void LetterH::power(Game& game) {
	game.playerLetH = 1;
	game.playerCounterH += 1;
	*game.playerDice *= game.playerCounterH;
}