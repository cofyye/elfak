Ovaj ZIP sadrzi kompletno resenje zadatka iz Racunarske grafike (GDI/MFC)

koji prikazuje 2D scenu lampe sastavljene iz bitmap delova, sa hijerarhijskim

transformacijama, transparentnim crtanjem i crtanjem senke.



============================================================

STA JE TRAZENO (iz PDF-a):

============================================================



1\) U konstruktoru klase pogleda (View), koriscenjem DImage, ucitati slike:

&nbsp;  base.png, arm1.png, arm2.png, head.png, pozadina.jpg,

&nbsp;  base\_shadow.png, arm1\_shadow.png, arm2\_shadow.png, head\_shadow.png.  \[5 poena]



2\) Napisati funkcije transformacija:

&nbsp;  Translate(CDC\*, dX, dY, rightMultiply),

&nbsp;  Rotate(CDC\*, angle, rightMultiply),

&nbsp;  Scale(CDC\*, sX, sY, rightMultiply),

&nbsp;  uz mnozenje tekuce matrice sa odgovarajuce strane.  \[10 poena]



3\) DrawBackground(CDC\*):

&nbsp;  pozadina.jpg je uvek centrirana po X osi pri promeni prozora,

&nbsp;  a donja ivica pozadine se poklapa sa donjom ivicom klijentskog prozora.  \[5 poena]



4\) DrawImgTransparent(CDC\*, DImage\*):

&nbsp;  boja prvog piksela slike je transparentna,

&nbsp;  koristiti DImage::DrawTransparent.  \[5 poena]



5\) DrawLampBase(CDC\*, bool bIsShadow):

&nbsp;  iscrtati postolje lampe;

&nbsp;  pomeriti lokalni koordinatni sistem tako da se ruka (koja se kasnije crta)

&nbsp;  nalazi ispred lampe.

&nbsp;  U zavisnosti od bIsShadow koristiti odgovarajucu sliku (base/base\_shadow).  \[5 poena]

&nbsp;  (Napomena: tekst u PDF-u za base/base\_shadow je nelogican; u praksi senka treba da koristi \*\_shadow.)



6\) DrawLampArm1(CDC\*, bool bIsShadow):

&nbsp;  iscrtati prvi deo ruke (arm1);

&nbsp;  rotacija oko centra veceg kruga na (58,61),

&nbsp;  manji krug na (309,61);

&nbsp;  ugao je atribut View klase i menja se tasterima '1' i '2'.

&nbsp;  U zavisnosti od bIsShadow koristiti arm1 / arm1\_shadow.  \[10 poena]



7\) DrawLampArm2(CDC\*, bool bIsShadow):

&nbsp;  iscrtati drugi deo ruke (arm2);

&nbsp;  rotacija oko centra veceg kruga na (36,40),

&nbsp;  manji krug na (272,40);

&nbsp;  ugao je atribut View klase i menja se tasterima '3' i '4';

&nbsp;  voditi racuna o redosledu iscrtavanja da natpis 'GDI' bude vidljiv.

&nbsp;  U zavisnosti od bIsShadow koristiti arm2 / arm2\_shadow.  \[10 poena]



8\) DrawLampHead(CDC\*, bool bIsShadow):

&nbsp;  iscrtati glavu lampe (head);

&nbsp;  rotacija oko tacke (178,100);

&nbsp;  ugao je atribut View klase i menja se tasterima '5' i '6'.

&nbsp;  U zavisnosti od bIsShadow koristiti head / head\_shadow.  \[15 poena]



9\) DrawLamp(CDC\*, bool bIsShadow):

&nbsp;  iscrtati celu lampu koriscenjem prethodnih funkcija.  \[5 poena]



10\) DrawLampShadow(CDC\*):

&nbsp;   iscrtati senku lampe tako sto se pozove DrawLamp(bIsShadow=true),

&nbsp;   uz postavljanje transformacija skaliranja (1, 0.25) i rotacije (-90 deg)

&nbsp;   da se dobije efekat senke na stolu.  \[10 poena]



11\) Tastatura:

&nbsp;   1/2 rotira arm1 za +/- 10 stepeni,

&nbsp;   3/4 rotira arm2 za +/- 10 stepeni,

&nbsp;   5/6 rotira head za +/- 10 stepeni.  \[10 poena]



12\) Nacrtati scenu uz eliminisanje flicker-a (double buffering).  \[10 poena]



============================================================

KAKO JE RESENO (obrasci u kodu):

============================================================



