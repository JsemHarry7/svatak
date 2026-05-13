---
title: DAT 2 — Bootstrap
description: Bootstrap framework, grid systém, komponenty, utility classes
tags: [maturita, dat, web, bootstrap, framework]
---

# Q: Co je Bootstrap?
A: Nejpopulárnější CSS framework (Twitter, 2011) pro rychlou tvorbu responzivních webů. Poskytuje grid systém (12 sloupců), hotové komponenty (navbar, cards, modals) a utility classes (mt-3, d-flex). V5 (2021) bez závislosti na jQuery.

# Q: Vyjmenuj 5 (+1) breakpointů Bootstrap 5.
A: (žádný) < 576px (extra small), `sm` ≥ 576px, `md` ≥ 768px, `lg` ≥ 992px, `xl` ≥ 1200px, `xxl` ≥ 1400px.

# Q: Co znamená `col-12 col-md-6 col-lg-4`?
A: Responzivní sloupec: mobile (< md) = 12/12 (plná šířka), tablet (md+) = 6/12 (polovina), desktop (lg+) = 4/12 (třetina). Mobil 1 sloupec → tablet 2 → desktop 3.

# CLOZE: Bootstrap grid je založený na {{12}} sloupcích a flexboxu. Struktura: `.container` → `.{{row}}` → `.{{col}}-*`.

# CLOZE: `.container` má max-width rostoucí s breakpointy. `.{{container-fluid}}` je vždy 100 % šířky.

# CLOZE: Bootstrap je {{mobile-first}} — breakpoint prefix znamená "od této velikosti nahoru".

# CLOZE: Pro propojení s Bootstrap JavaScript komponentami (modal, collapse, dropdown) se používají atributy `{{data-bs-toggle}}` a `{{data-bs-target}}`.

# MCQ: Co dělá `mx-auto` v Bootstrap?
- Margin x 100% (full width)
- !Margin-left: auto + margin-right: auto (horizontální centrování)
- Maximum margin
- Margin reset
> `mx-*` = margin x-axis (horizontální). `mx-auto` centruje element (typicky `.container` nebo image).

# MCQ: Která třída udělá flex kontejner v Bootstrapu?
- .flexbox
- !.d-flex
- .container-flex
- .flex
> `d-flex` = display: flex. Plus modifikátory: `flex-row`, `flex-column`, `justify-content-center`, `align-items-end`.

# MCQ: Co znamená `g-3` na `.row`?
- Grid 3 sloupce
- !Gap mezi sloupci (gutter)
- 3-pixel border
- Group 3
> `g-*` (0-5) = gutter (mezera mezi sloupci v row). `g-3` ≈ 1rem. Plus `gx-*` (jen horizontální), `gy-*` (jen vertikální).

# MCQ: Pro 3 stejné karty v gridu — který přístup je čistší v moderním Bootstrapu?
- 3× `<div class="col-12 col-md-4">` na každé kartě
- !`<div class="row row-cols-1 row-cols-md-3">` na rodiči, `<div class="col">` na dětech
- Obojí je stejné
- Žádné z těchto
> `row-cols-*` je modernější pattern pro uniformní grid. Pravidlo na rodiči, ne opakování col-* na každém dítěti. Pro různě široké sloupce klasický `col-X-Y` lepší.

# MCQ: Co je rozdíl mezi Bootstrap a Tailwind CSS?
- Není rozdíl
- !Bootstrap má komponenty (`.btn`, `.card`) + utility. Tailwind má jen utility, žádné komponenty.
- Bootstrap je novější
- Tailwind je menší
> Filozofie: Bootstrap = "přidej třídy, máš design". Tailwind = "sestav si vlastní design z utility tříd" (utility-first).

