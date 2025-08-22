#include<iostream>
using namespace std;

class Tacka {
private:
	double x;
	double y;
	double z;
public:

	Tacka();
	Tacka(double xx, double yy, double zz);
	Tacka(const Tacka& t);
	~Tacka(){};

	
	void SetTacka(double xx, double yy, double zz);
	Tacka* GetTacka();

	double getX() const { return this->x; }
	double getY() const { return this->y; }
	double getZ() const { return this->z; }
	double getR() const { return sqrt(x * x + y * y + z * z); }
	
	void printTacka() { cout << x << " " << y << " " << z << endl; };


	Tacka saberi(Tacka& t2);
	Tacka oduzmi(Tacka& t2);
	Tacka mnoziskalarom(const double sk);

	void rotirajdesno();
	void rotirajlevo();

	

	

};