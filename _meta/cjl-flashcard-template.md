# Šablona karetní sady pro jednu knihu (ČJL maturita)

Závazná struktura pro generování per-knihových flashcard decků do appky **rep**.
Formát karet viz `rep-card-format.md` v rootu repa.

**Cíl:** 12 karet standardně (13 u dramat s velkým obsazením postav). Strop 15.
Žádný bod 13bodové struktury ústní zkoušky ani uživatelův požadavek nesmí vypadnout.

**Zdroje obsahu (pořadí důvěry):**
1. `_cjl/rozbory/NN_*.md` — hotový rozbor titulu (primární)
2. `_cjl/rozbory/00_casova_osa_literatury.md` — dobový kontext, současníci (1:1 s rozborem)
3. `knihy-NENAHRAVAT-NA-GITHUB/NN_*.pdf` — plný text knihy → JEN pro výběr úryvku ke kartě 12
4. Obecné znalosti pouze tam, kde rozbor mlčí — označit, neimprovizovat data ani citace

---

## Struktura sady — 12 karet

### Blok A — Zasazení (karty 1–3)

**Karta 1 — Q: Zařazení díla**
Autor, rok vydání, literární období/směr, **literární druh + žánr se zdůvodněním**.
→ kryje body 5 (druh/žánr) a 13 (lit. kontext) zkoušky.

**Karta 2 — Q: Autor — 3 klíčové informace**
Tři věci, co o autorovi vědět: kdo byl (národnost, profese), životní osud, charakteristika tvorby.
→ uživatelův požadavek „informace o autorovi (3)".

**Karta 3 — Q: Kontext autorovy tvorby**
Kde dílo stojí v autorově dráze (prvotina / vrcholné / pozdní / posmrtné), zajímavost vzniku, inspirační zdroj (předloha, námět).
→ bod 12 zkoušky.

### Blok B — Literární kontext (karty 4–6)

**Karta 4 — Q: Dobový kontext**
Literárně-historické období + **3 znaky směru** + stručná společenská situace.
→ uživatelův požadavek „dobový kontext", bod 13 zkoušky.

**Karta 5 — Q: Současníci (autoři ze stejné doby)**
**2–3 autoři** téhož období/směru + **1 dílo** každého + **stručný děj** (1 věta).
Bere se 1:1 ze sekce „Další zástupci" v rozboru (= sync s časovou osou).
→ uživatelův požadavek „autoři ze stejné doby + dílo + stručný děj (2–3)".

**Karta 6 — Q: Další díla autora**
**2 další díla** autora + jejich **stručný děj** (pokud děj mají; u poezie ladění/téma).
→ uživatelův požadavek „další díla autora + děj (2)".

### Blok C — Rozbor díla (karty 7–11)

**Karta 7 — FREE: Děj a časoprostor**
Převyprávění děje jako u zkoušky (4–6 vět) + **kdy a kde** se odehrává (čas + místo).
→ uživatelův požadavek „děj", body 3 (časoprostor) a 14 (obsah) zkoušky.

**Karta 8 — Q: Téma a motivy**
Hlavní téma + **3–4 motivy**.
→ uživatelovy požadavky „téma", „motiv", bod 2 zkoušky.

**Karta 9 — Q: Kompoziční výstavba**
Způsob řazení děje (chronologický / retrospektivní / rámcový / paralelní), členění díla (kapitoly / jednání / zpěvy). **U poezie navíc veršová výstavba** — druh rýmu, strofika.
→ body 4 a 9 zkoušky.

**Karta 10 — Q: Vypravěč a typy promluv**
Typ vypravěče (er-forma / ich-forma; vševědoucí / personální / neosobní) nebo lyrický subjekt; formy promluv (přímá řeč, monolog, dialog, stranou…).
→ body 6 a 8 zkoušky.

**Karta 11 — Q: Postavy**
Hlavní postavy + stručná charakteristika každé.
→ bod 7 zkoušky.
*(U dramat s velkým obsazením — Revizor, Lakomec, Švejk — lze rozdělit na hlavní + vedlejší = karta navíc, sada pak 13 karet.)*

### Blok D — Jazyk a úryvek (karty 12–13)

**Karta 12 — Q: Jazykové prostředky a tropy/figury díla**
Typické jazykové prostředky díla + tropy/figury, které se v něm vyskytují, s krátkými příklady.
→ body 10 a 11 zkoušky (teoretická příprava).

**Karta 13 — FREE: Úryvek — najdi tropy a figury**
Konkrétní **úryvek z knihy** (vybraný z PDF, 4–8 řádků, hutný na prostředky) + úkol: *„Najdi a pojmenuj tropy, figury a jazykové prostředky. Cituj konkrétní příklady."* Modelová odpověď je vyjmenuje.
→ uživatelův požadavek „úryvek → tropy a figury", body 10–11 zkoušky.
*(Výjimka z limitu 300 znaků — úryvková karta je delší ze své podstaty.)*

---

## Číslování a typy karet — souhrn

| # | Karta | Typ | Bod zkoušky / požadavek |
|---|-------|-----|--------------------------|
| 1 | Zařazení díla | Q/A | 5, 13 |
| 2 | Autor — 3 info | Q/A | autor |
| 3 | Kontext autorovy tvorby | Q/A | 12 |
| 4 | Dobový kontext | Q/A | dobový kontext, 13 |
| 5 | Současníci | Q/A | autoři ze stejné doby |
| 6 | Další díla autora | Q/A | další díla |
| 7 | Děj a časoprostor | FREE | děj, 3, 14 |
| 8 | Téma a motivy | Q/A | téma, motiv, 2 |
| 9 | Kompozice (+ verš u poezie) | Q/A | 4, 9 |
| 10 | Vypravěč a promluvy | Q/A | 6, 8 |
| 11 | Postavy | Q/A | 7 |
| 12 | Jazykové prostředky a tropy | Q/A | 10, 11 |
| 13 | Úryvek — najdi tropy/figury | FREE | úryvek, 10, 11 |

Standardně **13 karet** (čistá, žádný bod nevynechán). Sloučením 10+11 lze stáhnout na 12.
Strop 15 — rezerva pro drama s velkým obsazením (rozdělení karty Postavy).

---

## Pravidla generování

1. **Frontmatter:** `title` = název knihy + autor; `tags: [cjl, maturita, <autor-slug>, <obdobi-slug>]`.
2. **Soubor:** `_cjl/flashcards/NN-nazev.md` (stejné NN jako rozbor).
3. **Jazyk:** česky.
4. **Anti-halucinace:** děj, postavy, citace striktně z rozboru a PDF. Když rozbor mlčí → nevymýšlet. Roky a názvy ověřit proti rozboru.
5. **Úryvek (karta 13):** vybrat z PDF pasáž bohatou na tropy/figury (oxymóron, metafora, personifikace, anafora…). Krátká, ať je rozbor zvládnutelný. U dramatu klíčová replika/monolog, u poezie sloka.
6. **Konzistence se synchronizovanými rozbory:** karta 5 (současníci) = přesně titíž 3 autoři jako sekce „Další zástupci" rozboru.
