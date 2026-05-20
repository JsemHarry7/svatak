---
title: DAT 12 — Souborový systém a streamy
description: NTFS/ext4/FAT32, cesty, kódování UTF-8, EOL, File× Stream, StreamReader/Writer, using, try-catch, stdin/stdout/stderr
tags: [maturita, dat, soubory, streamy, csharp, io]
---

# Q: Co je souborový systém?
A: Způsob, jakým OS organizuje data na disku. Zajišťuje **hierarchii** (složky), **metadata** (jméno, velikost, čas) a **oprávnění**.

# Q: 4 hlavní souborové systémy?
A: **NTFS** (Windows, žurnálovaný), **ext4** (Linux), **APFS** (macOS), **FAT32 / exFAT** (flash disky, SD karty).

# Q: Co je žurnálování?
A: **Deníček před zápisem** — systém poznamená "chci provést X". Při výpadku se po startu deníček dokončí nebo vrátí změny. **Prevence poškozených souborů.**

# Q: Rozdíl Windows × Linux cest?
A: **Oddělovač:** Win `\`, Linux `/`. **Kořen:** Win `C:\`, `D:\` (každý disk), Linux `/` (jeden). **Case sensitivity:** Win nerozlišuje, Linux ano.

# CLOZE: Windows oddělovač = `{{\}}`. Linux/macOS oddělovač = `{{/}}`.

# Q: Co je linuxové pravidlo "vše je soubor"?
A: V Linuxu jsou i klávesnice (`/dev/input`), disk (`/dev/sda`), tiskárna **reprezentovány jako soubory**. Operuje se s nimi stejně jako se souborem (read/write).

# Q: Rozdíl absolutní × relativní cesta?
A: **Absolutní** = od kořene (`C:\Users\axo\ukol.txt`, `/home/axo/ukol.txt`). Funguje vždy. **Relativní** = od pracovního adresáře (`./obrazky/foto.png`, `../jine/text.txt`). Funguje jen z konkrétního adresáře.

# Q: Co dělá `Path.Combine` a proč ho používat?
A: **Spojuje cesty multiplatformně** — používá správný oddělovač (Win `\`, Linux `/`). **Bez něj** by ruční `+` fungovalo jen na jednom OS. `Path.Combine("data", "f.txt")`.

# CODE: Path operace v C#
```csharp
using System.IO;

