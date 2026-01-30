Ovaj ZIP sadrzi kompletno resenje zadatka iz Racunarske grafike (GDI/MFC)

koji prikazuje 2D scenu bagera sastavljenu iz bitmap delova + metafajl viljuske.



============================================================

STA JE TRAZENO (iz PDF-a):

============================================================



1\) U konstruktoru View klase ucitati resurse (DImage):

&nbsp;  - bager.png, arm1.png, arm2.png, pozadina.png

&nbsp;  - metafajl viljuska.emf

&nbsp;  - za metafajl pribaviti okvirni pravougaonik

&nbsp;  - dealokacija u destruktoru  \[5 poena]



2\) Implementirati transformacije:

&nbsp;  - Translate(CDC\*, dx, dy, rightMultiply)

&nbsp;  - Rotate(CDC\*, angle, rightMultiply)

&nbsp;  - Scale(CDC\*, sx, sy, rightMultiply)

&nbsp;  - mnozenje trenutne world matrice sa leve ili desne strane  \[10 poena]



3\) DrawBackground(CDC\*):

&nbsp;  - pozadina.png uvek centrirana po X osi pri promeni velicine prozora

&nbsp;  - donja ivica pozadine se poklapa sa donjom ivicom klijentskog prozora  \[5 poena]



4\) DrawImgTransparent(CDC\*, DImage\*):

&nbsp;  - boja prvog piksela je transparentna

&nbsp;  - koristiti DImage::DrawTransparent  \[5 poena]



5\) DrawBody(CDC\*):

&nbsp;  - iscrtati telo bagera

&nbsp;  - pomeriti lokalni koordinatni sistem tako da se ruka kasnije crta ispred kabine  \[5 poena]



6\) DrawArm1(CDC\*):

&nbsp;  - crtanje arm1.png

&nbsp;  - rotacija oko centra veceg kruga (58, 61)

&nbsp;  - manji krug je na (309, 61)

&nbsp;  - ugao je atribut View klase i menja se tasterima  \[10 poena]



7\) DrawArm2(CDC\*):

&nbsp;  - crtanje arm2.png

&nbsp;  - rotacija oko centra veceg kruga (36, 40)

&nbsp;  - manji krug je na (272, 40)

&nbsp;  - ugao je atribut View klase i menja se tasterima  \[15 poena]



8\) DrawFork(CDC\*):

&nbsp;  - iscrtati viljuska.emf

&nbsp;  - rotacija oko (14, 20)

&nbsp;  - skaliranje viljuske 2.5x pri crtanju (da bude proporcionalna)  \[15 poena]



9\) DrawExcavator(CDC\*):

&nbsp;  - nacrtati ceo bager pozivom prethodnih funkcija

&nbsp;  - inicijalno donji desni ugao prozora  \[5 poena]



10\) Kontrole:

&nbsp;  - 1/2: arm1 +/- 10°

&nbsp;  - 3/4: arm2 +/- 10°

&nbsp;  - 5/6: viljuska +/- 10°

&nbsp;  - kursori levo/desno: pomeranje bagera po X osi

&nbsp;  - inicijalno sve komponente su na -90° u odnosu na prethodnu  \[15 poena]



11\) Eliminacija flicker-a (double buffering)  \[10 poena]



============================================================

KAKO JE RESENO (obrasci u kodu):

============================================================



