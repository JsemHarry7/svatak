---
title: DAT 11 — Kolekce
description: Pole, List, Stack, Queue, Dictionary, HashSet — vlastnosti, složitost, použití
tags: [maturita, dat, prg, csharp, kolekce, stack, queue, dictionary]
---

# Q: Co je LIFO a FIFO?
A: **LIFO** (Last In, First Out) — poslední přidaný prvek je první odebraný. Příklad: Stack (zásobník), undo historie. **FIFO** (First In, First Out) — první přidaný je první odebraný. Příklad: Queue (fronta), tisková fronta.

# Q: Jaká je časová složitost vyhledávání podle klíče v Dictionary?
A: `O(1)` — díky **hashování**. Klíč se přepočítá na index v hashovací tabulce, počítač jde "najisto" bez prohledávání. Mnohem rychlejší než List, kde hledání podle hodnoty je `O(n)`.

# CLOZE: `Stack<T>` má metody `{{Push}}` (přidat na vrchol), `{{Pop}}` (odebrat z vrcholu) a `{{Peek}}` (nahlédnout bez odebrání).

# CLOZE: `Queue<T>` má metody `{{Enqueue}}` (zařadit na konec) a `{{Dequeue}}` (odebrat ze začátku).

# CLOZE: `Dictionary<K, V>` se přidává přes metodu `{{Add}}` (vyhodí výjimku pokud klíč existuje) nebo přes indexer `{{dict[klic] = hodnota}}` (přidá nebo přepíše).

# CLOZE: `HashSet<T>` je kolekce {{unikátních}} prvků bez hodnot — drží jen klíče. Vhodné pro test "je tam tenhle prvek?" v O(1).

# MCQ: Co se stane při `Pop()` na prázdný Stack?
- Vrátí null
- Vrátí 0
- !Vyhodí `InvalidOperationException`
- Vrátí default(T)
> Klasická past. Vždy zkontroluj `Count > 0` před Pop/Peek. Bezpečnější alternativa: moderní `TryPop`.

# MCQ: Jaký je rozdíl mezi `Pop` a `Peek` u Stacku?
- !`Pop` odebere a vrátí prvek. `Peek` jen nahlédne na vrchol bez odebrání.
- Není rozdíl
- `Pop` je rychlejší
- `Peek` je deprecated
> Stejný rozdíl u Queue: `Dequeue` (odebere) × `Peek` (nahlédne).

# MCQ: Kdy je `Dictionary` lepší volba než `List`?
- !Když máš mnoho dat a potřebuješ rychle vyhledávat podle klíče (O(1) vs O(n))
- Vždy
- Nikdy
- Pro malá data
> List má lineární lookup O(n) — projde každý prvek. Dictionary používá hashování → konstantní O(1). Pro telefonní seznam, cache, konfiguraci, counting — vždy Dictionary.

# MCQ: Co je `TryGetValue` u Dictionary?
- !Defenzivní lookup — vrátí bool úspěchu + naplní out parameter. Bez výjimky pokud klíč neexistuje.
- Smaže klíč
- Přidá klíč
- Není v C#
> Pattern: `if (dict.TryGetValue("klic", out int hodnota)) { ... } else { ... }`. Efektivnější než `ContainsKey + dict[]` (2 lookups).

# MCQ: Counting pattern v Dictionary — co je nejefektivnější?
- !`pocty.TryGetValue(klic, out int aktualni); pocty[klic] = aktualni + 1;` (1 lookup)
- `if (pocty.ContainsKey(klic)) pocty[klic]++; else pocty[klic] = 1;` (2 lookups)
- For loop
- LINQ Sum
> TryGetValue je 1 lookup. ContainsKey + indexer jsou 2 lookups. Pro counting pattern (slova, znaky) je TryGetValue efektivnější.

# FREE: Vyjmenuj 4 hlavní kolekce v C# a jejich princip.
> 1) **Pole `int[]`** — statická, indexový přístup O(1), souvislý blok paměti, pevná velikost. 2) **`List<T>`** — dynamická náhrada pole, uvnitř obaluje pole, automaticky roste. 3) **`Stack<T>`** — LIFO (Push/Pop/Peek), undo historie, rekurze. 4) **`Queue<T>`** — FIFO (Enqueue/Dequeue/Peek), tisková fronta, hudební queue. 5) **`Dictionary<K, V>`** — klíč/hodnota s O(1) přes hashování, telefonní seznam, cache.

# FREE: Vysvětli hashování v Dictionary.
> Klíč se přepočítá hashovací funkcí na **index v hashovací tabulce**. Místo lineárního prohledávání (klíč po klíči) Dictionary jde rovnou na vypočítané místo v paměti. Výsledek: O(1) lookup nezávisle na velikosti kolekce. **Klíče musí mít implementovaný `GetHashCode()`** — string, int, vlastní třídy.

