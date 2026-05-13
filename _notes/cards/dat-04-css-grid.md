---
title: DAT 4 — CSS Grid
description: CSS Grid — 2D layout, řádky, sloupce, oblasti, fr jednotka
tags: [maturita, dat, web, css, grid, layout]
---

# Q: Co je CSS Grid?
A: 2D layout systém v CSS pro řízení současně řádků a sloupců. Aktivuje se přes `display: grid` na rodičovském elementu. Optimalizován pro celostránkové layouty (header / sidebar / content / footer).

# Q: Jaký je rozdíl mezi Flexbox a Grid?
A: Flexbox = 1D (jeden směr — řádek NEBO sloupec). Grid = 2D (řádky A sloupce současně). Flexbox pro distribuci v jedné linii (navigace). Grid pro celé layouty (holy grail). Často se kombinují — Grid pro hlavní layout, Flexbox uvnitř každé buňky.

# CLOZE: Jednotka `{{fr}}` (fraction) dělí volný prostor v gridu. `1fr 2fr` rozdělí volné místo v poměru 1:2.

# CLOZE: V CSS Grid lze pojmenovat oblasti přes `grid-template-areas` a přiřadit položku přes `{{grid-area: jmeno}}`.

# CLOZE: V `grid-template-areas` se prázdná buňka označuje tečkou `{{.}}`. Stejný název pro víc buněk = jedna oblast přes víc buněk.

# MCQ: Co dělá `grid-template-columns: 250px 1fr 200px`?
- 3 sloupce stejné šířky
- !3 sloupce: první 250px, druhý flexibilní (zbytek prostoru), třetí 200px
- 250 sloupců
- Padding sloupců
> 3 hodnoty = 3 sloupce. Konkrétní velikosti (`px`, `rem`) jsou fixní. `fr` (fraction) je flexibilní — pohltí volný prostor.

# MCQ: Co je `fr` a v čem se liší od `%`?
- !`fr` dělí **volný** prostor (po odečtení gap a fixed tracků). `%` je z **celkové** šířky.
- Není rozdíl
- `fr` je rychlejší
- `%` je pro tisk
> `fr` zohledňuje gap a fixní sloupce. `1fr 1fr 1fr` s `gap: 20px` rozdělí volný prostor po odečtení gapů. `33% 33% 33%` by způsobil overflow (gap se nepočítá).

# MCQ: Jaký je rozdíl mezi `repeat(auto-fit, ...)` a `repeat(auto-fill, ...)`?
- !`auto-fit` stáhne prázdné tracky (existující obsah se roztáhne). `auto-fill` drží prázdné tracky (víc buněk i bez obsahu).
- Není rozdíl
- `auto-fit` je novější
- `auto-fill` jen v Chrome
> Subtle rozdíl. Pro responzivní galerii typicky `auto-fit` — obsah se rozprostře. `auto-fill` nechává mezery.

# MCQ: Co znamená `grid-column: 1 / -1`?
- Sloupec mínus jedna
- !Element se rozprostře od první čáry do poslední (přes všechny sloupce)
- Skryje sloupec
- Repeat 1× -1
> `-1` v grid-column je **poslední čára**. Užitečné pro full-width prvky (např. header) bez znalosti přesného počtu sloupců.

# MCQ: Jak vycentruješ obsah uvnitř buňky Gridu?
- text-align: center
- !`place-items: center` na rodiči (kontejneru) — kombinace `justify-items` + `align-items`
- margin: auto
- vertical-align: middle
> Grid má `justify-items` (horizontální) a `align-items` (vertikální) — shorthand `place-items`. Pro jednu položku přepsání: `justify-self` + `align-self`.

# FREE: Vysvětli holy grail layout v CSS Grid.
> Layout stránky: header nahoře (full width), middle obsahuje sidebar vlevo + content uprostřed + aside vpravo, footer dole (full width). V CSS Grid přes `grid-template-areas`: tři řádky, tři sloupce. Header "hd hd hd" přes všechny sloupce. Middle "sidebar content aside". Footer "ft ft ft". Sloupce: `grid-template-columns: 250px 1fr 200px` (sidebar pevný, content flex, aside pevný). Plus `min-height: 100vh` ať footer drží dole.

