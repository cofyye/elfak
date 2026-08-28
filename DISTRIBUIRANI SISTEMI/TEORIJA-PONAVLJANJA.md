# Distribuirani sistemi — Teorija koja se ponavlja

### Pitanja sa blanketa, poređana od najčešćih (5/5) ka najređim (1/5)

**Obuhvaćeni rokovi (5):**

| Oznaka | Rok | Program |
|---|---|---|
| `2024` | 10.06.2024. | stari |
| `25-SP` | 20.10.2025. | SP (stari program) |
| `25-NP` | 27.10.2025. | NP (novi program) |
| `26-JUN` | 30.06.2026. | NP |
| `26-JUL` | 20.07.2026. | NP |

> **Napomena o fajlovima:** `blanketi/2025/... - 2025 apr - pu.pdf` je zapravo rok od **20.10.2025 (SP)**, a `... okt2 ...` je **27.10.2025 (NP)**. Ime fajla je pogrešno.

**Izvori odgovora** (sve je izvučeno isključivo iz materijala u ovom folderu):
`[U]` = `usmeni.pdf` · `[S]` = `DS BELO ZLATO skripta.pdf` · `[P1]` = `PREZENTACIJE/…za sajt1.pdf` · `[P2]` = `PREZENTACIJE/…za sajt2.pdf` · `[UV]` = `…uvod 2024 za sajt2.pdf` · `[H]` = `GFS i HDFS 2024.pdf`

**Legenda:**

- ✅ = tačan odgovor kod pitanja na zaokruživanje
- ❌ = netačan ponuđeni odgovor
- 🔁 = pitanje se doslovno ponavlja na više rokova
- ⚠️ = formulacija dozvoljava više odgovora — objašnjeno uz odgovor

---

# 🔴 UČESTALOST 5/5 — na SVAKOM roku

---

## TEMA 1 · Modeli konzistencije i skladišta podataka

**Na svih 5 rokova** (2024·6 · 25-SP·5 · 25-NP·9,10 · 26-JUN·13 · 26-JUL·12)
**Najisplativija tema na celom ispitu — uvek 1–2 pitanja.**

---

### P1. Definisati: a) striktnu; b) sekvencijalnu; c) uslovnu (kauzalnu); d) FIFO konzistenciju

`25-NP · 9` 🔁 *(primenjuje se i u 26-JUN·13, 26-JUL·12)*

> **Odgovor** `[U]` `[S·55,61]`
>
> **a) Striktna** — najjači model, bazira se na postojanju globalnog časovnika.
> *Skladište podataka je striktno konzistentno ako bilo koja operacija nad podatkom X vraća rezultat poslednje write operacije nad tim podatkom X.*
> Nije je moguće postići u DS: zasniva se na apsolutnom globalnom vremenu — ažuriranje bi moralo da se prostire ~10 puta brže od brzine svetlosti.
>
> **b) Sekvencijalna** — slabija od striktne, ali **najjači model koji se može postići u DS**.
> *Rezultat bilo kog izvršenja je isti kao da su (read i write) operacije svih procesa na skladištu podataka izvršene u nekom sekvencijalnom redosledu, i operacije svakog pojedinačnog procesa pojavljuju se u toj sekvenci u redosledu koji je određen njihovim programom.*
> ➜ Programski raspored svakog procesa mora biti ispoštovan; **svi procesi vide isti redosled**.
>
> **c) Uslovna (kauzalna)** — slabija od sekvencijalne.
> *Upisi koji su potencijalno uslovljeni moraju da se vide u svim procesima u istom redosledu. Konkurentni upisi se mogu videti u različitom redosledu u različitim procesima.*
> Implementira se pomoću **vektorskih časovnika**.
>
> **d) FIFO** — slabija od kauzalne.
> *Upisi koje obavi jedan proces vide se od strane drugih procesa po redosledu po kome su izdati. Upisi različitih procesa mogu se videti u različitom redosledu u različitim procesima.*
> Implementacija: svaki proces poruci ažuriranja dodaje process-id + redni broj.
>
> **Hijerarhija:** striktna ⊃ sekvencijalna ⊃ kauzalna ⊃ FIFO (svaki slabiji model je sadržan u jačem).

---

### P2. Tri procesa P1, P2 i P3 izvršavaju instrukcije nad tri deljive promenljive x, y i z. Postoje dve replike R1 i R2 u kojima se pamte promenljive x, y i z. Promenljive su inicijalizovane na nulu. Da li je ovakvo skladište podataka sekvencijalno konzistentno? Obrazložiti odgovor.

`2024 · 6`

```
   P1            P2            P3
   x=1           y=1           z=1
   print(y,z)    print(x,z)    print(x,y)

Operacije na replikama R1 i R2 se izvode u sledećem redosledu:

   R1                    R2
   x=1                   x=1
   print(y,z)            y=1
   y=1                   print(x,z)
   print(x,z)            print(y,z)
   z=1                   z=1
   print(x,y)            print(x,y)
```

> **Odgovor** `[U]`
>
> **NE — nije sekvencijalno konzistentno.**
>
> Sekvencijalna konzistencija zahteva da **sve replike vide JEDAN jedinstveni redosled svih operacija**, uz poštovanje programskog redosleda unutar svakog procesa.
>
> - R1: `x=1, print(y,z), y=1, print(x,z), z=1, print(x,y)`
> - R2: `x=1, y=1, print(x,z), print(y,z), z=1, print(x,y)`
>
> Redosledi **nisu isti** — u R1 se `print(y,z)` izvršava *pre* `y=1`, a u R2 *posle* `y=1`. Zato bi ista print naredba dala različit rezultat po replikama (R1: `y=0`, R2: `y=1`). Programski raspored jeste ispoštovan u obe replike, ali to nije dovoljno — **mora postojati jedan redosled zajednički za sve replike.**

---

### P3. Na Slici 1a) prikazana su tri konkurentna procesa. Inicijalne vrednosti promenljivih x, y i z su 0. Za svaki od dole navedenih rezultata štampanja odgovoriti i objasniti da li je moguć pod uslovom da je skladište podataka sekvencijalno konzistentno.

`25-SP · 5a`

```
   P1        P2        P3
   x=1       z=y       print(x,y,z)
   y=1
```

`a) x = 0, y = 1, z = 1`  `b) x = 1, y = 1, z = 0`  `c) x = 1, y = 0, z = 1`

> **Odgovor**
>
> **a) x=0, y=1, z=1 → ❌ NIJE MOGUĆE**
> `x=0` ⇒ `print` u P3 se izvršio **pre** `x=1` u P1. `y=1` ⇒ `y=1` (P1) se izvršilo **pre** `print`-a.
> Programski redosled u P1 je `x=1` pa `y=1`, dakle: `x=1 < y=1 < print < x=1` → **ciklus**.
>
> **b) x=1, y=1, z=0 → ✅ MOGUĆE**
> Validan redosled: `z=y` (P2 čita y=0 ⇒ z=0) → `x=1` → `y=1` → `print(1,1,0)`.
>
> **c) x=1, y=0, z=1 → ❌ NIJE MOGUĆE**
> `z=1` ⇒ P2 je izvršio `z=y` **posle** `y=1`, dakle `y=1 < z=y < print`.
> Ali `print` daje `y=0`, što znači `print < y=1` → **ciklus**.

---

### P4. Na Slici 1b) su prikazana dva konkurentna procesa. Inicijalne vrednosti promenljivih x i y su 0. Da li postoji model konzistencije kod koga je moguće da proces P1 štampa x = 1, y = 0, a proces P2 x = 0, y = 1? Obrazložiti odgovor.

`25-SP · 5b`

```
   P1            P2
   x=1           y=1
   print(x,y)    print(x,y)
```

> **Odgovor** `[U]`
>
> **DA — kod uslovne (kauzalne) konzistencije**, i kod svakog slabijeg modela (FIFO).
>
> Upisi `W(x)1` i `W(y)1` su **konkurentni** — nijedan proces nije pročitao vrednost onog drugog pre svog upisa, pa nema potencijalne uslovljenosti. Definicija kauzalne konzistencije: *„Konkurentni upisi se mogu videti u različitom redosledu u različitim procesima."* P1 vidi prvo svoj upis, P2 prvo svoj — dozvoljeno.
>
> **Kod sekvencijalne NIJE moguće**: zahtevalo bi `W(x) < P1.print < W(y) < P2.print < W(x)` — ciklus.

---

### P5. Na slici 2 prikazana su tri konkurentna procesa. Inicijalne vrednosti promenljivih x, y i z su 0. Ako proces P1 generiše izlaz 10, proces P2 izlaz 11, a proces P3 izlaz 01, da li je dobijeni rezultat korektan sa stanovišta sekvencijalne konzistencije? Objasniti zašto.

`25-NP · 10`

```
   Process P1        Process P2        Process P3
   x = 1;            y = 1;            z = 1;
   print(y, z);      print(x, z);      print(x, y);
```

> **Odgovor**
>
> **NE — nije korektno.**
>
> Raspis: P1 → `y=1, z=0`; P2 → `x=1, z=1`; P3 → `x=0, y=1`.
>
> 1. P3 vidi `x=0` ⇒ `P3.print` je **pre** `x=1` iz P1
> 2. Programski red P3: `z=1` je **pre** `P3.print`
> 3. Programski red P1: `x=1` je **pre** `P1.print`
>
> Iz 1+2+3: `z=1 < P3.print < x=1 < P1.print`.
> Ali P1 ispisuje `z=0`, što znači `P1.print < z=1` → **ciklus**. ∎
>
> 💡 Uporedi sa `[S·45]`: nad **istim** procesima kombinacija P1=00, P2=11, P3=10 **jeste** korektna. Dakle nije stvar procesa nego konkretnih ispisa — uvek proveri traženjem ciklusa.

---

### P6. Inicijalne vrednosti deljivih promenljivih u repliciranom skladištu podataka su x = y = 0. Aktivnosti procesa P1 i P2 su: P1: W(x)=1 R(y)=0 ; P2: W(y)=1 R(x)=0. Da li je ovakvo ponašanje dozvoljeno u slučaju sekvencijalne konzistencije? Objasniti.

`26-JUN · 13a`

> **Odgovor**
>
> **NE — nije dozvoljeno.**
>
> - `R(y)=0` u P1 ⇒ `P1.R(y)` je pre `W(y)1`
> - `R(x)=0` u P2 ⇒ `P2.R(x)` je pre `W(x)1`
> - Programski red: `W(x)1 < P1.R(y)` i `W(y)1 < P2.R(x)`
>
> Spajanjem: `W(x)1 < P1.R(y) < W(y)1 < P2.R(x) < W(x)1` → **ciklus**. Ne postoji jedinstveni sekvencijalni redosled. ∎
>
> *(Ovo je ista situacija kao P4, samo obrnuto postavljena.)*

---

### P7. Inicijalno x = 0. Procesi P1 i P2 se izvršavaju konkurentno: P1: W(x)=1 ; P2: W(x)=2. Kasnije procesi P3 i P4 čitaju x: P3 reads: 1 zatim 2 ; P4 reads: 2 zatim 1. Koji model konzistencije ovo dozvoljava?

`26-JUN · 13b`

> **Odgovor** `[U]` `[S·55b]`
>
> **Uslovna (kauzalna) konzistencija** — i svaki slabiji model (FIFO).
>
> `W(x)1` i `W(x)2` su **konkurentni upisi** (P2 nije čitao x pre svog upisa ⇒ nema uslovljenosti). Definicija: *„Konkurentni upisi se mogu videti u različitom redosledu u različitim procesima."*
>
> | Model | Dozvoljava? | Zašto |
> |---|---|---|
> | Striktna | ❌ | svi bi odmah morali videti poslednji upis |
> | Sekvencijalna | ❌ | svi procesi moraju videti **isti** redosled |
> | Kauzalna | ✅ | upisi su konkurentni |
> | FIFO | ✅ | upisi dolaze iz različitih procesa |

---

### P8. Da li je sledeće skladište podataka kauzalno konzistentno? Obrazložiti odgovor.

`26-JUL · 12A`

```
   P1:  W(x)=1
   P2:  R(x)=1  →  W(y)=1
   P3:  R(y)=1  →  W(z)=1
   P4:  R(z)=1  →  R(x)=0
```

> **Odgovor**
>
> **NE — nije kauzalno konzistentno.**
>
> Lanac potencijalne uslovljenosti (relacija je **tranzitivna**):
> `W(x)=1` → P2 čita x=1 → `W(y)=1` → P3 čita y=1 → `W(z)=1` → P4 čita z=1.
>
> Dakle `W(x)=1` **kauzalno prethodi** upisu `W(z)=1`. Po definiciji: *„Upisi koji su potencijalno uslovljeni moraju da se vide u svim procesima u istom redosledu."* Pošto je P4 već video `W(z)=1`, **mora** videti i `W(x)=1`.
>
> P4 međutim čita `R(x)=0` — vidi staru vrednost ⇒ **kauzalnost je narušena.** ∎

---

### P9. Da li ovakvo skladište podataka može biti sekvencijalno konzistentno? Ako može, koje vrednosti treba da vrate R(y)=? i R(x)=? Ako ne može, pokazati zašto ne može.

`26-JUL · 12B`

```
   P1:  W(x)=1  →  R(y)=?
   P2:  W(y)=1  →  R(x)=?
   P3:  R(x)=1  →  R(y)=0
```

> **Odgovor**
>
> **DA, može.** → **R(y) = 0** (u P1) i **R(x) = 1** (u P2)
>
> Validan sekvencijalni redosled:
>
> | # | Operacija | Proces | Provera |
> |---|---|---|---|
> | 1 | `W(x)=1` | P1 | |
> | 2 | `R(x)=1` | P3 | ✓ x je 1 |
> | 3 | `R(y)=0` | P3 | ✓ y je još 0 |
> | 4 | `R(y)=0` | P1 | ✓ prog. red P1: W(x) pre R(y) |
> | 5 | `W(y)=1` | P2 | |
> | 6 | `R(x)=1` | P2 | ✓ prog. red P2: W(y) pre R(x) |
>
> **Zašto je R(x)=1 prinudno:** P3 čita `x=1` ⇒ `W(x)1 < P3.R(x)`. P3 čita `y=0` ⇒ `P3.R(y) < W(y)1`. Prog. red P2: `W(y)1 < P2.R(x)`.
> Lanac: `W(x)1 < P3.R(x) < P3.R(y) < W(y)1 < P2.R(x)` ⇒ `P2.R(x)` je **posle** `W(x)1` ⇒ mora vratiti **1**.
>
> ⚠️ **R(y) u P1 može biti i 0 i 1** — obe vrednosti dopuštaju validan redosled (za `R(y)=1` samo pomeriš `P1.R(y)` iza `W(y)=1`). Na ispitu piši **R(y)=0, R(x)=1**, ali spomeni da je R(y)=1 takođe dopušteno.

---

## TEMA 2 · RPC — Sun RPC, DCE RPC, stubovi, XDR, semantike

**Na svih 5 rokova** (2024·3 · 25-SP·7,10 · 25-NP·4,5 · 26-JUN·2,3 · 26-JUL·2,3)
**Najveći blok pitanja po roku — obično 2–3 pitanja.**

---

### P10. Izlaz iz Sun IDL kompajlera sastoji se od više fajlova. Koji su to fajlovi i šta sadrže? Napisati na koji način se vrši generisanje ovih fajlova i kako se na osnovu generisanih fajlova formiraju izvršna klijentska i serverska aplikacija. Za svaki korak napisati odgovarajuću komandu.

`2024 · 3a` 🔁 *(`[S·16]`, `[S·70]` isto pitanje za DCE)*

> **Odgovor** `[U]` `[P1]` `[S·16]`
>
> **Poziv kompajlera:** `rpcgen -C primer.x`
>
> Generišu se **3 fajla**:
>
> | Fajl | Sadržaj |
> |---|---|
> | `primer.h` (**header**) | jedinstveni identifikator interfejsa, definicije tipova, konstanti i prototipova funkcija. Uključuje se sa `#include` **i u klijent i u server** kod |
> | `primer_clnt.c` (**klijent stub**) | procedure koje će klijent program pozivati; pakuju parametre u poruke, šalju poruku, primaju poruku, izvlače rezultat i prosleđuju ga klijentu |
> | `primer_svc.c` (**server stub**) | procedure koje se pozivaju kada poruka stigne do servera i koje zatim pozivaju odgovarajuću serversku proceduru |
>
> **Koraci i komande:**
>
> ```bash
> rpcgen -C primer.x                    # generiše primer.h, primer_clnt.c, primer_svc.c
> rpcgen -C -Ss primer.x > server.c     # generiše template serverskog koda
> # ručno se dopiše telo procedura u server.c i napiše client.c
> cc -o client client.c primer_clnt.c   # izvršna klijentska aplikacija
> cc -o server server.c primer_svc.c    # izvršna serverska aplikacija
> ```
>
> Pored generisanih fajlova pišu se i **klijent program** (`client.c` — uspostavlja vezu sa serverom preko `clnt_create()` na osnovu imena servera, broja programa i broja verzije) i **server program** (`server.c` — sadrži implementaciju procedura).
>
> **Konvencije imenovanja:** imena programa, verzije i procedura u IDL fajlu pišu se **VELIKIM SLOVIMA**. U klijentu se procedura poziva kao `imeprocedure_brojverzije`, a na serveru se implementira kao `imeprocedure_brojverzije_svc`.

---

### P11. Šta može biti argument serverske procedure kod Sun RPC, a šta povratna vrednost u slučaju SunRPC? Kako mora biti deklarisan rezultat serverske procedure i zašto?

`25-NP · 5a`

> **Odgovor** `[U]` `[P1]`
>
> - Sun RPC poziv udaljene procedure može imati **samo JEDAN ulazni argument i JEDAN izlazni parametar**. Ako treba preneti više parametara ili rezultata, to se postiže **preko struktura** (ili nizova).
> - Procedure na serveru **uzimaju kao parametar pokazivač** na podatak koji se prenosi i **vraćaju pokazivač** na rezultat.
> - **Rezultat mora biti deklarisan kao `static`.** Razlog: procedura vraća **pokazivač** na rezultat; ako bi promenljiva bila lokalna (automatska), ona bi bila uništena po izlasku iz funkcije i pokazivač bi pokazivao na nevažeću memoriju. `static` obezbeđuje da promenljiva postoji i nakon povratka iz procedure, pa server stub može da je spakuje u poruku.
>
> ```c
> int *saberi_1_svc(operandi *a, struct svc_req *rqstp)
> {
>     static int zbir;          /* MORA static */
>     zbir = a->x + a->y;
>     return &zbir;             /* vraća pokazivač */
> }
> ```

---

### P12. Napisati Sun RPC definiciju interfejsa … Prikazati poziv funkcije u klijentskoj aplikaciji.

🔁 **Traži se na 3 od 5 rokova** — samo se menja zadatak:

**(a)** `25-NP · 5b` — pristup udaljenoj proceduri koja prima podatke o transakciji (identifikator transakcije — ceo broj, iznos — realan broj). Pretpostavlja se valuta USD. Procedura kao rezultat vraća niz realnih brojeva: originalni iznos, iznos u evrima i iznos u funtama.
**(b)** `26-JUN · 2b` — sistem elektronskog glasanja koji omogućava registrovanim biračima da glasaju za kandidate i pregledaju rang-listu kandidata. Server treba da implementira metodu **vote**, kojom klijent prosleđuje jedinstveni broj birača, identifikator kandidata i vreme glasanja, pri čemu server proverava da li je birač već glasao i, ukoliko je glasanje validno, evidentira glas vraćajući informaciju o uspešnosti operacije. Takođe implementirati metodu **getTopCandidates**, koja vraća listu identifikatora kandidata sortiranu opadajuće prema broju osvojenih glasova.
**(c)** `26-JUL · 3` — procedura koja na osnovu skupa izmerenih temperatura pronalazi minimalnu i maksimalnu izmerenu temperaturu. Napisati kako bi izgledao poziv udaljene procedure u klijentskoj aplikaciji. Navesti potpis odgovarajuće procedure u serverskoj aplikaciji.