# FREE: Vysvětli rozdíl mezi `Dictionary` a `HashSet`.
> **Dictionary<K, V>** = páry klíč-hodnota. Lookup podle klíče, vrátí hodnotu. **HashSet<T>** = jen klíče, bez hodnot. Lookup vrací bool — "je tam tenhle prvek?". HashSet pro **detekci duplikátů**, tracking navštívených uzlů, tagy. Dictionary pro **mapování** klíče → hodnoty.

# FREE: Co je rozdíl mezi `Add` a `[]` u Dictionary?
> **`dict.Add(klíč, hodnota)`** — striktní. Vyhodí `ArgumentException`, pokud klíč už existuje. **`dict[klíč] = hodnota`** — shovívavý indexer. **Přidá** pokud klíč neexistuje, **přepíše** pokud existuje. Pro counting pattern používáš indexer, pro inicializaci se silnými klíči Add.

# CODE: Stack — LIFO pattern.
```cs
Stack<string> historie = new Stack<string>();
historie.Push("Otevřít soubor");
historie.Push("Napsat text");
historie.Push("Uložit");

string posledni = historie.Peek();              // "Uložit" (bez odebrání)
string odebrany = historie.Pop();               // "Uložit" (odebráno)
int kolik = historie.Count;                     // 2
```

# CODE: Queue — FIFO pattern.
```cs
Queue<string> tisk = new Queue<string>();
tisk.Enqueue("dokument1.pdf");
tisk.Enqueue("dokument2.pdf");

string dalsi = tisk.Peek();                     // "dokument1.pdf"
string vytisteno = tisk.Dequeue();              // "dokument1.pdf"
```

# CODE: Dictionary — telefonní seznam.
```cs
Dictionary<string, int> telefony = new Dictionary<string, int>();
telefony.Add("Karel", 123456);
telefony["Jana"] = 987654;                       // přidat nebo přepsat

// Bezpečné čtení
if (telefony.TryGetValue("Pepa", out int cislo)) {
    Console.WriteLine(cislo);
} else {
    Console.WriteLine("Pepa neexistuje");
}

// Iterace přes pár
foreach (var pair in telefony) {
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}
```

# CODE: Counting pattern — počítání slov.
```cs
string[] slova = { "ahoj", "svete", "ahoj", "svete", "ahoj" };
Dictionary<string, int> pocty = new Dictionary<string, int>();

foreach (string slovo in slova) {
    pocty.TryGetValue(slovo, out int aktualni);  // 0 pokud neexistuje
    pocty[slovo] = aktualni + 1;
}
// Výsledek: { "ahoj": 3, "svete": 2 }
```

# CODE: Bistro — kombinace 3 kolekcí.
```cs
// Menu = Dictionary (klíč→cena)
Dictionary<string, int> menu = new Dictionary<string, int>() {
    { "Burger", 150 }, { "Cola", 40 }, { "Hranolky", 60 }
};

// Objednávky = Queue (FIFO)
Queue<string> objednavky = new Queue<string>();
objednavky.Enqueue("Cola"); objednavky.Enqueue("Burger"); objednavky.Enqueue("Voda");

// Účtenky = Stack (LIFO)
Stack<int> uctenky = new Stack<int>();

while (objednavky.Count > 0) {
    string jidlo = objednavky.Dequeue();
    if (menu.TryGetValue(jidlo, out int cena)) {
        Console.WriteLine($"{jidlo} za {cena} Kč");
        uctenky.Push(cena);
    } else {
        Console.WriteLine($"Nemáme {jidlo}");
    }
}

// Suma manuálním cyklem (ne LINQ — ukázat iteraci)
int trzba = 0;
foreach (int cena in uctenky) trzba += cena;
Console.WriteLine($"Tržba: {trzba} Kč, poslední: {uctenky.Peek()} Kč");
```

# Q: Proč nepoužívat `ArrayList` v moderním C#?
A: `ArrayList` ukládá `object` — vyžaduje **boxing/unboxing** (převod hodnotových typů na referenční), pomalé a typově nebezpečné (runtime errors). Použij vždy **generický `List<T>`** — type safety + výkon.

# Q: Jak fronta implementovaná přes `List<T>` má `O(n)` Dequeue?
A: `Dequeue` odebírá z **začátku** (index 0). `list.RemoveAt(0)` posouvá všechny zbylé prvky o jeden index → lineární čas. Lepší implementace: **kruhový buffer** (LinkedList interně) → `O(1)` na obou koncích.

# Q: Co je `KeyValuePair<K, V>`?
A: Typ páru klíč-hodnota, který Dictionary používá interně. Při iteraci `foreach (var pair in dict)` je `pair` typu `KeyValuePair<string, int>`. Přístup přes `.Key` a `.Value`.
