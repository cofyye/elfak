#include <iostream>
#include "Buffer.h"

using namespace std;

int main()
{
    int n, temp = 0;
    cout << "Unesite N : ";
    cin >> n;

    Buffer buff_1(n);

    for(int i = 0; i < n; i++) {
      cout << "Unesite element #" << i+1 << " : ";
      cin >> temp;
      buff_1.push(temp);
    }

    Buffer* buff_2 = new Buffer(buff_1);
    int m;
    cout << "Unesite M : ";
    cin >> m;

    for(int i = 0; i < m; i++) {
      buff_1.pop();
    }

    buff_2->duplicate();

    buff_2->push(2);
    buff_2->push(1);
    buff_2->push(7);

    cout << "Prvi baffer : "; buff_1.print(); cout << endl;
    cout << "Drugi baffer : "; buff_2->print(); cout << endl;

    delete buff_2;

    return 0;
}
