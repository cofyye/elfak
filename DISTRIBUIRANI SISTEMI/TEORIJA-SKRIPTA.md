# DISTRIBUIRANI SISTEMI — SKRIPTA (samo teorija)

**62 teorijska pitanja sa 5 rokova, poređana od 5/5 ka 1/5.**
Mini-zadaci (računski i sa slika) su izbačeni — njih imaš u `TEORIJA-PONAVLJANJA.md`.

**Rokovi:** `2024` = 10.06.2024 · `25-SP` = 20.10.2025 · `25-NP` = 27.10.2025 · `26-JUN` = 30.06.2026 · `26-JUL` = 20.07.2026

**Oznake:** ✅ tačan odgovor · ❌ netačan · 🔁 ponavlja se na više rokova · ⚠️ nepotpuno u materijalima

---
---

# 🔴 5/5 — NA SVAKOM ROKU

---

## TEMA 1 · Konzistencija

---

### 1. Definisati: a) striktnu; b) sekvencijalnu; c) uslovnu (kauzalnu); d) FIFO konzistenciju.
`25-NP · 9` 🔁

**a) STRIKTNA** — najjači model, bazira se na postojanju globalnog časovnika.
Skladište podataka je striktno konzistentno ako **bilo koja operacija nad podatkom X vraća rezultat poslednje write operacije nad tim podatkom X**.
Nije je moguće postići u DS: zasniva se na apsolutnom globalnom vremenu — ažuriranje bi moralo da se prostire ~10 puta brže od brzine svetlosti.

**b) SEKVENCIJALNA** — slabija od striktne, ali **najjači model koji se može postići u DS**.
Rezultat bilo kog izvršenja je isti kao da su (read i write) operacije svih procesa na skladištu podataka **izvršene u nekom sekvencijalnom redosledu**, i operacije svakog pojedinačnog procesa pojavljuju se u toj sekvenci **u redosledu koji je određen njihovim programom**.
➜ Svi procesi vide **isti** redosled; programski raspored svakog procesa mora biti ispoštovan.

**c) USLOVNA (KAUZALNA)** — slabija od sekvencijalne.
**Upisi koji su potencijalno uslovljeni moraju da se vide u svim procesima u istom redosledu. Konkurentni upisi se mogu videti u različitom redosledu u različitim procesima.**
Implementira se pomoću **vektorskih časovnika**.

**d) FIFO** — slabija od kauzalne.
**Upisi koje obavi jedan proces vide se od strane drugih procesa po redosledu po kome su izdati. Upisi različitih procesa mogu se videti u različitom redosledu u različitim procesima.**
Implementacija: svaki proces poruci ažuriranja dodaje process-id + redni broj.

**Hijerarhija:** striktna ⊃ sekvencijalna ⊃ kauzalna ⊃ FIFO.

---

## TEMA 2 · RPC — Sun RPC, DCE RPC, stubovi, XDR

---

### 2. Izlaz iz Sun IDL kompajlera sastoji se od više fajlova. Koji su to fajlovi i šta sadrže? Napisati na koji način se vrši generisanje ovih fajlova i kako se na osnovu generisanih fajlova formiraju izvršna klijentska i serverska aplikacija. Za svaki korak napisati odgovarajuću komandu.
`2024 · 3a` 🔁

**Poziv kompajlera:** `rpcgen -C primer.x`

Generišu se **3 fajla**:

| Fajl | Sadržaj |
|---|---|
| `primer.h` — **header** | jedinstveni identifikator interfejsa, definicije tipova, konstanti i prototipova funkcija. Uključuje se sa `#include` **i u klijent i u server** kod |
| `primer_clnt.c` — **klijent stub** | procedure koje će klijent program pozivati; pakuju parametre u poruke, šalju poruku, primaju poruku, izvlače rezultat i prosleđuju ga klijentu |
| `primer_svc.c` — **server stub** | procedure koje se pozivaju kada poruka stigne do servera i koje zatim pozivaju odgovarajuću serversku proceduru |

**Koraci i komande:**
```bash
rpcgen -C primer.x                    # generiše primer.h, primer_clnt.c, primer_svc.c
rpcgen -C -Ss primer.x > server.c     # generiše template serverskog koda
# ručno se dopiše telo procedura u server.c i napiše client.c
cc -o client client.c primer_clnt.c   # izvršna klijentska aplikacija
cc -o server server.c primer_svc.c    # izvršna serverska aplikacija
```

Pored generisanih fajlova pišu se i **klijent program** (`client.c` — uspostavlja vezu preko `clnt_create()` na osnovu imena servera, broja programa i broja verzije) i **server program** (`server.c` — implementacija procedura).

**Konvencije:** imena programa/verzije/procedura u IDL fajlu — **VELIKIM SLOVIMA**. U klijentu se procedura poziva kao `imeprocedure_brojverzije`, a na serveru se implementira kao `imeprocedure_brojverzije_svc`.

---

### 3. Šta može biti argument serverske procedure kod Sun RPC, a šta povratna vrednost u slučaju SunRPC? Kako mora biti deklarisan rezultat serverske procedure i zašto?
`25-NP · 5a`

- Sun RPC poziv udaljene procedure može imati **samo JEDAN ulazni argument i JEDAN izlazni parametar**. Ako treba preneti više parametara ili rezultata, to se postiže **preko struktura** (ili nizova).
- Procedure na serveru **uzimaju kao parametar pokazivač** na podatak koji se prenosi i **vraćaju pokazivač** na rezultat.
- **Rezultat mora biti deklarisan kao `static`.** Razlog: procedura vraća **pokazivač** na rezultat; da je promenljiva lokalna (automatska), bila bi uništena po izlasku iz funkcije i pokazivač bi pokazivao na nevažeću memoriju. `static` obezbeđuje da promenljiva postoji i nakon povratka, pa server stub može da je spakuje u poruku.

```c
int *saberi_1_svc(operandi *a, struct svc_req *rqstp)
{
    static int zbir;          /* MORA static */
    zbir = a->x + a->y;
    return &zbir;             /* vraća pokazivač */
}
```

---

### 4. Objasniti aktivnosti koje obavljaju klijent stub i server stub prilikom poziva udaljene procedure.
`26-JUN · 2a` 🔁

**KLIJENT STUB** — funkcija koja liči na lokalnu funkciju, ali **ne sadrži kod procedure**, već kod za slanje parametara i prijem rezultata kroz mrežu:
- pakuje parametre u mrežnu poruku (**marshaling**), uz konverziju iz lokalnog u **standardni format podataka**
- poziva lokalni OS (SEND, pa RECEIVE) i čeka odgovor
- raspakuje rezultat iz poruke i vraća ga klijentskom procesu

**SERVER STUB:**
- prima poruku od lokalnog OS-a (RECEIVE), raspakuje je (**unmarshaling**) i izvlači argumente
- poziva željenu serversku proceduru i predaje joj parametre (stavlja ih na stek)
- pakuje rezultat u poruku i poziva lokalni OS (SEND)

**Svih 10 koraka poziva RPC-a:**
1. Klijent poziva lokalnu proceduru (klijent stub), parametri se smeštaju u stek
2. Klijent stub pakuje argumente, gradi mrežnu poruku, poziva lokalni OS (SEND pa RECEIVE)
3. Lokalni OS šalje poruku udaljenom OS-u (UDP ili TCP)
4. Poruka stiže do servera; OS je prosleđuje serverskom stubu, koji je raspakuje i izvlači argumente
5. Serverski stub poziva serversku proceduru i predaje joj parametre
6. Server izvršava proceduru i vraća rezultat serverskom stubu
7. Serverski stub pakuje rezultat u poruku i poziva lokalni OS (SEND, pa RECEIVE)
8. Serverski OS šalje poruku klijentskom OS-u
9. Lokalni OS prosleđuje poruku klijent stubu
10. Klijent stub izvlači rezultat iz poruke i vraća ga klijentskom procesu

---

### 5. Koji su načini prenosa parametara kod poziva udaljene procedure? Objasniti.
`25-SP · 10a`

- **call-by-value (po vrednosti)** — vrednosti parametara se kopiraju u stek; ako pozvana procedura modifikuje podatke, originalni podaci se **ne** menjaju
- **call-by-reference (po referenci)** — adrese parametara se smeštaju u stek; modifikacije se odražavaju na original
- **call-by-copy/restore** — vrednosti se stavljaju na stek kao kod prenosa po vrednosti, a **nakon završetka poziva** modifikovane vrednosti se upisuju preko originalnih (efekat kao kod prenosa po referenci)

➜ **Kod RPC-a se koristi copy/restore.**

**Zašto ne po referenci:** nema smisla proslediti adresu udaljenoj mašini (drugi adresni prostor). Da bi se postigao efekat prenosa po referenci, klijent stub **kopira podatke na koje pokazuje pointer u poruku** i pošalje je serveru; serverski stub koristi kopiju u adresnom prostoru servera i može pozvati serversku proceduru sa pointerom na taj podatak.

---

### 6. Kako se prenose skalarni parametri kod RPC? Šta predstavlja standardni format podataka i zašto je neophodan? Navesti jedan njegov primer. Ko obavlja konverziju podataka kod RPC?
`26-JUL · 2a`

- **Skalarni parametri se prenose po vrednosti** (u C-u se skalarni tipovi prenose po vrednosti, a polja po referenci). Dok su klijent i server ista mašina i parametri skalarni, ovo funkcioniše bez problema.
- **Zašto je standardni format neophodan:** u DS je uobičajeno da postoji **više tipova mašina**. Svaka mašina može imati različit način predstavljanja **brojeva** (big-endian, little-endian, dvojični ili jedinični komplement) i **karaktera** (ASCII, EBCDIC). Zato nije moguće preneti parametre onako kako su zapisani lokalno.
- **Rešenje:** koristi se **„standardno" kodiranje svih tipova podataka** koji se mogu prenositi kao parametri.
- **Primer:** **XDR** (eXternal Data Representation) kod Sun RPC-a.
- **Ko obavlja konverziju:** **klijentski i serverski stub** — oni obavljaju konverzije iz lokalnog u standardni način predstavljanja podataka i obrnuto.

---

### 7. Navesti gde se deklarišu promenljive čiji su tipovi definisani u XDR-u. Kako se definiše niz promenljive veličine? Kako se predstavljaju nizovi promenljive veličine u generisanom C kodu?
`26-JUL · 2b`

**Gde se deklarišu:** RPC definicije tipova se kompajliraju i nalaze u **izlaznom header fajlu** (`primer.h`). Definicija **ne alocira memorijski prostor** — to su samo definicije; promenljive se tek naknadno deklarišu (u klijent/server kodu koji uključuje header). **Deklaracije ne mogu stajati samostalno u `.x` fajlu, već moraju biti deo strukture ili u okviru `typedef`.**

**Niz fiksne veličine:**
```c
colortype palette[8];        /* --> colortype palette[8]; */
```

**Niz promenljive veličine — uglaste zagrade `< >`:**
```c
int  heights<50>;            /* max 50 vrednosti */
long x_vals<>;               /* max 2^32 - 1 vrednosti */
```

**Predstavljanje u generisanom C kodu — struktura sa dva polja:**
```c
typedef int heights<50>;
/* --> */
struct {
    int  heights_len;    /* broj elemenata niza */
    int *heights_val;    /* pokazivač na prvi element niza */
} heights;
```

---

### 8. Kako se može locirati server koji implementira udaljenu proceduru?
`25-NP · 4` 🔁

Postoje **2 tipa povezivanja (binding)**:

**1. STATIČKO POVEZIVANJE**
Klijent zna koji server treba da kontaktira i **adresa servera je u klijent stubu**. Kada klijent pozove proceduru, klijent stub samo prosledi poziv serveru čiju adresu ima. Poseban program **PORTMAPPER** na udaljenom serveru pamti preslikavanja **imena programa, broja verzije i broja porta** (portmapper radi na **portu 111**).
➜ *Mana:* ako server promeni adresu, mora se rekompajlirati klijent stub.

**2. DINAMIČKO POVEZIVANJE**
Postoji **centralizovana baza podataka smeštena u Name i Directory serverima** koja može locirati server koji obezbeđuje željeni servis. Name/Directory serveri vraćaju adresu traženog servera na osnovu **potpisa procedure** koja se poziva.
Tok: klijent pozove udaljenu proceduru → klijent stub kontaktira **Name server** da dobije adresu servera → Name server šalje adresu → klijent stub uspostavlja vezu sa serverom.
**Serverski stub registruje serversku proceduru u Name i Directory serveru prilikom startovanja servera.** Ako server promeni adresu, **dovoljno je promeniti ulaz u Name serveru.**

---

### 9. Šta se dešava kada server promeni mrežnu adresu u sistemu sa dinamičkim povezivanjem? Obrazložiti.
`25-SP · 10b` — **ZAOKRUŽIVANJE**

- ❌ **a.** Klijent mora ponovo da se kompajlira
- ✅ **b.** Server se ponovo registruje kod bindera sa novom adresom ← **TAČNO**
- ❌ **c.** Binder obaveštava sve aktivne klijente o promeni
- ✅ **d.** Klijent može da dobije novu adresu pri sledećem pozivu ← **TAČNO**

⚠️ **Tačna su i b) i d)** — dve strane istog mehanizma. Ako se traži samo jedan, **d)** je iz ugla klijenta (što pitanje sugeriše), **b)** je ono što radi server.

**Obrazloženje:** „Serverski stub registruje serversku proceduru u Name i Directory serveru prilikom startovanja servera. Ako server promeni adresu, dovoljno je promeniti ulaz u Name serveru." Pošto klijent stub **pri svakom pozivu** kontaktira Name server da dobije adresu, klijent dobija novu adresu pri sledećem pozivu.
- **a ❌** — rekompajliranje je potrebno samo kod **statičkog** povezivanja (adresa je u klijent stubu)
- **c ❌** — binder je **pasivan**; ne obaveštava klijente, oni ga sami pitaju (pull, ne push)

