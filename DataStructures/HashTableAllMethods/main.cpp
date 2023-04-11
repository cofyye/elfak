#include <iostream>
#include <bitset>
#include <cmath>

using namespace std;

unsigned int moduoMethod(unsigned int k, int len) {
    return k % len;
}

unsigned int squareMethod(unsigned int k, int len) {
    // Better than moduoMethod for scatter table but not enough good
    unsigned int kSquare = k * k;
    bitset<64U> a(kSquare);
    unsigned const int bits = 32;
    return kSquare >> (bits - 6);
}

unsigned int fibonacciMethod(unsigned int k, int len) {
    // Better than moduoMethod and squareMethod for scatter table
    unsigned const int a = 2654435769;
    unsigned const int ak = a * k;
    unsigned const int bits = 32;
    unsigned int agh =  ak >> (bits - 6);
    cout << agh;
    return agh;
}

unsigned int hashCode(double d) {
    // Used for real numbers
    if (d == 0) {
        return 0U;
    }
    else {
        int exponent;
        double mantissa = frexp(d, &exponent);
        unsigned int hash = (unsigned int)((2 * fabs(mantissa) - 1) * ~0U);
        return hash;
    }
}

unsigned int hashCode(char* key) {
    // Used for string and characters
    unsigned int res = 0;
    unsigned int a = 7; // asci bits character
    for (int i = 0; key[i] != 0; i++) {
        bitset<8> c(key[i]);
        res = res << a ^ key[i]; // a xor s[i] then left shift 
        bitset<32> a(res);
    }

    return res;
}

int main()
{
    char domain1[] = "domain.com";
    char domain2[] = "domain2.com";
    unsigned int hash1 = hashCode(domain1);
    unsigned int hash2 = hashCode(domain2);

    const int len = 64;

    unsigned int* table = new unsigned int[len];

    for (unsigned int i = 0; i < len; i++) {
        table[i] = 0;
    }

    for (unsigned int i = 200000; i < 200020; i++) {
        unsigned int index = fibonacciMethod(i, len); // Used method
        table[index] = i;
    }

    // Print table
    for (int i = 0; i < len; i++) {
        cout << "Index : " << i << " -> ";
        cout << table[i];
        cout << "\n";
    }

    delete[] table;

    return 0;
}