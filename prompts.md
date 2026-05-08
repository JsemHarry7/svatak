# Reusable prompty pro Claude Code

Tyhle prompty si kopíruj/upravuj. Slouží jako "makra" pro běžné úlohy.

> **Před prvním použitím Claude Code v tomto adresáři:** spusť `claude` v rootu maturita/, ať to načte CLAUDE.md jako context. Ověř to dotazem: *"Načti CLAUDE.md a stručně řekni, co je úkolem tohoto adresáře."*

---

## 1. Generuj zápisky z materiálů (SWI/DAT)

```
Generuj zápisky pro téma {SWI|DAT} {NN} - {název} z materiálů 
v _materials/{swi|dat}/{NN-tema}/.

Postup:
1. Načti VŠECHNY soubory v té složce. Rozliš zdroje podle podadresáře:
   - skola/ a moje/ = primární, důvěryhodné
   - spoluzaci-validovane/ = sekundární, brát s rezervou (zelené = forma OK, 
     ne nutně každý detail)
   - spoluzaci-nezarucene/ = jen pokud chybí jiný zdroj, vždy s ⚠️
2. Vytvoř zápisky podle šablony _meta/notes-template.md.
3. Output: _notes/{swi|dat}/{NN-tema}.md.
4. Cíl: 15min ústní zkouška, ne dizertace. Hutně, konkrétně.
5. Když si nejsi jistý technickou přesností -> ⚠️ a navrhni co ověřit.
6. Když materiál mlčí o něčem zjevně relevantním -> "[k doplnění]" nebo 
   "[doplněno z obecných znalostí]" jasně označit.

Po vygenerování mi vypiš:
- Co bylo pokryté ve zdrojích
- Co jsem doplnil ze svých znalostí (a co může být potřeba ověřit)
- ⚠️ položky k ověření
- Návrhy 3 nejpravděpodobnějších otázek komise
```

---

## 2. Vytvoř flashcards z hotových zápisků

```
Z _notes/{swi|dat}/{NN-tema}.md vytvoř flashcards do 
_flashcards/{swi|dat}-{NN}.md.

Formát Markdown:
- 8-12 kartiček
- **Q:** krátká otázka (max 1 věta)
- **A:** hutná odpověď (max 3 věty)

Pokrytí (priorita):
1. Hlavní pojmy a definice
2. Klíčové rozdíly / kontrasty (X vs Y)
3. Časté otázky komise z notes
4. ⚠️ položky (převrať jako otázku co potřebuju vědět)

NE: triviality, opakování, rozsáhlé výčty.
```

---

## 3. Otestuj mě (Q&A drill)

```
Otestuj mě na téma {SWI|DAT} {NN} ({název}).
Zdroj: _notes/{swi|dat}/{NN-tema}.md a _flashcards/{swi|dat}-{NN}.md.

Pravidla:
- Klaď otázky JEDNU PO DRUHÉ a vždy počkej na moji odpověď.
- Po každé mé odpovědi: zhodnocení (✅ správně / 🟡 částečně / ❌ chyba) 
  + doplnění/oprava.
- Začni od základů, postupuj k těžším.
- Po 5 otázkách udělej krátké průběžné shrnutí (co jdu / co kulhám).
- Skonči když řeknu "stop" nebo po 10 otázkách (cokoliv dřív).
- Na konci: doporučení, co revidovat.
```

---

## 4. DAT praxe — generuj cvičení ve formátu zkoušky

```
Pro DAT téma {NN} ({název}) navrhni mi praktické cvičení ve formátu maturitní 
zkoušky.

Output do _practice/dat-{NN}/zadani.md:
- Krátké, jasné zadání (jako u zkoušky)
- Časový limit: 20 min (pak budu mít 10 min na obhajobu)
- 3-5 hodnoticích kritérií (co budu muset splnit)
- Očekávaný výstup (co má "fungovat")

Pokud potřeba, vytvoř i kostru projektu v _practice/dat-{NN}/skeleton/ 
(setup, který bych dostal u zkoušky — třeba prázdný .NET projekt s DB connection).

Po vygenerování: NEČKEJ na řešení, jen mi řekni:
- "Zadání hotové v _practice/dat-{NN}/zadani.md, kostra v skeleton/"
- "Až to budeš mít, napiš 'review dat-{NN}'."
```

---

## 5. DAT code review

```
Zkontroluj moje řešení v _practice/dat-{NN}/.
Zadání bylo: viz zadani.md tamtéž.

Posuď:
1. **Funguje to?** Pokud ne, kde je chyba, jak ji opravit.
2. **Splňuje hodnoticí kritéria** ze zadání?
3. **Co by zkušená komise vytkla?** (best practices, čistý kód, edge cases)
4. **3 follow-up otázky**, které by se mohli zeptat: "co kdybyste...?"
5. **Co bych měl umět vysvětlit** o svém kódu (klíčové pasáže ústně).

Buď konkrétní, ne obecný. Cíl: vytrhnout slabá místa PŘED zkouškou.
```

---

## 6. ČJL — generuj rozbor