string p = Path.Combine("data", "subor.txt");
Path.GetFileName(p);                  // "subor.txt"
Path.GetExtension(p);                 // ".txt"
Path.GetDirectoryName(p);             // "data"
Path.GetFullPath(p);                  // absolutní cesta
Path.GetFileNameWithoutExtension(p);  // "subor"
```

# Q: 3 hlavní kódování?
A: **ASCII** (7 bitů, 128 znaků, jen angličtina). **UTF-8** (variabilní 1-4 B na znak, **dnešní standard**, podporuje všechny jazyky + emoji). **Windows-1250** (8 bitů, legacy česká).

# Q: Co je mojibake?
A: **Špatné zobrazení znaků** kvůli načtení souboru v jiném kódování ("Ť ďÁ" místo "Řeč"). Řešení: explicitně specifikovat `new StreamReader(path, Encoding.GetEncoding("windows-1250"))`.

# Q: EOL znaky podle OS?
A: **Windows**: `\r\n` (CR LF). **Linux/macOS**: `\n` (LF). **Klasický Mac**: `\r` (CR). Velký zdroj problémů při sdílení mezi systémy.

# Q: Rozdíl textový × binární soubor?
A: **Textový** = data pro lidi, bajty se převádějí na znaky podle kódování. `.txt`, `.csv`, `.json`. **Binární** = data pro stroje, raw bajty bez převodu. `.png`, `.mp3`, `.exe`.

# MCQ: Kterým způsobem rozhoduješ, zda je soubor textový nebo binární?
- Podle koncovky
- !Podle hlavičky souboru (magic number)
- Podle velikosti
- Podle data vytvoření
> Koncovka nedefinuje obsah (`.txt` může být cokoli). O typu rozhoduje hlavička (prvních pár bajtů — magic number).

# Q: 4 hlavní FileMode v C#?
A: **`Open`** (existující soubor), **`Create`** (přepíše! smaže existující), **`Append`** (přidat na konec), **`OpenOrCreate`** (otevři, jinak vytvoř).

# Q: Co je buffer při I/O?
A: **Mezi-paměť v RAM** před zápisem na disk. Zápis na disk je pomalý, proto se data hromadí v bufferu a jednorázově se "spláchnou" (`Flush()`). **⚠️ Pokud program spadne před Close/Flush, data v bufferu se ZTRATÍ.**

# Q: Proč používat `using` v C#?
A: **Garantuje zavření souboru (Dispose), i při výjimce.** Bez něj se může soubor zaseknout otevřený nebo se ztratí data z bufferu.

# CODE: using v C#
```csharp
// ✓ Modernější syntax (C# 8+)
using StreamReader sr = new StreamReader("file.txt");
string text = sr.ReadToEnd();
// uzavře na konci scope, i při výjimce
```

# Q: 5 nejčastějších I/O výjimek v C#?
A: **`FileNotFoundException`** (soubor neexistuje), **`DirectoryNotFoundException`** (složka neexistuje), **`UnauthorizedAccessException`** (chybí práva), **`IOException`** (obecná — disk plný, zamčený), **`PathTooLongException`** (cesta delší než limit).

# Q: 6 hlavních metod statické třídy `File`?
A: **`File.ReadAllText` / `WriteAllText`** (celý text), **`ReadAllLines` / `WriteAllLines`** (po řádcích), **`ReadAllBytes` / `WriteAllBytes`** (binární), **`AppendAllText`** (přidat na konec), **`File.Exists`**, **`Delete` / `Move` / `Copy`**.

# CODE: Rychlé File operace
```csharp
File.WriteAllText("a.txt", "Ahoj");
string text = File.ReadAllText("a.txt");

File.WriteAllLines("a.txt", new[] { "ř1", "ř2" });
string[] radky = File.ReadAllLines("a.txt");

byte[] data = File.ReadAllBytes("a.png");
File.WriteAllBytes("kopie.png", data);

File.AppendAllText("log.txt", "Nová položka\n");
bool ex = File.Exists("a.txt");
File.Move("a.txt", "b.txt");
File.Copy("a.txt", "kopie.txt", overwrite: true);
```

# Q: 4 hlavní operace `Directory`?
A: **`GetFiles(path)`** (soubory), **`GetFiles(path, "*", SearchOption.AllDirectories)`** (rekurzivně), **`GetDirectories(path)`** (podsložky), **`CreateDirectory(path)`** (vytvoř i mezisložky).

# CODE: Rekurzivní procházení složky
```csharp
string[] vsechny = Directory.GetFiles("data", "*", SearchOption.AllDirectories);
string[] txt = Directory.GetFiles("data", "*.txt", SearchOption.AllDirectories);

foreach (string soubor in vsechny)
{
    var fi = new FileInfo(soubor);
    Console.WriteLine($"{fi.Name} - {fi.Length:N0} B");
}
```

# Q: Co je Stream a kdy ho použít?
A: **Abstraktní třída v `System.IO`** reprezentující sekvenční proud bajtů. **Klíčová výhoda:** nečte celý soubor do paměti, **zvládne soubory větší než RAM** (10 GB log). Použít pro velké soubory.

# Q: 4 hlavní implementace Stream v .NET?
A: **`FileStream`** (soubor), **`MemoryStream`** (paměť), **`NetworkStream`** (síť/socket), **`GZipStream`** (komprese).

# Q: Rozdíl StreamReader × BinaryReader?
A: **`StreamReader`** = wrapper na Stream pro **TEXT** (po řádcích, ReadLine). **`BinaryReader`** = wrapper na Stream pro **BINÁRNÍ data typovaně** (`ReadInt32`, `ReadDouble`, `ReadString`).

# CODE: StreamReader (čtení textu po řádcích)
```csharp
using StreamReader sr = new StreamReader("velky.txt");
string radek;
while ((radek = sr.ReadLine()) != null)
{
    Console.WriteLine(radek);
}
```

# CODE: BinaryReader/Writer (binární data typovaně)
```csharp
using BinaryWriter bw = new BinaryWriter(File.Open("data.bin", FileMode.Create));
bw.Write(69);       // int (4 B)
bw.Write(3.14);     // double (8 B)
bw.Write("Ahoj");   // string s prefixem délky

