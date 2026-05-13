---
title: DAT 10 — Podprogramy a lambda
description: Funkce, procedury, parametry, rekurze, lambda funkce, scope
tags: [maturita, dat, prg, csharp, funkce, lambda, rekurze]
---

# Q: Jaký je rozdíl mezi funkcí a procedurou?
A: **Funkce** vrací hodnotu (má návratový typ a `return`). **Procedura** nevrací nic — v C#/Javě se značí `void`. Funkce: `int Secti(int a, int b) { return a+b; }`. Procedura: `void Vypis(string text) { Console.WriteLine(text); }`.

# Q: Co je rekurze a jaké má dvě části?
A: Funkce, která **volá sama sebe**. Dvě nutné části: 1) **Báze (base case)** — když je vstup triviálně jednoduchý, vrátí odpověď přímo (bez rekurze). 2) **Rekurzivní volání** — voláme sebe na MENŠÍM vstupu. Bez báze → StackOverflowException.

# CLOZE: Předávání hodnotou v C# — funkce dostane {{kopii}} hodnoty, originál se nemění. Předávání referencí přes `{{ref}}` nebo `{{out}}` — funkce může měnit originál.

# CLOZE: V C# se lambda funkce zapisuje šipkou `=>`. Příklad: `x {{=>}} x * 2` nebo `(a, b) {{=>}} a + b`.

# CLOZE: Typ pro funkci s návratovou hodnotou je `{{Func}}<T1, T2, TResult>` (poslední generic param = návratový). Pro `void` funkci je `{{Action}}<T>`.

# MCQ: Jaký je rozdíl mezi `ref` a `out` v C#?
- !`ref` — proměnná musí být inicializovaná před voláním, funkce může číst i měnit. `out` — proměnná nemusí být inicializovaná, funkce MUSÍ do ní zapsat.
- Není rozdíl
- `ref` je rychlejší
- `out` je deprecated
> Klasický příklad `out`: `int.TryParse(input, out int cislo)` — vrací bool úspěchu a přes out převedené číslo.

# MCQ: Co se vypíše?
```cs
List<int> list = new List<int> { 1, 2 };
PridejProsim(list);
Console.WriteLine(list.Count);

void PridejProsim(List<int> l) { l.Add(99); }
```
- 2
- !3
- Vyhodí výjimku
- 0
> List je referenční typ. Funkce dostane referenci, `Add(99)` mění sdílený objekt. Venku `list.Count == 3`.

# MCQ: Co předáš jako argument do funkce vyššího řádu?
- !Funkci samotnou (bez závorek, jako method group) nebo lambdu
- Výsledek volání funkce
- String s názvem funkce
- Třídu
> Higher-order function přijímá **funkci jako parametr**. `MojeFunkce(SoucetCifer)` — bez závorek = předáš referenci na funkci. `MojeFunkce(SoucetCifer(5))` — se závorkami = předáš výsledek volání (int).

# MCQ: Co dělá `pocty.TryGetValue(klic, out int hodnota)` u Dictionary?
- !Vrací bool úspěchu, do out parameteru naplní hodnotu (default(T) pokud klíč neexistuje)
- Vyhodí výjimku pokud klíč neexistuje
- Vrací jen hodnotu
- Smaže klíč
> TryGetValue je defenzivní pattern, který neházej výjimku. Vrací true + naplní out, nebo false + out je default(T) (0 pro int).

# FREE: Vysvětli rozdíl mezi předáváním hodnotou a referencí.
> **Hodnotou** (default) — funkce dostane kopii. Změna uvnitř nemění originál. Hodnotové typy (int, struct) vždy předávají hodnotou. **Referencí** přes `ref` (musí být inicializována, funkce může číst i měnit) nebo `out` (nemusí být inicializována, funkce musí zapsat). Referenční typy (List, class) předávají kopii **reference** — ukazují na stejný objekt, takže změny vnitřku objektu se projeví venku.

# FREE: Vysvětli lambda funkce a kdy je použít.
> Lambda = anonymní funkce v krátkém zápisu `(parametry) => výraz`. Pro jeden výraz: `x => x * 2`. Pro víc parametrů: `(a, b) => a + b`. Pro blok: `(a, b) => { ... return ...; }`. Použití: jednorázová logika jako argument jiné funkci — typicky LINQ (`Where`, `Select`, `OrderBy`). Když by se lambda opakovala 3× v kódu, vytáhni ji do pojmenované funkce.