> **Odgovor — obrazac koji radi za sve tri varijante** `[U]` `[P1]`
>
> **Struktura `.x` fajla:**
>
> ```c
> /* 1. deo: XDR definicije konstanti i tipova */
> const MAXTEMP = 100;
>
> struct temp_data {
>     float temps<MAXTEMP>;      /* niz promenljive veličine */
> };
>
> struct min_max {
>     float min;
>     float max;
> };
>
> /* 2. deo: definicija programa/verzije/procedura */
> program TEMP_PROG {
>     version TEMP_VERS {
>         min_max FIND_MIN_MAX(temp_data) = 1;   /* broj procedure */
>     } = 1;                                     /* broj verzije */
> } = 0x20345678;                                /* broj programa, 32 bita */
> ```
>
> **Poziv u klijentskoj aplikaciji:**
>
> ```c
> CLIENT *cln;
> temp_data td;
> min_max *rez;
>
> cln = clnt_create(server, TEMP_PROG, TEMP_VERS, "udp");
> if (cln == NULL) exit(2);
>
> td.temps.temps_len = n;
> td.temps.temps_val = niz;
>
> rez = find_min_max_1(&td, cln);        /* malim slovima + _brojverzije */
> if (rez == NULL) exit(3);
> printf("min=%f max=%f\n", rez->min, rez->max);
> clnt_destroy(cln);
> ```
>
> **Potpis procedure u serverskoj aplikaciji:**
>
> ```c
> min_max *find_min_max_1_svc(temp_data *argp, struct svc_req *rqstp);
> ```
>
> **Pravila koja se moraju poštovati:**
> - Imena programa/verzije/procedure u `.x` fajlu — **VELIKIM SLOVIMA**
> - Svaka procedura ima **tačno jedan ulazni i jedan izlazni parametar** → više podataka se pakuje u `struct`
> - U klijentu: `imeprocedure_brojverzije(&arg, cln)`; na serveru: `imeprocedure_brojverzije_svc(...)`
> - Broj programa je 32-bitni hex; korisnički opseg je `0x20000000–0x3fffffff` `[P1]`
> - Za varijantu (a) i (b) povratna vrednost je **niz/lista** → definiše se kao niz promenljive veličine (vidi **P16**)

---

### P13. Objasniti aktivnosti koje obavljaju klijent stub i server stub prilikom poziva udaljene procedure.

`26-JUN · 2a` 🔁 *(`[S·69]` — „Pobrojati redom aktivnosti prilikom poziva udaljene procedure")*

> **Odgovor** `[U]` `[S·69]`
>
> **Klijent stub** — funkcija koja liči na lokalnu funkciju, ali **ne sadrži kod procedure**, već kod za slanje parametara i prijem rezultata kroz mrežu:
> - pakuje parametre u mrežnu poruku (**marshaling**), uz konverziju iz lokalnog u **standardni format podataka**
> - poziva lokalni OS (SEND, pa RECEIVE) i čeka odgovor
> - raspakuje rezultat iz poruke i vraća ga klijentskom procesu
>
> **Server stub:**
> - prima poruku od lokalnog OS-a (RECEIVE), raspakuje je (**unmarshaling**) i izvlači argumente
> - poziva željenu serversku proceduru i predaje joj parametre (stavlja ih na stek)
> - pakuje rezultat u poruku i poziva lokalni OS (SEND)
>
> **Svih 10 koraka poziva RPC-a:**
> 1. Klijent poziva lokalnu proceduru (klijent stub), parametri se smeštaju u stek
> 2. Klijent stub pakuje argumente, gradi mrežnu poruku, poziva lokalni OS (SEND pa RECEIVE)
> 3. Lokalni OS šalje poruku udaljenom OS-u (UDP ili TCP)
> 4. Poruka stiže do servera; OS je prosleđuje serverskom stubu, koji je raspakuje i izvlači argumente
> 5. Serverski stub poziva serversku proceduru i predaje joj parametre
> 6. Server izvršava proceduru i vraća rezultat serverskom stubu
> 7. Serverski stub pakuje rezultat u poruku i poziva lokalni OS (SEND, pa RECEIVE za sledeće pozive)
> 8. Serverski OS šalje poruku klijentskom OS-u
> 9. Lokalni OS prosleđuje poruku klijent stubu
> 10. Klijent stub izvlači rezultat iz poruke i vraća ga klijentskom procesu

---

### P14. Koji su načini prenosa parametara kod poziva udaljene procedure? Objasniti.

`25-SP · 10a`

> **Odgovor** `[U]`
>
> - **call-by-value (po vrednosti)** — vrednosti parametara se kopiraju u stek; ako pozvana procedura modifikuje podatke, originalni podaci se **ne** menjaju
> - **call-by-reference (po referenci)** — adrese parametara se smeštaju u stek; modifikacije se odražavaju na original
> - **call-by-copy/restore** — vrednosti se stavljaju na stek kao kod prenosa po vrednosti, a **nakon završetka poziva** modifikovane vrednosti se upisuju preko originalnih (efekat kao kod prenosa po referenci)
>
> ➜ **Kod RPC-a se koristi copy/restore.**
>
> **Zašto ne po referenci:** nema smisla proslediti adresu udaljenoj mašini (drugi adresni prostor). Da bi se postigao efekat prenosa po referenci, klijent stub **kopira podatke na koje pokazuje pointer u poruku** i pošalje je serveru; serverski stub koristi kopiju u adresnom prostoru servera i može pozvati serversku proceduru sa pointerom na taj podatak. `[P1]`

---

### P15. Kako se prenose skalarni parametri kod RPC? Šta predstavlja standardni format podataka i zašto je neophodan? Navesti jedan njegov primer. Ko obavlja konverziju podataka kod RPC?

`26-JUL · 2a`

> **Odgovor** `[P1]` `[U]`
>
> - **Skalarni parametri se prenose po vrednosti** (u C-u se skalarni tipovi prenose po vrednosti, a polja po referenci). Dok su klijent i server ista mašina i parametri skalarni, ovo funkcioniše bez problema.
> - **Zašto je standardni format neophodan:** u DS je uobičajeno da postoji **više tipova mašina**. Svaka mašina može imati različit način predstavljanja **brojeva** (big-endian, little-endian, dvojični ili jedinični komplement) i **karaktera** (ASCII, EBCDIC). Zato nije moguće preneti parametre onako kako su zapisani lokalno.
> - **Rešenje:** koristi se **„standardno" kodiranje svih tipova podataka** koji se mogu prenositi kao parametri.
> - **Primer:** **XDR** (eXternal Data Representation) kod Sun RPC-a.
> - **Ko obavlja konverziju:** **klijentski i serverski stub** — oni obavljaju konverzije iz lokalnog u standardni način predstavljanja podataka i obrnuto.

---

### P16. Navesti gde se deklarišu promenljive čiji su tipovi definisani u XDR-u. Kako se definiše niz promenljive veličine? Kako se predstavljaju nizovi promenljive veličine u generisanom C kodu?

`26-JUL · 2b`

> **Odgovor** `[P1]`
>
> **Gde se deklarišu:** RPC definicije tipova se kompajliraju i nalaze u **izlaznom header fajlu** (`primer.h`). Definicija **ne alocira memorijski prostor** — to su samo definicije; promenljive se tek naknadno deklarišu (u klijent/server kodu, koji uključuje header). **Deklaracije ne mogu stajati samostalno u `.x` fajlu, već moraju biti deo strukture ili u okviru `typedef`.**
>
> **Niz fiksne veličine:**
> ```c
> colortype palette[8];        /* --> colortype palette[8]; */
> ```
>
> **Niz promenljive veličine — uglaste zagrade `< >`:**
> ```c
> int  heights<50>;            /* max 50 vrednosti */
> long x_vals<>;               /* max 2^32 - 1 vrednosti */
> ```
>
> **Predstavljanje u generisanom C kodu — struktura sa dva polja:**
> ```c
> typedef int heights<50>;
> /* --> */
> struct {
>     int  heights_len;    /* broj elemenata niza */
>     int *heights_val;    /* pokazivač na prvi element niza */
> } heights;
> ```
>
> **Kompletan primer** (interfejs za prosečnu vrednost niza) `[P1]`:
> ```c
> const MAXAVGSIZE = 200;
> struct input_data {
>     double input_data<MAXAVGSIZE>;
> };
> program AVERAGEPROG {
>     version AVERAGEVERS {
>         double AVERAGE(input_data) = 1;
>     } = 1;
> } = 0x29999999;
> ```
> Pristup na serveru: `input->input_data.input_data_len` i `input->input_data.input_data_val`.

---

### P17. Kako se može locirati server koji implementira udaljenu proceduru?

`25-NP · 4` 🔁 *(`[S·30]`)*

> **Odgovor** `[U]` `[S·30]`
>
> Postoje **2 tipa povezivanja (binding)**:
>
> **1. Statičko povezivanje**
> Klijent zna koji server treba da kontaktira i **adresa servera je u klijent stubu**. Kada klijent pozove proceduru, klijent stub samo prosledi poziv serveru čiju adresu ima. Poseban program **PORTMAPPER** na udaljenom serveru pamti preslikavanja **imena programa, broja verzije i broja porta** (portmapper radi na **portu 111**).
> ➜ *Mana:* ako server promeni adresu, mora se rekompajlirati klijent stub.
>
> **2. Dinamičko povezivanje**
> Postoji **centralizovana baza podataka smeštena u Name i Directory serverima** koja može locirati server koji obezbeđuje željeni servis. Name/Directory serveri vraćaju adresu traženog servera na osnovu **potpisa procedure** koja se poziva.
> Tok: klijent pozove udaljenu proceduru → klijent stub kontaktira **Name server** da dobije adresu servera → Name server šalje adresu → klijent stub uspostavlja vezu sa serverom.
> **Serverski stub registruje serversku proceduru u Name i Directory serveru prilikom startovanja servera.** Ako server promeni adresu, **dovoljno je promeniti ulaz u Name serveru.**

---

### P18. Šta se dešava kada server promeni mrežnu adresu u sistemu sa dinamičkim povezivanjem? Obrazložiti.

`25-SP · 10b` — **pitanje na zaokruživanje**

- ❌ **a.** Klijent mora ponovo da se kompajlira
- ✅ **b.** Server se ponovo registruje kod bindera sa novom adresom
- ❌ **c.** Binder obaveštava sve aktivne klijente o promeni
- ✅ **d.** Klijent može da dobije novu adresu pri sledećem pozivu

> **Odgovor** `[U]` ⚠️ **Tačna su i b) i d)** — opisuju dve strane istog mehanizma. Ako se traži samo jedan odgovor, **d)** opisuje ono što se dešava iz ugla klijenta (što pitanje i sugeriše), a **b)** ono što radi server.
>
> Obrazloženje iz materijala: *„Serverski stub registruje serversku proceduru u Name i Directory serveru prilikom startovanja servera. Ako server koji implementira željenu proceduru promeni adresu, dovoljno je promeniti ulaz u Name serveru."* Pošto klijent stub **pri svakom pozivu** kontaktira Name server da dobije adresu, klijent dobija novu adresu pri sledećem pozivu.
>
> - **a) netačno** — rekompajliranje je potrebno samo kod **statičkog** povezivanja (adresa je zapisana u klijent stubu)
> - **c) netačno** — binder je **pasivan**; ne obaveštava klijente, oni ga sami pitaju (pull, ne push)

---

### P19. U distribuiranom sistemu klijent koristi RPC za komunikaciju sa serverom. Klijent šalje zahtev serveru (npr. operation x()) sve dok ne dobije odgovor od servera. a) Koja RPC semantika u slučaju greške je u ovom slučaju implementirana? b) Objasniti kako mora da bude implementiran server da bi se ovakva semantika obezbedila. Navesti bar dve mogućnosti.

`25-SP · 7` 🔁 *(`[S·10]` — doslovno isto pitanje)*

> **Odgovor** `[U]` `[S·10]`
>
> **a) At-least-once („bar jednom") semantika.** Klijent šalje zahtev sve dok ne primi odgovor, dakle operacija će se izvršiti **najmanje jednom, a možda i više puta**.
>
> **b) Dve mogućnosti na strani servera:**
> 1. **Da procedura `operation x()` bude idempotentna** — da vraća uvek isti rezultat ma koliko puta se izvrši i da **ne menja stanje sistema** (npr. procedura koja vraća tačno vreme).
> 2. **Da server bude statefull** — da vodi računa o klijentskim zahtevima (redni brojevi zahteva ili bit za retransmisiju u zaglavlju), prepoznaje **duplikate** i u tom slučaju samo **retransmituje zapamćen odgovor**, bez ponovnog izvršavanja procedure.
>
> **Sve tri semantike** `[S·57]`:
> - **maybe (možda)** — nema nikakve garancije da će operacija biti izvršena; klijent ne zna da li je izvršena
> - **at-least-once** — izvršiće se bar jednom, a može i više puta (za idempotentne funkcije)
> - **at-most-once** — funkcija nije idempotentna, izvršiće se najviše jednom, a možda ni jednom (odmah se signalizira greška po isteku timeout-a)
> - *Idealno bi bilo **exactly once**, ali to se **ne može garantovati**.*

---

### P20. Koja semantika poziva udaljenih procedura je podržana kod DCE RPC i kako?

`2024 · 3b` 🔁 `26-JUN · 3b` *(„Koje semantike udaljenog poziva procedure podržava DCE RPC i na koji način su implementirane?")*

> **Odgovor** `[P1]` `[S·73]`
>
> DCE podržava **dve semantičke opcije**:
>
> 1. **„bar jednom" (at-least-once)** — koristi se ako su u pitanju **idempotentne** funkcije (mogu se izvršavati više puta bez posledica). **Takve procedure se moraju eksplicitno označiti kao `idempotent` u IDL-u.**
> 2. **„samo jednom" (at-most-once)** — koristi se kada funkcija **nije idempotentna** (poziv generiše efekte, npr. modifikuje podatke). **Ovo je podrazumevana semantika** — sve što nije označeno kao `idempotent`.
>
> **Kako je implementirano:** oznaka `[idempotent]` u definiciji procedure u IDL fajlu upravlja tim ponašanjem; runtime biblioteka na osnovu nje odlučuje da li sme da ponovi poziv posle timeout-a ili mora da signalizira grešku.

---

### P21. Koji tip povezivanja između klijenta i servera je podržan u DCE RPC-u? Opišite postupak povezivanja.

`26-JUN · 3a` 🔁 *(`[S·53]` — „Pobrojati redom korake kojima se ostvaruje povezivanje klijenta i servera kod DCE")*

> **Odgovor** `[U]` `[P1]` `[S·53]`
>
> **Tip povezivanja: DINAMIČKO** — aplikacije identifikuju resurse **po imenu**, bez potrebe da znaju gde je resurs lociran (za razliku od Sun RPC-a). Koristi se **CDS (Cell Directory Service)** — mehanizam koji omogućava korišćenje logičkih imena unutar DCE ćelije (grupa klijent-server mašina u okviru jednog LAN-a). CDS preslikava logičko ime u IP adresu servera. **GDS (Global Directory Service)** upravlja imenima **van** lokalne DCE ćelije; sprega između CDS i GDS je **GDA**.
>
> **DCE klijent pronalazi server u 2 koraka:** lociranje serverske mašine → lociranje odgovarajućeg procesa na toj mašini.
>
> **Postupak povezivanja (5 koraka):**
> 1. **Registrovanje broja porta** servera (procesa) u **DCE deamonu** (`rpcd`, koji ima poznati broj porta i održava tabelu parova `[server, brojPorta]`)
> 2. **Registrovanje servera u Directory serveru** (dostavlja adresu serverske mašine, ime servera i interfejse koje implementira)
> 3. **Klijent kontaktira Directory server** da dobije **IP adresu** serverske mašine
> 4. **Klijent kontaktira DCE deamon** da dobije **broj porta** server procesa kome želi da pristupi
> 5. **Poziv udaljene procedure**
>
> *(Registracija se obavlja pre nego što klijent pozove server.)*

---

### P22. Šta se dobija kao rezultat poziva uuidgen programa kod DCE RPC? Koji su naredni koraci u pisanju DCE klijent/server aplikacije?

`26-JUL · 3a`

> **Odgovor** `[U]` `[P1]`
>
> **Rezultat `uuidgen imefajla.idl`:** kreira se fajl `imefajla.idl` koji sadrži **globalno jedinstveni identifikator interfejsa (UUID) i broj verzije**, i garantuje da se isti ID neće nigde pojaviti u DS-u generisanom sa `uuidgen`. Kreira se prazan skelet interfejsa:
> ```c
> [ uuid(3d6ead56-06e3-11ca-8dd1-826901beabcd), version(1.0) ]
> interface INTERFACENAME
> {
> }
> ```
>
> **Naredni koraci:**
> 1. **Editovanje IDL fajla** — zameniti `INTERFACENAME` imenom interfejsa i deklarisati procedure (samo potpisi):
> ```c
> [ uuid(3d6ead56-06e3-11ca-8dd1-826901beabcd), version(1.0) ]
> interface math {
>     long get_sum([in] long first, [in] long second);
> }
> ```
> 2. **Poziv IDL kompajlera:** `idl primer.idl` → izlaz su **3 fajla**: header (`primer.h`), klijent stub (`primer_cstub.c`), server stub (`primer_sstub.c`)
> 3. **Pisanje klijent i server programa**, zatim kompajliranje i **linkovanje sa odgovarajućim stubom** da bi se dobio izvršni kod
>
> **DCE RPC programer u opštem slučaju piše 3 programa:** klijent kod, server inicijalizacijski kod (kojim se server registruje u Directory servisu) i server operativni kod.

---

### P23. Prilikom uspostavljanja DCE RPC komunikacije, koje tvrdnje su tačne?

`26-JUL · 3b` — **pitanje na zaokruživanje (više odgovora)**

- ✅ **A.** U DCE RPC sistemu klijent može koristiti CDS/Directory Server da pronađe odgovarajući server koji implementira traženi interfejs.
- ✅ **B.** Nakon pronalaženja servisa, komunikaciju između klijenta i servera dalje obavljaju RPC stubovi i runtime biblioteka.
- ❌ **C.** CDS i rpcbind čuvaju kôd udaljenih procedura koje treba izvršiti.
- ❌ **D.** Ako klijent ne pronađe servis u CDS-u ili rpcbind-u, RPC poziv se ipak izvršava jer klijentski stub sadrži kôd udaljene procedure.
- ✅ **E.** CDS koristi UUID interfejsa, dok Sun RPC koristi broj programa i verziju za identifikaciju servisa.

> **Odgovor:** **tačno je A, B, E**
>
> - **A ✅** — *„CDS služi da locira CDS server u kome se nalazi preslikavanje logičkog imena u IP adresu servera na kome je implementirana potrebna udaljena procedura."* `[U]`
> - **B ✅** — stubovi i runtime biblioteka obavljaju marshaling/unmarshaling i prenos
> - **C ❌** — CDS čuva **preslikavanje imena u adresu**, a rpcbind/DCE deamon **tabelu `[server, brojPorta]`**. Ni jedan ne čuva kôd procedura — kôd je isključivo u serverskoj aplikaciji.
> - **D ❌** — *„Klijent stub … stvarno ne sadrži kod koji treba da se izvrši u toj proceduri, već sadrži kod za slanje parametara i prijem rezultata kroz mrežu. Samo telo funkcije je i obavlja se na serveru."* `[U]` Ako se servis ne pronađe, emituje se **exception** (greška „klijent ne može da locira server").
> - **E ✅** — DCE identifikuje interfejs preko **UUID-a** (`uuidgen`), a Sun RPC preko **broja programa + broja verzije** (+ broja procedure).

---

## TEMA 3 · Razmena poruka — Message Queues, publish-subscribe, JMS

**Na svih 5 rokova** (2024·4b · 25-SP·11 · 25-NP·6 · 26-JUN·4b,5 · 26-JUL·5,6)

---

### P24. Interfejs koji je na raspolaganju aplikacijama za razmenu poruka kod Message queuing sistema. Objasniti.

`25-NP · 6` 🔁 `26-JUN · 4b` — **NAJPONAVLJANIJE PITANJE NA CELOM ISPITU (4×)**

> **Odgovor** `[P2]`
>
> Osnovni interfejs je **vrlo jednostavan i sadrži 4 operacije: `put`, `get`, `poll`, `notify`.**
> *(Adresiranje se obavlja obezbeđivanjem jedinstvenog imena odredišnog reda.)*
>
> | Operacija | Blokirajuća? | Šta radi |
> |---|---|---|
> | **`put`** | ne | Poziva je **pošiljalac**; prosleđuje poruku sistemu da bude dodata u specificirani red. |
> | **`get`** | **da** | Omogućava autorizovanom procesu da iz specificiranog reda pribavi **poruku koja je najduže u njemu**. Proces se **blokira ako je red prazan**. Varijacije omogućavaju traženje specifične poruke (pretraživanje po sadržaju). |
> | **`poll`** | ne | **Neblokirajuća varijanta `get`-a.** Ako je red prazan ili specifična poruka ne postoji, pozivajući proces **nastavlja sa izvršenjem** (ne blokira se). |
> | **`notify`** | ne | Omogućava procesu da **instalira handler** koji se automatski poziva kad god je poruka dodata u red. Može se iskoristiti i da automatski startuje proces koji će pribavljati poruke, ako se nijedan ne izvršava. Često se implementira kroz **demon na strani primaoca** koji neprekidno osluškuje dolazeće poruke. |
>
> **Dodatno:** aplikacija može stavljati poruke **samo u lokalni red**, i čitati samo iz lokalnog reda; redovima upravlja **upravljač redova (queue manager)**, zaseban proces. Politika upravljanja redom je uglavnom **FIFO**, ali većina implementacija podržava i **prioritete**.

---

### P25. Interfejs koji je na raspolaganju aplikacijama za razmenu poruka kod publish-subscribe sistema.

`25-SP · 11b`

> **Odgovor** `[P2]`
>
> Umesto pošiljaoca i primaoca imamo **Publisher (izdavač)** i **Subscriber (pretplatnik)**; umesto reda (queue) destinacija je **Topic (tema)**.
>
> | Operacija | Šta radi |
> |---|---|
> | **`publish(event)`** | Izdavači distribuiraju događaje u sistem. Događaj se emituje svim pretplatnicima koji su izrazili interesovanje za taj tip događaja. **Izdavači ne moraju znati identitete pojedinačnih pretplatnika.** |
> | **`subscribe(filter)`** | Pretplatnici izražavaju interesovanje za primanje određenih tipova događaja; specificira se **filter** koji definiše kriterijume (po tipu događaja, atributima itd.). |
> | **`notify(event)`** | Sistem se pobrine da svi pretplatnici čiji filteri odgovaraju događaju **dobiju obaveštenje** i isporuči im događaj. Ovaj mehanizam omogućava **odvojenu (decoupled) komunikaciju**. |
> | **`unsubscribe(filter)`** | Pretplatnik povlači interesovanje; sistem uklanja pretplatu i budući događaji mu se više ne isporučuju. |
>
> **Dva načina filtriranja:**
> - **Po temi (topic-based)** — izdavači klasifikuju događaje u teme (politika, sport, tehnologija…), pretplatnici biraju teme
> - **Po sadržaju (content-based)** — pretplatnici specificiraju kriterijume nad sadržajem (npr. „Obavesti me ako cena akcije Apple pređe 150 dolara")
>
> **Ključna razlika u odnosu na MQ:** kod MQ (point-to-point) poruku može primiti **samo jedan** primalac; kod pub/sub poruku može primiti **više** Subscriber-a. Subscriber prima samo poruke poslate **nakon** njegove prijave.

---

### P26. Koji od sledećih zahteva ne mora uvek da važi u message queuing sistemima? Obrazložiti.

`25-SP · 11a` — **pitanje na zaokruživanje**

- ❌ **a.** Pouzdana isporuka poruka
- ❌ **b.** Redosledna isporuka poruka
- ✅ **c.** Direktna veza između pošiljaoca i primaoca
- ⚠️ **d.** Tranzijentno skladištenje poruka

> **Odgovor:** **c) Direktna veza između pošiljaoca i primaoca** `[P2]` `[U]`
>
> MQ sistemi podržavaju **perzistentne asinhrone komunikacije** i po definiciji **ne zahtevaju ni od izvora ni od odredišta da budu aktivni za vreme prenosa poruka**. Aplikacije komuniciraju **isključivo preko redova** — red je posrednik koji odvaja pošiljaoca od primaoca. Direktna veza nikada nije potrebna.
>
> ⚠️ **Pazi:** i **d)** je sporno — MQ koristi **perzistentno**, a ne tranzijentno skladištenje. Ako pitanje dozvoljava više odgovora, zaokruži **c i d**. Ako je samo jedan — **c** je najsigurniji, jer opisuje samu definiciju odvojenosti (decoupling) koju MQ garantuje.
> *(Ni b) nije apsolutno: politika je „uglavnom FIFO", ali većina implementacija podržava prioritete koji menjaju redosled.)*

