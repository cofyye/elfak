#include <iostream>
#include <fstream>
#include "Complex.h"
#include "Buffer.h"

using namespace std;

int main()
{
    ofstream output("output.txt");

    int n;
    cout << "Unesite N : ";
    cin >> n;

    Buffer buff_1(n);

    for(int i = 0; i < n; i++) {
      Complex temp;
      temp.input();
      buff_1.push(temp);
    }

    cout << "Unesite N : ";
    cin >> n;

    Buffer buff_2(n);
    for(int i = 0; i < n; i++) {
      Complex temp;
      temp.input();
      buff_2.push(temp);
    }

    buff_1.append(buff_2);

    Buffer buff_3 = buff_1;
    buff_3 = buff_1;

    buff_3.popUnder(2);

    cout << buff_3 << endl;
    output << buff_3;

    output.close();
    return 0;
}