---

### 10. U distribuiranom sistemu klijent koristi RPC za komunikaciju sa serverom. Klijent šalje zahtev serveru (npr. operation x()) sve dok ne dobije odgovor od servera. a) Koja RPC semantika u slučaju greške je u ovom slučaju implementirana? b) Objasniti kako mora da bude implementiran server da bi se ovakva semantika obezbedila. Navesti bar dve mogućnosti.
`25-SP · 7` 🔁

**a) At-least-once („bar jednom") semantika.** Klijent šalje zahtev sve dok ne primi odgovor, dakle operacija će se izvršiti **najmanje jednom, a možda i više puta**.

**b) Dve mogućnosti na strani servera:**
1. **Da procedura `operation x()` bude idempotentna** — da vraća uvek isti rezultat ma koliko puta se izvrši i da **ne menja stanje sistema** (npr. procedura koja vraća tačno vreme).
2. **Da server bude statefull** — da vodi računa o klijentskim zahtevima (redni brojevi zahteva ili bit za retransmisiju u zaglavlju), prepoznaje **duplikate** i u tom slučaju samo **retransmituje zapamćen odgovor**, bez ponovnog izvršavanja procedure.

**Sve tri semantike:**
- **maybe (možda)** — nema garancije da će operacija biti izvršena; klijent ne zna da li je izvršena
- **at-least-once** — izvršiće se bar jednom, a može i više puta (za idempotentne funkcije)
- **at-most-once** — funkcija nije idempotentna, izvršiće se najviše jednom, a možda ni jednom (odmah se signalizira greška po isteku timeout-a)
- *Idealno bi bilo **exactly once**, ali to se **ne može garantovati**.*

---

### 11. Koja semantika poziva udaljenih procedura je podržana kod DCE RPC i kako?
`2024 · 3b` 🔁 `26-JUN · 3b`

DCE podržava **dve semantičke opcije**:

1. **„bar jednom" (at-least-once)** — koristi se ako su u pitanju **idempotentne** funkcije (mogu se izvršavati više puta bez posledica). **Takve procedure se moraju eksplicitno označiti kao `idempotent` u IDL-u.**
2. **„samo jednom" (at-most-once)** — koristi se kada funkcija **nije idempotentna** (poziv generiše efekte, npr. modifikuje podatke). **Ovo je podrazumevana semantika** — sve što nije označeno kao `idempotent`.

**Kako je implementirano:** oznaka `[idempotent]` u definiciji procedure u IDL fajlu upravlja tim ponašanjem; runtime biblioteka na osnovu nje odlučuje da li sme da ponovi poziv posle timeout-a ili mora da signalizira grešku.

---

### 12. Koji tip povezivanja između klijenta i servera je podržan u DCE RPC-u? Opišite postupak povezivanja.
`26-JUN · 3a` 🔁

**Tip povezivanja: DINAMIČKO** — aplikacije identifikuju resurse **po imenu**, bez potrebe da znaju gde je resurs lociran (za razliku od Sun RPC-a). Koristi se **CDS (Cell Directory Service)** — mehanizam koji omogućava korišćenje logičkih imena unutar DCE ćelije (grupa klijent-server mašina u okviru jednog LAN-a). CDS preslikava logičko ime u IP adresu servera. **GDS (Global Directory Service)** upravlja imenima **van** lokalne DCE ćelije; sprega između CDS i GDS je **GDA**.

**DCE klijent pronalazi server u 2 koraka:** lociranje serverske mašine → lociranje odgovarajućeg procesa na toj mašini.

**Postupak povezivanja (5 koraka):**
1. **Registrovanje broja porta** servera (procesa) u **DCE deamonu** (`rpcd`, koji ima poznati broj porta i održava tabelu parova `[server, brojPorta]`)
2. **Registrovanje servera u Directory serveru** (adresa serverske mašine, ime servera i interfejsi koje implementira)
3. **Klijent kontaktira Directory server** da dobije **IP adresu** serverske mašine
4. **Klijent kontaktira DCE deamon** da dobije **broj porta** server procesa kome želi da pristupi
5. **Poziv udaljene procedure**

*(Registracija se obavlja pre nego što klijent pozove server.)*

---

### 13. Šta se dobija kao rezultat poziva uuidgen programa kod DCE RPC? Koji su naredni koraci u pisanju DCE klijent/server aplikacije?
`26-JUL · 3a`

**Rezultat `uuidgen imefajla.idl`:** kreira se fajl `imefajla.idl` koji sadrži **globalno jedinstveni identifikator interfejsa (UUID) i broj verzije**, i garantuje da se isti ID neće nigde pojaviti u DS-u. Kreira se prazan skelet interfejsa:
```c
[ uuid(3d6ead56-06e3-11ca-8dd1-826901beabcd), version(1.0) ]
interface INTERFACENAME
{
}
```

**Naredni koraci:**
1. **Editovanje IDL fajla** — zameniti `INTERFACENAME` imenom interfejsa i deklarisati procedure (samo potpisi):
```c
[ uuid(3d6ead56-06e3-11ca-8dd1-826901beabcd), version(1.0) ]
interface math {
    long get_sum([in] long first, [in] long second);
}
```
2. **Poziv IDL kompajlera:** `idl primer.idl` → izlaz su **3 fajla**: header (`primer.h`), klijent stub (`primer_cstub.c`), server stub (`primer_sstub.c`)
3. **Pisanje klijent i server programa**, zatim kompajliranje i **linkovanje sa odgovarajućim stubom**

**DCE RPC programer piše 3 programa:** klijent kod, server inicijalizacijski kod (registracija u Directory servisu) i server operativni kod.

---

### 14. Prilikom uspostavljanja DCE RPC komunikacije, koje tvrdnje su tačne?
`26-JUL · 3b` — **ZAOKRUŽIVANJE (više odgovora)**

- ✅ **A.** U DCE RPC sistemu klijent može koristiti CDS/Directory Server da pronađe odgovarajući server koji implementira traženi interfejs. ← **TAČNO**
- ✅ **B.** Nakon pronalaženja servisa, komunikaciju između klijenta i servera dalje obavljaju RPC stubovi i runtime biblioteka. ← **TAČNO**
- ❌ **C.** CDS i rpcbind čuvaju kôd udaljenih procedura koje treba izvršiti.
- ❌ **D.** Ako klijent ne pronađe servis u CDS-u ili rpcbind-u, RPC poziv se ipak izvršava jer klijentski stub sadrži kôd udaljene procedure.
- ✅ **E.** CDS koristi UUID interfejsa, dok Sun RPC koristi broj programa i verziju za identifikaciju servisa. ← **TAČNO**

**Obrazloženje:**
- **A ✅** — „CDS služi da locira CDS server u kome se nalazi preslikavanje logičkog imena u IP adresu servera na kome je implementirana potrebna udaljena procedura."
- **B ✅** — stubovi i runtime biblioteka obavljaju marshaling/unmarshaling i prenos
- **C ❌** — CDS čuva **preslikavanje imena u adresu**, a rpcbind/DCE deamon **tabelu `[server, brojPorta]`**. Ni jedan ne čuva kôd procedura — kôd je isključivo u serverskoj aplikaciji.
- **D ❌** — „Klijent stub … stvarno ne sadrži kod koji treba da se izvrši u toj proceduri, već sadrži kod za slanje parametara i prijem rezultata kroz mrežu. Samo telo funkcije je i obavlja se na serveru." Ako se servis ne pronađe, emituje se **exception**.
- **E ✅** — DCE identifikuje interfejs preko **UUID-a** (`uuidgen`), a Sun RPC preko **broja programa + broja verzije** (+ broja procedure).

---

## TEMA 3 · Razmena poruka — MQ, publish-subscribe, JMS

---

### 15. Interfejs koji je na raspolaganju aplikacijama za razmenu poruka kod Message queuing sistema. Objasniti.
`25-NP · 6` 🔁 `26-JUN · 4b` — **NAJPONAVLJANIJE PITANJE (4×)**

Osnovni interfejs je **vrlo jednostavan i sadrži 4 operacije: `put`, `get`, `poll`, `notify`.**
*(Adresiranje se obavlja obezbeđivanjem jedinstvenog imena odredišnog reda.)*

| Operacija | Blokirajuća? | Šta radi |
|---|---|---|
| **`put`** | ne | Poziva je **pošiljalac**; prosleđuje poruku sistemu da bude dodata u specificirani red. |
| **`get`** | **da** | Omogućava autorizovanom procesu da iz specificiranog reda pribavi **poruku koja je najduže u njemu**. Proces se **blokira ako je red prazan**. Varijacije omogućavaju traženje specifične poruke (pretraživanje po sadržaju). |
| **`poll`** | ne | **Neblokirajuća varijanta `get`-a.** Ako je red prazan ili specifična poruka ne postoji, pozivajući proces **nastavlja sa izvršenjem**. |
| **`notify`** | ne | Omogućava procesu da **instalira handler** koji se automatski poziva kad god je poruka dodata u red. Može automatski startovati proces koji će pribavljati poruke. Često se implementira kroz **demon na strani primaoca** koji osluškuje dolazeće poruke. |

**Dodatno:** aplikacija može stavljati poruke **samo u lokalni red** i čitati samo iz lokalnog reda; redovima upravlja **upravljač redova (queue manager)**, zaseban proces. Politika upravljanja redom je uglavnom **FIFO**, ali većina implementacija podržava i **prioritete**.

---

### 16. Interfejs koji je na raspolaganju aplikacijama za razmenu poruka kod publish-subscribe sistema.
`25-SP · 11b`

Umesto pošiljaoca i primaoca imamo **Publisher (izdavač)** i **Subscriber (pretplatnik)**; umesto reda (queue) destinacija je **Topic (tema)**.

| Operacija | Šta radi |
|---|---|
| **`publish(event)`** | Izdavači distribuiraju događaje u sistem. Događaj se emituje svim pretplatnicima koji su izrazili interesovanje za taj tip događaja. **Izdavači ne moraju znati identitete pojedinačnih pretplatnika.** |
| **`subscribe(filter)`** | Pretplatnici izražavaju interesovanje za primanje određenih tipova događaja; specificira se **filter** koji definiše kriterijume (po tipu događaja, atributima itd.). |
| **`notify(event)`** | Sistem se pobrine da svi pretplatnici čiji filteri odgovaraju događaju **dobiju obaveštenje** i isporuči im događaj. Omogućava **odvojenu (decoupled) komunikaciju**. |
| **`unsubscribe(filter)`** | Pretplatnik povlači interesovanje; sistem uklanja pretplatu i budući događaji mu se više ne isporučuju. |

**Dva načina filtriranja:**
- **Po temi (topic-based)** — izdavači klasifikuju događaje u teme (politika, sport, tehnologija…), pretplatnici biraju teme
- **Po sadržaju (content-based)** — pretplatnici specificiraju kriterijume nad sadržajem (npr. „Obavesti me ako cena akcije Apple pređe 150 dolara")

**Ključna razlika u odnosu na MQ:** kod MQ (point-to-point) poruku može primiti **samo jedan** primalac; kod pub/sub poruku može primiti **više** Subscriber-a. Subscriber prima samo poruke poslate **nakon** njegove prijave.

---

### 17. Koji od sledećih zahteva ne mora uvek da važi u message queuing sistemima? Obrazložiti.
`25-SP · 11a` — **ZAOKRUŽIVANJE**

- ❌ **a.** Pouzdana isporuka poruka
- ❌ **b.** Redosledna isporuka poruka
- ✅ **c.** Direktna veza između pošiljaoca i primaoca ← **TAČNO**
- ⚠️ **d.** Tranzijentno skladištenje poruka

**Obrazloženje:** MQ sistemi podržavaju **perzistentne asinhrone komunikacije** i po definiciji **ne zahtevaju ni od izvora ni od odredišta da budu aktivni za vreme prenosa poruka**. Aplikacije komuniciraju **isključivo preko redova** — red je posrednik koji odvaja pošiljaoca od primaoca. Direktna veza nikada nije potrebna.

⚠️ **Pazi:** i **d)** je sporno — MQ koristi **perzistentno**, a ne tranzijentno skladištenje. Ako pitanje dozvoljava više odgovora, zaokruži **c i d**. Ako je samo jedan — **c**.
*(Ni b) nije apsolutno: politika je „uglavnom FIFO", ali većina implementacija podržava prioritete koji menjaju redosled.)*

---

### 18. Koje od navedenih tvrdnji tačno opisuju karakteristike i rad sistema zasnovanih na redovima poruka (Message Queues)? Obrazložiti.
`26-JUL · 5` — **ZAOKRUŽIVANJE (više odgovora)**

- ✅ **A.** Poruka se šalje u određeni red koji predstavlja odredište. ← **TAČNO**
- ✅ **B.** Više primalaca može čitati poruke iz istog reda u zavisnosti od modela rada. ← **TAČNO**
- ❌ **C.** Pošiljalac mora znati IP adresu svakog primaoca.
- ✅ **D.** Red poruka odvaja pošiljaoca od konkretne implementacije primaoca. ← **TAČNO**
- ❌ **E.** Promena primaoca zahteva promenu svih pošiljalaca.

**Obrazloženje:**
- **A ✅** — „Adresiranje je omogućeno obezbeđivanjem jedinstvenog imena odredišnog reda u sistemu." Odredište je red, ne proces.
- **B ✅** — „Više procesa može slati poruke u isti red, i isto tako, više primaoca mogu uklanjati poruke iz reda." Modeli: 1↔1, 1↔više, više↔1, više↔više. *(Konkretnu poruku i dalje prima samo JEDAN primalac.)*
- **C ❌** — adresira se **logičkim, lokacijski nezavisnim imenom reda**; preslikavanje imena u kontakt adresu `(računar, port)` održava upravljač redova.
- **D ✅** — suština decoupling-a: pošiljalac zna samo ime reda.
- **E ❌** — posledica D: promena primaoca ne dira pošiljaoce.

