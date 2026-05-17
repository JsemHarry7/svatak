---
title: SWI 6 — Chyby, testování, ladění
description: 4 druhy chyb, try/catch/finally, debugger, unit/integrační/E2E testy, mocky
tags: [maturita, swi, chyby, testovani, debugging]
---

# Q: Jaké 4 druhy chyb v programu znáš?
A: **Syntaktická** (porušení pravidel jazyka, compile-time), **sémantická** (program běží, ale počítá nesmysl), **typová** (špatný datový typ), **běhová / runtime výjimka** (něco neočekávaného za běhu — null, soubor nenalezen, výpadek sítě).

# CLOZE: {{Syntaktickou}} chytí kompilátor, {{sémantickou}} jen lidská kontrola a testy, {{runtime}} výjimky se řeší přes try/catch.

# Q: Co je sémantická chyba?
A: Program **běží, nepadá**, ale **počítá nesmysl** — špatný algoritmus, off-by-one error, špatný vzorec, nekonečný cyklus. Kompilátor ji nevidí, jen testy/lidská kontrola.

# MCQ: Off-by-one chyba (cyklus o 1 příliš/málo) je...
- Syntaktická
- !Sémantická
- Typová
- Runtime výjimka
> Program běží, ale výsledek je špatný o 1. Kompilátor nemůže poznat, že úmysl byl jiný.

# Q: Co je defensivní programování?
A: Ověříš stav **PŘED** akcí, např. `if (File.Exists(cesta)) { File.ReadAllText(cesta); }`. Čitelné, ale má problém **race condition** — mezi if a akcí může jiný proces stav změnit.

# Q: Co je race condition?
A: Chyba kdy se **dva procesy/vlákna zaplete**. Příklad: ověříš `File.Exists`, mezi tím jiný proces soubor smaže, pak `ReadAllText` selže navzdory předchozí kontrole.

# Q: Co je rozdíl mezi defensivním programováním a try/catch?
A: **Defensivní** = ověř před akcí (`if ...`). **Try/catch** = nech chybu nastat, chyť ji. Try/catch je **robustnější proti race condition**, defensivní je explicitnější.

# CODE: try/catch/finally struktura
```csharp
try
{
    var data = File.ReadAllText(path);
}
catch (FileNotFoundException ex)
{
    Console.WriteLine("Nenalezeno");
}
catch (Exception ex)
{
    Console.WriteLine("Jiná chyba: " + ex.Message);
}
finally
{
    // úklid, vždy proběhne
}
```

# Q: K čemu slouží blok `finally`?
A: Proběhne **vždy** (úspěch i chyba). Typicky pro **úklid** — zavřít stream, uvolnit zdroj, odpojit od DB. C# alternativa pro IDisposable: `using` block.

# Q: V jakém pořadí psát catch bloky?
A: **Specifický → obecný** (např. `FileNotFoundException` před `Exception`). Jinak by obecný `Exception` chytil všechno a specifický catch nikdy nedostal šanci.

# Q: Co se stane, když výjimku nikdo nezachytí?
A: Výjimka **propaguje nahoru zásobníkem volání** (call stack) — z funkce, kde nastala, do volajícího, do volajícího volajícího... Pokud nikdo `try/catch` nemá, **aplikace spadne**.

# Q: Rozdíl mezi návratovými kódy a výjimkami pro hlášení chyb?
A: **Návratové kódy** = funkce vrátí např. `-1` při chybě. Musíš kontrolovat **po každém volání**, lze tiše ignorovat = tichá chyba. **Výjimky** = chyba se vyhodí na povrch, **nelze ji tiše ignorovat**, propaguje stack.

# Q: Co je breakpoint?
A: Značka v IDE, kde se program **zastaví během ladění**. V tom okamžiku můžeš prohlédnout stav: hodnoty proměnných, call stack, krokovat dál.

# Q: Co je Watch v debuggeru?
A: **Okno s aktuálními hodnotami proměnných** v debug módu. Lze přidat výrazy, které chceš sledovat (např. `pole.Length`, `user.IsAdmin`).

# Q: 3 typy krokování v debuggeru?
A: **Step Over** = další řádek (přes volání funkce, nevleze dovnitř). **Step Into** = vleze dovnitř volané funkce. **Step Out** = dokončí aktuální funkci, vrátí se k volajícímu.

# MCQ: Step Into × Step Over rozdíl?
- Step Into je rychlejší
- !Step Over přejde přes volání funkce, Step Into vleze dovnitř
- Step Into chytí výjimku
- Není rozdíl
> Step Over = "udělej tu funkci jako celek, neřeš detail". Step Into = "ukaž mi, co se uvnitř děje".