Resursi (zahtev #1):

\- U CBagerView::CBagerView() ucitane su slike preko DImage::Load:

&nbsp; bager, arm1, arm2, pozadina.

\- Metafajl viljuska.emf se ucitava preko GetEnhMetaFile, a header preko

&nbsp; GetEnhMetaFileHeader (koriste se rclBounds dimenzije).

\- U destruktoru je DeleteEnhMetaFile(fork).

&nbsp; (Napomena: dealokacija DImage pokazivaca je napisana kao "delete bager, arm1, arm2, pozadina;"

&nbsp; sto u C++ ne brise sve pokazivace; idealno je delete za svaki posebno.)



Transformacije (zahtev #2):

\- Translate/Rotate/Scale koriste XFORM i CDC::ModifyWorldTransform.

\- rightMultiply kontrolise MWT\_RIGHTMULTIPLY vs MWT\_LEFTMULTIPLY.

\- Rotate koristi cos/sin, a ugao se pretvara u radijane.



Pozadina (zahtev #3):

\- DrawBackground uzima dimenzije pozadine i klijenta.

\- Pozadina je centrirana po X osi: topX = (clientW - pozadinaW)/2

\- Donja ivica je poravnata sa dnom prozora: topY = clientH - pozadinaH



Transparentno crtanje (zahtev #4):

\- Implementirano je rucno preko maske (BitBlt + SRCAND/SRCPAINT),

&nbsp; pri cemu se kao transparentna boja uzima pixel(0,0).

\- PDF trazi DImage::DrawTransparent, ali ovde je uradjen ekvivalentni

&nbsp; rezultat maskiranjem.



Telo bagera + pozicija (zahtev #5 i #9):

\- DrawBody racuna polozaj bagera tako da bude u donjem desnom uglu pozadine:

&nbsp; x = (centar pozadine) + pozadinaW - bodyW + kretanjeBagera

&nbsp; y = clientH - bodyH

\- pDC->SetViewportOrg(x, y) postavlja lokalni koordinatni sistem.

\- Zatim se telo crta transparentno.



Arm1 (zahtev #6):

\- Rotacija oko (58,61): Translate(-pivot) -> Rotate(arm1Angle) -> Translate(+pivot)

\- Nakon toga pomeraj (ARM1\_POMERAJ\_X/Y) da se arm1 pozicionira na telo.

\- arm1Angle se menja tastaturom (1/2).



Arm2 (zahtev #7):

\- Rotacija oko (36,40) + pomeraj do tacke veze sa arm1.

\- Zatim se dodatno primenjuje rotacija arm1 da bi arm2 nasledio transformaciju

&nbsp; (hijerarhijski odnos: arm2 zavisi od arm1).

\- arm2Angle se menja tastaturom (3/4).



Viljuska (zahtev #8):

\- Metafajl se rotira oko (14,20), zatim se primenjuje Scale(2.5,2.5),

&nbsp; pa Translate do tacke veze na arm2.

\- Zatim se primenjuju rotacije arm2 i arm1 (viljuska nasledjuje obe).

\- Iscrtavanje se radi preko CDC::PlayMetaFile.



Kontrole (zahtev #10):

\- OnKeyDown:

&nbsp; '1'/'2' menjaju arm1Angle po 10°

&nbsp; '3'/'4' menjaju arm2Angle po 10°

&nbsp; '5'/'6' menjaju forkAngle po 10°

&nbsp; VK\_LEFT/VK\_RIGHT (kao i A/D) menjaju kretanjeBagera po 10

\- Invalidate() forsira redraw.



Flicker (zahtev #11):

\- OnDraw koristi memorijski DC + kompatibilni bitmap (double buffering).

\- U mem DC se ukljucuje GM\_ADVANCED, crta se pozadina i bager,

&nbsp; a zatim se BitBlt prebaci na ekran.

\- OnEraseBkgnd vraca TRUE da se spreci pozadinsko brisanje.



============================================================

GDE GLEDATI U KODU:

============================================================



\- BagerView.cpp / BagerView.h

&nbsp; - DrawBackground, DrawImgTransparent, DrawBody, DrawArm1, DrawArm2, DrawFork

&nbsp; - OnDraw (double buffering), OnKeyDown (kontrole)

\- DImage.cpp / DImage.h

&nbsp; - Ucitavanje/Draw bitmapa

\- Slike/

&nbsp; - bager.png, arm1.png, arm2.png, pozadina.png, viljuska.emf



