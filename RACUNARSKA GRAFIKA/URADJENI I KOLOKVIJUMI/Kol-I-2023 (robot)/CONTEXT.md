Ovaj ZIP sadrzi kompletno resenje zadatka iz Racunarske grafike (GDI/MFC)

koji prikazuje 2D robota sastavljenog iz bitmap delova, sa hijerarhijskim

transformacijama, refleksijom za desnu polovinu, i globalnim rotiranjem + skaliranjem.



============================================================

STA JE TRAZENO (iz PDF-a):

============================================================



1\) U konstruktoru klase pogleda, koriscenjem klase DImage, ucitati slike:

&nbsp;  glava.png, nadkolenica.png, nadlaktica.png, podkolenica.png,

&nbsp;  podlaktica.png, saka.png, stopalo.png, telo.png, pozadina.jpg.

&nbsp;  Dealokaciju ostvariti u destruktoru.  \[5 poena]



2\) Omoguciti iscrtavanje u memorijskom DC-ju i eliminisati flicker.  \[10 poena]



3\) Napisati funkciju:

&nbsp;  void DrawBackground(CDC\* pDC),

&nbsp;  koja iscrtava sliku pozadina.jpg u prirodnoj velicini.  \[5 poena]



4\) Napisati funkciju:

&nbsp;  void DrawImgTransparent(CDC\* pDC, DImage\* pImage),

&nbsp;  koja iscrtava sliku pImage tako da je boja prvog piksela transparentna.

&nbsp;  Za transparentno iscrtavanje koristiti gotovu funkciju DrawTransparent klase DImage.  \[5 poena]



5\) Napisati funkcije transformacija:

&nbsp;  a) Translate(CDC\* pDC, float dX, float dY, bool rightMultiply)

&nbsp;  b) Rotate(CDC\* pDC, float angle, bool rightMultiply)

&nbsp;  c) Scale(CDC\* pDC, float sX, float sY, bool rightMultiply)

&nbsp;  Svetske transformacije definisati mnozenjem tekuce matrice sa odgovarajuce strane.  \[10 poena]



6\) U okviru View klase definisati atribute:

&nbsp;  - rotacija nadlaktice, podlaktice, sake i celog robota,

&nbsp;  - skaliranje celog robota.  \[2 poena]



7\) Napisati funkciju:

&nbsp;  void DrawHalf(CDC\* pDC),

&nbsp;  koja iscrtava levu polovinu robota (bez glave).

&nbsp;  Rotacije:

&nbsp;  - nadlaktica oko (35,35)

&nbsp;  - podlaktica oko (30,33)

&nbsp;  - saka oko (25,3)

&nbsp;  Kontrole i ogranicenja:

&nbsp;  - A/S menja ugao sake (min -10, max 30)

&nbsp;  - D/F menja ugao podlaktice (min -10, max 80)

&nbsp;  - G/H menja ugao nadlaktice (min -10, max 90)

&nbsp;  Koordinate za nadovezivanje date su u tabeli 1.  \[20 poena]



&nbsp;  Tabela 1 (nadovezivanje):

&nbsp;  - Telo (25,65) -> Nadlaktica (35,35)

&nbsp;  - Nadlaktica (22,167) -> Podlaktica (30,33)

&nbsp;  - Podlaktica (30,140) -> Saka (25,3)

&nbsp;  - Telo (61,262) -> Nadkolenica (29,20)

&nbsp;  - Nadkolenica (30,184) -> Podkolenica (25,37)

&nbsp;  - Podkolenica (25,248) -> Stopalo (20,16)



8\) Napisati funkciju:

&nbsp;  void DrawHead(CDC\* pDC),

&nbsp;  koja iscrtava glavu robota.  \[3 poena]



9\) Napisati funkciju:

&nbsp;  void DrawRobot(CDC\* pDC),

&nbsp;  koja iscrtava celog robota koristeci DrawHalf, DrawHead i transformacije.

&nbsp;  Desna polovina robota iscrtava se koriscenjem refleksije.  \[20 poena]



