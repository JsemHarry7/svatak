# 7 • Kolekce: pole, zásobník, fronta, slovník

**pole** (Array) 

- prvky bezprostředně za sebou
- přístup pomocí indexu
- v paměti uloženo hnedka za sebou, souvislý blok paměti
- v normálních jazycích se nedá po udání délky dále zvětšovat/zmenšovat
- přístup O(1) díky indexu

**kolekce** (*List*)

- v paměti uložený po částech
- uvnitř listu schované pole
    - když se naplní, `List` automaticky vytváří nové
    - staré pole zahodí
- dá se rozšiřovat
- ArrayList - pole objektů - nezmiňovat

**zásobník** - stack (LiFo)

- kolekce, kde přidáváte na jeden konec a odebíráte z toho samého
- jako talíře na sobě, odebíráme seshora a sbíráme seshora
- push, pop - JS
- využití: undo historie (Ctrl+Z)

**fronta** - queue (FiFo)

- **enqueue** (zařadit na konec), **dequeue** (odebrat ze začátku)
- nasází se do fronty
- prostě fronta u pokladny

**slovník** - k:v (*Dictionary<K, V>*)

- indexem není index (0..9), ale klíč - *většinou string*
- **funkce na základě hashovací tabulky**
    - aby počítač nemusel hledat “Karel”, zahashuje ho
    - to mu vygeneruje číslo (index) - po něm rovnou sáhne
    - **$O(1)$** přístup !

***HashSet<T>** (Java, C#)* jakožto implementace rychlého, unikátního listu (bez duplikátů)

- slovník, který má jen klíče bez hodnot
- hodnoty se neevidují.
- nás zajímá jenom jestli tam je, nebo není
- **$O(1)$** složitost

<aside>
⌨️

**praktická úloha**:

- udělat cvičení na zásobník a na frontu pomocí normálního pole / listu
- tzn.: udělat pole a přidat na něj funkce **push**, **pop**
- další implementace **enqueue**, **dequeue**
- ukázat, jak to funguje

  +   drobné hraní s k:v slovníkem

</aside>

## Praktická úloha

1. Implementace zásobníku (Stack)

```csharp
using System;
using System.Collections.Generic;

class MujZasobnik
{
    // Jako vnitřní úložiště použijeme List
    private List<string> _data = new List<string>();

    // PUSH - Přidat na vrchol (konec listu)
    public void Push(string polozka)
    {
        _data.Add(polozka);
        Console.WriteLine($"Push: {polozka}");
    }

    // POP - Odebrat z vrcholu (poslední prvek)
    public string Pop()
    {
        if (_data.Count == 0) return "Zásobník je prázdný!";

        // 1. Najdeme poslední index
        int posledniIndex = _data.Count - 1;
        // 2. Uložíme si hodnotu
        string hodnota = _data[posledniIndex];
        // 3. Smažeme prvek z Listu
        _data.RemoveAt(posledniIndex);
        
        return hodnota;
    }
}

// Použití
var stack = new MujZasobnik();
stack.Push("Talíř 1");
stack.Push("Talíř 2");
Console.WriteLine("Odebírám: " + stack.Pop()); // Vrátí Talíř 2
```

1. Implementace fronty

```csharp
class MojeFronta
{
    private List<string> _data = new List<string>();

    // ENQUEUE - Zařadit na konec
    public void Enqueue(string polozka)
    {
        _data.Add(polozka);
        Console.WriteLine($"Do fronty přišel: {polozka}");
    }

    // DEQUEUE - Odebrat ze začátku (index 0)
    public string Dequeue()
    {
        if (_data.Count == 0) return "Fronta je prázdná!";

        string hodnota = _data[0]; // Vezmeme prvního
        _data.RemoveAt(0);         // Smažeme ho (zbytek se posune - neefektivní!)
        return hodnota;
    }
}

// Použití
var queue = new MojeFronta();
queue.Enqueue("Zákazník 1");
queue.Enqueue("Zákazník 2");
Console.WriteLine("Odbaven: " + queue.Dequeue()); // Vrátí Zákazník 1
```

1. Slovník - ukázka

```csharp
// Definice: Klíč je String (Jméno), Hodnota je Int (Číslo)
Dictionary<string, int> telefonniSeznam = new Dictionary<string, int>();

// 1. Přidání prvků
telefonniSeznam.Add("Karel", 123456);
telefonniSeznam["Jana"] = 987654; // Modernější zápis (pokud neexistuje, vytvoří se)

// 2. Přístup k hodnotě
Console.WriteLine($"Karlovo číslo: {telefonniSeznam["Karel"]}");

// 3. Ošetření chyby (Klíč neexistuje)
if (telefonniSeznam.ContainsKey("Pepa"))
{
    Console.WriteLine(telefonniSeznam["Pepa"]);
}
else
{
    Console.WriteLine("Pepa v seznamu není.");
}

// 4. Procházení (Iterace) - používá se typ KeyValuePair
foreach (var polozka in telefonniSeznam)
{
    Console.WriteLine($"Jméno: {polozka.Key}, Číslo: {polozka.Value}");
}
```