---

### P27. Koje od navedenih tvrdnji tačno opisuju karakteristike i rad sistema zasnovanih na redovima poruka (Message Queues)? Obrazložiti.

`26-JUL · 5` — **pitanje na zaokruživanje (više odgovora)**

- ✅ **A.** Poruka se šalje u određeni red koji predstavlja odredište.
- ✅ **B.** Više primalaca može čitati poruke iz istog reda u zavisnosti od modela rada.
- ❌ **C.** Pošiljalac mora znati IP adresu svakog primaoca.
- ✅ **D.** Red poruka odvaja pošiljaoca od konkretne implementacije primaoca.
- ❌ **E.** Promena primaoca zahteva promenu svih pošiljalaca.

> **Odgovor:** **tačno je A, B, D** `[P2]`
>
> - **A ✅** — *„Adresiranje je omogućeno obezbeđivanjem jedinstvenog imena odredišnog reda u sistemu."* Odredište je red, ne proces.
> - **B ✅** — *„Više procesa može slati poruke u isti red, i isto tako, više primaoca mogu uklanjati poruke iz reda."* MQ podržava modele 1↔1, 1↔više, više↔1, više↔više. *(Napomena: konkretnu poruku i dalje prima samo JEDAN primalac.)*
> - **C ❌** — adresira se **logičkim, lokacijski nezavisnim imenom reda**; preslikavanje imena u kontakt adresu `(računar, port)` održava upravljač redova.
> - **D ✅** — to je suština decoupling-a: pošiljalac zna samo ime reda.
> - **E ❌** — direktna posledica D: promena primaoca ne dira pošiljaoce.

---

### P28. Šta predstavlja JMS administrativni objekat, koja je njegova uloga i koji su primeri takvih objekata? Iz čega se sastoji JMS poruka i kako i na osnovu čega se može vršiti filtriranje poruke u JMS?

`2024 · 4b`

> **Odgovor** `[P2]`
>
> **JMS administrativni objekti** su **unapred konfigurisani JMS objekti koje kreira administrator** za upotrebu od strane klijenata. Nisu dinamički kreirani od strane kôda aplikacije, već su konfigurisani i upravljani od strane administratora.
> **Uloga:** služe kao **„most" između kôda klijenta i JMS provajdera** — klijent dobija konekciju bez potrebe da zna detalje implementacije niskog nivoa.
>
> **Primeri (2 vrste):**
> - **Fabrike konekcija (ConnectionFactory)** — omogućavaju klijentima da uspostave konekcije sa JMS provajderom; enkapsuliraju detalje kreiranja konekcija, sesija i proizvođača/potrošača poruka
> - **Destinacije (Destination)** — JMS **tema (Topic)** ili JMS **red (Queue)**
>
> *(Pretražuju se preko **JNDI** provajdera, koji vraća odgovore na `lookup` zahteve na osnovu imena.)*
>
> **JMS poruka se sastoji od 3 dela:**
> 1. **Zaglavlje (header)** — sve informacije potrebne za identifikaciju i rutiranje poruke: odredište (referenca na temu ili red), prioritet, datum isteka, ID poruke, vremenska oznaka. Većinu polja kreira JMS provajder, neka može popuniti korisnik.
> 2. **Svojstva (properties)** — **definisana od strane korisnika**; koriste se za povezivanje dodatnih metapodataka aplikacije sa porukom (npr. polje za lokaciju).
> 3. **Telo (body)** — tekstualna poruka, niz bajtova, serijalizovani Java objekat, niz primitivnih Java vrednosti ili skup parova ime/vrednost → `TextMessage, StreamMessage, MapMessage, ObjectMessage, BytesMessage`.
>
> **Filtriranje — SELEKTOR PORUKE:** predikat definisan **preko vrednosti u delovima ZAGLAVLJA i SVOJSTAVA poruke — NE i tela.** Selektor se povezuje sa potrošačem poruka.

---

### P29. Šta predstavlja konekcija a šta sesija kod JMS? Koji tipovi konekcija su podržani kod JMS? Koji deo JMS poruke može biti korišćen prilikom definisanja selektora poruka?

`26-JUN · 5` — poslednji deo je **pitanje na zaokruživanje**

- ❌ **A.** Samo telo poruke
- ❌ **B.** Samo zaglavlje poruke
- ✅ **C.** Zaglavlje i svojstva poruke, ali ne i telo poruke
- ❌ **D.** Zaglavlje, svojstva i telo poruke

> **Odgovor** `[P2]`
>
> **KONEKCIJA** — da bi se komuniciralo sa JMS provajderom, prvo je neophodno uspostaviti vezu između klijentskog programa i JMS provajdera. To se postiže kroz **fabriku konekcija**. Rezultujuća konekcija je **logički kanal između klijenta i JMS provajdera**.
>
> **SESIJA** — konekcije se mogu koristiti za kreiranje jedne ili više sesija. **Sesija je serija operacija koje uključuju kreiranje, proizvodnju i konzumaciju poruka vezanih za jedan logički zadatak.** Objekat sesije je **centralan za rad JMS-a**: podržava metode za kreiranje poruka, proizvođača i potrošača poruka, kao i operacije za **kreiranje transakcija** (grupiše seriju operacija u jednu atomičnu, sve-ili-ništa; `commit` / `rollback`).
>
> **Tipovi konekcija (2):** **`TopicConnection`** i **`QueueConnection`**.
> Postoji jasna razlika između sesija za teme i sesija za redove: `TopicConnection` podržava jednu ili više **sesija za teme**, a `QueueConnection` jednu ili više **sesija za redove**.
>
> **Selektor → C)** — *„selektorom poruke — predikatom definisanim preko vrednosti u delovima zaglavlja i svojstava poruke (ne tela)."*

---

### P30. Šta sadrži zaglavlje JMS poruke? Koja je uloga svojstava JMS poruke? Navesti i objasniti načine prijema poruka u JMS-u i navesti mehanizme koji se koriste za realizaciju svakog načina prijema.

`26-JUL · 6`

> **Odgovor** `[P2]`
>
> **Zaglavlje sadrži** sve informacije potrebne za **identifikaciju i rutiranje** poruke:
> - odredište (referenca na temu ili red)
> - prioritet poruke
> - datum isteka
> - ID poruke
> - vremenska oznaka (timestamp)
>
> *Većinu ovih polja kreira JMS provajder, ali neka mogu biti specifično popunjena od strane korisnika.*
>
> **Uloga svojstava (properties):** definisana su **od strane korisnika** i koriste se za **povezivanje dodatnih metapodataka aplikacije sa porukom** — npr. kod sistema svesnog konteksta, svojstva mogu izraziti dodatni kontekst poput polja za lokaciju. Zajedno sa zaglavljem, svojstva su ono nad čime rade **selektori poruka**.
>
> **Dva načina prijema poruka i njihovi mehanizmi:**
>
> | Način | Mehanizam |
> |---|---|
> | **Sinhroni (blokirajući)** | Program **blokira** korišćenjem operacije **primanja** (`receive`) i čeka dok poruka ne stigne. |
> | **Asinhroni (event-driven)** | Program **uspostavlja objekat slušaoca poruka — `MessageListener`** — koji mora obezbediti metodu **`onMessage`**, koja se poziva kad god se identifikuje odgovarajuća dolazeća poruka. |
>
> *(Potrošač je složeniji od proizvođača iz dva razloga: mogućnost povezivanja **selektora** i postojanje ova **dva moda prijema**.)*

---

## TEMA 4 · Otpornost na greške — mirne i Vizantijske greške, konsenzus

**Na svih 5 rokova** (2024·7 · 25-SP·6 · 25-NP·12 · 26-JUN·14b · 26-JUL·11)

---

### P31. Koji tipovi grešaka u odnosu na trajanje postoje? Dati primer za svaku od njih. Objasniti razliku između „mirnih" i Vizantijskih grešaka.

`2024 · 7a` 🔁 *(`[S·59]` — doslovno isto + „koju je teže detektovati")*

> **Odgovor** `[U]` `[S·59]`
>
> **U odnosu na trajanje:**
>
> | Tip | Opis | Primer |
> |---|---|---|
> | **Prolazne (transient)** | Pojave se **jednom** i nestanu | Istekao timeout za prenos poruke, ali nakon ponovnog slanja poruka je uspešno otposlata |
> | **Periodične (intermittent)** | Greška se pojavi, nestane, pa se **ponovo pojavi**. **Najnezgodniji tip** | Greška u mrežnom kablu kod koga postoji povremeni prekid |
> | **Stalne (permanent)** | Postoje **sve dok se komponenta ne zameni** ispravnom | Greška u hard disku, pregorela komponenta, bagovit softver |
>
> **Mirna greška (fail-silent / fail-stop)** — komponenta **prestaje sa radom** i ne generiše nikakav izlaz ili generiše poruku o grešci.
>
> **Vizantijska greška** — komponenta **nastavlja sa radom i generiše pogrešan rezultat.** Nemamo jasnu naznaku da li je komponenta otkazala.
>
> **Koju je teže detektovati:** **Vizantijsku** — komponenta funkcioniše i generiše neki rezultat, koji je nekad očekivan a nekad ne, pa je teško identifikovati problematičnu komponentu. Mirna greška se lako identifikuje jer komponenta uopšte ne radi.
>
> *(Napomena: vremenska redundansa/retransmisija pomaže kod prolaznih i periodičnih, ali **NE** kod stalnih grešaka.)*

---

### P32. U grupi postoji deset repliciranih procesa. Ako mogu nastupiti samo mirne greške, koliko maksimalno procesa može otkazati a da se ipak dobije korektan rezultat? Ako mogu nastupiti greške vizantijskog tipa, koliko procesa može maksimalno otkazati a da se ipak dobije korektan rezultat? Šta ako mogu nastupiti greške vizantijskog tipa a procesi moraju postići konsenzus? Koliko u ovom slučaju maksimalno procesa može otkazati? Svaki odgovor obrazložiti.

`2024 · 7b` 🔁 *(`[S·2]` — isto pitanje sa 5 procesa)*

> **Odgovor** `[U]` `[S·2]`
>
> **Formule (sistem je k-tolerantan ako podnese k grešaka):**
>
> | Slučaj | Potreban broj komponenti | Obrazloženje |
> |---|---|---|
> | **Mirne greške** | **k + 1** | ako k otkaže, postoji još 1 koja korektno radi |
> | **Vizantijske greške** | **2k + 1** | k može generisati pogrešne rezultate, ali k+1 radi korektno i **većinskim glasanjem** izglasava korektan rezultat |
> | **Vizantijske + konsenzus** | **3k + 1** | Lamportov algoritam: da bi se tolerisalo k izdajnika mora postojati bar 3k+1 generala, tj. bar 2k+1 lojalnih (**više od 2/3 mora biti lojalno**) |
>
> **Za n = 10:**
> - **Mirne greške:** `k + 1 ≤ 10` ⇒ **k = 9** procesa može otkazati (mora ostati bar 1 ispravan)
> - **Vizantijske greške:** `2k + 1 ≤ 10` ⇒ `k ≤ 4.5` ⇒ **k = 4** procesa može otkazati (ostaje 6 ispravnih, većina glasa korektno)
> - **Vizantijske + konsenzus:** `3k + 1 ≤ 10` ⇒ `k ≤ 3` ⇒ **k = 3** procesa može otkazati (10 ≥ 3·3+1 = 10 ✓)
>
> 💡 **Zapamti oznaku:** u skripti se koristi **m** umesto k za konsenzus (`3m+1`). Isto je.

---

### P33. Šta se podrazumeva pod raspoloživošću a šta pod pouzdanošću sistema? Koliko je replika potrebno ako se moraju tolerisati a) f mirnih grešaka; b) f grešaka Vizantijskog tipa; c) f grešaka Vizantijskog tipa pri čemu sistem mora postići konsenzus.

`26-JUL · 11`

> **Odgovor** `[U]`
>
> - **Raspoloživost (availability)** — sistem koji je **spreman za korišćenje u trenutku kada je to potrebno**
> - **Pouzdanost (reliability)** — osobina sistema da **kontinualno radi (duže vreme) bez nastupanja grešaka**
> - **Razlika je u VREMENU:** raspoloživost se odnosi na **konkretan trenutak**, a pouzdanost na **dug vremenski period**
>
> **Broj replika:**
>
> | | Broj replika |
> |---|---|
> | **a) f mirnih grešaka** | **f + 1** |
> | **b) f Vizantijskih grešaka** | **2f + 1** |
> | **c) f Vizantijskih + konsenzus** | **3f + 1** |
>
> *(Ovo je ista formula kao P32, samo direktno postavljena.)*

---

### P34. Koji sistem je otporan na greške Vizantijskog tipa?

`25-SP · 6` — **pitanje na zaokruživanje**

- ❌ **a.** Sistem koji je otporan na otkaz više čvorova
- ❌ **b.** Sistem koji je otporan na gubitak više poruka
- ❌ **c.** Sistem koji je otporan na podelu mreže
- ✅ **d.** Sistem koji je otporan na maliciozne čvorove

> **Odgovor:** **d)** `[U]`
>
> Definicija: *„Vizantijska greška — komponenta **nastavlja sa radom i generiše pogrešan rezultat**."* To je upravo ponašanje **malicioznog (zlonamernog) čvora** — čvora koji šalje netačne i kontradiktorne informacije da bi sprečio ostale da postignu konsenzus (problem Vizantijskih generala).
>
> - **a) netačno** — otkaz čvorova (prestanak rada) je **mirna** greška, ne Vizantijska
> - **b) netačno** — gubitak poruka je problem **komunikacionih kanala** (problem dve armije), a ne Vizantijskih procesa
> - **c) netačno** — podela mreže je takođe problem kanala/povezanosti

---

### P35. U procesu množenja dve matrice korišćena je ABFT tehnika za detekciju i korekciju grešaka. Dobijen je sledeći rezultat: a) Utvrditi da li je nastupila greška u toku izračunavanja; b) Ako je nastupila greška, naći njenu lokaciju; c) Izvršiti korekciju greške, ako je greška nastupila.

`25-NP · 12` 🔁 *(`[S·32]` — ista matrica!)*

```
   ⎡  1   2   3  |  6 ⎤
   ⎢  2   3   4  |  9 ⎥
   ⎢  3   3   3  |  8 ⎥
   ⎣──────────────────⎦
   ⎣  6   7  10  | 23 ⎦
```

> **Odgovor** `[S·32]`
>
> **ABFT (Algorithm Based Fault Tolerance)** je vid **informacione redundanse** — matrici se dodaju kontrolna vrsta i kontrolna kolona (sume).
>
> **a) DA, nastupila je greška.** Proverom suma po vrstama i kolonama utvrđeno je neslaganje:
>
> | Vrsta | Suma elemenata | Kontrolna suma | |
> |---|---|---|---|
> | 1 | 1+2+3 = 6 | 6 | ✓ |
> | 2 | 2+3+4 = 9 | 9 | ✓ |
> | 3 | 3+3+3 = **9** | **8** | ✗ |
>
> | Kolona | Suma elemenata | Kontrolna suma | |
> |---|---|---|---|
> | 1 | 1+2+3 = 6 | 6 | ✓ |
> | 2 | 2+3+3 = **8** | **7** | ✗ |
> | 3 | 3+4+3 = 10 | 10 | ✓ |
>
> **b) Lokacija greške: presek 3. vrste i 2. kolone**, tj. element **C₃₂ = 3**.
>
> **c) Korekcija:**
> `C₃₂ = 3 + (7 − 8) = 3 − 1 = **2**`   *(po koloni)*
> ili
> `C₃₂ = 3 + (8 − 9) = 3 − 1 = **2**`   *(po vrsti)*
>
> **Provera:** vrsta 3 = 3+2+3 = 8 ✓ · kolona 2 = 2+3+2 = 7 ✓ · ukupno 6+9+8 = 23 ✓
>
> 💡 **Recept:** greška je uvek na **preseku vrste i kolone koje ne štimaju**; korekcija = `stara_vrednost + (kontrolna_suma − stvarna_suma)`.

---

### P36. Klijent je uputio zahtev serveru. Odgovor nije stigao. Da li klijent može da zaključi da li je došlo do otkaza servera ili je izgubljen odgovor? Obrazložiti.

`26-JUN · 14b`

> **Odgovor** `[U]`
>
> **NE, klijent ne može da razlikuje ove situacije.**
>
> *„Klijent ne može da razlikuje ove dve situacije, jer on **vidi samo istek timeout-a**."*
>
> Sve četiri RPC greške klijentu izgledaju identično — kao istek timeout-a:
> 1. Klijent ne može da locira server
> 2. Zahtev upućen ka serveru je izgubljen
> 3. Otkaz servera nakon prijema zahteva (a **procedura je možda već izvršena, a možda nije** — to su dve različite situacije koje takođe ne može razlikovati)
> 4. Odgovor servera ka klijentu je izgubljen
>
> **Posledica:** zato ne postoji **exactly-once** semantika. Klijent bira između:
> - **at-least-once** (ponavlja zahtev dok ne dobije odgovor — bezbedno **samo za idempotentne** procedure)
> - **at-most-once** (odmah emituje exception — za procedure koje menjaju stanje sistema)
>
> *Primer sa štampačem `[U]`: ne postoji nijedna kombinacija klijentske i serverske strategije koja garantuje da će se štampanje obaviti tačno jednom za svaku kombinaciju događaja na strani servera.*

