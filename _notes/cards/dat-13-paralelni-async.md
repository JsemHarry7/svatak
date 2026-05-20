---
title: DAT 13 — Paralelní a asynchronní programování
description: CPU × I/O bound, Thread × Task × Process, async/await, Parallel.For, race condition, lock, Interlocked, deadlock, CancellationToken
tags: [maturita, dat, paralelni, async, csharp, threading]
---

# Q: Co je rozdíl mezi paralelním a asynchronním programováním?
A: **Paralelní** = víc úloh **opravdu současně na různých jádrech** (CPU-bound, `Task.Run`, `Parallel.For`). **Asynchronní** = uvolnění vlákna **bez nového** během čekání (I/O-bound, `async`/`await`). Async nepotřebuje víc jader.

# Q: Co je rozdíl mezi concurrent a parallel?
A: **Concurrent** = víc úloh se **střídá na 1 jádře** (rychlé přepínání, vypadá paralelně). **Parallel** = víc úloh běží **opravdu zároveň na různých jádrech**.

# Q: Rozdíl CPU-bound × I/O-bound + jak je řešit?
A: **CPU-bound** = náročný výpočet → **`Task.Run` / `Parallel.For`** (rozdělit mezi jádra). **I/O-bound** = čekání na disk/síť/DB → **`async`/`await`** (uvolní vlákno během čekání).

# CLOZE: Pravidlo: {{Task.Run}} na výpočty, {{await}} na čekání.

# MCQ: Pro stahování dat z internetu (HTTP API) se hodí...
- Parallel.For
- Task.Run
- !async/await s HttpClient
- Thread.Start
> HTTP request je I/O-bound (čeká na network). Async/await uvolní vlákno během čekání.

# MCQ: Pro výpočet faktoriálu na velkém čísle se hodí...
- async/await
- !Task.Run + Parallel.For
- Thread.Sleep
- HttpClient
> Náročný výpočet je CPU-bound. Rozdělit mezi jádra přes Task.Run nebo Parallel.For.

# Q: Process × Thread × Task?
A: **Process** = spuštěný program s vlastní pamětí (chrome.exe). **Thread** = vlákno uvnitř procesu, **sdílí paměť**. **Task** = abstrakce nad vlákny, používá **ThreadPool** (recyklace).

# Q: Co je ThreadPool?
A: **Sada předem vytvořených vláken**, které se znovupoužívají. Vytvoření nového Thread je drahé (OS volání). `Task.Run` si bere volné vlákno z poolu a po skončení ho vrátí.

# Q: Proč Task lepší než Thread?
A: 1) **Recyklace vláken** (ThreadPool). 2) **Vracení hodnoty** přes `Task<T>` (Thread musí přes sdílenou proměnnou + Join). 3) **Async/await funguje s Task**, ne s Thread.

# CODE: Task<T> s návratovou hodnotou
```csharp
Task<int> t = Task.Run(() => {
    Thread.Sleep(1000);
    return 67;
});
int vysledek = await t;
```

# Q: 5 důležitých Task metod?
A: **`Task.Run(action)`** (pustí na ThreadPool), **`Task.WhenAll(tasks)`** (počkej na všechny), **`Task.WhenAny(tasks)`** (počkej na první), **`Task.Delay(ms)`** (async čekání), **`await task`** (asynchronně počkej).

# Q: Rozdíl `Task.Wait()` × `await task`?
A: **`Task.Wait()`** = synchronně blokuje vlákno → **deadlock risk** v UI vláknu. **`await task`** = asynchronně počká, **uvolní vlákno**. Vždy preferovat `await`.

# Q: Co dělá `async` a `await`?
A: **`async`** = označuje metodu používající `await`. **`await`** = počkej na Task, **uvolni vlákno pro jinou práci** mezitím. Kompilátor přepíše async metodu na state machine.

# Q: Pravidla pro async metodu?
A: 1) Klíčové slovo **`async`**. 2) Návratový typ **`Task`**, **`Task<T>`** nebo **`void`** (jen event handlery). 3) Konvence: jméno končí **`Async`**. 4) `await` jen uvnitř async metody.

