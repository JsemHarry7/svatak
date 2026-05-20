---
title: SWI 10 — Databáze
description: RDBMS, relační model, datové modelování (ER/logický/fyzický), vazby (1:1/1:N/M:N), integrita, ACID, indexy, NoSQL
tags: [maturita, swi, databaze, sql, nosql, acid]
---

# Q: Co je databáze?
A: **Organizovaná kolekce dat** + systém pro jejich uchovávání a získávání. **DBMS** (Database Management System) je software, který databázi spravuje.

# Q: Co je relační databáze?
A: Databáze založená na **relačním modelu** (Edgar F. Codd, 1970), kde data jsou v **tabulkách (relacích)**. Pevné schéma, komunikace přes **SQL**. Zástupci: MySQL, PostgreSQL, MS SQL Server, SQLite, Oracle.

# CLOZE: Relační model definoval {{Edgar F. Codd}} v roce {{1970}}.

# Q: 3 fáze datového modelování?
A: **A. Konceptuální** (ER diagram — CO v databázi bude, entity + relace). **B. Logický** (převod entit na tabulky, PK/FK, řešení M:N). **C. Fyzický** (implementace pro konkrétní DBMS, typy INT/VARCHAR, indexy).

# Q: Co je entita v ER modelu?
A: **Objekt reálného světa**, o kterém uchováváme informace (Student, Auto, Kurz). **Entitní typ** = šablona ("Člověk"). **Instance entity** = konkrétní objekt ("Franta Novák, 18 let").

# Q: Co je primární klíč (PK)?
A: **Unikátní identifikátor řádku** v tabulce. Nemůže být NULL ani duplicitní. Typicky `INT AUTO_INCREMENT` nebo `GUID`. **Má automatický index** → rychlé hledání podle PK.

# Q: Co je cizí klíč (FK)?
A: **Odkaz na PK jiné tabulky.** Vytváří vztah mezi tabulkami. Příklad: `Student.trida_id` (FK) ukazuje na `Trida.id` (PK). Zajišťuje **referenční integritu**.

# Q: Jak se realizuje vztah 1:N v relační DB?
A: **FK je na straně "N"** (na straně "mnoho"). Příklad: třída má mnoho studentů → tabulka `Student` má sloupec `trida_id` (FK na `Trida.id`).

# Q: Jak se realizuje vztah M:N v relační DB?
A: **Relační DB neumí M:N přímo uložit.** Vytvoří se **vazební (asociační) tabulka** s dvojicí FK. Rozpad na dvě vazby 1:N. Příklad: `HRAJE_V(id_herec FK, id_film FK, role)`.

# CODE: M:N vazební tabulka
```sql
HEREC (id PK, jmeno)
FILM (id PK, nazev)
HRAJE_V (id_herec FK, id_film FK, role)   -- vazební tabulka
```

# MCQ: Pro vztah M:N mezi Studenty a Kurzy potřebuješ...
- Jen FK v tabulce Student
- Jen FK v tabulce Kurz
- !Vazební tabulku (Student_Kurz)
- Speciální datový typ
> Relační DB neumí M:N přímo. Vazební tabulka rozdělí M:N na 2× 1:N.

# Q: Co je rekurzivní vazba?
A: Vztah **entity samé k sobě**. Příklad: **Zaměstnanec a jeho nadřízený** (oba v tabulce `Zamestnanec`, FK `manager_id` ukazuje na `Zamestnanec.id`).

# Q: Co je IS-A vazba (generalizace)?
A: **Dědičnost na úrovni dat.** Příklad: `Vozidlo` (obecná) + podtypy `Auto` a `Motorka`. "Auto IS A Vozidlo". V DB: buď 1 tabulka s NULL sloupci, nebo víc tabulek se společným ID.

# Q: 3 typy integrity dat?
A: **A. Doménová** (jaké hodnoty mohou být ve sloupci — datové typy + constraints). **B. Referenční** (vztahy mezi tabulkami přes FK). **C. Transakční / logická** (celá operace nebo nic — ACID).

# Q: 4 nejběžnější constraints v SQL?
A: **NOT NULL** (hodnota musí existovat), **UNIQUE** (žádné duplicity), **CHECK** (vlastní pravidlo, např. `vek >= 0`), **DEFAULT** (výchozí hodnota při INSERT bez specifikace).

# Q: 4 akce při mazání rodiče (referenční integrita)?
A: **RESTRICT / NO ACTION** (zakáže smazání, dokud existují potomci). **CASCADE** (smaže rodiče + automaticky všechny potomky). **SET NULL** (smaže rodiče, potomkům nastaví FK na NULL). **SET DEFAULT**.

# CLOZE: ON DELETE {{CASCADE}} = smaže potomky s rodičem. ON DELETE {{RESTRICT}} = zakáže smazání rodiče, dokud existují potomci.

# Q: Co je transakce?
A: **Sekvence operací, která se musí provést jako celek** — buď vše, nebo nic. Klasický příklad: převod peněz mezi účty (`UPDATE A -= 1000; UPDATE B += 1000`). Pokud cokoli selže, **ROLLBACK** vrátí stav, jinak **COMMIT** potvrdí.

