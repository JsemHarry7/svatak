# Progress tracker

Status legenda: ❌ neumím / 🟡 částečně / ✅ pohoda
Sebehodnocení: 1–10 (1 = vidím poprvé, 10 = vysvětlím komukoli)

---

## SWI (25 témat)

| # | Téma | Sebehodn. (před) | Sebehodn. (po) | Status | Poslední revize | Poznámka |
|---|------|---|---|---|---|---|
| 1 | Diagramy UML | 4 |  | ❌ |  |  |
| 2 | Algoritmus | 3 | 8 | ✅ | 2026-05-10 (3× drill) | Re-drill heslovitý formát s preemptivním "algoritmus × kód" rozdílem. **Vlastnosti: 6/7** (efektivita, hromadnost, determinovanost, správnost, vstup/výstup, výslednost). **Chybí KONEČNOST** — drill 7. vlastnost! Zápis kompletně se symboly flowchartu (oval start, kosočtverec rozhodnutí, obdélník akce, kosodelník vstup). Big O 5 tříd + bubble sort příklad. Mini B: vnořené cykly = O(n²) ✅. |
| 3 | Reprezentace dat | 4 |  | ❌ |  |  |
| 4 | Datové typy, proměnné | 5 | 8 | ✅ | 2026-05-09 (re-drill) | Re-drill heslovitý formát zavřel všech 7 bodů Popisu solidně. Imutabilita s string+GC příkladem ✅, lístek/dům aplikovaný ✅, všechny 3 druhy přetypování s C# příklady ✅. **Vlastní rozšíření metafory:** "smart home, dva přístupy, b zapne topení → a vidí" — silnější než lístek/dům, použít u zkoušky. Drobnost: funkční scope vs. třídní scope splývaly. |
| 5 | Návrhové vzory | 2 |  | ❌ |  |  |
| 6 | Chyby, testování, ladění | 3 |  | ❌ |  |  |
| 7 | Šifrování a kódování | 5 |  | ❌ |  |  |
| 8 | Kryptosystémy | 4 |  | ❌ |  |  |
| 9 | OOP | 5 | 6 | 🟡 | 2026-05-10 | Heslovitý lístek pokryl 9/10 subtémat. Solidní: dědičnost, abstrakce, interface, generika, statika (po self-correction). Q4 (abstraktní × interface "JE × UMÍ") perfektně. **2 reverzní chyby:** (1) field × property zobrazil OPAČNĚ — obě platí pro instanci, rozdíl je get/set akcesory + validace; (2) **POLYMORFISMUS REVERSED** — řekl "override při překladu, overloading za běhu", správně je opačně (overloading = compile-time, override = runtime). Drill: property syntax recitovat, mantra "overloading při překladu / override za běhu", vyjmenovat modifikátory (private/protected/public/internal). Re-drill ~2026-05-14. |
| 10 | Databáze | 2 |  | ❌ |  |  |
| 11 | Normalizace databáze | 3 |  | ❌ |  |  |
| 12 | Jazyk SQL | 5 |  | ❌ |  |  |
| 13 | Internet | 1 |  | ❌ |  |  |
| 14 | Návrh obsahového webu | 1 |  | ❌ |  |  |
| 15 | Webová stránka | 5 |  | ❌ |  |  |
| 16 | CSS kaskáda | 3 | 7 | ✅ | 2026-05-10 (re-drill) | Re-drill heslovitý: 3 faktory kaskády (origin/specificita/source order) ✅, `@layer` × `@media` rozlišení ✅ (mantra zafungovala). BEM s reálným příkladem `card__icon--small`. **Korekce v re-drillu:** specificity tuple má 4 kategorie **(inline, ID, [class+attr+pseudo-class], [element+pseudo-element])** — atribut a pseudo-třída se počítají JAKO třída, ne zvlášť. Inline = `style=""` v HTML = `(1,0,0,0)`. `!important` přebíjí celou specificitu. Pamatovat: pravidlo nikdy neposunout sloupce (inline = nejvyšší pozice). |
| 17 | Vlastnosti CSS | 3 | 7 | ✅ | 2026-05-10 | Heslovitý lístek pokryl všech 4 bodů Popisu solidně. Bonus moment: explicitní "em se v em násobí" — komise to ocení. Jednotky, barvy (HEX/RGB/HSL), proměnné s scope (`:root` global × `.card` lokální), písma s woff2/woff/ttf prioritou. Jediný gap: `@font-face` keyword nepojmenován (koncept ale věděl). `font-display: swap` proti FOIT. Drobnost: % u `padding`/`margin` je vždy vůči WIDTH rodiče, i pro vertikální. |
| 18 | React (teorie) | 1 |  | ❌ |  |  |
| 19 | Webové aplikace | 1 |  | ❌ |  |  |
| 20 | Ověřování identity | 1 |  | ❌ |  |  |
| 21 | RESTful | 1 |  | ❌ |  |  |
| 22 | ASP.NET | 2 |  | ❌ |  |  |
| 23 | Událostmi řízené prog. | 1 |  | ❌ |  |  |
| 24 | Programovací jazyky | 4 |  | ❌ |  |  |
| 25 | Architektura Android | 1 |  | ❌ |  |  |