# Q: Proč je `async void` problematické?
A: 1) **Výjimky nelze odchytit** přes try-catch u volajícího → spadne aplikace. 2) **Nelze čekat na dokončení** (`await` nefunguje). Použít **JEN pro event handlery** (Button_Click), jinak vždy `async Task`.

# CODE: async/await flow
```csharp
public async Task<string> StahniDataAsync()
{
    using HttpClient klient = new HttpClient();
    string odpoved = await klient.GetStringAsync("https://api.example.com");
    return odpoved;
}
```

# Q: Co dělá `Parallel.For`?
A: **Paralelní cyklus** — runtime rozdělí iterace mezi vlákna. `Parallel.For(0, 100, i => Pracuj(i))`. Pořadí nedeterministické.

# CODE: Parallel.For, ForEach, Invoke
```csharp
Parallel.For(0, 100, i => Pracuj(i));

Parallel.ForEach(soubory, soubor => Zpracuj(soubor));

Parallel.Invoke(
    () => Metoda1(),
    () => Metoda2()
);
```

# Q: Co je `Environment.ProcessorCount`?
A: **Počet logických jader CPU**. Použít pro řízení paralelizace: `ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }`.

# Q: Co je race condition?
A: **Více vláken pracuje se stejnou proměnnou** → výsledek závisí na pořadí (nedeterministická chyba). Klasický příklad: `citac++` ze 2 vláken — operace není atomická.

# CODE: Race condition příklad
```csharp
int citac = 0;
Parallel.For(0, 1_000_000, i => citac++);
Console.WriteLine(citac);   // očekáváme 1 000 000, je to méně
```

# Q: 3 způsoby řešení race condition?
A: **1) `lock(zamek) { citac++; }`** (blok kódu). **2) `Interlocked.Increment(ref citac)`** (atomické, rychlejší). **3) Concurrent kolekce** (`ConcurrentBag`, `ConcurrentDictionary`).

# CODE: lock a Interlocked
```csharp
// Lock - blok kódu
object zamek = new object();
lock (zamek)
{
    citac++;
}

// Interlocked - atomická operace
Interlocked.Increment(ref citac);
```

# Q: Pravidla pro `lock`?
A: 1) **Vždy `private readonly object`**, ne `this` ani typ. 2) Drž zámek **co nejkratší dobu**. 3) **Nikdy `await` uvnitř lock** (může způsobit deadlock).

# Q: 4 thread-safe kolekce v .NET?
A: **`ConcurrentBag<T>`** (místo List), **`ConcurrentDictionary<K,V>`** (místo Dictionary), **`ConcurrentQueue<T>`** (FIFO), **`ConcurrentStack<T>`** (LIFO).

# Q: Co je deadlock?
A: **Dvě vlákna se navzájem čekají** na zámek → zamrznutí. A drží X, čeká na Y; B drží Y, čeká na X.

# Q: Jak předejít deadlock?
A: **Zamykat zámky ve STEJNÉM POŘADÍ** napříč celým kódem (např. abecedně podle jména). Plus **nikdy `await` uvnitř `lock`**.

# Q: Co je closure problém v for cyklu?
A: **Lambda zachytí REFERENCI na proměnnou, ne hodnotu.** `for (int i = 0; i < 10; i++) Task.Run(() => Console.WriteLine(i));` — všechny tasky vypíší **10** (poslední hodnotu i).

# CODE: Closure problém řešení
```csharp
// ❌ ŠPATNĚ
for (int i = 0; i < 10; i++)
    Task.Run(() => Console.WriteLine(i));   // všechny vypíší 10

// ✓ SPRÁVNĚ
for (int i = 0; i < 10; i++)
{
    int kopie = i;
    Task.Run(() => Console.WriteLine(kopie));
}
```

# Q: Co je `CancellationToken`?
A: **Mechanismus pro zrušení Tasku.** `CancellationTokenSource cts = new(); cts.Cancel()` z venku → uvnitř tasku `token.ThrowIfCancellationRequested()` vyhodí `OperationCanceledException`.

