#include "LetterX.h"

void LetterX::power(Game& game) {
	if (game.playerLetX) {
		game.playerCounterX += 1;
		if (game.playerCounterX == 3) game.playerCounterX = 1;
		if (game.playerCounterX == 1) {
			*game.playerDice = (*game.playerDice / 2) + 1;
		}
		else {
			*game.playerDice = ((*game.playerDice / 2) + 1) / 2 + 1;
		}
		game.playerLetX = 0;
	}
	else {
		game.playerLetX = 1;
	}
}