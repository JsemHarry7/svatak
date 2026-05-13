---
title: DAT 3 — Flexbox
description: CSS Flexible Box Layout — 1D layout, kontejner, položky, osy, elasticita
tags: [maturita, dat, web, css, flexbox, layout]
---

# Q: Co je Flexbox?
A: CSS Flexible Box Layout Module — jednorozměrný (1D) layout systém pro rozmístění prvků v kontejneru po jedné dominantní ose. Aktivuje se přes `display: flex` na rodičovském elementu, jeho přímí potomci se stávají flex položkami.

# Q: Jaký je rozdíl mezi Flexbox a CSS Grid?
A: **Flexbox** je 1D (jeden směr — řádek NEBO sloupec). **Grid** je 2D (řádky A sloupce současně). Flexbox pro navigace, řady karet. Grid pro celostránkové layouty. Často se kombinují.

# CLOZE: Flexbox má dvě osy: {{hlavní}} osu (main axis) určenou vlastností `flex-direction` a {{křížovou}} osu (cross axis) kolmou na hlavní.

# CLOZE: Vlastnost `{{justify-content}}` zarovnává položky na **hlavní** ose. Vlastnost `{{align-items}}` zarovnává na **křížové** ose.

# CLOZE: Pro odsunutí jedné položky na opačný konec kontejneru se používá trik `{{margin: auto}}` (např. `margin-left: auto`).

# MCQ: Co dělá `display: flex` na rodiči?
- Změní rodiče na grid
- !Aktivuje flex kontext, přímí potomci se stávají flex položkami
- Změní rodiče na inline element
- Nic nedělá
> Pravidlo `display: flex` (nebo `inline-flex`) aktivuje flex layout. Pouze přímí potomci jsou flex položky — vnořené dále už ne.

# MCQ: Co dělá `flex-direction: column`?
- Sloučí sloupce
- !Změní hlavní osu na vertikální (položky pod sebou)
- Skryje sloupce
- Center vertikálně
> `flex-direction`: `row` (default — horizontální), `row-reverse`, `column` (vertikální), `column-reverse`. Změní směr hlavní osy.

# MCQ: Pro centrování prvku horizontálně i vertikálně přes Flexbox použiješ:
- !`display: flex; justify-content: center; align-items: center;` na rodiči
- `text-align: center` na rodiči
- `margin: auto` na potomkovi
- `display: center`
> Klasický flexbox centering: na rodiči `display: flex` + obě zarovnání. Funguje pro libovolný obsah, ne jen text.

# MCQ: Co dělá `flex: 1`?
- Šířka 1 pixel
- Šířka 1 %
- !Shorthand pro `flex: 1 1 0%` — položka roste rovnoměrně s ostatními
- Vypne flex
> `flex` shorthand: `flex-grow flex-shrink flex-basis`. `flex: 1` = `1 1 0%` = roste (1), zmenšuje se (1), výchozí 0. Všechny položky s `flex: 1` budou stejně velké.

# MCQ: Které values `justify-content` distribuují prostor mezi položkami?
- !space-between, space-around, space-evenly
- center, end, start
- baseline, stretch
- flex-grow, flex-shrink
> `space-between` = krajní u krajů, rovné mezery mezi. `space-around` = stejné mezery kolem každé (krajní poloviční). `space-evenly` = všechny mezery stejné včetně krajních.

# FREE: Vysvětli hlavní a křížovou osu Flexboxu.
> Hlavní osa (main axis) je primární směr, podél kterého jsou položky rozmísťovány. Její směr určuje `flex-direction`: `row` (default — horizontální), `column` (vertikální). Křížová osa (cross axis) je vždy kolmá k hlavní. `justify-content` řídí zarovnání na hlavní ose, `align-items` na křížové. Klíč: osy se přepínají podle `flex-direction`, takže "vertikální" zarovnání není absolutní.

# FREE: Vysvětli `flex-grow`, `flex-shrink` a `flex-basis`.
> Vlastnosti flex položky pro elasticitu. **`flex-basis`** = výchozí velikost před distribucí prostoru (např. `250px`, `auto`). **`flex-grow`** = bezrozměrné číslo, kolik volného prostoru pohltí (v poměru k ostatním). **`flex-shrink`** = ochota zmenšit se při nedostatku místa. Analogie pružiny: basis = klidová délka, grow = expanze, shrink = stlačitelnost. Shorthand: `flex: 1 1 0%`.

# FREE: Co je `flex-wrap` a kdy ho použiješ?
> `flex-wrap` rozhoduje, zda se položky zalomí do nového řádku/sloupce, když se nevejdou. Hodnoty: `nowrap` (default — zůstávají v jednom řádku, mohou přetéct), `wrap` (zalomí), `wrap-reverse` (zalomí opačně). Pro responzivní galerie karet typicky `flex-wrap: wrap` + `flex-basis: 250px` + `flex-grow: 1`.

# FREE: K čemu slouží `margin: auto` ve flex položce?
> Pohltí veškerý volný prostor v daném směru. Klasické použití: `margin-left: auto` na poslední položce navigace odtlačí ji na pravý okraj kontejneru, ale **logo a nav-links zůstávají vedle sebe vlevo**. To je rozdíl oproti `justify-content: space-between`, které by všechny tři položky rozdělilo.

# CODE: Napiš centrovaný element přes Flexbox.
```css
.parent {
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 100vh;
}
```

# CODE: Napiš responzivní galerii karet s wrap.
```css
.gallery {
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
}
.card {
    flex-grow: 1;
    flex-basis: 250px;
}
```

# CODE: Navbar s logem vlevo a akcemi vpravo (přes margin auto).
```html
<nav class="main-nav">
    <div class="logo">LOGO</div>
    <ul class="links">...</ul>
    <div class="actions">...</div>
</nav>
```
```css
.main-nav {
    display: flex;
    align-items: center;
    gap: 1rem;
}
.actions {
    margin-left: auto;
}
```

# CODE: Sticky button na spodek karty (vnořený flex column).
```css
.card {
    display: flex;
    flex-direction: column;
    height: 300px;
}
.card-button {
    margin-top: auto;
}
```

# Q: Co je "min-width: 0" past ve Flexbox?
A: Flex položka může přetéct kontejner, pokud obsahuje dlouhý nepřerušitelný text. Default `min-width: auto` znamená "nesmí se zmenšit pod velikost obsahu". Řešení: `min-width: 0` na položce povolí zmenšení a text se zalomí.

# Q: Co se stane s `align-items: stretch` (default)?
A: Položky se natáhnou na výšku kontejneru (na křížové ose). Proto všechny karty v jednom řádku mají automaticky stejnou výšku — bez explicitního nastavení.

# Q: K čemu slouží `gap` u flexboxu?
A: Definuje **mezeru mezi položkami**. Identicky funguje u Grid. `gap: 20px` (všechny mezery), `gap: 20px 30px` (row 20, column 30). Preferovat před `margin` mezi položkami — čistší, žádné double-margin problémy.
