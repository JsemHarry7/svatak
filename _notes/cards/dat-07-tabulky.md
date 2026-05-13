---
title: DAT 7 — Tabulky v HTML
description: Struktura tabulky, colspan/rowspan, CSS stylování, přístupnost
tags: [maturita, dat, web, html, tabulky]
---

# Q: Vyjmenuj základní strukturální tagy HTML tabulky.
A: `<table>` (kořen), `<caption>` (popisek), `<colgroup>` + `<col>` (definice sloupců pro stylování), `<thead>` (hlavička), `<tbody>` (tělo), `<tfoot>` (patička), `<tr>` (řádek), `<th>` (hlavičková buňka), `<td>` (datová buňka).

# Q: Jaký je rozdíl mezi `<th>` a `<td>`?
A: **`<th>`** = hlavičková buňka, sémanticky popisek dat. Default: bold + centered. **`<td>`** = datová buňka, konkrétní hodnota. Sémantický rozdíl je klíčový pro přístupnost (čtečky) a SEO.

# CLOZE: Atribut `{{colspan="N"}}` spojí buňku přes N sloupců horizontálně. Atribut `{{rowspan="N"}}` přes N řádků vertikálně.

# CLOZE: CSS vlastnost `border-collapse: {{collapse}}` slijí okraje sousedních buněk (preferováno). Hodnota `{{separate}}` (default) drží okraje oddělené s mezerou.

# CLOZE: Pro přístupnost se na `<th>` doplňuje atribut `{{scope}}` s hodnotou `"col"` (header sloupce) nebo `"row"` (header řádku).

# MCQ: Lze tabulky používat pro layout stránky?
- Ano, je to standard
- !Historicky ano, dnes ne — tabulka je sémantický element pro tabulární data. Pro layout slouží Flexbox/Grid.
- Jen pro mobilní stránky
- Jen v Bootstrap
> Tabulky pro layout = anti-pattern. Důvody: sémanticky špatně, špatná přístupnost (čtečky zmatené), špatná responzivita. Pro layout vždy Flexbox/Grid.

# MCQ: Co se stane, když u `rowspan="2"` přidáš první `<td>` v dalším řádku?
- Funguje normálně
- !Layout se rozsype — první buňka v dalším řádku je už pokrytá rowspan, nesmí se přidávat
- Rowspan ignoruje
- Vytvoří se nový řádek
> Při `rowspan` se v dalších řádcích **vynechá** první `<td>`, který je pokrytý. Pokud ho přidáš, sloupce se posunou.

# MCQ: Jak vytvoříš zebra tabulku se střídavým pozadím řádků?
- !`tbody tr:nth-child(odd) { background: #f5f5f5; }`
- `tr.zebra`
- `table.zebra`
- Není možné v CSS
> CSS pseudo-třída `:nth-child(odd)` / `:nth-child(even)` umožní cílit lichá nebo sudá pořadí řádků.

# MCQ: Co znamená `<colgroup>` + `<col>`?
- !Definice sloupců pro stylování celých sloupců najednou (background, border, width). Lze stylovat jen omezené vlastnosti.
- Skupiny řádků
- Captions
- Není to validní HTML
> `<colgroup>` skupina sloupců, `<col span="N">` jednotlivé sloupce. Lze stylovat `background`, `border`, `width`, `visibility`. Pro padding/font/color musíš na `<td>`/`<th>`.

# MCQ: Co je `<caption>` a kde se zobrazí?
- !Popisek/název tabulky, default nad tabulkou, lze přesunout přes `caption-side: bottom`. Důležitý pro přístupnost.
- Footer tabulky
- Atribut
- HTML komentář
> `<caption>` čtečka přečte jako "tabulka: [text]...". SEO bonus. Měl by být prvním dítětem `<table>`.

