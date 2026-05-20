# DAT 12 — Souborový systém a streamy

## 🎯 Leaked zadání

> **Klíčová slova:** Struktura souborového systému, práce se soubory (textové, binární), datové proudy (streamy)

### Sekce 1 — Struktura souborového systému

V adresáři `Data` je cvičební struktura. Vytvoř program, který:
- **Projde celou strukturu** a vypíše všechny soubory a složky
- **Vypíše celkovou velikost** všech souborů v bajtech
- **Vypíše celkový počet souborů a složek**

### Sekce 2 — Textový soubor

- Pro každý **textový soubor v Data** načti a vypiš jeho obsah
- Vytvoř nový soubor **`Output/jmena.txt`** (mimo Data) a zapiš do něj **veškerá jména** získaná z textových souborů
- Nově vytvořený soubor **znovu otevři** a vypiš jeho obsah

### Sekce 3 — Binární soubor

- Vytvoř nový binární soubor **`Output/byte.bin`** a zapiš do něj **10 náhodných bajtů**
- Nově vytvořený soubor **znovu otevři**, přečti bajty a vypiš je na obrazovku

---

## 📂 Připravená struktura

```
dat12-soubory/
├── Program.cs              ← TVŮJ KÓD
├── dat12-soubory.csproj
├── Data/                   ← vstup (předpřipraveno)
│   ├── jmena1.txt          ("Anna\nPetr Novak\nMarie")
│   ├── jmena2.txt          ("Tomas\nLucie Cerna")
│   ├── obrazek.png         (binární — ignoruje se při čtení textu)
│   └── subfolder/
│       └── jmena3.txt      ("David Pokorny\nKarolina Mala")
└── Output/                 ← výstup (vytvoříš)
    ├── jmena.txt           (všechna jména z Data)
    └── byte.bin            (10 random bajtů)
```

## 🚀 Spuštění

```bash
cd _practice/dat12-soubory
dotnet run
```

---

## 📋 Mikroúlohy

Postupně, ne všechno najednou. Po každé pošli "mám N" + zkontroluju.

### Mikroúloha 1 — Rekurzivní procházení adresáře

V `Program.cs` napiš kód, který:
- Projde **rekurzivně adresář `Data`**
- Vypíše **každý soubor a složku** se cestou (ne strom s odsazením, jen seznam)

**Tip:** `Directory.GetFiles(path, "*", SearchOption.AllDirectories)` + `Directory.GetDirectories(path, "*", SearchOption.AllDirectories)`.

**Očekávaný výstup (přibližně):**
```
== Soubory ==
Data\jmena1.txt
Data\jmena2.txt
Data\obrazek.png
Data\subfolder\jmena3.txt

== Složky ==
Data\subfolder
```

### Mikroúloha 2 — Statistiky (počty + velikost)

Přidej:
- **Celkový počet** souborů a složek
- **Celkovou velikost** všech souborů v bajtech

**Tip:** `new FileInfo(path).Length` + LINQ `Sum()` nebo manuální cyklus.

**Očekávaný výstup:**
```
Počet souborů: 4
Počet složek: 1
Celková velikost: 76 B
```

### Mikroúloha 3 — Načti a vypiš obsah textových souborů

Pro **každý `.txt` soubor** v Data:
- Vypiš název souboru
- Vypiš jeho obsah (čtení po řádcích přes `StreamReader`)

**Tip:** filtr `*.txt`, `using StreamReader` s `ReadLine()` v cyklu.

**Očekávaný výstup:**
```
--- jmena1.txt ---
Anna
Petr Novak
Marie

--- jmena2.txt ---
Tomas
Lucie Cerna

--- jmena3.txt ---
David Pokorny
Karolina Mala
```

### Mikroúloha 4 — Vytvoř `Output/jmena.txt`

Posbírej **všechna jména** ze všech textových souborů v Data a zapiš je do `Output/jmena.txt`.

**Tip:** `Directory.CreateDirectory("Output")` (vytvoří složku pokud neexistuje), `StreamWriter` pro zápis.

### Mikroúloha 5 — Znovu otevři a vypiš `Output/jmena.txt`

Po vytvoření znovu otevři `Output/jmena.txt` a vypiš jeho obsah na konzoli.

### Mikroúloha 6 — Vytvoř `Output/byte.bin` s 10 random bajty

**Tip:** `BinaryWriter` + `Random.NextBytes(byte[] buffer)`.

### Mikroúloha 7 — Znovu přečti `Output/byte.bin` a vypiš bajty

**Tip:** `BinaryReader.ReadByte()` v cyklu nebo `File.ReadAllBytes`.

**Očekávaný výstup (čísla budou jiná):**
```
Bajty z byte.bin: 42, 127, 13, 200, 99, 7, 250, 156, 33, 88
```

---

## 🐛 Důležité pravidla pro C#

1. **Vždy `using`** pro StreamReader/Writer, BinaryReader/Writer
2. **`Path.Combine`** pro cesty (ne `+`!)
3. **`Directory.CreateDirectory`** před zápisem do nové složky
4. **`try-catch`** kolem I/O operací u final solution
5. **`Random.NextBytes(buffer)`** pro náhodné bajty

---

## Po dokončení všech 7

Pošli "mám final" — zkontroluju kompletní Program.cs jako exam-mode finál.
