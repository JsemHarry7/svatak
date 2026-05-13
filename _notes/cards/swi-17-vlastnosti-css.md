---
title: SWI 17 — Vlastnosti CSS
description: CSS jednotky, barvy, proměnné, písma a ligatury
tags: [maturita, swi, web, css, jednotky, barvy, typografie]
---

# Q: Jaký je rozdíl mezi `em` a `rem`?
A: `em` je relativní k font-size **rodičovského** elementu (kaskáduje — `em` v `em` se násobí). `rem` je relativní k font-size **kořenového** elementu (`<html>`) — vždy předvídatelný. Pro typografii preferuj `rem`.

# Q: Vyjmenuj relativní jednotky CSS.
A: `em` (k rodiči), `rem` (k root), `%` (k vlastnosti rodiče), `vw` (% šířky viewportu), `vh` (% výšky viewportu), `vmin`/`vmax`.

# CLOZE: V CSS znamená `100vw` 100 % šířky {{viewportu}} (okna prohlížeče). `100%` znamená 100 % {{rodičovského}} elementu.

# CLOZE: CSS proměnnou definujeme syntaxí `--{{nazev}}: hodnota;` typicky v `:root` selektoru. Použijeme ji přes funkci `{{var}}(--nazev)`.

# CLOZE: HSL formát barev se skládá z {{hue}} (odstín 0–360°), {{saturation}} (sytost 0–100 %) a {{lightness}} (jas 0–100 %).

# CLOZE: Pro načtení vlastního fontu z externího souboru se používá pravidlo `{{@font-face}}`.

# MCQ: Jaká je nejlepší jednotka pro typografii kvůli přístupnosti?
- px
- !rem
- vw
- %
> `rem` respektuje uživatelské nastavení font-size v prohlížeči. Uživatel s problémy se zrakem může zvětšit text v nastavení; `rem`-based design ho respektuje, `px`-based ne.

# MCQ: Co je výhoda HSL oproti HEX pro design system?
- !Snadné variace (snížíš jas, dostaneš tmavší verzi stejné barvy bez přepočítávání)
- HSL je rychlejší
- HSL má víc barev
- HEX nepodporují všechny browsery
> HSL umožňuje měnit jen jeden kanál (např. Lightness) pro variaci. V HEX bys musel přepočítávat. Pro hover/active stavy a design system výrazně lepší.

# MCQ: Co znamená `0.5` v `rgba(0, 0, 0, 0.5)`?
- Saturation
- !Alpha (průhlednost) — 0 = úplně průhledné, 1 = neprůhledné
- Hue
- Velikost
> Alpha kanál řídí průhlednost. `rgba(0,0,0,0.5)` = poloprůhledná černá.

# MCQ: Kde jsou CSS proměnné dostupné, když je definuješ na `.card` selectoru?
- Globálně
- !Pouze uvnitř `.card` a všech jeho potomků v DOM
- Pouze v `.card` samotném
- Pouze v stejném CSS souboru
> CSS proměnné kaskádují DOM hierarchií. Definované na `.card` jsou dostupné uvnitř `.card` a jeho potomků, ne sourozencům ani mimo. Globální = `:root`.

# MCQ: Co dělá `font-display: swap` v `@font-face`?
- Zaměňuje fonty náhodně
- !Během načítání vlastního fontu zobrazí fallback písmo, pak se prohodí
- Vypne načítání fontu
- Optimalizuje barvy
> `swap` zabraňuje FOIT (Flash of Invisible Text). Bez něj může být text neviditelný, dokud se font nenačte. Lepší UX.

# FREE: Vysvětli rozdíl mezi `em`, `rem` a `%` v CSS.
> `em` je relativní k font-size rodiče — kaskáduje, takže nesting násobí (1.5em ve 1.5em rodiči = 2.25em base). `rem` je relativní k font-size root elementu (`<html>`) — vždy předvídatelný. `%` se chová podle konkrétní vlastnosti: `width: 50%` = 50 % šířky rodiče, `font-size: 50%` = 50 % font-size rodiče. Pro typografii preferuj `rem`, pro layout `%` nebo `fr`.