---

### 19. Šta predstavlja JMS administrativni objekat, koja je njegova uloga i koji su primeri takvih objekata? Iz čega se sastoji JMS poruka i kako i na osnovu čega se može vršiti filtriranje poruke u JMS?
`2024 · 4b`

**JMS administrativni objekti** su **unapred konfigurisani JMS objekti koje kreira administrator** za upotrebu od strane klijenata. Nisu dinamički kreirani od strane kôda aplikacije, već su konfigurisani i upravljani od strane administratora.
**Uloga:** služe kao **„most" između kôda klijenta i JMS provajdera** — klijent dobija konekciju bez potrebe da zna detalje implementacije niskog nivoa.

**Primeri (2 vrste):**
- **Fabrike konekcija (ConnectionFactory)** — omogućavaju klijentima da uspostave konekcije sa JMS provajderom; enkapsuliraju detalje kreiranja konekcija, sesija i proizvođača/potrošača poruka
- **Destinacije (Destination)** — JMS **tema (Topic)** ili JMS **red (Queue)**

*(Pretražuju se preko **JNDI** provajdera, koji vraća odgovore na `lookup` zahteve na osnovu imena.)*

**JMS poruka se sastoji od 3 dela:**
1. **Zaglavlje (header)** — sve informacije za identifikaciju i rutiranje poruke: odredište (referenca na temu ili red), prioritet, datum isteka, ID poruke, vremenska oznaka. Većinu polja kreira JMS provajder, neka može popuniti korisnik.
2. **Svojstva (properties)** — **definisana od strane korisnika**; koriste se za povezivanje dodatnih metapodataka aplikacije sa porukom (npr. polje za lokaciju).
3. **Telo (body)** — tekstualna poruka, niz bajtova, serijalizovani Java objekat, niz primitivnih Java vrednosti ili skup parova ime/vrednost → `TextMessage, StreamMessage, MapMessage, ObjectMessage, BytesMessage`.

**Filtriranje — SELEKTOR PORUKE:** predikat definisan **preko vrednosti u delovima ZAGLAVLJA i SVOJSTAVA poruke — NE i tela.** Selektor se povezuje sa potrošačem poruka.

---

### 20. Šta predstavlja konekcija a šta sesija kod JMS? Koji tipovi konekcija su podržani kod JMS? Koji deo JMS poruke može biti korišćen prilikom definisanja selektora poruka?
`26-JUN · 5` — poslednji deo je **ZAOKRUŽIVANJE**

- ❌ **A.** Samo telo poruke
- ❌ **B.** Samo zaglavlje poruke
- ✅ **C.** Zaglavlje i svojstva poruke, ali ne i telo poruke ← **TAČNO**
- ❌ **D.** Zaglavlje, svojstva i telo poruke

**KONEKCIJA** — da bi se komuniciralo sa JMS provajderom, prvo je neophodno uspostaviti vezu između klijentskog programa i JMS provajdera. To se postiže kroz **fabriku konekcija**. Rezultujuća konekcija je **logički kanal između klijenta i JMS provajdera**.

**SESIJA** — konekcije se mogu koristiti za kreiranje jedne ili više sesija. **Sesija je serija operacija koje uključuju kreiranje, proizvodnju i konzumaciju poruka vezanih za jedan logički zadatak.** Objekat sesije je **centralan za rad JMS-a**: podržava metode za kreiranje poruka, proizvođača i potrošača poruka, kao i operacije za **kreiranje transakcija** (grupiše seriju operacija u jednu atomičnu, sve-ili-ništa; `commit` / `rollback`).

**Tipovi konekcija (2):** **`TopicConnection`** i **`QueueConnection`**.
Postoji jasna razlika između sesija za teme i sesija za redove: `TopicConnection` podržava jednu ili više **sesija za teme**, a `QueueConnection` jednu ili više **sesija za redove**.

**Selektor → C)** — „selektorom poruke — predikatom definisanim preko vrednosti u delovima zaglavlja i svojstava poruke (ne tela)."

---

### 21. Šta sadrži zaglavlje JMS poruke? Koja je uloga svojstava JMS poruke? Navesti i objasniti načine prijema poruka u JMS-u i navesti mehanizme koji se koriste za realizaciju svakog načina prijema.
`26-JUL · 6`

**Zaglavlje sadrži** sve informacije potrebne za **identifikaciju i rutiranje** poruke:
- odredište (referenca na temu ili red)
- prioritet poruke
- datum isteka
- ID poruke
- vremenska oznaka (timestamp)

*Većinu ovih polja kreira JMS provajder, ali neka mogu biti popunjena od strane korisnika.*

**Uloga svojstava (properties):** definisana su **od strane korisnika** i koriste se za **povezivanje dodatnih metapodataka aplikacije sa porukom** — npr. kod sistema svesnog konteksta, svojstva mogu izraziti dodatni kontekst poput polja za lokaciju. Zajedno sa zaglavljem, svojstva su ono nad čime rade **selektori poruka**.

**Dva načina prijema poruka i njihovi mehanizmi:**

| Način | Mehanizam |
|---|---|
| **Sinhroni (blokirajući)** | Program **blokira** korišćenjem operacije **primanja** (`receive`) i čeka dok poruka ne stigne. |
| **Asinhroni (event-driven)** | Program **uspostavlja objekat slušaoca poruka — `MessageListener`** — koji mora obezbediti metodu **`onMessage`**, koja se poziva kad god se identifikuje odgovarajuća dolazeća poruka. |

*(Potrošač je složeniji od proizvođača iz dva razloga: mogućnost povezivanja **selektora** i postojanje ova **dva moda prijema**.)*

---

## TEMA 4 · Otpornost na greške

---

### 22. Koji tipovi grešaka u odnosu na trajanje postoje? Dati primer za svaku od njih. Objasniti razliku između „mirnih" i Vizantijskih grešaka.
`2024 · 7a` 🔁

**U odnosu na trajanje:**

| Tip | Opis | Primer |
|---|---|---|
| **Prolazne (transient)** | Pojave se **jednom** i nestanu | Istekao timeout za prenos poruke, ali nakon ponovnog slanja poruka je uspešno otposlata |
| **Periodične (intermittent)** | Greška se pojavi, nestane, pa se **ponovo pojavi**. **Najnezgodniji tip** | Greška u mrežnom kablu kod koga postoji povremeni prekid |
| **Stalne (permanent)** | Postoje **sve dok se komponenta ne zameni** ispravnom | Greška u hard disku, pregorela komponenta, bagovit softver |

**Mirna greška (fail-silent / fail-stop)** — komponenta **prestaje sa radom** i ne generiše nikakav izlaz ili generiše poruku o grešci.

**Vizantijska greška** — komponenta **nastavlja sa radom i generiše pogrešan rezultat.** Nemamo jasnu naznaku da li je komponenta otkazala.

**Koju je teže detektovati:** **Vizantijsku** — komponenta funkcioniše i generiše neki rezultat, koji je nekad očekivan a nekad ne, pa je teško identifikovati problematičnu komponentu. Mirna greška se lako identifikuje jer komponenta uopšte ne radi.

*(Napomena: vremenska redundansa/retransmisija pomaže kod prolaznih i periodičnih, ali **NE** kod stalnih grešaka.)*

---

### 23. Šta se podrazumeva pod raspoloživošću a šta pod pouzdanošću sistema? Koliko je replika potrebno ako se moraju tolerisati a) f mirnih grešaka; b) f grešaka Vizantijskog tipa; c) f grešaka Vizantijskog tipa pri čemu sistem mora postići konsenzus.
`26-JUL · 11` 🔁

- **Raspoloživost (availability)** — sistem koji je **spreman za korišćenje u trenutku kada je to potrebno**
- **Pouzdanost (reliability)** — osobina sistema da **kontinualno radi (duže vreme) bez nastupanja grešaka**
- **Razlika je u VREMENU:** raspoloživost se odnosi na **konkretan trenutak**, a pouzdanost na **dug vremenski period**

**Broj replika:**

| | Broj replika | Obrazloženje |
|---|---|---|
| **a) f mirnih grešaka** | **f + 1** | ako f otkaže, ostaje bar 1 koja korektno radi |
| **b) f Vizantijskih grešaka** | **2f + 1** | f može generisati pogrešne rezultate, ali f+1 radi korektno i **većinskim glasanjem** izglasava korektan rezultat |
| **c) f Vizantijskih + konsenzus** | **3f + 1** | Lamportov algoritam: mora postojati bar **2f+1 lojalnih** — **više od 2/3 mora biti lojalno** |

---

### 24. Koji sistem je otporan na greške Vizantijskog tipa?
`25-SP · 6` — **ZAOKRUŽIVANJE**

- ❌ **a.** Sistem koji je otporan na otkaz više čvorova
- ❌ **b.** Sistem koji je otporan na gubitak više poruka
- ❌ **c.** Sistem koji je otporan na podelu mreže
- ✅ **d.** Sistem koji je otporan na maliciozne čvorove ← **TAČNO**

**Obrazloženje:** „Vizantijska greška — komponenta **nastavlja sa radom i generiše pogrešan rezultat**." To je upravo ponašanje **malicioznog (zlonamernog) čvora** — čvora koji šalje netačne i kontradiktorne informacije da bi sprečio ostale da postignu konsenzus (problem Vizantijskih generala).
- **a ❌** — otkaz čvorova (prestanak rada) je **mirna** greška
- **b ❌** — gubitak poruka je problem **komunikacionih kanala** (problem dve armije)
- **c ❌** — podela mreže je takođe problem kanala/povezanosti

---

### 25. Klijent je uputio zahtev serveru. Odgovor nije stigao. Da li klijent može da zaključi da li je došlo do otkaza servera ili je izgubljen odgovor? Obrazložiti.
`26-JUN · 14b`

**NE, klijent ne može da razlikuje ove situacije.**

„Klijent ne može da razlikuje ove dve situacije, jer on **vidi samo istek timeout-a**."

Sve četiri RPC greške klijentu izgledaju identično — kao istek timeout-a:
1. Klijent ne može da locira server
2. Zahtev upućen ka serveru je izgubljen
3. Otkaz servera nakon prijema zahteva (a **procedura je možda već izvršena, a možda nije** — ni to ne može razlikovati)
4. Odgovor servera ka klijentu je izgubljen

**Posledica:** zato ne postoji **exactly-once** semantika. Klijent bira između:
- **at-least-once** (ponavlja zahtev dok ne dobije odgovor — bezbedno **samo za idempotentne** procedure)
- **at-most-once** (odmah emituje exception — za procedure koje menjaju stanje sistema)

*Primer sa štampačem: ne postoji nijedna kombinacija klijentske i serverske strategije koja garantuje da će se štampanje obaviti tačno jednom za svaku kombinaciju događaja na strani servera.*

---
---

# 🟠 4/5

---

## TEMA 5 · Osnovne osobine DS — skalabilnost, transparentnosti, heterogenost, middleware

---

### 26. Šta predstavlja skalabilnost distribuiranog sistema? Navesti i objasniti tehnike skaliranja distribuiranih sistema.
`2024 · 1` 🔁 `25-NP · 1` 🔁 `26-JUN · 1b` — **javlja se 3×**

**Skalabilnost (proširljivost)** — mogućnost proširenja sistema, tj. dodavanja novih računara. Posmatra se kroz **3 dimenzije**:
1. Skalabilnost u odnosu na **broj korisnika i resursa**
2. Skalabilnost u odnosu na **geografsku udaljenost** resursa i korisnika
3. **Administrativna skalabilnost** — sistemom se može lako upravljati čak i ako se prostire kroz više administrativnih domena

**3 TEHNIKE SKALIRANJA:**

**1. Skrivanje komunikacionog kašnjenja**
Koriste se **asinhrone** komunikacije umesto sinhronih — klijent se ne blokira dok čeka odgovor, već radi drugi posao; kada stigne odgovor generiše se prekid. Asinhrone komunikacije nisu od koristi kod **interaktivnih** aplikacija — tada je rešenje **download-ovati deo koda na klijentsku stranu**.

**2. Distribucija**
Komponente se **dele na manje delove**, a zatim se ti delovi distribuiraju na više mašina u sistemu. *Primer: DNS i Web.*

**3. Replikacija**
Postavljanje **kopije resursa blizu mesta korišćenja** da bi se smanjilo komunikaciono kašnjenje i balansiralo opterećenje.

**Problem tehnika skaliranja:** postojanje više kopija dovodi do **problema konzistencije** — modifikacija jedne kopije dovodi do neslaganja sa ostalima; za sinhronizaciju bi bila potrebna **globalna sinhronizacija**, koju je gotovo nemoguće postići u DS-u.

**Decentralizovani algoritmi** (preduslov skalabilnosti): nijedna mašina nema kompletnu sliku o stanju sistema; odluke se donose na osnovu lokalnih podataka; otkaz mašine ne ruši sistem; nema pretpostavke o globalnom časovniku.

---

### 27. Šta se podrazumeva pod transparentnošću konkurencije?
`2024 · 2` — **ZAOKRUŽIVANJE**

- ❌ **a)** sve niti mogu pristupati deljivim strukturama podataka
- ✅ **b)** procesi mogu pristupati resursima bez međusobne interferencije ← **TAČNO**
- ❌ **c)** repliciranim resursima se pristupa kao da postoji samo jedna kopija
- ❌ **d)** novi čvorovi se mogu dodati sistemu bez promene aplikacije

