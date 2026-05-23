---
title: MATURITA FINAL — SWI + DAT shrnutí
description: Max 5 karet per téma. SWI = teorie. DAT = okecávání kódu (NE programovat). Pokrytí všech podtémat z maturitních okruhů.
tags: [maturita, swi, dat, final]
---

# 🎓 SWI — Softwarové inženýrství (25 témat)

---

## SWI 1 — Diagramy UML

**Q:** Co je UML a kdo ho vyvinul?
**A:** **Unified Modeling Language** — standardizovaný vizuální jazyk pro modelování softwaru. Vyvinula konsorcium **OMG** (Object Management Group). 14 typů diagramů, rozdělených na **strukturální** (class, object, package...) a **behaviorální** (use case, sequence, activity, state...).

**Q:** Co je Use Case diagram a kdo jsou aktéři?
**A:** Diagram **případů užití** (případy užití = use case, synonyma!). Ukazuje **co systém umí z pohledu uživatele**. **Aktéři** = role mimo systém (uživatel, admin, externí systém). Use cases v ovále, aktéři jako figurky, vztahy přes `<<include>>` (vždy proběhne) a `<<extend>>` (volitelně).

**Q:** Class diagram — co jsou 4 visibility modifiers?
**A:** `+` public (přístupné odkudkoli), `-` private (jen ze třídy), `#` protected (třída + dědicové), `~` package-private (Java). U asociace mezi třídami: **kardinalita** (`1`, `*`, `0..1`, `1..5`).

**Q:** Sequence diagram × Activity diagram?
**A:** **Sequence** = časová posloupnost zpráv mezi objekty (lifeline + activation bar + sync/async šipky + combined fragments alt/opt/loop). **Activity** = workflow procesu (swimlanes pro role, fork/join `▬` pro paralelní větve, rhombus pro rozhodnutí).

**Q:** Rozdíl sync × async zprávy v sequence diagramu?
**A:** **Plná šipka** = synchronní (čeká na response). **Otevřená šipka** = asynchronní (pošle a pokračuje).

---

## SWI 2 — Algoritmus

**Q:** Definice algoritmu + 7 vlastností?
**A:** **Konečná posloupnost kroků vedoucí k řešení**. Vlastnosti: **konečnost**, hromadnost, determinovanost, správnost, vstup, výstup, výslednost. Drill: konečnost ⚠️ často chybí ve výčtu.

**Q:** Algoritmus × kód?
**A:** **Algoritmus** je **myšlenka** (lze zapsat v češtině, pseudokódem, vývojovým diagramem). **Kód** je **konkrétní implementace** v programovacím jazyce.

**Q:** Asymptotická složitost — 5 hlavních tříd?
**A:** **O(1)** konstantní, **O(log n)** logaritmická (binary search), **O(n)** lineární, **O(n log n)** linearitmická (merge sort), **O(n²)** kvadratická (bubble sort, vnořené cykly). Big O = horní mez růstu času s velikostí vstupu.

**Q:** Symboly vývojového diagramu?
**A:** **Ovál** = start/konec, **kosočtverec** = rozhodnutí, **obdélník** = akce, **kosodelník** = vstup/výstup, **šipky** = tok.

**Q:** Bubble sort — princip + složitost?
**A:** Vícekrát projdi pole, opakovaně porovnávej **sousední** dvojice, prohoď když jsou ve špatném pořadí. **O(n²)** = vnořené cykly. Jednoduché, ale pomalé pro velká data.

---

## SWI 3 — Reprezentace dat

**Q:** Číselné soustavy 4 nejběžnější?
**A:** **2 (binární)**, **8 (osmičkový)**, **10 (desítkový)**, **16 (hexadecimální)**. Konverze: binární↔dec přes součet mocnin (`101100₂ = 32+8+4 = 44`). Hex↔dec přes binární mezikrok (`0x1A = 00011010 = 26`).

**Q:** Co je float precision past?
**A:** Binární floating-point **IEEE 754** nemůže přesně reprezentovat některá desetinná čísla (jako 1/3 v desítkové). Proto `0.1 + 0.2 != 0.3` (vrátí 0.30000000000000004). **Pro peníze používej `decimal`**, ne `float/double`. NIKDY `==` na floaty — `Math.Abs(a-b) < epsilon`.

**Q:** Základní × složené datové typy?
**A:** **Základní** (primitivní): `int`, `double`, `bool`, `char`. Hodnotové, leží na stacku. **Složené**: `string`, pole, objekt, struct. Většinou referenční (heap).

**Q:** Imutabilita stringu?
**A:** `string` je **referenční ALE imutabilní**. Změna stringu vytvoří **nový objekt v paměti**, neměnu existující. Proto `string s = "a"; s = s + "b";` vytvoří 2 objekty (původní zůstane v GC dokud se neuvolní).

**Q:** Unix timestamp + 2038 problém?
**A:** Počet **sekund od 1.1.1970 UTC**. Uložen jako 32-bit signed integer → přeteče v **roce 2038** (Y2K38 problém). Moderní systémy používají 64-bit (vyřešeno).

**Q:** Jednotky velikosti dat?
**A:** **bit** (b, 0/1) = nejmenší. **Byte** (B) = 8 bitů. **KB/MB/GB/TB** = 1000× (decimal) nebo 1024× (binary, **KiB/MiB/GiB**). Drill: Windows ukazuje 1024 ale jmenuje "GB" (matoucí), ostatní systémy 1000. **Pro síť**: rychlost v **bitech** (Mbps = megabit), úložiště v **bytech**.

**Q:** Znaky a kódování (ASCII, UTF-8)?
**A:** **ASCII** (1963, 7 bitů, 128 znaků) = anglická abeceda + čísla + symboly. **Latin-1** (8 bitů, 256) = západní Evropa. **Unicode** (univerzální tabulka 150k+ znaků). **UTF-8** = nejpoužívanější Unicode kódování, variabilní 1-4 byty, **kompatibilní s ASCII** (ASCII znaky = 1 byte). Mojibake = špatné kódování při čtení.

---

## SWI 4 — Datové typy, proměnné

**Q:** Statická × dynamická typovost?
**A:** **Statická** = typ se určuje při **kompilaci**, kontrola dopředu (C#, Java, TypeScript). **Dynamická** = typ se určuje **za běhu**, flexibilnější, ale chyby až za běhu (JS, Python).

**Q:** Hodnotové × referenční typy?
**A:** **Hodnotové** = uloží přímou hodnotu v paměti (stack), kopírují se při přiřazení (`int`, `bool`, `struct`). **Referenční** = uloží **adresu** v paměti (heap), při přiřazení se kopíruje **odkaz** (string, pole, objekt).

**Q:** Proč 2 instance se sdíleným seznamem ukazují stejná data?
**A:** Pole/seznam = **referenční typ**. `a = b` zkopíruje **odkaz**, ne data. Obě proměnné ukazují na **stejnou paměť**. Změna přes `a` viditelná i přes `b`. Pro nezávislou kopii potřebuješ **clone/spread**.

**Q:** Obor platnosti (scope)?
**A:** **Třídní** (member fields — celá třída), **metodový/funkční** (lokální proměnné — jen v té metodě), **blokový** (uvnitř `{}` ifu/cyklu). Vnořené scopes vidí ven, ne dovnitř.

**Q:** 3 druhy přetypování?
**A:** **Implicitní** (automatic, bezpečné: `int → double`), **explicitní** (cast, riziko ztráty: `(int)3.7 = 3`), **konverze** (parse: `int.Parse("42")`, může hodit exception).

---

## SWI 5 — Návrhové vzory

**Q:** 3 kategorie návrhových vzorů + příklady?
**A:** **Vytvářecí** (Factory Method, Singleton, Builder). **Strukturální** (Adapter, Facade, Decorator, Proxy, Composite). **Behaviorální** (Observer, Command, Strategy, State, Template Method). Vzory popsala **Gang of Four** (1994).

**Q:** Singleton — princip + thread-safe varianty?
**A:** **Garantuje JEDNU instanci** napříč aplikací. Privátní konstruktor + statická `GetInstance()`. Thread-safe: **lock**, **double-checked locking**, **eager init**, **`Lazy<T>`**, **enum** (Java). ⚠️ Často kritizován jako anti-pattern (globální stav, špatně testovatelný).

**Q:** Factory Method × Observer × Command?
**A:** **Factory Method** (vytvářecí) — metoda vytváří objekty různých tříd podle parametru. Cross-platform UI (Windows/Mac button). **Observer** (behaviorální) — 1:N notifikace o změně (eventy v UI). **Command** (behaviorální!) — zabaluje akci do objektu (undo/redo, makra).

**Q:** Co NENÍ návrhový vzor?
**A:** **MVC** (Model-View-Controller) NENÍ návrhový vzor, je to **architektonický vzor** / **typ architektury**. Podobně **MVVM, Layered, Microservices**. Drill chyták!

**Q:** Use cases include × extend?
**A:** **`<<include>>`** = vždy proběhne jako součást use case (např. *přihlášení* je include v *objednávce*). **`<<extend>>`** = volitelné rozšíření (např. *zapomenuté heslo* extend přihlášení).

---

## SWI 6 — Chyby, testování, ladění

**Q:** 4 druhy chyb?
**A:** **Syntaktické** (kompilátor nepřejde — chybí středník). **Sémantické** (kompiluje, ale dělá špatně — `>` místo `<`). **Runtime** (běhové — null reference, dělení nulou). **Logické** (algoritmus špatně — průměr / 0).

**Q:** Výjimky × chybové kódy?
**A:** **Výjimka** = vyhodí kontext, propaguje **vzhůru** stackem, dokud ji někdo `catch`. Pokud nikdo, **crash + stack trace**. **Chybový kód** = return value, **proběhne tiše**, programátor musí kontrolovat. Výjimky modernější + bezpečnější.

**Q:** Pyramida testů?
**A:** Snizí nahoru: **unit testy** (mnoho, izolované, rychlé), **integrační** (středně, propojení komponent), **E2E** (málo, celá app, pomalé). Důvody: rychlost, **lokalizace chyby**, **údržba**.

**Q:** 3 typy stepping v debugger?
**A:** **Step Into (F11)** = vstoupí do volané funkce. **Step Out (Shift+F11)** = vyskočí z aktuální funkce. **Step Over (F10)** = přeskočí volání funkce (provede ji bez vstupu).

**Q:** Co je mock + 3 důvody?
**A:** **Náhrada za reálné závislosti** (DB, API) v testech. Důvody: **rychlost** (žádné HTTP), **izolace** (testuju jen tu jednotku), **determinismus** (vždy stejná odpověď).

---

## SWI 7 — Šifrování a kódování

**Q:** Kódování × šifrování?
**A:** **Kódování** = převod do jiného formátu, **NEUTAJUJE** (Base64, ASCII). **Šifrování** = převod s cílem **utajení**, vyžaduje klíč. Drill: Base64 ≠ šifrování!

**Q:** Symetrické × asymetrické šifrování + DH?
**A:** **Symetrické** (AES, DES) = **stejný klíč** pro šifrování i dešifrování. Rychlé, ale problém s distribucí klíče. **Asymetrické** (RSA, ECC) = **veřejný + soukromý** klíč. Pomalejší. **Diffie-Hellman** = **výměna klíčů**, NE šifrování ani asymetrické.

**Q:** Hash — vlastnosti?
**A:** **Deterministický** (stejný input → stejný hash), **lavinový efekt** (drobná změna → úplně jiný hash), **NELZE zpět** (jednosměrný). SHA-256, Argon2 (pro hesla). Hesla NIKDY plaintextem, vždy hash + **salt** (random string) proti rainbow tables.

**Q:** Transpozice × substituce?
**A:** **TransPOZICE** = mění POŘADÍ písmen (Anagram, scytale). **SUBStituce** = substituuje PÍSMENA jinými (Caesar, Vigenère). Drill chyták!

**Q:** Digitální podpis — 3 vlastnosti?
**A:** **Autenticita** (kdo to podepsal), **integrita** (nebylo to změněno), **nepopiratelnost** (nemůže popřít). Mechanismus: hash zprávy + zašifrovat hash **soukromým** klíčem. Příjemce dešifruje **veřejným**, porovná hash.

---

## SWI 8 — Kryptosystémy

**Q:** 4 vlastnosti kryptosystému?
**A:** **Důvěrnost** (šifrování), **autenticita** (podpis + certifikát), **integrita** (hash), **nepopiratelnost** (podpis). Každá řeší jiný aspekt bezpečnosti.

**Q:** Kerckhoffsův princip?
**A:** **Bezpečnost systému závisí JEN na utajení klíče**, ne na utajení algoritmu. Algoritmus může být veřejný (a často je — AES, RSA). Důvod: tajné algoritmy se nedají efektivně peer-review-ovat.

**Q:** ECB × CBC × GCM?
**A:** Módy šifrování. **ECB** (Electronic Codebook) = bloky šifrovány samostatně → **stejný plaintext = stejný ciphertext** (klasický "tučňák" problém). **CBC** = každý blok XORován s předchozím. **GCM** = counter + autentizace, moderní default.

**Q:** Chain of trust v HTTPS?
**A:** **Root CA** (preinstalovaná v OS/browser) → **Intermediate CA** (Let's Encrypt) → **End certifikát** (tvoje doména). Klient kontroluje řetězec podpisů až k Root CA. Pokud chybí důvěra → "neznámé spojení".

**Q:** PFS (Perfect Forward Secrecy)?
**A:** **Efemérní DH** klíč per spojení. I když dlouhodobý privát klíč později leakne, **historická spojení nelze dešifrovat**. TLS 1.3 PFS **povinné**.

---

## SWI 9 — OOP

**Q:** 4 pilíře OOP?
**A:** **Enkapsulace** (skrytí dat za interface, private + properties), **dědičnost** (Class B extends A, sdílí + rozšiřuje), **polymorfismus** (1 interface, různé implementace), **abstrakce** (popis CO objekt umí, ne JAK). Někdy mluvíme o **3** (bez abstrakce, kterou někdo nepočítá zvlášť).

**Q:** Abstraktní třída × interface?
**A:** **Abstract class** = **CO JE** (zvíře). Může mít konkrétní metody + abstraktní (k implementaci). Třída **dědí JEN 1** abstract class. **Interface** = **CO DĚLÁ** (může-letat, IComparable). Jen signatury (od C# 8+ default metody). Třída implementuje **N interfaces**.

**Q:** Polymorfismus — overload × override?
**A:** **Overload** (přetížení) = stejné jméno, **různé parametry** → compile-time resolution. **Override** (přepsání) = stejná signatura v potomkovi → runtime resolution (virtual table). Drill chyták!

**Q:** Field × Property?
**A:** **Field** = raw datový sloty (`private int _vek;`). **Property** = getter/setter, může mít **validační logiku** (`public int Vek { get => _vek; set => _vek = value < 0 ? 0 : value; }`). Konvence: field private + property public.

**Q:** Generika — k čemu?
**A:** **Type-safe znovupoužitelnost.** `List<T>`, `Dictionary<K, V>`. Bez generik bys měl `List<object>` + boxing/unboxing + runtime cast (`as int`). S generikem `List<int>` — compile-time check + výkon.

---

## SWI 10 — Databáze

**Q:** SQL × NoSQL?
**A:** **SQL** (relační) = **tabulky + řádky + vztahy**, ACID, fixní schéma (SQLite, PostgreSQL, MSSQL). **NoSQL** = **flexibilní schéma**, různé typy: dokumentové (MongoDB), klíč-hodnota (Redis), grafové (Neo4j), wide-column (Cassandra). Volba podle dat — strukturovaná × dokument × graf.

**Q:** ACID — 4 vlastnosti?
**A:** **Atomicita** (vše nebo nic), **Konzistence** (data validní před i po), **Izolace** (paralelní transakce se neruší), **Trvanlivost** (po commit se data nezahodí). Klasické SQL DB.

**Q:** Klíče — PK, FK, kandidátní, superklíč?
**A:** **Primární klíč (PK)** = jednoznačně identifikuje záznam, NOT NULL, jediný per tabulka. **Cizí klíč (FK)** = odkaz na PK jiné tabulky (referenční integrita). **Superklíč** = jakákoli množina sloupců co identifikuje (může být přebytečná). **Kandidátní klíč** = MINIMÁLNÍ superklíč. PK je vybraný kandidátní.

**Q:** Indexy — co a proč?
**A:** **Datová struktura** pro rychlé hledání (typicky B-tree, NE binary tree!). Bez indexu = full scan **O(n)**. S indexem = **O(log n)**. **Trade-off**: rychlejší SELECT, **pomalejší** INSERT/UPDATE/DELETE (musí se aktualizovat index). PK má automatický index.

**Q:** 4 ON DELETE akce?
**A:** **CASCADE** (smaž potomky), **RESTRICT** (zakaž smazat rodiče pokud má potomky), **SET NULL** (potomek dostane NULL), **SET DEFAULT** (potomek dostane default hodnotu). Volba podle byznys pravidla.

---

## SWI 11 — Normalizace

**Q:** 3 anomálie nenormalizované DB?
**A:** **Insert anomaly** (nemůžu vložit záznam protože chybí jiné údaje). **Update anomaly** (musím updatnout na N místech, jinak inkonzistence). **Delete anomaly** (smazáním ztratím i jiné údaje). Normalizace tomu předchází.

**Q:** 1NF × 2NF × 3NF × BCNF?
**A:** **1NF** = atomické hodnoty (žádné pole/seznam v buňce). **2NF** = 1NF + žádný neklíčový sloupec nezávisí na **části** složeného PK (jen na celém). **3NF** = 2NF + žádné **tranzitivní** závislosti (sloupec → sloupec → klíč). **BCNF** = 3NF + každý determinant je **superklíč**.

**Q:** OLTP × OLAP?
**A:** **OLTP** (Online Transaction Processing) = transakční, **normalizované**, optimalizováno pro **rychlé INSERT/UPDATE** (e-shop, banking). **OLAP** (Online Analytical Processing) = analytické, **denormalizované** (star schema), optimalizováno pro **velké SELECT/agregace** (reporty, dashboardy).

**Q:** Funkční závislost?
**A:** **X → Y** = z X jednoznačně určím Y. Příklad: `student_id → jmeno` (student má jedno jméno). `(order_id, product_id) → quantity` (kombinace určuje quantity). Důležité pro identifikaci klíčů.

**Q:** Tranzitivní závislost (3NF chyták)?
**A:** A → B → C, kde B není klíč. Pak C **tranzitivně** závisí na A skrz B → **porušení 3NF**. Příklad: `employee_id → dept_id → dept_name`. Řešení: vytáhnout dept do samostatné tabulky.

---

## SWI 12 — Jazyk SQL

**Q:** 4 kategorie SQL příkazů?
**A:** **DDL** (Data Definition Language) = struktura (CREATE, ALTER, DROP, **TRUNCATE**). **DML** (Data Manipulation) = data (INSERT, UPDATE, DELETE, SELECT někdy). **DCL** (Data Control) = oprávnění (GRANT, REVOKE). **TCL** (Transaction Control) = transakce (BEGIN, COMMIT, ROLLBACK, SAVEPOINT).

**Q:** TRUNCATE × DELETE bez WHERE?
**A:** **TRUNCATE = DDL** (struktura, pracuje se storage), **NEJRYCHLEJŠÍ** pro vyprázdnění (dealokuje stránky bez per-row logu). **DELETE bez WHERE = DML**, funkčně stejné, ale **mnohem pomalejší** (loguje per řádek). Drill chyták: TRUNCATE patří k DDL!

**Q:** Pořadí klauzulí v SELECT?
**A:** `SELECT ... FROM ... [JOIN ON] ... WHERE ... GROUP BY ... HAVING ... ORDER BY ... LIMIT`. **WHERE** filtruje **řádky** (před GROUP BY), **HAVING** filtruje **skupiny** (po GROUP BY). Chyták: "víc než X" → HAVING (po agregaci), ne ORDER.

**Q:** 6 constraints?
**A:** **PRIMARY KEY** (PK, jedinečné + NOT NULL), **FOREIGN KEY** (FK, odkaz na PK jiné tabulky), **UNIQUE** (jedinečné, ale může NULL), **NOT NULL** (povinné), **CHECK** (custom validační podmínka), **DEFAULT** (výchozí hodnota).

**Q:** Transakce — flow + SAVEPOINT?
**A:** `BEGIN TRANSACTION; ... INSERT/UPDATE ... ; COMMIT;` nebo `ROLLBACK;`. **COMMIT** uloží natrvalo. **ROLLBACK** vrátí zpět. **SAVEPOINT name** uvnitř transakce vytvoří checkpoint, na který lze rollbacknout přes `ROLLBACK TO name` (částečný rollback).

**Q:** 6 nejčastějších SQL příkazů — co dělá který?
**A:** **`CREATE TABLE`** (DDL) = vytvoří novou tabulku. **`ALTER TABLE`** (DDL) = změní strukturu (přidá/smaže sloupec). **`DROP TABLE`** (DDL) = smaže tabulku včetně dat. **`INSERT INTO`** (DML) = přidá nový řádek. **`UPDATE`** (DML) = změní existující řádky (s WHERE!). **`DELETE FROM`** (DML) = smaže řádky (s WHERE, bez WHERE smaže VŠE).

**Q:** Konkrétní syntax INSERT/UPDATE/DELETE?
**A:**
```sql
INSERT INTO produkty (nazev, cena) VALUES ('Klávesnice', 900);
UPDATE produkty SET cena = 850 WHERE id = 1;
DELETE FROM produkty WHERE cena < 100;
```
Drill: **UPDATE/DELETE BEZ `WHERE` ovlivní VŠECHNY řádky** (klasická katastrofa).

---

## SWI 13 — Internet

**Q:** URL — anatomie?
**A:** `protocol://subdomain.domain.tld:port/path?query#fragment`. Příklad: `https://www.example.com:443/blog/article?id=5#section`. Pořadí: **protokol → doména → port → cesta → query → fragment**.

**Q:** DNS — co a jak?
**A:** **Domain Name System** = překlad **doménového jména → IP adresa**. Hierarchie: **resolver** (ISP) → **root DNS** → **TLD DNS** (.com, .cz) → **authoritative DNS** (vlastník domény). Reverse DNS = IP → doména (výjimka).

**Q:** MIME type?
**A:** **Multipurpose Internet Mail Extensions** = identifikace typu obsahu. Formát: **`typ/podtyp`** (např. `text/html`, `image/png`, `application/json`). Deklaruje **server v HTTP `Content-Type` hlavičce**, NE jako přípona souboru (drill chyták).

**Q:** Absolutní × relativní URL?
**A:** **Absolutní** = celá URL včetně protokolu a domény (`https://www.example.com/blog`). **Relativní** = jen cesta vůči aktuálnímu kontextu (`/blog`, `../images/foto.png`). Relativní funguje jen v rámci stejné domény.

**Q:** Typy domén?
**A:** **TLD** (Top Level) = `.com`, `.cz`, `.org`. **SLD** (Second Level) = `example` v `example.com`. **Subdoména** = `www`, `mail`, `blog`. Registruje se SLD pod TLD, subdomény si nastaví majitel.

---

## SWI 14 — Návrh webu

**Q:** Workflow tvorby webu (7 fází)?
**A:** **1)** Analýza/cíl → **2)** Persona/UX research → **3)** Wireframe → **4)** Design (UI) → **5)** Implementace → **6)** Testování → **7)** Deploy + bezpečnost + monitoring. Iterativní, ne lineární.

