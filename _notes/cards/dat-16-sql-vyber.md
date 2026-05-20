---
title: DAT 16 — SQL výběr a filtrování
description: SELECT pevné pořadí, WHERE × HAVING, INNER × LEFT JOIN, GROUP BY + agregace, subquery, CTE, NULL pasti, SQLite specifika
tags: [maturita, dat, sql, sqlite, exam-leak]
---

# Q: Pevné syntaktické pořadí SELECT klauzulí?
A: **SELECT → FROM → JOIN → WHERE → GROUP BY → HAVING → ORDER BY → LIMIT**.

# Q: Logické pořadí provádění SELECT?
A: **FROM → JOIN → WHERE → GROUP BY → HAVING → SELECT → DISTINCT → ORDER BY → LIMIT**. Důsledek: alias ze SELECT nelze ve WHERE (ještě neexistuje), ale lze v ORDER BY.

# CLOZE: V WHERE {{nelze}} použít alias ze SELECT (ještě neexistuje), v ORDER BY {{lze}}.

# Q: Rozdíl WHERE × HAVING?
A: **WHERE filtruje řádky PŘED agregací**, **HAVING filtruje skupiny PO agregaci**. Agregační funkce (`AVG`, `COUNT`) lze jen v HAVING.

# CODE: WHERE + HAVING kombinace
```sql
SELECT trida_id, COUNT(*) AS pocet
FROM student
WHERE vek > 17                  -- nejdřív filtruj řádky
GROUP BY trida_id
HAVING COUNT(*) > 2;            -- pak filtruj skupiny
```

# MCQ: `WHERE COUNT(*) > 5` selže, proč?
- Špatná syntaxe
- !Agregační funkce nelze ve WHERE, GROUP BY ještě neproběhlo
- COUNT bere jen 1 argument
- Chybí FROM
> Logické pořadí: WHERE proběhne PŘED GROUP BY a agregací. Pro filtraci skupin použij HAVING.

# Q: Rozdíl INNER JOIN × LEFT JOIN?
A: **INNER JOIN** vrací jen řádky se **shodou v obou** tabulkách. **LEFT JOIN** vrací **vše z levé** + shody z pravé (NULL kde není shoda).

# Q: Co je CROSS JOIN?
A: **Kartézský součin** — každý řádek levé × každý řádek pravé. Vzniká i z `SELECT * FROM A, B` bez WHERE nebo `JOIN bez ON`. Pro velké tabulky katastrofa.

# Q: Co je SELF JOIN a k čemu?
A: **Tabulka sama se sebou** s 2 různými aliasy. Pro **rekurzivní vazby** (např. zaměstnanec ↔ manager).

# CODE: SELF JOIN
```sql
SELECT z.jmeno AS zamestnanec, m.jmeno AS manager
FROM zamestnanec z
LEFT JOIN zamestnanec m ON z.manager_id = m.id;
```

# Q: Proč `WHERE jmeno = NULL` nefunguje?
A: **NULL není rovno ničemu, ani sobě.** SQL používá 3-hodnotovou logiku (TRUE/FALSE/UNKNOWN), `NULL = NULL` je UNKNOWN. Musí se použít **`IS NULL`** / **`IS NOT NULL`**.

# CLOZE: Místo `WHERE email = NULL` použij `WHERE email {{IS NULL}}`.

# Q: 6 hlavních agregačních funkcí?
A: **`COUNT(*)`** (počet řádků), **`COUNT(sloupec)`** (ne-NULL), **`SUM`** (součet), **`AVG`** (průměr), **`MIN`** (minimum), **`MAX`** (maximum). Plus **`COUNT(DISTINCT)`** (unikátní hodnoty).

# Q: Rozdíl `COUNT(*)` × `COUNT(sloupec)`?
A: **`COUNT(*)`** = počet **VŠECH řádků** (i s NULL). **`COUNT(sloupec)`** = počet řádků, kde **sloupec NENÍ NULL**. Příklad: 100 studentů, 30 bez emailu → COUNT(*) = 100, COUNT(email) = 70.

# Q: Pravidlo pro GROUP BY?
A: **Vše v SELECT, co není agregační funkce, musí být v GROUP BY** (nebo funkčně závislé na PK skupiny).

# CODE: GROUP BY příklad
```sql
SELECT trida_id, COUNT(*) AS pocet
FROM student
GROUP BY trida_id;
```

# CODE: Top 5 pattern (GROUP BY + ORDER + LIMIT)
```sql
SELECT student_id, AVG(skore) AS prumer
FROM hodnoceni
GROUP BY student_id
ORDER BY prumer DESC
LIMIT 5;
```

# Q: Co dělá DISTINCT?
A: **Odstraní duplicitní řádky** z výsledku. Hodí se po JOIN, který může duplikovat (student s víc hodnoceními).

# Q: 5 hlavních operátorů ve WHERE?
A: **`=`/`!=`/`<`/`>`** (porovnání), **`BETWEEN x AND y`** (rozsah), **`IN (a,b,c)`** (seznam hodnot), **`LIKE '%vzor%'`** (vzor), **`IS NULL`/`IS NOT NULL`**.

# Q: LIKE vzory — co znamenají `%` a `_`?
A: **`%`** = libovolný počet znaků (i 0). **`_`** = právě jeden znak. Příklad: `LIKE 'A%'` = začíná na A, `LIKE 'A_a'` = A + 1 znak + a (Aba).