**Obrazloženje:** „Transparentnost konkurencije omogućava da više konkurentnih procesa, tj. više procesa istovremeno koristi isti resurs, **a da korisnici ne primete** da se isti resurs koristi istovremeno. Ovakav istovremeni pristup istom resursu mora da ostavi resurs u konzistentnom stanju, i to se postiže **serijalizacijom** deljenog resursa."
- **c)** je definicija **transparentnosti replikacije**
- **d)** je definicija **skalabilnosti / otvorenosti**

---

### 28. Koje su osobine sistema koji poseduje transparentnost konkurencije?
`25-NP · 2` — **ZAOKRUŽIVANJE (više odgovora)**

- ✅ **a)** Rezultat istovremenih zahteva isti je kao i kada bi se izvršavali sekvencijalno. ← **TAČNO**
- ✅ **b)** Korisnik ne mora da zna da postoje drugi konkurentni korisnici. ← **TAČNO**
- ❌ **c)** Korisnik mora ručno sinhronizovati pristup deljenim resursima
- ✅ **d)** Sistem sprečava nekonzistentnost podataka tokom istovremenog pristupa. ← **TAČNO**

**Obrazloženje:**
- **a ✅** — konzistentnost se postiže **serijalizacijom** pristupa ⇒ rezultat je isti kao da su se zahtevi izvršili sekvencijalno
- **b ✅** — „Važno je postići da korisnici ne primete da se isti resurs koristi jednovremeno"
- **c ❌** — upravo suprotno: **sistem** obavlja sinhronizaciju (mehanizmima zaključavanja i sinhronizacije), a **ne korisnik**. Da korisnik mora ručno da sinhronizuje, transparentnosti ne bi ni bilo.
- **d ✅** — „Jednovremeni pristup deljivom resursu mora ostaviti resurs u konzistentnom stanju"

---

### 29. Šta se podrazumeva pod transparentnošću replikacije?
`25-NP · 3` — **ZAOKRUŽIVANJE**

- ❌ **a)** proces je svestan šeme replikacije i može to iskoristiti
- ✅ **b)** repliciranim resursima se pristupa kao da postoji samo jedna kopija ← **TAČNO**
- ❌ **c)** resurs će upravljati svim zahtevima na isti način bez obzira na lokaciju klijenta
- ❌ **d)** podaci se ne mogu menjati i mogu se zapamtiti na disku

**Obrazloženje:** „Transparentnost replikacije omogućava postojanje većeg broja replikacija (kopija) istog resursa u DS-u da bi se povećale performanse i smanjilo komunikaciono kašnjenje. Kopija resursa se postavlja bliže mestu odakle se obavlja pristup, a **sve kopije imaju isto ime**. **Korisnici ne treba da budu svesni postojanja više kopija resursa.**"
- **a ❌** — direktna suprotnost: proces **ne sme** biti svestan replikacije
- Dodatno: ako DS podržava replikaciju, **automatski podržava i lokacionu transparentnost** (jer sve kopije imaju isto ime).

---

### 30. Šta se podrazumeva pod pristupnom transparentnošću?
`26-JUN · 1c` — **ZAOKRUŽIVANJE**

- ❌ **A.** udaljenim resursima se pristupa korišćenjem lokaciono nezavisnih imena
- ✅ **B.** lokalnim i udaljenim resursima se pristupa korišćenjem istih operacija ← **TAČNO**
- ❌ **C.** repliciranim resursima se pristupa kao da postoji samo jedna kopija
- ❌ **D.** resurs će upravljati svim zahtevima na isti način bez obzira na lokaciju klijenta

**Obrazloženje:** „Pristupna transparentnost — podacima i resursima se pristupa **na jedinstven način, bez obzira da li se oni nalaze na udaljenom ili lokalnom računaru**."
Primer: različiti OS mogu koristiti različite načine imenovanja fajlova; razlike u imenovanju i manipulaciji fajlovima moraju biti skrivene od korisnika i aplikacije.
- **A ❌** — to je **lokacijska** transparentnost (postiže se logičkim imenima, npr. URL)
- **C ❌** — to je **transparentnost replikacije**

---

### 31. Objasniti migracionu transparentnost i transparentnost paralelizacije.
`26-JUL · 1b`

**Migraciona transparentnost** — resurs može **promeniti svoju lokaciju a da klijent to ne primeti ni zna**. Resurs se može kretati a da pri tome **ne dolazi do promene imena resursa**.
*Primer: mobilni telefoni — pozivalac i pozvani nisu svesni kretanja onog drugog.*

**Transparentnost paralelizacije** — **paralelizacija procesa se izvršava transparentno** za aplikativnog programera i korisnika aplikacije. Korisnik nije svestan **da i kako** je aplikacija paralelizovana i **gde se izvršavaju procesi**.
*Primer: **Hadoop**.*

**Svih 7 tipova transparentnosti** (ako traži sve):
1. **Pristupna** — jedinstven način pristupa, bez obzira da li je resurs lokalni ili udaljeni
2. **Lokacijska** — korisnik ne zna gde je resurs fizički lociran; postiže se logičkim imenima (URL)
3. **Migraciona** — resurs menja lokaciju bez promene imena i bez znanja klijenta
4. **Konkurencije** — više procesa istovremeno koristi isti resurs, a da to ne primete; postiže se serijalizacijom
5. **Replikacije** — više kopija istog resursa, sve sa istim imenom; korisnik nije svestan
6. **Za otkaze** — korisnik ne zna da je resurs u kvaru; **jedan od najtežih problema u DS** (teško je razlučiti da li je server nedostupan ili je mreža zagušena)
7. **Paralelizacije** — korisnik nije svestan da i kako je aplikacija paralelizovana

---

### 32. Kroz koje sve aspekte se manifestuje heterogenost u distribuiranim sistemima?
`26-JUL · 1a`

**DS je sastavljen od heterogenog skupa računara.** Heterogenost se ogleda u sledećem:

1. **Hardver računara** — različit skup instrukcija, različita interna prezentacija podataka
2. **Operativni sistemi** — interfejs za razmenu poruka razlikuje se od OS-a do OS-a (npr. Berkeley sockets i WinSock)
3. **Programski jezici** — karakteri i strukture podataka se različito predstavljaju u različitim jezicima, što je problem ako aplikacije treba međusobno da komuniciraju
4. **Implementacije od strane različitih projektanata** — dok se ne usvoje zajednički standardi, različite implementacije ne mogu međusobno da komuniciraju (**OSI model** je jedan način rešavanja problema heterogenosti)

*(Heterogenost je jedna od 4 osnovne osobine DS-a: heterogenost, transparentnost, otvorenost, skalabilnost.)*

---

### 33. Šta je funkcija middleware-a u distribuiranom sistemu?
`26-JUN · 1a` 🔁

**Osnovni cilj middleware-a je da se sakrije heterogenost platforme na kojoj je sistem izgrađen — od same aplikacije, kao i da se sakrije komunikacija.** Mrežni OS se nadograđuje dodatnim SW slojem — middleware-om.

- Middleware je računarski softver koji **pruža usluge (servise) višeg nivoa** softverskim aplikacijama
- **Middleware komunikacioni protokoli** oslobađaju aplikativnog programera detalja vezanih za komunikaciju između procesa — **komunikacija je skrivena iza poziva procedure ili metoda** (RPC / RMI)
- Neke usluge koje pružaju: **autentifikacija i autorizacija**
- Middleware sistemi nude kompletan skup usluga i **ne dozvoljavaju korišćenje ničeg drugog do njihovih interfejsa** prema uslugama

**Modeli middleware-a:**
- **RPC model** — resursi se modeluju kao **procedure**; skriva se mrežna komunikacija, proces ima utisak da je pozvao lokalnu proceduru
- **Objektno-orijentisan model** — resursi se modeluju kao **objekti** (podaci + funkcije nad podacima); svaki objekat implementira **interfejs** koji skriva unutrašnje detalje; **objekat nije distribuiran, već interfejs**

**Razlika u odnosu na ostale OS:**
- **Distribuirani OS** — čvrsto spregnut OS za multiprocesore i **homogene** multiračunare; čini da skup nezavisnih računara korisnicima izgleda kao jedan računar
- **Mrežni OS** — **slabo spregnut** OS za **heterogene** multiračunare; omogućava korisnicima da pristupe uslugama na drugoj mašini
- **Middleware** — dodatni sloj **iznad mrežnog OS-a** koji sakriva heterogenost i komunikaciju

---

## TEMA 6 · Logički i vektorski časovnici

---

### 34. Ako se koriste vektorski časovnici, šta je najviše što možemo doznati?
`25-SP · 1` — **ZAOKRUŽIVANJE**

- ❌ **a)** ako je V(a) < V(b) tada se a desilo pre b
- ✅ **b)** V(a) < V(b) ako i samo ako se a desilo pre b ← **TAČNO**
- ❌ **c)** ako se a desilo pre b tada je V(a) < V(b)
- ❌ **d)** ako je V(a) = V(b) tada su a i b neuređeni

**Obrazloženje:** pitanje traži **NAJJAČU** tvrdnju koja važi. Iz materijala:
„Za bilo koja 2 događaja e i e′ važi sledeće: **Ako je e → e′ tada važi V(e) < V(e′)**; **Ako je V(e) < V(e′) tada važi e → e′**."

Kod vektorskih časovnika važe **OBA smera**, dakle **ekvivalencija (ako i samo ako)** — to je odgovor **b)**. Odgovori a) i c) su svaki po jedan smer te ekvivalencije i takođe su tačni, ali **nisu „najviše što možemo doznati"**.
- **d) netačno** — ako je `V(a) = V(b)`, događaji su **jednaki**, a ne neuređeni. Konkurentni (neuređeni) su kada se **ne može uspostaviti** ni `V(e) ≤ V(e′)` ni `V(e) ≥ V(e′)`.

⚡ **Ključna razlika u odnosu na Lamporta:** kod Lamportovih markica važi **samo jedan smer** (`a→b ⇒ T(a)<T(b)`), pa se **NE MOŽE** utvrditi da li su događaji uslovljeni. Vektorski časovnici to mogu.

**Pravila za implementaciju vektorskih časovnika (obavezno znati uz ovo):**
1. Vektor se **inicijalizuje na 0** u svim procesima: `Vi[j] = 0`, za `i, j = 1…N`
2. Proces `Pi` **inkrementira i-ti element** svog lokalnog vektora **pre nego što obeleži lokalni događaj**: `Vi[i]++`. Poruka se šalje zajedno sa `Vi`.
3. Kada `Pj` primi poruku, **poredi lokalni vektor sa primljenim, element po element, i postavlja svaki element na VEĆU od dve vrednosti**

**Poređenje:** `V = V′` ako `V[i] = V′[i]` za svako i · `V ≤ V′` ako `V[i] ≤ V′[i]` za svako i.
**Značenje:** `Vi[i]` = broj događaja u `Pi`. Ako je `Vi[j] = k` (j≠i), `Pi` zna da se u `Pj` desilo `k` događaja.

---

### 35. Šta će se dogoditi ako se koristi Ricart-Agrawala algoritam i dva procesa koji žele pristup istoj kritičnoj sekciji generišu zahtev sa istom vrednošću Lamportove markice?
`26-JUN · 10a`

**Problem:** „Moguće je da više konkurentnih događaja (koji nisu međusobno uslovljeni) imaju **iste vremenske markice**, što nije poželjno! Ovo dovodi do konfuzije ako više procesa treba da donese odluku na osnovu vremenskih markica dva događaja."

Kod Ricart-Agrawala se pobednik bira po **manjoj markici** — ako su markice iste, remi se ne može razrešiti i **oba procesa bi mogla da uđu u kritičnu sekciju** (narušeno uzajamno isključivanje) ili bi nastao **deadlock** (oba čekaju).

**REŠENJE:** „Prisiliti da svaka markica bude **jedinstvena**. Lamportovoj vremenskoj markici dodati **još jedan identifikator** koji predstavlja **globalno jedinstveni id procesa** u kome je nastao događaj (adresa hosta + proces ID)."

➜ Markica postaje par **`(T, id_procesa)`** i poredi se **leksikografski**: prvo po T, a kod jednakog T **pobeđuje manji id procesa**. Time je remi uvek razrešen deterministički i svi procesi donose istu odluku.

---

## TEMA 7 · Grupna komunikacija i uređenje poruka

---

### 36. Šta je definicija potpuno uređene grupne komunikacije?
`2024 · 5` — **ZAOKRUŽIVANJE**

- ❌ **a)** poruke se isporučuju procesima u FIFO redosledu
- ❌ **b)** poruke se procesima isporučuju po redosledu realnog vremena
- ❌ **c)** poruke se isporučuju procesima po redosledu „desilo se pre"
- ✅ **d)** poruke se isporučuju svim procesima po istom redosledu ← **TAČNO**

**Obrazloženje:** „Obezbediti potpuno uređenu grupnu komunikaciju znači obezbediti operaciju kojom se **sve poruke isporučuju u istom redosledu svim prijemnicima**."
- **a) FIFO konzistencija** — samo poruke iz istog izvora u redosledu slanja
- **b) realno vreme** — nemoguće u DS-u (nema globalnog časovnika)
- **c) „desilo se pre"** — to je **kauzalno (uslovno) uređenje**, slabije od totalnog

---

### 37. Šta je definicija potpuno uređene grupne komunikacije? *(otvoreno pitanje)*
`25-SP · 2` 🔁 *(isto pitanje kao br. 36, bez ponuđenih odgovora)*

**Definicija:** Potpuno uređena grupna komunikacija odnosi se na komunikaciju u kojoj **sve replike treba da prime isti skup poruka u istom redosledu**, tj. da sve replike budu u konzistentnom stanju. Obezbediti je znači obezbediti **operaciju (multicast) kojom se sve poruke isporučuju u istom redosledu svim prijemnicima**.

