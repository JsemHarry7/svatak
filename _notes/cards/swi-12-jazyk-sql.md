---
title: SWI 12 — Jazyk SQL
description: Historie, dialekty, 5 skupin (DDL/DML/DQL/DCL/TCL), constraints, view, transakce, SELECT pořadí
tags: [maturita, swi, sql, databaze, ddl, dml]
---

# Q: Co je SQL?
A: **Structured Query Language** — standardizovaný **deklarativní jazyk** pro relační databáze. Říkám CO chci, ne JAK to počítač najde.

# Q: Kdo vyvinul SQL a kdy?
A: **IBM v 70. letech.** Původně **SEQUEL** (Structured English QUEry Language), kvůli ochranné známce zkráceno na SQL. **První standard SQL-86 (ANSI)**.

# CLOZE: SQL = {{Structured Query Language}}. Původně {{SEQUEL}}, vyvinula firma {{IBM}}.

# Q: Co je deklarativní jazyk?
A: Píšu **CO chci**, ne **JAK to získat**. Optimalizátor DB plánuje cestu sám (full table scan, index seek, hash join). Příklad: `SELECT * FROM users WHERE age > 18`.

# Q: Co jsou dialekty SQL?
A: **Variant SQL** od konkrétního výrobce DBMS — vychází ze standardu, přidává vlastní funkce. Příklady: **T-SQL** (MS SQL), **PL/SQL** (Oracle), **pgSQL** (PostgreSQL).

# Q: 5 skupin SQL příkazů?
A: **DDL** (Data Definition — struktura), **DML** (Data Manipulation — data), **DQL** (Data Query — SELECT), **DCL** (Data Control — oprávnění), **TCL** (Transaction Control).

# CLOZE: DDL = {{Data Definition Language}}, DML = {{Data Manipulation Language}}, DCL = {{Data Control Language}}, TCL = {{Transaction Control Language}}.

# Q: 4 příkazy DDL?
A: **`CREATE`** (vytvoří objekt), **`ALTER`** (upraví), **`DROP`** (smaže objekt nenávratně), **`TRUNCATE`** (vyprázdní tabulku, strukturu nechá).

# Q: 3 hlavní příkazy DML?
A: **`INSERT`** (vloží data), **`UPDATE`** (aktualizuje), **`DELETE`** (smaže konkrétní řádky). Plus `SELECT` (někdy DQL).

# Q: 2 příkazy DCL?
A: **`GRANT`** (udělí oprávnění), **`REVOKE`** (odebere).

# Q: 3 hlavní příkazy TCL?
A: **`COMMIT`** (potvrdí transakci), **`ROLLBACK`** (vrátí zpět), **`SAVEPOINT`** (bod uvnitř transakce).

# MCQ: `CREATE TABLE` patří do skupiny...
- !DDL
- DML
- DCL
- TCL
> CREATE mění **strukturu** DB (přidá tabulku) → Data Definition Language.

# MCQ: `INSERT INTO ...` patří do skupiny...
- DDL
- !DML
- DCL
- TCL
> INSERT mění **data** v existující tabulce → Data Manipulation Language.

# Q: Rozdíl DROP × TRUNCATE × DELETE?
A: **DROP** = smaže celý objekt (i strukturu). **TRUNCATE** = vyprázdní tabulku, strukturu nechá, rychlé (DDL). **DELETE** = smaže řádky podle WHERE, pomalejší (DML, log per řádek, lze rollback).

# MCQ: Pro vyprázdnění tabulky bez možnosti rollback se nejlépe hodí...
- DROP TABLE
- DELETE FROM
- !TRUNCATE TABLE
- ALTER TABLE
> TRUNCATE smaže všechna data, strukturu nechá, rychlé (DDL operace). DELETE FROM bez WHERE dělá totéž, ale pomaleji.

# Q: Co je VIEW (pohled) v SQL?
A: **Virtuální tabulka** — uložený SELECT, tváří se jako tabulka. Není fyzické úložiště dat, ale "okno". **Použití:** bezpečnost (skryjeme citlivé sloupce), zjednodušení (komplexní JOINy schované), abstrakce.

# CODE: View pro bezpečnost
```sql
CREATE VIEW student_public AS
SELECT id, jmeno, prijmeni, trida_id
FROM student;
-- email + vek skryté

GRANT SELECT ON student_public TO public_user;
```

# Q: 5 databázových objektů?
A: **Tabulka** (Table), **Pohled** (View), **Index** (urychluje vyhledávání), **Sekvence** (generátor PK), **Stored procedure** (kód v DB). Plus **Trigger** (akce při události).

# Q: 6 typů constraints?
A: **`PRIMARY KEY`** (NOT NULL + UNIQUE), **`FOREIGN KEY`** (odkaz na jinou tabulku), **`UNIQUE`** (žádné duplicity), **`NOT NULL`** (nesmí být prázdný), **`CHECK`** (vlastní pravidlo), **`DEFAULT`** (výchozí hodnota).