using BinaryReader br = new BinaryReader(File.Open("data.bin", FileMode.Open));
int n = br.ReadInt32();
double d = br.ReadDouble();
string s = br.ReadString();
```

# Q: 3 standardní streamy konzole?
A: **stdin** (vstup, `Console.ReadLine()`), **stdout** (výstup, `Console.WriteLine()`), **stderr** (chyby, `Console.Error.WriteLine()`).

# Q: Co je shell přesměrování?
A: **`program > vystup.txt`** (stdout do souboru), **`program 2> chyby.log`** (stderr do souboru), **`program < vstup.txt`** (vstup ze souboru), **`p1 | p2`** (stdout p1 → stdin p2 = pipe).

# Q: Rozdíl `Directory` × `DirectoryInfo`?
A: **`Directory`** = statická třída, vrací **stringy s cestami**. **`DirectoryInfo`** = objektová, vrací **bohatší objekty s metadaty** (`Length`, `LastWriteTime`...). Stejně u `File` × `FileInfo`.

# Q: Co je `SearchOption.AllDirectories`?
A: Parametr `Directory.GetFiles` pro **rekurzivní prohledávání všech podsložek**. Bez něj jen aktuální složka.

# Q: Async varianty I/O v C#?
A: **`File.ReadAllTextAsync`**, **`File.WriteAllTextAsync`**, **`StreamReader.ReadLineAsync`**. Pro velké soubory nebo síťové zdroje, **neblokují vlákno** během čekání.

# FREE: Popis flow čtení a zápisu textového souboru s ošetřením chyb v C#.
> ```csharp
> try {
>     // Kontrola existence
>     if (!File.Exists("input.txt")) {
>         Console.WriteLine("Soubor neexistuje");
>         return;
>     }
>     
>     // Čtení (po řádcích pro velké soubory)
>     using StreamReader sr = new StreamReader("input.txt");
>     using StreamWriter sw = new StreamWriter("output.txt");
>     
>     string radek;
>     while ((radek = sr.ReadLine()) != null) {
>         sw.WriteLine(radek.ToUpper());  // zpracování
>     }
>     // při Dispose se buffer flushne na disk
> }
> catch (UnauthorizedAccessException) {
>     Console.WriteLine("Chybí práva");
> }
> catch (IOException ex) {
>     Console.WriteLine($"I/O chyba: {ex.Message}");
> }
> ```

# FREE: Vysvětli, proč pro práci se soubory v C# používáme using a co se stane bez něj.
> Práce se soubory vyžaduje **uvolnit OS handle** po skončení. `using` blok automaticky zavolá **Dispose()** (= `Close()`) na konci scope, **garantovaně i při výjimce**. Bez `using`: 1) Pokud nastane výjimka před manuálním `Close()`, soubor zůstane **otevřený** (locked) → další pokus o otevření selže. 2) **Buffer se neflushne** na disk → ztráta dat. 3) **Soubor zůstane zamčený** pro jiné procesy, dokud GC neuvolní (může být minuty). 4) **Wasted memory** (StreamReader drží data v RAM). `using` je nejjednodušší a nejbezpečnější způsob — vždy preferovat.
