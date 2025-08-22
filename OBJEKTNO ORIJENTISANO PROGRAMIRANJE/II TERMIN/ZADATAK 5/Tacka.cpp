#include "Tacka.h"

Tacka::Tacka() {
	x = y = z = 0;
}

Tacka::Tacka(double xx, double yy, double zz) {
	this->x = xx;
	this->y = yy;
	this->z = zz;
}

Tacka::Tacka(const Tacka& t) {
	x = t.x;
	y = t.y;
	z = t.z;
}

void Tacka::SetTacka(double xx, double yy, double zz) {
	this->x = xx;
	this->y = yy;
	this->z = zz;
}

Tacka* Tacka::GetTacka() {
	Tacka* t = new Tacka();
	t->SetTacka(x, y, z);
	return t;
}


Tacka Tacka::saberi(Tacka& t2) 
{
	Tacka t;
	t.x = this->x + t2.x;
	t.y = this->y + t2.y;
	t.z = this->z + t2.z;

	return t;
}


Tacka Tacka::oduzmi(Tacka& t2) {
	Tacka t;
	t.x = this->x - t2.x;
	t.y = this->y - t2.y;
	t.z = this->z - t2.z;

	return t;
}

Tacka Tacka::mnoziskalarom(const double sk) {
	x = x * sk;
	y = y * sk;
	z = z * sk;
	return *this;
}