**Q:** UX × UI?
**A:** **UX** (User Experience) = **jak se to používá**, flow, struktura, hierarchie informací. **UI** (User Interface) = **jak to vypadá**, vizuální design, barvy, typografie. UX je proces, UI je výstup.

**Q:** SEO — 5 pilířů?
**A:** **Obsah** (kvalitní, relevantní), **klíčová slova** (research, density), **technické SEO** (rychlost, mobile-first, schema.org), **off-page** (backlinky, autorita), **UX** (Core Web Vitals — LCP, FID, CLS). Plus **HTTPS, alt atributy, meta description, canonical, Open Graph**.

**Q:** Responzivita — 3 viewporty?
**A:** **Mobile** (typicky < 768px), **tablet** (768-1024px), **desktop** (> 1024px). **Mobile-first** = CSS píšeš primárně pro mobile, postupně rozšiřuješ media queries. Google indexuje mobile-first od 2018.

**Q:** Mobile-first × Desktop-first?
**A:** **Mobile-first** = default styles pro mobil, `@media (min-width: X)` přidává pro větší. **Desktop-first** = default pro desktop, `@media (max-width: X)` zmenšuje. Mobile-first je **standard** (drobné CSS pro většinu, většina trafiku z mobilů).

---

## SWI 15 — Webová stránka

**Q:** Struktura HTML5 dokumentu?
**A:** `<!DOCTYPE html>` + `<html lang="cs">` (root) → `<head>` (meta, title, link CSS) + `<body>` (viditelný obsah). V body sémantické sekce: header, nav, main, article, section, aside, footer.

**Q:** Sémantické tagy × generické divy?
**A:** **Sémantické** (`<article>`, `<nav>`, `<header>`) **mají význam** — screen reader, SEO, parsery rozumí struktuře. **`<div>`** je generic wrapper bez významu. Pravidlo: vždy preferuj sémantický tag, `<div>` jen pro layout/styling.

**Q:** Atributy — globální × specifické?
**A:** **Globální** = lze na všech elementech (`id`, `class`, `style`, `data-*`, `lang`, `aria-*`, `title`). **Specifické** = jen na určitých (např. `href` jen na `<a>`, `src` jen na `<img>`/`<script>`).

**Q:** Co patří do `<head>`?
**A:** `<meta charset="UTF-8">`, `<meta name="viewport" content="width=device-width, initial-scale=1.0">` (povinné pro responzivitu!), `<title>`, `<meta name="description">`, `<link rel="stylesheet">`, `<script>` (může i v body).

**Q:** Hierarchie nadpisů?
**A:** `<h1>` → `<h6>`. **Jedno `<h1>` per stránka** (hlavní nadpis pro SEO). Nepřeskakuj úrovně (h1 → h2 → h3, ne h1 → h3). Strukturuje obsah pro screen readery + parsery.

---

## SWI 16 — CSS kaskáda

**Q:** Kaskáda — 3 hlavní faktory?
**A:** **Origin** (user-agent < user < author), **specificita** (tuple 4 kategorií), **source order** (později vyhrává při stejné specificitě). `!important` přebíjí celou specificitu (ale zhoršuje údržbu).

**Q:** Specificita tuple — 4 kategorie?
**A:** **(inline, ID, class+attr+pseudo-class, element+pseudo-element)**. Inline `style=""` = nejvyšší (1,0,0,0). ID `#id` = (0,1,0,0). Class/attr/pseudo-class = (0,0,1,0). Element/pseudo-element = (0,0,0,1). Třída + atribut + pseudo-třída se počítají **JAKO třída**, ne zvlášť.

**Q:** `@layer` × `@media`?
**A:** **`@layer`** = **CSS cascade layers**, řízení priority napříč souborů (moderní, 2022+). **`@media`** = **responzivní queries** (rozměry, dark mode, prefers-reduced-motion). Naprosto jiné věci, jen podobný `@` prefix.

**Q:** BEM konvence?
**A:** **Block__Element--Modifier**. Příklad: `card__icon--small`. `card` = block, `icon` = element uvnitř, `small` = modifier (varianta). Zabraňuje konfliktům třídních jmen + samodokumentace HTML/CSS.

**Q:** CSS isolation — způsoby?
**A:** **BEM** (manuální naming convention), **CSS Modules** (build-time scoped class names), **Shadow DOM** (nativní, Web Components), **CSS-in-JS** (styled-components, emotion). Cíl: zabránit, aby CSS v jedné komponentě ovlivnilo jinou.

---

## SWI 17 — Vlastnosti CSS

**Q:** CSS jednotky — absolutní × relativní?
**A:** **Absolutní** = pevné (`px`, `cm`, `mm`). **Relativní** = počítané z kontextu (`em` = z parent font-size, `rem` = z root font-size, `%` = z parent property, `vw`/`vh` = viewport size). Pro responzivitu preferuj relativní (zejména `rem`).

**Q:** `em` × `rem`?
**A:** **`em`** = relativní k **rodiči** (může se násobit při zanoření — `1.2em` v `1.2em` = `1.44em`). **`rem`** = relativní k **root** (`<html>`, defaultně 16px). `rem` je predictable, `em` je nuance.

**Q:** CSS proměnné — kde a jak?
**A:** Definice: `--nazev: hodnota;` (např. `--barva-primarni: #007bff;`). Použití: `color: var(--barva-primarni)`. **Scope**: na `:root` = globální, na třídě = lokální. Lze přepsat v media query (dark mode).

**Q:** Barvy — 3 formáty?
**A:** **HEX** (`#ff0000`), **RGB** (`rgb(255, 0, 0)` nebo `rgba()` s alpha), **HSL** (`hsl(0, 100%, 50%)` — hue/saturation/lightness, intuitivnější pro variace). Plus modern `oklch()`, `color()` pro wide-gamut.

**Q:** `@font-face` + font-display?
**A:** `@font-face { font-family: 'Inter'; src: url('inter.woff2') format('woff2'); font-display: swap; }`. Načte vlastní font. **`font-display: swap`** = zobraz fallback dokud font nenačte, pak vyměň (proti FOIT — Flash of Invisible Text).

**Q:** Co jsou ligatury v typografii?
**A:** **Spojení dvou+ znaků do jednoho glyphu** pro lepší čitelnost. Klasické: **fi → ﬁ**, **fl → ﬂ**, **ffi → ﬃ**. CSS: `font-feature-settings: "liga" 1;` (default zapnuto v moderních fontech). **Programovací ligatury** (Fira Code, JetBrains Mono) spojují `==`, `=>`, `!=` do jednoho symbolu.

---

## SWI 18 — React (teorie)

**Q:** React — co + kdo?
**A:** **JavaScriptová knihovna** pro UI od **Mety** (Facebook), 2013, aktuální **React 19** (2024). Komponentový přístup, deklarativní, **Virtual DOM**, unidirectional data flow.

**Q:** Virtual DOM × Shadow DOM (klasický chyták)?
**A:** **Virtual DOM** = **odlehčená kopie DOMu** v JS paměti, **optimalizace překreslování** (diff + apply jen rozdílů). React jen toto. **Shadow DOM** = **izolace stylů a struktury uvnitř komponenty** (Web Components, `<video>` controls). **React NEPOUŽÍVÁ Shadow DOM**, má jiné mechanismy (CSS Modules, styled-components).

**Q:** JSX — co + jak se zpracovává?
**A:** **Rozšíření JS syntaxe** pro HTML-like kód v JS. **NENÍ HTML, NENÍ čistý JS.** Transpiluje se na volání `React.createElement(...)`. Atributy **camelCase** (`className` ne `class`, `htmlFor` ne `for` — rezervovaná JS slova). JS výrazy v `{}`.

**Q:** Transpilace × kompilace?
**A:** **Transpilace** = source-to-source (čitelný → čitelný). JSX → JS, TS → JS, ES2024 → ES2015. **Kompilace** = source → strojový kód (C → exe). Babel, SWC, esbuild = transpilery v React ekosystému.

**Q:** Event loop + microtasks × macrotasks?
**A:** JS je **single-threaded** + asynchronní díky **event loop**. **Microtasky** (`Promise.then`, `MutationObserver`) mají vyšší prioritu než **macrotasky** (`setTimeout`, DOM events). Pořadí: synchronní kód → všechny microtasks → 1 macrotask → render → opakuj. `console.log` je **synchronní**, ne microtask.