# CODE: Constraints v CREATE TABLE
```sql
CREATE TABLE student (
    id INT PRIMARY KEY AUTO_INCREMENT,
    email VARCHAR(120) UNIQUE NOT NULL,
    vek INT CHECK (vek >= 0 AND vek <= 120),
    status VARCHAR(20) DEFAULT 'active',
    trida_id INT,
    FOREIGN KEY (trida_id) REFERENCES trida(id) ON DELETE SET NULL
);
```

# Q: Rozdíl PRIMARY KEY × UNIQUE?
A: **PK:** NOT NULL + UNIQUE, **právě jeden** per tabulka, identifikuje řádek. **UNIQUE:** žádné duplicity, **povolí NULL**, může být **víc UNIQUE** v tabulce (email + rodné_číslo).

# Q: Co znamená ON DELETE u FOREIGN KEY?
A: **Co se stane s potomky** při mazání rodiče. **CASCADE** (smaž potomky), **RESTRICT** (zakaž mazání), **SET NULL** (nastav FK na NULL), **SET DEFAULT**.

# Q: Pevné pořadí klauzulí SELECT?
A: **SELECT → FROM → JOIN → WHERE → GROUP BY → HAVING → ORDER BY → LIMIT**. Zapamatovat: výběr → zdroj → spoj → filtr → seskup → filtr skupin → seřaď → limit.

# Q: Co je transakce?
A: **Sekvence SQL operací, která se musí provést jako celek** (atomicita). Buď všechno projde (`COMMIT`), nebo nic (`ROLLBACK`).

# CODE: Transakce příklad
```sql
BEGIN TRANSACTION;
    UPDATE ucty SET zustatek = zustatek - 1000 WHERE id = 1;
    UPDATE ucty SET zustatek = zustatek + 1000 WHERE id = 2;
COMMIT;
-- nebo při chybě: ROLLBACK
```

# Q: Co je SAVEPOINT?
A: **Bod uvnitř transakce**, ke kterému lze **částečně rollbacknout**. Umožní vrátit část transakce, ne celou. `ROLLBACK TO savepoint_name`.

# Q: Co zaručují transakce (ACID)?
A: **A**tomicita (vše nebo nic), **C**onsistency (DB konzistentní), **I**solation (souběžné transakce se neruší), **D**urability (po commitu data trvale uložena).

# MCQ: Po `UPDATE student SET trida = '4.C'` bez WHERE...
- Vyhodí chybu
- !Přepíše třídu u **všech** studentů na '4.C'
- Aktualizuje jen prvního
- Vyžádá si potvrzení
> UPDATE bez WHERE = update všech řádků. Klasická katastrofa. VŽDY WHERE!

# Q: Co je auto_increment / sekvence?
A: **Generátor unikátních čísel** pro PK (typicky 1, 2, 3...). MySQL `AUTO_INCREMENT`, PostgreSQL `SERIAL` nebo sequence, SQLite `AUTOINCREMENT` nebo `INTEGER PRIMARY KEY`.

# Q: Co je stored procedure?
A: **Kus kódu uložený přímo v databázi**. Lze volat z aplikace. Výhody: optimalizace, bezpečnost (skryje SQL logiku), redukce síťové komunikace. Nevýhody: business logika v DB (těžší údržba, vendor lock-in).

# Q: Co je trigger?
A: **Akce, která se automaticky spustí** při události na tabulce (INSERT/UPDATE/DELETE). Příklad: audit log (každý update se zapíše do log tabulky), automatický timestamp.

# FREE: Popis kompletní DDL příkaz pro vytvoření tabulky `Objednavka` s constraints.
```sql
CREATE TABLE Objednavka (
    id INT PRIMARY KEY AUTO_INCREMENT,
    cislo_objednavky VARCHAR(20) UNIQUE NOT NULL,
    datum DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    cena DECIMAL(10, 2) CHECK (cena >= 0),
    zakaznik_id INT NOT NULL,
    stav VARCHAR(20) DEFAULT 'nova',
    FOREIGN KEY (zakaznik_id) REFERENCES Zakaznik(id) ON DELETE RESTRICT
);
```

# FREE: Vysvětli rozdíl mezi DDL, DML, DCL a TCL s konkrétními příkazy.
> **DDL** (Data Definition Language) mění **strukturu** DB — vytváří/upravuje/maže objekty (tabulky, views, indexy). Příkazy: `CREATE`, `ALTER`, `DROP`, `TRUNCATE`. **DML** (Data Manipulation Language) mění **data** v existujících tabulkách. Příkazy: `INSERT`, `UPDATE`, `DELETE` (někdy plus `SELECT` jako DQL). **DCL** (Data Control Language) řídí **oprávnění** — kdo smí co. Příkazy: `GRANT`, `REVOKE`. **TCL** (Transaction Control Language) řídí **transakce** — atomické operace. Příkazy: `BEGIN/COMMIT/ROLLBACK/SAVEPOINT`. Rozdělení dává smysl: DDL = architekt (staví), DML = uživatel (pracuje s daty), DCL = administrator (řídí přístup), TCL = developer (řeší konzistenci).
