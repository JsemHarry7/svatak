---
title: SWI 15 — Webová stránka
description: HTML tagy, atributy, struktura stránky, header tagy
tags: [maturita, swi, web, html, sémantika]
---

# Q: Co je HTML?
A: HyperText Markup Language — značkovací jazyk pro strukturu webové stránky. Není programovací (žádné proměnné, smyčky), jen popisuje, co je co. Skládá se z tagů, které obalují obsah a dávají mu sémantický význam.

# Q: Jaký je rozdíl mezi HTML, CSS a JS?
A: HTML = struktura (kostry budovy). CSS = vzhled (omítka, barvy). JS = chování (výtahy, interakce). Trojice základních webových technologií.

# CLOZE: Párový tag má {{otevírací}} a {{uzavírací}} značku (např. `<p>...</p>`). Nepárový tag stojí sám (např. `<img>`, `<br>`, `<meta>`).

# CLOZE: Atribut `{{id}}` musí být unikátní na celé stránce. Atribut `{{class}}` může být na více elementech a element může mít více tříd oddělených mezerou.

# CLOZE: Hierarchie nadpisů je {{h1}}–{{h6}}. Na stránce by měl být `<h1>` jen jednou (hlavní nadpis).

# MCQ: Proč musí mít `<img>` atribut `alt`?
- Pro SEO
- Pro přístupnost
- Pro failover (když se obrázek nenačte)
- !Všechno výše uvedené
> `alt` je povinný kvůli: 1) přístupnost (čtečka obrazovky čte slepému uživateli), 2) failover (obrázek se nenačte, zobrazí se alt text), 3) SEO (Google indexuje obrázky podle alt). Pro čistě dekorativní obrázky `alt=""`.

# MCQ: Co dělá `<!DOCTYPE html>`?
- Nastaví barvu stránky
- !Říká browseru, že je to HTML5 a aby nepoužil "quirks mode"
- Definuje šablonu stránky
- Importuje knihovny
> Bez DOCTYPE prohlížeč jede v quirks mode (kompatibilita se starými stránkami, nepředvídatelné chování). Vždycky první řádek HTML souboru.

# MCQ: Který tag je sémantický (nese význam)?
- div
- span
- !article
- (žádný)
> `<article>` = samostatný uzavřený obsah (blog post, novinový článek). Sémantický tag říká *co tahle část znamená*. `<div>` a `<span>` jsou bez sémantiky — jen kontejnery pro stylování.

# MCQ: Co je rozdíl mezi `<header>` a `<h1>`?
- Není rozdíl
- !`<header>` je sémantický element pro hlavičku stránky/sekce, `<h1>` je hlavní nadpis (jeden z header tagů h1-h6)
- `<h1>` je novější než `<header>`
- `<header>` je deprecated
> `<header>` je krabice, `<h1>` je text v ní. Klidně může být `<h1>` uvnitř `<header>`.

# FREE: Vyjmenuj sémantické HTML5 tagy a kdy je použít.
> `<header>` — hlavička stránky nebo sekce (logo, navigace, název). `<nav>` — navigační odkazy. `<main>` — hlavní unikátní obsah (1× per stránka). `<article>` — samostatný uzavřený obsah (blog post, komentář, produkt). `<section>` — tematická skupina obsahu s vlastním nadpisem. `<aside>` — doplňkový obsah (sidebar). `<footer>` — patička stránky nebo sekce.

# FREE: Popiš strukturu HTML5 dokumentu.
> Začíná `<!DOCTYPE html>`, pak `<html lang="cs">` s atributem jazyka. Uvnitř `<head>` s metadaty (charset, viewport, title, link na CSS, meta description) a `<body>` s viditelným obsahem. Standardní sémantická struktura v body: header → main (article + aside) → footer.

# FREE: Vyjmenuj 3 globální a 3 specifické atributy.
> Globální (fungují na jakémkoli tagu): `id` (unikátní identifikátor), `class` (třída pro CSS/JS), `style` (inline CSS), `title` (tooltip), `data-*` (vlastní data). Specifické (pro konkrétní tag): `href` (na `<a>`), `src` (na `<img>`, `<script>`, `<iframe>`), `alt` (na `<img>`), `type` (na `<input>`, `<button>`), `action` a `method` (na `<form>`).

# FREE: Co patří do `<head>` a proč je důležitý viewport meta tag?
> Do head patří metadata (neviditelné kromě title): `<meta charset="UTF-8">` (kódování), `<meta name="viewport" ...>` (responzivita), `<title>` (záložka), `<link rel="stylesheet">` (CSS), `<meta name="description">` (SEO), `<link rel="icon">` (favicon). Viewport meta tag je **esenciální** pro mobil — bez něj browser zobrazí stránku v defaultní šířce 980 px zoom-out, nečitelné.

# CODE: Napiš minimální HTML5 dokument.
```html
<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Moje stránka</title>
</head>
<body>
    <h1>Ahoj svete!</h1>
</body>
</html>
```

# CODE: Napiš sémantickou strukturu blogového článku.
```html
<body>
    <header>
        <h1>Můj blog</h1>
        <nav>
            <ul>
                <li><a href="/">Domů</a></li>
                <li><a href="/o-mne">O mně</a></li>
            </ul>
        </nav>
    </header>
    <main>
        <article>
            <header>
                <h2>Název článku</h2>
                <time datetime="2026-05-12">12. května 2026</time>
            </header>
            <section>
                <p>Obsah...</p>
            </section>
            <footer>Autor: ...</footer>
        </article>
    </main>
    <footer>&copy; 2026</footer>
</body>
```

# CODE: Napiš odkaz, který otevře externí stránku v novém tabu bezpečně.
```html
<a href="https://example.com" target="_blank" rel="noopener">Odkaz</a>
```

# Q: Co je rozdíl mezi `<strong>` a `<b>`?
A: Sémanticky: `<strong>` = důležitost (skutečné zvýraznění), `<b>` = jen vizuální tučnost bez sémantického významu. Pro maturitu používej `<strong>` a `<em>` (důraz) místo `<b>` a `<i>`.

# Q: Co je `<figure>` + `<figcaption>`?
A: Sémantická skupina pro obrázek/diagram s popiskem. `<figure>` obalí obsah (typicky `<img>`), `<figcaption>` je popisek. Vhodné pro články — kapitola s obrázkem.

# Q: K čemu slouží HTML entity?
A: Pro znaky, které mají v HTML zvláštní význam: `&lt;` = `<`, `&gt;` = `>`, `&amp;` = `&`, `&quot;` = `"`, `&nbsp;` = nedělitelná mezera, `&copy;` = ©. Pokud chceš zobrazit `<` jako text, musíš použít entitu.
