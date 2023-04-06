#include "LetterO.h"

void LetterO::power(Game& game) {
	int value = game.getPlayerCurrentK() + 1;
	game.setPlayerCurrentK(value);
}