---

# 🟠 UČESTALOST 4/5

---

## TEMA 5 · Osnovne osobine DS — skalabilnost, transparentnosti, heterogenost, middleware

**Na 4 od 5 rokova** (2024·1,2 · 25-NP·1,2,3 · 26-JUN·1 · 26-JUL·1) — *nema samo na 25-SP*
**Skoro uvek PRVO pitanje na blanketu.**

---

### P37. Šta predstavlja skalabilnost distribuiranog sistema? Navesti i objasniti tehnike skaliranja distribuiranih sistema.

`2024 · 1` 🔁 `25-NP · 1` 🔁 `26-JUN · 1b` — **javlja se 3×** *(`[S·48]`, `[S·68]`)*

> **Odgovor** `[U]` `[S·48,68]`
>
> **Skalabilnost (proširljivost)** — mogućnost proširenja sistema, tj. dodavanja novih računara. Posmatra se kroz **3 dimenzije**:
> 1. Skalabilnost u odnosu na **broj korisnika i resursa**
> 2. Skalabilnost u odnosu na **geografsku udaljenost** resursa i korisnika
> 3. **Administrativna skalabilnost** — sistemom se može lako upravljati čak i ako se prostire kroz više administrativnih domena
>
> **3 TEHNIKE SKALIRANJA:**
>
> **1. Skrivanje komunikacionog kašnjenja**
> Koriste se **asinhrone** komunikacije umesto sinhronih — klijent se ne blokira dok čeka odgovor, već radi drugi posao; kada stigne odgovor generiše se prekid. Asinhrone komunikacije nisu od koristi kod **interaktivnih** aplikacija — tada je rešenje **download-ovati deo koda na klijentsku stranu** da bi se ubrzala obrada.
>
> **2. Distribucija**
> Komponente se **dele na manje delove**, a zatim se ti delovi distribuiraju na više mašina u sistemu. *Primer: DNS i Web.*
>
> **3. Replikacija**
> Postavljanje **kopije resursa blizu mesta korišćenja** da bi se smanjilo komunikaciono kašnjenje i balansiralo opterećenje.
>
> **Problem tehnika skaliranja:** postojanje više kopija dovodi do **problema konzistencije** — modifikacija jedne kopije dovodi do neslaganja sa ostalima; za sinhronizaciju bi bila potrebna **globalna sinhronizacija**, koju je gotovo nemoguće postići u DS-u.
>
> **Decentralizovani algoritmi** (preduslov skalabilnosti) imaju osobine: nijedna mašina nema kompletnu sliku o stanju sistema; odluke se donose na osnovu lokalnih podataka; otkaz mašine ne ruši sistem; nema pretpostavke o globalnom časovniku.

---

### P38. Šta se podrazumeva pod transparentnošću konkurencije?

`2024 · 2` — **pitanje na zaokruživanje**

- ❌ **a)** sve niti mogu pristupati deljivim strukturama podataka
- ✅ **b)** procesi mogu pristupati resursima bez međusobne interferencije
- ❌ **c)** repliciranim resursima se pristupa kao da postoji samo jedna kopija
- ❌ **d)** novi čvorovi se mogu dodati sistemu bez promene aplikacije

> **Odgovor:** **b)** `[UV]` `[U]` *(na blanketu je zaokruženo b)*
>
> Definicija: *„Transparentnost konkurencije omogućava da više konkurentnih procesa, tj. više procesa istovremeno koristi isti resurs, **a da korisnici ne primete** da se isti resurs koristi istovremeno. Ovakav istovremeni pristup istom resursu mora da ostavi resurs u konzistentnom stanju, i to se postiže **serijalizacijom** deljenog resursa."*
>
> - **c)** je definicija **transparentnosti replikacije**
> - **d)** je definicija **skalabilnosti / otvorenosti**, ne transparentnosti konkurencije

---

### P39. Koje su osobine sistema koji poseduje transparentnost konkurencije?

`25-NP · 2` — **pitanje na zaokruživanje (više odgovora)**

- ✅ **a)** Rezultat istovremenih zahteva isti je kao i kada bi se izvršavali sekvencijalno.
- ✅ **b)** Korisnik ne mora da zna da postoje drugi konkurentni korisnici.
- ❌ **c)** Korisnik mora ručno sinhronizovati pristup deljenim resursima
- ✅ **d)** Sistem sprečava nekonzistentnost podataka tokom istovremenog pristupa.

> **Odgovor:** **tačno je a, b, d** `[UV]`
>
> - **a ✅** — konzistentnost se postiže **serijalizacijom** pristupa deljivom resursu ⇒ rezultat je isti kao da su se zahtevi izvršili sekvencijalno
> - **b ✅** — *„Važno je postići da korisnici ne primete da se isti resurs koristi jednovremeno"*
> - **c ❌** — upravo suprotno: **sistem** obavlja sinhronizaciju (mehanizmima zaključavanja i sinhronizacije), a **ne korisnik**. Da korisnik mora ručno da sinhronizuje, transparentnosti ne bi ni bilo.
> - **d ✅** — *„Jednovremeni pristup deljivom resursu mora ostaviti resurs u konzistentnom stanju"*

---

### P40. Šta se podrazumeva pod transparentnošću replikacije?

`25-NP · 3` — **pitanje na zaokruživanje**

- ❌ **a)** proces je svestan šeme replikacije i može to iskoristiti
- ✅ **b)** repliciranim resursima se pristupa kao da postoji samo jedna kopija
- ❌ **c)** resurs će upravljati svim zahtevima na isti način bez obzira na lokaciju klijenta
- ❌ **d)** podaci se ne mogu menjati i mogu se zapamtiti na disku

> **Odgovor:** **b)** `[UV]` `[U]`
>
> Definicija: *„Transparentnost replikacije omogućava postojanje većeg broja replikacija (kopija) istog resursa u DS-u da bi se povećale performanse i smanjilo komunikaciono kašnjenje. Kopija resursa se postavlja bliže mestu odakle se obavlja pristup, a **sve kopije imaju isto ime**. **Korisnici ne treba da budu svesni postojanja više kopija resursa.**"*
>
> - **a ❌** — direktna suprotnost: proces **ne sme** biti svestan replikacije
> - 💡 Dodatno: ako DS podržava replikaciju, **automatski podržava i lokacionu transparentnost** (jer sve kopije imaju isto ime).

---

### P41. Šta se podrazumeva pod pristupnom transparentnošću?

`26-JUN · 1c` — **pitanje na zaokruživanje**

- ❌ **A.** udaljenim resursima se pristupa korišćenjem lokaciono nezavisnih imena
- ✅ **B.** lokalnim i udaljenim resursima se pristupa korišćenjem istih operacija
- ❌ **C.** repliciranim resursima se pristupa kao da postoji samo jedna kopija
- ❌ **D.** resurs će upravljati svim zahtevima na isti način bez obzira na lokaciju klijenta

> **Odgovor:** **B)** `[UV]` `[S·33]`
>
> Definicija: *„Pristupna transparentnost — podacima i resursima se pristupa **na jedinstven način, bez obzira da li se oni nalaze na udaljenom ili lokalnom računaru**."*
> Primer: različiti OS mogu koristiti različite načine imenovanja fajlova; razlike u imenovanju i manipulaciji fajlovima moraju biti skrivene od korisnika i aplikacije.
>
> - **A ❌** — to je **lokacijska** transparentnost (postiže se logičkim imenima, npr. URL)
> - **C ❌** — to je **transparentnost replikacije**

---

### P42. Objasniti migracionu transparentnost i transparentnost paralelizacije.

`26-JUL · 1b`

> **Odgovor** `[UV]` `[S·33]`
>
> **Migraciona transparentnost** — resurs može **promeniti svoju lokaciju a da klijent to ne primeti ni zna**. Resurs se može kretati a da pri tome **ne dolazi do promene imena resursa**.
> *Primer: mobilni telefoni — pozivalac i pozvani nisu svesni kretanja onog drugog.*
>
> **Transparentnost paralelizacije** — **paralelizacija procesa se izvršava transparentno** za aplikativnog programera i korisnika aplikacije. Korisnik nije svestan **da i kako** je aplikacija paralelizovana i **gde se izvršavaju procesi**.
> *Primer: **Hadoop**.*
>
> **Za slučaj da traži sve tipove transparentnosti (7):** pristupna, lokacijska, migraciona, konkurencije, replikacije, za otkaze, paralelizacije.

---

### P43. Kroz koje sve aspekte se manifestuje heterogenost u distribuiranim sistemima?

`26-JUL · 1a`

> **Odgovor** `[UV]` `[U]`
>
> **DS je sastavljen od heterogenog skupa računara.** Heterogenost se ogleda u sledećem:
>
> 1. **Hardver računara** — različit skup instrukcija, različita interna prezentacija podataka
> 2. **Operativni sistemi** — interfejs za razmenu poruka razlikuje se od OS-a do OS-a (npr. Berkeley sockets i WinSock)
> 3. **Programski jezici** — karakteri i strukture podataka se različito predstavljaju u različitim jezicima, što je problem ako aplikacije treba međusobno da komuniciraju
> 4. **Implementacije od strane različitih projektanata** — dok se ne usvoje zajednički standardi, različite implementacije ne mogu međusobno da komuniciraju (**OSI model** je jedan način rešavanja problema heterogenosti)
>
> 💡 Vezano pitanje: **middleware** postoji upravo da sakrije heterogenost (vidi **P44**), a **standardni format podataka / XDR** rešava heterogenost na nivou prenosa parametara (vidi **P15**).

---

### P44. Šta je funkcija middleware-a u distribuiranom sistemu?

`26-JUN · 1a` 🔁 *(`[S·67]`)*

> **Odgovor** `[U]` `[S·67]` `[UV]`
>
> **Osnovni cilj middleware-a je da se sakrije heterogenost platforme na kojoj je sistem izgrađen — od same aplikacije, kao i da se sakrije komunikacija.** Mrežni OS se nadograđuje dodatnim SW slojem — middleware-om.
>
> - Middleware je računarski softver koji **pruža usluge (servise) višeg nivoa** softverskim aplikacijama
> - **Middleware komunikacioni protokoli** oslobađaju aplikativnog programera detalja vezanih za komunikaciju između procesa — **komunikacija je skrivena iza poziva procedure ili metoda** (RPC / RMI)
> - Neke usluge koje pružaju: **autentifikacija i autorizacija**
> - Middleware sistemi nude kompletan skup usluga i **ne dozvoljavaju korišćenje ničeg drugog do njihovih interfejsa** prema uslugama
>
> **Modeli middleware-a:**
> - **RPC model** — resursi se modeluju kao **procedure**; skriva se mrežna komunikacija, proces ima utisak da je pozvao lokalnu proceduru
> - **Objektno-orijentisan model** — resursi se modeluju kao **objekti** (podaci + funkcije nad podacima); svaki objekat implementira **interfejs** koji skriva unutrašnje detalje; **objekat nije distribuiran, već interfejs**
>
> **Razlika u odnosu na ostale OS** `[U]` *(`[S·8]` — traženo i kao zasebno pitanje)*:
> - **Distribuirani OS** — čvrsto spregnut OS za multiprocesore i **homogene** multiračunare; čini da skup nezavisnih računara korisnicima izgleda kao jedan računar
> - **Mrežni OS** — **slabo spregnut** OS za **heterogene** multiračunare; omogućava korisnicima da pristupe uslugama na drugoj mašini
> - **Middleware** — dodatni sloj **iznad mrežnog OS-a** koji sakriva heterogenost i komunikaciju

---

## TEMA 6 · Logički i vektorski časovnici

**Na 4 od 5 rokova** (25-SP·1 · 25-NP·7 · 26-JUN·10a · 26-JUL·9)

---

### P45. Ako se koriste vektorski časovnici, šta je najviše što možemo doznati?

`25-SP · 1` — **pitanje na zaokruživanje**

- ❌ **a)** ako je V(a) < V(b) tada se a desilo pre b
- ✅ **b)** V(a) < V(b) ako i samo ako se a desilo pre b
- ❌ **c)** ako se a desilo pre b tada je V(a) < V(b)
- ❌ **d)** ako je V(a) = V(b) tada su a i b neuređeni

> **Odgovor:** **b)** `[U]` `[S·31]`
>
> Pitanje traži **NAJJAČU** tvrdnju koja važi. Iz materijala:
> *„Za bilo koja 2 događaja e i e′ važi sledeće: **Ako je e → e′ tada važi V(e) < V(e′)**; **Ako je V(e) < V(e′) tada važi e → e′**."*
>
> Kod vektorskih časovnika važe **OBA smera**, dakle **ekvivalencija (ako i samo ako)** — to je odgovor **b)**, koji je jači od a) i c) pojedinačno (a) i c) su svaki po jedan smer te ekvivalencije i takođe su tačni, ali nisu „najviše što možemo doznati").
>
> - **d) netačno** — ako je `V(a) = V(b)`, događaji su **isti/jednaki**, a ne neuređeni. Konkurentni (neuređeni) su kada se **ne može uspostaviti** ni `V(e) ≤ V(e′)` ni `V(e) ≥ V(e′)`.
>
> ⚡ **Ključna razlika u odnosu na Lamporta:** kod Lamportovih markica važi **samo jedan smer** (`a→b ⇒ T(a)<T(b)`), pa se **NE MOŽE** utvrditi da li su događaji uslovljeni. Vektorski časovnici to mogu.

---

### P46. Na sl. 1 prikazana su tri procesa koja međusobno komuniciraju. Korišćenjem vektorskih časovnika odrediti koji su događaji međusobno uslovljeni a koji konkurentni.

`25-NP · 7` 🔁 *(`[S·42]`, `[S·71]` iste varijante)*

> **Odgovor — postupak** `[U]` `[S·31,58]`
>
> **Pravila za implementaciju vektorskih časovnika:**
> 1. Vektor se **inicijalizuje na 0** u svim procesima: `Vi[j] = 0`, za `i, j = 1…N` (N = broj procesa)
> 2. Proces `Pi` **inkrementira i-ti element** svog lokalnog vektora **pre nego što obeleži lokalni događaj**: `Vi[i]++`. Poruka se šalje zajedno sa `Vi`.
> 3. Kada `Pj` primi poruku, **poredi lokalni vektor sa primljenim, element po element, i postavlja svaki element na VEĆU od dve vrednosti**
>
> **Poređenje vektora:**
> - `V = V′` ako je `V[i] = V′[i]` za svako `i = 1…N`
> - `V ≤ V′` ako je `V[i] ≤ V′[i]` za svako `i = 1…N`
>
> **Zaključivanje:**
> - `e → e′` ⟺ `V(e) < V(e′)` — događaji su **međusobno uslovljeni**
> - Ako se **ne može uspostaviti** ni `V(e) ≤ V(e′)` ni `V(e) ≥ V(e′)` (vektori se **ne mogu porediti**) ⇒ događaji su **KONKURENTNI** i nisu međusobno uslovljeni
>
> **Značenje elemenata:** `Vi[i]` je broj događaja koji su se desili u `Pi`. Ako je `Vi[j] = k` (j ≠ i), tada `Pi` zna da se u `Pj` desilo `k` događaja.
>
> 💡 **Na ispitu:** obeleži svaki događaj vektorom po pravilima 1–3, pa uporedi parove. Onaj par kod koga jedan vektor ima **veću** vrednost u jednoj komponenti a **manju** u drugoj → **konkurentni**.

---

### P47. Šta će se dogoditi ako se koristi Ricart-Agrawala algoritam i dva procesa koji žele pristup istoj kritičnoj sekciji generišu zahtev sa istom vrednošću Lamportove markice?

`26-JUN · 10a`

> **Odgovor** `[U]`
>
> **Problem:** *„Moguće je da više konkurentnih događaja (koji nisu međusobno uslovljeni) imaju **iste vremenske markice**, što nije poželjno! Ovo dovodi do konfuzije ako više procesa treba da donese odluku na osnovu vremenskih markica dva događaja."*
> Kod Ricart-Agrawala se pobednik bira po **manjoj markici** — ako su markice iste, remi se ne može razrešiti i **oba procesa bi mogla da uđu u kritičnu sekciju** (narušeno uzajamno isključivanje) ili bi nastao **deadlock** (oba čekaju).
>
> **REŠENJE:** *„Prisiliti da svaka markica bude **jedinstvena**. Lamportovoj vremenskoj markici dodati **još jedan identifikator** koji predstavlja **globalno jedinstveni id procesa** u kome je nastao događaj (adresa hosta + proces ID)."*
>
> ➜ Markica postaje par **`(T, id_procesa)`**, i poredi se **leksikografski**: prvo po T, a kod jednakog T **pobeđuje manji id procesa**. Time je remi uvek razrešen deterministički i svi procesi donose istu odluku.

---

### P48. U distribuiranoj bazi podataka koriste se Lamportove markice za uređivanje događaja. Dva ažuriranja dobijaju sledeće markice: Update X = 25 i Update Y = 27. Da li se može zaključiti da je ažuriranje X uzrokovalo ažuriranje Y? Obrazložiti odgovor.

`26-JUL · 9a` 🔁 *(`[S·44]`)*

> **Odgovor** `[U]` `[S·44]`
>
> **NE, ne može se zaključiti.**
>
> Kod Lamportovih markica važi **samo jedan smer**:
> `a → b ⇒ T(a) < T(b)`
> ali **obrnuto NE važi**: iz `T(a) < T(b)` **ne sledi** `a → b`.
>
> *„Problem sa Lamportovim markicama je što se pomoću njih **ne može utvrditi koji su događaji međusobno uslovljeni, a koji su konkurentni**! Ako imamo dva događaja i znamo samo njihove vremenske markice i važi recimo T(a) < T(b), mi ne možemo da tvrdimo da se a desilo pre b."*
>
> Iz `T(X)=25 < T(Y)=27` sledi samo da **Y nije uzrokovalo X**. Događaji X i Y mogu biti **potpuno konkurentni** (u procesima koji ne razmenjuju poruke).
>
> ➜ Da bi se uzročnost pouzdano utvrdila, moraju se koristiti **VEKTORSKI časovnici** (vidi **P45**).

---

### P49. Događaji A, B, C i D u distribuiranom sistemu imaju sledeće vektorske časovnike: A[1,0,0], B[2,0,0], C[2,1,0], D[1,2,0]. Koji od gore navedenih vektorskih časovnika nije moguć i zašto?

`26-JUL · 9b`

> **Odgovor:** **D[1,2,0] nije moguć.**
>
> **Obrazloženje** `[U]` `[S·31]`
>
> Analiza po pravilima vektorskih časovnika:
>
> | Događaj | Vektor | U kom procesu | Šta znači |
> |---|---|---|---|
> | A | `[1,0,0]` | **P1** (1. događaj) | P1 zna 0 događaja iz P2 i P3 |
> | B | `[2,0,0]` | **P1** (2. događaj) | isto |
> | C | `[2,1,0]` | **P2** (1. događaj) | P2 zna za **2** događaja iz P1 (primio poruku posle B) |
> | D | `[1,2,0]` | **P2** (2. događaj) | P2 zna za **samo 1** događaj iz P1 |
>
> **Kontradikcija:** C je **prvi** događaj u P2 (`V[2] = 1`), a D je **drugi** (`V[2] = 2`). Dakle D nastupa **posle** C u istom procesu.
>
> Ali komponente vektorskog časovnika **nikada ne mogu da opadnu** unutar istog procesa — pravilo 3 kaže da se element uvek postavlja na **VEĆU** od dve vrednosti (vreme ne ide unazad).
>
> U C je `V[1] = 2`, a u D je `V[1] = 1 < 2`. **Nemoguće** — P2 ne može „zaboraviti" da je već znao za 2 događaja iz P1.
>
> ➜ Korektna vrednost za D bila bi **[2,2,0]**.

---

## TEMA 7 · Grupna komunikacija i uređenje poruka

**Na 4 od 5 rokova** (2024·5 · 25-SP·2 · 25-NP·8 · 26-JUN·12)

---

### P50. Šta je definicija potpuno uređene grupne komunikacije?

`2024 · 5` — **pitanje na zaokruživanje**

- ❌ **a)** poruke se isporučuju procesima u FIFO redosledu
- ❌ **b)** poruke se procesima isporučuju po redosledu realnog vremena
- ❌ **c)** poruke se isporučuju procesima po redosledu „desilo se pre"
- ✅ **d)** poruke se isporučuju svim procesima po istom redosledu

> **Odgovor:** **d)** `[U]` `[S·22]` *(na blanketu je zaokruženo d)*
>
> Definicija: *„Obezbediti potpuno uređenu grupnu komunikaciju znači obezbediti operaciju kojom se **sve poruke isporučuju u istom redosledu svim prijemnicima**."*
>
> - **a) FIFO konzistencija** — samo poruke iz istog izvora u redosledu slanja
> - **b) realno vreme** — nemoguće u DS-u (nema globalnog časovnika)
> - **c) „desilo se pre"** — to je **kauzalno (uslovno) uređenje**, slabije od totalnog

