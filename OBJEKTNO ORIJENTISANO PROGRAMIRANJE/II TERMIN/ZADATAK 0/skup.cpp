
#include "skup.h"
#include <iostream>

using namespace std;

void Skup::izbaciDuplikat()
{
	for (int i = 0; i < brojElemenata; i++)
	{
		for (int j = i+1; j < brojElemenata; j++)
		{
			if (niz[i] == niz[j])
			{
				for (int k = j; k < brojElemenata - 1; k++)
				{
					niz[k] = niz[k + 1];
				}
				brojElemenata--;
				j--;
			}
		}
	}
}

bool Skup::ispitajElement(const Complex& a)
{
	int fleg = 0;
	for (int i = 0; i < brojElemenata; i++)
	{
		if (a == niz[i])
		{
			return true;
		}
	}
	return false;
}

void Skup::ulaz()
{
	Complex pomocna;
	for (int i = 0; i < brojElemenata; i++)
	{
		pomocna.unos();
		niz[i] = pomocna;
	}
}

void Skup::urediNerastuce()
{
	Complex pom;

	for (int i = 0; i < brojElemenata-1; i++)
	{
		int imax=i;
		for (int j = i+1; j < brojElemenata; j++)
		{
			if (niz[imax] < niz[j])
			{
				imax = j;
			}
		}
		pom = niz[imax];
		niz[imax] = niz[i];
		niz[i] = pom;
	}
}

void Skup::izlaz()
{
	cout << "\nOVO JE NIZ:\n";
	for (int i = 0; i < brojElemenata; i++)
	{
		cout << niz[i]<<' ';
	}
	cout << "\n";
}

Skup Skup::razlika(const Skup& a) {
	Skup temp(this->brojElemenata);
	int k = 0;
	for (int i = 0; i < this->brojElemenata; i++) {
		int flag = 0;

		for (int j = 0; j < a.brojElemenata; j++) {
			if (niz[i] == a.niz[j]) {
				flag = 1;
				break;
			}
		}

		if (flag == 0) {
			temp.niz[k++] = niz[i];
		}
	}
	Skup rez(k);
	for (int i = 0; i < k; i++) {
		rez.niz[i] = temp.niz[i];
	}
	return rez;
}

Skup Skup::unija(const Skup& a) {
	Skup temp(this->brojElemenata + a.brojElemenata);
	int k = 0;
	for (int i = 0; i < this->brojElemenata; i++) {
		temp.niz[k++] = niz[i];
	}
	for (int i = 0; i < a.brojElemenata; i++) {
		temp.niz[k++] = a.niz[i];
	}

	Skup rez(k);
	for (int i = 0; i < k; i++) {
		rez.niz[i] = temp.niz[i];
	}
// 	rez.izbaciDuplikat();
	return rez;
}

Skup Skup::presek(const Skup& a) {
	Skup temp(this->brojElemenata > a.brojElemenata ? a.brojElemenata : this->brojElemenata);
	int k = 0;
	for (int i = 0; i < this->brojElemenata; i++) {
		for (int j = 0; j < a.brojElemenata; j++) {
			if (this->niz[i] == a.niz[j]) {
				temp.niz[k++] = this->niz[i];
			}
		}
	}
	Skup rez(k);
	for (int i = 0; i < k; i++) {
		rez.niz[i] = temp.niz[i];
	}
	return rez;
}


