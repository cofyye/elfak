Ovaj projekat NIJE resenje konkretnog zadatka.

Ovo je “toolbox” / puskica sa sablon-kodom za najcesce ponavljane delove

zadataka iz Racunarske grafike (GDI/MFC).



Namena:

\- da sluzi kao pomocni kontekst (primeri i sabloni),

\- koji se koristi prilikom resavanja NOVIH zadataka,

\- kada je potrebno brzo napisati standardni GDI/MFC kod u View fajlu.



============================================================

STA OVAJ PROJEKAT SADRZI

============================================================



Ovaj paket sadrzi kratke, proverene blokove koda koji se

ponavljaju kroz vecinu kolokvijumskih zadataka:



1\) Svetske transformacije (World Transform)

&nbsp;  - LoadIdentity

&nbsp;  - Translate

&nbsp;  - Rotate

&nbsp;  - Scale

&nbsp;  - Mirror (Scale(-1,1))

&nbsp;  - rightMultiply / leftMultiply (ModifyWorldTransform)



2\) Eliminacija flicker-a (double buffering)

&nbsp;  - iscrtavanje u memorijskom DC-ju

&nbsp;  - BitBlt iz memDC u screen DC

&nbsp;  - OnEraseBkgnd -> return TRUE

&nbsp;  - SetGraphicsMode(GM\_ADVANCED)



3\) Tastatura (OnKeyDown)

&nbsp;  - menjanje uglova

&nbsp;  - clamp (min/max)

&nbsp;  - koraci od 5 ili 10 stepeni

&nbsp;  - Invalidate() nakon izmene stanja



4\) Ucitavanje resursa

&nbsp;  - DImage (PNG/JPG/BMP)

&nbsp;  - EMF metafajlovi (GetEnhMetaFile, PlayEnhMetaFile, header.rclBounds)



5\) Osnovni GDI primeri

&nbsp;  - CPen / CBrush

&nbsp;  - boje, linije, fill



============================================================

STA OVAJ PROJEKAT NE SADRZI

============================================================



\- NEMA kompletnu logiku nekog konkretnog zadatka

\- NEMA hijerarhiju delova (to dolazi iz konkretnih projekata)

\- NEMA “gotovu scenu”

\- NEMA DImage::DrawTransparent



Ako se u PDF-u trazi transparentno crtanje:

\- a DImage nema DrawTransparent,

\- koristi se rucna maska (BitBlt + SRCAND/SRCPAINT),

&nbsp; sto je standardna zamena u studentskim resenjima.



============================================================

KADA TREBA KORISTITI OVAJ PROJEKAT

============================================================



Ovaj toolbox treba koristiti kada u PDF-u novog zadatka

prepoznas jednu ili vise sledecih stvari:



\- “Translate / Rotate / Scale”

\- “ModifyWorldTransform”

\- “rightMultiply / leftMultiply”

\- “GM\_ADVANCED”

\- “eliminisati flicker”

\- “iscrtavanje u memorijskom DC-ju”

\- “OnKeyDown kontrole (ugao, ogranicenja)”

\- “EMF metafajl”

\- “svetske transformacije”



Tada se:

\- blokovi iz ovog projekta koriste kao osnova,

\- i kopiraju direktno u \*View.cpp fajl\* novog zadatka,

\- a zatim se prilagodjavaju zahtevu zadatka.



============================================================

ORGANIZACIJA FAJLOVA

============================================================



\- puskica gdi.txt

&nbsp; - copy/paste blokovi koda

&nbsp; - transformacije, flicker, OnKeyDown, asset loading



\- puskica gdi.pdf

&nbsp; - ista puskica u PDF formatu (za brzo gledanje)



\- uradjeni blanketi.pdf

&nbsp; - primeri popunjenih sablona (korisno za vezbu ili proveru)



============================================================

BITNE NAPOMENE

============================================================



\- U Rotate funkcijama voditi racuna da li je ugao u stepenima ili radijanima.

&nbsp; Ako PDF koristi stepene:

&nbsp;   angleRad = angleDeg \* pi / 180



\- Ovaj projekat je pomocni kontekst.

&nbsp; Ne koristi se kao “reference po imenu”,

&nbsp; vec kao izvor standardnih GDI/MFC obrazaca.



============================================================

NAMENA

============================================================



\- Brzi copy/paste tokom rada na zadacima

\- Podsetnik na standardni GDI/MFC kod

