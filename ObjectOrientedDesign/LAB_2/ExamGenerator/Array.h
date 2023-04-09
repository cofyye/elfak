#pragma once

#include <string>

template <class T>
class Array {
private:
	int currentEl;
	T** arr;
public:
	Array();
	~Array();
	void push(T* element);
	T pop();
	T shift();
	T remove(int index);
	T* getElement(int index);

	inline int getNumberOfElements() {
		return this->currentEl;
	}
};

template <class T>
Array<T>::Array() {
	// Initialization
	this->currentEl = 0;
	this->arr = new T * [this->currentEl];
}

template <class T>
Array<T>::~Array() {
	// Destructor
	if (this->arr != 0) {
		delete[] this->arr;
	}
	this->currentEl = 0;
}

template <class T>
void Array<T>::push(T* element) {
	// Change size of current elements to one more
	this->currentEl += 1;

	// Initialization of a new array
	T** newArr = new T * [this->currentEl];

	// Check if size of current elements greater than one
	if (this->currentEl > 1) {
		// Copy old elements into new array
		for (int i = 0; i < this->currentEl - 1; i++) {
			newArr[i] = this->arr[i];
		}
	}

	// Push element into array
	newArr[this->currentEl - 1] = element;

	// Delete old array
	delete[] this->arr;

	// Initialization of a old array to new one
	this->arr = newArr;
}

template <class T>
T Array<T>::pop() {
	// If list is empty, return empty subject
	if (this->currentEl < 1) {
		return T();
	}

	// Initialization of a new array
	T** newArr = new T * [this->currentEl];

	// Initialization of a element who will be removed
	T temp = *this->arr[this->currentEl - 1];

	// Remove element from tail
	for (int i = 0; i < this->currentEl - 1; i++) {
		newArr[i] = this->arr[i];
	}

	// Delete old array
	delete[] this->arr;

	// Decrease current element for one
	this->currentEl -= 1;
	// Initialization of a old array to new one
	this->arr = newArr;

	// Return removed element
	return temp;
}

template <class T>
T Array<T>::shift() {
	// If list is empty, return empty subject
	if (this->currentEl < 1) {
		return T();
	}

	// Initialization of a new array
	T** newArr = new T * [this->currentEl];

	// Initialization of a element who will be removed
	T temp = *this->arr[0];

	// Remove element from head
	for (int i = 1, j = 0; i < this->currentEl; i++, j++) {
		newArr[j] = this->arr[i];
	}

	// Delete old array
	delete[] this->arr;

	// Decrease current element for one
	this->currentEl -= 1;
	// Initialization of a old array to new one
	this->arr = newArr;

	// Return removed element
	return temp;
}

template <class T>
T Array<T>::remove(int index) {
	// If list is empty, return empty subject
	if (this->currentEl < 1) {
		return T();
	}

	// Check if index is correct
	if (index < 0 || index >= this->currentEl) {
		return T();
	}

	// Initialization of a new array
	T** newArr = new T * [this->currentEl + 1];

	// Initialization of a element who will be removed
	T temp = *this->arr[index];

	// Remove element with specific index
	int j = 0;
	for (int i = 0; i < index; i++, j++) {
		newArr[j] = this->arr[i];
	}
	for (int i = index + 1; i < this->currentEl; i++, j++) {
		newArr[j] = this->arr[i];
	}

	// Delete old array
	delete[] this->arr;

	// Decrease current element for one
	this->currentEl -= 1;
	// Initialization of a old array to new one
	this->arr = newArr;

	// Return removed element
	return temp;
}

template <class T>
T* Array<T>::getElement(int index) {
	if (index < 0 || index >= this->currentEl) {
		return new T();
	}

	return this->arr[index];
}