# FREE: Vyjmenuj základní strukturální tagy tabulky a jejich účel.
> `<table>` kořen. `<caption>` popisek (přístupnost). `<colgroup>` + `<col>` pro stylování celých sloupců. `<thead>` hlavička (sémanticky oddělená, default bold). `<tbody>` tělo dat. `<tfoot>` patička (souhrny). `<tr>` řádek. `<th>` hlavičková buňka (s `scope` atributem pro přístupnost). `<td>` datová buňka. Při tisku se thead/tfoot opakují na každé stránce.

# FREE: Vysvětli rozdíl mezi `border-collapse: collapse` a `separate`.
> `collapse` (preferováno) — okraje sousedních buněk se slijí, žádné dvojité linky, čistší vzhled. `separate` (default) — každá buňka má svůj okraj, mezi nimi mezera regulovaná `border-spacing`. Pro datové tabulky používej `collapse`.

# FREE: Vysvětli pattern použití colspan + rowspan v komplexnější tabulce.
> `colspan="N"` rozprostře buňku přes N sloupců (např. nadpis nad několika sloupci). `rowspan="N"` přes N řádků (např. kategorie napravo od několika subkategorií). **Lze kombinovat**: `<td colspan="2" rowspan="3">` = buňka 2×3. Past při rowspan: další řádky **NEDOSTÁVAJÍ** první `<td>` — je pokrytý rowspan.

# CODE: Napiš minimální tabulku s thead/tbody.
```html
<table>
    <caption>Studenti</caption>
    <thead>
        <tr>
            <th scope="col">Jméno</th>
            <th scope="col">Věk</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <th scope="row">Anna</th>
            <td>25</td>
        </tr>
        <tr>
            <th scope="row">Petr</th>
            <td>30</td>
        </tr>
    </tbody>
</table>
```

# CODE: Tabulka s colspan a rowspan.
```html
<table>
    <thead>
        <tr>
            <th>Den</th>
            <th>Dopoledne</th>
            <th>Odpoledne</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Pondělí</td>
            <td colspan="2">Workshop celý den</td>
        </tr>
        <tr>
            <td rowspan="2">Úterý-Středa</td>
            <td>Přednáška</td>
            <td>Cvičení</td>
        </tr>
        <tr>
            <!-- první td chybí — pokrytý rowspan ze řádku výše -->
            <td>Přednáška</td>
            <td>Lab</td>
        </tr>
    </tbody>
</table>
```

# CODE: BEM stylování tabulky.
```css
.schedule {
    border-collapse: collapse;
    width: 100%;
}
.schedule__head {
    background: #1a1a2e;
}
.schedule__head th {
    color: white;
    padding: 0.8rem;
}
.schedule td {
    padding: 0.8rem;
    border: 1px solid #ddd;
    text-align: center;
}
.schedule__event--break {
    color: red;
    font-style: italic;
}
```

# CODE: Zebra řádky se hoverem.
```css
tbody tr:nth-child(odd) {
    background: #f5f5f5;
}
tbody tr:hover {
    background: #fffae5;
}
```

# Q: Jak udělat tabulku responzivní na mobilech?
A: Nejjednodušší: **horizontální scroll** přes `overflow-x: auto` na obalu — `<div style="overflow-x: auto;"><table>...</table></div>`. Pokročilejší: skrýt méně důležité sloupce přes `@media (max-width: 600px) { .skryt-na-mobil { display: none; } }`.

# Q: K čemu slouží `<colgroup>` `<col>` styling omezení?
A: `<col>` lze stylovat jen `background`, `border`, `width`, `visibility`. **Nelze**: `padding`, `font`, `color` — ty patří na buňky `<td>`/`<th>`. Důvod: rendering optimalizace.

# Q: Lze `<th>` umístit i v `<tbody>`?
A: Ano. Klasické pro **hlavičky řádků** (např. první sloupec = popisek řádku). Použij `<th scope="row">` pro přístupnost. Příklad: telefonní seznam, kde každý řádek je osoba a jméno je v `<th scope="row">`.
