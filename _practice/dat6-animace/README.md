# CSS Animace a Transformace – Maturitní praktický úkol

**Čas:** 30 minut  
**Soubory:** `index.html` + `style.css`

---

## Popis

Máš připravený HTML soubor s portfoliem produktů a CSS soubor se základními styly.  
Tvým úkolem je oživit stránku pomocí CSS animací a přechodů.

> **Důležité:** HTML soubor **neupravuj**. Pracuješ pouze v `style.css`.

---

## Úkoly

### Úkol 1 – Karty produktů

Karty momentálně nereagují na pohyb myši. Uživatel by měl mít pocit, že se karta
"zvedá" při najetí — podobně jako fyzická karta ze stolu. Přechod by měl být plynulý,
ne skokový.

Zamysli se také nad ikonou uvnitř karty — může dát uživateli najevo interaktivitu
i samostatně.

### Úkol 2 – Indikátor načítání

Element `.loader` má již správný tvar i barvu, ale stojí na místě. Roztočit ho
by naznačilo uživateli, že aplikace pracuje. Pohyb by měl být pravidelný
a bez přerušení.

### Úkol 3 – Notifikační zpráva

Zpráva se aktuálně objeví okamžitě a tvrdě. Bylo by přirozenější, kdyby
nenápadně přijela nebo se vynořila — ideálně s malým zpožděním po načtení stránky.
Po dokončení animace by měla zůstat viditelná.

### Úkol 4 – Tlačítko

Tlačítko vypadá staticky. Dej uživateli vizuální zpětnou vazbu — při najetí myší
i při samotném kliknutí. Změny by měly proběhnout rychle a přirozeně.

### Úkol 5 – Přístupnost

Někteří uživatelé mají v operačním systému zapnutou možnost omezení pohybu
na obrazovce (např. kvůli epilepsii nebo kinetóze). Zajisti, aby tvé animace
tento požadavek respektovaly.

---

## Hodnocení

| Úkol | Body |
|------|------|
| Úkol 1 – Karty | 20 |
| Úkol 2 – Indikátor načítání | 20 |
| Úkol 3 – Notifikace | 20 |
| Úkol 4 – Tlačítko | 20 |
| Úkol 5 – Přístupnost | 20 |
| **Celkem** | **100** |

Bonusové body za kreativní nebo technicky zajímavé řešení přesahující základní požadavky.

---

## Nápověda

- Přechody a animace by měly být plynulé — vyhni se příliš dlouhým nebo křečovitým pohybům
- Pro nejlepší výkon animuj vlastnosti, které zpracovává GPU, ne ty co přepočítávají layout
- Pozor na to, v jakém stavu prvek zůstane po skončení animace