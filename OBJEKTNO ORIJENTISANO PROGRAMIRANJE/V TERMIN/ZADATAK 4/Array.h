#pragma once

#include <iostream>

using namespace std;

template <class T>
class Array {
    private:
        T* arr;
        int maxEl;
        int currEl;
    public:
        Array();
        Array(int n);
        ~Array();
        inline int getSize() { return this->maxEl; };
        void addElem(T elem);
        void print();

        T& operator[](int index);
        Array<T>& operator=(const Array<T>& arr);
        Array<T>* operator+(const Array<T>& arr);
};

template <class T>
Array<T>::Array() {
    this->maxEl = 10;
    this->currEl = 0;
    this->arr = new T[this->maxEl];
}

template <class T>
Array<T>::Array(int n) {
    this->maxEl = n;
    this->currEl = 0;
    this->arr = new T[this->maxEl];
}

template <class T>
Array<T>::~Array() {
    if(this->arr != 0) delete[] this->arr;
}

template <class T>
void Array<T>::addElem(T elem) {
    if(this->currEl < (this->maxEl)) {
        this->arr[this->currEl] = elem;
        this->currEl++;
    } else {
        cout << "[INFO] Cannot add element into array because is full !" << endl;
    }
}

template <class T>
void Array<T>::print() {
    for(int i = 0; i < this->currEl; i++) {
        cout << this->arr[i] << " ";
    }
    cout << endl;
}

template <class T>
T& Array<T>::operator[](int index) {
    if(index >= 0 && index < this->currEl) {
        return this->arr[index];
    } else {
        cout << "[INFO] Out of index !" << endl;
    }
}

template <class T>
Array<T>& Array<T>::operator=(const Array<T>& arr) {
    if(this != &arr) {
        this->maxEl = arr.maxEl;
        this->currEl = arr.currEl;
        if(this->arr != 0) delete[] this->arr;
        this->arr = new T[this->maxEl];
        for(int i = 0; i < this->currEl; i++) {
            this->arr[i] = arr.arr[i];
        }
    } else {
        cout << "[INFO] Error !" << endl;
    }
    return *this;
}

template <class T>
Array<T>* Array<T>::operator+(const Array<T>& arr) {
    Array<T>* temp = new Array<T>();
    temp->maxEl = this->currEl > arr.currEl ? arr.maxEl : this->maxEl;
    temp->currEl = this->currEl > arr.currEl ? arr.currEl : this->currEl;
    if(temp->arr != 0) delete[] temp->arr;
    temp->arr = new T[temp->maxEl];
    for(int i = 0; i < temp->currEl; i++) {
        temp->arr[i] = this->arr[i] + arr.arr[i];
    }

    return temp;
}
