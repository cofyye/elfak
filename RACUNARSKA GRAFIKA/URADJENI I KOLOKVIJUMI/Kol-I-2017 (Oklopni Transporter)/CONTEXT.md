Ovaj ZIP sadrzi kompletno resenje zadatka iz Racunarske grafike (GDI/MFC)

koji prikazuje 2D scenu oklopnog transportera sastavljenog od tela i 4 noge,

sa animacijom koraka (faze nogu), skaliranjem, pomeranjem pozadine i double buffering-om.



============================================================

STA JE TRAZENO (iz PDF-a):

============================================================



1\) U konstruktoru klase pogleda, koriscenjem klase DImage, ucitati slike:

&nbsp;  Body.png, Leg1.png, Leg2.png, Leg3.png i Back2.jpg.

&nbsp;  Dealokaciju ostvariti u destruktoru.  \[5 poena]



2\) Napisati funkcije:

&nbsp;  - LoadIdentity(CDC\* pDC)

&nbsp;  - Translate(CDC\* pDC, float dX, float dY, bool rightMultiply)

&nbsp;  - Rotate(CDC\* pDC, float angle, bool rightMultiply)

&nbsp;  - Scale(CDC\* pDC, float sX, float sY, bool rightMultiply)

&nbsp;  koje postavljaju jedinicnu matricu ili definisu svetske transformacije

&nbsp;  mnozenjem tekuce matrice sa odgovarajuce strane.  \[10 poena]



3\) Napisati funkciju:

&nbsp;  void DrawScene(CDC\* pDC, CRect rect)

&nbsp;  koja iscrtava scenu u okviru rect.

&nbsp;  Scena se sastoji od:

&nbsp;  - tela transportera (Body.png),

&nbsp;  - 4 noge (pozivima DrawLeg),

&nbsp;  - pozadine (Back2.\*).

&nbsp;  Gornji levi ugao tela je inicijalno (100,100).

&nbsp;  Sve noge su spustene u odnosu na telo za 168 px.

&nbsp;  Zadnje noge su pomerene duz X za 72 px, prednje za 270 px.  \[10 poena]



4\) Napisati funkciju:

&nbsp;  void DrawLeg(CDC\* pDC, double alpha, double dx, double dy)

&nbsp;  koja iscrtava jednu nogu:

&nbsp;  - dx,dy su pomeraji gornjeg levog ugla noge u odnosu na telo

&nbsp;  - alpha je ugao rotacije gornjeg dela (Leg1.png) u odnosu na neutralu (vertikalno)

&nbsp;  Prednja 2 noge su zarotirane po 20° u suprotnim smerovima na slici.

&nbsp;  Donja dva dela noge moraju ostati vertikalna.

&nbsp;  Centar rotacije: gornji deo 28 px od gornjeg levog ugla, donji deo 29 px.  \[25 poena]



5\) Pritiskom na kursorski taster desno (→) omoguciti kretanje transportera udesno.

&nbsp;  Svakim pritiskom gornji deo noge prolazi kroz stanja:

&nbsp;  -20°, -10°, 0°, +10°, +20° (ciklicno).

&nbsp;  Sekvenca je ista za sve noge, ali svaka noga je u drugoj fazi.  \[25 poena]



6\) Pritiskom na kursorske tastere gore/dole (↑/↓) omoguciti

&nbsp;  povecanje i smanjenje transportera za po 10%.  \[5 poena]



7\) Pritiskom na A/D pomerati pozadinu za 200 px levo/desno,

&nbsp;  a na W/S gore/dole.

&nbsp;  Ne dozvoliti izlazak iz okvira slike (clamp).  \[10 poena]



8\) Eliminisati flicker (double buffering).  \[10 poena]



============================================================

KAKO JE RESENO (obrasci u kodu):

============================================================