---

## SWI 19 — Webové aplikace

**Q:** SPA × MPA?
**A:** **SPA** (Single Page Application) = jedna HTML stránka, JS routing, AJAX requesty (React, Vue). Rychlé navigace, ale velký initial JS bundle. **MPA** (Multi Page) = každá URL = nová HTML stránka ze serveru (klasický PHP, Razor). Pomalejší navigace, lepší SEO out-of-box.

**Q:** Request-response cyklus + porty?
**A:** Klient pošle **HTTP request** (metoda, URL, headers, body) na server. Server vrátí **HTTP response** (status code, headers, body). **HTTP = port 80**, **HTTPS = port 443**. HTTPS na port 80 selže přes TLS handshake.

**Q:** HTTP metody?
**A:** **GET** (číst, idempotentní), **POST** (vytvořit, NE idempotentní), **PUT** (update plný, idempotentní), **PATCH** (update částečný, NE idempotentní), **DELETE** (smazat, idempotentní), **HEAD** (jen headers), **OPTIONS** (CORS preflight).

**Q:** 401 × 403?
**A:** **401 Unauthorized** = "nevíme kdo jsi" (chybí přihlášení). **403 Forbidden** = "víme kdo jsi, ale nemáš práva". Drill chyták: rozlišuj!

**Q:** 5 nejčastějších stavových kódů?
**A:** **200 OK**, **201 Created** (po POST), **301/302 redirect**, **400 Bad Request** (klient chyba), **401/403** (auth), **404 Not Found**, **422 Unprocessable Entity** (validace), **500 Internal Server Error** (server crash).

---

## SWI 20 — Ověřování identity

**Q:** Hash hesel + salt?
**A:** **NIKDY plaintextem.** Hash + **salt** (random string per uživatel). Salt zabrání **rainbow tables** (předpočítaným hashům). Použij **bcrypt, Argon2, scrypt** (pomalé, anti-brute-force), NIKDY MD5/SHA-256 (rychlé = špatné na hesla).

**Q:** Token-based auth × session?
**A:** **Session** = server drží stav (session ID v cookie). **Token (JWT)** = stateless, server jen ověří podpis. Token vhodnější pro **microservices, mobil, SPA**.

**Q:** JWT — struktura + bezpečnost?
**A:** 3 části: **Header.Payload.Signature**. **Base64 encoded** + podpis. **NENÍ ŠIFROVANÝ!** Payload je čitelný (jwt.io). Hodnota: **podpis** chrání proti manipulaci, **NE odposlechu**. Proti odposlechu = HTTPS. Drill chyták: JWT není šifrování!

**Q:** OAuth × OpenID Connect?
**A:** **OAuth 2.0** = **authorization** ("co smíš dělat") — delegace přístupu, např. "umožni této appce číst moje fotky". **OpenID Connect** = nad OAuth = **authentication** ("kdo jsi") — identifikace uživatele. Sign-in with Google = OpenID Connect.

**Q:** 2FA — typy?
**A:** **Druhý faktor** přidává bezpečnost. Typy: **TOTP** (Authenticator apps, čas-based code), **SMS** (slabší, SIM swap risk), **hardware token** (YubiKey), **biometrika** (Touch ID, Face ID). Doporučení: TOTP / hardware.

---

## SWI 21 — RESTful

**Q:** REST principy — 6 omezení?
**A:** **Client-server**, **stateless** (každý request samostatný, server nedrží session), **cacheable**, **uniform interface** (jednotné API), **layered system** (klient neví co je za proxy), **code on demand** (volitelné, výjimečné). V praxi nejvíc důležité: stateless + uniform interface + správné HTTP metody.

**Q:** REST URL konvence?
**A:** **Plurál** jmen entit, **path id** pro konkrétní záznam, **sloveso v HTTP metodě**, ne URL. `GET /users` (všichni), `GET /users/5` (jeden), `POST /users` (vytvořit), `PUT /users/5` (update). NIKDY `/getUsers`, `/createUser`.

**Q:** AJAX × Fetch API?
**A:** **AJAX** (Asynchronous JavaScript and XML) = historický koncept asynchronních requestů bez page reloadu (Gmail, Maps revoluce 2005). **Fetch API** = moderní browser API pro HTTP requesty (`fetch()` + Promise). XMLHttpRequest = legacy.

**Q:** JSON — 6 datových typů?
**A:** **string**, **number**, **boolean**, **null**, **object** (`{}`), **array** (`[]`). Žádné date (string nebo timestamp), žádný comment. JSON je **subset JS**, ne ekvivalent.

**Q:** Idempotence metod?
**A:** **Idempotent** = N volání stejný efekt jako 1 volání. **GET, PUT, DELETE** = idempotent (smaž 5× → pořád smazaný). **POST** = NENÍ idempotent (POST 5× = 5 záznamů). Důležité pro retry logic v sítových chybách.

**Q:** JSON × XML — proč JSON vyhrál?
**A:** **XML** (eXtensible Markup Language) = starší (1996), formát s tagy `<user><name>Honza</name></user>`. **Verbose, schemas (XSD), SOAP**. **JSON** (2001) = lehčí `{"user": {"name": "Honza"}}`. **Méně overhead, nativní v JS, jednodušší parsing**. JSON dnes default pro REST API, XML přežívá v enterprise (SOAP, RSS feeds, konfigurace).

---

## SWI 22 — ASP.NET

**Q:** ASP.NET — co + typy aplikací?
**A:** **Framework od Microsoftu** pro server-side web app v C#. Typy: **Razor Pages** (page-based, jednoduché), **MVC** (controller-based), **Web API** (REST JSON), **Blazor** (C# místo JS — server-side i client WASM).

**Q:** Kestrel + reverzní proxy?
**A:** **Kestrel** = built-in HTTP server pro ASP.NET (cross-platform, rychlý). Před něj typicky **reverzní proxy** (Nginx, IIS, Apache) pro **TLS termination, load balancing, static files**. Architektura: Internet → Reverse proxy → Kestrel → ASP.NET app.

**Q:** Razor syntax — 3 modes?
**A:** **`@expression`** (jednoduchý výraz: `@User.Name`), **`@{ blok C# kódu }`** (víc příkazů), **`@:text`** (přepnout zpět do textu uvnitř C# bloku). Plus `@for/@if` jako control flow.

**Q:** Dependency Injection (DI)?
**A:** Komponenta dostane závislosti **přes konstruktor**, ne si je vytváří sama. Registrace v `Program.cs` (`builder.Services.AddScoped<IService, Service>()`). Lifecycles: **Singleton** (1 per app), **Scoped** (1 per request — DbContext!), **Transient** (1 per resolve — mailer).

**Q:** appsettings.json × User Secrets?
**A:** **`appsettings.json`** = config v repu (connection string DEV, feature flags). **User Secrets** = lokální dev secrets (`dotnet user-secrets`), **NE v repu**. **Environment variables** = produkční secrets (CI/CD).

---

## SWI 23 — Eventy

**Q:** Observer pattern × Pub-Sub?
**A:** **Observer** = 1 publisher, N subscribers, **přímá vazba** (subscriber zná publishera). Rychlé, vhodné pro UI eventy. **Pub-Sub** = subscribers neznají publishera, **přes broker** (message queue). Volnější vazba, vhodné pro mikroslužby.

**Q:** Event, subscriber, publisher?
**A:** **Event** = signál že se něco stalo (např. `ButtonClicked`). **Publisher** = vysílá events. **Subscriber** = poslouchá events a reaguje. **Subscribe** (registrace handleru), **unsubscribe** (odhlásit, jinak memory leak).

**Q:** C# delegát × event?
**A:** **Delegát** = typesafe **ukazatel na funkci** (`Action`, `Func<T>`, vlastní delegate types). **Event** = delegát s omezeným přístupem zvenku — mimo třídu jen `+=` (subscribe) a `-=` (unsubscribe), NE `=` (přepsat). Klíčové slovo `event` v deklaraci.

**Q:** Event delegation v JS DOM?
**A:** **1 listener na rodiči** + kontrola `e.target` (přes bubbling). Funguje pro **dynamicky přidané** prvky (které ještě neexistovaly při registraci). Výhoda: méně listenerů → výkon + memory.

**Q:** Memory leak v eventech — jak?
**A:** Subscriber drží referenci na handler. Pokud se zapomeneš `unsubscribe`, **publisher drží subscribera v paměti i po zničení**. Klasický leak. Moderní: **AbortController** (web), **WeakRef**, automatický cleanup v Angular/Vue.

---

## SWI 24 — Programovací jazyky

**Q:** Procedurální × OOP × funkcionální × deklarativní?
**A:** **Procedurální** (kroky: C, Pascal). **OOP** (objekty + zprávy: Java, C#). **Funkcionální** (čisté funkce, immutable: Haskell, F#). **Deklarativní** = říkám CO chci, ne JAK (SQL, HTML, Prolog). Moderní jazyky multi-paradigm.

**Q:** Kompilátor × interpret × hybrid?
**A:** **Kompilátor** = překlad celého kódu PŘED spuštěním (C → exe, Go, Rust). **Interpret** = řádek po řádku za běhu (Python, JS). **Hybrid** = kompilace do **mezikódu** (bytecode) + JIT/AOT (Java → JVM bytecode, C# → IL → CLR).

**Q:** Architektura .NET — CLR, IL, JIT?
**A:** **CLR** (Common Language Runtime) = běhové prostředí .NET. **IL** (Intermediate Language, bytecode .NET). **JIT** (Just-In-Time) = překládá IL → strojový kód **lazy per metoda při prvním volání**. **AOT** = předem (Native AOT, rychlejší start).

**Q:** JIT × AOT — trade-off?
**A:** **JIT** = překlad za běhu, **optimalizuje pro aktuální HW** + profile-guided, ale **pomalejší start** (warmup). **AOT** = překlad předem, **rychlejší start + menší binary**, ale méně optimalizace. Použití: JIT pro server, AOT pro mobil/CLI.

**Q:** BCL? TypeScript jak se zpracovává?
**A:** **BCL** = **Base Class Library** v .NET (String, List, Console, File, Math). Standardní knihovna. **TypeScript** = transpiluje se na JS přes `tsc`. NENÍ interpret přímo, JS pak interpretuje engine.

**Q:** Multiplatformnost — Mono × .NET Core × .NET 5+?
**A:** **.NET Framework** (2002) = JEN Windows. **Mono** (2004, komunitní) = .NET pro Linux/Mac/iOS/Android (zdroj pro Unity, Xamarin). **.NET Core** (2016, MS) = oficiální cross-platform .NET. **.NET 5+** (2020+) = unified runtime (sloučení Framework + Core + Mono). **Today**: jeden `.NET 8/9/10` napříč všemi platformami.

---

## SWI 25 — Android architektura

**Q:** 5 vrstev Android architektury?
**A:** **1)** Linux Kernel → **2)** HAL (Hardware Abstraction Layer) → **3)** Android Runtime + Native Libraries → **4)** Application Framework → **5)** Applications. Stack, každá vrstva poskytuje služby té nad ní.

**Q:** ART × Dalvik?
**A:** **ART** (Android Runtime) nahradil **Dalvik VM** v Androidu 5.0 (2014). Dalvik = JIT (za běhu). **ART = hybrid AOT + JIT** — kompiluje při instalaci, profile-guided při nečinnosti. Spouští **DEX** bytecode (Dalvik Executable). Drill: DEX vzniká přes **D8/R8 dexer** ze .class, ART ho kompiluje na strojový kód.

**Q:** 4 základní komponenty Android aplikace?
**A:** **Activity** (jedna obrazovka s UI), **Service** (background bez UI — hudba, navigace), **Broadcast Receiver** (reaguje na události — wifi, baterie), **Content Provider** (sdílí data s jinými apps přes URI).

**Q:** Sandbox + AndroidManifest.xml?
**A:** **Sandbox** = každá aplikace má vlastní **Linux UID** + proces + paměť + filesystem. Izolace + bezpečnost. **AndroidManifest.xml** = **povinný config**, registruje všechny komponenty + permissions + intent filters. Bez registrace komponenta **NEEXISTUJE**.

**Q:** Activity Lifecycle?
**A:** `onCreate → onStart → onResume → onPause → onStop → onDestroy`. **Resume PŘED Pause!** Při rotaci se Activity **úplně zničí a vytvoří znovu** → proto **ViewModel** (přežije konfigurační změny). Drill chyták: pořadí.

**Q:** Co je Intent + 2 typy?
**A:** **Zpráva** spouštějící komponentu nebo přenášející data. **Explicitní** = znám konkrétní cílovou komponentu (`Intent(this, DetailActivity::class.java)`). **Implicitní** = deklaruji záměr (`Intent.ACTION_VIEW` + URL), systém najde vhodnou app + případně Intent Chooser dialog.

**Q:** Co je Compose a MVVM?
**A:** **Jetpack Compose** (stable 2021) = **moderní deklarativní UI framework**, místo XML píšeš Kotlin funkce s `@Composable`. Reactive recomposition. **MVVM** (Model-View-ViewModel) = Google-doporučená architektura. View pozoruje state ve ViewModelu, ViewModel volá Model (Repository). **ViewModel přežije rotaci** = klíčová výhoda.

**Q:** Permissions — 4 kategorie + runtime?
**A:** **Normal** (auto při instalaci — INTERNET, VIBRATE). **Dangerous** (runtime, user schvaluje za běhu — CAMERA, LOCATION). **Signature** (apps se stejným podpisem). **Special** (Settings — SYSTEM_ALERT_WINDOW). **Runtime permissions** od Android 6.0 (2015) — dangerous se neudělují při instalaci, ale při prvním použití dialogem.

---

# 💻 DAT — Data a kódování (25 témat)

> Format: kód s ??? doplnit, NEBO "co tento kód dělá", NEBO "proč X místo Y". Žádné programování od nuly.

---

## DAT 1 — HTML5 a sémantika

**Q (TEORIE):** Co je HTML a HTML5?
**A:** **HyperText Markup Language** — značkovací jazyk pro popis **struktury** webových stránek. NE programovací jazyk (žádná logika, jen popis). **HTML5** (2014) přinesl sémantické tagy (`<article>`, `<nav>`, `<header>`), nativní `<video>`/`<audio>`, formuláře (`type="email"`, validace), Canvas, Web Storage.

**Q (TEORIE):** Proč sémantické tagy místo `<div>`?
**A:** **3 důvody:** 1) **Accessibility** — screen reader oznámí *"navigation"*, *"article"*, slepí lidé se orientují. 2) **SEO** — Google parsuje strukturu, hierarchii. 3) **Údržba** — kód je samodokumentující. `<div>` je generický wrapper bez významu.

**Q (TEORIE):** Co je typografie na webu?
**A:** Volba a kontrola **písma**: `font-family` (rodina, fallback chain → generická), `font-size` (rem pro responzivitu), `line-height` (řádkování ~1.5), `letter-spacing`, `font-weight`. Plus **typografická pravidla**: česká mezera u `10 %`, dlouhé pomlčky, uvozovky `„"`.

**Q (FILL):** Doplň 5 sémantických tagů.
```html
<???>Logo + navigace</???>     <!-- horní část stránky -->
<???>Hlavní obsah</???>         <!-- hlavní obsah -->
<???>Článek</???>               <!-- samostatný uzavřený obsah -->
<???>Pravý sloupec</???>        <!-- postranní obsah -->
<???>Copyright</???>            <!-- patička -->
```
**A:** `<header>`, `<main>`, `<article>`, `<aside>`, `<footer>`. Plus `<nav>` (navigace), `<section>` (logický oddíl bez vlastní identity).

**Q (CONCEPT):** Co dělá `<meta name="viewport" content="width=device-width, initial-scale=1.0">`?
**A:** **POVINNÉ pro responzivitu**. Říká browseru, aby viewport měl šířku zařízení (ne defaultní desktop 980px) a aby nepřibližoval. Bez něj mobil renderuje stránku jako desktop a zmenší ji = nečitelné.