# CODE: LIKE příklady
```sql
WHERE prijmeni LIKE 'No%';      -- Novák, Novotný, Nováková
WHERE jmeno LIKE '%an%';        -- Anna, Daniela
WHERE kod LIKE 'A_a';           -- Aba, Ada (přesně 3 znaky)
```

# Q: Co je LIMIT a OFFSET?
A: **`LIMIT n`** = max n řádků. **`OFFSET m`** = přeskoč prvních m. Pro stránkování: `LIMIT 3 OFFSET 3` = 2. stránka po 3 záznamech (přeskoč 3, vezmi další 3).

# Q: Co je subquery (poddotaz)?
A: **Dotaz uvnitř jiného dotazu**. Skalární (vrací 1 hodnotu) — `WHERE vek > (SELECT AVG(vek) FROM ...)`. S IN — `WHERE id IN (SELECT ...)`. Korelovaný (závisí na vnějším, pomalejší).

# Q: Co je CTE (Common Table Expression)?
A: **Pojmenovaný dočasný výsledek** přes `WITH name AS (SELECT ...)`. Čitelnější než vnořené subquery. Lze víc CTE oddělených čárkami, lze i rekurzivní.

# CODE: CTE příklad
```sql
WITH prumery AS (
    SELECT student_id, AVG(skore) AS prumer
    FROM hodnoceni
    GROUP BY student_id
)
SELECT s.jmeno, p.prumer
FROM prumery p
JOIN student s ON s.id = p.student_id
ORDER BY p.prumer DESC
LIMIT 5;
```

# Q: Anti-join pattern (najdi to, co tam CHYBÍ)?
A: **LEFT JOIN + WHERE pravá_strana IS NULL.** Najde řádky levé tabulky, které **nemají shodu** v pravé.

# CODE: Studenti BEZ hodnocení
```sql
SELECT s.jmeno, s.prijmeni
FROM Student s
LEFT JOIN Hodnoceni h ON s.id = h.student_id
WHERE h.student_id IS NULL;     -- klíč: kde není shoda, NULL
```

# Q: SQLite specifika (pro exams.sqlite)?
A: **`AUTOINCREMENT`** (ne MySQL AUTO_INCREMENT), **`TEXT`** místo `VARCHAR`, **žádné `FULL OUTER JOIN`**, **datumy jako TEXT** v ISO formátu, **`||`** pro concat (ne `CONCAT()`), **`STRFTIME`** pro datumy.

# CODE: Concat v SQLite vs MySQL
```sql
-- SQLite/PostgreSQL
SELECT jmeno || ' ' || prijmeni FROM student;

-- MySQL
SELECT CONCAT(jmeno, ' ', prijmeni) FROM student;
```

# Q: Jak prohlédnout schéma `exams.sqlite` v DB Browser?
A: Tab **Database Structure** (přehled tabulek + sloupců). Plus SQL příkazy: `SELECT name FROM sqlite_master WHERE type='table'` (výpis tabulek), `.schema table_name` (struktura).

# Q: Co dělá `UPDATE/DELETE bez WHERE`?
A: **Přepíše/smaže VŠECHNY řádky tabulky** bez varování. Klasická katastrofa. **VŽDY používat WHERE!**

# Q: 5 skupin SQL příkazů (vzpomeň z SWI 12)?
A: **DDL** (CREATE/ALTER/DROP), **DML** (INSERT/UPDATE/DELETE), **DQL** (SELECT), **DCL** (GRANT/REVOKE), **TCL** (COMMIT/ROLLBACK).

# FREE: Napiš dotaz na top 3 studenty s nejvyšším průměrným skóre.
```sql
SELECT
    s.jmeno || ' ' || s.prijmeni AS student,
    ROUND(AVG(h.skore), 2) AS prumer
FROM Student s
JOIN Hodnoceni h ON s.id = h.student_id
GROUP BY s.id, s.jmeno, s.prijmeni
ORDER BY prumer DESC
LIMIT 3;
```

# FREE: Napiš dotaz, který najde předměty s víc než 5 hodnoceními a průměrem nad 70.
```sql
SELECT
    p.nazev,
    COUNT(h.skore) AS pocet_hodnoceni,
    ROUND(AVG(h.skore), 2) AS prumer
FROM Predmet p
JOIN Hodnoceni h ON p.id = h.predmet_id
GROUP BY p.id, p.nazev
HAVING COUNT(h.skore) > 5 AND AVG(h.skore) > 70
ORDER BY prumer DESC;
```

# FREE: Popis kompletní flow při práci s `exams.sqlite` v DB Browser.
> 1) Otevři DB Browser for SQLite. 2) File → Open Database → vyber `exams.sqlite`. 3) **Database Structure** tab — prohlédni si schéma (tabulky + sloupce + typy). 4) **Browse Data** tab — proklikni tabulky, podívej se na ukázková data (rychlý overview). 5) **Execute SQL** tab — piš dotazy. Postupně: jednoduché `SELECT * FROM tabulka LIMIT 5` pro sanity check, pak komplexnější. 6) **Spusť dotaz** (F5 nebo Execute) → výsledek dole. 7) Při chybě syntaxe → DB Browser zobrazí chybovou hlášku. 8) **Pro každý úkol** zadání: 1× zkus jednoduchý dotaz, ověř výstup, pak refine pro přesné zadání. 9) Po dokončení **netřeba commit** (READ-ONLY pro SELECTy).
