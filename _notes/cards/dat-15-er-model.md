---
title: DAT 15 — ER model a návrh databáze
description: 3 úrovně návrhu, entity/atributy/relace, klíče (kandidátní/PK/FK/surrogate/složený), kardinalita 1:1/1:N/M:N, rekurzivní, slabá entita, ISA
tags: [maturita, dat, databaze, er-model, navrh]
---

# Q: Co je ER model?
A: **Entity-Relationship** model — **konceptuální návrh databáze**. Popisuje CO budeme ukládat (entity + atributy + vazby), neřeší JAK.

# Q: 3 úrovně návrhu databáze?
A: **Konceptuální** (ER diagram — entity + relace), **Logický** (tabulky + PK/FK), **Fyzický** (datové typy, indexy v konkrétním DBMS).

# CLOZE: Konceptuální → {{logický}} → {{fyzický}}. ER diagram → tabulky → CREATE TABLE.

# Q: Rozdíl entita × instance?
A: **Entita = TYP** (Student obecně, šablona). **Instance = KONKRÉTNÍ VÝSKYT** (Karel Novák). V ER diagramu kreslíš entity, ne instance.

# Q: Změna terminologie ER → logický?
A: Entita → **Tabulka**. Atribut → **Sloupec**. Relace → **Propojení přes FK**. Instance → **Řádek**.

# Q: Co je kandidátní klíč?
A: **Minimální množina atributů**, která jednoznačně identifikuje záznam. Tabulka může mít víc kandidátních (např. id i email).

# Q: Co je primární klíč (PK)?
A: **Vybraný kandidátní klíč**, **NOT NULL + UNIQUE**, **právě jeden** per tabulka. Slouží jako oficiální identifikátor řádku.

# Q: Co je cizí klíč (FK)?
A: **Odkaz na PK jiné tabulky.** Vytváří vazbu mezi tabulkami. Příklad: `Student.trida_id` ukazuje na `Trida.id`.

# Q: Rozdíl surrogate × přirozený klíč?
A: **Surrogate (umělý ID)** = AUTO_INCREMENT / GUID, stabilní, krátký, bez business významu. **Přirozený** = z reálného atributu (ISBN, rodné_číslo), může být citlivý nebo nestabilní. **Doporučené: surrogate** pro většinu případů.

# Q: Co je složený klíč?
A: **PK z více sloupců.** Typicky **vazební tabulka** pro N:M. Příklad: `Zapis(student_id, predmet_id)` — PK = oba sloupce dohromady.

# Q: 3 typy kardinality?
A: **1:1** (jeden ku jednomu — zaměstnanec ↔ občanka). **1:N** (jeden ku mnoha — oddělení ↔ zaměstnanci). **M:N** (mnoho ku mnoha — studenti ↔ předměty).

# Q: Jak implementovat 1:1 v relační DB?
A: **FK + UNIQUE** v jedné z tabulek. UNIQUE zaručí, že každý odkaz je jen jednou. Často se 1:1 **slučuje do jedné tabulky**, pokud není důvod oddělit.

# CODE: 1:1 implementace
```sql
CREATE TABLE Profil (
    id INT PRIMARY KEY,
    uzivatel_id INT NOT NULL UNIQUE,    -- UNIQUE = 1:1
    bio TEXT,
    FOREIGN KEY (uzivatel_id) REFERENCES Uzivatel(id)
);
```

# Q: Jak implementovat 1:N v relační DB?
A: **FK na straně N** (na straně "mnoho"). Příklad: třída ↔ studenti = FK `trida_id` v tabulce Student.

# CODE: 1:N implementace
```sql
CREATE TABLE Zamestnanec (
    id INT PRIMARY KEY,
    jmeno VARCHAR(50),
    oddeleni_id INT NOT NULL,   -- FK na straně N
    FOREIGN KEY (oddeleni_id) REFERENCES Oddeleni(id)
);
```

# Q: Jak implementovat M:N v relační DB?
A: **Relační DB neumí M:N přímo!** Rozkládá se na **2 vazby 1:N přes vazební tabulku** s složeným PK z 2 FK.

# CODE: M:N implementace
```sql
CREATE TABLE Zapis (
    student_id INT NOT NULL,
    predmet_id INT NOT NULL,
    datum_zapisu DATE,
    znamka VARCHAR(5),
    PRIMARY KEY (student_id, predmet_id),       -- složený PK
    FOREIGN KEY (student_id) REFERENCES Student(id),
    FOREIGN KEY (predmet_id) REFERENCES Predmet(id)
);
```

# Q: Klíčová věta pro obhajobu M:N?
A: ***"M:N nelze v relační databázi implementovat napřímo, vždy se rozkládá na dvě vazby 1:N přes vazební tabulku."***

# Q: Co je rekurzivní vazba?
A: **Entita má vazbu sama na sebe.** FK ve stejné tabulce ukazující na vlastní PK. Příklady: zaměstnanec ↔ manager, komentář ↔ odpověď, kategorie ↔ nadřazená.

# CODE: Rekurzivní vazba
```sql
CREATE TABLE Zamestnanec (
    id INT PRIMARY KEY,
    jmeno VARCHAR(50),
    manager_id INT NULL,    -- NULL pro CEO!
    FOREIGN KEY (manager_id) REFERENCES Zamestnanec(id)
);
```