**Klasični primer (obavezno navesti):** replicirani bankovni računi u New York-u i San Francisco-u. Stanje je 1000$. U SF se dodaje 100$, u NY se istovremeno dodaje 1% kamate.
- NY: prvo kamata (1010$), pa +100$ → **1110$**
- SF: prvo +100$ (1100$), pa kamata → **1111$**

➜ **To ne sme da se desi!** Mora se obezbediti da se operacije ažuriranja obave **u istom redosledu** na svim replikama.

**Algoritam (Lamportove markice + ACK):**
- Poruka ažuriranja se obeležava **logičkim vremenom izvora** (proširene markice `T.id`)
- Poruka se prosleđuje **svima u grupi (pa i sebi)**
- Kada poruka stigne: smešta se u **lokalni queue čekanja**, poruke se uređuju po markicama, i **ACK se prosleđuje svima (pa i sebi)**
- Poruke iz istog izvora se isporučuju u redosledu slanja (**FIFO**)
- **Poruka se prosleđuje aplikaciji samo ako je na vrhu reda i potvrđena od strane SVIH procesa**
- `Pi` šalje ACK za poruku od `Pj` ako: `Pi` nije poslao poruku ažuriranja, **ili** je `id(Pi) > id(Pj)`, **ili** je zahtev `Pi` već obrađen

---

### 38. Koje časovnike ćete koristiti ako je potrebno ostvariti: a) totalno uređenu grupnu komunikaciju (multicast); b) uslovno (kauzalno) uređenu grupnu komunikaciju?
`25-NP · 8`

**a) Totalno uređena grupna komunikacija (multicast) → LAMPORTOVI (skalarni) LOGIČKI ČASOVNICI**
Poruka ažuriranja se obeležava logičkim vremenom izvora (proširena markica `T.id`), poruke se u redovima čekanja uređuju po markicama i isporučuju aplikaciji tek kada su potvrđene od svih procesa. Tako **svi procesi vide isti redosled**.
*(Alternativa iz materijala: **centralni koordinator** koji svakoj write operaciji dodeljuje jedinstven id — ali može postati usko grlo.)*

**b) Uslovno (kauzalno) uređena grupna komunikacija → VEKTORSKI ČASOVNICI**
„Implementacija uslovne konzistencije obavlja se pomoću **vektorskih časovnika**."
Pravilo: **časovnik se inkrementira SAMO kod slanja poruke, ne i kod prijema.** Poruka `m` se ne prosleđuje procesu `Pj` dok se ne zadovolje **dva uslova**:
- **a)** `tm[i] = Vj[i] + 1` — obezbeđuje da je `Pj` primio sve prethodne poruke od `Pi`
- **b)** `tm[k] ≤ Vj[k]`, za svako `k ≠ i` — obezbeđuje da je `Pj` primio sve poruke koje je `Pi` primio pre slanja `m`

Ako uslovi nisu ispunjeni, poruka se **baferuje** dok ne stignu poruke koje joj prethode.

---

### 39. Objasniti razliku između potpuno uređene grupne komunikacije (total ordering) i uređenja međusobno zavisnih događaja (causal ordering). Da li totalno uređenje garantuje kauzalno uređenje?
`26-JUN · 12`

| | **Totalno uređenje (total ordering)** | **Kauzalno uređenje (causal ordering)** |
|---|---|---|
| Zahtev | **Sve** poruke se isporučuju **svim** procesima u **istom redosledu** | Samo **potencijalno uslovljene** (kauzalno povezane) poruke moraju se svuda videti u istom redosledu |
| Konkurentne poruke | moraju i one biti u istom redosledu svuda | mogu se videti u **različitom** redosledu u različitim procesima |
| Časovnici | Lamportovi (skalarni) | **Vektorski** |
| Jačina | **jači** zahtev | **slabiji** zahtev |

**Da li totalno uređenje garantuje kauzalno? NE — ne obavezno.**

Totalno uređenje garantuje samo da **svi vide ISTI redosled**, ali ne garantuje da je taj redosled **saglasan sa kauzalnošću**. Moguće je da svi procesi isporuče poruku `m2` (odgovor) pre poruke `m1` (uzrok) — svi vide isti redosled (totalno uređenje ✓), ali je kauzalnost narušena (✗).

➜ Zato materijal za kauzalno uređenje traži **posebnu implementaciju vektorskim časovnicima sa baferovanjem**: „Ako se šalje poruka m1, može se desiti da proces P3 prvo primi njen odgovor m2 pa tek onda poruku m1, što nije dobro, pa se mora obezbediti baferovanje poruke m2 dok ne stigne m1."

Da bi bilo i totalno **i** kauzalno, potrebno je **kauzalno-totalno uređenje** (kombinacija oba mehanizma).

---
---

# 🟡 3/5

---

## TEMA 8 · Replikacija

---

### 40. Koje vrste replika postoje? Koje se informacije mogu proslediti replikama kada se obavlja ažuriranje? Koji protokol konzistencije se koristi za postizanje sekvencijalne konzistencije?
`25-NP · 11` 🔁

**VRSTE REPLIKA (3):**
1. **Permanentne replike** — početni skup replika koje obrazuju distribuirano skladište podataka; broj im je relativno mali (npr. klaster servera na jednoj lokaciji sa Round Robin raspodelom, ili mirror lokacije preko web hostinga)
2. **Replike inicirane od strane servera** — **privremene**, postavljaju se u regione odakle dolazi veliki broj zahteva. Server vodi računa o **broju pristupa fajlu i odakle pristupi dolaze**; ako broj obraćanja pređe **prag replikacije**, kreira se replika bliže klijentima
3. **Replike inicirane od strane klijenta** — **keš** na klijentu ili proxy serveru u istom LAN-u. **Server nema nikakvu odgovornost da održava ove kopije konzistentnim** — klijent je dužan da to radi sam

**ŠTA SE PROSLEĐUJE PRI AŽURIRANJU (3 opcije; cilj je smanjenje saobraćaja):**
1. **Samo obaveštenje o obavljenom ažuriranju** (invalidacija ostalih kopija) — pogodno kada je **odnos broja upisa naspram čitanja veliki**
2. **Slanje modifikovanih podataka** — pogodno kada je **broj čitanja mnogo veći od broja modifikacija**
3. **Slanje operacija koje su izvršene** nad modifikovanim podacima — poznato kao **AKTIVNA REPLIKACIJA**; prenosi se samo komanda umesto velikog fajla → minimalan saobraćaj, ali **više procesorskog vremena** jer svaka replika izvršava komande

**PROTOKOL ZA SEKVENCIJALNU KONZISTENCIJU:**
**Protokoli zasnovani na postojanju primarne kopije** — „Najjači vid konzistencije koji se može ostvariti u DS-u je sekvencijalna konzistencija i za njega se koriste protokoli zasnovani na postojanju primarne kopije."
Dve varijante:
- **Remote-write** — primar je **fiksiran** na jednoj lokaciji; sve modifikacije idu preko njega. „Primar može urediti sve dolazne zahteve za modifikacijom na globalno jedinstveni način tako da svi procesi vide sve write operacije u istom redosledu."
- **Local-write** — primar **privremeno migrira** na lokalnu kopiju klijenta koji je inicirao ažuriranje

*(Ostale dve vrste protokola: **replicirani write protokoli** — aktivna replikacija ili kvorum; i **keš koherentni protokoli** — inicirani od klijenta.)*

---

### 41. Bankovni server je repliciran. Razmatraju se dva dizajna: i) sve replike izvršavaju sve zahteve; ii) jedan server izvršava zahteve i periodično ažurira replike. Koje tehnike repliciranja se koriste u slučaju i) a koje u slučaju ii)?
`26-JUN · 14a`

**i) Sve replike izvršavaju sve zahteve → AKTIVNA REPLIKACIJA**
- Koristi se kod **grupe ravnopravnih procesa (RAVNE GRUPE)** — svi procesi imaju jednaku funkciju
- Klijentski zahtev se prosleđuje **svim** procesima u grupi; svi obrađuju zahtev **u istom redosledu**
- Zahteva **potpuno uređenu grupnu komunikaciju** (Lamportove markice ili centralni koordinator)
- Klijentski interfejs koristi **većinsko glasanje** da prosledi tačan odgovor klijentu
- **Prednost:** dobro se nosi sa **Vizantijskim** greškama · **Mana:** problem sinhronizacije

**ii) Jedan server izvršava i periodično ažurira replike → PASIVNA REPLIKACIJA (primary-backup)**
- Koristi se kod **hijerarhijski organizovanih grupa** — 1 koordinator (**primar**), ostali su **backup** procesi
- Primarni server obavlja ceo posao; ako otkaže, jedan od backup servera preuzima posao (otkaz treba da bude nevidljiv za aplikaciju)
- Backup detektuje otkaz preko **heartbeat** poruka („Da li si živ?"); ako nema odgovora u timeout-u, bira se novi primar (**konsenzus**)
- **Prednost:** jednostavniji dizajn, nema problema sa sinhronizacijom (poruka ide samo primaru) · **Mana:** **loše radi sa Vizantijskim greškama** — primar može pogrešno raditi a da greška ne bude otkrivena

---

## TEMA 9 · Uzajamno isključivanje

---

### 42. Da li Ricart & Agrawala algoritam uzajamnog isključivanja koristi logičke časovnike? Kratko obrazložiti odgovor.
`25-SP · 3`

**DA, koristi.**

„Distribuirani algoritam: Ricart & Agrawala … **Koristi logičke časovnike i grupnu komunikaciju**."

Kada proces želi da uđe u kritičnu sekciju, generiše poruku koja sadrži **tri stvari**:
1. **ID tog procesa**
2. **ime željenog resursa**
3. **logički časovnik** (Lamportova vremenska markica)

Logički časovnik je **neophodan za razrešavanje sukoba**: kada dva procesa istovremeno traže istu kritičnu sekciju, svaki poredi **vremensku markicu svog zahteva sa markicom primljenog zahteva** — **proces sa manjom vremenskom markicom pobeđuje**. Bez logičkih časovnika ne bi postojao kriterijum za odlučivanje ko ima prednost.

**Kako algoritam radi (za slučaj da traži i to):** zahtev se šalje **svim** procesima u grupi; da bi se ušlo u KS, čeka se **dozvola od svih**. Kada proces A primi zahtev od B:
- ako **nije zainteresovan** za resurs → odmah šalje **OK**
- ako se **već nalazi u KS** → ne odgovara, smešta zahtev u svoj **red čekanja**
- ako je **i sam poslao zahtev** → poredi markice; **manja markica pobeđuje**. Ako pobedi B → šalje OK; ako pobedi A → smešta zahtev u red čekanja
- kada A **okonča** pristup KS → šalje OK **svim zahtevima iz svog reda čekanja**

**Broj poruka: 2(n−1)** (n−1 zahtev + n−1 potvrda).
**Mana:** greška u bilo kom prenosu blokira ceo sistem; ako jedan proces otkaže, pristup resursu je nemoguć.

---

## TEMA 10 · Sinhronizacija fizičkih časovnika

---

### 43. Zašto je u DS teško postići savršenu sinhronizaciju časovnika?
`26-JUN · 6` 🔁

**Osnovni razlog: NE POSTOJI GLOBALNI ČASOVNIK.**

Detaljno:
1. **Svaka mašina ima svoj časovnik** — procesi na različitim računarima imaju različitu predstavu o vremenu
2. **Drift kristala** — tajmer je kvarcni kristal; brzina oscilacije zavisi od **vrste kristala, načina sečenja i veličine napona**. Ako sistem ima N računara, **svih N kristala će oscilovati sa neznatno različitim brzinama**, pa softverski časovnici postepeno ispadaju iz sinhronizma
3. **Komunikaciono kašnjenje** — sve metode sinhronizacije svode se na **razmenu vrednosti časovnika između računara**, a tu je problem što se **kašnjenje ne može tačno izmeriti**; dok poruka sa vremenom stigne, to vreme je već zastarelo
4. **Sinhronizacija se mora stalno ponavljati** — tokom vremena časovnici opet izađu iz sinhronizma

➜ **Zato se često koriste logički časovnici** — nije uvek potrebno apsolutno vreme, već je dovoljno da se **uzajamno zavisni događaji odvijaju u korektnom redosledu**, što je mnogo lakše postići.

*(Kako radi tajmer: kvarcni kristal pod naponom osciluje; uz tajmer idu **brojač** i **holding registar**. Holding registar pamti inicijalnu vrednost brojača. Svaka oscilacija **dekrementira** brojač; kada brojač postane 0, generiše se **prekid** i lokalni časovnik se inkrementira za 1 vremensku jedinicu, pa se brojač vraća na vrednost iz holding registra. Svaki prekid je jedan **otkucaj časovnika**.)*

---

### 44. Kristijanov algoritam usvaja da je:
`26-JUN · 7` — **ZAOKRUŽIVANJE.** Odabrati korektan odgovor i obrazložiti ga.

- ❌ **a)** Kašnjenje kroz mrežu tačno poznato
- ✅ **b)** Kašnjenja zahteva i odgovora su približno jednaka ← **TAČNO**
- ❌ **c)** Serverski časovnik je uvek sporiji
- ❌ **d)** Svi klijenti međusobno komuniciraju da bi odredili tačno vreme

**Obrazloženje:** „Da bi klijent uskladio svoj časovnik on mora da uzme u obzir propagaciono kašnjenje između klijenta i servera tačnog vremena i to određuje razliku **(T1 − T0)/2**, gde je T0 trenutak kada je klijent uputio zahtev, a T1 trenutak kada je klijent primio odgovor."

**Deljenje kružnog vremena sa 2** ima smisla samo pod pretpostavkom da je kašnjenje **simetrično** — da poruka zahteva i poruka odgovora putuju **približno jednako dugo**.