# FREE: Vysvětli `grid-template-areas` a jeho výhody.
> Nejčitelnější způsob Grid layoutu. Pojmenuje oblasti "ASCII art" stylem: každý řádek je řetězec v uvozovkách, slova oddělená mezerou. Stejný název = jedna oblast přes víc buněk. Tečka `.` = prázdné. Pak `grid-area: nazev` přiřadí položku. Výhody: 1) Layout vizuálně čitelný v CSS, 2) Responzivita = jen přepsat areas v media query. Příklad: `"header header" "sidebar main" "footer footer"`.

# FREE: Co je rozdíl mezi `auto`, `min-content`, `max-content` a `fr` v grid-template-columns?
> `auto` = velikost dle obsahu. `min-content` = minimální (nejdelší slovo). `max-content` = maximální (celý obsah bez zalomení). `fr` = flexibilní podíl volného prostoru. `minmax(min, max)` = rozsah, např. `minmax(200px, 1fr)`. Pro responzivní layouty často `minmax(250px, 1fr)` s `auto-fit`.

# FREE: Vysvětli responzivní Grid bez media query přes `auto-fit`.
> Pattern `repeat(auto-fit, minmax(250px, 1fr))` v grid-template-columns. Browser sám vytvoří tolik sloupců, kolik se vejde, každý minimálně 250 px (jinak by se nezalomil), max flexibilní (vyplní zbytek). Žádný media query, full responsivita. Klasické řešení pro karty galerii.

# CODE: Napiš holy grail layout s `grid-template-areas`.
```css
.grid-container {
    min-height: 100vh;
    display: grid;
    grid-template-columns: 250px 1fr 200px;
    grid-template-rows: auto 1fr auto;
    grid-template-areas:
        "header  header  header"
        "sidebar content aside"
        "footer  footer  footer";
    gap: 20px;
}
.header  { grid-area: header; }
.sidebar { grid-area: sidebar; }
.content { grid-area: content; }
.aside   { grid-area: aside; }
.footer  { grid-area: footer; }
```

# CODE: Responzivní mobil-fallback přes media query.
```css
@media (max-width: 768px) {
    .grid-container {
        grid-template-columns: 1fr;
        grid-template-areas:
            "header"
            "sidebar"
            "content"
            "aside"
            "footer";
    }
}
```

# CODE: Auto-fit responzivní galerie.
```css
.gallery {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
    gap: 20px;
}
```

# CODE: Položka přes víc buněk (line numbers).
```css
.title {
    grid-column: 1 / -1;     /* od první do poslední čáry (přes všechny sloupce) */
    grid-row: 1;
}
.featured {
    grid-column: 1 / 3;      /* od čáry 1 do čáry 3 (přes 2 sloupce) */
    grid-column: 1 / span 2; /* alternativně */
}
```

# Q: Jaký je rozdíl mezi `min-height: 100vh` a `height: 100vh` na grid containeru?
A: `height: 100vh` = **PŘESNĚ** 100 % viewportu. Když obsah přeteče (delší článek), prvky se mačkají nebo přetékají. `min-height: 100vh` = **alespoň** 100 %. Když je obsahu málo, footer se drží dole. Když je víc, kontejner roste. Pro full-page layout vždy `min-height`.

# Q: Co dělá `gap: 20px` v Gridu?
A: Definuje mezeru mezi řádky a sloupci (gutter). `gap: 20px 30px` = row-gap 20, column-gap 30. Funguje stejně jako u Flexboxu. Preferovat před margins mezi položkami.

# Q: K čemu slouží `place-content`?
A: Shorthand pro `align-content` + `justify-content` — distribuuje celou mřížku v kontejneru. `place-content: center` centruje grid jako celek (pokud nezabírá celý kontejner).