---

## DAT (25 témat)

| # | Téma | Sebehodn. (před) | Sebehodn. (po) | Status | Poslední revize | Poznámka |
|---|------|---|---|---|---|---|
| 1 | HTML5 a sémantika | 5 | 8 | ✅ | 2026-05-09 | 4 mikroúlohy + finální stránka. Sémantická hierarchie čistá (header→nav, main→article→sections+aside, footer), figure refactor správně (img+figcaption bez redundance), form s fieldset+legend a všemi label-for/id páry. CSS typografie linknutá, font fallback chain, rem hierarchie. Pamatovat: legend bez dvojtečky, alt × figcaption oddělit (alt = popis pro slepé, figcaption = kontext pro všechny), česká typografie chce mezeru u `10 %`. Materiály v `_materials/dat/01/` neexistují → notes ⚠️ z obecných znalostí. |
| 2 | Bootstrap | 3 |  | ❌ |  |  |
| 3 | Flexbox | 5 | 8 | ✅ | 2026-05-09 | Validated assignment 5 modulů hotový. Vlastní extras u Modul 4 (.pricing flex-wrap, gap, align-items stretch) přidány pro responzivitu = obhajitelné. Past s "mezi nimi" v Modulu 1 — text dvojsmyslný, řešení komise = jen actions má margin-left: auto, ale alternativní interpretace (oba mají) je obhajitelná. U zkoušky drž referenci, mít alternativní argument pro doptání. Pamatovat: `min-width: 0` na flex-grow potomky s textem (anti-overflow), `flex-basis: 250px` u stretchujícich karet (rovnoměrná základní šířka). |
| 4 | CSS Grid | 5 | 8 | ✅ | 2026-05-10 | Validated assignment "holy grail" layout zvládnut. Mobile-first přístup (default mobile, media query desktop) — obhajitelný, defendovat jako "moderní praxe". Dva opravené bugy: `height: 100vh` → `min-height` (assignment chce "minimálně"), boundary `>=768px` → `>768px` (assignment: 768 = mobil), inkonzistence area names "asd" vs "aside". Pamatovat: **každý název v grid-template-areas MUSÍ mít matching grid-area na položce**, jinak duch-oblast. Modern range syntax `(width > 768px)` funguje, ale `(min-width: ...)` má lepší browser compat. |
| 5 | Pozicování + z-index | 5 |  | ❌ |  |  |
| 6 | CSS animace | 2 |  | ❌ |  |  |
| 7 | Tabulky v HTML | 5 |  | ❌ |  |  |
| 8 | Datové typy a pole | 2 | 7 | ✅ | 2026-05-09 | 5 mikroúloh + finální 4-section task za 40 min. Pamatovat: **pole je REFERENČNÍ typ** (variable drží adresu, obsah žije na heapu), ne hodnotový — vlastní pozorování "změna se projeví venku" měla být klíč. Drobnosti: ASCII v identifikátorech (ne `Pondělí`), labely sekcí ve výstupu (`Matice:`, `Trojúhelník:`), `i, j` místo `x, y` pro matrix indexy. Single-pass diagonála chytrá, drill-worthy strategie pro lookup vs drillování. |
| 9 | Spojové struktury, stromy | 1 |  | ❌ |  |  |
| 10 | Podprogramy a lambda | 2 | 8 | ✅ | 2026-05-08 (2× drill + 2× praxe) | 2 praktické úlohy (SoucetCifer; TryNacti+PridejValidni+Spocitej — `out` z producent i konzument strany, HOF s lambdou). Ref-type passing pochopeno přes metaforu lístku/domu: **mutace** objektu prosakuje ven (`Add`), **přepsání** reference ne (`= new List`) — to by chtělo `ref`. Drobnosti k zapamatování: čistší if syntax (bez `== true`), funkce vs procedura — obě mají parametry, rozdíl je v návratové hodnotě; scope funkční = celé tělo funkce (ne parametr); v odpovědi o lambdě vždy doplnit konkrétní příklad (LINQ Where). |
| 11 | Kolekce | 3 | 8 | ✅ | 2026-05-10 | 5 mikroúloh + finální Bistro task. Stack (LIFO Push/Pop/Peek) ✅, Queue (FIFO Enqueue/Dequeue/Peek) ✅, Dictionary (Add/[]/ContainsKey/TryGetValue/iterace) ✅, counting pattern přes TryGetValue (efektivnější) ✅, kombinace všech 4 v Bistru ✅. **Self-debugged Peek bez závorek** — method group vs metoda volání (stejná lekce z DAT 10 mikroúlohy 11 s `SoucetCifer`). Pamatovat: TryGetValue je 1 lookup, ContainsKey+[] jsou 2 lookups. Drobnosti v Bistru: chybí "Kč" suffix v závěrečných řádcích, typo Hranolky 150 vs 60 (nezasáhlo výstup, ale "číst zadání precizně"). |
| 12 | Souborový systém + streamy | 4 |  | ❌ |  |  |
| 13 | Paralelní/async programování | 1 |  | ❌ |  |  |
| 14 | Git a GitHub | 5 | 8 | ✅ | 2026-05-10 | 5 mikroúloh + merge conflict resolution. Init/add/commit/log ✅, branching (switch -c, izolace) ✅, fast-forward merge ✅, .gitignore + past s už trackovanými soubory + `git rm --cached` ✅, **merge conflict** s manuální resolution ✅, `--graph` Y-struktura. Pamatovat: gitignore filtruje JEN untracked, na trackované soubory už nepůsobí. `git merge --abort` pro zrušení rozdělaného mergu. Default branch master (starší config), moderní = main. |
| 15 | ER model | 2 |  | ❌ |  |  |
| 16 | SQL výběr a filtrování | 5 |  | ❌ |  |  |
| 17 | REST API v ASP.NET | 2 |  | ❌ |  |  |
| 18 | Razor Pages | 1 |  | ❌ |  |  |
| 19 | Tag Helpers + formuláře | 1 |  | ❌ |  |  |
| 20 | Next.js (SSR/CSR) | 1 |  | ❌ |  |  |
| 21 | ORM | 1 |  | ❌ |  |  |
| 22 | React komponenty | 3 |  | ❌ |  |  |
| 23 | React hooks | 3 |  | ❌ |  |  |
| 24 | React Router | 3 |  | ❌ |  |  |
| 25 | Správa stavu v Reactu | 2 |  | ❌ |  |  |

