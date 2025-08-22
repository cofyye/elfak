#pragma once

class Poligon {
  
  private:
    int brojTemena;
    int *x;
    int *y;
    
  public:
    Poligon();
    Poligon(int brojTemena);
    ~Poligon();
    inline int dajBrojTemena() {
      return this->brojTemena;
    }
    double izracunajObim();
    void ucitaj();
    void najudaljenijeTacke(int &x1, int &y1, int &x2, int &y2);
    void prikazi();
    void izbaci();
};