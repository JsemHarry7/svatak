---
title: SWI 11 — Normalizace databáze
description: 3 anomálie, funkční závislost, 1NF/2NF/3NF/BCNF, denormalizace, OLTP/OLAP
tags: [maturita, swi, databaze, normalizace]
---

# Q: Co je normalizace databáze?
A: **Postup návrhu relační DB**, který omezuje redundanci (duplicitní data) a chrání před anomáliemi při práci s daty. Cíl: každá informace existuje **přesně na jednom místě**.

# Q: 3 anomálie při nenormalizované DB?
A: **Update** (musíš měnit info na víc místech), **insert** (nelze vložit jednu věc bez druhé), **delete** (smazáním řádku omylem smažeš i info, kterou chceš zachovat).

# CLOZE: Anomálie: {{update}} (víc míst), {{insert}} (závislost), {{delete}} (ztráta info).

# Q: Co je 1NF?
A: **První normální forma** — každý sloupec obsahuje **jednu atomickou hodnotu** (ne seznam, ne víc věcí v jedné buňce).

# MCQ: `telefony = "123, 456, 789"` v jednom sloupci porušuje...
- 2NF
- 3NF
- !1NF
- BCNF
> Více hodnot v jedné buňce = porušení atomicity = 1NF.

# Q: Jak opravit porušení 1NF u sloupce s víc telefony?
A: Vytáhnout do **samostatné tabulky** `Telefon(id, person_id, cislo)` s FK na osobu — vazba 1:N.

# Q: Co je 2NF?
A: **Druhá normální forma** = 1NF + **každý neklíčový atribut závisí na CELÉM kandidátním klíči**. Řeší **částečné závislosti**, typicky u **složených klíčů**.

# CODE: Porušení 2NF příklad
```
OrderItem(order_id, product_id, product_name, quantity)
PK = (order_id, product_id)

product_name závisí JEN na product_id, ne na celém PK
→ porušení 2NF

Oprava:
Product(product_id, product_name)
OrderItem(order_id, product_id, quantity)
```

# Q: Co je 3NF?
A: **Třetí normální forma** = 2NF + **neklíčové atributy nezávisí na jiných neklíčových** (žádná tranzitivní závislost).

# Q: Co je tranzitivní závislost?
A: `PK → A → B`, kde **A není klíč**. Pak PK určuje B přes mezičlánek A. V jedné tabulce to nechceme — info o A by se duplikovala v každém řádku se stejným A.

# CODE: Porušení 3NF
```
Employee(employee_id, jmeno, dept_id, dept_name)

dept_name závisí na dept_id (ne na employee_id)
→ tranzitivní závislost employee_id → dept_id → dept_name
→ porušení 3NF

Oprava:
Department(dept_id, dept_name)
Employee(employee_id, jmeno, dept_id)
```

# Q: Co je BCNF?
A: **Boyce-Coddova normální forma** — **silnější než 3NF**. Pro každou netriviální FD `X → Y` musí být **X superklíč**. Řeší vzácnější situace s víc kandidátními klíči, kdy 3NF ještě pustí závislost, BCNF už ne.

# Q: Co je funkční závislost (FD)?
A: **`X → Y`** = "hodnota X jednoznačně určuje hodnotu Y". Příklad: `student_id → jmeno` (jeden student má jedno jméno).

# Q: Co je superklíč?
A: **Jakákoli množina atributů**, která jednoznačně určuje řádek. Kandidátní klíč je **minimální** superklíč (nelze odebrat atribut bez ztráty jedinečnosti).

# Q: Co je denormalizace?
A: **Schválné porušení normalizace** ve prospěch **rychlosti čtení**. Typicky v OLAP (reporty) — data se duplikují, aby se vyhnulo JOINům. Trade-off rychlost × konzistence.

# Q: Co je rozdíl mezi OLTP a OLAP?
A: **OLTP** (Online Transaction Processing) = produkční aplikace, hodně zápisů, **více normalizováno**. **OLAP** (Online Analytical Processing) = analytika, hodně čtení, **často denormalizováno** (méně JOINů, rychlejší).

# MCQ: Pro e-shop (vkládání objednávek, úpravy stavu) by se hodila...
- !Více normalizovaná OLTP databáze
- Denormalizovaná OLAP databáze
- 0NF
- BCNF povinné
> E-shop = hodně zápisů + konzistence kritická → OLTP s normalizací (3NF default).

# MCQ: Pro reporting dashboard (rychlé čtení agregací) by se hodilo...
- Plně normalizovaná DB
- !Denormalizovaná OLAP databáze
- BCNF + 4NF
- NoSQL graph
> Reporty = hodně čtení, agregace, denormalizace šetří JOINy = rychlejší.

# Q: Trade-off normalizace vs denormalizace?
A: **Normalizace:** víc tabulek, méně duplicity, vyšší konzistence, **víc JOINů** (pomalejší čtení). **Denormalizace:** méně tabulek, **duplicita**, riziko nekonzistence, **rychlejší čtení** (méně JOINů).

# Q: V jakém pořadí jdou normální formy?
A: **0NF** (nenormalizováno) → **1NF** (atomicita) → **2NF** (celý PK) → **3NF** (bez tranzitivních) → **BCNF** (superklíč). Vyšší normální formy (4NF, 5NF, 6NF) v praxi řídce.

# CLOZE: V OLTP DB je typický cíl {{3NF}}. V OLAP se schválně {{denormalizuje}} pro výkon čtení.

# Q: Klíčová slova:
A: **PK** (primární klíč), **kandidátní klíč** (minimální množina určující řádek), **složený klíč** (PK z víc sloupců), **neklíčový atribut** (není součástí žádného kandidátního klíče), **funkční závislost** (`X → Y`), **tranzitivní závislost** (`PK → A → B`), **superklíč** (množina určující řádek).

# FREE: Popis postupný proces normalizace tabulky `OrderItem(order_id, product_id, product_name, quantity, customer_name)` od 0NF do 3NF.
> **0NF:** všechno v jedné tabulce, customer_name a product_name spolu s detaily objednávky. **1NF kontrola:** atomické hodnoty? Ano (předpokládám). **2NF (PK = (order_id, product_id)):** product_name závisí jen na product_id → vytáhnout: `Product(product_id, product_name)`. customer_name závisí jen na order_id → vytáhnout: `Order(order_id, customer_name)`. **OrderItem(order_id, product_id, quantity)** zůstává. **3NF kontrola:** customer_name v Order — pokud existuje samostatná tabulka Customer, mohla by to být tranzitivní (`order_id → customer_id → customer_name`), pak vytáhnout: `Customer(customer_id, customer_name)`, `Order(order_id, customer_id)`. **Výsledek 3NF:** Customer, Order, Product, OrderItem — 4 tabulky s vazbami.

# FREE: Vysvětli, proč v praxi v OLAP schválně porušujeme 3NF.
> OLAP slouží pro **analytiku a reporty** — typicky agregace přes miliony řádků (`SELECT SUM(...) FROM ... JOIN ... JOIN ...`). Plně normalizovaná schéma vyžaduje **mnoho JOINů**, které u velkých dat zpomalí dotaz na sekundy nebo minuty. Denormalizací (např. **star schema** — jedna fact tabulka s replikovanými dimensions) eliminujeme JOINy → dotaz běží rychle (sub-second). **Riziko**: pokud se zdrojová data změní, denormalizovaná kopie musí být aktualizována (ETL pipeline). Konzistence se zachovává tím, že OLAP **není primární zdroj pravdy** — data se pravidelně načítají z OLTP.
