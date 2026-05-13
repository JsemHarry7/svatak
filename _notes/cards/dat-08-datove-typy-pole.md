---
title: DAT 8 — Datové typy a pole
description: Strukturované typy, enum, pole, vícerozměrná pole, jagged pole
tags: [maturita, dat, prg, csharp, pole, enum]
---

# Q: Jaký je rozdíl mezi elementárním a strukturovaným datovým typem?
A: **Elementární** typ drží jednu nedělitelnou hodnotu (`int`, `bool`, `char`, `double`). **Strukturovaný** se skládá z více prvků: **homogenní** (stejnorodé — pole, string, enum), **heterogenní** (různorodé — struct, class).

# Q: Pole je hodnotový nebo referenční typ?
A: **REFERENČNÍ.** Proměnná typu `int[]` drží **adresu** na blok dat v haldě (heap). Při předání do funkce funkce dostane kopii reference — ukazují na stejný objekt, změny prvků se projeví venku.

# CLOZE: V C# se velikost pole zjistí přes `{{Length}}`. U `List<T>` se používá `{{Count}}`.

# CLOZE: Pravoúhlé vícerozměrné pole se v C# zapisuje `int[{{,}}]`, jagged pole `int[{{][}}]`.

# CLOZE: `int[]` pole má default hodnoty {{0}}. `bool[]` má default {{false}}. `string[]` má default {{null}}.

# CLOZE: V `int[,]` matici se prvek čte přes `matice[{{i, j}}]` (čárka). V `int[][]` přes `pole[{{i}}][{{j}}]` (dvě závorky).

# MCQ: Co se vypíše?
```cs
int[] a = { 1, 2, 3 };
int[] b = a;
b[0] = 99;
Console.WriteLine(a[0]);
```
- 1
- !99
- Vyhodí výjimku
- 0
> Pole je referenční typ. `b = a` zkopíruje referenci, oba ukazují na stejný objekt. Změna `b[0]` = změna `a[0]`. Klasická past s reference vs value.

# MCQ: Jaká je časová složitost přístupu k poli na indexu?
- !O(1)
- O(n)
- O(log n)
- O(n²)
> Pole je souvislý blok paměti. Browser zná start adresu, index → přímý výpočet adresy. Konstantní složitost.

# MCQ: Jaká je časová složitost vkládání prvku doprostřed pole?
- O(1)
- !O(n)
- O(log n)
- Není možné
> Pole má statickou velikost. Pro vložení uprostřed musíš všechny prvky za pozicí posunout o jeden index → lineární čas. List<T> má dynamickou velikost, ale insert uprostřed je taky O(n).

# MCQ: Jaký je rozdíl mezi pravoúhlým `int[,]` a jagged `int[][]` polem?
- !Pravoúhlé = jeden souvislý blok paměti, fixní obdélník. Jagged = pole polí, každý řádek může být jinak dlouhý, různé objekty na haldě.
- Není rozdíl
- Jagged je rychlejší
- Pravoúhlé umí jen `int`
> Pravoúhlé je rychlejší (souvislá paměť), menší zátěž pro garbage collector. Jagged flexibilnější (různě dlouhé řádky), ale víc heap objektů. Pro matice → pravoúhlé. Pro trojúhelníky / různě dlouhé → jagged.

# MCQ: Co je `enum` v C# pro počítač a co pro programátora?
- Třída
- !Pro počítač číslo, pro programátora pojmenovaná hodnota
- Boolean
- Pole
> Enum mapuje pojmenované konstanty na čísla. `enum Den { Pondeli, Utery }` → Pondeli=0, Utery=1. Pro programátora čitelné, pro počítač číslo. Zvyšuje **čitelnost a bezpečnost** (kompilátor chytí překlep).

# FREE: Vysvětli rozdíl mezi pravoúhlým a jagged polem s use cases.
> Pravoúhlé `int[,]` = klasická matice, fixní obdélník, jeden souvislý blok paměti. Vhodné pro matematické matice, šachovnice, sudoku, fixní gridy. Rychlejší přístup, menší GC zátěž. Jagged `int[][]` = pole polí, každý řádek může být jinak dlouhý. Vhodné pro trojúhelníkové matice, studenti v ročnících (různé počty), Pascalův trojúhelník. Flexibilnější, ale víc objektů na haldě.

