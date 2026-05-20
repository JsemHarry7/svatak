# DAT 16 — SQL výběr a filtrování — Setup

## 📂 Co máš v této složce

```
dat16-sql-vyber/
├── exams.sqlite          ← databáze (5 tabulek, ~180 řádků)
├── build_db.py           ← script, který DB vytvořil (pro reference)
├── README.md             ← tenhle soubor
└── queries/              ← sem ukládáš SQL dotazy
    ├── 01-jednoduche.sql
    ├── 02-where.sql
    ├── 03-joiny.sql
    ├── 04-group-by.sql
    ├── 05-having.sql
    ├── 06-subquery-cte.sql
    └── final-zkouska.sql
```

## 🛠 Setup — DB Browser for SQLite

**1) Stáhni:** [sqlitebrowser.org/dl](https://sqlitebrowser.org/dl/) → Windows → 64-bit installer
**2) Nainstaluj** (default volby)
**3) Otevři aplikaci** → **File → Open Database...** → vyber `exams.sqlite`

## 🎮 Workflow při psaní dotazů

### V DB Browser:

1. **Database Structure** tab — koukni na schéma (tabulky + sloupce + typy)
2. **Browse Data** tab — prokliky se daty, sanity check
3. **Execute SQL** tab — sem píšeš SQL dotazy
4. **F5** nebo tlačítko ▶ = spusť dotaz, výsledek dole

### Kam ukládat dotazy:

**Do souborů `queries/0X-tema.sql`** v této složce. Pro každou mikroúlohu vlastní soubor. Když budu kontrolovat, otevřu ten soubor.

**Formát souboru:**

```sql
-- Mikroúloha 1.1: Jména všech studentů
SELECT jmeno FROM Student;

-- Mikroúloha 1.2: Studenti starší 18 let
SELECT jmeno, prijmeni, vek
FROM Student
WHERE vek > 18
ORDER BY prijmeni;

-- Mikroúloha 1.3: Top 5 nejstarších
SELECT jmeno, prijmeni, vek
FROM Student
ORDER BY vek DESC
LIMIT 5;
```

Komentáře `-- popis úlohy` před každým dotazem, ať vidím, co řešíš.

---

## 📊 Schéma databáze (přehled)

```
Trida (id PK, nazev UNIQUE, rocnik 1-4)
                │
                │ 1:N
                ▼
Student (id PK, jmeno, prijmeni, vek 14-25, email UNIQUE, trida_id FK)
                │
                │ 1:N
                ▼
Hodnoceni (id PK, student_id FK, predmet_id FK, ucitel_id FK, skore 0-100, datum)
                │            │              │
                │ N:1        │ N:1          │ N:1
                ▼            ▼              ▼
              Student     Predmet         Ucitel
                          (id PK,         (id PK, jmeno,
                           nazev,          prijmeni, aprobace,
                           kredity)        email UNIQUE)
```

### Data v DB

| Tabulka | Počet řádků |
|---|---|
| Trida | 5 (1.A, 1.B, 2.A, 3.A, 4.A) |
| Predmet | 6 (Matematika, Programování v C#, Databáze, Web Dev, Češtin, Angličtina) |
| Ucitel | 5 (Novak, Svobodová, Dvořák, Procházková, Černý) |
| Student | 18 (vč. 1 bez emailu, 1 bez třídy) |
| Hodnoceni | ~136 (skóre 45-100, datum 2026-01-15 až 2026-05-15) |

### Edge cases v datech (pro testy):

- **Adam Bílý** (id=13) — nemá žádné hodnocení (anti-join pattern test)
- **Barbora Růžová** (id=18) — nemá `trida_id` (LEFT JOIN test)
- **Adam Bílý** — nemá `email` (IS NULL test)

---

## 📚 Pořadí mikroúloh

| # | Téma | Cíl |
|---|---|---|
| 1 | Jednoduché SELECT + WHERE | FROM, WHERE, ORDER BY, LIMIT, DISTINCT |
| 2 | WHERE operátory | BETWEEN, IN, LIKE, IS NULL, AND/OR |
| 3 | JOINy | INNER JOIN, LEFT JOIN, vícenásobné JOIN |
| 4 | GROUP BY + agregace | COUNT, SUM, AVG, MIN, MAX |
| 5 | HAVING | filtrace skupin po agregaci |
| 6 | Subquery + CTE | poddotazy, WITH |
| **Final** | Leaked-style úloha | mix všeho |

Po každé mikroúloze pošli soubor `queries/0X-tema.sql` ke kontrole.

## 🐛 Užitečné SQL pro průzkum DB

```sql
-- Výpis všech tabulek
SELECT name FROM sqlite_master WHERE type='table';

-- Struktura konkrétní tabulky
SELECT sql FROM sqlite_master WHERE name='Student';

-- Rychlý overview prvních 5 řádků z tabulky
SELECT * FROM Student LIMIT 5;
SELECT * FROM Hodnoceni LIMIT 5;
```