# FREE: Vysvětli Bootstrap grid systém s příkladem responzivního layoutu.
> Grid je založený na 12 sloupcích + flexboxu. Struktura: `.container` → `.row` → `.col-*`. Pro responzivitu se kombinují třídy: `<div class="col-12 col-md-6 col-lg-4">` = mobil plná šířka, tablet polovina, desktop třetina. Bootstrap je mobile-first, breakpointy `sm/md/lg/xl/xxl` přidávají styly nahoru.

# FREE: Vyjmenuj klíčové utility classes Bootstrap (skupiny + příklady).
> **Spacing**: `m-3`, `p-2`, `mt-4`, `mb-3`, `mx-auto`, `py-5` (0–5 + auto). **Display**: `d-flex`, `d-none`, `d-md-block` (responzivní skrytí). **Text**: `text-center`, `text-primary`, `fw-bold`, `lead`. **Barvy**: `bg-primary`, `bg-dark`, `text-white`. **Flex**: `justify-content-center`, `align-items-end`, `flex-column`. **Velikost**: `w-100`, `h-100`, `vh-100`.

# FREE: Popiš strukturu Bootstrap navbar s mobile collapse.
> `<nav class="navbar navbar-expand-lg navbar-dark bg-dark">` jako kořen. Uvnitř `.container-fluid`. `<a class="navbar-brand">` pro logo. `<button class="navbar-toggler" data-bs-toggle="collapse" data-bs-target="#navId">` pro hamburger. `<div class="collapse navbar-collapse" id="navId">` jako rozbalovací obsah s `<ul class="navbar-nav">` a `<li class="nav-item"><a class="nav-link">`.

# CODE: Napiš základní Bootstrap setup s CDN.
```html
<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Bootstrap</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <!-- obsah -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
```

# CODE: Napiš responzivní 3-sloupcový card grid.
```html
<div class="container my-4">
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-3">
        <div class="col">
            <div class="card h-100">
                <img src="..." class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title">Název</h5>
                    <p class="card-text">Text</p>
                    <a href="#" class="btn btn-primary">Akce</a>
                </div>
            </div>
        </div>
        <!-- víc karet... -->
    </div>
</div>
```

# CODE: Napiš Bootstrap navbar s collapse.
```html
<nav class="navbar navbar-expand-lg navbar-dark bg-dark">
    <div class="container-fluid">
        <a class="navbar-brand" href="#">Logo</a>
        <button class="navbar-toggler" type="button"
                data-bs-toggle="collapse" data-bs-target="#navMenu">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navMenu">
            <ul class="navbar-nav">
                <li class="nav-item"><a class="nav-link" href="#">Domů</a></li>
                <li class="nav-item"><a class="nav-link" href="#">O nás</a></li>
            </ul>
        </div>
    </div>
</nav>
```

# CODE: Napiš formulář v Bootstrap.
```html
<form>
    <div class="mb-3">
        <label for="email" class="form-label">Email</label>
        <input type="email" id="email" class="form-control" required>
    </div>
    <div class="mb-3 form-check">
        <input type="checkbox" id="check1" class="form-check-input">
        <label for="check1" class="form-check-label">Souhlasím</label>
    </div>
    <button type="submit" class="btn btn-primary">Odeslat</button>
</form>
```

# Q: Jaké jsou Bootstrap theme barvy?
A: `primary` (modrá), `secondary` (šedá), `success` (zelená), `danger` (červená), `warning` (žlutá), `info` (světle modrá), `light`, `dark`. Použití: `btn-primary`, `bg-success`, `text-danger`, `border-warning`.

# Q: Co je rozdíl mezi `btn-primary` a `btn-outline-primary`?
A: `btn-primary` = vyplněný background v theme barvě. `btn-outline-primary` = pouze border a text v té barvě, transparent background. Pro sekundární akce se hodí outline.

# Q: Proč musí být Bootstrap JS na konci `<body>`?
A: Aby DOM byl načtený před spuštěním JS. JS hledá elementy přes ID/třídy a vázal by se na ně — kdyby běžel před vytvořením elementů, nefungoval by. Klasická praxe pro všechny `<script>` které manipulují DOM.
