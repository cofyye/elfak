\# cofyye – GDI (GDI/MFC Toolbox)



Ovaj projekat NIJE resenje konkretnog zadatka.

Ovo je pomocni toolbox sa cistim, organizovanim primerima i sablonima

za najcesce ponavljane delove zadataka iz Racunarske grafike (MFC/GDI).



Namena:

\- da sluzi kao centralni izvor za standardni GDI/MFC kod,

\- koji se kopira i prilagodjava u View.cpp fajlu novog zadatka,

\- bez ponovnog pisanja istih pomocnih funkcija.



------------------------------------------------------------

STA OVAJ PROJEKAT SADRZI

------------------------------------------------------------



Ovaj toolbox sadrzi implementacije i primere za:



1\) Svetske transformacije (World Transform)

\- LoadIdentity

\- Translate

\- Rotate (stepeni → radijani)

\- Scale

\- Mirror (Scale(-1,1))

\- rightMultiply / leftMultiply (ModifyWorldTransform)



2\) Upravljanje transformacijama

\- cuvanje i vracanje XFORM matrice (save/restore obrazac)

\- sprecavanje “razlivanja” transformacija izmedju delova scene



3\) Eliminacija flicker-a (double buffering)

\- crtanje u memorijskom DC-ju

\- BitBlt iz memDC u screen DC

\- GM\_ADVANCED

\- OnEraseBkgnd vraca TRUE



4\) Transparentno crtanje (color key)

\- transparentna boja = prvi piksel (0,0)

\- maska preko BitBlt (SRCAND / SRCPAINT)

\- alternativa za DImage bez DrawTransparent



5\) Rad sa resursima

\- DImage (PNG/JPG/BMP)

\- pravilno ucitavanje i dealokacija

\- bezbedan delete pokazivaca



6\) EMF metafajlovi

\- GetEnhMetaFile

\- PlayEnhMetaFile

\- GetEnhMetaFileHeader (rclBounds)

\- skaliranje i rotacija metafajlova



------------------------------------------------------------

KADA TREBA KORISTITI OVAJ TOOLBOX

------------------------------------------------------------



Ovaj projekat treba koristiti kao referencu kada u PDF-u novog zadatka

vidis jednu ili vise sledecih stvari:



\- ModifyWorldTransform / SetWorldTransform

\- GM\_ADVANCED

\- Translate / Rotate / Scale / Mirror

\- rightMultiply / leftMultiply

\- “eliminisati flicker”

\- “iscrtavanje u memorijskom DC-ju”

\- “boja prvog piksela je transparentna”

\- EMF metafajl (PlayMetaFile)

\- hijerarhijsko crtanje delova sa pivot tackama



U tim slucajevima:

\- odgovarajuci kod se uzima iz ovog toolbox-a,

\- kopira u View.cpp novog zadatka,

\- i prilagodjava konkretnim pivotima, offsetima i kontrolama iz PDF-a.



------------------------------------------------------------

KAKO KORISTITI TOOLBOX (PRAVILO)

------------------------------------------------------------



1\) Prvo analizirati PDF zadatka:

&nbsp;  - koje funkcije se traze

&nbsp;  - koji delovi scene postoje

&nbsp;  - koje su pivot tacke i veze (roditelj/dete)

&nbsp;  - koje kontrole i ogranicenja uglova postoje



2\) Zatim u ovom projektu pronaci:

&nbsp;  - odgovarajuci helper za transformacije

&nbsp;  - double buffering skeleton

&nbsp;  - transparent masku (ako treba)

&nbsp;  - EMF primer (ako postoji metafajl)



3\) Kopirati samo potrebne delove u View.cpp novog zadatka

&nbsp;  i prilagoditi:

&nbsp;  - imena resursa

&nbsp;  - numericke vrednosti (pivot, offset, opsezi uglova)

&nbsp;  - redosled crtanja



------------------------------------------------------------

STA OVAJ PROJEKAT NIJE

------------------------------------------------------------



\- nije gotovo resenje zadatka

\- nije zamena za citanje PDF-a

\- ne treba ga koristiti “copy/paste u celini”

\- sluzi iskljucivo kao toolbox i referenca



------------------------------------------------------------

NAPOMENA

------------------------------------------------------------



Ako konkretan template nema DImage::DrawTransparent,

transparentno crtanje se resava maskom

(BitBlt + SRCAND/SRCPAINT) sa color-key = piksel (0,0).



Ovaj projekat je pomocni kontekst za analizu i implementaciju zadataka.