**Q (STYLE):** Proč `<button>` místo `<div onclick>` pro tlačítko?
**A:** **Sémantika** + **accessibility** + **klávesnice** (Tab navigace, Enter aktivace). Screen reader oznámí *"button"*. `<div onclick>` nemá nic z toho. Plus `<button>` má focus styling defaultně.

**Q (FILL):** Form s labelem + validací.
```html
<form>
    <label ???="email">Email</label>     <!-- ???1 = atribut spojující label s inputem -->
    <input type="email" ???="email" ??? />   <!-- ???2 = id atribut;  ???3 = povinné -->
</form>
```
**A:** `???1 = for`, `???2 = id="email"`, `???3 = required`.

**Q (CHYTÁK):** Rozdíl `alt` × `figcaption`?
**A:** **`alt`** na `<img>` = popis pro **slepé / když se obrázek nenačte**. **`<figcaption>`** uvnitř `<figure>` = **popisek viditelný všem** (titulek pod obrázkem). NEDUPLIKOVAT obojí stejně.

---

## DAT 2 — Bootstrap

**Q (TEORIE):** Co je Bootstrap?
**A:** **Nejpoužívanější CSS framework** (Twitter, 2011). Připravené komponenty + utility classes + grid system. Aktuální **verze 5** (2021, dropped jQuery). Alternativa k psaní vlastních CSS — rychlý prototyp, konzistentní design out-of-box. Konkurence: Tailwind CSS, Bulma.

**Q (TEORIE):** Bootstrap grid — princip 12 sloupců?
**A:** **Mobile-first 12-column grid**. Container → row → cols. **5 breakpointů**: `xs` (default), `sm` (576px+), `md` (768px+), `lg` (992px+), `xl` (1200px+), `xxl` (1400px+). `col-md-6` = polovina šířky od `md` výše. Součet sloupců na řádku má dát 12.

**Q (TEORIE):** Bootstrap utility classes?
**A:** **Atomické helper třídy** pro běžné CSS bez psaní vlastního. **Spacing**: `m-3` (margin all 3), `mb-2` (margin-bottom 2), `px-4` (padding x-axis 4). **Display**: `d-flex`, `d-none`. **Text**: `text-center`, `text-danger`. **Background**: `bg-primary`. Princip "compose nad write".

**Q (FILL):** Bootstrap grid.
```html
<div class="???">                          <!-- ???1 = wrapper s max-width -->
    <div class="???">                      <!-- ???2 = řádek -->
        <div class="???">Sloupec 1</div>   <!-- ???3 = sloupec 1/12 -->
        <div class="col-md-6">Sloupec 2</div>
    </div>
</div>
```
**A:** `???1 = container` (`container-fluid` = full-width), `???2 = row`, `???3 = col` (auto-width nebo `col-12 col-md-6`).

**Q (FILL):** Bootstrap card.
```html
<div class="???">                          <!-- ???1 = card wrapper -->
    <img src="..." class="???" alt="...">  <!-- ???2 = obrázek na vrchu -->
    <div class="???">                       <!-- ???3 = tělo s paddingem -->
        <h5 class="???">Nadpis</h5>         <!-- ???4 = titulek karty -->
        <button class="btn ???">Klik</button> <!-- ???5 = modré primary tlačítko -->
    </div>
</div>
```
**A:** `card`, `card-img-top`, `card-body`, `card-title`, `btn-primary`.

**Q (CONCEPT):** Co dělá `navbar-expand-lg`?
**A:** **Na large breakpointu a větším je navbar rozbalený** (horizontální menu). **Na menších se schová do hamburger menu** (collapse). Drill: `navbar-expand-X` ≠ `navbar-collapse` (collapse je inner wrapper).

**Q (CHYTÁK):** `col-md-6` × `row-cols-md-2`?
**A:** **`col-md-6`** = jednotlivý sloupec má **6/12 = polovinu** šířky od md breakpointu. **`row-cols-md-2`** = na řádku jsou **2 sloupce uniformně** od md breakpointu (auto šířka). Pattern volíš podle: každý jiný / všechny stejné.

**Q (STYLE):** Proč `btn btn-primary` ne jen `btn-primary`?
**A:** `btn` = **base styly** tlačítka (padding, font, transitions). `btn-primary` = **jen varianta barvy**. Bootstrap utility design — base + variant.

---

## DAT 3 — Flexbox

**Q (TEORIE):** Co je Flexbox a kdy ho použít?
**A:** **CSS layout model pro 1D rozložení** (řádek NEBO sloupec). Aktivuje se `display: flex` na rodiči. Děti se stávají **flex items** s automatickým chováním (zarovnání, rozestupy). Vhodný pro **navbar, card content, button group, formuláře**. Pro 2D (grid + cols) použij CSS Grid.

**Q (TEORIE):** Main axis × Cross axis?
**A:** **Main axis** = směr, kterým se kladou items (defaultně **horizontálně** s `flex-direction: row`). **Cross axis** = kolmá osa. `justify-content` zarovnává podél **main**, `align-items` podél **cross**. Když změníš `flex-direction: column`, osy se prohodí.

**Q (TEORIE):** Co dělá `gap` a proč > margin?
**A:** **`gap`** na flex/grid kontejneru = mezera **MEZI** prvky (ne před prvním, za posledním). Moderní (~2021), elegantní. **Margin** by vyžadovalo `:first-child`/`:last-child` triky pro stejný efekt. Mantra: *"vždy gap, nikdy margin mezi flex items."*

**Q (FILL):** Centered card v middle screen.
```css
body {
    display: ???;                  /* ???1 */
    ???: center;                   /* ???2 = na hlavní ose */
    ???: center;                   /* ???3 = na příčné ose */
    min-height: 100vh;
}
```
**A:** `???1 = flex`, `???2 = justify-content`, `???3 = align-items`.

**Q (CONCEPT):** Co dělá `flex: 1` na potomkovi?
**A:** Zkratka pro `flex-grow: 1; flex-shrink: 1; flex-basis: 0`. Element **vyplní zbývající místo**, šíří se proporcionálně. Klasický pattern: navbar logo (`flex: 0`), menu (`flex: 1`), avatar (`flex: 0`).

**Q (CHYTÁK):** `flex: none` × `flex-grow: 0`?
**A:** **`flex: none`** = `0 0 auto` = **nepružit ANI nesmrštit**, pevná velikost. **`flex-grow: 0`** = neroste, ALE může **shrinkovat** (default shrink: 1). Pro pevně logo používej `flex: none`.

**Q (FILL):** Horizontální × vertikální flex?
```css
.row-layout {
    display: flex;
    flex-direction: ???;   /* ???1 = horizontálně (default) */
}
.col-layout {
    display: flex;
    flex-direction: ???;   /* ???2 = vertikálně (sloupec) */
}
```
**A:** `???1 = row`, `???2 = column`.

**Q (STYLE):** `gap` × `margin` mezi flex items?
**A:** **`gap: 1rem`** na rodiči = moderní (od 2021), **mezera jen MEZI prvky** (ne před první, za posledním). **`margin`** na items = legacy, vyžaduje `:first-child`/`:last-child` triky. Vždy preferuj `gap`.

---

## DAT 4 — CSS Grid

**Q (TEORIE):** Co je CSS Grid a kdy?
**A:** **CSS layout model pro 2D rozložení** (řádky × sloupce simultánně). Aktivuje se `display: grid`. **Definice mřížky** přes `grid-template-columns/rows` + opčně **pojmenované oblasti** `grid-template-areas`. Vhodný pro **page layout** (holy grail), **dashboards**, **galerie**.

**Q (TEORIE):** Flexbox × Grid — kdy co?
**A:** **Flexbox** = 1D layout (jen jedna osa), dynamický, vhodný pro **komponenty** (navbar, card body, button row). **Grid** = 2D (řádky + sloupce), explicit areas, vhodný pro **page-level layout**. Kombinace: Grid pro page, Flex uvnitř komponent.

**Q (TEORIE):** Co je `fr` jednotka a `repeat()`?
**A:** **`fr` (fraction)** = část zbývajícího místa po fixních hodnotách. `200px 1fr 1fr` = 200px fixní + 2 sloupce dělící zbytek. **`repeat(N, X)`** = opakování `repeat(3, 1fr)` = `1fr 1fr 1fr`. **`repeat(auto-fit, minmax(250px, 1fr))`** = responzivní auto-grid.

**Q (FILL):** Holy grail layout.
```css
body {
    display: ???;                                  /* ???1 */
    ???: 200px 1fr 200px;                          /* ???2 = velikosti sloupců */
    ???: 80px 1fr 60px;                            /* ???3 = velikosti řádků */
    ???:                                            /* ???4 = pojmenované oblasti */
        "header header header"
        "side main ads"
        "footer footer footer";
}
header { ???: header; }                            /* ???5 = přiřazení k oblasti */
```
**A:** `grid`, `grid-template-columns`, `grid-template-rows`, `grid-template-areas`, `grid-area`.

**Q (CONCEPT):** Co je `1fr`?
**A:** **Fraction** unit — část zbývajícího místa po fixních hodnotách. `200px 1fr 1fr` = 200px sloupec + 2 sloupce stejně dělící zbytek. Flexibilní + responzivní.

**Q (CHYTÁK):** `grid-template-areas` × `grid-template`?
**A:** **`grid-template-areas`** = jen pojmenované areas. **`grid-template`** = **SHORTHAND** pro template-rows + template-columns + template-areas dohromady. Drill: u area použij **specifickou** property, ne shorthand.

**Q (FILL):** Auto-fit responzivní grid.
```css
.cards {
    display: grid;
    grid-template-columns: ???(auto-fit, minmax(250px, 1fr));   /* ???1 */
    gap: 1rem;
}
```
**A:** `???1 = repeat`. Pattern: `repeat(auto-fit, minmax(min, 1fr))` = automaticky se přizpůsobí, každý sloupec min 250px, jinak roste rovnoměrně.

**Q (STYLE):** Grid × Flexbox — kdy co?
**A:** **Grid** = 2D layout (řádky × sloupce simultánně), explicit areas (holy grail, dashboardy). **Flexbox** = 1D layout (řádek NEBO sloupec), dynamický (navbar, card content). Kombinace: Grid pro page-level, Flex pro components.

---

## DAT 5 — Pozicování + z-index

**Q (TEORIE):** 5 hodnot CSS position?
**A:** **`static`** (default — v normal flow), **`relative`** (v flow, ale lze posunout přes top/left, dělá anchor pro absolute potomky), **`absolute`** (vyjmuto z flow, pozice vůči nejbližšímu positioned předkovi), **`fixed`** (vyjmuto, vůči viewportu, neuhne se při scrollu), **`sticky`** (hybrid — flow + sticky behavior při scrollu).

**Q (TEORIE):** Co je stacking context?
**A:** **Vrstvový kontext**, ve kterém se vyhodnocuje `z-index`. Defaultně root document. **Tvořen positioned elementem s `z-index`** NEBO speciálními property: `transform`, `opacity < 1`, `filter`, `isolation: isolate`. Důsledek: `z-index` potomka **NEPROJDE** ven ze svého stacking contextu.

**Q (TEORIE):** Co je obtékání textu (float)?
**A:** **`float: left/right`** = původně pro obtékání obrázku textem (časopisecký layout). Element se přesune k okraji, ostatní obsah ho **obteče**. Dnes legacy — Flexbox/Grid lepší. Float ještě používán pro **drop cap** (zvětšené první písmeno).

**Q (FILL):** Sticky header + modal + badge.
```css
.header     { position: ???; top: 0; }              /* ???1 = drží se na vrchu při scrollu */
.modal-bg   { position: ???; top:0; left:0; ... }   /* ???2 = drží se vůči viewportu */
.parent     { position: ???; }                       /* ???3 = anchor pro absolutní dítě */
.badge      { position: ???; top: -5px; right: -5px; } /* ???4 = vůči nejbližšímu positioned předkovi */
```
**A:** `sticky`, `fixed`, `relative`, `absolute`.

**Q (CONCEPT):** Proč `transform: scale(1)` vytvoří nový stacking context?
**A:** **Klasická past.** Některé CSS vlastnosti **vytvoří nový stacking context**, i když mají identitní hodnotu. Triggery: `transform`, `opacity < 1`, `filter`, `isolation: isolate`, `position: fixed/sticky`. Důsledek: z-index potomků se renderuje **uvnitř** tohoto contextu — `z-index: 9999` na potomkovi nezasáhne ven.

**Q (CHYTÁK):** Sticky NEFUNGUJE pokud...?
**A:** **Některý PŘEDEK má `overflow: hidden/auto/scroll`**. Sticky element se "drží" k overflow contextu — pokud rodič scrolluje, sticky zmizí s rodičem. Drill: zkontroluj všechny předky.

**Q (FILL):** Centrování absolutně.
```css
.center {
    position: absolute;
    top: 50%;
    left: 50%;
    ???: translate(-50%, -50%);   /* ???1 = posune element o vlastní rozměry */
}
```
**A:** `???1 = transform`. Alternativa: `inset: 0; margin: auto`.

**Q (STYLE):** z-index — doporučené hierarchie?
**A:** **Konvenční vrstvy:** sticky 10-100, dropdowns 100-200, modals 1000+, toasts 9999+. **Round numbers** (1, 10, 100, 1000, 9999), ne random (`6767`). Drill: vždy v rámci stacking contextu.

---

## DAT 6 — CSS animace

**Q (TEORIE):** 3 typy CSS animací?
**A:** **1) Transformace** (`transform: scale/rotate/translate/skew`) = okamžitá změna. **2) Transitions** (`transition: prop dur timing`) = plynulý přechod z A do B při změně property (hover, class). **3) Keyframe animations** (`@keyframes` + `animation`) = multi-step, spouští se sám, podporuje infinite + fill-mode.

**Q (TEORIE):** Co je timing function?
**A:** **Křivka rychlosti** animace. **`linear`** = konstantní. **`ease`** (default) = pomalý start + konec, rychlé střed. **`ease-in`** = pomalý start. **`ease-out`** = pomalý konec. **`ease-in-out`** = obojí. Custom: **`cubic-bezier(x1, y1, x2, y2)`**.

**Q (TEORIE):** Proč prefers-reduced-motion?
**A:** **Accessibility media query.** Někteří uživatelé mají na OS úrovni nastaveno *"snížit pohyb"* (epileptik, motion sickness, vestibular disorder). V `@media (prefers-reduced-motion: reduce) { ... }` **vypneš animace** (nebo zkrátíš na 0.01ms). Komise oceňuje.

**Q (FILL):** Spinner.
```css
???                                  /* ???1 = klíčové slovo pro definici */
spin {
    from { transform: rotate(0deg); }
    to   { transform: rotate(???); } /* ???2 = jedna otáčka */
}
.spinner {
    animation: spin 1s ??? ???;     /* ???3 = lineární časování;  ???4 = nekonečně */
}
```
**A:** `@keyframes`, `360deg`, `linear`, `infinite`. Drill: úhel **VŽDY s jednotkou** (`deg`, `rad`, `turn`). `rotate(360)` neexistuje.

**Q (CHYTÁK):** `transition` × `animation`?
**A:** **`transition`** = plynulá změna **z A do B**, spuštěná **změnou property** (hover, focus, class add). Jednoduché. **`animation`** = multi-step **`@keyframes`**, spouští se sám/přes class, podporuje infinite, fill-mode atd. Mantra: *"hover scale = transition. spinner = animation."*

**Q (FILL):** Hover button.
```css
.btn { ???: transform 0.2s ease; }     /* ???1 = plynulý přechod */
.btn:hover { transform: ???(1.05); }   /* ???2 = transformace velikosti */
```
**A:** `transition`, `scale`.

