#pragma once

class Stack {
  private:
    int size;
    int* array;
    int top;
  public:
    Stack(int size);
    ~Stack();
    void push(int el);
    int pop();
    bool isEmpty();
    int numberOfElements();
    int getTop();
    void print();
};