# CLOZE: Rekurzivní FK musí být {{NULL}} pro nejvyšší úroveň (CEO nemá nadřízeného).

# Q: Co je slabá entita?
A: **Entita bez vlastního PK** — identifikuje se přes vztah k silné entitě. PK = složený z FK na rodiče + diskriminátor. Příklad: položka objednávky.

# CODE: Slabá entita
```sql
CREATE TABLE Polozka (
    objednavka_id INT NOT NULL,
    poradi INT NOT NULL,
    nazev VARCHAR(100),
    PRIMARY KEY (objednavka_id, poradi),    -- složený PK
    FOREIGN KEY (objednavka_id) REFERENCES Objednavka(id) ON DELETE CASCADE
);
```

# Q: Co je ISA vazba?
A: **Dědičnost na úrovni dat.** "Student ISA Osoba" (student JE druh osoby). 2 implementace: Single Table (vše v 1 tabulce s NULL) nebo Class Table (společné v jedné, specifické v druhé, 1:1).

# Q: ON DELETE — 4 akce?
A: **CASCADE** (smaž potomky), **RESTRICT** (zakaž mazání), **SET NULL** (nastav FK na NULL), **SET DEFAULT**.

# MCQ: U vztahu Objednávka → Položky by se měl použít...
- RESTRICT
- !CASCADE
- SET NULL
- SET DEFAULT
> Položka bez objednávky nedává smysl (slabá entita) → CASCADE smaže položky s objednávkou.

# MCQ: U vztahu Oddělení → Zaměstnanci by se měl použít...
- !RESTRICT (nesmazat, dokud existují zaměstnanci)
- CASCADE (smazat všechny zaměstnance)
- SET NULL
- TRUNCATE
> Smazání oddělení by nemělo automaticky smazat zaměstnance — buď je převést jinam (přes UPDATE), nebo zakázat smazání (RESTRICT).

# Q: Co je cyklus FK a jak se řeší?
A: Dvě tabulky odkazují na sebe navzájem (např. Oddeleni.leader_id ↔ Zamestnanec.oddeleni_id). **Problém:** nelze vložit první záznam, protože FK ještě neexistuje. **Řešení:** nullable FK + dvoufázový insert (insert s NULL → insert druhého → UPDATE prvního).

# Q: Postup návrhu DB (kroky)?
A: 1) Sběr požadavků. 2) Identifikace entit (podstatná jména). 3) Atributy + PK. 4) Vztahy + kardinalita (slovesa). 5) FK (1:N → na straně N). 6) Vazební tabulky pro N:M. 7) Normalizace (1NF/2NF/3NF). 8) ER diagram.

# Q: Pravidla pojmenování entit a vazeb?
A: **Entity** = podstatná jména v **jednotném čísle** (`Zamestnanec`, ne `Zamestnanci`). **Vazby** pojmenuj **slovesem** ("vlastní", "patří k", "obsahuje", "rezervuje").

# FREE: Popis postupný návrh DB pro školu s entitami Student, Třída, Učitel, Předmět.
> **Entity:** Student (id, jméno, email), Třída (id, kód, ročník), Učitel (id, jméno, aprobace), Předmět (id, název). **Vztahy:** Třída ↔ Student = **1:N** → FK `Student.trida_id`. Třída ↔ Učitel (třídní) = **1:1** → FK `Trida.tridni_id UNIQUE`. Student ↔ Předmět = **M:N** (s rolí "známka") → vazební tabulka **Zapis(student_id, predmet_id, znamka)**. Učitel ↔ Předmět = **M:N** (jeden učí víc předmětů, předmět může mít víc učitelů) → vazební **Vyuka(ucitel_id, predmet_id, rok)**. PK všude `id INT AUTO_INCREMENT`. Constraints: Student.email UNIQUE, Trida.kód UNIQUE. ON DELETE: Zapis CASCADE (smaž zápisy s předmětem nebo studentem), Student.trida_id SET NULL.

# FREE: Vyřeš cyklus FK mezi Oddělení a Zaměstnanec (oddělení má leadera, zaměstnanec patří do oddělení).
> **Schéma:** Oddeleni má `leader_id` (FK na Zamestnanec), Zamestnanec má `oddeleni_id` (FK na Oddeleni). **Cyklus.** Problém: nelze insertnout první záznam — Oddeleni potřebuje leader_id (Zaměstnanec neexistuje), Zamestnanec potřebuje oddeleni_id (Oddeleni neexistuje). **Řešení 1 — Nullable FK:** `leader_id INT NULL`. Postup: 1) Insert Oddeleni s leader_id=NULL. 2) Insert Zamestnanec s oddeleni_id=X. 3) UPDATE Oddeleni SET leader_id=Y WHERE id=X. **Řešení 2 — DEFERRABLE constraints** (PostgreSQL): FK se kontroluje až na konci transakce, lze vložit v libovolném pořadí. **Řešení 3 — Refactor:** místo `leader_id` v Oddeleni dej `je_leader: BOOLEAN` v Zamestnanec.
