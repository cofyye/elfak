Ovaj ZIP sadrzi kompletno resenje zadatka iz Racunarske grafike (GDI/MFC)

koji prikazuje 2D scenu robota (Transformers) sastavljenog iz bitmap delova,

sa hijerarhijskim transformacijama i kontrolom preko tastature.



============================================================

STA JE TRAZENO (iz PDF-a):

============================================================



1\) U konstruktoru klase pogleda, koriscenjem klase DImage, ucitati slike:

&nbsp;  body1.png, arm1.png, arm2.png, leg1.png, leg2.png i background.jpg.

&nbsp;  Dealokaciju ostvariti u destruktoru.  \[5 poena]



2\) Omoguciti iscrtavanje u memorijskom DC-ju i eliminisati flicker.  \[10 poena]



3\) Napisati funkciju:

&nbsp;  void DrawBackground(CDC\* pDC, CRect rc),

&nbsp;  koja iscrtava sliku pozadina.png u prirodnoj velicini u okviru rc,

&nbsp;  poravnatu uz gornju i levu ivicu.  \[5 poena]

&nbsp;  (Napomena: u resursima je background.jpg, a u tekstu se pominje pozadina.png.)



4\) Napisati funkciju:

&nbsp;  void DrawImgTransparent(CDC\* pDC, DImage\* pImage),

&nbsp;  koja iscrtava sliku pImage u pDC tako da je boja prvog piksela transparentna.

&nbsp;  Za transparentno iscrtavanje koristiti DImage::DrawTransparent.  \[5 poena]



5\) Napisati transformacije:

&nbsp;  void Translate(CDC\* pDC, float dX, float dY, bool rightMultiply)

&nbsp;  void Rotate(CDC\* pDC, float angle, bool rightMultiply)

&nbsp;  Svetske transformacije definisati mnozenjem tekuce matrice sa odgovarajuce strane.  \[5 poena]



6\) Napisati funkciju:

&nbsp;  void DrawArm1(CDC\* pDC),

&nbsp;  koja iscrtava prvi deo ruke (arm1.png). Deo se rotira oko (34,31),

&nbsp;  a drugi deo ruke se rotira oko (210,102).

&nbsp;  U ovoj funkciji definisati rotaciju za ugao (atribut View klase),

&nbsp;  koji se menja na tastere Q i A.  \[10 poena]



7\) Napisati funkciju:

&nbsp;  void DrawArm2(CDC\* pDC),

&nbsp;  koja iscrtava drugi deo ruke (arm2.png). Deo se rotira oko (23,61)

&nbsp;  i poklapa se sa odgovarajucom tackom na arm1.png.

&nbsp;  U ovoj funkciji definisati rotaciju za ugao (atribut View klase),

&nbsp;  koji se menja na tastere T i G.

&nbsp;  Voditi racuna o redosledu crtavanja.  \[15 poena]



8\) Napisati funkciju:

&nbsp;  void DrawLeg1(CDC\* pDC),

&nbsp;  koja iscrtava prvi deo noge (leg1.png). Deo se rotira oko (30,125),

&nbsp;  a drugi deo noge se rotira oko (237,125).

&nbsp;  U ovoj funkciji definisati rotaciju za ugao (atribut View klase),

&nbsp;  koji se menja na tastere Q i A.  \[10 poena]



9\) Napisati funkciju:

&nbsp;  void DrawLeg2(CDC\* pDC),

&nbsp;  koja iscrtava drugi deo noge (leg2.png). Deo se rotira oko (35,60).

&nbsp;  U ovoj funkciji definisati rotaciju za ugao (atribut View klase),

&nbsp;  koji se menja na tastere R i F.  \[10 poena]



10\) Napisati funkciju:

&nbsp;   void DrawBody1(CDC\* pDC),

&nbsp;   koja iscrtava telo (body1.png). Deo se rotira oko (26,133).

&nbsp;   U ovoj funkciji definisati rotaciju za ugao (atribut View klase),

&nbsp;   koji se menja na tastere W i S.  \[10 poena]



11\) Napisati funkciju:

&nbsp;   void DrawTransformer(CDC\* pDC),

&nbsp;   koja iscrtava citavog transformera koriscenjem prethodnih funkcija,

&nbsp;   i pozvati sve neophodne funkcije u OnDraw() da bi se iscrtala scena.  \[5 poena]



12\) Omoguciti da se pritiskom na kursorske tastere (levo/desno) izvrsi transformacija robota.

&nbsp;   Ograniciti minimalne i maksimalne uglove tako da se rasklapanje vrsi do polozaja garda,

&nbsp;   a sklapanje do automobila.  \[10 poena]



============================================================

KAKO JE RESENO (obrasci u kodu):

============================================================



