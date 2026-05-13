---
title: SWI 4 — Datové typy, proměnné
description: Datové typy, proměnné, typovost, reference, imutabilita, hodnotové/referenční typy, přetypování
tags: [maturita, swi, prg, datove-typy, csharp]
---

# Q: Co je datový typ a co je proměnná?
A: Datový typ určuje pravidla, jaké hodnoty může proměnná držet a kolik místa zabírá v paměti. Proměnná je pojmenované místo v paměti, do kterého ukládáme hodnotu daného typu.

# Q: Jaký je rozdíl mezi staticky a dynamicky typovaným jazykem?
A: Statický (C#, Java) — typ se určí při překladu a nelze ho změnit. Dynamický (JS, Python) — typ určují data za běhu, lze ho měnit.

# Q: Jaký je rozdíl mezi deklarací a inicializací proměnné?
A: Deklarace vyhlašuje proměnnou (`int x;` — vyhradí místo, žádná hodnota). Inicializace je první přiřazení hodnoty (`x = 5;`). Lze obojí současně: `int x = 5;`.

# CLOZE: V C# je `string` {{referenční}} typ, ale chová se {{imutabilně}} — každá změna vytvoří nový objekt.

# CLOZE: Hodnotové typy (`int`, `bool`, `struct`) žijí v paměti na {{stacku}} (zásobníku), referenční typy (`class`, `string`, pole) na {{heapu}} (haldě).

# CLOZE: Konstanta deklarovaná klíčovým slovem {{const}} v C# nelze po inicializaci přepsat.

# MCQ: Které z následujících jsou hodnotové typy v C#?
- !int, bool, struct, enum
- string, class, pole
- List<T>, Dictionary
- interface, delegate
> Hodnotové typy v C#: int, bool, double, char, struct, enum. Referenční: class, string, pole, interface, delegate.

# MCQ: Co se vypíše?
```cs
int[] a = { 1, 2, 3 };
int[] b = a;
b[0] = 99;
Console.WriteLine(a[0]);
```
- 1
- !99
- 0
- Vyhodí výjimku
> Pole je referenční typ. `b = a` zkopíruje referenci, oba ukazují na stejný objekt. Změna b[0] = změna a[0].

# MCQ: Co dělá `(int)3.7`?
- !Přetypuje na 3 (desetinná část se zahodí)
- Zaokrouhlí na 4
- Vyhodí výjimku
- Vrátí 3.7
> Explicitní cast `(int)` zkracuje (truncates) desetinnou část, NEZAOKROUHLUJE.

# MCQ: Která varianta správně převede string "42" na int?
- (int)"42"
- !int.Parse("42")
- int x = "42"
- (Int32)"42"
> Cast `(int)` funguje jen mezi numerickými typy. String → int vyžaduje konverzi přes Parse (nebo TryParse).

# FREE: Vysvětli rozdíl mezi hodnotovým a referenčním typem na příkladu předávání do funkce.
> Hodnotový typ se předává **kopií hodnoty** — změna uvnitř funkce nezasáhne originál. Referenční typ se předává **kopií reference** — funkce a volající ukazují na stejný objekt, takže změna vnitřku objektu (např. list.Add()) se projeví venku. Metafora "lístek a dům": hodnota = celý dům, reference = lístek k domu.

# FREE: Popiš tři druhy přetypování v C# s příklady.
> 1) **Implicitní** — automatické, bez ztráty dat: `int x = 5; double y = x;`. 2) **Explicitní (cast)** — programátor řekne `(typ)`, přijímá riziko ztráty: `double a = 3.7; int b = (int)a;` (b = 3). 3) **Konverze** — mezi typy, které spolu nesouvisí, voláme metodu: `int.Parse("42")`, `Convert.ToInt32(...)`, `x.ToString()`.

# FREE: Co je imutabilita a uveď příklad v C#.
> Imutabilita = obsah objektu nelze měnit zevnitř, každá změna vytvoří nový objekt. V C# je `string` imutabilní: `s = s + "a"` nezmění původní string, vyrobí nový a `s` na něj ukáže. Pro intenzivní práci s textem se používá `StringBuilder`, který je mutabilní.

# FREE: Vyjmenuj modifikátory přístupu v C# a co každý dělá.
> `private` — vidí jen tatáž třída. `protected` — třída a její potomci. `public` — všichni. `internal` — stejné assembly (typicky stejný projekt). `protected internal` — kombinace.

# CODE: Napiš v C# property `Vek` typu int s validací (nesmí být záporné).
```cs
private int vek;
public int Vek {
    get { return vek; }
    set {
        if (value < 0) throw new ArgumentException("Věk nesmí být záporný.");
        vek = value;
    }
}
```

# CODE: V C# napiš zkrácený zápis (auto-property) pro property Jmeno typu string.
```cs
public string Jmeno { get; set; }
```

# Q: Co se stane při operaci `int x = 10 / 3;`?
A: Vrátí 3 (integer division). C# u `int / int` provede celočíselné dělení a desetinnou část zahodí. Pro 3.333... by alespoň jeden operand musel být `double`: `10 / 3.0` nebo `(double)10 / 3`.

# Q: Co je obor platnosti (scope)?
A: Pravidlo, odkud je proměnná viditelná v kódu. Globální (celá třída/projekt), lokální funkční (uvnitř metody), lokální bloková (uvnitř `{ }` jako `for`/`if`). Po opuštění scope proměnná zaniká.

# Q: Co znamená `var` v C# a je to dynamický typ?
A: `var` je **statický typ inferovaný z pravé strany**, NENÍ to dynamický typ. `var x = 5;` znamená `int x = 5;` a po deklaraci nelze `x = "ahoj"`. Pro skutečně dynamický typ slouží klíčové slovo `dynamic`.
