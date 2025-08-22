#include "Array.h"

template <class T>
Array<T>::Array() {
    this->n = 10;
    this->arr = new T[this->n];
}

template <class T>
Array<T>::Array(int n) {
    this->n = n;
    this->arr = new T[this->n];
}

template <class T>
Array<T>::~Array() {
    delete[] this->arr;
}

template <class T>
T& Array<T>::operator[](int index) {
    return this->arr[index];
}

