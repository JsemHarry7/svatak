# svatak

Maturitní příprava pro P4A — SWI / DAT / ČJL.
Ústní zkouška: **25. 5. 2026, odpoledne.** *(Dopoledne je ještě naděje. Odpoledne už jen panika.)*

---

## Příběh tohoto repa

Začalo to jednoduše. Chtěl jsem si někam dát zápisky, ať nemám pět notesů po pokojích a polovinu cool myšlenek v Discordu.

Za týden tu byly šablony.

Za dva týdny systém promptů pro Claude Code, který generuje zápisky z materiálů, vytváří flashcards, testuje mě a píše code review.

Za tři týdny vlastní flashcard appka se SRS, která původně měla být "jen pomocníček na opakování." Ten teď žije ve [vlastním repu](https://github.com/JsemHarry7/rep) a má hooks, routing a designové tokeny. *Anyway.*

Pak jsem se podíval do kalendáře a začal panikařit. **Tobě**, kdo tohle čteš, panikařit nebudu pomáhat, ale aspoň ti můžu ušetřit pár hodin tím, co je už vygenerované a otestované.

---

## Pro spolužáky z P4A

Pokud jdeš z toho stejného losovacího koše, klidně si ber cokoliv užitečného. Pár věcí ale měj na paměti:

- **Žádný zápisek tady není oficiální učitelská pravda.** Většina je hybrid (zelený spolužácký zápis + Claude doplnění + moje úpravy + sebetestování). Kde uvidíš `⚠️` nebo `[doplněno z obecných znalostí]`, ber to jako *"fakt to ověř, neručím za to"*.
- **Halucinace jsou reálné riziko.** Claude umí napsat dobře vypadající nesmysl. Pokud na něčem stavíš, projdi to s primárním zdrojem (Microsoft Learn, MDN, React docs).
- **Můj styl mluvení ≠ tvůj styl mluvení.** Zápisky obsahují mnemotechniky a metafory, které mi sedí. Pro komisi to funguje, když to říkám já — ne když to z toho odříkáváš ty. Přepiš si to po svém.
- **`spoluzaci-nezarucene/` — DON'T.** Jméno mluví za sebe.

---

## Co tady je

```
_materials/    # surové materiály — spolužáci (validovaní/nezaručení), Prchalovy zapisky (dik)
_notes/        # generované zápisky, po editaci (cíl: 15 min mluvení)
_flashcards/   # Q/A pro rep
_cjl/          # knihy, rozbory, poznámky z filmů
_meta/         # šablony + tabulka maturitních okruhů
_practice/     # DAT — reálný kód (C#, React, HTML, CSS, Bootstrap)
rep/           # submodule — flashcard appka (živé v JsemHarry7/rep)
progress.md    # kde aktuálně stojím (status, slabá místa)
maturita_plan.md  # plán bloků den po dni
cjl_plan.md       # plán ČJL den po dni
prompts.md        # reusable Claude Code makra (#1 zápisky, #2 flashcards, #3 test, ...)
rep-card-format.md  # spec pro kartičky do rep appky
CLAUDE.md      # konvence — Claude si to čte automaticky
```

Detailnější popis konvencí (hierarchie zdrojů, anti-halucinace, styl zápisků): viz [`CLAUDE.md`](CLAUDE.md).

---

## Hierarchie zdrojů (tier list)

| Tier | Co | Důvěra |
|------|---|--------|
| **S** | `prchal/` — Prchalovy zápisky z hodiny | Plná |
| **A** | `spoluzaci-validovane/` (zelené, učitel kývl) | Forma OK, *ne nutně každý detail* |
| **A** | Microsoft Learn / MDN / oficiální docs | Boring ale true |
| **B** | Claude doplnění s `[doplněno z obecných znalostí]` | Ověř si |
| **C** | `spoluzaci-nezarucene/` | Jen když nic jiného. Křížek po ruce. |

---

## Pokud to čteš v noci před zkouškou

Drop perfectionism.

1. Otevři `progress.md`, najdi co máš na 🟡.
2. Projdi flashcards (rep, nebo `_flashcards/`).
3. Témata na ❌: buď máš čas (Claude prompt #1, rychlý průchod, 15 min na téma), nebo nemáš (naučit se 30 s úvod + 3 hlavní pojmy, dost na to abys něco řekl).
4. Spát.

Pohoda. *(Tohle si říkám hlavně sám sobě.)*

---

## Tech disclaimer

Tohle repo používá [Claude Code](https://docs.claude.com/en/docs/claude-code) (Anthropic) jako pracovní nástroj. **Není to "AI mi napsalo maturitu".** Je to *"AI mi zorganizovalo bordel, vygenerovalo první draft, a já jsem to pak třikrát přečetl, opravil, otestoval se a předělal."* Stejně jako kdybych si půjčil přípravy od spolužáka. Taky bych si to neopisoval naslepo.

Workflow je popsaný v `CLAUDE.md` a `prompts.md` — pokud chceš stejný setup pro sebe, existuje sanitizovaná [šablona](https://github.com/JsemHarry7/_svatak-template) bez konkrétního obsahu. *(Jakmile bude pushnutá. Zatím lokálně.)*

---

## Licence

Žádná formální. Pokud z toho něco použiješ, neuváděj mě, neptej se. Pokud z toho dostaneš jedničku, pozvi mě na pivo.