# FREE: Vysvětli enum a jeho výhody.
> Enum = datový typ s omezenou pojmenovanou množinou stavů. Pro počítač je to číslo, pro programátora text. Výhody: 1) **Čitelnost** — `if (stav == Stav.Aktivni)` vs `if (stav == 1)`. 2) **Bezpečnost** — kompilátor zachytí překlep `Den.Pondelu`. 3) **IDE napovídá** hodnoty. 4) **Switch** s enumem je sebe-dokumentující.

# FREE: Co je default hodnota pro různé typy polí v C#?
> `int[]` startuje **nulami**, `bool[]` startuje **`false`**, `double[]` startuje **0.0**, `char[]` startuje **'\0'** (null char), `string[]` startuje **`null`** (string je referenční typ, default reference = null). Při `new int[5]` máš 5 nul ihned k dispozici, není třeba je inicializovat.

# CODE: Vytvoř pole a iteruj přes něj.
```cs
int[] cisla = { 10, 20, 30, 40, 50 };

for (int i = 0; i < cisla.Length; i++) {
    Console.WriteLine($"[{i}] = {cisla[i]}");
}

foreach (int c in cisla) {
    Console.WriteLine(c);
}
```

# CODE: Najdi maximum v poli (pattern z hlavy).
```cs
int[] cisla = { 12, 7, 138, 25, 90, 3 };
int max = cisla[0];                                  // začni prvním prvkem!
for (int i = 1; i < cisla.Length; i++) {              // od i=1, ne 0
    if (cisla[i] > max) max = cisla[i];
}
Console.WriteLine($"Max: {max}");
```

# CODE: Pravoúhlá matice 3×3.
```cs
int[,] matice = new int[3, 3];
int count = 1;
for (int i = 0; i < 3; i++) {
    for (int j = 0; j < 3; j++) {
        matice[i, j] = count;
        count++;
        Console.Write($"{matice[i, j]} ");
    }
    Console.WriteLine();
}
// Výstup: 1 2 3 / 4 5 6 / 7 8 9
```

# CODE: Jagged pole — trojúhelník.
```cs
int[][] trojuhelnik = new int[5][];
int count = 1;
for (int i = 0; i < 5; i++) {
    trojuhelnik[i] = new int[i + 1];                  // i-tý řádek má i+1 prvků
    for (int j = 0; j <= i; j++) {
        trojuhelnik[i][j] = count;
        count++;
        Console.Write($"{trojuhelnik[i][j]} ");
    }
    Console.WriteLine();
}
```

# CODE: Enum a switch.
```cs
enum Den { Pondeli = 1, Utery, Streda, Ctvrtek, Patek, Sobota, Nedele }

Den dnes = Den.Streda;
switch (dnes) {
    case Den.Sobota:
    case Den.Nedele:
        Console.WriteLine("Víkend");
        break;
    default:
        Console.WriteLine("Pracovní den");
        break;
}
Console.WriteLine($"Hodnota: {(int)dnes}");  // 3
```

# Q: Inicializuj `max` na `0` při hledání maxima — past?
A: ANO past. Pokud pole obsahuje **jen záporná čísla** (např. `{-5, -3, -8}`), inicializace `max = 0` vrátí `0`, ne nejvyšší záporné. Správně: `int max = cisla[0]; for (int i = 1; ...)` — startuj prvním prvkem.

# Q: Co se stane při `pole[10]` na 5-prvkovém poli?
A: Vyhodí `IndexOutOfRangeException` za běhu. Kompilátor to nemůže chytit, protože index může být počítaný za běhu. Vždy ověř `i < pole.Length` v cyklu.

# Q: Jak se přistoupí k poslednímu prvku pole?
A: `pole[pole.Length - 1]`. Pole jsou indexovaná od 0, takže poslední index je o jedna menší než velikost. **Past:** `pole[pole.Length]` vyhodí IndexOutOfRangeException.

# Q: Co je `Array.Sort(pole)`?
A: Statická metoda pro **třídění pole na místě** (in-place). Modifikuje pole, nic nevrací. Default ascending. Pro descending: `Array.Sort(pole); Array.Reverse(pole);` nebo `Array.Sort(pole, (a, b) => b - a)`.