**Q (CHYTÁK):** `animation-fill-mode: forwards` × `paused`?
**A:** **`forwards`** = **drží POSLEDNÍ keyframe** po dokončení animace (chtěné chování). **`paused`** = animation-play-state, **zastaví animaci** (běžící/pozastavená). Naprosto jiné. Drill: `forwards` pro "zůstaň v konečném stavu".

**Q (STYLE):** Accessibility — animace?
**A:** **`@media (prefers-reduced-motion: reduce) { ... }`** — user na OS úrovni zapne "snížit pohyb" (epileptik, motion sickness). V této query **zruš animations** nebo dej **0.01ms duration**. Komise to ocení jako bonus.

---

## DAT 7 — Tabulky

**Q (TEORIE):** Kdy `<table>` a kdy ne?
**A:** **Tabulky JEN pro tabulková data** — data v řádcích × sloupcích s vztahem (cenník, statistika, kalendář). **NIKDY pro layout** (před CSS Grid se to dělalo, ale dnes anti-pattern: zlobí accessibility + responzivita).

**Q (TEORIE):** Struktura tabulky?
**A:** **`<table>`** wrapper → **`<caption>`** (title, accessibility) → **`<thead>`** (header row + `<th scope="col">`) → **`<tbody>`** (data rows) → **`<tfoot>`** (volitelné, sumace). Plus **`<colgroup>`/`<col>`** pro stylování celých sloupců.

**Q (TEORIE):** Co je accessibility v tabulce?
**A:** **`<caption>`** = title pro screen reader. **`scope="col"`/`"row"`** na `<th>` = propojí header s daty (screen reader oznámí *"plán: basic, cena: 100 Kč"*). Pro komplexní tabulky **`id`** na `<th>` + **`headers="id1 id2"`** na `<td>`.

**Q (FILL):** Struktura tabulky.
```html
<???>                          <!-- ???1 = wrapper -->
    <???>Ceník 2026</???>       <!-- ???2 = title -->
    <???>                       <!-- ???3 = header sekce -->
        <tr>
            <??? scope="col">Plán</???>  <!-- ???4 = header buňka -->
        </tr>
    </???>
    <???>                       <!-- ???5 = data sekce -->
        <tr>
            <td>Basic</td>
        </tr>
    </???>
</???>
```
**A:** `table`, `caption`, `thead`, `th`, `tbody`.

**Q (FILL):** Sloučení buněk.
```html
<td ???="2">Spans 2 columns</td>     <!-- ???1 = horizontálně -->
<td ???="3">Spans 3 rows</td>        <!-- ???2 = vertikálně -->
```
**A:** `colspan`, `rowspan`. Drill: `col` = horizontální (kolony), `row` = vertikální (řádky).

**Q (FILL):** CSS pro tabulku.
```css
table {
    ???: collapse;          /* ???1 = spojí dvojité bordery */
    width: 100%;
}
th { text-align: ???; }     /* ???2 = na střed */
```
**A:** `border-collapse`, `center`.

**Q (CONCEPT):** Co je `scope` atribut?
**A:** **Accessibility** — propojí header buňku s daty. `scope="col"` = header sloupce, `scope="row"` = header řádku. Screen reader pak ví, ke kterému headeru data patří.

**Q (STYLE):** Kdy NEpoužívat `<table>`?
**A:** **Tabulky JEN pro tabulková data** (řádky × sloupce s vztahem). NIKDY pro layout (před CSS Grid se to dělalo). Pro layout: Flexbox/Grid.

---

## DAT 8 — Datové typy a pole

**Q (TEORIE):** Strukturované × primitivní typy?
**A:** **Primitivní** (value types) = jednoduchá hodnota v paměti (`int`, `bool`, `char`, `double`). Stack-allocated, kopírují se. **Strukturované** (composite) = z více částí (`struct`, `class`, pole, string). Většinou reference, heap. **Struct** = value type s atributy (hybrid).

**Q (TEORIE):** Co je enum a kdy ho použít?
**A:** **Výčtový typ** — pojmenovaná množina konstant. Místo magic numbers (`0 = Pondeli`, `1 = Utery`) máš jména: `enum Den { Pondeli, Utery, ... }`. **Type-safe** (kompilátor zakontroluje), **switch exhaustive check**, **autocomplete**. Defaultně podtyp `int`, lze změnit.

**Q (TEORIE):** Rectangular × jagged pole?
**A:** **Rectangular** `int[,]` = **obdélníkové** (všechny "řádky" mají STEJNOU délku). Souvislý blok paměti, rychlý. `new int[3, 4]`. **Jagged** `int[][]` = **zubaté** (pole polí, různé délky). Více alokací, flexibilnější. `new int[3][]` + jednotlivě `[i] = new int[N]`.

**Q (FILL):** Primitivní typy v C#.
```csharp
??? cena = 99;                  // ???1 = 32-bit integer
??? vaha = 78.5;                // ???2 = 64-bit floating-point (DEFAULT pro literál!)
??? mena = 1234.56m;            // ???3 = přesné, suffix 'm', pro peníze
??? jeAktivni = true;           // ???4 = boolean
```
**A:** `int`, `double`, `decimal`, `bool`. Drill: `78.5` je defaultně **`double`**, ne `float`. Float vyžaduje `78.5f`.

**Q (CHYTÁK):** `Length` × `Count`?
**A:** **`Length`** = property u **pole** (`int[]`, `string`). **`Count`** = property u **kolekcí** (`List<T>`, `Dictionary`). `int[] x = {...}; x.Length` ✓. `List<int> y; y.Count` ✓. Drill: mantra "Length pro array, Count pro kolekce".

**Q (FILL):** 2D pole.
```csharp
int[???] rectangular = new int[3, 4];   // ???1 = rectangular (čtvercové)
int[???] jagged = new int[3][];          // ???2 = jagged (zubaté, různé délky)
jagged[0] = ??? int[] { 1, 2, 3 };       // ???3 = inicializace při assignment
```
**A:** `[,]`, `[][]`, `new`. Drill: `new int[] { 1, 2, 3 }` při assignmentu (collection initializer bez `new` funguje JEN při deklaraci).

**Q (CONCEPT):** Enum — k čemu?
**A:** **Výčtový typ** = množina pojmenovaných konstant. `enum Den { Pondeli, Utery, ... }`. Místo magic numbers (0/1/2/...) máš jména. Type-safe, autocomplete, switch exhaustive check.

**Q (STYLE):** Proč `decimal` pro peníze, ne `double`?
**A:** `double` (IEEE 754 binary float) **nemůže přesně reprezentovat** 0.1, 0.2 atd. — kumulativní chyby. `decimal` (128-bit, base 10) je **přesný pro desetinná čísla v desítkové soustavě**. Mantra: *"float/double pro fyziku, decimal pro peníze."*

---

## DAT 9 — Spojové struktury a stromy

**Q (TEORIE):** Co je spojový seznam (linked list)?
**A:** **Datová struktura z uzlů (nodes)**. Každý uzel obsahuje **data** + **referenci na další uzel** (a u doubly i `Prev`). Na rozdíl od pole **NENÍ souvislý v paměti**, uzly jsou rozeseté + spojené přes ukazatele. Výhoda: **O(1) insert/delete** v středu (mám-li referenci).

**Q (TEORIE):** Co je strom a binární strom?
**A:** **Strom** = hierarchická struktura, **uzly s potomky** (jeden root, žádné cykly). **Binární strom** = každý uzel má **max 2 potomky** (Left, Right). **BST (Binary Search Tree)** = binární strom s pravidlem *"levý < uzel < pravý"* → hledání O(log n) ve vyváženém.

**Q (TEORIE):** Ukazatel (pointer/reference)?
**A:** **Reference v paměti** na jiný objekt. V C# **referenční typy** (string, class, array) implicitně držené přes referenci. **Null** = neukazuje nikam (čtení = NullReferenceException). Linked list, strom = datové struktury postavené **na ukazatelích/referencích**.

**Q (FILL):** Linked List node.
```csharp
class Uzel<T> {
    public T ??? { get; set; }      // ???1 = data
    public Uzel<T>? ??? { get; set; } // ???2 = ref na DALŠÍ uzel (singly)
}
```
**A:** `Data` (nebo `Value`/`Hodnota`), `Next` (nebo `Dalsi`). Drill: **PascalCase** pro public property v C#.

**Q (FILL):** Binary Tree node.
```csharp
class StromUzel<T> {
    public T Hodnota { get; set; }
    public StromUzel<T>? ??? { get; set; }  // ???1 = levý potomek
    public StromUzel<T>? ??? { get; set; }  // ???2 = pravý potomek
}
```
**A:** `Left`, `Right`. C# konvence **PascalCase**.

**Q (CONCEPT):** BST (Binary Search Tree) — invariant?
**A:** Pro **každý uzel**: všechny hodnoty v **levém** podstromě jsou **menší** než hodnota uzlu, všechny v **pravém** jsou **větší**. Hledání/insert/delete = **O(log n)** ve vyváženém. Nevyvážený degraduje na **O(n)**.

**Q (STYLE):** Singly × Doubly linked list?
**A:** **Singly** = každý uzel má **`Next`** (jen jeden směr). **Doubly** = `Next` + **`Prev`** (oba směry). Doubly umožňuje **iterovat zpět** + smazat uzel v O(1) (mám reference na předka).

**Q (CHYTÁK):** Pole × LinkedList — kdy co?
**A:** **Pole/List<T>** = **O(1) access podle indexu**, O(n) insert/delete v středu (musí se posunout zbytek). **LinkedList<T>** = O(n) access, **O(1) insert/delete kdekoli** (mám-li referenci). Pole častější — moderní cache locality.

---

## DAT 10 — Podprogramy a lambda

**Q (TEORIE):** Funkce × procedura?
**A:** **Funkce** = vrací hodnotu (`int Soucet(a, b) => a+b`). **Procedura** = NIC nevrací (`void Vypis(text) {...}`). V C# obojí je "metoda" — rozdíl jen v return type (`void` = procedura). V matematice klasický rozdíl, v moderním programování spíš synonyma.

**Q (TEORIE):** Co je lambda funkce?
**A:** **Anonymní funkce zapsaná jednou ze 3 forem**: 1) `x => x * 2` (1 param, 1 výraz, implicit return). 2) `(a, b) => a + b` (víc paramů). 3) `(x) => { return x * 2; }` (víc příkazů). Hodnota: lze přiřadit do proměnné (`Func<int, int>`) nebo předat jako argument (filter, map).

**Q (TEORIE):** Co je rekurze?
**A:** **Funkce volá sama sebe** s menším problémem + **ukončovací podmínka** (base case). Klasický příklad: faktoriál (`n * Faktorial(n-1)` + `if (n <= 1) return 1`). Bez base case = nekonečná rekurze = **StackOverflowException**. Vhodné pro stromy, fraktály, Divide & Conquer (quicksort).

