#pragma once

class Buffer {
    private:
        int *arr;
        int size;
        int numOfEls = 0;
    public:
        Buffer();
        Buffer(int size);
        Buffer(const Buffer& buf);
        ~Buffer();
        inline int getNumOfEls() {
            return this->numOfEls;
        }
        void push(int data);
        int pop();
        void print();
        void duplicate();
        void removee();
};
