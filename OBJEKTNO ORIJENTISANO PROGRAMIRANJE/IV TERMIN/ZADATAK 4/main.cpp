#include <iostream>
#include <stdlib.h>
#include <string>
#include "Azdaja.h"
#include "Put.h"
#include "PutGora.h"
#include "PutMora.h"
#include "PutMocvara.h"

using namespace std;

int main()
{
    Azdaja *azdaja = new Azdaja();

    azdaja->uzmiMore();
    azdaja->uzmiGoru();
    azdaja->uzmiMocvaru();
    azdaja->uzmiPlaninu();

    azdaja->sortirajTezinu();
    azdaja->sortirajLepotu();

    delete azdaja;

    return 0;
}