# Q: Co dělá `Stopwatch`?
A: **High-resolution timer** pro měření času (přesnější než `DateTime.Now`). `Stopwatch sw = Stopwatch.StartNew(); ... sw.Stop(); sw.ElapsedMilliseconds`.

# Q: Co je PLINQ?
A: **Paralelní LINQ.** `.AsParallel()` v LINQ chain → runtime rozdělí práci mezi vlákna. Pozor: pro **malá data pomalejší** než LINQ (overhead). Vyplatí se pro velká data.

# CODE: PLINQ
```csharp
int[] cisla = Enumerable.Range(1, 1_000_000).ToArray();
int suma = cisla.AsParallel()
                .Where(x => x % 2 == 0)
                .Sum();
```

# Q: Rozdíl `Task.WhenAll` × `Task.WhenAny`?
A: **`WhenAll`** = počkej na **VŠECHNY** tasky, vrátí pole výsledků. **`WhenAny`** = počkej na **PRVNÍHO**, kdo skončí. Use case: WhenAll pro rozdělení práce, WhenAny pro timeout pattern.

# CODE: Sekvenční vs paralelní hledání maxima
```csharp
// Sekvenčně
int NajdiMaxSekvencne(int[] pole)
{
    int max = pole[0];
    for (int i = 1; i < pole.Length; i++)
        if (pole[i] > max) max = pole[i];
    return max;
}

// Paralelně (rozdělit pole mezi jádra)
async Task<int> NajdiMaxParalelne(int[] pole)
{
    int pocetJader = Environment.ProcessorCount;
    int velikostCasti = pole.Length / pocetJader;
    Task<int>[] tasky = new Task<int>[pocetJader];

    for (int j = 0; j < pocetJader; j++)
    {
        int start = j * velikostCasti;
        int end = (j == pocetJader - 1) ? pole.Length : start + velikostCasti;
        int lokalniStart = start, lokalniEnd = end;   // closure fix!
        tasky[j] = Task.Run(() => NajdiMaxVCasti(pole, lokalniStart, lokalniEnd));
    }

    int[] dilciMaxima = await Task.WhenAll(tasky);
    return dilciMaxima.Max();
}
```

# FREE: Popis flow paralelního výpočtu maxima v poli s měřením času.
> 1) `Stopwatch sw = Stopwatch.StartNew()`. 2) Sekvenčně: jednoduchý for cyklus, sleduje dosavadní max. O(n). 3) Sekvenční čas: `sw.Stop(); sw.ElapsedMilliseconds`. 4) Paralelně: spočítat počet jader `Environment.ProcessorCount`, rozdělit pole na N stejně velkých částí (poslední vezme zbytek). 5) Pro každou část `Task<int>` najít max v té části. **Pozor closure** — `int lokalniStart = start;` před `Task.Run`. 6) `await Task.WhenAll(tasky)` — pole dílčích maxim. 7) Maximum ze všech dílčích maxim. 8) Měření času, porovnání. Teoretické zrychlení = počet jader. Reálné zrychlení ~5-6x na 8 jádrech (overhead na rozdělení a sloučení).

# FREE: Vysvětli rozdíl async/await pro I/O a Task.Run pro CPU s konkrétním příkladem.
> **I/O úloha — stáhnout 3 webové stránky:** `var a = await client.GetStringAsync(url1); var b = await ...` — během každého `await` se **vlákno uvolní** pro jinou práci (server obsluhuje jiný request). **Bez async** by vlákno čekalo 3 vteřiny pasivně. **Optimum: paralelně přes Task.WhenAll:** `await Task.WhenAll(client.GetStringAsync(u1), client.GetStringAsync(u2), client.GetStringAsync(u3))` — všechny 3 současně. **CPU úloha — spočítat faktoriály 1000 čísel:** `await Task.Run(() => Pole.Select(Faktorial).ToArray())` — výpočet na ThreadPool vlákně, hlavní vlákno (UI) reaguje. **Lepší: Parallel.For** — rozdělí mezi jádra. **Pravidlo:** I/O = uvolnění vlákna (`async`/`await`), CPU = víc jader (`Task.Run`, `Parallel.For`). Když to obrátíš, plýtváš zdroji.
