# svatak

Maturitní příprava pro P4A — SWI / DAT / ČJL.
Ústní zkouška: **25. 5. 2026, odpoledne.** *(Dopoledne je ještě naděje. Odpoledne už jen panika.)*

Mega-repo s celou moji maturitní přípravou na jednom místě: zápisky, surové materiály, plány den po dni, flashcards, praktický kód, ČJL rozbory. Všechno verzované v gitu, organizované přes [Claude Code](https://docs.claude.com/en/docs/claude-code) workflow (konvence v [`CLAUDE.md`](CLAUDE.md)).

Cíl je, aby jednoho dne v květnu 2026 z toho někdo (nejlíp já) odpověděl na 3 otázky tak dobře, že se k tomu komise nebude vracet.

---

## Co tady je

```
_materials/    # surové materiály — spolužáci (validovaní/nezaručení), Prchalovy zapisky (dik)
_notes/        # generované zápisky, po editaci (cíl: 15 min mluvení)
_flashcards/   # Q/A pro rep
_cjl/          # knihy, rozbory, poznámky z filmů
_meta/         # šablony + tabulka maturitních okruhů
_practice/     # DAT — reálný kód (C#, React, HTML, CSS, Bootstrap)
rep/           # submodule — flashcard appka (žije v JsemHarry7/rep)
progress.md    # kde aktuálně stojím (status, slabá místa)
maturita_plan.md  # plán bloků den po dni
cjl_plan.md       # plán ČJL den po dni
prompts.md        # reusable Claude Code makra (#1 zápisky, #2 flashcards, #3 test, ...)
rep-card-format.md  # spec pro kartičky do rep appky
CLAUDE.md      # konvence — Claude si to čte automaticky
```

Detailnější popis konvencí (hierarchie zdrojů, anti-halucinace, styl zápisků): viz [`CLAUDE.md`](CLAUDE.md).

---

## Pro spolužáky z P4A

Pokud jdeš z toho stejného losovacího koše, klidně si ber cokoliv užitečného. Pár věcí ale měj na paměti:

- **Žádný zápisek tady není oficiální učitelská pravda.** Většina je hybrid (Prchalovy zápisky + zelené spolužácké + Claude doplnění + moje úpravy + sebetestování). Kde uvidíš `⚠️` nebo `[doplněno z obecných znalostí]`, ber to jako *"fakt to ověř, neručím za to"*.
- **Halucinace jsou reálné riziko.** Claude umí napsat dobře vypadající nesmysl. Pokud na něčem stavíš, projdi to s primárním zdrojem (Microsoft Learn, MDN, React docs).
- **Můj styl mluvení ≠ tvůj styl mluvení.** Zápisky obsahují mnemotechniky a metafory, které mi sedí. Pro komisi to funguje, když to říkám já — ne když to z toho odříkáváš ty. Přepiš si to po svém.
- **`spoluzaci-nezarucene/` — DON'T.** Jméno mluví za sebe.

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

## Sesterský projekt: rep

[**rep**](https://github.com/JsemHarry7/rep) (žije v `rep/` jako git submodule) je **flashcard appka se spaced repetition systémem**, kterou jsem napsal víceméně paralelně s tímhle repem. Původně to měl být "rychlý pomocníček na opakování" — teď má React Router, hooks, designové tokeny, cloud sync přes Cloudflare D1 a Google OAuth.

Soubor `rep-card-format.md` popisuje Markdown formát kartiček, který appka přijímá. Claude je generuje přes prompt #2 v [`prompts.md`](prompts.md) — vstup: zápisek z `_notes/`, výstup: `.md` soubor s 10–20 kartami, který jen přetáhneš do appky.

Pokud z tohohle repa použiješ jen jednu věc, ať je to ona.

---

## Pokud to čteš v noci před zkouškou

Drop perfectionism.

1. Otevři `progress.md`, najdi co máš na 🟡.
2. Projdi flashcards (rep, nebo `_flashcards/`).
3. Témata na ❌: buď máš čas (Claude prompt #1, rychlý průchod, 15 min na téma), nebo nemáš (naučit se 30 s úvod + 3 hlavní pojmy, dost na to abys něco řekl).
4. Spát.

Pohoda. *(Tohle si říkám hlavně sám sobě.)*

---

## Příběh tohoto repa

**5 dní.** Tolik uplynulo od `git init` po pushnutou flashcard appku se SRS.

- **Den 1:** chtěl jsem si někam dát zápisky, ať nemám pět notesů po pokojích a polovinu cool myšlenek v Discordu. `git init`, adresářová struktura, první šablona.
- **Den 2–3:** šablony se rozrostly, vznikl systém promptů pro Claude Code (generuj zápisky, vytvoř flashcards, otestuj mě, code review). `progress.md` jako single source of truth.
- **Den 4–5:** vlastní flashcard appka se SRS — React + Vite, custom routing, cloud sync. Začalo to jako "potřebuju lepší než Anki, ten je ošklivej" a skončilo to vlastním produktem.

Někdy mezi tím jsem se podíval do kalendáře, viděl, že do zkoušky zbývá víc dní než jsem strávil stavbou tohohle bordelu, a začal panikařit. **Tobě**, kdo tohle čteš, panikařit nebudu pomáhat, ale aspoň ti můžu ušetřit pár hodin tím, co je už vygenerované a otestované.

---

## Tech disclaimer

Tohle repo používá [Claude Code](https://docs.claude.com/en/docs/claude-code) (Anthropic) jako pracovní nástroj. **Není to "AI mi napsalo maturitu".** Je to *"AI mi zorganizovalo bordel, vygenerovalo první draft, a já jsem to pak třikrát přečetl, opravil, otestoval se a předělal."* Stejně jako kdybych si půjčil přípravy od spolužáka. Taky bych si to neopisoval naslepo.

Workflow je popsaný v `CLAUDE.md` a `prompts.md` — pokud chceš stejný setup pro sebe, existuje sanitizovaná [šablona](https://github.com/JsemHarry7/_svatak-template) bez konkrétního obsahu. *(Jakmile bude pushnutá. Zatím lokálně.)*

---

## Licence

Žádná formální. Pokud z toho něco použiješ, neuváděj mě, neptej se. Pokud z toho dostaneš jedničku, pozvi mě na pivo.
