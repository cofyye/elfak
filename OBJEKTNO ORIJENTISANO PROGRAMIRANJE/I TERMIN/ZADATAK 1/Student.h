#pragma once
class Student
{
private:
	char* ime;
	char* prezime;
	int brojIndeksa;
	int maxBrIspita;
	int brPolIspita = 0;
	int* nizOcena;
public:
	Student(int maxBrIspita);
	~Student();
	inline int VratiBrIndeksa() {
	    return this->brojIndeksa;
	}
	inline int VratiBrPolIspita() {
	    return this->brPolIspita;
	}
	void DodajOcenu(int ocena);
	void Ucitaj();
	void PrikaziOcene();
	void PrikaziPodatke();
	void setIme(char* ime);
	void setPrezime(char* prezime);
};