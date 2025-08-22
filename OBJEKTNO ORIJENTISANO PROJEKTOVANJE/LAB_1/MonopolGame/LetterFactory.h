#pragma once

#include "Letter.h"
#include "Letter1.h"
#include "LetterH.h"
#include "LetterO.h"
#include "LetterP.h"
#include "LetterS.h"
#include "LetterT.h"
#include "LetterX.h"
#include "NullObject.h"

class LetterFactory {
public:
	static Letter* loadLetter(char c) {
		switch (c) {
		case '1':
			return new Letter1(c);
			break;
		case 'H':
			return new LetterH(c);
			break;
		case 'O':
			return new LetterO(c);
			break;
		case 'P':
			return new LetterP(c);
			break;
		case 'S':
			return new LetterS(c);
			break;
		case 'T':
			return new LetterT(c);
			break;
		case 'X':
			return new LetterX(c);
			break;
		default:
			return new NullObject(c);
			break;
		}
	}
};