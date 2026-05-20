# DAT 13 — Paralelní a asynchronní programování

## 🎯 Zadání (typický exam-style per Prchal)

**Hledání maxima v poli sekvenčně vs paralelně** s měřením času.

Vytvoř konzolovou aplikaci v C#, která:

1. **Vygeneruje** velké pole náhodných čísel (např. 100 mil. položek)
2. **Sekvenčně** najde největší číslo (klasický `for` cyklus)
3. **Paralelně** najde největší číslo rozdělením pole na N částí, kde N = počet jader
4. **Změří čas** obou variant a porovná je
5. **Vypíše zrychlení** (sekvenční čas / paralelní čas)

Cíl: ukázat, že paralelní verze je u velkých dat **výrazně rychlejší**.

---

## 📋 Mikroúlohy

### Mikroúloha 1 — Vygeneruj pole

V `Main()` napiš kód:
- Vytvoř konstantu `VELIKOST = 100_000_000` (100 milionů čísel)
- Vygeneruj pole `int[]` této velikosti, naplň ho **náhodnými čísly**
- Vypiš informaci o velikosti pole a počtu jader CPU

**Tipy:**
- `int[] cisla = new int[VELIKOST];`
- `Random rnd = new Random(42);` — seed pro opakovatelnost
- Cyklus + `rnd.Next()` pro každý prvek
- `Environment.ProcessorCount` = počet logických jader

**Očekávaný výstup (přibližně):**
```
Pole obsahuje 100,000,000 čísel.
Počet jader CPU: 8
```

---

### Mikroúloha 2 — Sekvenční max + měření času

Přidej:
- **Sekvenční metodu** `NajdiMaxSekvencne(int[] pole)` která projde `for` cyklem a vrátí největší
- **Měření času** přes `Stopwatch.StartNew()` a `sw.ElapsedMilliseconds`
- **Vypsání výsledku** a délky

**Tipy:**
- `using System.Diagnostics;` pro Stopwatch
- `Stopwatch sw = Stopwatch.StartNew();` → operace → `sw.Stop();`
- `Console.WriteLine($"Sekvenčně: max = {max}, čas = {sw.ElapsedMilliseconds} ms");`

**Očekávaný výstup (čas záleží na HW):**
```
Sekvenčně: max = 2147483594, čas = 350 ms
```

---

### Mikroúloha 3 — Paralelní hledání přes Task.Run

**Cíl:** rozděl pole na **N částí** podle počtu jader, pro každou část `Task<int>`, slouč dílčí maxima.

**Postup:**
1. Spočítej `pocetJader = Environment.ProcessorCount`
2. `velikostCasti = pole.Length / pocetJader`
3. Vytvoř pole `Task<int>[] tasky = new Task<int>[pocetJader];`
4. `for` cyklus j = 0 to pocetJader-1:
   - `start = j * velikostCasti`
   - `end = (j == pocetJader-1) ? pole.Length : start + velikostCasti` (poslední bere zbytek)
   - **⚠️ POZOR na closure:** zkopíruj start/end do lokálních proměnných!
     ```csharp
     int lokalniStart = start;
     int lokalniEnd = end;
     ```
   - `tasky[j] = Task.Run(() => NajdiMaxVCasti(pole, lokalniStart, lokalniEnd));`
5. `int[] dilciMaxima = await Task.WhenAll(tasky);`
6. Najdi maximum z `dilciMaxima` (cyklem nebo `dilciMaxima.Max()`)

**Pomocná metoda `NajdiMaxVCasti(int[] pole, int start, int end)`:**
```csharp
static int NajdiMaxVCasti(int[] pole, int start, int end)
{
    int max = pole[start];
    for (int i = start + 1; i < end; i++)
    {
        if (pole[i] > max) max = pole[i];
    }
    return max;
}
```

**Důležité:** `Main` musí být `async Task Main()` aby ses mohl `await` u `Task.WhenAll`.

---

### Mikroúloha 4 — Porovnání časů + zrychlení

Po obou verzích:
- Vypiš čas obou (sekvenční + paralelní)
- Spočítej **zrychlení** = `(double)sekvencniCas / paralelniCas`
- Vypiš `Zrychlení: 5.83×` (formát s 2 desetinnými místy přes `:F2`)

**Tip:** `Console.WriteLine($"Zrychlení: {zrychleni:F2}×");`

**Očekávaný výstup:**
```
Sekvenčně:  max = 2147483594, čas = 350 ms
Paralelně:  max = 2147483594, čas = 60 ms

Zrychlení: 5.83×
```

(Konkrétní čísla závisí na HW. Důležité: **obě varianty vrátí STEJNÉ maximum**.)

---

### Mikroúloha 5 (BONUS) — Parallel.For varianta

Implementuj **třetí variantu** pomocí `Parallel.For` (one-liner-like):
- Použij **thread-local state** pattern (lepší než lock na každé iteraci):

```csharp
static int NajdiMaxParallelFor(int[] pole)
{
    object zamek = new object();
    int globalniMax = pole[0];

    Parallel.For(
        0, pole.Length,
        () => int.MinValue,                  // init lokálního stavu
        (i, state, localMax) =>              // tělo iterace
            pole[i] > localMax ? pole[i] : localMax,
        localMax =>                          // finalizace (sloučení)
        {
            lock (zamek)
            {
                if (localMax > globalniMax)
                    globalniMax = localMax;
            }
        }
    );

    return globalniMax;
}
```

Změř i tuhle verzi a porovnej.

---

## 🐛 Důležité pravidla pro async/parallel

1. **`Main` musí být `async Task`** pro použití `await`
2. **Closure problém:** v `for` cyklu **vždy lokální kopie** proměnných před `Task.Run`
3. **CPU-bound** úlohy = `Task.Run` / `Parallel.For` (víc jader)
4. **I/O-bound** úlohy = `async/await` (uvolnění vlákna)
5. **`Stopwatch.StartNew()`** přesnější než `DateTime.Now`

---

## 🚀 Spuštění

```bash
cd _practice/dat13-async
dotnet run
```

Po dokončení Mikroúloh 1-4 by měla být **paralelní verze 4-7× rychlejší** na 8 jádrech.