Resursi (zahtev #1):

\- U CTransformersView konstruktoru ucitani su DImage resursi:

&nbsp; body, arm1, arm2, leg1, leg2, background.

\- U destruktoru se radi delete za svaki od DImage pokazivaca pojedinacno.



Flicker / double buffering (zahtev #2):

\- OnDraw koristi memorijski DC (CreateCompatibleDC) i kompatibilan bitmap,

&nbsp; crta sve u mem DC pa BitBlt na ekran.

\- GM\_ADVANCED je ukljucen (SetGraphicsMode) da bi world transformacije radile korektno.



Pozadina (zahtev #3):

\- Implementirana je funkcija DrawBackground(CDC\*, CRect).

\- Napomena: resenje koristi DImage::Draw(pDC, rcImg, rcDC) tako da se pozadina

&nbsp; razvlaci (StretchBlt) preko celog clientRect-a (rcDC).

&nbsp; PDF trazi prirodnu velicinu i poravnanje gore/levo u okviru rc,

&nbsp; tako da je ovde implementacija "stretch-to-fit" (odstupanje od zahteva).



Transparentno crtanje (zahtev #4):

\- Implementirana je DrawImgTransparent, ali ne koristi DImage::DrawTransparent.

\- Umesto toga se pravi maska (CreateBitmap 1bpp) i radi BitBlt kombinacijom

&nbsp; SRCAND/SRCPAINT, pri cemu se kao transparentna boja uzima piksel(0,0).

\- Efekat je transparentno crtanje kao u zadatku, ali metod je rucni (odstupanje od zahteva).



Transformacije (zahtev #5):

\- Translate i Rotate koriste XFORM + CDC::ModifyWorldTransform.

\- Parametar rightMultiply bira MWT\_RIGHTMULTIPLY ili MWT\_LEFTMULTIPLY.

\- Rotate koristi standardan cos/sin (angle u radijanima).



Hijerarhijsko crtanje delova (zahtevi #6–#11):

\- Pivot tacke su definisane kao konstante i odgovaraju PDF-u:

&nbsp; ARM1\_ROT (34,31), ARM2\_ROT (23,61), LEG1\_ROT (30,125),

&nbsp; LEG2\_ROT (35,60), BODY\_ROT (26,133).

\- Svaka funkcija (DrawArm1/DrawArm2/DrawLeg1/DrawLeg2/Body1) radi obrazac:

&nbsp; 1) sacuva old world transform (GetWorldTransform)

&nbsp; 2) Translate(-pivot) -> Rotate(angle) -> Translate(+pivot)

&nbsp; 3) Translate(offset) da se deo pozicionira u sceni

&nbsp; 4) dodatne rotacije oko roditeljskih delova (nasledjivanje hijerarhije)

&nbsp; 5) DrawImgTransparent za konkretan bitmap

&nbsp; 6) vrati staru transformaciju (SetWorldTransform)



Konkretnije veze (ko je "roditelj" kome):

\- DrawArm2 nasledjuje rotaciju arm1 (i potom body i leg1).

\- DrawArm1 nasledjuje rotaciju body i leg1.

\- Body1 nasledjuje rotaciju oko leg1 (pored svoje rotacije).

\- DrawLeg2 nasledjuje rotaciju oko leg1.

\- DrawLeg1 koristi ugao izveden iz arm1Angle (arm1Angle - 100) kao "sinhronizaciju"

&nbsp; noge sa stanjem transformacije.



Kontrola uglova (zahtev #12 + pojedinacni zahtevi #6–#10):

\- Atributi View klase (TransformersView.h):

&nbsp; arm1Angle, arm2Angle, leg2Angle, bodyAngle i kreanjeRobota.

\- OnKeyDown:

&nbsp; Q/A menjaju arm1Angle u opsegu 0..100 (korak 5)

&nbsp; T/G menjaju arm2Angle u opsegu 0..280 (korak 5)

&nbsp; R/F menjaju leg2Angle u opsegu 0..-210 (korak 5, negativan ugao)

&nbsp; W/S menjaju bodyAngle u opsegu -100..-90 (korak 5)

\- VK\_RIGHT / VK\_LEFT rade "automatsku transformaciju" menjanjem vise uglova odjednom,

&nbsp; uz clamp na granice (min/max) da bi se doslo do garda ili automobila.

\- VK\_UP / VK\_DOWN menjaju kreanjeRobota, ali samo kada su uglovi u krajnjem polozaju

&nbsp; (arm1Angle=100, arm2Angle=280, leg2Angle=-210, bodyAngle=-100).

\- Invalidate() se poziva da se scena osvezi.



Napomena o fajlu koordinate.txt:

\- Fajl koordinate.txt postoji u projektu, ali NIJE pomenut u PDF-u kao ulaz

&nbsp; i nije koriscen u implementaciji (nema ucitavanja iz tog fajla).

\- Preporuka: ignorisati ga ili ukloniti radi manjeg suma u bazi primera.



============================================================

GDE GLEDATI U KODU:

============================================================



\- TransformersView.cpp / TransformersView.h

&nbsp; - DrawBackground, DrawImgTransparent, Translate, Rotate

&nbsp; - DrawArm1, DrawArm2, DrawLeg1, DrawLeg2, Body1, DrawTransformer

&nbsp; - OnDraw (double buffering), OnKeyDown (kontrole i ogranicenja uglova)

\- DImage.cpp / DImage.h

&nbsp; - Ucitavanje (Load), crtanje (Draw), pomocne funkcije

\- Slike/

&nbsp; - arm1.png, arm2.png, leg1.png, leg2.png, body1.png, background.jpg