---

## ČJL (20 knih)

| # | Titul | Autor | První pass | Poslední revize | Status | Poznámka |
|---|-------|-------|-----------|-----------------|--------|----------|
| 1 | Romeo a Julie | Shakespeare | 2026-05-09 | 2026-05-09 | 🟡 | Papírek 5/10. Faktická chyba: Merkucio = přítel, NE Romeův bratranec. Pojmový zmatek "jamb = forma promluvy" (jamb je metrum). Kostra OK. Díry: kontext autora (raná tragédie, italský námět z Bandella), 5 jednání + prolog-sonet Chóru, blankvers (≠ jen "jamb"), oxymóron jako signature prostředek (uměl definici, ne příklad), Romeův EXIL do Mantovy, nedoručený dopis. Před zkouškou re-pass body 6, 8, 11 z rozboru. |
| 2 | Lakomec | Molière | 2026-05-09 | 2026-05-09 | 🟡 | Papírek 6/10. Klasicismus pevně (3 jednoty, vysoký/nízký žánr, Plautus/Aulularia po doptání ✓). V úryvku gradace + řeč. otázka + hyperbola ✓; **expresivní slovo "lotře"** ⭐ samostatný nález. Plot tvist převrácený: Šipka ukradne truhličku → **Kleant** ji použije pro Marianu (NE pro Valéra+Elišku); Anselm = **deus ex machina** vyřeší Valéra+Elišku. Díry: časoprostor úplně, postavy Šipka/Čipera/Jakub, **apostrofa × personifikace** (rozlišit), próza jako výjimka v tvorbě Molièra po doptání ✓. Další zástupci klasicismu (Racine/Faidra, Corneille/Cid, La Fontaine/Bajky) chybí. |
| 3 | Revizor | Gogol | 2026-05-09 | 2026-05-09 | 🟡 | Papírek 4/10 — **nejslabší ze dne, k cílené revizi dřív než R&J/Lakomec**. Plot kostra ✓, "Chlestakovština" jako pojem ⭐, hyperbola+gradace+ironie v úryvku ✓ (formulace ironie *"hejtman vzhlíží k někomu, kdo mu nesahá ani po kotníky"* solidní pro zkoušku). **Faktické chyby:** kompozice "rámcová" → ve skutečnosti **chronologická, 5 jednání**; postak odhalí "konverzaci" → ve skutečnosti **dopis** Trjapičkinovi. **Klíčové díry (1–4 v doptání všechno minul, učební minimum předáno):** **němá scéna** ⭐ na konci, **Osip** (sluha chytřejší než pán), **Bobčinskij+Dobčinskij** jako spouštěč omylu, **Puškin dal Gogolovi námět** (perlička pro komisi). Místo: maloměsto/újezdní město, NE venkov. Komická jména postav (Skvoznik, Ljapkin-Ťapkin, Zemljanika) jako gogolovský prostředek úplně chybí. |
| 4 | Obraz Doriana Graye | Wilde |  |  | ❌ |  |
| 5 | Kytice | Erben |  |  | ❌ |  |
| 6 | Po nás ať přijde potopa | Gellner |  |  | ❌ |  |
| 7 | Deset malých černoušků | Christie |  |  | ❌ |  |
| 8 | Na západní frontě klid | Remarque |  |  | ❌ |  |
| 9 | O myších a lidech | Steinbeck |  |  | ❌ |  |
| 10 | Farma zvířat | Orwell |  |  | ❌ |  |
| 11 | 1984 | Orwell |  |  | ❌ |  |
| 12 | Sněhulák | Nesbø |  |  | ❌ |  |
| 13 | Krysař | Dyk |  |  | ❌ |  |
| 14 | Švejk (1. díl) | Hašek |  |  | ❌ |  |
| 15 | RUR | Čapek |  |  | ❌ |  |
| 16 | Bylo nás pět | Poláček |  |  | ❌ |  |
| 17 | Ostře sledované vlaky | Hrabal |  |  | ❌ |  |
| 18 | Spalovač mrtvol | Fuks |  |  | ❌ |  |
| 19 | Smrt krásných srnců | Pavel |  |  | ❌ |  |
| 20 | Vyšetřování ztráty třídní knihy | Cimrman |  |  | ❌ |  |

---

## Týdenní snapshot

Aktualizuj jednou týdně (např. neděle večer):

| Datum | SWI ✅/🟡/❌ | DAT ✅/🟡/❌ | ČJL ✅/🟡/❌ | Dnů zbývá | Tempo (na den) | Poznámka |
|-------|-----|-----|-----|-----------|------|----------|
| 2026-05-07 | 0/0/25 | 0/0/25 | 0/0/20 | 18 | start | Setup day |
|  |  |  |  |  |  |  |