---

### P51. Šta je definicija potpuno uređene grupne komunikacije? *(otvoreno pitanje)*

`25-SP · 2` 🔁 *(isto pitanje kao P50, samo bez ponuđenih odgovora)* `[S·22]`

> **Odgovor** `[U]` `[S·22]`
>
> **Definicija:** Potpuno uređena grupna komunikacija odnosi se na komunikaciju u kojoj **sve replike treba da prime isti skup poruka u istom redosledu**, tj. da sve replike budu u konzistentnom stanju. Obezbediti je znači obezbediti **operaciju (multicast) kojom se sve poruke isporučuju u istom redosledu svim prijemnicima**.
>
> **Klasični primer (obavezno navesti):** replicirani bankovni računi u New York-u i San Francisco-u. Stanje je 1000$. U SF se dodaje 100$, u NY se istovremeno dodaje 1% kamate.
> - NY: prvo kamata (1010$), pa +100$ → **1110$**
> - SF: prvo +100$ (1100$), pa kamata → **1111$**
>
> ➜ **To ne sme da se desi!** Mora se obezbediti da se operacije ažuriranja obave **u istom redosledu** na svim replikama.
>
> **Algoritam (Lamportove markice + ACK):**
> - Poruka ažuriranja se obeležava **logičkim vremenom izvora** (proširene markice `T.id`)
> - Poruka se prosleđuje **svima u grupi (pa i sebi)**
> - Kada poruka stigne: smešta se u **lokalni queue čekanja**, poruke se uređuju po markicama, i **ACK se prosleđuje svima (pa i sebi)**
> - Poruke iz istog izvora se isporučuju u redosledu slanja (**FIFO**)
> - **Poruka se prosleđuje aplikaciji samo ako je na vrhu reda i potvrđena od strane SVIH procesa**
> - `Pi` šalje ACK za poruku od `Pj` ako: `Pi` nije poslao poruku ažuriranja, **ili** je `id(Pi) > id(Pj)`, **ili** je zahtev `Pi` već obrađen

---

### P52. Koje časovnike ćete koristiti ako je potrebno ostvariti: a) totalno uređenu grupnu komunikaciju (multicast); b) uslovno (kauzalno) uređenu grupnu komunikaciju?

`25-NP · 8`

> **Odgovor** `[U]` `[S·49]`
>
> **a) Totalno uređena grupna komunikacija (multicast) → LAMPORTOVI (skalarni) LOGIČKI ČASOVNICI**
> Poruka ažuriranja se obeležava logičkim vremenom izvora (proširena markica `T.id`), poruke se u redovima čekanja uređuju po markicama i isporučuju aplikaciji tek kada su potvrđene od svih procesa. Tako **svi procesi vide isti redosled**.
> *(Alternativa iz materijala: **centralni koordinator** koji svakoj write operaciji dodeljuje jedinstven id — ali može postati usko grlo.)*
>
> **b) Uslovno (kauzalno) uređena grupna komunikacija → VEKTORSKI ČASOVNICI**
> *„Implementacija uslovne konzistencije obavlja se pomoću **vektorskih časovnika**."*
> Pravilo: **časovnik se inkrementira SAMO kod slanja poruke, ne i kod prijema.** Poruka `m` se ne prosleđuje procesu `Pj` dok se ne zadovolje **dva uslova**:
> - **a)** `tm[i] = Vj[i] + 1` — obezbeđuje da je `Pj` primio sve prethodne poruke od `Pi`
> - **b)** `tm[k] ≤ Vj[k]`, za svako `k ≠ i` — obezbeđuje da je `Pj` primio sve poruke koje je `Pi` primio pre slanja `m`
>
> Ako uslovi nisu ispunjeni, poruka se **baferuje** dok ne stignu poruke koje joj prethode.

---

### P53. Objasniti razliku između potpuno uređene grupne komunikacije (total ordering) i uređenja međusobno zavisnih događaja (causal ordering). Da li totalno uređenje garantuje kauzalno uređenje?

`26-JUN · 12`

> **Odgovor** `[U]`
>
> | | **Totalno uređenje (total ordering)** | **Kauzalno uređenje (causal ordering)** |
> |---|---|---|
> | Zahtev | **Sve** poruke se isporučuju **svim** procesima u **istom redosledu** | Samo **potencijalno uslovljene** (kauzalno povezane) poruke moraju se svuda videti u istom redosledu |
> | Konkurentne poruke | moraju i one biti u istom redosledu svuda | mogu se videti u **različitom** redosledu u različitim procesima |
> | Časovnici | Lamportovi (skalarni) | **Vektorski** |
> | Jačina | **jači** zahtev | **slabiji** zahtev |
>
> **Da li totalno uređenje garantuje kauzalno?**
>
> **NE — ne obavezno.** Totalno uređenje garantuje samo da **svi vide ISTI redosled**, ali ne garantuje da je taj redosled **saglasan sa kauzalnošću**. Moguće je da svi procesi isporuče poruku `m2` (odgovor) pre poruke `m1` (uzrok) — svi vide isti redosled (totalno uređenje ✓), ali je kauzalnost narušena (✗).
>
> ➜ Zato materijal za kauzalno uređenje traži **posebnu implementaciju vektorskim časovnicima sa baferovanjem** (vidi P52b): *„Ako se šalje poruka m1, može se desiti da proces P3 prvo primi njen odgovor m2 pa tek onda poruku m1, što nije dobro, pa se mora obezbediti baferovanje poruke m2 dok ne stigne m1."*
>
> 💡 Da bi bilo i totalno **i** kauzalno, potrebno je **kauzalno-totalno uređenje** (kombinacija oba mehanizma).

---

# 🟡 UČESTALOST 3/5

---

## TEMA 8 · Replikacija — vrste replika, protokoli konzistencije

**Na 3 od 5 rokova** (25-NP·11 · 26-JUN·14a,15a · 26-JUL·10)

---

### P54. Koje vrste replika postoje? Koje se informacije mogu proslediti replikama kada se obavlja ažuriranje? Koji protokol konzistencije se koristi za postizanje sekvencijalne konzistencije?

`25-NP · 11` 🔁 *(`[S·63]`)*

> **Odgovor** `[U]` `[S·63]`
>
> **VRSTE REPLIKA (3):**
> 1. **Permanentne replike** — početni skup replika koje obrazuju distribuirano skladište podataka; broj im je relativno mali (npr. klaster servera na jednoj lokaciji sa Round Robin raspodelom, ili mirror lokacije preko web hostinga)
> 2. **Replike inicirane od strane servera** — **privremene**, postavljaju se u regione odakle dolazi veliki broj zahteva. Server vodi računa o **broju pristupa fajlu i odakle pristupi dolaze**; ako broj obraćanja pređe **prag replikacije**, kreira se replika bliže klijentima
> 3. **Replike inicirane od strane klijenta** — **keš** na klijentu ili proxy serveru u istom LAN-u. **Server nema nikakvu odgovornost da održava ove kopije konzistentnim** — klijent je dužan da to radi sam
>
> **ŠTA SE PROSLEĐUJE PRI AŽURIRANJU (3 opcije; cilj je smanjenje saobraćaja):**
> 1. **Samo obaveštenje o obavljenom ažuriranju** (invalidacija ostalih kopija) — pogodno kada je **odnos broja upisa naspram čitanja veliki**
> 2. **Slanje modifikovanih podataka** — pogodno kada je **broj čitanja mnogo veći od broja modifikacija**
> 3. **Slanje operacija koje su izvršene** nad modifikovanim podacima — poznato kao **AKTIVNA REPLIKACIJA**; prenosi se samo komanda umesto velikog fajla → minimalan saobraćaj, ali **više procesorskog vremena** jer svaka replika izvršava komande
>
> **PROTOKOL ZA SEKVENCIJALNU KONZISTENCIJU:**
> **Protokoli zasnovani na postojanju primarne kopije** — *„Najjači vid konzistencije koji se može ostvariti u DS-u je sekvencijalna konzistencija i za njega se koriste protokoli zasnovani na postojanju primarne kopije."*
> Dve varijante:
> - **Remote-write** — primar je **fiksiran** na jednoj lokaciji; sve modifikacije idu preko njega. *„Primar može urediti sve dolazne zahteve za modifikacijom na globalno jedinstveni način tako da svi procesi vide sve write operacije u istom redosledu — što je pogodno za postizanje sekvencijalne konzistencije."*
> - **Local-write** — primar **privremeno migrira** na lokalnu kopiju klijenta koji je inicirao ažuriranje

---

### P55. Bankovni server je repliciran. Razmatraju se dva dizajna: i) sve replike izvršavaju sve zahteve; ii) jedan server izvršava zahteve i periodično ažurira replike. Koje tehnike repliciranja se koriste u slučaju i) a koje u slučaju ii)?

`26-JUN · 14a`

> **Odgovor** `[U]`
>
> **i) Sve replike izvršavaju sve zahteve → AKTIVNA REPLIKACIJA**
> - Koristi se kod **grupe ravnopravnih procesa (RAVNE GRUPE)** — svi procesi imaju jednaku funkciju
> - Klijentski zahtev se prosleđuje **svim** procesima u grupi; svi obrađuju zahtev **u istom redosledu**
> - Zahteva **potpuno uređenu grupnu komunikaciju** (Lamportove markice ili centralni koordinator)
> - Klijentski interfejs koristi **većinsko glasanje** da prosledi tačan odgovor klijentu
> - **Prednost:** dobro se nosi sa **Vizantijskim** greškama · **Mana:** problem sinhronizacije
>
> **ii) Jedan server izvršava i periodično ažurira replike → PASIVNA REPLIKACIJA (primary-backup)**
> - Koristi se kod **hijerarhijski organizovanih grupa** — 1 koordinator (**primar**), ostali su **backup** procesi
> - Primarni server obavlja ceo posao; ako otkaže, jedan od backup servera preuzima posao (otkaz treba da bude nevidljiv za aplikaciju)
> - Backup detektuje otkaz preko **heartbeat** poruka („Da li si živ?"); ako nema odgovora u timeout-u, bira se novi primar (**konsenzus**)
> - **Prednost:** jednostavniji dizajn, nema problema sa sinhronizacijom (poruka ide samo primaru) · **Mana:** **loše radi sa Vizantijskim greškama** — primar može pogrešno raditi a da greška ne bude otkrivena

---

### P56. Distribuirani sistem koristi primary-backup protokol. Postoje 4 replike: R1, R2, R3, R4. R1 je primar. Klijent šalje write(x=10). Redom navesti korake koji se dešavaju.

`26-JUL · 10a`

> **Odgovor** `[U]` *(remote-write protokol, koraci W1–W5)*
>
> | Korak | Šta se dešava |
> |---|---|
> | **W1** | Klijent upućuje `write(x=10)` **svom lokalnom serveru** (jednoj od backup replika, npr. R2) |
> | **W2** | Lokalni server **prosleđuje zahtev za modifikaciju primaru R1** — sve modifikacije se obavljaju isključivo na primarnoj kopiji |
> | **W3** | **Primar R1 obavlja modifikaciju** (x=10) i **šalje promenu svim ostalim kopijama** (R2, R3, R4) |
> | **W4** | Kada backup replike obave ažuriranje, **javljaju primaru da su uspešno ažurirane** (potvrde) |
> | **W5** | Tek nakon toga **primar potvrđuje lokalnom serveru, koji dozvoljava klijentu da nastavi sa radom** |
>
> 💡 **Ključna napomena:** *„Primar može urediti sve dolazne zahteve za modifikacijom na globalno jedinstveni način tako da svi procesi vide sve write operacije u istom redosledu"* — zato ovaj protokol postiže **sekvencijalnu konzistenciju**.
> **Čitanja** se mogu obavljati **lokalno** nad bilo kojom kopijom; samo upisi moraju ići preko primara.

---

### P57. Distribuirani sistem koristi protokol baziran na kvorumu. U sistemu postoji 12 replika. Koje vrednosti Read i Write kvoruma je najbolje odabrati ako se čitanja obavljaju često a ažuriranja retko? Obrazložiti.

`26-JUL · 10b` 🔁 *(`[S·50]`, `[S·64]` — isti tip)*

> **Odgovor:** **N_R = 1, N_W = 12** `[U]` `[S·50]`
>
> **Uslovi koje kvorumi MORAJU zadovoljiti (Giffordov algoritam):**
> 1. **N_R + N_W > N** — sprečava **read-write** konflikte (garantuje da se read i write kvorum uvek preklapaju, pa bar jedan server u read kvorumu ima najnoviju verziju)
> 2. **N_W > N / 2** — sprečava **write-write** konflikte (dva istovremena upisa se moraju preklopiti)
>
> **Računica za N = 12, čitanja česta ⇒ N_R što manji:**
> - `N_R = 1` ⇒ iz uslova 1: `1 + N_W > 12` ⇒ `N_W > 11` ⇒ **N_W = 12**
> - Provera uslova 2: `12 > 12/2 = 6` ✓
>
> **Obrazloženje izbora:** pošto se **čita često**, `N_R = 1` znači da klijent za čitanje kontaktira **samo jedan** server → čitanje je maksimalno brzo i jeftino. Pošto se **ažurira retko**, skupa cena upisa (mora se ažurirati **svih 12 replika**) se plaća retko i ne opterećuje sistem.
>
> 💡 **Suprotan slučaj** (česti upisi, retka čitanja): uzeo bi `N_W = 7` (minimum iz uslova 2) i `N_R = 6`.

---

## TEMA 9 · Uzajamno isključivanje

**Na 3 od 5 rokova** (25-SP·3 · 26-JUN·9,10 · 26-JUL·8)

---

### P58. Da li Ricart & Agrawala algoritam uzajamnog isključivanja koristi logičke časovnike? Kratko obrazložiti odgovor.

`25-SP · 3`

> **Odgovor** `[U]`
>
> **DA, koristi.**
>
> *„Distribuirani algoritam: Ricart & Agrawala … **Koristi logičke časovnike i grupnu komunikaciju.**"*
>
> Kada proces želi da uđe u kritičnu sekciju, generiše poruku koja sadrži **tri stvari**:
> 1. **ID tog procesa**
> 2. **ime željenog resursa**
> 3. **logički časovnik** (Lamportova vremenska markica)
>
> Logički časovnik je **neophodan za razrešavanje sukoba**: kada dva procesa istovremeno traže istu kritičnu sekciju, svaki poredi **vremensku markicu svog zahteva sa markicom primljenog zahteva** — **proces sa manjom vremenskom markicom pobeđuje**. Bez logičkih časovnika ne bi postojao kriterijum za odlučivanje ko ima prednost.

---

### P59. Sistem ima 10 procesa. Koliko je poruka potrebno razmeniti da bi se ušlo u kritičnu sekciju ako se koristi: a) Centralizovani algoritam; b) Ricart–Agrawala algoritam?

`26-JUN · 9` 🔁 `26-JUL · 8` — **DOSLOVNO ISTO PITANJE NA DVA UZASTOPNA ROKA**
*(26-JUL varijanta glasi: „…da bi proces ušao **i napustio** kritičnu sekciju")*

> **Odgovor** `[U]`
>
> **a) Centralizovani algoritam: 3 poruke** — *nezavisno od broja procesa!*
>
> | Poruka | Smer |
> |---|---|
> | `REQUEST` | proces → koordinator |
> | `OK` (grant) | koordinator → proces |
> | `RELEASE` | proces → koordinator (kada napusti KS) |
>
> *„Dobre strane algoritma: jednostavan i lak za implementaciju, **zahteva razmenu samo tri poruke** (request, OK i release)."*
>
> **b) Ricart–Agrawala: 2(n − 1) = 2 · 9 = 18 poruka**
>
> *„Ovaj algoritam zahteva razmenu **2(n-1) poruka** za pristup kritičnoj sekciji (**n−1 zahtev i n−1 potvrda**)."*
> Zahtev se šalje **svim** ostalim procesima (9 poruka) i mora se primiti **OK od svih** (9 poruka).
>
> 💡 **Zapamti razliku:** centralizovani je **konstantan** (3), Ricart–Agrawala **raste linearno** sa brojem procesa.
> **Mane R-A:** greška u bilo kom prenosu blokira ceo sistem; ako jedan proces otkaže, pristup resursu je nemoguć.

---

### P60. Pet procesa jednovremeno traži zahtev za pristup istoj kritičnoj sekciji. Lamportove markice zahteva su P1: 9, P2: 6, P3: 6, P4: 12, P5: 10. Prikazati kako izgledaju baferi svakog procesa ako se koristi Ricart-Agrawala algoritam. Koji zahtevi će odmah biti potvrđeni?

`26-JUN · 10b` 🔁 *(`[S·1]` — isti tip zadatka)*

> **Odgovor** `[U]` `[S·1]`
>
> **Pravilo za remi (markice 6 i 6):** kod jednakih markica pobeđuje proces sa **manjim ID-em** (vidi **P47**) → `(6, P2)` pobeđuje `(6, P3)`.
>
> **Prioritetni redosled (rastuće po `(markica, id)`):**
> `P2(6,2)` → `P3(6,3)` → `P1(9,1)` → `P5(10,5)` → `P4(12,4)`
>
> **Pravilo Ricart-Agrawala:** proces koji je i sam poslao zahtev **šalje OK** onome ko ga pobeđuje, a **smešta u svoj bafer** zahteve onih koje on pobeđuje.
>
> **BAFERI:**
>
> | Proces | Markica | Bafer (zahtevi koje zadržava) | Kome šalje OK |
> |---|---|---|---|
> | **P2** | 6 | **P3, P1, P5, P4** | nikome (pobeđuje sve) |
> | **P3** | 6 | **P1, P5, P4** | P2 |
> | **P1** | 9 | **P5, P4** | P2, P3 |
> | **P5** | 10 | **P4** | P2, P3, P1 |
> | **P4** | 12 | **—** (prazan) | P2, P3, P1, P5 |
>
> **Koji zahtevi će odmah biti potvrđeni:**
>
> **Samo zahtev procesa P2** dobija OK od **sva 4** ostala procesa ⇒ **P2 odmah ulazi u kritičnu sekciju.**
>
> Ostali imaju nepotpune potvrde: P3 ima 3/4, P1 ima 2/4, P5 ima 1/4, P4 ima 0/4.
>
> **Šta se dešava dalje:** kada P2 okonča pristup KS, šalje OK **svim zahtevima iz svog bafera** → tada P3 ima sve potvrde i ulazi u KS, pa P1, pa P5, pa P4.

---

## TEMA 10 · Sinhronizacija fizičkih časovnika — NTP, Kristijan, Berkeley

**Na 3 od 5 rokova** (25-SP·4 · 26-JUN·6,7,8 · 26-JUL·7)

---

### P61. Časovnik na računaru se sinhronizuje korišćenjem NTP protokola. Zabeležena su sledeća vremena: a) vreme na klijentu kada upućuje zahtev serveru: 6:22:15.100; b) vreme prijema zahteva na serveru: 7:05:10.700; c) vreme kada server šalje odgovor: 7:05:10.710; d) vreme na klijentu kada prima odgovor od servera: 6:22:15.250. Na koju vrednost će se postaviti klijentski časovnik?

`25-SP · 4`

> **Odgovor: 7:05:10.780** `[U]`
>
> **Formule** `[U]`:
> - kružno vreme propagacije: **δ = (t₁ − t₀) + (t₃ − t₂)**
> - offset klijentovog časovnika: **θ = [(t₁ − t₀) + (t₂ − t₃)] / 2**
> - tačno vreme: **t₃′ = t₃ + θ**  *(ekvivalentno: `t₃′ = t₂ + δ/2`)*
>
> gde su: `t₀` = klijent šalje, `t₁` = server prima, `t₂` = server šalje, `t₃` = klijent prima.
>
> **Računica:**
>
> | Korak | Vrednost |
> |---|---|
> | `t₁ − t₀` | `7:05:10.700 − 6:22:15.100` = **+2575.600 s** (42 min 55.600 s) |
> | `t₃ − t₂` | `6:22:15.250 − 7:05:10.710` = **−2575.460 s** |
> | **δ** | `2575.600 + (−2575.460)` = **0.140 s** = **140 ms** (kružno kašnjenje) |
> | **θ** | `(2575.600 + 2575.460) / 2` = `5151.060 / 2` = **2575.530 s** |
> | **t₃′** | `6:22:15.250 + 2575.530 s` = `6:22:15.250 + 42min 55.530s` = **7:05:10.780** |
>
> **Kraća provera:** `t₃′ = t₂ + δ/2 = 7:05:10.710 + 0.070 = 7:05:10.780` ✓
>
> ⚠️ **Obavezno spomeni na ispitu:** *„Vreme nikad ne sme da ide unazad! Nema vremeplova."* Ovde klijent kasni pa se sme skočiti napred. Da je dobijeno vreme **manje** od trenutnog, promena bi se vršila **postepeno** — povećanjem holding registra časovnika (usporavanjem časovnika) dok ne dostigne željenu vrednost.

