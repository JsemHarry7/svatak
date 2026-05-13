---
title: SWI 16 — CSS kaskáda
description: CSS specificita, kaskáda, selektory, izolace (@layer), BEM
tags: [maturita, swi, web, css, kaskada, specificita, bem]
---

# Q: Jaké 3 faktory rozhodují o vítězném CSS pravidle?
A: 1) **Origin** (původ — User Agent / Author / User / !important), 2) **Specificita** (jak přesný je selektor), 3) **Source order** (které pravidlo je v kódu později). Vyhodnocují se v tomto pořadí.

# Q: Vyjmenuj 4 kategorie specificity v pořadí od nejvyšší.
A: 1) Inline styl (`style="..."` v HTML), 2) ID (`#id`), 3) Class + Atribut + Pseudo-třída (`.class`, `[attr]`, `:hover`), 4) Element + Pseudo-element (`p`, `::before`).

# CLOZE: Specificita se zapisuje jako 4-tuple {{(inline, ID, class+attr+pseudo, element+pseudo-element)}}.

# CLOZE: `!important` přebíjí standardní kaskádu, ale uživatelský `!important` má {{vyšší prioritu}} než autorský `!important` (kvůli přístupnosti).

# CLOZE: Selektor `#hlavni .btn` má specificitu {{(0, 1, 1, 0)}}.

# CLOZE: Pseudo-třída (např. `:hover`) se počítá jako {{třída}} (0, 0, 1, 0). Pseudo-element (např. `::before`) se počítá jako {{element}} (0, 0, 0, 1).

# MCQ: Která specificita vyhraje?
- (0, 0, 5, 0)
- (0, 0, 0, 10)
- !(0, 1, 0, 0)
- (0, 0, 4, 4)
> Porovnává se zleva doprava. Vyšší řád vyhrává. ID (sloupec 2) vždy vyhraje nad libovolným počtem tříd nebo elementů.

# MCQ: Co je rozdíl mezi `@media` a `@layer`?
- !@media aplikuje podmínkově podle viewport/zařízení (KDY). @layer řídí prioritu skupin pravidel napříč specificitou (KDO).
- @layer je starší
- Není rozdíl
- @media je jen pro tisk
> Klasická past! Mantra: *"@media KDY, @layer KDO"*. @media = responzivita. @layer = architektura priority.

# MCQ: Která pseudo-třída vybere první prvek mezi svými sourozenci?
- :first
- !:first-child
- :first-of-type
- :nth-of-type(1)
> `:first-child` vybere element, který je prvním dítětem svého rodiče. `:first-of-type` vybere první svého typu. Často se zaměňují.

# MCQ: Spočítej specificity selektoru `nav ul li.active`.
- (0, 0, 0, 3)
- !(0, 0, 1, 3)
- (0, 1, 0, 3)
- (0, 0, 4, 0)
> 3 elementy (nav, ul, li) + 1 třída (.active) = (0, 0, 1, 3). Klasická past: zapomeneš počítat třídu v kombinátorovém řetězci.

# MCQ: V BEM konvenci pro tlačítko v kartě, které je primary variantou, jak vypadají třídy?
- .card_button_primary
- !.card__button + .card__button--primary
- .card-button.primary
- .card>button.primary
> BEM: Block__Element--Modifier. Dvojité podtržítko (`__`) pro element, dvojitá pomlčka (`--`) pro modifier. Modifier se přidává k základnímu elementu, nesubstituuje ho.

# FREE: Vysvětli, co dělá `@layer` a jak souvisí se specificitou.
> @layer (kaskádové vrstvy) poskytuje novou dimenzi priority, která **přebíjí specificity uvnitř vrstev**. Vrstvy se deklarují v pořadí (`@layer reset, base, components, utilities`), pozdější vrstva má vyšší prioritu — bez ohledu na specificity selektorů uvnitř. Tím se řeší "specificity wars" — vývojáři nemusí cpát ID selektory a !important k přebití. Architectural řešení.

# FREE: Vysvětli kombinátory CSS a jejich specificity.
> **Potomek** (mezera) — `div p` všechny p uvnitř div. **Dítě** (`>`) — `ul > li` jen přímé li. **Sousední sourozenec** (`+`) — `h1 + p` první p přímo po h1. **Obecný sourozenec** (`~`) — `h1 ~ p` všechny p po h1 na stejné úrovni. Specificita kombinátoru = **součet** specificit jednotlivých selektorů.

# FREE: Vysvětli princip BEM a proč ho používáme.
> BEM = Block (komponenta jako `card`), Element (část bloku jako `card__title`), Modifier (varianta jako `card--featured` nebo `card__button--primary`). Pravidla: dvojité podtržítko pro element (`block__element`), dvojitá pomlčka pro modifier (`block--modifier` nebo `block__element--modifier`). Důvody: 1) plochá specificita (vše jsou jen třídy 0,0,1,0), 2) žádné kolize jmen, 3) self-documenting — z názvu třídy vidíš, kam patří.

# FREE: Vyjmenuj 5 origin priorit kaskády od nejnižší.
> 1) User Agent Styles (výchozí browseru), 2) Author Styles (developer CSS), 3) User Styles (uživatelské, např. browser extension), 4) Author `!important`, 5) User `!important`. Pravidla s vyšším číslem přebíjí nižší. `!important` otáčí pořadí mezi User a Author (vyšší = User !important pro přístupnost).

# CODE: Napiš CSS s několika selektory různé specificity.
```css
p { color: black; }              /* (0,0,0,1) */
.text { color: blue; }            /* (0,0,1,0) */
#hlavni { color: red; }           /* (0,1,0,0) */
.btn:hover { color: green; }      /* (0,0,2,0) — třída + pseudo-třída */
```

# CODE: Napiš BEM CSS pro tlačítko a jeho varianty.
```css
.btn {
    padding: 0.5rem 1rem;
    border: 0;
    cursor: pointer;
}
.btn--primary {
    background: blue;
    color: white;
}
.btn--large {
    font-size: 1.5rem;
    padding: 0.75rem 1.5rem;
}
```

# CODE: Napiš @media query pro tmavé téma podle preference uživatele.
```css
@media (prefers-color-scheme: dark) {
    body {
        background: #111;
        color: #eee;
    }
}
```

# CODE: Napiš @layer pro architekturu CSS projektu.
```css
@layer reset, base, components, utilities;

@layer base {
    h1 { font-size: 2rem; }
}

@layer components {
    .btn { padding: 0.5rem 1rem; }
}
```

# Q: Co se počítá jako třída při výpočtu specificity?
A: Třída (`.class`), atribut (`[type="text"]`) a pseudo-třída (`:hover`, `:focus`, `:nth-child`) — všechno do stejné kategorie (0, 0, 1, 0). Pseudo-element (`::before`, `::after`) se počítá jako element (0, 0, 0, 1).

# Q: Co dělá `:not(selektor)`?
A: Vybere elementy, které NEodpovídají vnitřnímu selektoru. Specificita je dána obsaženým selektorem — `:not(#id)` má specificitu ID (0,1,0,0).