# Q: Co jsou unit testy?
A: Testy **jedné metody/funkce izolovaně**, typicky pomocí **mocků** pro závislosti (DB, API). Rychlé (milisekundy), deterministické. Cíl: pokrýt **edge cases** (prázdný vstup, hraniční hodnoty, záporná čísla, NaN).

# Q: Co jsou integrační testy?
A: Testy, které ověřují **spolupráci dvou+ modulů** — např. že API správně komunikuje s databází. Pomalejší než unit, ale validují, že komponenty do sebe zapadají.

# Q: Co jsou E2E testy?
A: **End-to-end** testy simulují **reálného uživatele** klikajícího v prohlížeči. Nástroje: **Cypress**, **Playwright**, Selenium. Testují celý průchod aplikací (login → košík → objednávka). Nejpomalejší a nejkřehčí.

# CLOZE: Pyramida testů: hodně {{unit}} testů, méně {{integračních}}, málo {{E2E}}.

# Q: Co je mock a proč se používá?
A: **Falešný objekt** nahrazující reálnou závislost (DB, API, file system). Důvody: **rychlost** (žádná DB), **determinismus** (nezáleží na stavu DB), **izolace** (selhání DB nezpůsobí selhání testu funkce).

# MCQ: V unit testu metody, která volá databázi, ideálně použiješ...
- Reálnou produkční DB
- Reálnou testovací DB
- !Mock databáze
- Skipnout test
> Unit test = izolovaný. Reálná DB = pomalá, závisí na stavu, není deterministická.

# Q: C# frameworky pro unit testy?
A: **xUnit** (moderní default), **NUnit**, **MSTest**. Pro mockování: **Moq**, **NSubstitute**.

# CODE: xUnit test příklad
```csharp
[Fact]
public void Soucet_DvouCisel_VraciSpravnyVysledek()
{
    var result = Kalkulator.Soucet(2, 3);
    Assert.Equal(5, result);
}

[Theory]
[InlineData(2, 3, 5)]
[InlineData(0, 0, 0)]
[InlineData(-5, 5, 0)]
public void Soucet_RuznaCisla(int a, int b, int expected)
{
    Assert.Equal(expected, Kalkulator.Soucet(a, b));
}
```

# Q: Co je edge case a proč je důležitý v testech?
A: **Hraniční stav** — prázdný vstup, null, 0, záporné číslo, max int, NaN, infinity, Unicode/emoji, mezera. Edge cases bývají **nejčastější zdroj bugů** v produkci, proto je unit testy musí pokrýt.

# Q: Co testuje E2E test, co neumí unit?
A: **Celý průchod aplikací** jako uživatel — UI render, JS interakce, network volání, reakci backendu, DB perzistenci. Unit testy testují jen jednotlivé části, E2E ověří, že **dohromady fungují**.

# Q: Co je TDD?
A: **Test-Driven Development** — napřed napiš **failing test**, pak implementaci, která ho splní, pak refaktoruj. Cyklus: red (selhává) → green (prochází) → refactor.

# MCQ: Jaký mechanismus hlášení chyb je moderní default v OOP jazycích?
- Návratové kódy
- !Výjimky (try/catch)
- Globální flag proměnné
- Logování bez handlingu
> Výjimky propagují stack a nelze je tiše ignorovat. Návratové kódy = starý C-style přístup.

# FREE: Vysvětli rozdíl mezi syntaktickou, sémantickou, typovou a runtime chybou s příkladem každé.
> **Syntaktická** = chybí středník, překlep v keywordu (`while` → `whlie`) → kompilátor odmítne zkompilovat. **Sémantická** = `for (int i = 0; i <= arr.Length; i++)` místo `<` → projde compile, ale crash na poslední iteraci kvůli index out of range; nebo `(a + b) / 2` místo `(a * b) / 2` → běží, ale výsledek je špatný. **Typová** = `int x = "abc";` → C# kompilátor odmítne; v JS by se to spustilo a crashlo až za běhu. **Runtime výjimka** = `var f = File.ReadAllText("neexistujici.txt");` → kompilátor i typový systém OK, ale `FileNotFoundException` za běhu.

# FREE: Popis flow ladění bugu, kde se data v databázi neukládají.
> 1) Reprodukuj bug (zkus akci, vidíš že data nejsou v DB). 2) Najdi vstupní bod (controller, handler), kde se data zpracovávají. 3) Postav breakpoint na začátek metody. 4) Spusť aplikaci v debug módu, proveď akci. 5) Při zastavení na breakpointu krokuj Step Over, sleduj hodnoty proměnných v Watch. 6) Hledej moment, kde se očekává volání DB. 7) Step Into do té volání. 8) Zkontroluj, zda se opravdu volá `SaveChanges()` / `Insert()`. 9) Pokud ne, najdi proč (chybí volání, výjimka tichá, špatný objekt). 10) Oprav, znovu test bez breakpointu.