---

### P62. Zašto je u DS teško postići savršenu sinhronizaciju časovnika?

`26-JUN · 6` 🔁 *(`[S·28]`)*

> **Odgovor** `[U]` `[S·28]`
>
> **Osnovni razlog: NE POSTOJI GLOBALNI ČASOVNIK.**
>
> Detaljno:
> 1. **Svaka mašina ima svoj časovnik** — procesi na različitim računarima imaju različitu predstavu o vremenu
> 2. **Drift kristala** — tajmer je kvarcni kristal; brzina oscilacije zavisi od **vrste kristala, načina sečenja i veličine napona**. Ako sistem ima N računara, **svih N kristala će oscilovati sa neznatno različitim brzinama**, pa softverski časovnici postepeno ispadaju iz sinhronizma
> 3. **Komunikaciono kašnjenje** — sve metode sinhronizacije svode se na **razmenu vrednosti časovnika između računara**, a tu je problem što se **kašnjenje ne može tačno izmeriti**; dok poruka sa vremenom stigne, to vreme je već zastarelo
> 4. **Sinhronizacija se mora stalno ponavljati** — tokom vremena časovnici opet izađu iz sinhronizma
>
> ➜ **Zato se često koriste logički časovnici** — nije uvek potrebno apsolutno vreme, već je dovoljno da se **uzajamno zavisni događaji odvijaju u korektnom redosledu**, što je mnogo lakše postići.

---

### P63. Kristijanov algoritam usvaja da je:

`26-JUN · 7` — **pitanje na zaokruživanje.** Odabrati korektan odgovor i obrazložiti ga.

- ❌ **a)** Kašnjenje kroz mrežu tačno poznato
- ✅ **b)** Kašnjenja zahteva i odgovora su približno jednaka
- ❌ **c)** Serverski časovnik je uvek sporiji
- ❌ **d)** Svi klijenti međusobno komuniciraju da bi odredili tačno vreme

> **Odgovor:** **b)** `[U]`
>
> **Obrazloženje:** *„Da bi klijent uskladio svoj časovnik on mora da uzme u obzir propagaciono kašnjenje između klijenta i servera tačnog vremena i to određuje razliku **(T1 − T0)/2**, gde je T0 trenutak kada je klijent uputio zahtev, a T1 trenutak kada je klijent primio odgovor."*
>
> **Deljenje kružnog vremena sa 2** ima smisla samo pod pretpostavkom da je kašnjenje **simetrično** — da poruka zahteva i poruka odgovora putuju **približno jednako dugo**. To je odgovor b).
>
> - **a ❌** — kašnjenje **nije** tačno poznato; da jeste, ne bi bilo potrebe za procenom deljenjem sa 2 (i to je upravo glavni izvor greške algoritma)
> - **c ❌** — nema takve pretpostavke; server tačnog vremena je referenca (ima WWV prijemnik za UTC), a ne „sporiji"
> - **d ❌** — to opisuje **Berkeley** algoritam (interna sinhronizacija prozivkom svih mašina). Kod Kristijana je **server pasivan** — samo daje info o tačnom vremenu, a klijenti međusobno **ne** komuniciraju

---

### P64. Zašto Berkeley algoritam šalje korekciju vremena umesto tačnog vremena?

`26-JUN · 8`

> **Odgovor** `[U]`
>
> Berkeley obezbeđuje **internu sinhronizaciju** mašina unutar DS-a, **bez potrebe za eksternim izvorom tačnog vremena**. Server vremena obavlja **periodičnu prozivku svake mašine**, izračunava **srednje vreme** i saopštava svim mašinama **kako da podese svoje časovnike**.
>
> **Razlozi za slanje korekcije (a ne apsolutnog vremena):**
>
> 1. **Vreme ne sme da ide unazad** — *„Vreme nikad ne sme da ide unazad! Nema vremeplova."* Ako bi se slalo apsolutno vreme, mašini koja žuri bi ono bilo manje od trenutnog i časovnik bi skočio unazad, što bi narušilo redosled događaja. Slanjem **korekcije** svaka mašina zna **koliko i u kom smeru** da podesi — a ako je korekcija negativna, primenjuje je **postepeno** (usporavanjem časovnika povećanjem holding registra).
>
> 2. **Eliminiše se komunikaciono kašnjenje** — dok apsolutno vreme putuje kroz mrežu, ono zastari. **Relativna korekcija ostaje ispravna** bez obzira na to kada tačno stigne.
>
> 3. **Cilj Berkeley algoritma nije globalno tačno vreme, već zajedničko vreme.** *„Ovaj metod ne zahteva izvor tačnog vremena, već da se u okviru jednog DS-a usklade časovnici. To je sasvim zadovoljavajuće jer je bitno da računari jednog DS-a imaju istu predstavu o vremenu, ne mora to vreme da bude baš globalno tačno."*

---

### P65. Koje informacije o vremenu se nalaze u poruci koja stiže od NTP servera? Kako to klijent koristi da podesi svoj časovnik?

`26-JUL · 7`

> **Odgovor** `[U]`
>
> **Vremenska polja u NTP poruci (4):**
>
> | Polje | Značenje |
> |---|---|
> | **Reference timestamp** | vreme kada je sistemski časovnik **poslednji put bio postavljen/korigovan** |
> | **Origin timestamp** (`t₀`) | kada je poruka **upućena od klijenta ka serveru** |
> | **Receive timestamp** (`t₁`) | kada je poruka **stigla od klijenta na server** |
> | **Transmit timestamp** (`t₂`) | kada **server šalje odgovor** klijentu |
>
> *(Uz njih zaglavlje sadrži i: LI — prestupna sekunda, VN — verzija, Mode — režim rada, **Stratum** — nivo servera, Poll, Precision, **Root delay** — kružno vreme do referentnog servera, Reference ID.)*
>
> **Kako klijent koristi ove informacije:**
> Klijent beleži i **`t₃`** — trenutak prijema odgovora — pa računa:
>
> - kružno vreme propagacije: **δ = (t₁ − t₀) + (t₃ − t₂)**
> - offset svog časovnika: **θ = [(t₁ − t₀) + (t₂ − t₃)] / 2**
> - novo (tačno) vreme: **t₃′ = t₃ + θ**  *(ili ekvivalentno `t₂ + δ/2`)*
>
> Ako je dobijeno vreme **veće** od trenutnog, časovnik se postavlja direktno; ako je **manje**, korekcija se vrši **postepeno** (usporavanjem časovnika), jer vreme ne sme da ide unazad.
>
> **Dodatno o NTP-u:** protokol **aplikativnog nivoa**, na transportnom koristi **UDP**, osluškuje na **portu 123**, definisan standardom **RFC 5905**. Serveri se hijerarhijski organizuju u **stratume** (nivo 0 = atomski časovnici, nivo 1 = direktno povezani na izvor, itd.). Podržava **3 režima**: simetrični (najpreciznije, za master servere), **klijent-server** (sličan Kristijanovom — ovo je režim iz zadatka) i multicast (brze LAN mreže).

---

## TEMA 11 · Hadoop / HDFS

**Na 3 od 5 rokova** (2024·8b · 25-SP·12 · 25-NP·14) — **NE pojavljuje se ni na jednom roku 2026!**

---

### P66. Navesti sve demone u Hadoop klasteru, objasniti njihove uloge kao i gde se izvršavaju u Hadoop klasteru. Šta predstavlja blok i koje su prednosti korišćenja blokova kod HDFS? Pretpostavimo da je fajl veličine 514MB sačuvan na HDFS-u. Ako je veličina bloka 64MB i podrazumevani faktor replikacije 4, koliki je ukupni broj blokova i kolika je veličina svakog od njih?

`2024 · 8b` 🔁 *(`[S·20]`, `[S·52]`)*

> **Odgovor** `[H]` `[U]` `[S·20]`
>
> **DEMONI** — postoje **dve vrste**: HDFS demoni i MapReduce demoni.
>
> **HDFS demoni:**
>
> | Demon | Koliko | Gde se izvršava | Uloga |
> |---|---|---|---|
> | **NameNode (NN)** | **samo jedan** | na **glavnom (master)** čvoru | Centralni kontroler HDFS-a. Održava **fajl sistem namespace**; čuva **metapodatke** (kako su fajlovi podeljeni na blokove, koji slave čvorovi čuvaju koje blokove, faktor replikacije); nadgleda ponašanje DataNode-ova i koordiniše pristup podacima. **NE čuva same podatke** nad kojima se vrši obrada. Vodi računa da svaki blok zadovolji definisani faktor replikacije. Podatke drži u **glavnoj memoriji**. |
> | **DataNode (DN)** | **više** | na **slave** čvorovima | Odgovoran za **čuvanje podataka u blokovima**, primanje naredbi od NN i davanje informacija NN-u. Šalje **heartbeat svake 3 sekunde**; **svaki 10. heartbeat je izveštaj o blokovima** (koje blokove čuva). |
> | **Secondary NameNode** | jedan | na **serveru na kome NIJE NN** | **NIJE backup NN.** Radi konkurentno sa NN kao pomoćni demon; u regularnim intervalima **preuzima edit log od NN, spaja ga lokalno sa starim checkpoint-om** i formira **novi checkpoint** koji vraća NN-u. NN ga koristi pri sledećem restartu (skraćuje vreme restarta). |
>
> ⚠️ **MapReduce demoni** su u prezentaciji `GFS i HDFS 2024.pdf` samo **navedeni kao kategorija** („Postoje dve vrste demona Hadoop-a: HDFS demoni i MapReduce demoni") — **detalji o njima nisu u ovim fajlovima.** Ako profesor traži i njih, taj deo treba dopuniti sa predavanja/snimka.
>
> **ŠTA JE BLOK:** Hadoop koristi blokove da sačuva fajl ili delove fajla. **Hadoop blok je fajl na fajl sistemu koji se nalazi u osnovi** (HDFS radi nad postojećim lokalnim fajl sistemom svakog čvora). Podrazumevana veličina je **64MB** (u novijim verzijama **128MB**).
>
> **PREDNOSTI BLOKOVA (3):**
> 1. Imaju **fiksnu veličinu**, pa je lako izračunati koliko će ih stati na disk
> 2. Raspodelom na više čvorova omogućavaju da **fajl bude veći nego bilo koji pojedinačni disk** u klasteru
> 3. **Blokovi se repliciraju na više čvorova**, što omogućava HDFS-u da bude **otporan na greške (Fault Tolerant)**
>
> **RAČUNICA (514MB, blok 64MB, faktor replikacije 4):**
>
> Broj blokova **po jednoj kopiji**: `⌈514 / 64⌉ = ⌈8.03⌉ = **9 blokova**`
> - **8 punih blokova × 64MB** = 512MB
> - **1 blok × 2MB** (ostatak: 514 − 512 = 2MB) — *poslednji blok zauzima samo onoliko koliko mu treba*
>
> **UKUPAN BROJ BLOKOVA** (sa replikacijom 4): `9 × 4 = **36 blokova**`
>
> **Veličine:** **32 bloka po 64MB** + **4 bloka po 2MB**
> **Ukupno zauzeće:** `514MB × 4 = 2056MB`

---

### P67. Sledeće pitanje odnosi se na HDFS. Tačna tvrđenja obeležite sa T(tačno) a netačna sa F(netačno). Obrazložiti.

`25-SP · 12` — **T/F pitanje**

| # | Tvrdnja | |
|---|---|---|
| **a** | Faktor replikacije može se konfigurisati na nivou klastera i takođe na nivou fajla. | ✅ **T** |
| **b** | Izveštaj o blokovima sa svakog DataNode čvora sadrži listu svih blokova koji su smešteni na tom DataNode čvoru. | ✅ **T** |
| **c** | Korisnički podaci se skladište na lokalnom fajl sistemu DataNode čvorova. | ✅ **T** |
| **d** | DataNode čvor zna kojim fajlovima pripadaju blokovi koji su na njemu smešteni. | ❌ **F** |

> **Obrazloženja** `[H]`
>
> - **a) T** — *„Faktor replikacije se može promeniti u Hadoop konfiguraciji ili čak postaviti faktor za **svaki individualni fajl**."* (podrazumevano je 3)
> - **b) T** — *„DataNode-ovi šalju heartbeat-ove svake tri sekunde i **svaki deseti heartbeat je izveštaj o blokovima, gde DN obaveštava NN koje blokove čuva**."*
> - **c) T** — *„HDFS funkcioniše **nad postojećim fajl sistemom** na svakom čvoru Hadoop klastera"* i *„Hadoop blok je **fajl na fajl sistemu koji se nalazi u osnovi**."*
> - **d) F** — **NameNode** je taj koji zna kako su fajlovi podeljeni na blokove i gde su blokovi locirani. DataNode samo čuva blokove i prijavljuje **koje blokove ima**, ne i **kom fajlu pripadaju**. *„NN … ima informaciju o blokovima koji čine fajl i gde su ti blokovi locirani u klasteru."*

---

### P68. Koji fajlovi se trajno pamte na lokalnom disku NameNode-a? Koje informacije se ne pamte trajno na lokalnom disku, a neophodne su prilikom restarta NameNode-a? Odakle se dobijaju te informacije?

`25-NP · 14a` 🔁 *(`[S·26]`)*

> **Odgovor** `[H]` `[U]` `[S·26]`
>
> **Fajlovi koji se TRAJNO pamte na lokalnom disku NameNode-a (2):**
>
> | Fajl | Sadržaj |
> |---|---|
> | **`fsimage`** (Checkpoint image / **Namespace fajl**) | **snapshot metapodataka fajl sistema** — file namespace i preslikavanje fajl → blokovi |
> | **`edit log`** | fajl u kome se **trajno čuvaju sve promene** koje se dešavaju u HDFS NameNode-u |
>
> *(Trajno čuvanje metapodataka NN-a na lokalnom fajl sistemu naziva se **Checkpoint**.)*
>
> **Šta se NE pamti trajno, a neophodno je pri restartu:**
> **Preslikavanje blokova na DataNode čvorove** — tj. **na kom DataNode-u se koji blok fizički nalazi**. NameNode ove podatke drži **samo u glavnoj memoriji**, jer se u velikim sistemima često menjaju.
>
> **Odakle se dobijaju:** iz **izveštaja o blokovima (block report)** koje **DataNode-ovi šalju NameNode-u** — svaki 10. heartbeat je izveštaj u kome DN prijavljuje koje blokove čuva. Nakon restarta NN rekonstruiše mapu blok→čvor iz tih izveštaja.
>
> **Postupak restarta:** *„Samo u slučaju kada se restartuje NN, promene iz **edit loga** se primenjuju na poslednju verziju **Namespace fajla (fsimage)** da bi se dobila nova verzija metapodataka fajl sistema."*
> Pošto se u klasterima retko vrši restart, edit log može mnogo narasti pa restart traje dugo → zato postoji **Secondary NameNode** koji periodično spaja edit log sa checkpoint-om.

---

### P69. Sledeće pitanje odnosi se na HDFS. Tačna tvrđenja obeležite sa T(tačno) a netačna sa F(netačno).

`25-NP · 14b` — **T/F pitanje** *(drugi set tvrdnji)*

| # | Tvrdnja | |
|---|---|---|
| **a** | Svaki fajl je podrazumevano podeljen na 32 MB po defaultu | ❌ **F** |
| **b** | NameNode čuva podatke fajla u obliku blokova podataka | ❌ **F** |
| **c** | HDFS se koristi za scenarije koji zahtevaju istovremeno upisivanje u istu datoteku | ❌ **F** |
| **d** | Faktor replikacije se može konfigurisati na nivou klastera (podrazumevano je podešeno na 3) i takođe na nivou fajla | ✅ **T** |

> **Obrazloženja** `[H]`
>
> - **a) F** — podrazumevana veličina bloka je **64MB** (u novijim verzijama Hadoop-a **128MB**), nikako 32MB
> - **b) F** — **NameNode NE čuva podatke fajla**, već samo **metapodatke**: *„NN ne čuva podatke nad kojima se vrši obrada, on samo ima informaciju o blokovima koji čine fajl i gde su ti blokovi locirani u klasteru."* Podatke u blokovima čuvaju **DataNode-ovi**.
> - **c) F** — HDFS ima **jedan writer i više reader-a** (write-once, read-many model); nije namenjen istovremenom upisivanju u istu datoteku
> - **d) T** — isto kao P67·a, uz eksplicitno navedeno da je podrazumevano **3**

---

# 🟢 UČESTALOST 2/5

---

## TEMA 12 · Distribuirani fajl sistemi (DFS)

**Na 2 od 5 rokova** (2024·8a · 25-NP·13)

---

### P70. Kada smo govorili o DFS rekli smo da server može biti projektovan kao statefull ili stateless. Pored svakog od tvrđenja staviti oznaku tačno (T) ili netačno (F).

`2024 · 8a` — **T/F pitanje** 🔁 *(`[S·25]` — isti set tvrdnji)*

| # | Tvrdnja | |
|---|---|---|
| **1** | Implementacija klijentske strane može biti komplikovanija sa statefull serverom | ✅ **T** |
| **2** | Zaključavanje fajla je teško implementirati kod stateless servera | ✅ **T** |
| **3** | Kod statefull servera, svaki klijentski zahtev mora da sadrži kompletnu informaciju o zahtevu (npr. ime fajla, offset, itd.) | ❌ **F** |
| **4** | Lakše je izboriti se sa greškama kod stateless nego kod statefull servera | ✅ **T** |

> **Obrazloženja** `[U]` `[S·25]`
>
> **Podsetnik na definicije:**
> - **Stateless server** — ne pamti ništa o tome koji klijent pristupa kom fajlu. **Sve informacije potrebne da bi se opslužio klijent, mora da pruži sam klijent** pri svakom pristupu.
> - **Statefull server** — pamti koji je klijent otvorio koji fajl.
>
> - **1) T** — kod statefull servera klijent mora dodatno da vodi računa o oporavku od otkaza servera (ponovno uspostavljanje stanja, reotvaranje fajlova), što komplikuje klijentsku stranu. *(`[S·25c]` navodi T.)*
> - **2) T** — *„Kod statefull servera može se izvršiti **file locking** — da se spreči jednovremena modifikacija istog fajla od više klijenata."* Kod stateless servera server ne zna ko je šta otvorio, pa zaključavanje nema na čemu da počiva.
> - **3) F** — to važi za **STATELESS** server, ne statefull. *„Mane stateless-a: poruke koje se razmenjuju da bi se pristupilo fajlu su mnogo duže — u svakoj poruci klijent mora da navede ime fajla i komandu koja treba da se izvrši."*
> - **4) T** — *„Stateless je **otporniji na otkaz servera**, jer se nikakve informacije neće izgubiti otkazom servera."* Kod statefull-a: *„Teže se postiže otpornost na otkaze. Ako server otkaže, informacija o tome koji klijent je pristupio kom fajlu biće izgubljena."*
>
> **Prednosti/mane u kratkom:**
>
> | | **Stateless** | **Statefull** |
> |---|---|---|
> | Otpornost na otkaz servera | ✅ bolja | ❌ lošija |
> | Dužina poruka | ❌ duže | ✅ kraće |
> | Performanse | ❌ lošije | ✅ bolje |
> | Konzistencija / file locking | ❌ nemoguće | ✅ lakše |
> | Obaveštavanje klijenata o promeni fajla | ❌ ne može | ✅ može |

---

### P71. Koji modeli pristupa udaljenom fajlu postoje? Navesti prednosti i nedostatke svakog modela.

`25-NP · 13a`

> **Odgovor** `[U]`
>
> Postoje **2 modela**:
>
> **1. Upload/Download model**
> Jedini servisi za pristup su **read** i **write**: **read** preuzima (download-uje) fajl sa servera, pamti ga na klijent mašini, sve operacije se obavljaju **lokalno**, a kada klijent završi, **vraća fajl na server (upload)**.
> - ✅ **Prednosti:** jednostavan model; **dobre performanse**, jer se operacije obavljaju lokalno pa **nema mrežnog saobraćaja** tokom rada
> - ❌ **Nedostaci:** šta ako klijent **nema dovoljno prostora** da zapamti ceo fajl? Šta ako mu **nije potreban ceo fajl** nego samo deo? Šta ako neki **drugi klijent istovremeno želi da modifikuje** isti fajl?
>
> **2. Model udaljenog pristupa**
> **Sve operacije nad fajlom se obavljaju na udaljenoj mašini** (`open, close, read, write, read byte, write byte`); fajl se **ne pomera sa servera**. Sve se realizuje pomoću **RPC mehanizma**.
> - ✅ **Prednosti:** **lakše je implementirati deljenje fajlova** — ako jedan klijent modifikuje fajl, ta promena je **svima odmah vidljiva**
> - ❌ **Nedostaci:** **sve vreme se zahteva pristup serveru** ⇒ može nastati **zagušenje mreže** i **preopterećenje servera**; **performanse (brzina) su gore** od prethodnog modela