**Q (TEORIE):** Obor platnosti proměnné (scope)?
**A:** **Kde je proměnná viditelná**. **Blokový** (`{ }` uvnitř if/for/funkce — nejmenší). **Funkční/metodový** (celá metoda). **Třídní** (member fields, celá třída). **Globální** (málokdy v C# — statické třídy). Vnořené scopes **vidí ven**, ne dovnitř. **Shadowing** = vnořená proměnná překryje stejně pojmenovanou z vnějšího scope.

**Q (FILL):** Funkce × procedura.
```csharp
??? PozdravProcedura(string jmeno) {     // ???1 = nic nevrací
    Console.WriteLine($"Ahoj {jmeno}!");
}
int PozdravFunkce(string jmeno) {
    ??? jmeno.Length;                     // ???2 = vrátit hodnotu
}
```
**A:** `void`, `return`. Drill: `return` v `int` metodě **MUSÍ mít hodnotu** (`return 1`, ne jen `return`).

**Q (FILL):** ref × out parametry.
```csharp
void Zmenit(??? int x) { x = 10; }       // ???1 = modifikuje existující (musí být init)
void Vratit(??? int x) { x = 20; }       // ???2 = vrátí PŘES parametr (nemusí být init)
```
**A:** `ref`, `out`. Drill: `ref` = obousměrné (před voláním inicializované). `out` = jen výstupní (před voláním nemusí být inicializované).

**Q (FILL):** Lambda funkce.
```csharp
???<int, int> zdvojnasob = x => x * 2;    // ???1 = bere int, vrací int
???<string> vypis = s => Console.WriteLine(s);  // ???2 = bere string, NIC nevrací
???<int> citac = () => 42;                 // ???3 = nic neberé, vrací int
```
**A:** `Func<int, int>`, `Action<string>`, `Func<int>`. Drill: **Func má POSLEDNÍ generický typ = NÁVRAT**. Action vždy void.

**Q (CONCEPT):** Rekurze — princip?
**A:** **Funkce volá sama sebe** s **menším argumentem** + **ukončovací podmínka**. Bez ukončovací podmínky = StackOverflowException. Klasika: faktoriál (`n * Faktorial(n-1)` + `if (n <= 1) return 1`).

**Q (CHYTÁK):** Closure v lambdě?
**A:** Lambda **zachytí proměnné z vnějšího scope** (closure). Pozor: zachytí **referenci, ne hodnotu**. Past v `for` cyklu s `Task.Run` — všechny tasky vidí poslední hodnotu `i`. Řešení: `int kopie = i;` před lambdou.

---

## DAT 11 — Kolekce

**Q (TEORIE):** Co je kolekce + 5 hlavních?
**A:** **Datová struktura držící skupinu objektů**. V C# v `System.Collections.Generic`: **`List<T>`** (dynamické pole), **`Dictionary<K,V>`** (key→value mapa), **`HashSet<T>`** (unikátní hodnoty, bez duplikátů), **`Queue<T>`** (FIFO — fronta), **`Stack<T>`** (LIFO — zásobník).

**Q (TEORIE):** FIFO × LIFO?
**A:** **FIFO** (First In First Out) = **fronta** (Queue). První přidaný = první ven. Klasická fronta v obchodě. **LIFO** (Last In First Out) = **zásobník** (Stack). Poslední přidaný = první ven. Talíře v komínu. Volba podle pořadí, jaké chceš zachovat.

**Q (TEORIE):** Pole × List?
**A:** **Pole `int[]`** = **fixní velikost**, deklarovaná při vytvoření, nelze měnit délku. Rychlé, přímý přístup. **`List<T>`** = **dynamické pole**, můžeš `Add`/`Remove`, vnitřně se realokuje při růstu. Pohodlnější + flexibilní. Pro většinu use cases List.

**Q (FILL):** Hlavní kolekce.
```csharp
???<string> seznam = new();               // ???1 = dynamické pole
???<string, int> slovnik = new();         // ???2 = key-value mapa
???<int> unikatni = new();                // ???3 = množina bez duplikátů (default)
???<string> fronta = new();               // ???4 = FIFO
???<string> zasobnik = new();             // ???5 = LIFO
```
**A:** `List`, `Dictionary`, `HashSet`, `Queue`, `Stack`.

**Q (FILL):** Operace.
```csharp
list.???("novy");        // ???1 = přidat na konec
list.???(2);             // ???2 = odstranit podle INDEXU
list.???("hodnota");     // ???3 = odstranit podle HODNOTY (první match)
queue.???("a");          // ???4 = přidat do fronty
queue.???();             // ???5 = odebrat z fronty
stack.???("a");          // ???6 = přidat na zásobník
stack.???();             // ???7 = odebrat ze zásobníku
```
**A:** `Add`, `RemoveAt`, `Remove`, `Enqueue`, `Dequeue`, `Push`, `Pop`. Drill: `RemoveAt(index)` × `Remove(item)`.

**Q (CONCEPT):** LINQ — k čemu?
**A:** **Language Integrated Query** — SQL-like queries nad kolekcemi. `arr.Where(x => x > 5).Select(x => x * 2).OrderBy(x => x)`. Fluent API, deferred execution (vyhodnocuje se až při enumerate). Funkcionální styl, čitelnější než for cykly.

**Q (CHYTÁK):** `Dictionary.ContainsKey` × indexer?
**A:** **`dict.ContainsKey("key")`** = bezpečná kontrola, vrátí bool. **`dict["key"]`** = pokud klíč neexistuje, **vyhodí KeyNotFoundException**. Bezpečnější: `TryGetValue(key, out value)`. Drill: před indexer vždy ContainsKey/TryGetValue.

**Q (STYLE):** `HashSet` × `List` — kdy co?
**A:** **`HashSet<T>`** = O(1) Contains, **bez duplikátů**, neudržuje pořadí. **`List<T>`** = O(n) Contains, **dovoluje duplikáty**, udržuje pořadí. Pro "už jsem ho viděl" kontrolu používej HashSet.

---

## DAT 12 — Souborový systém

**Q (TEORIE):** Co je souborový systém?
**A:** **Způsob, jakým OS organizuje data na disku**. Hierarchická struktura (složky/soubory) + metadata (jméno, velikost, datum, oprávnění). **Hlavní:** NTFS (Windows, žurnálovaný), ext4 (Linux), APFS (macOS), FAT32/exFAT (flash disky). Linux pravidlo *"vše je soubor"* (i klávesnice je `/dev/input`).

**Q (TEORIE):** Textový × binární soubor?
**A:** **Textový** = data jako znaky (UTF-8 kódování). `.txt`, `.csv`, `.json`, `.xml`, `.html`, kód. Otevřitelný v editoru. **Binární** = raw bajty bez konverze. `.png`, `.mp3`, `.exe`, `.zip`, `.pdf`. Identifikace přes **magic number** (první bajty), ne přípona.

**Q (TEORIE):** Co je stream a buffer?
**A:** **Stream** = sekvenční proud bajtů (`FileStream`, `NetworkStream`). Nečte celý soubor do paměti, **zvládne soubory větší než RAM**. **Buffer** = mezi-paměť v RAM před zápisem na disk (rychlejší). `Flush()` = spláchne buffer. `using` zaručí Dispose() = uzavření + flush.

**Q (FILL):** File API.
```csharp
string text = ???("a.txt");             // ???1 = celý text
???("b.txt", "obsah");                   // ???2 = zápis celého textu
string[] lines = ???("a.txt");           // ???3 = po řádcích
byte[] bytes = ???("kniha.pdf");         // ???4 = binární
```
**A:** `File.ReadAllText`, `File.WriteAllText`, `File.ReadAllLines`, `File.ReadAllBytes`.

**Q (FILL):** Stream pro velké soubory.
```csharp
??? StreamReader sr = new StreamReader("velky.txt");   // ???1 = auto-dispose klíčové slovo
string? line;
while ((line = sr.???()) != null) {                     // ???2 = jeden řádek
    Console.WriteLine(line);
}
```
**A:** `using`, `ReadLine`. Drill: `using` garantuje `Dispose()` na konci scope, **i při výjimce**.

**Q (FILL):** Procházení složky.
```csharp
string[] vse = Directory.???("data", "*", SearchOption.???);  // ???1, ???2
string cesta = ???("data", "podslozka", "file.txt");           // ???3 = multiplatformně
```
**A:** `GetFiles`, `AllDirectories`, `Path.Combine`. Drill: `Path.Combine` automaticky vyřeší `\` × `/` podle OS.

**Q (CONCEPT):** Co je stream a kdy ho použít?
**A:** **Sekvenční proud bajtů** v `System.IO`. Nečte celý soubor do paměti, **zvládne soubory větší než RAM**. Pro **velké soubory** (10 GB log) nebo síťové zdroje. Pro malé textové soubory (`<10 MB`) stačí `File.ReadAllText` (jednodušší).

**Q (CHYTÁK):** Magic number × file extension?
**A:** **Magic number** = první bajty souboru identifikující typ (PNG = `89 50 4E 47`, JPEG = `FF D8 FF`). **Spolehlivá** identifikace. **Extension** (`.png`, `.jpg`) = jen jméno, lze přejmenovat. Důvěryhodný způsob detekce typu = magic, ne přípona.

---

## DAT 13 — Async/parallel

**Q (TEORIE):** Co je vlákno (thread) × proces?
**A:** **Proces** = běžící program s vlastní pamětí (chrome.exe). **Vlákno** = jednotka výpočtu UVNITŘ procesu, sdílí paměť s ostatními vlákny stejného procesu. Více vláken = paralelizace + komunikace přes sdílenou paměť (ale **race conditions**).

**Q (TEORIE):** Paralelní × asynchronní programování?
**A:** **Paralelní** = víc úloh běží **OPRAVDU SOUČASNĚ** na různých jádrech CPU (CPU-bound — výpočet). **Asynchronní** = úloha **uvolní vlákno během čekání** (I/O-bound — síť, disk). Bez paralelizace, jen efektivnější využití času.

**Q (TEORIE):** Co je Task v C#?
**A:** **Abstrakce nad vláknem** v `System.Threading.Tasks`. **`Task`** = operace bez návratu, **`Task<T>`** = vrací hodnotu. Používá **ThreadPool** (recyklace vláken, levné). Důležité metody: `Task.Run`, `Task.WhenAll`, `Task.WhenAny`, `Task.Delay`.

**Q (CONCEPT):** Async/await × Task.Run × Parallel.For — KDY CO?
**A:** **`async/await`** = **I/O-bound** (síť, disk, DB) — uvolní vlákno, neblokuje. **`Task.Run`** = **CPU-bound** (výpočet) — spustí na ThreadPool, hlavní vlákno volné. **`Parallel.For`** = **paralelní cyklus** přes jádra (CPU-bound, hodně iterací). Mantra: *"await na čekání, Task.Run na výpočty, Parallel.For na cykly."*

**Q (FILL):** Async metoda.
```csharp
??? Task<string> StahniAsync(string url) {       // ???1 = klíčové slovo pro async
    using HttpClient client = new();
    string data = ??? client.GetStringAsync(url); // ???2 = čeká na Task
    return data;
}
```
**A:** `async`, `await`.

**Q (FILL):** Paralelní hledání + měření času.
```csharp
???.For(0, 100, i => Pracuj(i));                  // ???1 = třída pro paralelní cykly
??? sw = ???.StartNew();                           // ???2 = high-res timer
int jader = ???.ProcessorCount;                    // ???3 = environment info
Task<int>[] tasky = new Task<int>[jader];
// ...
int[] vysledky = await Task.???(tasky);            // ???4 = počká na všechny
```
**A:** `Parallel`, `Stopwatch`, `Environment`, `WhenAll`.

**Q (CHYTÁK):** Closure v `for` cyklu s Task.Run?
**A:** **Past!** Lambda zachytí REFERENCI na proměnnou `i`. Po skončení cyklu je `i = N+1` u všech tasků. Řešení: **lokální kopie**:
```csharp
for (int i = 0; i < 10; i++) {
    int kopie = i;
    tasky[i] = Task.Run(() => Pracuj(kopie));
}
```

**Q (CHYTÁK):** Race condition — řešení?
**A:** Více vláken modifikuje stejnou proměnnou → nedeterministické. Řešení: **`lock(zamek) { citac++; }`** (blok kódu) NEBO **`Interlocked.Increment(ref citac)`** (atomická, rychlejší) NEBO **Concurrent kolekce** (`ConcurrentBag`, `ConcurrentDictionary`).

**Q (TEORIE):** Synchronizace — `lock` × `Mutex` × `Semaphore`?
**A:** **`lock(obj) { ... }`** = C# syntaxe pro **kritickou sekci**, vlastní jednomu vláknu zaráz. Single-process. **`Mutex`** = jako lock, ale lze sdílet **mezi procesy** (named mutex). **`Semaphore(N)`** = umožní **N současných vláken** (např. limit 5 connection v poolu). Mantra: lock pro běžně, Mutex pro cross-process, Semaphore pro limit počtu.

---

## DAT 14 — Git

**Q (TEORIE):** Co je Git a co řeší?
**A:** **Distribuovaný verzovací systém** (Linus Torvalds, 2005). Sleduje **historii změn** v projektu, umožňuje **paralelní vývoj** přes větve, **merge**, **rollback** na předchozí stav. **Distribuovaný** = každý developer má **celou historii lokálně** (offline práce).

**Q (TEORIE):** Co je GitHub × Git?
**A:** **Git** = nástroj (CLI, lokální). **GitHub** = **webová platforma** kolem Gitu — hosting repozitářů, PR/code review, issues, CI/CD (GitHub Actions), social. Alternativy: GitLab, Bitbucket, Codeberg.

**Q (TEORIE):** Větve (branches) — k čemu?
**A:** **Paralelní linie vývoje**. Main = stabilní, feature branches = vývoj nové funkce v izolaci. Po hotovo `merge` zpět do main. **Pattern Git Flow** (main + develop + feature/release/hotfix) nebo **GitHub Flow** (main + feature). Default branch dříve `master`, dnes **`main`** (od 2020).

**Q (FILL):** Základní příkazy.
```bash
git ???                # inicializace
git ???                # vidět změny
git ??? .              # přidat do staging
git ??? -m "msg"       # commit
git ???                # historie
```
**A:** `init`, `status`, `add`, `commit`, `log`.

**Q (FILL):** Větve.
```bash
git ??? -c feature-x      # MODERNÍ vytvořit + přepnout
git ???                   # vidět všechny větve
git ??? feature-x         # v main: sloučit feature do main
git ???                   # stáhnout + auto-merge ze vzdáleného
```
**A:** `switch`, `branch`, `merge`, `pull`. Drill: `switch -c` (moderní) × `checkout -b` (legacy).

**Q (CONCEPT):** Co je staging area?
**A:** **Mezistupně mezi working dir a commit historií**. `git add` přesune změny ze working dir → staging. `git commit` vezme staged změny → vytvoří commit. Umožňuje **selektivní commits** (jen některé soubory).

**Q (CHYTÁK):** `.gitignore` u trackovaného souboru?
**A:** **`.gitignore` filtruje JEN untracked soubory.** Pokud je soubor už trackovaný (commited), `.gitignore` na něj NEMÁ vliv. Řešení: `git rm --cached soubor` = odstraní z trackingu (zachová na disku), pak `.gitignore` zafunguje.

**Q (CONCEPT):** Merge conflict — co dělat?
**A:** Git neumí auto-merge → označí konflikty v souborech (`<<<<<<<`, `=======`, `>>>>>>>`). **Manuálně vyřeš** (zachovej co chceš), smaž markery, `git add` + `git commit`. NEBO `git merge --abort` pro zrušení.

**Q (TEORIE):** Git CLI × klientské aplikace?
**A:** **CLI** (`git` v terminálu) = nativní, plný control, naučit se pro pochopení. **GUI klienti** (snadnější UX): **GitHub Desktop** (oficiální, jednoduchý), **GitKraken** (cross-platform, vizuální), **SourceTree** (Atlassian), **Tower** (Mac). **VS Code / IDE** mají integrovaný Git plugin (commit, push, history). CLI = power, GUI = pohodlí.

---

## DAT 15 — ER model (draw.io)

**Q (TEORIE):** Co je ER model?
**A:** **Entity-Relationship Model** — Peter Chen (1976), grafický návrh databáze PŘED implementací. **3 stavební prvky:** entita (objekt, tabulka), atribut (vlastnost), vztah (relace mezi entitami). Slouží jako **dokumentace + komunikace** mezi developery + analytiky.

**Q (TEORIE):** Co je relační databáze?
**A:** **Data uložená v tabulkách (relations)** s pevným schématem. Tabulky propojené přes **klíče** (PK, FK). Vychází z **relační algebry** (Codd, 1970). ACID transakce, SQL jazyk. Příklady: MSSQL, PostgreSQL, MySQL, SQLite, Oracle.

**Q (TEORIE):** Co je kardinalita?
**A:** **Počet instancí jedné entity vázaných na druhou**. Notace: **1**, **N/M** (mnoho), **0..1** (0 nebo 1), **1..N** (alespoň 1), **\*** (any). Typy vztahů: **1:1**, **1:N** (one-to-many), **M:N** (many-to-many).

**Q (CONCEPT):** Co je entita, atribut, vztah?
**A:** **Entita** = objekt z reálného světa (Uživatel, Kniha). V diagramu obdélník. **Atribut** = vlastnost entity (jméno, email). Elipsa nebo seznam v obdélníku. **Vztah** = vazba mezi entitami (Uživatel půjčuje Knihu). Čára/kosočtverec.

**Q (CONCEPT):** Vztahy 1:1, 1:N, M:N — realizace?
**A:** **1:1** = FK + UNIQUE constraint na jedné straně (`uzivatel.profilId UNIQUE`). **1:N** = FK na "many" straně (`kniha.autorId`). **M:N** = **VAZEBNÍ TABULKA** s composite PK (`Autor_Kniha(autorId, knihaId)`).

**Q (CONCEPT):** Kandidátní × primární × superklíč?
**A:** **Superklíč** = jakákoli množina sloupců co identifikuje řádek (může být přebytečná). **Kandidátní klíč** = **MINIMÁLNÍ** superklíč (žádný sloupec navíc). **Primární klíč** = vybraný kandidátní (1 per tabulka, NOT NULL).

**Q (STYLE):** Surrogate × natural key?
**A:** **Surrogate** = umělý interní ID (`id INT AUTO_INCREMENT`). Stabilní, anonymní. **Natural** = z reálných dat (email, ISBN, SSN). **Konvence: surrogate** — natural se může změnit (email update), reálná entita ne (interní ID).

**Q (CHYTÁK):** M:N s atributy?
**A:** Pokud má vazba **vlastní atributy** (datum výpůjčky, status, počet kusů), vazební tabulka se stává **plnohodnotnou entitou**. Plus potřebuje **surrogate PK** (vlastní `id`), pokud chceš **opakované záznamy** (např. user si půjčí stejnou knihu 2× v různých datech).

---

## DAT 16 — SQL výběr

**Q (TEORIE):** Co je SQL?
**A:** **Structured Query Language** — **deklarativní** dotazovací jazyk pro relační DB (1974, IBM). Píšeš **CO chceš**, ne JAK (RDBMS optimalizér vyřeší). Standardizován ISO/ANSI, ale dialekty se liší (MSSQL T-SQL × MySQL × PostgreSQL × SQLite).

**Q (TEORIE):** Co je SELECT a JOIN?
**A:** **`SELECT`** = výběr dat z tabulek. Klíčové prvky: sloupce, FROM, WHERE filter, ORDER BY, LIMIT. **`JOIN`** = spojení 2+ tabulek přes shodné hodnoty (typicky FK = PK). Typy: **INNER** (jen kde match), **LEFT** (všechny z levé), **RIGHT**, **FULL OUTER**, **CROSS**.

**Q (TEORIE):** Co je agregace?
**A:** **Funkce přes skupinu řádků**, vrátí JEDNU hodnotu. `COUNT(*)`, `SUM`, `AVG`, `MIN`, `MAX`. Typicky kombinováno s **`GROUP BY`** (např. *"průměrná známka per student"*). Filter agregovaných výsledků = **`HAVING`**.

**Q (FILL):** Pořadí klauzulí.
```sql
???   sloupce        -- ???1
???   tabulka        -- ???2
???   tabulka2 ON ?  -- ???3 = spojení tabulek
???   filter         -- ???4 = filter ŘÁDKŮ
???   sloupce        -- ???5 = seskupení
???   filter         -- ???6 = filter SKUPIN
???   sloupec ???    -- ???7 = řazení;  ???8 = sestupně
???   5;             -- ???9 = limit
```
**A:** `SELECT`, `FROM`, `JOIN`, `WHERE`, `GROUP BY`, `HAVING`, `ORDER BY`, `DESC`, `LIMIT`.

**Q (FILL):** Agregace.
```sql
SELECT
    ???(*),                  -- ???1 = počet řádků
    ???(price),              -- ???2 = součet
    ???(price),              -- ???3 = průměr
    ???(price), ???(price)   -- ???4 = min;  ???5 = max
FROM produkty;
```
**A:** `COUNT`, `SUM`, `AVG`, `MIN`, `MAX`.

**Q (CHYTÁK):** WHERE × HAVING?
**A:** **WHERE** = filtruje **JEDNOTLIVÉ ŘÁDKY** PŘED `GROUP BY`. **HAVING** = filtruje **SKUPINY** PO `GROUP BY` (může pracovat s agregacemi). Mantra: *"nad X / víc než Y po agregaci" → HAVING. "kde X = Y na úrovni řádku" → WHERE.*

**Q (FILL):** Concat v SQLite.
```sql
SELECT jmeno ??? ' ' ??? prijmeni AS cele_jmeno FROM student;   -- ???
```
**A:** `||` (dvojité pipe). Drill: `||` v SQLite/PostgreSQL, `+` v MSSQL, `CONCAT()` v MySQL.

**Q (CHYTÁK):** LIMIT × TOP?
**A:** **LIMIT N** (SQLite, MySQL, PostgreSQL) na konec query. **TOP N** (MSSQL) hned po SELECT. Pozice jiná!
```sql
-- SQLite
SELECT ... ORDER BY x DESC LIMIT 5;
-- MSSQL
SELECT TOP 5 ... ORDER BY x DESC;
```

---

## DAT 17 — REST API v ASP.NET

**Q (TEORIE):** Co je REST API?
**A:** **Representational State Transfer** — architektonický styl pro webové API (Roy Fielding, 2000). **Klíčové principy:** stateless (každý request samostatný), uniform interface (jednotný způsob), URL = zdroj (`/users/5`), HTTP metoda = akce (GET/POST/PUT/DELETE). Response typicky JSON.

**Q (TEORIE):** Co je controller v ASP.NET?
**A:** **C# třída obsahující endpointy** (akce). Dědí z `ControllerBase` (Web API) nebo `Controller` (MVC s views). Anotovaná **`[ApiController]`** (auto-validation, problem details) + **`[Route]`** (URL prefix). Public metody s **`[HttpGet/Post/Put/Delete]`** = endpointy.

**Q (TEORIE):** Co je routing v Web API?
**A:** **Mapování URL → akce v controlleru**. Atributové routing přes **`[Route("api/[controller]")]`** (`[controller]` = jméno třídy bez Controller suffix). Metody s **`[HttpGet("{id}")]`** přidávají relativní path + parametry. Alternativa: convention-based (`Program.cs` `MapControllerRoute`).

**Q (FILL):** Controller.
```csharp
???                              // ???1 = atribut "REST API controller"
???("api/[controller]")          // ???2 = atribut pro base route
public class MoviesController : ControllerBase
{
    private readonly AppDbContext _db;
    public MoviesController(AppDbContext db) { _db = db; }

    ???                          // ???3 = GET
    public ActionResult<List<Movie>> GetAll() {
        return Ok(_db.Movies.ToList());
    }
}
```
**A:** `[ApiController]`, `[Route]`, `[HttpGet]`. Drill: **PascalCase** atributy.

**Q (CHYTÁK):** Relativní × absolutní route?
**A:** Když controller má `[Route("api/[controller]")]`, metody mají **relativní path** k base. `[HttpGet]` = base. `[HttpGet("{id}")]` = `api/movies/{id}`. `[HttpGet("/abs")]` s lomítkem = **přepíše base** (málokdy chceš). Drill: bez lomítka = relativní (idiomatic).

**Q (CHYTÁK):** SaveChanges po Add/Update/Remove?
**A:** **EF Core: každá změna MUSÍ být následována `_db.SaveChanges()`** (nebo `SaveChangesAsync`). Add bez SaveChanges = data NEjsou v DB. Klasická chyba začátečníka.

**Q (FILL):** Status kódy.
```csharp
return ???();                          // ???1 = 200 OK
return ???();                          // ???2 = 404 Not Found
return ???();                          // ???3 = 204 No Content (po DELETE/PUT)
return ???(nameof(GetById), new { id }, obj);   // ???4 = 201 Created (po POST)
```
**A:** `Ok`, `NotFound`, `NoContent`, `CreatedAtAction`.

**Q (STYLE):** Proč `ActionResult<T>` ne jen `T`?
**A:** **`ActionResult<T>`** umožní vrátit **buď T NEBO status helper** (`NotFound()`, `BadRequest()`). Bez něj bys musel vracet jen `T` (a chyby řešit přes výjimky). `ActionResult<T>` je flexibilní + typed.

---

## DAT 18 — Razor Pages

**Q (TEORIE):** Co jsou Razor Pages?
**A:** **Page-based framework v ASP.NET Core** (od 2017). Alternativa k MVC pro **jednodušší aplikace**. Každá stránka má **2 soubory**: `Index.cshtml` (view, Razor markup) + `Index.cshtml.cs` (PageModel, C# code-behind). Klasický server-side rendering MPA.

**Q (TEORIE):** Co je Razor?
**A:** **Server-side template engine** od Microsoftu. Mix HTML + C# kódu. **Syntaxe:** `@expression` (inline výraz), `@{ blok }` (víc příkazů), `@for`/`@if` (control flow), `@page` (directive). Razor zkompiluje na C# kód, který generuje HTML response.

**Q (TEORIE):** GET × POST × handlery?
**A:** **`OnGet()`** = handler pro **HTTP GET** (zobrazit stránku/form). **`OnPost()`** = handler pro **HTTP POST** (zpracovat form submit). **Pojmenované handlery** = `OnPostSave()`, `OnPostReset()` — víc akcí na jedné stránce, vybírá se přes `?handler=Save` v URL nebo `asp-page-handler` v HTML.

**Q (FILL):** PageModel.
```csharp
public class LoginModel : PageModel
{
    ???                       // ???1 = atribut: property naplnit z POST body
    public InputModel Input { get; set; } = new();

    public ??? OnGet() { return Page(); }     // ???2 = co handler pro GET vrací (klasický return type)
    
    public IActionResult ???() {              // ???3 = jméno handler metody pro POST
        if (!???.IsValid) return Page();      // ???4 = property pro validation state
        return ???("/Dashboard");             // ???5 = PRG pattern redirect
    }
    
    public IActionResult ???() { ... }        // ???6 = pojmenovaný handler "Reset"
}
```
**A:** `[BindProperty]`, `void` (NEBO `IActionResult`), `OnPost`, `ModelState`, `RedirectToPage`, `OnPostReset`.

**Q (CHYTÁK):** EmailAddress × DataType(DataType.Email)?
**A:** **`[EmailAddress]`** = SKUTEČNÁ VALIDACE formátu (a@b.c regex). **`[DataType(DataType.Email)]`** = jen RENDER HINT pro HTML (`type="email"` input). Drill: validace VS render. Použít obojí pro best result.

**Q (FILL):** Pretty URL pattern.
```csharp
@page "{email?}"               // route s volitelným parametrem

public class LoginModel : PageModel {
    ???                        // ???1 = atribut "naplň i z GET URL"
    public string? Email { get; set; }
}
```
**A:** `[BindProperty(SupportsGet = true)]`. Drill: bez `SupportsGet=true` se property naplní jen z POST, ne z GET URL parametru.

**Q (CONCEPT):** PRG pattern?
**A:** **Post-Redirect-Get** = po úspěšném POST udělej **redirect na GET stránku** (s ID/success message). Zabraňuje **double-submit při F5** (browser by reposnal). Klasický anti-pattern: po POST vrátit Page() — F5 = duplicitní záznam.

**Q (CHYTÁK):** Kdy se spouští validace?
**A:** **JEN v OnPost** přes `ModelState.IsValid`. **NE automaticky v OnGet**. Pokud chceš validovat GET (např. v pretty URL), musíš ručně `TryValidateModel(...)`.

---

## DAT 19 — Tag Helpers

**Q (TEORIE):** Co jsou Tag Helpers?
**A:** **Atributy `asp-*` na HTML elementech** v Razor stránkách. Generují HTML server-side podle C# modelu — žádný manuální string concat. Příklady: `asp-for` (link na property), `asp-page` (URL k page), `asp-validation-for` (validation span). Modernější než HTML Helpers (`@Html.TextBoxFor`).

**Q (TEORIE):** Model binding — co to je?
**A:** **Automatické naplnění C# objektu z HTTP requestu** (form data, query string, route, body JSON). Razor: property s **`[BindProperty]`** se naplní z **POST body**. Plus **`[BindProperty(SupportsGet=true)]`** i z GET URL. **Validace** se spustí pak přes `ModelState.IsValid`.

**Q (TEORIE):** Validace formuláře?
**A:** **2 vrstvy:** **klient** (`data-val-*` atributy z model annotations, jQuery Unobtrusive Validation) = rychlá UX. **Server** (`ModelState.IsValid` v OnPost) = **single source of truth**. Anotace: `[Required]`, `[EmailAddress]`, `[StringLength]`, `[Range]`, `[Compare]`.

**Q (FILL):** Form helpers.
```html
<form ???="post">                              <!-- ???1 = HTTP metoda -->
    <label ???="Input.Email">Email</label>     <!-- ???2 = link na property -->
    <input ???="Input.Email" />
    <span ???="Input.Email"></span>            <!-- ???3 = validation message pro pole -->
    
    <select ???="Input.KurzId" ???="Model.AvailableKurzy">  <!-- ???4 = options source -->
    </select>
    
    <button ???="Save">Uložit</button>         <!-- ???5 = pojmenovaný handler -->
    <a ???="Detail" ???-id="@id">Detail</a>    <!-- ???6 = page;  ???7 = route prefix -->
</form>
```
**A:** `method`, `asp-for`, `asp-validation-for`, `asp-items`, `asp-page-handler`, `asp-page`, `asp-route`.

**Q (CHYTÁK):** `asp-for` na select × `asp-items`?
**A:** **`asp-for`** = property KAM se uloží vybraná hodnota. **`asp-items`** = ZDROJ options k zobrazení. Mantra: *"asp-for = kam uložit, asp-items = co nabídnout."*

**Q (CONCEPT):** name × id v asp-for?
**A:** `asp-for="Input.Email"` vygeneruje `name="Input.Email"` (pro **model binding** při POST) + `id="Input_Email"` (pro **label-input** pár přes `for=`). Oba atributy z jedné property.

**Q (CHYTÁK):** Client-side × server-side validation?
**A:** **Klient** = `data-val-*` atributy + jQuery Unobtrusive Validation = JS validace v browser (rychlá UX). **Server** = `ModelState.IsValid` = single source of truth. **S vypnutým JS klient stejně POST udělá, server validuje sám.** Nikdy nespoléhej JEN na klient.

**Q (CONCEPT):** asp-page-handler mechanizmus?
**A:** `asp-page-handler="Save"` v HTML vygeneruje **query param `?handler=Save`** v URL. ASP.NET podle něj rozhodne, kterou metodu volat: `OnPostSave()` místo `OnPost()`. Pattern pro **víc akcí na jedné stránce**.

---

## DAT 20 — Next.js

**Q (TEORIE):** Co je Next.js a kdo to dělá?
**A:** **React framework od Vercelu** (2016, dnes Next 15+). Postavený nad Reactem, přidává: file-based routing, SSR/SSG/ISR, image optimization, API routes, middleware. Full-stack řešení out-of-box. **Knihovna × framework**: React je lib (jen UI), Next je framework (volá tvůj kód).

**Q (TEORIE):** SSR × CSR — co a kdy?
**A:** **SSR** (Server-Side Rendering) = HTML se generuje **na serveru při requestu**, klient dostane hotové HTML. Lepší **SEO**, rychlejší first paint, ale větší zátěž serveru. **CSR** (Client-Side Rendering) = server pošle prázdné HTML + JS, browser renderuje. SPA klasika. Slabší SEO bez SSR.

**Q (TEORIE):** Co je file-based routing?
**A:** **Soubory a složky = URL struktura**, žádná centrální routes konfigurace. Next 13+ App Router: `app/page.tsx` = `/`, `app/about/page.tsx` = `/about`, `app/blog/[slug]/page.tsx` = `/blog/:slug` (dynamic). **Konvenční soubory**: `layout.tsx`, `loading.tsx`, `error.tsx`, `not-found.tsx`.

**Q (CONCEPT):** Server × Client Components?
**A:** **Server** (default v App Router) = render NA SERVERU, klient dostane HTML. **Bez hooks** (useState/useEffect). Můžou `async/await` pro DB/API. **Client** = `"use client"` direktiva PRVNÍ řádek. Má hooks, eventy, browser API. Větší JS bundle.

**Q (FILL):** Dynamic route Next 15.
```tsx
// app/blog/[slug]/page.tsx
export default ??? function Page({         // ???1 = async (kvůli await)
    params,
}: {
    params: ???<{ slug: string }>;          // ???2 = wrapper type pro Next 15
}) {
    const { slug } = ??? params;             // ???3 = klíčové slovo pro odbalit
    return <h1>{slug}</h1>;
}
```
**A:** `async`, `Promise`, `await`. Drill Next 15: `params` je **Promise**, musí se await. Next 14 byl přímý objekt.

**Q (FILL):** Client Component.
```tsx
???;                                     // ???1 = direktiva PRVNÍ řádek
import { useState, useEffect } from "react";

export default function Clock() {
    const [time, setTime] = useState<string | null>(null);
    useEffect(() => {
        const id = setInterval(() => setTime(new Date().toLocaleTimeString()), 1000);
        return () => ???(id);             // ???2 = cleanup
    }, []);
    return <p>{time ?? "Načítám..."}</p>;
}
```
**A:** `"use client"`, `clearInterval`.

**Q (CHYTÁK):** `<Link>` v Next × React Router?
**A:** **Next.js:** `import Link from "next/link"; <Link href="/about">`. **React Router:** `import { Link } from "react-router-dom"; <Link to="/about">`. Drill: **Next používá `href`** (jako HTML `<a>`), **React Router `to`**.

**Q (CHYTÁK):** SSG × SSR × ISR × CSR?
**A:** **SSG** = HTML při buildu (statické pro všechny). **SSR** = HTML při každém requestu (personalizované). **ISR** = SSG, ale re-builduje po N sekundách (best of both). **CSR** = render v browseru po načtení JS (klasické SPA).

---

## DAT 21 — Prisma ORM

**Q (TEORIE):** Co je ORM?
**A:** **Object-Relational Mapping** = abstrakce mezi **objekty v kódu** a **relační databází**. Místo psaní SQL píšeš v jazyce aplikace (TS/C#), ORM přeloží na SQL. Výhody: **type-safety, autocomplete, migrace, ochrana před SQL injection**. Nevýhody: výkon, N+1 problém, "leaky abstraction".

**Q (TEORIE):** Co je Prisma?
**A:** **Moderní TypeScript ORM** (2019). 3 hlavní části: **`schema.prisma`** (DSL pro definici DB schématu), **migrations** (versioned SQL changes), **Prisma Client** (auto-generated type-safe API). Konkurence: TypeORM, Drizzle, Sequelize. V .NET ekvivalent: **Entity Framework Core**.

**Q (TEORIE):** Co je migrace?
**A:** **Versioned SQL change** představující změnu v DB schématu. `prisma migrate dev --name add-users` vygeneruje SQL soubor + aplikuje + regeneruje klient. Migrace jsou **committed do repa** → každý dev/produkce dostane stejné změny. Klíčové pro **schema evoluci** bez ztráty dat.

**Q (FILL):** Schema.
```prisma
??? client { provider = "prisma-client" }   // ???1 = klient
??? db { provider = "sqlite" }              // ???2 = databáze

??? User {                                  // ???3 = entita (tabulka)
    id    Int    @id @default(???)          // ???4 = auto-inkrement
    email String @???                       // ???5 = UNIQUE constraint
    posts Post[]                            // 1:N relace
}

??? Post {
    id      Int  @id @default(autoincrement())
    title   String
    authorId Int
    author  User @???(fields: [authorId], references: [id])  // ???6 = relace (FK)
}
```
**A:** `generator`, `datasource`, `model`, `autoincrement()`, `unique`, `relation`.

**Q (FILL):** CRUD.
```ts
const user = await prisma.user.???({    // ???1 = INSERT
    data: { email: "..." }
});

const post = await prisma.post.findUnique({
    where: { id: 5 },
    ???: { author: true }                // ???2 = eager load relace
});
```
**A:** `create`, `include`.

**Q (CHYTÁK):** `where` × `include` × `select`?
**A:** **`where`** = filtruje **které** záznamy vrátit. **`include`** = vrátí + **navazující relace** (`include: { author: true }`). **`select`** = vrátí JEN vybraná pole (pick).

**Q (CONCEPT):** Migrace — workflow?
**A:** 1) Edituj `schema.prisma` (přidej model/pole). 2) `npx prisma migrate dev --name nazev` = vygeneruje SQL migration soubor + aplikuje na dev DB + regeneruje klient. 3) Při deployu: `prisma migrate deploy` (aplikuje pending migrace na produkci).

**Q (CHYTÁK):** N+1 problém?
**A:** `posts.forEach(p => prisma.user.findUnique({ where: { id: p.authorId } }))` = 1 query na posts + N queries na authors = **N+1 dotazů**. Řešení: `prisma.post.findMany({ include: { author: true } })` = JEDEN dotaz se JOIN. Klasická perf past.

---

## DAT 22 — React komponenty

**Q (TEORIE):** Co je React komponenta?
**A:** **JS/TS funkce přijímající props a vracející JSX**. Stavební kámen React aplikace. Tag-like syntaxe (`<Komponenta />`) je jen **syntax cukr** pro volání funkce. **Pravidlo:** velkým písmenem (`Pozdrav`) — malé je HTML element (`p`, `div`).

**Q (TEORIE):** Co jsou props?
**A:** **Properties** = vstupní data komponenty od rodiče. Předávají se jako atributy v JSX (`<Avatar src="..." size={32} />`). **Immutable** z perspektivy dítěte (nesmí měnit). V TS typované přes `type ComponentProps = { ... }`. Speciální: **`children`** prop pro obsah mezi tagy.

**Q (TEORIE):** Co je state?
**A:** **Interní paměť komponenty** ovlivňující render. Vytvoří se přes `useState(initial)` hook. Změna setterem (`setX`) triggeruje **re-render**. Klíčové vs props: **state vlastní komponenta** (může měnit), **props dostává od rodiče** (immutable).

**Q (FILL):** Komponenta s typed props.
```tsx
???                            // ???1 = klíčové slovo pro TS type
Props = {
    text: string;
    onSmaz: (id: number) => ???;   // ???2 = návrat funkce co NIC nevrací
};

function Item({ text, onSmaz }: Props) {
    return <button ???={() => onSmaz(1)}>Smaž</button>;  // ???3 = event handler
}
```
**A:** `type`, `void`, `onClick`.

**Q (CHYTÁK):** `onClick={fn()}` × `onClick={() => fn()}`?
**A:** **`onClick={fn()}`** = volá HNED při renderu, vrátí výsledek do onClick (typicky undefined). **`onClick={() => fn()}`** = předává FUNKCI, která se spustí AŽ při kliknutí. Drill: arrow wrapper povinný pokud chceš parametr.

**Q (CONCEPT):** Lifting state up?
**A:** **State žije v nejbližším společném předkovi** komponent, které ho potřebují. Předává se DOLŮ přes props. Komunikace zpět = **callback předaný dolů + volaný v dítěti**.

**Q (CHYTÁK):** Data tečou KAM v Reactu?
**A:** **SHORA DOLŮ** (unidirectional data flow). Rodič → dítě přes props. **NE od dětí k rodičům.** Komunikace zpět = callback předaný z rodiče dolů, volaný v dítěti. Klasický chyták komise!

**Q (CHYTÁK):** Immutable update v setState?
**A:** **NIKDY nemutuj.** `arr.push()`, `obj.x = ...` = stejná reference = React nepřekreslí. Vždy:
- Add: `setArr([...arr, novy])`
- Remove: `setArr(arr.filter(...))`
- Update: `setArr(arr.map(x => x.id === id ? {...x, prop: nove} : x))`

---

## DAT 23 — React hooks

**Q (TEORIE):** Co je hook a kdy přibyly?
**A:** **Funkce začínající `use`** poskytující funkcionálním komponentám schopnosti tříd: **stav, lifecycle, refs**. Před React 16.8 (únor 2019) jen třídy. Dnes **standard = funkcionální + hooks**. **Vlastní hooks** = composing built-in hooks pro reusable logiku (`useFetch`, `useLocalStorage`).

**Q (TEORIE):** 3 pravidla hooks?
**A:** **1)** Volat **JEN na top-level** komponenty (ne v `if`, `for`, vnořených funkcích). **2)** Volat **JEN z React komponent nebo vlastních hooks**. **3)** Pořadí volání **stejné každý render** (React identifikuje hooky podle pořadí, ne názvu). Lint plugin `react-hooks/rules-of-hooks` hlídá.

**Q (TEORIE):** Co je side effect?
**A:** **Operace mimo komponentu** — fetch z API, `document.title`, subscribe na event, `localStorage`, timer. Komponenta sama by měla být **pure** (props → JSX). Side effects patří do **`useEffect`** (mimo render fázi, po commit do DOMu).

**Q (FILL):** 4 základní hooky.
```tsx
const [x, setX] = ???(0);              // ???1 = lokální stav
???(() => { fetch(...); }, []);         // ???2 = side effect (mount, deps, cleanup)
const ref = ???<HTMLInputElement>(null); // ???3 = DOM ref / mutable bez re-render
const id = ???();                        // ???4 = stabilní unikátní ID
```
**A:** `useState`, `useEffect`, `useRef`, `useId`.

**Q (CONCEPT):** Dep array — kdy se useEffect spustí?
**A:** **`[]`** = JEN po mount. **`[x]`** = po mount + při změně `x`. **`[x, y]`** = po mount + při změně x NEBO y. **Bez deps** = po KAŽDÉM renderu (často infinite loop!).

**Q (CHYTÁK):** Infinite loop v useEffect?
**A:** **`setState` UVNITŘ useEffect s vadným dep arrayem** → každý render triggeruje effect → setState → re-render → ... React: `Maximum update depth exceeded`. Řešení: nedávej state co setState mění do deps NEBO podmiň update.

**Q (CONCEPT):** Cleanup function — kdy?
**A:** Vrácená funkce z useEffectu. Spouští se: **1) PŘED dalším spuštěním efektu** (deps change). **2) PŘI unmount** komponenty. Pro úklid timerů, listenerů, subscriptions, AbortControlleru.

**Q (CHYTÁK):** Fetch v useEffect — race condition?
**A:** Když user rychle naviguje pryč PŘED tím než fetch odpoví, fetch dokončí + zavolá setState na unmounted komponentě → React warning. Řešení: **AbortController** v cleanup (`return () => controller.abort()`) NEBO flag `cancelled`.

---

## DAT 24 — React Router

**Q (TEORIE):** Co je React Router a proč?
**A:** **Knihovna pro client-side routing v SPA** (npm `react-router-dom`). SPA = jedna HTML stránka, ale chceš **víc URL** (`/`, `/about`, `/users/5`). Router mění URL + obsah **bez page reloadu**, podporuje back button, sdílení URL. Aktuální verze 6+.

**Q (TEORIE):** SPA × MPA + jak routing funguje?
**A:** **SPA** = jeden HTML soubor, JS routing. **MPA** = každá URL = nová HTML stránka ze serveru. **SPA routing**: Router intercept klik na `<Link>`, použije **HTML5 History API** (`pushState`) k změně URL **bez HTTP requestu**, vymění obsah uvnitř `<Routes>`.

**Q (TEORIE):** Dynamic route — co a kdy?
**A:** **URL parametr v cestě**, např. `/users/:id`. V Route: `path="/users/:id"`. V komponentě: `const { id } = useParams()`. Použití: detail stránka pro entitu identifikovanou ID/slug. Pozor: `useParams` vrací **strings**, ne čísla.

**Q (FILL):** Setup.
```tsx
// main.tsx
import { ??? } from "react-router-dom";        // ???1 = root wrapper

<???>
    <App />
</???>
```
**A:** `BrowserRouter`.

**Q (FILL):** Routes.
```tsx
import { ???, ???, Link } from "react-router-dom";  // ???1 = switcher;  ???2 = jedna trasa

<???>
    <??? path="/" element={<Home />} />
    <??? path="/users/???" element={<UserDetail />} />  // ???3 = dynamic param
    <??? path="???" element={<NotFound />} />            // ???4 = wildcard
</???>
```
**A:** `Routes`, `Route`, `:id`, `*`. Drill: `:id` = dynamic, `*` = wildcard (poslední).

**Q (CHYTÁK):** `<Link to>` × `<a href>` × `<Route path>`?
**A:** **`<Link to="/about">`** = client-side navigace v Reactu (BEZ page reload). **`<a href="/about">`** = HTML anchor (REFRESHNE stránku, ztratíš state). **`<Route path="/about">`** = definice trasy. Drill: 3 podobné atributy, různé komponenty.

**Q (CHYTÁK):** `useParams` vrací co?
**A:** **Vrací objekt se STRINGS**, ne čísla! `const { id } = useParams();` → `id: string`. Pokud porovnáváš s `u.id: number`, **musíš konvertovat**: `Number(id)` nebo `parseInt(id, 10)`.

**Q (CONCEPT):** `useNavigate()` — kdy?
**A:** **Programatická navigace** z kódu, ne klikem. Po akci (uložení, smazání), conditional (`if (success) nav("/done")`), browser back (`nav(-1)`). `<Link>` pro klikatelné, `useNavigate` pro automatické.

**Q (TEORIE):** `<Navigate>` komponenta — deklarativní redirect?
**A:** **`<Navigate to="/login" replace />`** = komponenta-redirect používaný **v JSX, ne v handleru**. Typický use case: **auth guard** — pokud user není přihlášený, vrátíš `<Navigate to="/login" />` místo chráněného obsahu. `replace` flag = nahradí current entry v history (nelze back). Alternativa k `useNavigate` v render fázi.

---

## DAT 25 — Context + Reducer

**Q (TEORIE):** Co je správa stavu (state management) a kdy je složitá?
**A:** **Jak organizovat data sdílená napříč aplikací** (user, theme, cart). V malé aplikaci stačí `useState` + props. **Složitější:** víc komponent na různých úrovních potřebuje stejná data → **prop drilling** nebo state management knihovna (Redux, Zustand, Jotai, MobX) NEBO built-in `useContext` + `useReducer`.

**Q (TEORIE):** Co je useContext?
**A:** **React hook + Context API** pro **globální state v rámci subtree**. Místo prop drillingu komponenta kdekoli "naladí" data přes `useContext(MyContext)`. Tři kroky: **createContext** (vytvoř), **`<Provider value={...}>`** (poskytni), **useContext** (čti).

**Q (TEORIE):** Co je useReducer?
**A:** **React hook pro composite state s komplexními updaty**. Alternativa k `useState` když máš objekt/pole s 3+ různými update operacemi. **Reducer = čistá funkce `(state, action) => newState`**. Komponenta volá `dispatch(action)` místo víc setterů. Inspirováno Reduxem.

**Q (CONCEPT):** Co je prop drilling a co Context?
**A:** **Prop drilling** = předávat prop přes mezi-komponenty, které ji NEpoužívají, jen propouští dál. **Context** = "wifi" pro data napříč stromem. Komponenta kdekoli v subtree si data **naladí** přes `useContext`, bez propagace přes každého rodiče.

**Q (FILL):** 3 kroky Context.
```tsx
// 1) Vytvořit
const MyContext = ???<Type | null>(null);    // ???1 = funkce pro vytvoření

// 2) Provider obal
<MyContext.???value={data}>                   // ???2 = atribut na Provider
    <App />
</MyContext.???>

// 3) Číst v komponentě
const data = ???(MyContext);                  // ???3 = hook pro čtení
```
**A:** `createContext`, `Provider`, `useContext`.

**Q (CONCEPT):** Reducer — co a kdy?
**A:** **Funkce `(state, action) => newState`** = čistá funkce, vrací nový state. Místo `useState` + 5 různých setterů máš **1 dispatch** + **switch v reduceru**. Vhodné pro **composite state** (objekt/pole) s **víc update operacemi** centralizovanými.

**Q (FILL):** useReducer.
```tsx
type Action = { type: "ADD"; payload: Item } | { type: "DELETE"; id: number };

function reducer(state: Item[], action: Action): Item[] {
    ???(action.type) {                       // ???1 = klíčové slovo
        case "ADD": return [...state, action.payload];
        case "DELETE": return state.???(i => i.id !== action.id);   // ???2 = remove
        ???: return state;                    // ???3 = fallback
    }
}

const [items, ???] = useReducer(reducer, []);  // ???4 = setter
???({ type: "ADD", payload: novy });           // ???5 = vystřelit akci
```
**A:** `switch`, `filter`, `default`, `dispatch`, `dispatch`.

**Q (CONCEPT):** useContext + useReducer = mini Redux?
**A:** Kombinace: reducer drží state v rodiči (centralizované updaty), Context ho propaguje napříč subtree. Komponenty kdekoli mohou **číst state** + **dispatchnout akci** bez prop drillingu. **Bez Reduxu**, jen vestavěnými React hooks.

---

# 🔥 Hot chytáky napříč DAT — TOP 25 pro drill

1. **TRUNCATE = DDL** (SWI 12), ne DML
2. **JWT NENÍ šifrovaný** (SWI 20) — base64 + signature
3. **AES = symetrické**, RSA/ECC asymetrické, DH = výměna klíčů (SWI 7-8)
4. **TransPOZICE = pozice/pořadí**, SUBStituce = písmena (SWI 7)
5. **B-tree**, NE binary tree (SWI 10)
6. **Superklíč × kandidátní klíč** — kandidátní je MINIMÁLNÍ (SWI 11)
7. **React má JEN Virtual DOM**, NE Shadow DOM (SWI 18)
8. **Microtask** = `Promise.then`, NE `console.log` (synchronní)
9. **TypeScript = transpilace** na JS (SWI 24)
10. **BASIC = vysokoúrovňový**, NE nízkoúrovňový (SWI 24)
11. **Data tečou SHORA DOLŮ** v Reactu (DAT 22)
12. **`var` v JS = legacy** (function scope, hoisting), `const`/`let` modern
13. **C# atributy PascalCase** (`[Route]`, ne `[ROUTE]`)
14. **`Length` pro array, `Count` pro kolekce**
15. **`onClick={() => fn()}`**, ne `onClick={fn()}`
16. **`<Link to>` × `<a href>` × `<Route path>`** — 3 různé atributy
17. **`useParams` vrací STRINGS**, ne čísla — `Number(id)`
18. **HAVING pro filter skupin, WHERE pro filter řádků**
19. **`||` concat v SQLite**, `+` v MSSQL, `CONCAT()` v MySQL
20. **LIMIT × TOP** (SQLite vs MSSQL) + jiná pozice v syntaxi
21. **Specifická CSS property** > shorthand (`grid-template-areas` × `grid-template`)
22. **`flex: none` × `flex-grow: 0`** (pevné vs shrinkuje)
23. **CSS úhly s jednotkou** (`360deg`, ne `360`)
24. **`transition` × `animation`** (A→B vs keyframes)
25. **Lifecycle Android: Resume PŘED Pause** (SWI 25)

---

## 📅 Před zkouškou (pondělí ráno)

- Projeť všech 25 chytáků nahoře (3 minuty)
- Projeť "Co řeknu jako první" sekce z `_notes/swi/NN-*.md` (15 minut)
- **Spát do 22:30 v neděli.**

**Mantra:** *"U DAT doplním otazníky podle dokumentace. U SWI mluvím 15 min od úvodu po Q&A."*

To stačí. Zvládneš.