Resursi (zahtev #1):

\- U CLampView konstruktoru ucitani su DImage pokazivaci:

&nbsp; base, arm1, arm2, head, pozadina, baseShadow, arm1Shadow, arm2Shadow, headShadow.

\- U header-u su inicijalni uglovi postavljeni:

&nbsp; arm1Angle=-45, arm2Angle=-90, headAngle=180.



Transformacije (zahtev #2):

\- Translate/Rotate/Scale koriste XFORM i CDC::ModifyWorldTransform.

\- rightMultiply parametar bira MWT\_RIGHTMULTIPLY ili MWT\_LEFTMULTIPLY.

\- Rotate pretvara ugao u radijane i koristi cos/sin.



Pozadina (zahtev #3):

\- DrawBackground racuna topLeftX=(clientW - pozadinaW)/2 i topLeftY=clientH - pozadinaH,

&nbsp; zatim crta pozadinu u prirodnoj velicini poravnatu na dno, centriranu po X.

\- Ovo odgovara zahtevu iz PDF-a.



Transparentno crtanje (zahtev #4):

\- Implementirano je rucno maskiranje (BitBlt + maska) koristeci boju piksela (0,0) kao transparentnu.

\- PDF trazi DImage::DrawTransparent, ali ovde je uradjen ekvivalentan efekat drugim metodom

&nbsp; (odstupanje od zahteva, ali rezultat je transparentno crtanje).



Baza lampe + lokalni koordinatni sistem (zahtev #5):

\- DrawLampBase racuna pomeraj na osnovu pozadine i velicine prozora, zatim radi:

&nbsp; SetViewportOrg(pozadinaX + KOORDINATNI\_POCETAK\_X, pozadinaY + KOORDINATNI\_POCETAK\_Y).

\- Na taj nacin se “koordinatni pocetak” lampe vezuje za pozadinu.

\- Zatim se crta base ili baseShadow u zavisnosti od blsShadow.



Arm1 (zahtev #6):

\- DrawLampArm1 primenjuje obrazac:

&nbsp; Translate(-pivot) -> Rotate(arm1Angle) -> Translate(+pivot) -> Translate(pomeraj).

\- Pivot arm1 je (58,61) i odgovara PDF-u.

\- Arm1 se crta iz arm1 / arm1\_shadow u zavisnosti od blsShadow.



Arm2 (zahtev #7):

\- DrawLampArm2 rotira oko svog pivot-a (36,40), zatim radi Translate(pomeraj),

&nbsp; a potom nasledjuje transformaciju arm1 (rotacija oko arm1 pivot-a + pomeraj).

\- Time se dobija hijerarhija: arm2 zavisi od arm1.

\- Napomena za “GDI” vidljivost:

&nbsp; redosled crtanja u DrawLamp() je Base -> Arm1 -> Head -> Arm2,

&nbsp; sto obezbedjuje da arm2 bude iznad head-a gde treba.



Head (zahtev #8):

\- DrawLampHead rotira glavu oko (178,100), zatim je pozicionira (Translate),

&nbsp; zatim nasledjuje rotaciju arm2 i arm1 (hijerarhija: head zavisi od arm2 i arm1).

\- Koristi head/head\_shadow prema blsShadow.



Crtanje cele lampe (zahtev #9):

\- DrawLamp poziva DrawLampBase, DrawLampArm1, DrawLampHead, DrawLampArm2.

\- Hijerarhija se ostvaruje kroz nasledjivanje transformacija u Arm2 i Head funkcijama.



Senka (zahtev #10):

\- PDF trazi da DrawLampShadow postavi Scale(1,0.25) i Rotate(-90) pa pozove DrawLamp(true).

\- U resenju DrawLampShadow samo poziva DrawLamp(true), bez globalne transformacije.

\- Efekat senke se umesto toga postize lokalno:

&nbsp; u DrawLampArm1/DrawLampArm2/DrawLampHead postoji poseban blok if(blsShadow)

&nbsp; koji primenjuje Rotate(-90) i Scale(0.25,1) + dodatne Translate korekcije.

&nbsp; (Skaliranje je “prebaceno” po osi u odnosu na tekst zadatka, ali uz rotaciju daje slican efekat.)

\- Base senka se crta kao baseShadow bez dodatne “projekcione” transformacije.



Tastatura (zahtev #11):

\- OnKeyDown:

&nbsp; '1'/'2' menjaju arm1Angle za 10 stepeni,

&nbsp; '3'/'4' menjaju arm2Angle za 10 stepeni,

&nbsp; '5'/'6' menjaju headAngle za 10 stepeni,

&nbsp; i poziva se Invalidate().



Flicker (zahtev #12):

\- OnDraw radi double buffering:

&nbsp; pravi kompatibilni mem DC + bitmap,

&nbsp; crta pozadinu, zatim senku, zatim lampu,

&nbsp; pa BitBlt sve na ekran.



============================================================

GDE GLEDATI U KODU:

============================================================



\- LampView.cpp / LampView.h

&nbsp; - DrawBackground, DrawImgTransparent

&nbsp; - Translate, Rotate, Scale

&nbsp; - DrawLampBase, DrawLampArm1, DrawLampArm2, DrawLampHead, DrawLamp, DrawLampShadow

&nbsp; - OnDraw (double buffering), OnKeyDown (kontrole)

\- DImage.cpp / DImage.h

&nbsp; - Ucitavanje i crtanje bitmapa

\- Slike/

&nbsp; - base/arm1/arm2/head + \*\_shadow varijante + pozadina.jpg