- **a ❌** — kašnjenje **nije** tačno poznato; da jeste, ne bi bilo potrebe za procenom deljenjem sa 2 (i to je glavni izvor greške algoritma)
- **c ❌** — nema takve pretpostavke; server tačnog vremena je referenca (ima WWV prijemnik za UTC), a ne „sporiji"
- **d ❌** — to opisuje **Berkeley** algoritam. Kod Kristijana je **server pasivan** — samo daje info o tačnom vremenu, a klijenti međusobno **ne** komuniciraju

**Kako radi Kristijanov algoritam:** zahteva **eksterni izvor tačnog vremena** — jedna mašina je opremljena **WWV prijemnikom** (server tačnog vremena) koji prima signale sa radio stanice o **UTC** vremenu. Svaki računar šalje poruku serveru i traži tačno vreme; server je **pasivan**.
**Formula:** `T_novo = C_utc + (T1 − T0)/2`
**Vreme nikad ne sme da ide unazad!** Ako klijent dobije vreme manje od svog trenutnog, promena se vrši **postepeno** — holding registar se poveća, čime se časovnik klijenta **uspori** dok ne dostigne željenu vrednost.

---

### 45. Zašto Berkeley algoritam šalje korekciju vremena umesto tačnog vremena?
`26-JUN · 8`

Berkeley obezbeđuje **internu sinhronizaciju** mašina unutar DS-a, **bez potrebe za eksternim izvorom tačnog vremena**. Server vremena obavlja **periodičnu prozivku svake mašine**, izračunava **srednje vreme** i saopštava svim mašinama **kako da podese svoje časovnike**.

**Razlozi za slanje korekcije (a ne apsolutnog vremena):**

1. **Vreme ne sme da ide unazad** — „Vreme nikad ne sme da ide unazad! Nema vremeplova." Ako bi se slalo apsolutno vreme, mašini koja žuri bi ono bilo manje od trenutnog i časovnik bi skočio unazad, što bi narušilo redosled događaja. Slanjem **korekcije** svaka mašina zna **koliko i u kom smeru** da podesi — a ako je korekcija negativna, primenjuje je **postepeno** (usporavanjem časovnika).

2. **Eliminiše se komunikaciono kašnjenje** — dok apsolutno vreme putuje kroz mrežu, ono zastari. **Relativna korekcija ostaje ispravna** bez obzira na to kada tačno stigne.

3. **Cilj Berkeley algoritma nije globalno tačno vreme, već zajedničko vreme.** „Ovaj metod ne zahteva izvor tačnog vremena, već da se u okviru jednog DS-a usklade časovnici. To je sasvim zadovoljavajuće jer je bitno da računari jednog DS-a imaju istu predstavu o vremenu, ne mora to vreme da bude baš globalno tačno."

*(I Kristijanov i Berkeley algoritam su prvenstveno projektovani za **privatne mreže**.)*

---

### 46. Koje informacije o vremenu se nalaze u poruci koja stiže od NTP servera? Kako to klijent koristi da podesi svoj časovnik?
`26-JUL · 7`

**Vremenska polja u NTP poruci (4):**

| Polje | Značenje |
|---|---|
| **Reference timestamp** | vreme kada je sistemski časovnik **poslednji put bio postavljen/korigovan** |
| **Origin timestamp** (`t₀`) | kada je poruka **upućena od klijenta ka serveru** |
| **Receive timestamp** (`t₁`) | kada je poruka **stigla od klijenta na server** |
| **Transmit timestamp** (`t₂`) | kada **server šalje odgovor** klijentu |

*(Uz njih zaglavlje sadrži i: **LI** — da li će zadnji sat tog dana imati prestupnu sekundu, **VN** — verzija protokola, **Mode** — režim rada, **Stratum** — nivo servera, **Poll** — max interval između prozivki, **Precision** — preciznost časovnika, **Root delay** — kružno vreme do referentnog servera, **Reference ID** — identifikator referentnog servera.)*

**Kako klijent koristi ove informacije:**
Klijent beleži i **`t₃`** — trenutak prijema odgovora — pa računa:

- kružno vreme propagacije: **δ = (t₁ − t₀) + (t₃ − t₂)**
- offset svog časovnika: **θ = [(t₁ − t₀) + (t₂ − t₃)] / 2**
- novo (tačno) vreme: **t₃′ = t₃ + θ**  *(ili ekvivalentno `t₂ + δ/2`)*

Ako je dobijeno vreme **veće** od trenutnog, časovnik se postavlja direktno; ako je **manje**, korekcija se vrši **postepeno** (usporavanjem časovnika), jer vreme ne sme da ide unazad.

**Dodatno o NTP-u:** protokol **aplikativnog nivoa**, na transportnom koristi **UDP**, osluškuje na **portu 123**, definisan standardom **RFC 5905**. Serveri se hijerarhijski organizuju u **stratume** (nivo 0 = atomski časovnici/izvori tačnog vremena, nivo 1 = direktno povezani na izvor — najveća preciznost, nivo 2 = povezani sa nivoom 1, itd.). Podržava **3 režima**: **simetrični** (najpreciznije, za master servere), **klijent-server** (sličan Kristijanovom) i **multicast** (brze LAN mreže — server periodično emituje vreme).

---

## TEMA 11 · Hadoop / HDFS

*(⚠️ Ne pojavljuje se ni na jednom roku iz 2026. Ako polažeš po novom programu — preskoči celu ovu temu.)*

---

### 47. Navesti sve demone u Hadoop klasteru, objasniti njihove uloge kao i gde se izvršavaju u Hadoop klasteru. Šta predstavlja blok i koje su prednosti korišćenja blokova kod HDFS?
`2024 · 8b` 🔁

**DEMONI** — postoje **dve vrste**: HDFS demoni i MapReduce demoni.

**HDFS demoni:**

| Demon | Koliko | Gde se izvršava | Uloga |
|---|---|---|---|
| **NameNode (NN)** | **samo jedan** | na **glavnom (master)** čvoru | Centralni kontroler HDFS-a. Održava **fajl sistem namespace**; čuva **metapodatke** (kako su fajlovi podeljeni na blokove, koji slave čvorovi čuvaju koje blokove, faktor replikacije); nadgleda ponašanje DataNode-ova i koordiniše pristup podacima. **NE čuva same podatke** nad kojima se vrši obrada. Vodi računa da svaki blok zadovolji faktor replikacije. Podatke drži u **glavnoj memoriji**. |
| **DataNode (DN)** | **više** | na **slave** čvorovima | Odgovoran za **čuvanje podataka u blokovima**, primanje naredbi od NN i davanje informacija NN-u. Šalje **heartbeat svake 3 sekunde**; **svaki 10. heartbeat je izveštaj o blokovima** (koje blokove čuva). |
| **Secondary NameNode** | jedan | na **serveru na kome NIJE NN** | **NIJE backup NN.** Radi konkurentno sa NN kao pomoćni demon; u regularnim intervalima **preuzima edit log od NN, spaja ga lokalno sa starim checkpoint-om** i formira **novi checkpoint** koji vraća NN-u. NN ga koristi pri sledećem restartu (skraćuje vreme restarta). |

⚠️ **MapReduce demoni** su u prezentaciji `GFS i HDFS 2024.pdf` samo **navedeni kao kategorija** — detalji nisu u tim fajlovima. Ako profesor traži i njih, dopuni sa predavanja/snimka.

**ŠTA JE BLOK:** Hadoop koristi blokove da sačuva fajl ili delove fajla. **Hadoop blok je fajl na fajl sistemu koji se nalazi u osnovi** (HDFS radi nad postojećim lokalnim fajl sistemom svakog čvora). Podrazumevana veličina je **64MB** (u novijim verzijama **128MB**).

**PREDNOSTI BLOKOVA (3):**
1. Imaju **fiksnu veličinu**, pa je lako izračunati koliko će ih stati na disk
2. Raspodelom na više čvorova omogućavaju da **fajl bude veći nego bilo koji pojedinačni disk** u klasteru
3. **Blokovi se repliciraju na više čvorova**, što omogućava HDFS-u da bude **otporan na greške (Fault Tolerant)**

**Otpornost na greške kod HDFS** postiže se **replikacijom blokova**, uz tehniku **Rack Awareness**: za svaki blok jedna kopija ide u jedan rack, a preostale dve u drugi rack — tako gubitak celog rack-a ne uništava sve kopije.

---

### 48. Sledeće pitanje odnosi se na HDFS. Tačna tvrđenja obeležite sa T(tačno) a netačna sa F(netačno). Obrazložiti.
`25-SP · 12` — **T/F**

| # | Tvrdnja | |
|---|---|---|
| **a** | Faktor replikacije može se konfigurisati na nivou klastera i takođe na nivou fajla. | ✅ **T** |
| **b** | Izveštaj o blokovima sa svakog DataNode čvora sadrži listu svih blokova koji su smešteni na tom DataNode čvoru. | ✅ **T** |
| **c** | Korisnički podaci se skladište na lokalnom fajl sistemu DataNode čvorova. | ✅ **T** |
| **d** | DataNode čvor zna kojim fajlovima pripadaju blokovi koji su na njemu smešteni. | ❌ **F** |

**Obrazloženja:**
- **a) T** — „Faktor replikacije se može promeniti u Hadoop konfiguraciji ili čak postaviti faktor za **svaki individualni fajl**." (podrazumevano je 3)
- **b) T** — „DataNode-ovi šalju heartbeat-ove svake tri sekunde i **svaki deseti heartbeat je izveštaj o blokovima, gde DN obaveštava NN koje blokove čuva**."
- **c) T** — „HDFS funkcioniše **nad postojećim fajl sistemom** na svakom čvoru" i „Hadoop blok je **fajl na fajl sistemu koji se nalazi u osnovi**."
- **d) F** — **NameNode** je taj koji zna kako su fajlovi podeljeni na blokove i gde su locirani. DataNode samo čuva blokove i prijavljuje **koje blokove ima**, ne i **kom fajlu pripadaju**.

---

### 49. Koji fajlovi se trajno pamte na lokalnom disku NameNode-a? Koje informacije se ne pamte trajno na lokalnom disku, a neophodne su prilikom restarta NameNode-a? Odakle se dobijaju te informacije?
`25-NP · 14a` 🔁

**Fajlovi koji se TRAJNO pamte na lokalnom disku NameNode-a (2):**

| Fajl | Sadržaj |
|---|---|
| **`fsimage`** (Checkpoint image / **Namespace fajl**) | **snapshot metapodataka fajl sistema** — file namespace i preslikavanje fajl → blokovi |
| **`edit log`** | fajl u kome se **trajno čuvaju sve promene** koje se dešavaju u HDFS NameNode-u |

*(Trajno čuvanje metapodataka NN-a na lokalnom fajl sistemu naziva se **Checkpoint**.)*

**Šta se NE pamti trajno, a neophodno je pri restartu:**
**Preslikavanje blokova na DataNode čvorove** — tj. **na kom DataNode-u se koji blok fizički nalazi**. NameNode ove podatke drži **samo u glavnoj memoriji**, jer se u velikim sistemima često menjaju.

**Odakle se dobijaju:** iz **izveštaja o blokovima (block report)** koje **DataNode-ovi šalju NameNode-u** — svaki 10. heartbeat je izveštaj u kome DN prijavljuje koje blokove čuva. Nakon restarta NN rekonstruiše mapu blok→čvor iz tih izveštaja.

**Postupak restarta:** „Samo u slučaju kada se restartuje NN, promene iz **edit loga** se primenjuju na poslednju verziju **Namespace fajla (fsimage)** da bi se dobila nova verzija metapodataka fajl sistema."
Pošto se u klasterima retko vrši restart, edit log može mnogo narasti pa restart traje dugo → zato postoji **Secondary NameNode**.

---

### 50. Sledeće pitanje odnosi se na HDFS. Tačna tvrđenja obeležite sa T(tačno) a netačna sa F(netačno).
`25-NP · 14b` — **T/F**

| # | Tvrdnja | |
|---|---|---|
| **a** | Svaki fajl je podrazumevano podeljen na 32 MB po defaultu | ❌ **F** |
| **b** | NameNode čuva podatke fajla u obliku blokova podataka | ❌ **F** |
| **c** | HDFS se koristi za scenarije koji zahtevaju istovremeno upisivanje u istu datoteku | ❌ **F** |
| **d** | Faktor replikacije se može konfigurisati na nivou klastera (podrazumevano je podešeno na 3) i takođe na nivou fajla | ✅ **T** |

**Obrazloženja:**
- **a) F** — podrazumevana veličina bloka je **64MB** (u novijim verzijama **128MB**), nikako 32MB
- **b) F** — **NameNode NE čuva podatke fajla**, već samo **metapodatke**: „NN ne čuva podatke nad kojima se vrši obrada, on samo ima informaciju o blokovima koji čine fajl i gde su ti blokovi locirani u klasteru." Podatke u blokovima čuvaju **DataNode-ovi**.
- **c) F** — HDFS ima **jedan writer i više reader-a** (write-once, read-many); nije namenjen istovremenom upisivanju u istu datoteku
- **d) T** — isto kao br. 48·a, uz eksplicitno navedeno da je podrazumevano **3**

---
---

# 🟢 2/5

---

## TEMA 12 · Distribuirani fajl sistemi (DFS)

---

### 51. Kada smo govorili o DFS rekli smo da server može biti projektovan kao statefull ili stateless. Pored svakog od tvrđenja staviti oznaku tačno (T) ili netačno (F).
`2024 · 8a` — **T/F** 🔁

| # | Tvrdnja | |
|---|---|---|
| **1** | Implementacija klijentske strane može biti komplikovanija sa statefull serverom | ✅ **T** |
| **2** | Zaključavanje fajla je teško implementirati kod stateless servera | ✅ **T** |
| **3** | Kod statefull servera, svaki klijentski zahtev mora da sadrži kompletnu informaciju o zahtevu (npr. ime fajla, offset, itd.) | ❌ **F** |
| **4** | Lakše je izboriti se sa greškama kod stateless nego kod statefull servera | ✅ **T** |