# FREE: Co je rekurze a uveď klasický příklad.
> Funkce volající sama sebe. Klasický příklad — faktoriál: `n! = n * (n-1)!`. Báze: `0! = 1` (nebo `n <= 1 → 1`). Rekurze: `Faktorial(n) = n * Faktorial(n-1)`. Bez báze nekonečné volání → StackOverflowException. Vhodné pro problémy s **přirozenou hierarchií** (strom, dělení a panování). Alternativa k iteraci, často čitelnější, ale paměťově dražší (call stack).

# FREE: Co je funkce vyššího řádu a uveď příklad.
> Funkce, která **přijímá funkci jako parametr** nebo **funkci vrací**. Klasický příklad v C# — LINQ `Where`: `cisla.Where(x => x % 2 == 0)`. `Where` je higher-order function, lambda je její argument. Vlastní příklad: `int Apply(int x, Func<int, int> f) { return f(x); }` — `Apply` aplikuje libovolnou funkci na x.

# CODE: Funkce vs procedura.
```cs
// Funkce — vrací hodnotu
int Secti(int a, int b) {
    return a + b;
}

// Procedura — nevrací nic
void Pozdrav(string jmeno) {
    Console.WriteLine($"Ahoj, {jmeno}");
}
```

# CODE: Předávání hodnotou × ref × out.
```cs
int x = 10;

void ZkusZmenit(int p) { p = 999; }              // hodnotou (kopie)
void ZmenRef(ref int p) { p = 999; }             // ref (sdílí originál)
void DejHodnotu(out int p) { p = 42; }           // out (funkce musí zapsat)

ZkusZmenit(x); Console.WriteLine(x);             // 10 (originál nezměněn)
ZmenRef(ref x); Console.WriteLine(x);            // 999

int y;                                            // bez inicializace OK pro out
DejHodnotu(out y); Console.WriteLine(y);         // 42
```

# CODE: Lambda + Func + LINQ Where.
```cs
List<int> cisla = new List<int> { 1, 2, 3, 4, 5, 6 };

// Lambda v proměnné
Func<int, bool> jeSude = x => x % 2 == 0;

// Lambda jako argument LINQ
var sude = cisla.Where(x => x % 2 == 0).ToList();
// nebo
var sude2 = cisla.Where(jeSude).ToList();
```

# CODE: Higher-order function.
```cs
int AplikujFunkci(int x, Func<int, int> f) {
    return f(x);
}

int mocnina = AplikujFunkci(5, n => n * n);      // 25
int dvojnasobek = AplikujFunkci(5, n => n * 2);  // 10
```

# CODE: Rekurze — faktoriál.
```cs
int Faktorial(int n) {
    if (n <= 1) return 1;                         // báze — pokrývá i 0 a záporná čísla
    return n * Faktorial(n - 1);                  // rekurze
}

Console.WriteLine(Faktorial(5));                  // 120
```

# CODE: Rekurze — součet cifer čísla.
```cs
int SoucetCifer(int n) {
    if (n < 10) return n;                         // báze
    return n % 10 + SoucetCifer(n / 10);          // poslední cifra + zbytek rekurzivně
}

Console.WriteLine(SoucetCifer(123));              // 6 (1+2+3)
```

# Q: Proč je `if (n <= 1)` lepší báze pro faktoriál než `if (n == 1)`?
A: Robustnější — pokryje i `n = 0` (0! = 1 matematicky) a záporná čísla (vrátí 1 místo nekonečné rekurze + StackOverflow). Pravidlo: báze by měla zahrnovat **všechny "triviální nebo nesmyslné" vstupy**, ne jen ten matematicky správný.

# Q: K čemu slouží `var` jako návratový typ z LINQ Where?
A: `Where` vrací `IEnumerable<T>`, ne `List<T>`. `var` umožní kompilátoru odvodit typ. Pokud chceš `List<T>`, přidej `.ToList()` na konec. Klasická past při psaní `List<int> result = cisla.Where(...)` — kompilátor řve.

# Q: Co je obor platnosti (scope) v C#?
A: Pravidlo, odkud je proměnná viditelná. **Globální** = celá třída / projekt. **Lokální funkční** = uvnitř metody. **Lokální bloková** = uvnitř `{ }` (např. `for (int i = 0; ...)` — `i` žije jen v cyklu). Po opuštění scope proměnná zaniká.

# Q: Co je rozdíl mezi method group a method invocation?
A: **Method group** = jméno metody **bez závorek** (`SoucetCifer`) — reference na funkci, kterou lze předat jako argument. **Method invocation** = jméno **se závorkami** (`SoucetCifer(5)`) — spustí funkci a vrátí výsledek. Pro higher-order function předáváš method group.