---

### P72. Koje semantike deljenja fajlova postoje? Objasniti njihove karakteristike.

`25-NP · 13b`

> **Odgovor** ⚠️
>
> **Napomena o izvorima:** u fajlovima ovog foldera (`usmeni.pdf`, `DS BELO ZLATO skripta.pdf`, prezentacije) **eksplicitan spisak semantika deljenja fajlova (UNIX / sesijska / immutable / transakciona semantika) NIJE pronađen** — dokument `usmeni.pdf` se na tom mestu prekida rečenicom *„IMA JOŠ, NEGO ME MRZI VIŠE DA SLUŠAM OVO ☹"*. **Ovaj deo mora se dopuniti sa predavanja/snimka.**
>
> **Ono što JESTE u fajlovima i što treba iskoristiti za odgovor:**
>
> Iz **DFS zahteva** `[U]` — DFS mora obezbediti **transparentnost konkurencije**: *„fajlu može pristupati više korisnika, koji mogu i da modifikuju fajl, pa se tada mora obezbediti da **svi procesi imaju jedinstven pogled na stanje tog fajla** (svi procesi vide izmene)."*
>
> Iz **modela pristupa** `[U]`:
> - **Model udaljenog pristupa** — *„ako jedan klijent modifikuje fajl, ta promena je **svima odmah vidljiva**"* ⇒ odgovara **UNIX semantici** (svaka operacija je odmah vidljiva svima)
> - **Upload/Download model** — klijent radi nad **lokalnom kopijom** i vraća je tek na kraju ⇒ odgovara **sesijskoj semantici** (promene su vidljive tek po zatvaranju fajla)
>
> Iz **stateless/statefull** `[U]`:
> - **Statefull** server *„zna koji klijenti pristupaju kom fajlu i može da ih obavesti da je fajl promenjen"* i podržava **file locking**
> - **Stateless** server *„nema načina da obavesti ostale klijente da je fajl modifikovan"* ⇒ **klijent sam vodi računa o konzistenciji**
>
> 💡 **Savet za ispit:** ako se pitanje pojavi, gradi odgovor oko para **UNIX semantika ↔ model udaljenog pristupa** i **sesijska semantika ↔ upload/download model**, pa dodaj ulogu stateless/statefull servera. Za pun odgovor proveri predavanje.

---

## TEMA 13 · Chord / P2P sistemi

**Na 2 od 5 rokova** (25-SP·8 · 26-JUN·15)

---

### P73. U Chord prstenu koji koristi 5-to bitne identifikatore prisutni su čvorovi sa identifikatorima 1, 4, 9, 11, 14, 18, 20, 21, 28. Pretpostavimo da se čvor sa identifikatorom 15 pridružuje Chord prstenu tako što kontaktira čvor sa identifikatorom 20. Redom napisati korake koji opisuju pridruživanje čvora 15 prstenu.

`25-SP · 8` 🔁 *(`[S·5b]` — DOSLOVNO isti zadatak sa istim brojevima!)*

> **Odgovor** `[U]` `[S·5b]`
>
> *(Prsten veličine 2⁵ = 32, ID-evi 0–31. Fajl sa ključem k dodeljuje se prvom čvoru sa `ID ≥ k` u smeru kazaljke na satu — `succ(k)`.)*
>
> **Koraci:**
>
> 1. **N15 kontaktira N20** i poziva funkciju **`join(N20)`**
> 2. **N20 pronalazi neposrednog sledbenika za čvor N15** → to je **N18** (prvi aktivni čvor sa ID ≥ 15)
> *(Funkcija `join` obaveštava samo čvor N15 ko mu je sledbenik; **ostale čvorove ne obaveštava** o prisustvu N15)*
> 3. **N15 postavlja N18 za svog sledbenika** i **obaveštava N18 da je on sada njegov novi prethodnik**
> 4. **N18 postavlja N15 za svog prethodnika**
> 5. **Ključevi koji su bili dodeljeni N18, a manji su ili jednaki 15** (tj. iz opsega (14, 15]) **prebacuju se na N15**
> 6. **Prethodnik čvora N15 se dodaje TEK KASNIJE**, kada **N14 pokrene protokol za stabilizaciju**: N14 pita svog sledbenika (za koga još uvek misli da je N18): **„Ko je tvoj prethodnik?"**
> 7. **N18 odgovara: „N15"** ⇒ N14 shvata da je dodat novi čvor i **postavlja N15 za svog sledbenika** i obaveštava ga o tome porukom **„Ja sam tvoj prethodnik"**
> 8. **N15 postavlja N14 za svog prethodnika**
>
> 💡 **Ključna ideja:** *„Da bi pointeri na prethodnika i sledbenika uvek bili validni, svaki čvor u Chord mreži **periodično izvršava protokol za stabilizaciju** da bi otkrio novopridružene čvorove ili one koji su izašli iz mreže."* Finger tabele se ažuriraju kasnije — ako one nisu validne, to **samo usporava** pronalaženje sadržaja, ali je pronalaženje i dalje moguće preko sledbenika i prethodnika.

---

### P74. U Chord prstenu koji koristi 6-to bitne identifikatore prisutni su čvorovi sa identifikatorima 4, 12, 20, 35, 50. Faktor replikacije je 3. Ubačen je fajl sa identifikatorom 18. Ko je primarni vlasnik fajla sa identifikatorom 18? U kojim čvorovima se nalaze replike? Šta se dešava ako čvor 20 otkaže? Ko je novi primar za fajl sa identifikatorom 18? Gde se sada nalaze kopije tog fajla?

`26-JUN · 15a`

> **Odgovor** `[U]` ⚠️
>
> *(Prsten veličine 2⁶ = 64, ID-evi 0–63. Aktivni čvorovi: **4, 12, 20, 35, 50**.)*
>
> **1. Ko je primarni vlasnik fajla 18?**
> Pravilo: *„Fajl sa ključem k se dodeljuje peer čvoru sa ID-em `ID ≥ k`, gledano u smeru kazaljke na satu."*
> Prvi čvor sa `ID ≥ 18` → **ČVOR 20**. Dakle **primarni vlasnik je čvor 20** = `succ(18)`.
>
> **2. Gde su replike (faktor replikacije 3)?**
> Kopije se smeštaju na **primar + njegova 2 naredna sledbenika** iz liste sledbenika:
> **čvor 20 (primar), čvor 35, čvor 50**
>
> **3. Šta se dešava ako čvor 20 otkaže?**
> *„Ako čvor N primeti da je njegov neposredni sledbenik otkazao (ne odgovara na heartbeat poruku „Are you alive?"), on zamenjuje pointer pointerom prvog živog čvora u svojoj listi."* Takođe: *„Kada čvor napušta mrežu, svi ključevi koji su mu bili dodeljeni se **prebacuju njegovom sledbeniku**."*
>
> - Čvor **12** (prethodnik) detektuje otkaz i postavlja **35** za svog novog sledbenika
> - Ključevi iz opsega (12, 20] — uključujući **fajl 18** — prelaze na **čvor 35**
> - Broj kopija pada na 2, pa se pokreće **re-replikacija** da se vrati faktor 3
>
> **4. Ko je novi primar za fajl 18?** → **ČVOR 35** (`succ(18)` nakon otkaza 20)
>
> **5. Gde su sada kopije?** → **čvor 35 (novi primar), čvor 50, i čvor 4**
> *(prsten se zatvara — posle 50 sledeći aktivni čvor u smeru kazaljke je **4**)*
>
> ⚠️ **Napomena o izvorima:** materijali eksplicitno opisuju **listu pointera na sledbenike** kao mehanizam oporavka od otkaza i prenos ključeva na sledbenika, ali **ne opisuju izričito „faktor replikacije r ⇒ r uzastopnih sledbenika"**. To je standardni Chord mehanizam koji sledi iz opisanog; navedi ga uz obrazloženje preko liste sledbenika.

---

### P75. Chord prsten ima 2²⁰ mogućih identifikatora. Mreža ima samo 10.000 čvorova. Koliki je očekivani broj koraka za pronalaženje željenog sadržaja?

`26-JUN · 15b` — **pitanje na zaokruživanje**

- ❌ **a)** O(2²⁰)
- ❌ **b)** O(10000)
- ✅ **c)** O(log 10000)
- ❌ **d)** O(20)

> **Odgovor:** **c) O(log 10000)** `[U]` `[S·11]`
>
> **Obrazloženje:**
> - Bez **finger tabele**, sukcesivnim kontaktiranjem naslednika, broj koraka je **O(n)** gde je `n` broj **čvorova** → to bi bilo b) O(10000)
> - *„Da bi se smanjio broj koraka, svaki peer pamti ne samo ko su mu prethodnici i sledbenici nego ima i **FINGER TABELU** u kojoj pamti m najbližih suseda. Tako se broj koraka smanji na **O(log n)**, što je dosta manje."*
> - Ovde je `n` = **broj aktivnih čvorova = 10.000**, a **ne** broj mogućih identifikatora ⇒ **O(log 10000)** ≈ 13 koraka
>
> - **a ❌** — 2²⁰ je veličina **prostora identifikatora**, ne broj čvorova; složenost ne zavisi od njega
> - **d ❌** — O(20) = O(m) = broj vrsta finger tabele; to je **najgori slučaj / gornja granica**, a pitanje traži **očekivani** broj koraka
>
> 💡 *„Broj koraka za pronalaženje željenog sadržaja je O(n), pa što je n veće, složenost je veća."* → sa finger tabelom **O(log n)**.

---

## TEMA 14 · Java RMI i udaljeni objekti

**Na 2 od 5 rokova** (26-JUN·4a · 26-JUL·4)

---

### P76. Objasniti ulogu RMI Registry-ja i proces registracije udaljenog objekta.

`26-JUL · 4a`

> **Odgovor** `[P2]` `[U]`
>
> **Uloga:** **RMI registar je BINDER za Java RMI.** *„Omogućava serveru da objavi uslugu i klijentu da dobije stub za pristupanje njoj."* U binderu postoji **tabela sa preslikavanjem tekstualnog imena u referencu udaljenog objekta** `[imeObjekta, referencaObjekta]`.
>
> Pristupa mu se putem metoda klase **`Naming`**, čiji metodi kao argument uzimaju **URL-formatiran string**: `//imeRačunara:port/nazivObjekta`
>
> **Proces registracije i korišćenja (6 koraka):**
> 1. U okviru **serverske aplikacije** registruje se udaljeni objekat u RMI registru **pod određenim imenom** — metodama **`bind` / `rebind`** klase `Naming` (`rebind` zamenjuje već postojeće povezivanje). Vrednost vezana za ime je **referenca na udaljeni objekat**. Bind/rebind se vrši za **svaki serverski objekat** koji se registruje, pri čemu se svaki identifikuje svojim **logičkim imenom**.
> 2. Kada **klijentska aplikacija** želi da pristupi udaljenom objektu, **kontaktira RMI registar sa imenom objekta** (metoda **`lookup`**) i dobija **referencu udaljenog objekta**, koja se koristi prilikom instanciranja **stub-a** na klijentskoj strani.
> 3. Klijent poziv metoda udaljenog objekta **upućuje stub-u** — sintaksa za udaljeni poziv je **identična lokalnoj**.
> 4. **Stub serijalizuje** informacije potrebne za poziv (**ID metode i ulazne parametre**) i šalje ih **skeletonu** u poruci.
> 5. **Skeleton** prima poruku, **deserijalizuje** podatke, prosleđuje poziv objektu koji implementira metod; metod se izvršava i generiše rezultat; skeleton **serijalizuje povratnu vrednost** i šalje je stub-u.
> 6. **Stub** prima poruku, **deserijalizuje** povratnu vrednost i vraća rezultat klijentu.
>
> *(Klasa udaljenog objekta na serveru implementira udaljeni interfejs i **nasleđuje `UnicastRemoteObject`**; `super()` poziva konstruktor koji obavlja inicijalizacije da bi server mogao da čeka i uslužuje zahteve.)*

---

### P77. Dat je URL //server.etf.rs:2500/Kalkulator koji se koristi za pronalaženje udaljenog objekta u Java RMI. Objasniti značenje svakog njegovog elementa.

`26-JUL · 4b`

> **Odgovor** `[P2]`
>
> Metodi klase **`Naming`** uzimaju kao argument **URL-formatiran niz u obliku `//imeRačunara:port/nazivObjekta`**. Za `//server.etf.rs:2500/Kalkulator`:
>
> | Element | Vrednost | Značenje |
> |---|---|---|
> | **`//`** | — | oznaka početka URL-a za RMI registar |
> | **`imeRačunara`** | `server.etf.rs` | **ime (host) računara na kome se izvršava RMI registar**, tj. mašine na kojoj je server registrovao svoj udaljeni objekat |
> | **`port`** | `2500` | **broj porta na kome sluša RMI registar** na tom računaru *(podrazumevani port RMI registra je 1099; ovde je eksplicitno naveden 2500)* |
> | **`nazivObjekta`** | `Kalkulator` | **logičko (tekstualno) ime pod kojim je udaljeni objekat registrovan** u RMI registru. To je ključ u tabeli `[imeObjekta, referencaObjekta]` — preko njega `lookup` vraća referencu udaljenog objekta |
>
> **Kako se koristi:**
> ```java
> // server:
> Naming.rebind("//server.etf.rs:2500/Kalkulator", objekat);
> // klijent:
> IKalkulator k = (IKalkulator) Naming.lookup("//server.etf.rs:2500/Kalkulator");
> ```

---

### P78. Navesti i objasniti načine prosleđivanja objekata u Java RMI. Objasniti razlike koje postoje između navedenih načina prosleđivanja.

`26-JUL · 4c`

> **Odgovor** `[P2]`
>
> **Dva načina prosleđivanja parametara kod poziva udaljenog metoda:**
>
> **1. PO VREDNOSTI (by value)** — primitivni tipovi podataka i **serijalizovani objekti** (klase koje implementiraju `java.io.Serializable`)
> *„Serijalizovani objekat: objekat čije se stanje može razlikovati na različitim lokacijama, prenosi se po vrednosti; objekat se **serijalizuje, šalje primaocu i deserijalizuje** kako bi se izgradila **lokalna kopija**. Kada se objekat prenosi prema vrednosti, u procesu primaocu se **kreira NOVI objekat**. Metode ovog novog objekta mogu se pozivati **lokalno**, što može rezultirati **različitim stanjem novog objekta u odnosu na stanje originalnog objekta** u procesu pošiljaocu."*
>
> **2. PO UDALJENOJ REFERENCI (by remote reference)** — **udaljeni objekti** (klase koje implementiraju `java.rmi.Remote`)
> *„Objekat koji je vezan za lokaciju u kojoj se izvršava (server) prenosi se putem udaljene reference: **njegov STUB se prosleđuje drugoj strani**."*
>
> **RAZLIKE:**
>
> | | **Po vrednosti** | **Po udaljenoj referenci** |
> |---|---|---|
> | Šta se prenosi | **kopija objekta** (serijalizovana) | **stub** (predstavnik objekta) |
> | Gde objekat živi | kreira se **NOVI objekat** kod primaoca | objekat ostaje **na originalnoj mašini** |
> | Poziv metoda | izvršava se **lokalno** kod primaoca | izvršava se **udaljeno**, preko mreže |
> | Stanje | dve **nezavisne** kopije — stanja se mogu **razići** | **jedno jedinstveno stanje** — svi vide iste promene |
> | Uslov | klasa implementira `Serializable` | klasa implementira `Remote` |
>
> 💡 **Pravilo za udaljene metode:** svaka udaljena metoda **mora biti deklarisana da baca `RemoteException`** (radi rukovanja neuspelim komunikacijama), **vraća samo 1 rezultat** i ima 0, 1 ili više ulaznih parametara.

---

### P79. Šta je tačno u slučaju reference udaljenog objekta?

`26-JUN · 4a` — **pitanje na zaokruživanje** 🔁 *(`[S·75a]`)*

- ⚠️ **A.** Referenca udaljenog objekta omogućava pristup metodama objekta kao da je lokalni
- ❌ **B.** Referenca udaljenog objekta sadrži direktnu memorijsku adresu udaljenog objekta
- ✅ **C.** Stub koristi referencu da usmeri poziv preko mreže ka pravom objektu
- ❌ **D.** Udaljene reference ne mogu biti serijalizovane ni prosleđene drugim procesima