# CODE: SQL transakce
```sql
BEGIN TRANSACTION;
    UPDATE ucty SET zustatek = zustatek - 1000 WHERE id = A;
    UPDATE ucty SET zustatek = zustatek + 1000 WHERE id = B;
COMMIT;  -- nebo ROLLBACK při chybě
```

# Q: Co znamená ACID?
A: 4 vlastnosti transakcí v RDBMS. **A — Atomicity** (vše nebo nic), **C — Consistency** (DB zůstane konzistentní, constraints splněny), **I — Isolation** (souběžné transakce se neruší), **D — Durability** (po commitu jsou data trvale uložena, i při výpadku).

# CLOZE: A v ACID = {{Atomicity}}, C = {{Consistency}}, I = {{Isolation}}, D = {{Durability}}.

# MCQ: Po výpadku napájení musí být commitnutá transakce stále v DB. Které ACID písmeno to garantuje?
- Atomicity
- Consistency
- Isolation
- !Durability
> Durability = trvanlivost. Data zapsaná po COMMIT zůstanou i při výpadku.

# MCQ: Dvě souběžné transakce nesmí se navzájem ovlivnit. Které ACID písmeno?
- Atomicity
- Consistency
- !Isolation
- Durability
> Isolation = izolace. Každá transakce vidí stav, jako by běžela sama.

# Q: Co je index v databázi?
A: **Pomocná datová struktura, která urychluje vyhledávání** v tabulce. Bez indexu = **full table scan** O(n). S indexem typicky **O(log n)** (B-tree).

# Q: Trade-off při použití indexu?
A: **+** Rychlejší SELECT/WHERE/JOIN/ORDER BY. **−** Pomalejší INSERT/UPDATE/DELETE (index musí být aktualizován), větší velikost DB.

# CODE: Vytvoření indexu
```sql
CREATE INDEX idx_users_email ON users(email);
-- nyní SELECT ... WHERE email = '...' je rychlý
```

# Q: 3 typy indexů?
A: **B-tree** (default, rovnost i range queries), **Hash** (jen pro rovnost, velmi rychlý), **Full-text** (vyhledávání v textu, např. `LIKE '%word%'`).

# Q: Co je NoSQL?
A: **Not Only SQL.** Alternativa pro **velká množství dat bez pevné struktury** (Big Data). **Schemaless**, lepší škálovatelnost (horizontal), horší podpora joinů. Často **rezignuje na ACID** ve prospěch eventual consistency.

# Q: 4 typy NoSQL databází + příklad každého?
A: **1) Dokumentové** (JSON) — **MongoDB** (e-shopy, CMS). **2) Klíč-hodnota** — **Redis** (cache, session). **3) Grafové** — **Neo4j** (sociální sítě, knowledge graphs). **4) Sloupcové** — **Cassandra** (Big Data analytika).

# MCQ: Pro cache session tokenu uživatele bys použil...
- MySQL
- MongoDB
- !Redis
- Cassandra
> Redis = klíč-hodnota, extrémně rychlý in-memory store. Ideální pro cache.

# MCQ: Pro sociální síť, kde modeluješ "kdo zná koho", se hodí...
- MySQL s vazební tabulkou
- MongoDB
- !Neo4j (grafová DB)
- Redis
> Grafová DB modeluje vztahy nativně. Dotaz "najdi všechny lidi 2 stupně od X" je v grafové DB rychlý, v relační (s rekurzivními JOIN) pomalý.

# Q: Kdy použít SQL vs NoSQL?
A: **SQL** = strukturovaná data, komplexní vazby přes JOIN, **ACID transakce kritické** (banky, finance, ERP). **NoSQL** = velký objem, různorodá data, **eventual consistency OK**, potřeba horizontal scaling (Big Data, mikroslužby).

# FREE: Popis ER diagram pro školní systém s entitami Student, Třída, Kurz.
> **Entity:** Student (id, jméno, email), Třída (id, kód, ročník), Kurz (id, název, kredity). **Vazby:** Třída ↔ Student = **1:N** (jedna třída má víc studentů, student je v 1 třídě) → FK `Student.trida_id`. Student ↔ Kurz = **M:N** (student je v víc kurzech, kurz má víc studentů) → **vazební tabulka** `ZAPIS(id_student FK, id_kurz FK, datum_zapisu)`. Primární klíče: každá tabulka má `id` (INT auto-increment). Constraints: `Student.email UNIQUE`, `Student.jmeno NOT NULL`, `Trida.kód UNIQUE`.

# FREE: Popis ACID transakci na příkladu převodu peněz s konkrétními problémy bez ACID.
> Bez ACID hrozí: **A bez:** první UPDATE projde, druhý spadne kvůli chybě → 1000 Kč zmizí z účtu A, ale nepřibude na B. Peníze ztracené. **C bez:** během transakce může jiný dotaz vidět nekonzistentní stav (A odečteno, B ne). Reporty vrátí špatný total. **I bez:** dvě souběžné transakce čtou stejný stav (1000 Kč na A), obě odečtou → A = -1000 místo 0. **D bez:** transakce úspěšně commitne, ale výpadek napájení před zápisem na disk → data ztracená. ACID **zajistí**: pokud cokoli selže, ROLLBACK vrátí původní stav. Souběžné transakce vidí izolované stavy. Po COMMIT data přežijí jakýkoli pád.
