---
title: DAT 6 — CSS animace
description: CSS transformace, přechody (transition), klíčové snímky (@keyframes), animation
tags: [maturita, dat, web, css, animace, keyframes, transition]
---

# Q: Jaký je rozdíl mezi `transition` a `@keyframes`?
A: **`transition`** spouští plynulou změnu vlastnosti **při změně stavu** (hover, focus, JS přidání třídy). Jen 2 stavy. **`@keyframes`** definuje animaci s **víc mezikroky** (0%, 50%, 100%), spouští se **automaticky** bez interakce, lze opakovat infinite, měnit směr.

# Q: Proč animovat `transform` a `opacity` místo `width` a `top`?
A: `transform` a `opacity` běží na **GPU** — nespouštějí reflow (přepočet layoutu). `width`, `height`, `top`, `left` běží na **CPU** — spouštějí reflow, browser přepočítává polohu všech ostatních prvků. Pro plynulé 60 FPS animace vždy transform + opacity.

# CLOZE: 2D transformace v CSS: `translate(x, y)` (posun), `scale(n)` (velikost), `rotate(Ndeg)` (otočení), `{{skew}}` (zkřivení).

# CLOZE: `animation-fill-mode: {{forwards}}` znamená, že prvek po dokončení animace zůstane v posledním klíčovém snímku.

# CLOZE: Pro infinite opakování animace se používá `animation-iteration-count: {{infinite}}`. Pro střídavé pendlování `animation-direction: {{alternate}}`.

# CLOZE: Pro respektování přístupnosti uživatelů s motion sensitivity slouží `@media (prefers-reduced-motion: {{reduce}})`.

# MCQ: Co dělá `transition: all 0.3s ease`?
- !Plynulé přechody všech animovatelných vlastností v délce 0.3s s ease křivkou
- Spustí animaci ihned
- Vypne všechny animace
- Nic
> `transition` shorthand: `property duration timing-function delay`. `all` = všechny vlastnosti, `0.3s` = délka, `ease` = křivka rychlosti.

# MCQ: Které vlastnosti **nelze** plynule animovat přes transition?
- color, opacity
- transform, width
- !display, visibility
- background-color, border-radius
> `display` a `visibility` přepínají skokově. Pro fade-out s display: none je potřeba kombinace opacity + animation s `forwards`.

# MCQ: Jak udělat nekonečný spinner?
- transition s infinite
- !`@keyframes spin { from { rotate(0) } to { rotate(360deg) } }` + `animation: spin 1s linear infinite`
- JS interval
- Background-image
> Transition nemůže opakovat. Pro infinite musí být `@keyframes` s `animation-iteration-count: infinite`. `linear` zajistí konstantní rychlost (bez easing oscilací).

# MCQ: Co je `transform-origin: top left`?
- !Bod, kolem kterého se transformace aplikuje, v levém horním rohu
- Pozice elementu
- Pozice cursoru
- Padding
> Default je `50% 50%` (střed). Použití: rotace kolem rohu (jako otevírající se kniha), scale od konkrétního bodu.

# MCQ: Co dělá `will-change: transform, opacity`?
- !Hint pro browser, aby předem připravil GPU vrstvu pro animaci
- Animuje automaticky
- Vypne animaci
- Nic, je deprecated
> `will-change` říká browseru, že chystáš animaci → připraví GPU vrstvu. NEPŘEHÁNĚJ — moc `will-change` zpomalí stránku. Použij jen na element, který opravdu animuješ.

# FREE: Vysvětli rozdíl mezi transition a keyframes.
> **Transition** reaguje na **změnu stavu** (hover, focus, JS přidání třídy). Jen dva stavy: před a po. Nelze opakovat. Klasické pro mikrointerakce (hover efekty). **Keyframes** definují animaci s víc mezikroky (0–100%), spouští se automaticky, lze opakovat infinite, měnit směr (alternate). Klasické pro loadery, slide-iny, složité animace. Pravidlo: *"reaguje na akci uživatele → transition, běží samo → keyframes"*.