> **Odgovor:** **C)** `[U]` `[P2]`
>
> **Šta je referenca udaljenog objekta:** *„Kada klijent pozove metod udaljenog objekta, poruka mora da specificira objekat čiji se metod poziva, i to radi pomoću **jedinstvenog ID-a udaljenog objekta, tj. preko reference udaljenog objekta**."* Referenca **mora da garantuje svoju jedinstvenost u DS-u**, pa se gradi kao:
>
> **`[IP adresa računara + broj porta procesa koji je kreirao objekat + vreme kreiranja + lokalni broj objekta]`**
>
> (ili može sadržati informacije o interfejsu udaljenog objekta, kao što je njegovo ime).
>
> - **C ✅** — *„Proxy pakuje parametre u poruku i prosleđuje poruku serveru (**poruka sadrži referencu udaljenog objekta, id metode i parametre poziva**)."* Referenca je upravo ono što stub koristi za usmeravanje poziva.
> - **B ❌** — referenca **NE sadrži memorijsku adresu**, već mrežni identifikator (IP + port + vreme + lokalni broj). Memorijska adresa ne bi imala smisla u drugom adresnom prostoru.
> - **D ❌** — **mogu** se prosleđivati: *„Objekat koji je vezan za lokaciju u kojoj se izvršava prenosi se putem **udaljene reference: njegov stub se prosleđuje drugoj strani**."* (vidi P78)
> - ⚠️ **A** — tvrdnja jeste u duhu **pristupne transparentnosti** (*„Sintaksa za udaljeni poziv je identična lokalnoj"*), ali to je zasluga **stub-a/proxy-ja**, a ne same reference. Referenca je samo **identifikator**. Zato je **C precizniji odgovor**; ako se traži više odgovora, A se može zaokružiti kao dodatni.

---

# ⚪ UČESTALOST 1/5

*Pojavile su se samo jednom, ali su „zrele" da se ponove — posebno one iz 2026.*

---

### P80. U distribuiranom sistemu sa 5 procesa, P1 P2 P3 P4 P5, za izbor koordinatora koristi se Bully algoritam. P5 je koordinator. Proces P2 detektuje otkaz koordinatora. Prikazati sve poruke koje će se razmeniti do izbora novog koordinatora.

`26-JUN · 11` *(1/5)* 🔁 *(`[S·7]` — objašnjenje algoritma)*

> **Odgovor** `[U]` `[S·7]`
>
> **Polazne pretpostavke:** svaki proces ima **jedinstven ID**; svaki proces **zna ID-eve ostalih**, ali **ne zna da li je odgovarajući proces živ**. Cilj je pronaći **aktivni proces sa NAJVEĆIM ID-em** i proglasiti ga koordinatorom.
>
> **Razmena poruka (P5 je otkazao):**
>
> | # | Poruka | Od → Ka | Ishod |
> |---|---|---|---|
> | 1 | **ELECTION** | P2 → **P3, P4, P5** | P2 šalje svim procesima sa **većim** ID-em |
> | 2 | **OK** | P3 → P2 | P3 je živ ⇒ **P2 se povlači** iz izbora |
> | 3 | **OK** | P4 → P2 | P4 je živ |
> | — | *(nema odgovora)* | P5 ✗ | P5 je otkazao |
> | 4 | **ELECTION** | P3 → **P4, P5** | P3 nastavlja izbor |
> | 5 | **OK** | P4 → P3 | **P3 se povlači** iz izbora |
> | — | *(nema odgovora)* | P5 ✗ | |
> | 6 | **ELECTION** | P4 → **P5** | P4 šalje jedinom sa većim ID-em |
> | — | *(nema odgovora u timeout-u)* | P5 ✗ | ⇒ **P4 je POBEDNIK** |
> | 7 | **COORDINATOR** | P4 → **P1, P2, P3** | P4 obaveštava sve da je on novi koordinator |
>
> **Ukupno: 7 poruka** (3 ELECTION rundе sa 6 poruka + 3 OK + 3 COORDINATOR = 6+3+3 = 12 pojedinačnih poruka, ako se broje sve pojedinačno).
>
> **Pravilo:** *„Ako u okviru određenog vremena ne stigne odgovor ni od jednog procesa, to znači da nijedan proces sa većim ID-em nije aktivan i **pobednik je proces koji je inicirao izbore**. Ako stigne odgovor na poruku izbora, proces se **povlači iz izbora** i čeka poruku od novog koordinatora."*
>
> ➜ **Novi koordinator: P4.**

---

### P81. Na slici 2 su prikazana tri procesa koja međusobno razmenjuju poruke (poruke su označene kvadratićima sa brojem unutar kvadrata). Svaki proces nezavisno kreira tačke provere (označene sa C1 do C9). a) Definisati konzistentni presek. b) Ako u procesima P2 i P3 dođe do greške u trenutku koji je označen sa "crash", da li je moguće izvršiti oporavak sistema od greške i vratiti ga u konzistentno stanje korišćenjem navedenih tačaka provere? Ako je moguće odrediti liniju oporavka (tj. skup tačaka koje čine konzistentni presek). Obavezno obrazložiti odgovor.

`26-JUL · 13` *(1/5)* 🔁 *(`[S·4]` — isti tip zadatka sa rešenim primerom)*

> **Odgovor** `[U]` `[S·4]`
>
> **a) DEFINICIJA KONZISTENTNOG PRESEKA**
>
> *„Da bi oporavak od greške bio moguć u DS-u, svi procesi moraju da se vrate u stanje odakle je moguće izvršiti oporavak — mora se naći **linija oporavka (konzistentni presek)**. **Konzistentni presek je skup tačaka provere (checkpoint-a) u različitim procesima koje omogućavaju da se od njih sistem restartuje i krene dalje sa radom.**"*
>
> **Formalno:** skup checkpoint-a **C** je konzistentan ako za sve događaje `e` i `e′` važi:
>
> **(e ∈ C) ∧ (e′ → e) ⟹ e′ ∈ C**
>
> **U prevodu (PRAVILO ZA REŠAVANJE):**
> - **Ako je zabeležen PRIJEM poruke, mora biti zabeleženo i SLANJE te poruke.** ❌ *Ne sme se desiti da checkpoint beleži prijem poruke, a da nije zabeleženo njeno slanje iz nekog drugog procesa — to bi bila „poruka iz budućnosti".*
> - **Obrnuto SME:** neki proces može zabeležiti **slanje** poruke, a drugi proces u svom checkpoint-u **nije zabeležio prijem** — to je samo poruka „u letu" (in-flight), što je dozvoljeno.
>
> **b) POSTUPAK NALAŽENJA LINIJE OPORAVKA**
> 1. Kreni od **poslednjih** checkpoint-a pre tačke otkaza u svakom procesu
> 2. Za svaku poruku koja **preseca** zamišljenu liniju: proveri smer strelice
>    - strelica ide **sleva nadesno kroz liniju** (slanje pre linije, prijem posle) → **OK**, poruka u letu
>    - strelica ide **zdesna nalevo** (prijem pre linije, slanje posle) → **NIJE konzistentno**, pomeri checkpoint unazad
> 3. Ponavljaj dok se ne nađe presek u kome nijedna poruka nije „primljena a nije poslata"
> 4. Ako se pomeranje nastavlja unazad kroz sve checkpoint-e do početnog stanja, nastupio je **DOMINO EFEKAT**
>
> **DOMINO EFEKAT:** *„Ako procesi nezavisno jedan od drugog donose odluku gde će kreirati checkpointe, može biti vrlo teško naći liniju oporavka i često se javlja domino efekat. Tada sistem mora da se vrati u početno stanje."*
>
> **REŠENJE za domino efekat — KOORDINISANO beleženje stanja (dvofazni blokirajući protokol):**
> 1. Koordinator periodično šalje svim procesima **`checkpoint_REQUEST`**
> 2. Proces prestaje sa aktivnostima, kreira checkpoint, beleži svoje stanje
> 3. Proces javlja koordinatoru **`checkpoint_ACK`**
> 4. Kada koordinator primi sve potvrde, šalje svima **`checkpoint_DONE`** → procesi se deblokiraju
>
> ➜ Zapamćeno stanje u svim procesima je **konzistentno** i domino efekat je izbegnut. Jedina mana je postojanje koordinatora.
>
> **Za konkretan crtež:** primeni pravilo iz b) na tačke C1–C9. Rešeni primer sa istom logikom je `[S·4]`:
> - `Linija(11, 21, 31)` — **JESTE** konzistentan presek: nema primljenih poruka koje nisu poslate
> - `Linija(11, 22, 32)` — **NIJE**: S2 prima poruku od S1, a S1 ju je poslao **nakon** svog checkpoint-a
> - `Linija(12, 23, 33)` — **NIJE**: S3 šalje poruku pre checkpoint-a, a S2 je prima nakon svog checkpoint-a
>
> **Podsetnik — dve strategije oporavka:**
> - **Backward Recovery** — povratak u prethodno korektno stanje pomoću **tačaka provere**; skupo, ali **najčešće korišćeno**
> - **Forward Recovery** — prelazak u **novo** korektno stanje; zahteva da se **identifikuju sve moguće greške** unapred; **mnogo ređe se koristi**

---

### P82. Navesti kriterijume za podelu komunikacija u distribuiranim sistemima. Koji sve tipovi komunikacija u distribuiranom sistemu su podržani od strane MPI i kojim funkcijama? Obrazložiti izvršenje svake funkcije.

`2024 · 4a` *(1/5)*

> **Odgovor** `[U]` `[P2]`
>
> **KRITERIJUMI ZA PODELU KOMUNIKACIJA (3):**
>
> | Kriterijum | Vrste |
> |---|---|
> | **Postojanost (persistency)** | **Perzistentne** — poruka se pamti u komunikacionom serveru koliko je potrebno da bi se isporučila odredištu ⟷ **Tranzijentne** — poruka se odbacuje ako komunikacioni server nije u stanju da je isporuči |
> | **Sinhronizacija** | **Sinhrone** — pošiljalac se blokira dok se poruka ne zapamti u lokalnom baferu odredišnog hosta ⟷ **Asinhrone** — pošiljalac nastavlja sa radom odmah nakon što prosledi poruku |
> | **Vremenska zavisnost** | **Diskretna** ⟷ **strimovana** komunikacija |
>
> **MPI podržava skoro sve oblike TRANZIJENTNIH komunikacija.** *(MPI je middleware za paralelne sisteme; komunikacija se obavlja u okviru grupe procesa — par `(id_grupe, id_procesa)` jedinstveno identifikuje izvor/odredište umesto transportne adrese.)*
>
> **MPI KOMUNIKACIONE PRIMITIVE:**
>
| Primitiva | Značenje |
|---|---|
| **`MPI_bsend`** | Kopiraj izlaznu poruku u **lokalni bafer MPI sistema** i nastavi sa radom → **neblokirajuća, tranzijentna asinhrona** komunikacija |
| **`MPI_send`** | Pošalji poruku i čekaj dok se **ne iskopira u bafer odredišnog hosta** (izvor je blokiran dok poruka ne bude smeštena u MPI bafer odredišta). *Semantika zavisi od implementacije.* |
| **`MPI_ssend`** | Pošalji poruku i čekaj **dok ne krene prijem** (izvor je blokiran dok prijemnik ne prihvati zahtev) → **sinhronizovana** komunikacija |
| **`MPI_sendrecv`** | Pošalji poruku i **čekaj odgovor** (izvor je blokiran dok ne stigne odgovor iz odredišta) → **ponaša se isto kao RPC**; najjača forma sinhronizovane komunikacije |
| **`MPI_isend`** | Prosledi **referencu** (pokazivač) na izlaznu poruku i nastavi sa izvršenjem (**nema kopiranja** iz korisničkog u MPI bafer). Postoji mogućnost provere da li je komunikacija završena ili blokiranja dok se ne završi |
| **`MPI_issend`** | Prosledi **referencu** na izlaznu poruku i **čekaj dok ne krene prijem** |
| **`MPI_recv`** | Prihvati poruku; **prijemnik se BLOKIRA** dok poruka ne stigne |
| **`MPI_irecv`** | Prijemnik je **spreman** da prihvati poruku — može da **proveri** da li je poruka stigla ili da čeka dok ne stigne (**neblokirajuće**) |

---

### P83. Sveprisutni distribuirani sistemi.

`25-SP · 9` *(1/5)*

> **Odgovor** `[UV]` `[U]`
>
> **Sveprisutni (ugrađeni / embedded) DS** je **treći tip DS** u podeli po oblasti primene (uz distribuirane računarske sisteme i distribuirane informacione sisteme).
>
> **Ključna razlika:** prva dva tipa DS karakteriše relativno **visoka stabilnost** — čvorovi su fiksni i imaju stalnu, kvalitetnu mrežnu konekciju. Sa pojavom mobilnih i ugrađenih računarskih sistema, **nestabilnost postaje uobičajeno ponašanje**.
>
> **Osobine uređaja:**
> - uglavnom **mali**
> - **napajaju se pomoću baterija**
> - **mobilni** su
> - imaju **samo bežične veze**
>
> **Najvažnija osobina: ODSUSTVO ADMINISTRATIVNOG UPRAVLJANJA.** Posledice:
> - Uređaji **sami moraju da otkriju okruženje i da se ugnezde u njega**
> - Uređaji **moraju biti svesni da se okruženje stalno može menjati**
>
> **Primeri:**
> - **Kućni sistemi** — organizovani oko jednog centralnog računara (automatsko paljenje svetla, sistemi za navodnjavanje, alarmni sistemi, kućni aparati)
> - **Elektronski sistemi za monitoring pacijenata**
> - **Senzorske mreže** — sistemi za akviziciju podataka i nadzor (merenje temperature, vlažnosti… i slanje informacija do bazne stanice gde se obavlja procesiranje)

---

### P84. Koje od navedenih tvrdnji tačno opisuju karakteristike i način rada distribuiranih transakcija? Obrazložiti.

`26-JUL · 1c` *(1/5)* — **pitanje na zaokruživanje (više odgovora)**

- ✅ **A.** Distribuirana transakcija obuhvata operacije koje se izvršavaju na više distribuiranih resursa.
- ✅ **B.** Cilj distribuiranih transakcija je da se obezbedi konzistentno stanje sistema.
- ❌ **C.** Distribuirane transakcije značajno poboljšavaju performanse i smanjuju vreme odziva sistema u poređenju sa lokalnim transakcijama.
- ❌ **D.** Distribuirane transakcije ne zahtevaju koordinaciju između učesnika.

> **Odgovor:** **tačno je A i B** `[UV]` `[U]` `[S·21]`
>
> - **A ✅** — *„U DS transakcija je obično sastavljena od **više podtransakcija koje su raspoređene na više servera**."*
> - **B ✅** — *„Sistem za obradu transakcija obezbeđuje da sve ili nijedna operacija u transakciji budu izvršene bez greške. Nakon obavljene transakcije, **sistem mora da bude u poznatom konzistentnom stanju**."* Takođe **C** u ACID-u: *„transakcija ne ugrožava skup invarijanti (ograničenja) sistema."*
> - **C ❌** — u materijalima **nema** takve tvrdnje. Naprotiv, distribuirana transakcija zahteva **koordinaciju kroz mrežu i protokol potvrde**, što **dodaje** režijske troškove u odnosu na lokalnu transakciju.
> - **D ❌** — direktna suprotnost: *„**Monitor za obradu transakcija (TP monitor)** je middleware koji omogućava aplikaciji da obavi pristup više servera/baza podataka nudeći joj transakcioni programski model. **TP monitor KOORDINIRA potvrdu podtransakcija prema standardnom protokolu.**"*
>
> **ACID osobine (obavezno znati uz ovo pitanje)** `[UV]` `[S·21]`:
>
> | | Osobina | Značenje |
> |---|---|---|
> | **A** | **Atomicity** (atomičnost) | transakcija se obavi **kompletno ili se uopšte ne obavi** |
> | **C** | **Consistency** (konzistentnost) | transakcija **ne ugrožava skup invarijanti** (ograničenja) sistema — npr. transakcija sa negativnom vrednošću biće odbijena |
> | **I** | **Isolation** (izolacija) | konkurentne transakcije moraju biti **serijalizovane** — izvršavaju se u nekom redosledu, ali taj redosled mora biti **vidljiv na isti način za sve** |
> | **D** | **Durability** (trajnost) | kada se transakcija obavi, **ne može se poništiti** i ostaje trajna čak i u slučaju otkaza sistema |

---
---

# 📊 REZIME

## Ukupno unikatnih pitanja koja treba naučiti: **84**

| # | Tema | Učestalost | Broj pitanja | Oznake |
|---|---|:---:|:---:|---|
| 1 | Modeli konzistencije i skladišta podataka | **5/5** | **9** | P1–P9 |
| 2 | RPC — Sun RPC, DCE RPC, stubovi, XDR, semantike | **5/5** | **14** | P10–P23 |
| 3 | Razmena poruka — MQ, publish-subscribe, JMS | **5/5** | **7** | P24–P30 |
| 4 | Otpornost na greške — mirne/Vizantijske, konsenzus | **5/5** | **6** | P31–P36 |
| 5 | Osnovne osobine DS — skalabilnost, transparentnosti | 4/5 | 8 | P37–P44 |
| 6 | Logički i vektorski časovnici | 4/5 | 5 | P45–P49 |
| 7 | Grupna komunikacija i uređenje poruka | 4/5 | 4 | P50–P53 |
| 8 | Replikacija — vrste replika, protokoli | 3/5 | 4 | P54–P57 |
| 9 | Uzajamno isključivanje | 3/5 | 3 | P58–P60 |
| 10 | Sinhronizacija fizičkih časovnika (NTP/Kristijan/Berkeley) | 3/5 | 5 | P61–P65 |
| 11 | Hadoop / HDFS | 3/5 | 4 | P66–P69 |
| 12 | Distribuirani fajl sistemi (DFS) | 2/5 | 3 | P70–P72 |
| 13 | Chord / P2P | 2/5 | 3 | P73–P75 |
| 14 | Java RMI i udaljeni objekti | 2/5 | 4 | P76–P79 |
| 15 | Bully algoritam | 1/5 | 1 | P80 |
| 16 | Tačke provere / konzistentni presek / oporavak | 1/5 | 1 | P81 |
| 17 | MPI teorija (tipovi komunikacija) | 1/5 | 1 | P82 |
| 18 | Sveprisutni distribuirani sistemi | 1/5 | 1 | P83 |
| 19 | Distribuirane transakcije | 1/5 | 1 | P84 |
| | **UKUPNO** | | **84** | |

---

## Raspodela po prioritetu

| Prioritet | Teme | **Broj pitanja** | Udeo |
|---|---|:---:|:---:|
| 🔴 **OBAVEZNO (5/5)** | Teme 1–4 | **36** | **43%** |
| 🟠 **Vrlo verovatno (4/5)** | Teme 5–7 | **17** | 20% |
| 🟡 **Verovatno (3/5)** | Teme 8–11 | **16** | 19% |
| 🟢 **Moguće (2/5)** | Teme 12–14 | **10** | 12% |
| ⚪ **Retko (1/5)** | Teme 15–19 | **5** | 6% |

> 💡 **Ako imaš malo vremena:** nauči **36 pitanja iz tema 1–4** (43% gradiva) — pokrivaju otprilike **polovinu svakog blanketa** i garantovano se pojavljuju na svakom roku.
> Sa temama 5–7 (**53 pitanja, 63%**) pokrivaš praktično sve što se realno pojavljuje.

---

## Pitanja na zaokruživanje — brza tabela za ponavljanje

| Pitanje | Rok | ✅ Tačan odgovor |
|---|---|---|
| **P18** Server promeni adresu (dinamičko povezivanje) | 25-SP·10b | **b + d** ⚠️ |
| **P23** DCE RPC tvrdnje | 26-JUL·3b | **A, B, E** |
| **P26** Šta ne mora da važi u MQ | 25-SP·11a | **c** *(i d ⚠️)* |
| **P27** Karakteristike Message Queues | 26-JUL·5 | **A, B, D** |
| **P29** Deo JMS poruke za selektor | 26-JUN·5 | **C** (zaglavlje + svojstva, **ne** telo) |
| **P34** Otpornost na Vizantijske greške | 25-SP·6 | **d** (maliciozni čvorovi) |
| **P38** Transparentnost konkurencije | 2024·2 | **b** |
| **P39** Osobine transparentnosti konkurencije | 25-NP·2 | **a, b, d** |
| **P40** Transparentnost replikacije | 25-NP·3 | **b** |
| **P41** Pristupna transparentnost | 26-JUN·1c | **B** |
| **P45** Vektorski časovnici — najviše što možemo doznati | 25-SP·1 | **b** (ako i samo ako) |
| **P50** Potpuno uređena grupna komunikacija | 2024·5 | **d** |
| **P63** Kristijanov algoritam usvaja | 26-JUN·7 | **b** (kašnjenja približno jednaka) |
| **P75** Chord — očekivani broj koraka | 26-JUN·15b | **c** O(log 10000) |
| **P79** Referenca udaljenog objekta | 26-JUN·4a | **C** *(A sporno ⚠️)* |
| **P84** Distribuirane transakcije | 26-JUL·1c | **A, B** |

**T/F pitanja:**

| Pitanje | Rok | Odgovori |
|---|---|---|
| **P67** HDFS (faktor repl., block report, lokalni FS, DN zna fajlove) | 25-SP·12 | **T, T, T, F** |
| **P69** HDFS (32MB, NN čuva podatke, istovremeni upis, faktor repl.) | 25-NP·14b | **F, F, F, T** |
| **P70** DFS stateless/statefull | 2024·8a | **T, T, F, T** |

---

## Formule koje se moraju znati napamet

| Oblast | Formula |
|---|---|
| **Otpornost — mirne greške** | `k + 1` komponenti za k-tolerantnost |
| **Otpornost — Vizantijske** | `2k + 1` komponenti |
| **Otpornost — Vizantijske + konsenzus** | `3k + 1` komponenti *(min. `2k+1` lojalnih; >2/3 lojalno)* |
| **Uzajamno isključivanje — centralizovani** | **3** poruke *(request, OK, release)* — nezavisno od n |
| **Uzajamno isključivanje — Ricart–Agrawala** | **2(n − 1)** poruka |
| **NTP — kružno kašnjenje** | `δ = (t₁ − t₀) + (t₃ − t₂)` |
| **NTP — offset** | `θ = [(t₁ − t₀) + (t₂ − t₃)] / 2` |
| **NTP — novo vreme** | `t₃′ = t₃ + θ = t₂ + δ/2` |
| **Kristijan** | `T_novo = C_utc + (T₁ − T₀)/2` |
| **Kvorum (Gifford)** | `N_R + N_W > N` *(read-write konflikt)* **i** `N_W > N/2` *(write-write konflikt)* |
| **Chord — broj koraka** | bez finger tabele `O(n)`, sa finger tabelom **`O(log n)`** *(n = broj čvorova)* |
| **Chord — vlasnik ključa** | `succ(k)` = prvi čvor sa `ID ≥ k` u smeru kazaljke |
| **Chord — finger tabela** | red `j` čvora `i` pokazuje na `succ(i + 2^(j−1))`, `j = 1…m` |
| **HDFS — broj blokova** | `⌈veličina_fajla / veličina_bloka⌉ × faktor_replikacije` |
| **Pouzdanost — serijski (svi moraju raditi)** | `R = R₁ · R₂ · … · Rₙ` |
| **Pouzdanost — paralelni (bar jedan radi)** | `R = 1 − (1 − Rᵢ)ⁿ` |
| **ABFT — korekcija** | `nova = stara + (kontrolna_suma − stvarna_suma)` |
| **Erasure coding** | `(n, k)` kod toleriše **`n − k`** grešaka |

---

## Zadaci (nisu teorija, ali se ponavljaju — za orijentaciju)

Poslednja 3–5 pitanja svakog blanketa su **programerski zadaci**, i još su repetitivniji od teorije:

| Zadatak | Rokovi | Ponavljanje |
|---|---|---|
| **Java RMI — MQTT broker** *(subscribe/publish/createTopic)* | 2024, 25-NP, 26-JUL | **3× skoro doslovno** |
| **WCF — full-duplex kalkulator** *(callback sa izrazom)* | 25-SP, 25-NP, 26-JUN | **3× skoro doslovno** |
| **MPI — paralelni upis/čitanje binarne datoteke po šemi sa slike** | svi rokovi | **5×** (menja se broj procesa i podela) |
| **gRPC — .NET servis + proto fajl** | 2024, 25-NP, 26-JUN, 26-JUL | 4× (menja se domen) |
| **JMS — sistem za razmenu poruka** | 25-SP, 26-JUL | 2× |

---

*Dokument generisan iz: `blanketi/` (5 rokova) + `usmeni.pdf` + `DS BELO ZLATO skripta.pdf` + `PREZENTACIJE/` + `GFS i HDFS 2024.pdf`*
*Svi odgovori su izvučeni isključivo iz tih fajlova. Mesta gde materijal nije potpun označena su sa ⚠️.*