**Definicije:**
- **Stateless server** — ne pamti ništa o tome koji klijent pristupa kom fajlu. **Sve informacije potrebne da bi se opslužio klijent, mora da pruži sam klijent** pri svakom pristupu.
- **Statefull server** — pamti koji je klijent otvorio koji fajl.

**Obrazloženja:**
- **1) T** — kod statefull servera klijent mora dodatno da vodi računa o oporavku od otkaza servera (ponovno uspostavljanje stanja, reotvaranje fajlova), što komplikuje klijentsku stranu
- **2) T** — „Kod statefull servera može se izvršiti **file locking** — da se spreči jednovremena modifikacija istog fajla od više klijenata." Kod stateless servera server ne zna ko je šta otvorio, pa zaključavanje nema na čemu da počiva.
- **3) F** — to važi za **STATELESS** server, ne statefull. „Mane stateless-a: poruke su mnogo duže — u svakoj poruci klijent mora da navede ime fajla i komandu koja treba da se izvrši."
- **4) T** — „Stateless je **otporniji na otkaz servera**, jer se nikakve informacije neće izgubiti otkazom servera." Kod statefull-a: „Teže se postiže otpornost na otkaze. Ako server otkaže, informacija o tome koji klijent je pristupio kom fajlu biće izgubljena."

**Uporedna tabela:**

| | **Stateless** | **Statefull** |
|---|---|---|
| Otpornost na otkaz servera | ✅ bolja | ❌ lošija |
| Dužina poruka | ❌ duže | ✅ kraće |
| Performanse | ❌ lošije | ✅ bolje |
| Konzistencija / file locking | ❌ nemoguće | ✅ lakše |
| Obaveštavanje klijenata o promeni fajla | ❌ ne može | ✅ može |

---

### 52. Koji modeli pristupa udaljenom fajlu postoje? Navesti prednosti i nedostatke svakog modela.
`25-NP · 13a`

Postoje **2 modela**:

**1. UPLOAD/DOWNLOAD MODEL**
Jedini servisi za pristup su **read** i **write**: **read** preuzima (download-uje) fajl sa servera, pamti ga na klijent mašini, sve operacije se obavljaju **lokalno**, a kada klijent završi, **vraća fajl na server (upload)**.
- ✅ **Prednosti:** jednostavan model; **dobre performanse**, jer se operacije obavljaju lokalno pa **nema mrežnog saobraćaja** tokom rada
- ❌ **Nedostaci:** šta ako klijent **nema dovoljno prostora** da zapamti ceo fajl? Šta ako mu **nije potreban ceo fajl** nego samo deo? Šta ako neki **drugi klijent istovremeno želi da modifikuje** isti fajl?

**2. MODEL UDALJENOG PRISTUPA**
**Sve operacije nad fajlom se obavljaju na udaljenoj mašini** (`open, close, read, write, read byte, write byte`); fajl se **ne pomera sa servera**. Sve se realizuje pomoću **RPC mehanizma**.
- ✅ **Prednosti:** **lakše je implementirati deljenje fajlova** — ako jedan klijent modifikuje fajl, ta promena je **svima odmah vidljiva**
- ❌ **Nedostaci:** **sve vreme se zahteva pristup serveru** ⇒ može nastati **zagušenje mreže** i **preopterećenje servera**; **performanse (brzina) su gore** od prethodnog modela

---

### 53. Koje semantike deljenja fajlova postoje? Objasniti njihove karakteristike.
`25-NP · 13b` ⚠️

⚠️ **Napomena:** u fajlovima ovog foldera **eksplicitan spisak semantika deljenja fajlova NIJE pronađen** — `usmeni.pdf` se na tom mestu prekida. **Ovaj deo dopuni sa predavanja/snimka.**

**Ono što JESTE u fajlovima i što treba iskoristiti za odgovor:**

Iz **DFS zahteva** — DFS mora obezbediti **transparentnost konkurencije**: „fajlu može pristupati više korisnika, koji mogu i da modifikuju fajl, pa se tada mora obezbediti da **svi procesi imaju jedinstven pogled na stanje tog fajla** (svi procesi vide izmene)."

Iz **modela pristupa**:
- **Model udaljenog pristupa** — „ako jedan klijent modifikuje fajl, ta promena je **svima odmah vidljiva**" ⇒ odgovara **UNIX semantici** (svaka operacija je odmah vidljiva svima)
- **Upload/Download model** — klijent radi nad **lokalnom kopijom** i vraća je tek na kraju ⇒ odgovara **sesijskoj semantici** (promene su vidljive tek po zatvaranju fajla)

Iz **stateless/statefull**:
- **Statefull** server „zna koji klijenti pristupaju kom fajlu i može da ih obavesti da je fajl promenjen" i podržava **file locking**
- **Stateless** server „nema načina da obavesti ostale klijente da je fajl modifikovan" ⇒ **klijent sam vodi računa o konzistenciji**

💡 **Za ispit:** gradi odgovor oko para **UNIX semantika ↔ model udaljenog pristupa** i **sesijska semantika ↔ upload/download model**, pa dodaj ulogu stateless/statefull servera.

---

## TEMA 13 · Chord / P2P

---

### 54. Chord prsten ima 2²⁰ mogućih identifikatora. Mreža ima samo 10.000 čvorova. Koliki je očekivani broj koraka za pronalaženje željenog sadržaja?
`26-JUN · 15b` — **ZAOKRUŽIVANJE**

- ❌ **a)** O(2²⁰)
- ❌ **b)** O(10000)
- ✅ **c)** O(log 10000) ← **TAČNO**
- ❌ **d)** O(20)

**Obrazloženje:**
- Bez **finger tabele**, sukcesivnim kontaktiranjem naslednika, broj koraka je **O(n)** gde je `n` broj **čvorova** → to bi bilo b)
- „Da bi se smanjio broj koraka, svaki peer pamti ne samo ko su mu prethodnici i sledbenici nego ima i **FINGER TABELU** u kojoj pamti m najbližih suseda. Tako se broj koraka smanji na **O(log n)**."
- Ovde je `n` = **broj aktivnih čvorova = 10.000**, a **ne** broj mogućih identifikatora ⇒ **O(log 10000)** ≈ 13 koraka

- **a ❌** — 2²⁰ je veličina **prostora identifikatora**, ne broj čvorova
- **d ❌** — O(20) = O(m) = broj vrsta finger tabele; to je **najgori slučaj / gornja granica**, a pitanje traži **očekivani** broj koraka

**Osnove Chord-a (za slučaj da traži i to):**
- Svim čvorovima i fajlovima dodeljuju se **m-bitni identifikatori** na prstenu veličine 2^m
- ID čvora = `hash(IP adresa, broj porta)` · ključ fajla = `hash(imeFajla ILI sadržajFajla)`
- **Fajl sa ključem k dodeljuje se prvom čvoru sa `ID ≥ k`** u smeru kazaljke na satu — to je `succ(k)`
- Dovoljno je da svaki peer zna **direktnog prethodnika i sledbenika** — sadržaj se uvek može naći, ali u O(n) koraka
- **Finger tabela** čvora `i` ima najviše **m vrsta**; vrsta `j` sadrži `succ(i + 2^(j−1))`, za `j = 1…m`

---

## TEMA 14 · Java RMI i udaljeni objekti

---

### 55. Objasniti ulogu RMI Registry-ja i proces registracije udaljenog objekta.
`26-JUL · 4a`

**Uloga:** **RMI registar je BINDER za Java RMI.** „Omogućava serveru da objavi uslugu i klijentu da dobije stub za pristupanje njoj." U binderu postoji **tabela sa preslikavanjem tekstualnog imena u referencu udaljenog objekta** `[imeObjekta, referencaObjekta]`.

Pristupa mu se putem metoda klase **`Naming`**, čiji metodi kao argument uzimaju **URL-formatiran string**: `//imeRačunara:port/nazivObjekta`

**Proces registracije i korišćenja (6 koraka):**
1. U okviru **serverske aplikacije** registruje se udaljeni objekat u RMI registru **pod određenim imenom** — metodama **`bind` / `rebind`** klase `Naming` (`rebind` zamenjuje već postojeće povezivanje). Vrednost vezana za ime je **referenca na udaljeni objekat**. Bind/rebind se vrši za **svaki serverski objekat**, pri čemu se svaki identifikuje svojim **logičkim imenom**.
2. Kada **klijentska aplikacija** želi da pristupi udaljenom objektu, **kontaktira RMI registar sa imenom objekta** (metoda **`lookup`**) i dobija **referencu udaljenog objekta**, koja se koristi prilikom instanciranja **stub-a** na klijentskoj strani.
3. Klijent poziv metoda udaljenog objekta **upućuje stub-u** — sintaksa za udaljeni poziv je **identična lokalnoj**.
4. **Stub serijalizuje** informacije potrebne za poziv (**ID metode i ulazne parametre**) i šalje ih **skeletonu** u poruci.
5. **Skeleton** prima poruku, **deserijalizuje** podatke, prosleđuje poziv objektu koji implementira metod; metod se izvršava i generiše rezultat; skeleton **serijalizuje povratnu vrednost** i šalje je stub-u.
6. **Stub** prima poruku, **deserijalizuje** povratnu vrednost i vraća rezultat klijentu.

*(Klasa udaljenog objekta na serveru implementira udaljeni interfejs i **nasleđuje `UnicastRemoteObject`**; `super()` poziva konstruktor koji obavlja inicijalizacije da bi server mogao da čeka i uslužuje zahteve.)*

---

### 56. Dat je URL //server.etf.rs:2500/Kalkulator koji se koristi za pronalaženje udaljenog objekta u Java RMI. Objasniti značenje svakog njegovog elementa.
`26-JUL · 4b`

Metodi klase **`Naming`** uzimaju kao argument **URL-formatiran niz u obliku `//imeRačunara:port/nazivObjekta`**. Za `//server.etf.rs:2500/Kalkulator`:

| Element | Vrednost | Značenje |
|---|---|---|
| **`//`** | — | oznaka početka URL-a za RMI registar |
| **`imeRačunara`** | `server.etf.rs` | **ime (host) računara na kome se izvršava RMI registar**, tj. mašine na kojoj je server registrovao svoj udaljeni objekat |
| **`port`** | `2500` | **broj porta na kome sluša RMI registar** na tom računaru *(podrazumevani port RMI registra je 1099; ovde je eksplicitno naveden 2500)* |
| **`nazivObjekta`** | `Kalkulator` | **logičko (tekstualno) ime pod kojim je udaljeni objekat registrovan** u RMI registru. To je ključ u tabeli `[imeObjekta, referencaObjekta]` — preko njega `lookup` vraća referencu udaljenog objekta |

**Kako se koristi:**
```java
// server:
Naming.rebind("//server.etf.rs:2500/Kalkulator", objekat);
// klijent:
IKalkulator k = (IKalkulator) Naming.lookup("//server.etf.rs:2500/Kalkulator");
```

---

### 57. Navesti i objasniti načine prosleđivanja objekata u Java RMI. Objasniti razlike koje postoje između navedenih načina prosleđivanja.
`26-JUL · 4c`

**Dva načina prosleđivanja parametara kod poziva udaljenog metoda:**

**1. PO VREDNOSTI (by value)** — primitivni tipovi podataka i **serijalizovani objekti** (klase koje implementiraju `java.io.Serializable`)
„Serijalizovani objekat: objekat čije se stanje može razlikovati na različitim lokacijama, prenosi se po vrednosti; objekat se **serijalizuje, šalje primaocu i deserijalizuje** kako bi se izgradila **lokalna kopija**. Kada se objekat prenosi prema vrednosti, u procesu primaocu se **kreira NOVI objekat**. Metode ovog novog objekta mogu se pozivati **lokalno**, što može rezultirati **različitim stanjem novog objekta u odnosu na stanje originalnog objekta** u procesu pošiljaocu."

**2. PO UDALJENOJ REFERENCI (by remote reference)** — **udaljeni objekti** (klase koje implementiraju `java.rmi.Remote`)
„Objekat koji je vezan za lokaciju u kojoj se izvršava (server) prenosi se putem udaljene reference: **njegov STUB se prosleđuje drugoj strani**."

**RAZLIKE:**

| | **Po vrednosti** | **Po udaljenoj referenci** |
|---|---|---|
| Šta se prenosi | **kopija objekta** (serijalizovana) | **stub** (predstavnik objekta) |
| Gde objekat živi | kreira se **NOVI objekat** kod primaoca | objekat ostaje **na originalnoj mašini** |
| Poziv metoda | izvršava se **lokalno** kod primaoca | izvršava se **udaljeno**, preko mreže |
| Stanje | dve **nezavisne** kopije — stanja se mogu **razići** | **jedno jedinstveno stanje** — svi vide iste promene |
| Uslov | klasa implementira `Serializable` | klasa implementira `Remote` |

💡 **Pravilo za udaljene metode:** svaka udaljena metoda **mora biti deklarisana da baca `RemoteException`** (radi rukovanja neuspelim komunikacijama), **vraća samo 1 rezultat** i ima 0, 1 ili više ulaznih parametara.

---

### 58. Šta je tačno u slučaju reference udaljenog objekta?
`26-JUN · 4a` — **ZAOKRUŽIVANJE** 🔁

- ⚠️ **A.** Referenca udaljenog objekta omogućava pristup metodama objekta kao da je lokalni
- ❌ **B.** Referenca udaljenog objekta sadrži direktnu memorijsku adresu udaljenog objekta
- ✅ **C.** Stub koristi referencu da usmeri poziv preko mreže ka pravom objektu ← **TAČNO**
- ❌ **D.** Udaljene reference ne mogu biti serijalizovane ni prosleđene drugim procesima