Resursi (zahtev #1):

\- U CTransporterView konstruktoru ucitani su DImage resursi:

&nbsp; body, leg1, leg2, leg3, pozadina (Back2.jpg).

\- Inicijalne faze nogu su postavljene kroz nizove:

&nbsp; legAngles\[0..3] i legPrevAngels\[0..3] (razlicite pocetne faze).

\- Napomena (bug): u destruktoru je napisano:

&nbsp; delete body, leg1, leg2, leg3, pozadina;

&nbsp; sto u C++ ne brise sve pokazivace vec efektivno samo prvi.

&nbsp; Ispravno bi bilo delete za svaki pokazivac posebno.



Transformacije + LoadIdentity (zahtev #2):

\- LoadIdentity postavlja world transform na identitet (SetWorldTransform).

\- Translate/Rotate/Scale koriste XFORM i ModifyWorldTransform,

&nbsp; sa MWT\_RIGHTMULTIPLY ili MWT\_LEFTMULTIPLY u zavisnosti od rightMultiply.

\- Rotate radi konverziju u radijane i koristi cos/sin.



DrawScene (zahtev #3):

\- Pozadina se crta kroz DrawPozadina(pDC, rect).

\- Telo se crta sa viewport origin na (OFFSET\_X + kretanjeTransportera, OFFSET\_Y),

&nbsp; gde su OFFSET\_X=100 i OFFSET\_Y=100 (kao u zadatku).

\- Noge se crtaju pozivom DrawLeg(pDC, alpha, dx, dy) za 4 noge:

&nbsp; - zadnje noge dx = BACK\_LEGS\_OFFSET\_X (72 uz korekciju u kodu), dy = 168

&nbsp; - prednje noge dx = FRONT\_LEGS\_OFFSET\_X (270), dy = 168

&nbsp; (Napomena: oba zadnja poziva koriste isti dx/dy pa se noge preklapaju;

&nbsp;  moguce je da je jedna noga “iza” druge, ali u 2D to izgleda kao duplikat.)



Transparentno crtanje (implicitno, iako nije eksplicitna tacka u PDF-u):

\- Implementirano je rucno maskiranje:

&nbsp; - boja piksela (0,0) se uzima kao transparentna

&nbsp; - pravi se 1bpp maska i radi BitBlt kombinacijom SRCAND/SRCPAINT

\- Ovo je standardna zamena kada DImage nema DrawTransparent ili kada se ne koristi.



DrawLeg (zahtev #4):

\- Noga se crta u 3 segmenta:

&nbsp; 1) Leg1: rotira se oko tacke (LEG\_ROT\_X, LEG\_ROT\_Y) i pomera za (dx,dy)

&nbsp; 2) Leg2: zadrzava vertikalu tako sto rotira za -alpha oko svoje tacke,

&nbsp;    zatim nasledjuje rotaciju leg1 (rotacija oko tacke leg1+pomeraj)

&nbsp; 3) Leg3: slicno, nasledjuje rotaciju leg2 (-alpha) i leg1 (alpha),

&nbsp;    uz svoje pomeraje (LEG3\_OFFSET\_\*)

\- Skala transportera se primenjuje (Scale(transporterScale)) pre crtanja svakog segmenta.



Animacija faza nogu + kretanje (zahtev #5):

\- OnKeyDown na VK\_RIGHT poziva ChangeAngle za svaku nogu i pomera transporter.

\- ChangeAngle implementira “ping-pong” kretanje izmedju -20 i +20 u koracima 10,

&nbsp; koristeci prevAngle da odredi smer promene.

\- Napomena: kod menja faze i na VK\_LEFT (PDF trazi samo desno),

&nbsp; i pomera transporter i ulevo.



Skaliranje (zahtev #6):

\- VK\_UP povecava transporterScale za 0.1 (10%).

\- VK\_DOWN smanjuje transporterScale za 0.1 (10%).

\- Napomena: nema clamp-a na minimalnu vrednost (moze postati premala ili negativna).



Pomeranje pozadine + ogranicenja (zahtev #7):

\- A/D/W/S menjaju offsetPozadineX/Y za 200.

\- Pozadina se crta kroz rect definisan kao:

&nbsp; CRect(offsetPozadineX, offsetPozadineY,

&nbsp;       pozadina->Width()+offsetPozadineX, pozadina->Height()+offsetPozadineY)

&nbsp; sto prakticno pomera pozadinu u prozoru.

\- Napomena (bug): provere granica su pomešane:

&nbsp; offsetPozadineY se poredi sa pozadina->Width(), a offsetPozadineX sa pozadina->Height(),

&nbsp; sto verovatno nije ispravno ogranicenje.



Flicker (zahtev #8):

\- OnDraw pravi mem DC + kompatibilni bitmap velicine clientRect-a,

&nbsp; crta scenu u mem DC, pa BitBlt na ekran.

\- OnEraseBkgnd postoji u message map (eliminacija brisanja pozadine).



============================================================

GDE GLEDATI U KODU:

============================================================



\- TransporterView.cpp / TransporterView.h

&nbsp; - LoadIdentity, Translate, Rotate, Scale

&nbsp; - DrawImgTransparent (maska), DrawPozadina

&nbsp; - DrawBody, DrawLeg, DrawScene

&nbsp; - OnDraw (double buffering), OnKeyDown (animacija, skala, pozadina)

\- DImage.cpp / DImage.h

&nbsp; - Load, Draw, dimenzije slike

\- Slike/

&nbsp; - Body.png, Leg1.png, Leg2.png, Leg3.png, Back2.jpg