# FREE: Vyjmenuj timing functions a kdy které.
> **`ease`** (default) — pomalý start, rychlý střed, pomalý konec. **`linear`** — konstantní (pro spinnery!). **`ease-in`** — pomalý start, rychlý konec (pro výstupy/zmizení). **`ease-out`** — rychlý start, pomalý konec (pro vstupy/objevení). **`ease-in-out`** — pomalý start + konec. **`cubic-bezier(x1,y1,x2,y2)`** — vlastní křivka. **`steps(n)`** — skoková animace.

# FREE: Co je `animation-fill-mode` a kdy je kritický?
> Určuje stav prvku **mimo** animaci (před delay nebo po dokončení). `none` (default) — vrátí se do CSS výchozího stavu. `forwards` — zůstane v posledním klíčovém snímku. `backwards` — během delay má hodnoty prvního snímku. `both` — kombinace. **Kritický pro slide-in notifikace**: bez `forwards` se notifikace po dokončení animace vrátí do výchozí pozice (zmizí). S `forwards` zůstane viditelná.

# FREE: Vysvětli `prefers-reduced-motion` a proč je důležitý.
> Media query, která detekuje uživatelovo OS nastavení **omezit pohyb** (epilepsie, kinetóza, ADHD). V CSS: `@media (prefers-reduced-motion: reduce) { * { animation: none; transition: none; } }`. Best practice = ne úplně vypnout, ale **omezit pohyb** (např. nahradit slide animaci za fade-only). Komise to **zkouší** — *"jak ošetřuješ animace pro přístupnost?"*.

# CODE: Hover karta která se zvedne při najetí.
```css
.card {
    transition: transform 0.3s ease, box-shadow 0.3s ease;
}
.card:hover {
    transform: translateY(-8px);
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
}
```

# CODE: Nekonečný spinner.
```css
@keyframes spin {
    from { transform: rotate(0deg); }
    to   { transform: rotate(360deg); }
}

.spinner {
    width: 40px;
    height: 40px;
    border: 4px solid #ccc;
    border-top-color: #3498db;
    border-radius: 50%;
    animation: spin 1s linear infinite;
}
```

# CODE: Slide-in notifikace s delay, zůstane viditelná.
```css
@keyframes slideIn {
    from { transform: translateX(100%); opacity: 0; }
    to   { transform: translateX(0); opacity: 1; }
}
.notification {
    opacity: 0;                              /* skrytá před začátkem */
    animation: slideIn 0.5s ease-out 0.5s forwards;
    /*                  trvani timing delay fill-mode */
}
```

# CODE: Tlačítko s hover + press feedback.
```css
.btn {
    background: #3498db;
    color: white;
    padding: 0.8rem 1.5rem;
    transition: background 0.2s ease, transform 0.1s ease;
}
.btn:hover { background: #2980b9; }
.btn:active { transform: scale(0.95); }      /* "press down" feedback */
```

# CODE: Respect prefers-reduced-motion.
```css
@media (prefers-reduced-motion: reduce) {
    *, *::before, *::after {
        animation-duration: 0.01ms !important;
        animation-iteration-count: 1 !important;
        transition-duration: 0.01ms !important;
    }
}
```

# Q: Co je rozdíl mezi `transition: all` a `transition: transform`?
A: `transition: all` animuje **všechny změny** všech vlastností. Méně efektivní — browser musí sledovat všechny změny. `transition: transform` jen konkrétní vlastnost — rychlejší, předvídatelnější. Pro produkční kód preferuj explicitní výčet (`transition: transform 0.2s, opacity 0.2s`).

# Q: Co se stane, když animuješ button `:active` s `scale(0.95)`?
A: Button se při kliku zmenší o 5 % — UX feedback "press down" (jako fyzické tlačítko). Konvenčně se používá `scale(0.95)` (mírné zmenšení), ne `scale(1.05)` (růst při kliku) — to by vypadalo nepřirozeně.

# Q: Co je shorthand `animation` v CSS?
A: `animation: name duration timing-function delay iteration-count direction fill-mode play-state`. Příklad: `animation: pulse 2s ease-in-out 0s infinite alternate forwards running`. Konvence: jméno nejdřív, ostatní v pořadí podle potřeby.