# FREE: Vyjmenuj formáty CSS barev s příklady.
> **Named** (klíčová slova): `red`, `blue`, `gainsboro` — 140 předdefinovaných jmen. **HEX**: `#FF0000` — šestimístný hexadecimální kód (kompaktní). **RGB**: `rgb(255, 0, 0)` — kanály červená/zelená/modrá. **RGBA**: `rgba(0, 0, 0, 0.5)` — RGB + alpha (průhlednost). **HSL**: `hsl(0, 100%, 50%)` — hue/saturation/lightness (intuitivní). **HSLA**: HSL + alpha. **Moderní**: `oklch()`, `color-mix()` — HDR podpora.

# FREE: Vysvětli CSS proměnné a jejich výhody.
> CSS proměnné (custom properties) jsou uživatelsky definované hodnoty. Definice: `--main-color: #1b7f5f;` typicky v `:root` (globální). Použití: `color: var(--main-color);`. Výhody: 1) jedno místo pro klíčové hodnoty (změníš jednou, projeví se všude), 2) theme switching přes `[data-theme="dark"]`, 3) runtime dynamika (JS může měnit), 4) DRY princip.

# FREE: Popiš proces přidání vlastního fontu na web.
> Použiju `@font-face` pravidlo v CSS. Uvnitř specifikuji `font-family` (jméno, kterým ho budu volat), `src: url(...) format('woff2'), url(...) format('woff')` (cesta k souborům — woff2 primární, woff fallback), volitelně `font-weight`, `font-style`, `font-display: swap` (anti-FOIT). Potom v ostatních pravidlech používám jméno jako jakýkoli font: `font-family: 'MujFont', sans-serif;`.

# CODE: Napiš CSS proměnné v `:root` a jejich použití.
```css
:root {
    --main-color: #1b7f5f;
    --base-spacing: 1rem;
    --font-stack: 'Inter', sans-serif;
}

.button {
    background: var(--main-color);
    padding: var(--base-spacing);
    font-family: var(--font-stack);
}
```

# CODE: Napiš theme switching přes CSS proměnné.
```css
:root {
    --bg: white;
    --text: black;
}
[data-theme="dark"] {
    --bg: #1a1a1a;
    --text: #f0f0f0;
}
body {
    background: var(--bg);
    color: var(--text);
}
```

# CODE: Napiš `@font-face` deklaraci.
```css
@font-face {
    font-family: 'MujFont';
    src: url('/fonts/MujFont.woff2') format('woff2'),
         url('/fonts/MujFont.woff') format('woff');
    font-weight: 400;
    font-style: normal;
    font-display: swap;
}

body {
    font-family: 'MujFont', sans-serif;
}
```

# CODE: Vytvoř hover variace tlačítka přes HSL.
```css
.btn { background: hsl(150, 60%, 40%); }
.btn:hover { background: hsl(150, 60%, 30%); }       /* tmavší */
.btn:disabled { background: hsl(150, 20%, 40%); }    /* méně sytá */
```

# Q: Co jsou ligatury?
A: Grafické spojení dvou nebo více znaků do jednoho glyfu (symbolu). Typografická estetika — např. "fi" → propojený glyf. V ikonových fontech (Material Icons) může ligatura zastoupit celé slovo: text "favorite" → srdíčko. V CSS: `font-feature-settings: "liga" on;`.

# Q: K čemu slouží font fallback chain `'Inter', Arial, sans-serif`?
A: Browser zkouší fonty v pořadí, použije první dostupný. Custom font (Inter) → web-safe (Arial) → generická skupina (sans-serif). Bez fallback by chyběl font, browser by použil default.

# Q: Jaký je rozdíl mezi `font-weight: bold` a `font-weight: 700`?
A: Ekvivalentní. `bold` = 700, `normal` = 400. Číselné hodnoty (100–900 po 100) umožňují přesné odstínování — `300` (light), `500` (medium), `600` (semibold), `800` (extra-bold).