> **Pozn.:** Pro většinu titulů už mám hotové rozbory v `_cjl/rozbory/`.
> Tento prompt používej jen pro tituly, kde rozbor chybí.
> Pro existující rozbory použij prompt #6b níže.

```
Generuj rozbor pro ČJL titul {NN}: "{Autor — Titul}".

Postup:
1. Pokud jsou v _materials/cjl/{NN-titul}/ nějaké materiály, použij je.
2. Pokud ne nebo jsou neúplné, doplň z obecných literárně-historických 
   znalostí. VŽDY OZNAČ to, co jsi doplnil, kurzívou nebo "[doplněno]".
3. Použij šablonu _meta/cjl-rozbor-template.md.
4. Output: _cjl/rozbory/{NN}-{titul}.md.

Speciálně pro ústní s pracovním listem:
- Sekce "Pracovní list — checklist přístupu" musí být použitelná na neznámý 
  úryvek (obecná strategie, ne konkrétní úryvek)
- "K čemu to chci říct víc" - 3-5 zajímavých postřehů, na které lze navázat

Po vygenerování:
- Vypiš, co je z konkrétních materiálů a co jsi doplnil
- Navrhni 3 možné nelitterární texty, které by mohly být v páru u zkoušky 
  (pro trénink párování)
```

---

## 6b. ČJL — obohať existující rozbor (HLAVNÍ ČJL WORKFLOW)

```
Mám hotový rozbor v _cjl/rozbory/{NN}-{titul}.md. Obohať ho:

1. Načti existující rozbor — neměň ho, jen doplň/označ.
2. Načti šablonu _meta/cjl-rozbor-template.md a zkontroluj, jestli rozbor 
   pokrývá všechny sekce. Co chybí, navrhni doplnění (ne přepis).
3. Pokud existují poznámky z A/V (filmu/audia) v 
   _cjl/audio_video_notes/{NN}-{titul}.md, integruj je.
4. Doplň 3-5 "zajímavých postřehů" do sekce "K čemu to chci říct víc" 
   — věci, na kterých můžu u zkoušky stavět (nečekané paralely, citace, 
   historický detail).
5. Označ věci, které jsou doplněné, jako *kurzíva* nebo "[doplněno]".

Output: ulož pod _cjl/rozbory/{NN}-{titul}.md (přepiš), ale TY DOPLNĚNÉ ČÁSTI 
musí být jasně označené, abych viděl, co jsem napsal já a co je nové.
```

---

## 7. ČJL — flashcards z rozboru

```
Z _cjl/rozbory/{NN}-{titul}.md udělej flashcards do _cjl/flashcards/{NN}-{titul}.md.

8-10 kartiček, formát Q/A.

Pokrytí (priorita):
1. Žánr + literární období
2. Místo, čas
3. Hlavní postavy (1-2 nejdůležitější)
4. Hlavní téma
5. Charakteristický rys stylu autora
6. Souvislost s autorovými dalšími díly nebo s dobou
7. 1-2 motivy / symboly
```

---

## 8. ČJL — denní revize (rotace)

```
Načti _meta/progress.md (sekce ČJL). 
Vyber 1-2 tituly, které byly poslední revize > 3 dny zpět NEBO mají status 🟡.

Pro každý vybraný titul:
- 5 rychlých Q/A (z flashcards) bez nahlížení do nich
- Jeden krátký "test úryvku": vyber libovolný odstavec z rozboru, předlož 
  mi ho, zeptej se "z jakého díla je to a proč si to myslíš"

Skonči po 5-10 minutách max. Cílem není naučit se, ale udržet to čerstvé.
```

---

## 9. Aktualizace progress trackeru

```
Aktualizuj _meta/progress.md.

Pro {SWI|DAT} {NN} ({název}):
- Status: {❌|🟡|✅}
- Sebehodnocení: _/10
- Datum poslední revize: dnes
- Poznámka: {volný text — slabá místa, co revidovat}

Pokud položka v progress.md zatím není, založ ji.
```

---

## 10. Týdenní status check

```
Načti _meta/progress.md a maturita_plan.md.

Vypočti:
- Kolik témat SWI je ✅ / 🟡 / ❌
- Kolik DAT je ✅ / 🟡 / ❌  
- Kolik ČJL knih je ✅ / 🟡 / ❌
- Kolik dní zbývá do zkoušky (25.5.2026)
- Kolik témat denně bych měl splnit, abych to stihl

Pokud zaostávám: aktivuj crisis protocol z maturita_plan.md, navrhni triage 
(které témata na minimum, kde hlubokou).

Pokud jdu po plánu: pochval, žádné přibližování. 
Pokud jdu předčasně: navrhni kde přidat hloubku (nejslabší místa).
```

---

## Tip: Slash-commands

V Claude Code můžeš použít custom slash-commands. Pokud si vytvoříš 
`.claude/commands/zapisky.md` s prompt #1 (s `$ARGUMENTS`), pak v session 
napíšeš `/zapisky swi 5` a spustí to.

Je to výhodné pro každodenní operace. Návod: 
https://docs.claude.com/en/docs/claude-code/slash-commands
