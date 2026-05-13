---
title: SWI 2 — Algoritmus
description: Algoritmus, vlastnosti, zápis, algoritmická složitost
tags: [maturita, swi, zal, algoritmus, slozitost]
---

# Q: Jak definuješ algoritmus?
A: Konečná posloupnost přesně definovaných kroků, která pro daný vstup vede k požadovanému výstupu.

# Q: Odkud pochází slovo "algoritmus"?
A: Od arabského matematika al-Chvárizmího z 9. století.

# Q: Jaký je rozdíl mezi algoritmem a programem (kódem)?
A: Algoritmus je abstraktní postup nezávislý na jazyce. Program je konkrétní implementace algoritmu v určitém programovacím jazyce.

# CLOZE: Algoritmus musí splňovat vlastnost {{konečnost}} (musí v konečném čase skončit) a {{determinovanost}} (každý krok je jednoznačně definovaný).

# CLOZE: Vlastnost algoritmu, která říká, že řeší celou třídu problémů (ne jen jeden konkrétní vstup), se nazývá {{hromadnost}} (nebo obecnost).

# CLOZE: Vlastnost algoritmu, která vyjadřuje, že vrací výsledek (výstup), se nazývá {{rezultativnost}} (nebo výslednost).

# MCQ: Která vlastnost algoritmu je porušena nekonečným cyklem?
- Determinovanost
- !Konečnost
- Hromadnost
- Správnost
> Konečnost (finitnost) říká, že algoritmus musí v konečném počtu kroků skončit.

# MCQ: Co znamená determinovanost algoritmu?
- Že vrátí správný výsledek
- !Že každý krok je jednoznačně určen — při stejném vstupu projde stejnými kroky
- Že řeší třídu problémů
- Že je efektivní
> Determinovanost = jednoznačnost kroků, ne výstupu. Důsledek: stejný vstup → stejný výstup.

# MCQ: Jakou složitost má vyhledávání hodnoty v neseřazeném poli?
- O(1)
- O(log n)
- !O(n)
- O(n²)
> Bez seřazení musíme prohledat každý prvek lineárně.

# MCQ: Jakou složitost má binární vyhledávání v seřazeném poli?
- O(1)
- !O(log n)
- O(n)
- O(n²)
> Každá iterace půlí prohledávaný úsek. Po k krocích zbývá n/2^k prvků, k = log₂(n).

# MCQ: Co dělá Big O notace?
- Měří přesný počet operací
- !Popisuje asymptotické chování algoritmu pro velká n
- Měří paměť algoritmu
- Měří rychlost CPU
> Big O ignoruje konstanty a nižší řády, sleduje **trend růstu** pro velká n.

# FREE: Vyjmenuj 5–7 vlastností algoritmu a stručně popiš každou.
> Konečnost: algoritmus skončí v konečném čase. Determinovanost: každý krok jednoznačně určen. Rezultativnost: vrací výsledek. Hromadnost: řeší třídu problémů. Vstup/Výstup: má definovaný vstup a výstup. Správnost: pro platný vstup dává správný výsledek. Efektivnost: v rozumném čase s rozumnými zdroji.

# FREE: Popiš způsoby, jak lze algoritmus zapsat.
> Slovní popis (přirozený jazyk pro člověka), vývojový diagram (grafické symboly: ovál = start/konec, obdélník = akce, kosočtverec = rozhodnutí, rovnoběžník = vstup/výstup), strukturogram (Nassi-Shneiderman, vnořené obdélníky), pseudokód (mezi programátory, jazykově nezávislý), programovací jazyk (konkrétní implementace pro počítač).

# FREE: Vysvětli, proč je důležité znát algoritmickou složitost.
> Dva algoritmy řešící stejný problém mohou mít dramaticky odlišnou rychlost při velkém vstupu. Algoritmus O(n²) na milionu prvků potřebuje miliardu operací. Při výběru algoritmu pro reálná data je složitost klíčový faktor mezi sekundovou a hodinovou odezvou.

# FREE: Porovnej O(n) a O(1) řešení součtu 1 až N.
> O(n) řešení používá cyklus a sčítá každé číslo (n operací). O(1) řešení používá Gaussův vzorec n*(n+1)/2 — tři operace nezávisle na n. Pro n = 10⁹ je O(n) sekundy, O(1) okamžitě.

# CODE: Napiš v C# rekurzivní funkci pro faktoriál.
```cs
int Faktorial(int n) {
    if (n <= 1) return 1;          // ukončovací podmínka
    return n * Faktorial(n - 1);   // rekurzivní volání
}
```

# CODE: Napiš v C# dvojitě vnořený cyklus s časovou složitostí O(n²).
```cs
for (int i = 0; i < n; i++) {
    for (int j = 0; j < n; j++) {
        Console.WriteLine(i * j);
    }
}
```

# Q: Jaká je složitost O(n + 5)?
A: O(n). Big O ignoruje konstanty a nižší řády.

# Q: Jaká je složitost O(n² + n)?
A: O(n²). Big O sleduje **dominantní** člen — n² roste rychleji než n.

# Q: Co je rozdíl mezi best, worst a average case složitostí?
A: Best case = nejlepší možný vstup (např. už seřazené pole pro řazení). Worst case = nejhorší (seřazené pozpátku). Average case = typický vstup. Bez upřesnění se obvykle myslí worst case.
