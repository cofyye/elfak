#include "LetterX.h"

void LetterX::power(Game& game) {
	if (game.getPlayerLetX()) {
		int valueX = game.getPlayerCounterX() + 1;
		game.setPlayerCounterX(valueX);
		if (game.getPlayerCounterX() == 3) game.setPlayerCounterX(1);
		if (game.getPlayerCounterX() == 1) {
			int valueDice = (game.getPlayerDice() / 2) + 1;
			game.setPlayerDice(valueDice);
		}
		else {
			int valueDice = ((game.getPlayerDice() / 2) + 1) / 2 + 1;
			game.setPlayerDice(valueDice);
		}
		game.setPlayerLetX(false);
	}
	else {
		game.setPlayerLetX(true);
	}
}