**Šta je referenca udaljenog objekta:** „Kada klijent pozove metod udaljenog objekta, poruka mora da specificira objekat čiji se metod poziva, i to radi pomoću **jedinstvenog ID-a udaljenog objekta, tj. preko reference udaljenog objekta**." Referenca **mora da garantuje svoju jedinstvenost u DS-u**, pa se gradi kao:

**`[IP adresa računara + broj porta procesa koji je kreirao objekat + vreme kreiranja + lokalni broj objekta]`**

(ili može sadržati informacije o interfejsu udaljenog objekta, kao što je njegovo ime).

**Obrazloženje:**
- **C ✅** — „Proxy pakuje parametre u poruku i prosleđuje poruku serveru (**poruka sadrži referencu udaljenog objekta, id metode i parametre poziva**)."
- **B ❌** — referenca **NE sadrži memorijsku adresu**, već mrežni identifikator. Memorijska adresa ne bi imala smisla u drugom adresnom prostoru.
- **D ❌** — **mogu** se prosleđivati: „Objekat koji je vezan za lokaciju u kojoj se izvršava prenosi se putem **udaljene reference: njegov stub se prosleđuje drugoj strani**." (vidi br. 57)
- ⚠️ **A** — tvrdnja jeste u duhu **pristupne transparentnosti** („Sintaksa za udaljeni poziv je identična lokalnoj"), ali to je zasluga **stub-a/proxy-ja**, a ne same reference. Referenca je samo **identifikator**. Zato je **C precizniji**; ako se traži više odgovora, A se može zaokružiti kao dodatni.

---
---

# ⚪ 1/5

---

### 59. Definisati konzistentni presek.
`26-JUL · 13a` 🔁

„Da bi oporavak od greške bio moguć u DS-u, svi procesi moraju da se vrate u stanje odakle je moguće izvršiti oporavak — mora se naći **linija oporavka (konzistentni presek)**. **Konzistentni presek je skup tačaka provere (checkpoint-a) u različitim procesima koje omogućavaju da se od njih sistem restartuje i krene dalje sa radom.**"

**Formalno:** skup checkpoint-a **C** je konzistentan ako za sve događaje `e` i `e′` važi:

**(e ∈ C) ∧ (e′ → e) ⟹ e′ ∈ C**

**U prevodu (PRAVILO ZA REŠAVANJE):**
- **Ako je zabeležen PRIJEM poruke, mora biti zabeleženo i SLANJE te poruke.** ❌ *Ne sme se desiti da checkpoint beleži prijem poruke, a da nije zabeleženo njeno slanje iz nekog drugog procesa.*
- **Obrnuto SME:** neki proces može zabeležiti **slanje** poruke, a drugi proces u svom checkpoint-u **nije zabeležio prijem** — to je poruka „u letu" (in-flight), što je dozvoljeno.

**DOMINO EFEKAT:** „Ako procesi nezavisno jedan od drugog donose odluku gde će kreirati checkpointe, može biti vrlo teško naći liniju oporavka i često se javlja domino efekat. Tada sistem mora da se vrati u početno stanje."

**REŠENJE — KOORDINISANO beleženje stanja (dvofazni blokirajući protokol):**
1. Koordinator periodično šalje svim procesima **`checkpoint_REQUEST`**
2. Proces prestaje sa aktivnostima, kreira checkpoint, beleži svoje stanje
3. Proces javlja koordinatoru **`checkpoint_ACK`**
4. Kada koordinator primi sve potvrde, šalje svima **`checkpoint_DONE`** → procesi se deblokiraju

➜ Zapamćeno stanje u svim procesima je **konzistentno** i domino efekat je izbegnut. Jedina mana je postojanje koordinatora.

**Dve strategije oporavka od greške:**
- **Backward Recovery** — povratak u prethodno korektno stanje pomoću **tačaka provere**; skupo sa stanovišta performansi, ali **najčešće korišćeno**
- **Forward Recovery** — prelazak u **novo** korektno stanje, kao da nije nastala greška; zahteva da se **identifikuju sve moguće greške** unapred; **mnogo ređe se koristi**

---

### 60. Navesti kriterijume za podelu komunikacija u distribuiranim sistemima. Koji sve tipovi komunikacija u distribuiranom sistemu su podržani od strane MPI i kojim funkcijama? Obrazložiti izvršenje svake funkcije.
`2024 · 4a`

**KRITERIJUMI ZA PODELU KOMUNIKACIJA (3):**

| Kriterijum | Vrste |
|---|---|
| **Postojanost (persistency)** | **Perzistentne** — poruka se pamti u komunikacionom serveru koliko je potrebno da bi se isporučila odredištu ⟷ **Tranzijentne** — poruka se odbacuje ako komunikacioni server nije u stanju da je isporuči |
| **Sinhronizacija** | **Sinhrone** — pošiljalac se blokira dok se poruka ne zapamti u lokalnom baferu odredišnog hosta ⟷ **Asinhrone** — pošiljalac nastavlja sa radom odmah nakon što prosledi poruku |
| **Vremenska zavisnost** | **Diskretna** ⟷ **strimovana** komunikacija |

**MPI podržava skoro sve oblike TRANZIJENTNIH komunikacija.** *(MPI je middleware za paralelne sisteme; komunikacija se obavlja u okviru grupe procesa — par `(id_grupe, id_procesa)` jedinstveno identifikuje izvor/odredište umesto transportne adrese.)*

**MPI KOMUNIKACIONE PRIMITIVE:**

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

### 61. Sveprisutni distribuirani sistemi.
`25-SP · 9`

**Sveprisutni (ugrađeni / embedded) DS** je **treći tip DS** u podeli po oblasti primene (uz distribuirane računarske sisteme i distribuirane informacione sisteme).

**Ključna razlika:** prva dva tipa DS karakteriše relativno **visoka stabilnost** — čvorovi su fiksni i imaju stalnu, kvalitetnu mrežnu konekciju. Sa pojavom mobilnih i ugrađenih računarskih sistema, **nestabilnost postaje uobičajeno ponašanje**.

**Osobine uređaja:**
- uglavnom **mali**
- **napajaju se pomoću baterija**
- **mobilni** su
- imaju **samo bežične veze**

**Najvažnija osobina: ODSUSTVO ADMINISTRATIVNOG UPRAVLJANJA.** Posledice:
- Uređaji **sami moraju da otkriju okruženje i da se ugnezde u njega**
- Uređaji **moraju biti svesni da se okruženje stalno može menjati**

**Primeri:**
- **Kućni sistemi** — organizovani oko jednog centralnog računara (automatsko paljenje svetla, sistemi za navodnjavanje, alarmni sistemi, kućni aparati)
- **Elektronski sistemi za monitoring pacijenata**
- **Senzorske mreže** — sistemi za akviziciju podataka i nadzor (merenje temperature, vlažnosti… i slanje informacija do bazne stanice gde se obavlja procesiranje)

**Puna podela DS-a (za kontekst):**
- **Na arhitekturnom nivou:** klijent-server · peer-to-peer
- **U odnosu na oblast primene:** distribuirani računarski sistemi (**klasteri** — homogeni, LAN, MPI middleware; **grid** — heterogeni, veliko geografsko područje) · distribuirani informacioni sistemi (obrada transakcija, integracija poslovnih aplikacija) · **ugrađeni (sveprisutni)** sistemi

---

### 62. Koje od navedenih tvrdnji tačno opisuju karakteristike i način rada distribuiranih transakcija? Obrazložiti.
`26-JUL · 1c` — **ZAOKRUŽIVANJE (više odgovora)**

- ✅ **A.** Distribuirana transakcija obuhvata operacije koje se izvršavaju na više distribuiranih resursa. ← **TAČNO**
- ✅ **B.** Cilj distribuiranih transakcija je da se obezbedi konzistentno stanje sistema. ← **TAČNO**
- ❌ **C.** Distribuirane transakcije značajno poboljšavaju performanse i smanjuju vreme odziva sistema u poređenju sa lokalnim transakcijama.
- ❌ **D.** Distribuirane transakcije ne zahtevaju koordinaciju između učesnika.

**Obrazloženje:**
- **A ✅** — „U DS transakcija je obično sastavljena od **više podtransakcija koje su raspoređene na više servera**."
- **B ✅** — „Sistem za obradu transakcija obezbeđuje da sve ili nijedna operacija u transakciji budu izvršene bez greške. Nakon obavljene transakcije, **sistem mora da bude u poznatom konzistentnom stanju**."
- **C ❌** — u materijalima **nema** takve tvrdnje. Naprotiv, distribuirana transakcija zahteva **koordinaciju kroz mrežu i protokol potvrde**, što **dodaje** režijske troškove u odnosu na lokalnu transakciju.
- **D ❌** — direktna suprotnost: „**Monitor za obradu transakcija (TP monitor)** je middleware koji omogućava aplikaciji da obavi pristup više servera/baza podataka nudeći joj transakcioni programski model. **TP monitor KOORDINIRA potvrdu podtransakcija prema standardnom protokolu.**"

**ACID osobine (obavezno znati uz ovo pitanje):**

| | Osobina | Značenje |
|---|---|---|
| **A** | **Atomicity** (atomičnost) | transakcija se obavi **kompletno ili se uopšte ne obavi** |
| **C** | **Consistency** (konzistentnost) | transakcija **ne ugrožava skup invarijanti** (ograničenja) sistema — npr. transakcija sa negativnom vrednošću biće odbijena |
| **I** | **Isolation** (izolacija) | konkurentne transakcije moraju biti **serijalizovane** — izvršavaju se u nekom redosledu, ali taj redosled mora biti **vidljiv na isti način za sve** |
| **D** | **Durability** (trajnost) | kada se transakcija obavi, **ne može se poništiti** i ostaje trajna čak i u slučaju otkaza sistema |

**Transakcija** = skup operacija koje se obavljaju kao **jedna nedeljiva (atomična) operacija**. Primitive: `Begin_transaction`, `End_transaction`, `Abort_transaction`, `Read`, `Write`.

---
---

# 📊 REZIME

**Ukupno: 62 teorijska pitanja**

| Učestalost | Teme | Pitanja | Brojevi |
|---|---|:---:|---|
| 🔴 **5/5** | Konzistencija · RPC · Razmena poruka · Otpornost na greške | **25** | 1–25 |
| 🟠 **4/5** | Osnovne osobine DS · Časovnici · Grupna komunikacija | **14** | 26–39 |
| 🟡 **3/5** | Replikacija · Uzajamno isključivanje · Sinhronizacija · HDFS | **11** | 40–50 |
| 🟢 **2/5** | DFS · Chord · Java RMI | **8** | 51–58 |
| ⚪ **1/5** | Konzistentni presek · MPI · Sveprisutni DS · Transakcije | **4** | 59–62 |

**Po temama:**

| Tema | Pitanja | Brojevi |
|---|:---:|---|
| 1 · Konzistencija | 1 | 1 |
| 2 · RPC | 13 | 2–14 |
| 3 · Razmena poruka (MQ, pub-sub, JMS) | 7 | 15–21 |
| 4 · Otpornost na greške | 4 | 22–25 |
| 5 · Osnovne osobine DS | 8 | 26–33 |
| 6 · Logički i vektorski časovnici | 2 | 34–35 |
| 7 · Grupna komunikacija | 4 | 36–39 |
| 8 · Replikacija | 2 | 40–41 |
| 9 · Uzajamno isključivanje | 1 | 42 |
| 10 · Sinhronizacija fizičkih časovnika | 4 | 43–46 |
| 11 · Hadoop / HDFS | 4 | 47–50 |
| 12 · DFS | 3 | 51–53 |
| 13 · Chord / P2P | 1 | 54 |
| 14 · Java RMI | 4 | 55–58 |
| 15–19 · Ostalo (1/5) | 4 | 59–62 |

---

## Sva pitanja na zaokruživanje — brza provera

| Br. | Pitanje | ✅ Tačno |
|---|---|---|
| **9** | Server promeni adresu (dinamičko povezivanje) | **b + d** ⚠️ |
| **14** | DCE RPC tvrdnje | **A, B, E** |
| **17** | Šta ne mora da važi u MQ | **c** *(i d ⚠️)* |
| **18** | Karakteristike Message Queues | **A, B, D** |
| **20** | Deo JMS poruke za selektor | **C** (zaglavlje + svojstva, **ne** telo) |
| **24** | Otpornost na Vizantijske greške | **d** (maliciozni čvorovi) |
| **27** | Transparentnost konkurencije | **b** |
| **28** | Osobine transparentnosti konkurencije | **a, b, d** |
| **29** | Transparentnost replikacije | **b** |
| **30** | Pristupna transparentnost | **B** |
| **34** | Vektorski časovnici — najviše što doznajemo | **b** (ako i samo ako) |
| **36** | Potpuno uređena grupna komunikacija | **d** |
| **44** | Kristijanov algoritam usvaja | **b** (kašnjenja približno jednaka) |
| **54** | Chord — očekivani broj koraka | **c** O(log 10000) |
| **58** | Referenca udaljenog objekta | **C** *(A sporno ⚠️)* |
| **62** | Distribuirane transakcije | **A, B** |

**T/F pitanja:**

| Br. | Pitanje | Odgovori |
|---|---|---|
| **48** | HDFS (faktor repl. · block report · lokalni FS · DN zna fajlove) | **T, T, T, F** |
| **50** | HDFS (32MB · NN čuva podatke · istovremeni upis · faktor repl.) | **F, F, F, T** |
| **51** | DFS stateless/statefull | **T, T, F, T** |

---

*Izvori: `usmeni.pdf` · `DS BELO ZLATO skripta.pdf` · `PREZENTACIJE/` · `GFS i HDFS 2024.pdf` · `blanketi/` (5 rokova)*
*Mesta gde materijal nije potpun označena su sa ⚠️.*
