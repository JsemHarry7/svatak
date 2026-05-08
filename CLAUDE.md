# Maturitní příprava — P4A — SWI/DAT/ČJL

Tento adresář je pracovní prostor pro maturitní zkoušku.
**Termín ústní:** 25.5.2026 odpoledne. Dnes je 7.5.2026.
Hlavní plán: `maturita_plan.md`. Plán ČJL: `cjl_plan.md`.

## Předměty

- **SWI** (Softwarové inženýrství) — 25 témat, ústní zkouška, 15 min příprava + 15 min mluvení
- **DAT** (Data a kódování) — 25 témat, **30 min příprava (částečně u PC)** + 15 min mluvení
- **ČJL** (Český jazyk a literatura) — 20 knih, ústní s pracovním listem

## Adresářová struktura

```
_materials/    SUROVÉ vstupy — školní zápisky, slidy, vzorová zadání
  swi/{NN-tema}/
    skola/                    # od učitele
    spoluzaci-validovane/     # zelené (učitelem ověřené přípravy spolužáků)
    spoluzaci-nezarucene/     # ostatní spolužácké (používat opatrně nebo vůbec)
    moje/                     # moje vlastní zápisky z hodin
  dat/{NN-tema}/  (stejná struktura)

_notes/        MOJE generované zápisky — cíl: 15min ústní výklad
  swi/{NN-tema}.md
  dat/{NN-tema}.md

_flashcards/   Q/A pro repetice
  swi-{NN}.md
  dat-{NN}.md

_practice/     DAT praktický kód — reálné běžící projekty
  razor-app/
  react-app/
  sql-sandbox/
  next-app/

_cjl/          ČJL — rozbory, odkazy na A/V
  rozbory/{NN-titul}.md
  flashcards/{NN-titul}.md

_meta/         šablony, prompty, progress
  notes-template.md
  cjl-rozbor-template.md
  prompts.md
  progress.md
```

## Konvence při generování obsahu

### Hierarchie zdrojů (DŮLEŽITÉ — stav k 7.5.2026)

**Reality check:** Učitel oficiální materiály ke studiu nesdílí. `skola/` adresáře pro SWI/DAT jsou prázdné. Vlastní zápisky z hodin (`moje/`) existují jen u některých témat. Pracujeme s tím, co je.

Pořadí důvěry pro **SWI/DAT**:

1. **Primární (pokud existuje):** `moje/` (vlastní zápisky z hodin) — jsem byl u toho výkladu
2. **Primární (default):** `spoluzaci-validovane/` (zelené, učitelem schválené přípravy) — doporučená forma a obsah, **ale ne nutně každý technický detail**
3. **Doplňkové autoritativní:** oficiální docs ve formě URL nebo zkopírovaných pasáží:
   - Microsoft Learn (ASP.NET, .NET, C#)
   - MDN Web Docs (HTML, CSS, JS, web standardy)
   - React docs (react.dev)
   - Wikipedia / učebnice (obecné koncepty: algoritmy, OOP, sítě)
4. **Doplňkové neověřené:** `spoluzaci-nezarucene/` — vyhýbat se, pokud možno
5. **Tvoje obecné znalosti:** kde nic z výše uvedeného nepokrývá, doplníš, **ale VŽDY OZNAČ jako "[doplněno z obecných znalostí]"** s návrhem zdroje k ověření

Pořadí důvěry pro **ČJL**:

1. **Primární:** existující rozbory v `_cjl/rozbory/` (uživatelovy vlastní)
2. **Doplňkové:** poznámky po shlédnutí filmu/audio v `_cjl/audio_video_notes/`
3. **Doplňkové autoritativní:** literárně-historické encyklopedické zdroje
4. ČJL rozbory pro většinu titulů uživatel **už má hotové** — Claude Code je negeneruje, ale obohacuje (flashcards, doplnění kontextu, testování)

### Anti-halucinace (ZPŘÍSNĚNO)

Bez autoritativních školních materiálů jsme náchylnější k halucinacím. Pravidla:

- **Když si nejsi jistý technickou přesností** (verze API, konkrétní syntax, číselná hodnota, název konkrétní třídy/metody) → **OZNAČ ⚠️** a navrhni přesný URL k ověření.
- **Když zelené spolužácké zápisky obsahují tvrzení, které ti přijde podezřelé**, neopravuj tiše — explicitně řekni "spolužácký materiál říká X, ale standardně/oficiálně je Y, doporučuji ověřit u zdroje Z".
- **Žádné "vylepšování" terminologie** — pokud zelené materiály používají určitý termín, zachovej ho (i když je nemoderní), protože to je termín, na který se ptá učitel.
- **Když materiál o něčem mlčí**, napiš `[k doplnění]` místo vymýšlení.
- **Halucinace v technickém obsahu = padák u zkoušky.** Raději díra v zápiscích než falešná jistota.

### Workflow důsledek
Protože nemáme školní autoritativní zdroje, **každé téma musí projít sebetestováním** (prompt #3 v `_meta/prompts.md`) **PŘED** označením ✅ v `progress.md`. Není to volitelný krok — je to validace, že materiál sedí.

### Styl zápisků
- Cílový posluchač = já, sám sobě, před zkouškou — bez mlžení, hutně
- Česká odborná terminologie tak, jak se učí na škole
- Příklady kódu konkrétní a krátké (ne celé příklady z reálných projektů)
- Když existuje šablona v `_meta/`, drž se jí

## Co po Claude Code chci

1. **Generovat zápisky** z `_materials/` podle šablon v `_meta/`
2. **Vytvářet flashcards** z hotových zápisků
3. **Testovat mě** — klást otázky, hodnotit odpovědi, opravovat
4. **DAT praxe** — generovat cvičení ve formátu zkoušky, kontrolovat můj kód
5. **ČJL rozbory** — generovat z materiálů a obecných znalostí literárně-historické kontexty
6. **Aktualizovat `_meta/progress.md`** podle toho, jak postupuji

## Co po Claude Code NEchci

- Vymýšlet si fakta, příklady kódu, citace
- Přidávat info, které není v materiálech, bez označení "[doplněno]"
- "Vylepšovat" terminologii, která je sice neelegantní, ale je to ta, co se ptá učitel
- Vytvářet dlouhé, přebytečně podrobné zápisky — cíl je 15 min mluvení, ne dizertace

## Reusable prompty
Viz `_meta/prompts.md`. Jsou tam připravené prompty pro generaci zápisků, flashcards, kvízů, cvičení a code review.