10\) Obezbediti istovremenu rotaciju i skaliranje celog robota

&nbsp;   koriscenjem atributa View klase koji se menjaju na tastere 1 i 2.

&nbsp;   Robot treba da se rotira oko centra i smanjuje/povecava.  \[15 poena]



11\) Pozvati sve neophodne funkcije u OnDraw() kako bi se iscrtala citava scena.  \[5 poena]



============================================================

KAKO JE RESENO (obrasci u kodu):

============================================================



Resursi (zahtev #1):

\- U CRobotView konstruktoru ucitani su DImage resursi:

&nbsp; glava, nadkolenica, nadlaktica, podkolenica, podlaktica, saka, stopalo, telo, pozadina.

\- Napomena: u ovom projektu DImage klasa NE sadrzi DrawTransparent funkciju (postoji samo Draw),

&nbsp; pa transparentno crtanje nije moguce uraditi "po tekstu zadatka" bez dopune templejta.

\- U destruktoru se vrsi delete za pokazivace (pojedinacno).



Flicker / double buffering (zahtev #2):

\- OnDraw kreira kompatibilni mem DC + bitmap velicine clientRect-a,

&nbsp; crta u mem DC, pa na kraju BitBlt na ekran.

\- GM\_ADVANCED je ukljucen (SetGraphicsMode) da bi world transformacije radile.

\- OnEraseBkgnd vraca 1 da se spreci brisanje pozadine (manje treperenja).



Pozadina (zahtev #3):

\- DrawBackground crta pozadina.jpg u prirodnoj velicini na (0,0):

&nbsp; pozadina->Draw(pDC, CRect(0,0,w,h), CRect(0,0,w,h)).

\- Ovo odgovara zahtevu (nema dodatnog centriranja/poravnanja kao kod bager/lampa zadataka).



Transparentno crtanje (zahtev #4):

\- Implementirano je rucno maskiranje (BitBlt + maska), koristeci boju piksela (0,0) kao transparentnu.

\- PDF trazi DImage::DrawTransparent, ali ta funkcija ne postoji u ovom DImage templejtu,

&nbsp; pa je rucna maska realna zamena.



Transformacije (zahtev #5):

\- Translate/Rotate/Scale koriste XFORM i CDC::ModifyWorldTransform.

\- rightMultiply bira MWT\_RIGHTMULTIPLY ili MWT\_LEFTMULTIPLY.

\- Rotate pretvara ugao u radijane (TO\_RAD) i koristi cos/sin.



Atributi i kontrole uglova (zahtevi #6, #7, #10):

\- Atributi u View klasi:

&nbsp; nadlakticaAngle, podlakticaAngle, sakaAngle, robotAngle, robotScale, mirrorBody.

\- OnKeyDown:

&nbsp; - G/H: nadlakticaAngle +5/-5, ograniceno na \[-10, 90]

&nbsp; - D/F: podlakticaAngle +5/-5, ograniceno na \[-10, 80]

&nbsp; - A/S: sakaAngle +5/-5, ograniceno na \[-10, 30]

&nbsp; - '1': robotAngle -= 10; robotScale -= 0.05

&nbsp; - '2': robotAngle += 10; robotScale += 0.05

&nbsp; Nakon izmene poziva se Invalidate().



Hijerarhija ruke (zahtev #7 + tabela 1):

\- Konstante na vrhu RobotView.cpp direktno implementiraju tabelu 1:

&nbsp; NADLAKTICA\_ROTACIJA=(35,35)

&nbsp; NADLAKTICA\_POMERAJ=(25-35, 65-35)  // telo(25,65) -> nadlaktica(35,35)

&nbsp; PODLAKTICA\_ROTACIJA=(30,33)

&nbsp; PODLAKTICA\_POMERAJ=(22-30 + NADLAKTICA\_POMERAJ\_X, 167-33 + NADLAKTICA\_POMERAJ\_Y)

&nbsp; SAKA\_ROTACIJA=(25,3)

&nbsp; SAKA\_POMERAJ=(30-25 + PODLAKTICA\_POMERAJ\_X, 140-3 + PODLAKTICA\_POMERAJ\_Y)



\- DrawNadlaktica:

&nbsp; rotacija oko (35,35) za nadlakticaAngle, zatim translate pomeraj do tela.

\- DrawPodlaktica:

&nbsp; rotacija oko (30,33) za podlakticaAngle + pomeraj,

&nbsp; zatim nasledjuje nadlakticaAngle oko tacke (NADLAKTICA\_ROTACIJA + NADLAKTICA\_POMERAJ).

\- DrawSaka:

&nbsp; rotacija oko (25,3) za sakaAngle + pomeraj,

&nbsp; zatim nasledjuje rotaciju podlaktice i nadlaktice (dve hijerarhijske rotacije).



Hijerarhija noge (tabela 1, bez rotacija):

\- NADKOLENICA\_POMERAJ = (61-29, 262-20)

\- PODKOLENICA\_POMERAJ = (30-25 + NADKOLENICA\_POMERAJ\_X, 184-37 + NADKOLENICA\_POMERAJ\_Y)

\- STOPALO\_POMERAJ     = (25-20 + PODKOLENICA\_POMERAJ\_X, 248-16 + PODKOLENICA\_POMERAJ\_Y)

\- U resenju nema uglova za noge (u PDF-u se ne trazi kontrola za noge), vec se samo pozicioniraju.



Globalna rotacija + skaliranje robota (zahtev #10):

\- Svaki deo (telo, ruke, noge, glava) nakon svojih lokalnih transformacija primenjuje globalni obrazac:

&nbsp; Translate(-CENTER) -> Scale(robotScale) -> Rotate(robotAngle) -> Translate(+CENTER).

\- CENTER\_X i CENTER\_Y su izvedeni iz POMERAJ\_FIGURE i “512” konstanti, da bi rotacija bila oko centra figure.

\- Ovo obezbedjuje simultanu rotaciju i skaliranje “oko centra”.



Refleksija desne polovine (zahtev #9):

\- DrawRobot:

&nbsp; - postavi viewport origin na (POMERAJ\_FIGURE\_X, POMERAJ\_FIGURE\_Y), nacrta glavu i levu polovinu.

&nbsp; - zatim postavi viewport origin na (POMERAJ\_FIGURE\_X + MIRROR\_POMERAJ, POMERAJ\_FIGURE\_Y),

&nbsp;   ukljuci mirrorBody=true i nacrta ponovo DrawHalf.

\- Kada je mirrorBody=true:

&nbsp; - u delovima se radi Scale(-1,1) (horizontalna refleksija),

&nbsp; - globalna rotacija se primenjuje sa suprotnim znakom ugla (mirrorBody ? -robotAngle : robotAngle)

&nbsp;   da bi se zadrzao konzistentan smer pri ogledanju.



DrawHalf / DrawHead / DrawRobot (zahtevi #7–#11):

\- DrawHalf crta: telo + noga (nadkolenica/podkolenica/stopalo) + ruka (nadlaktica/podlaktica/saka).

\- DrawHead crta glava.png uz translate (POMERAJ\_GLAVE\_X/Y) i globalnu transformaciju.

\- OnDraw poziva: DrawBackground, DrawRobot i zatim BitBlt na ekran.



============================================================

GDE GLEDATI U KODU:

============================================================



\- RobotView.cpp / RobotView.h

&nbsp; - Translate, Rotate, Scale

&nbsp; - DrawBackground, DrawImgTransparent

&nbsp; - DrawTelo, DrawNadlaktica, DrawPodlaktica, DrawSaka

&nbsp; - DrawNadkolenica, DrawPodkolenica, DrawStopalo

&nbsp; - DrawHalf, DrawHead, DrawRobot

&nbsp; - OnDraw (double buffering), OnKeyDown (kontrole i ogranicenja)

\- DImage.cpp / DImage.h

&nbsp; - Load i Draw (napomena: nema DrawTransparent u ovom templejtu)

\- Slike/

&nbsp; - glava.png, telo.png, delovi ruke/noge, pozadina.jpg



