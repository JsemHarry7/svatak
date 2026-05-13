---
title: DAT 1 — HTML5 a sémantika
description: Struktura stránky, sémantické tagy v HTML5, typografie na webu
tags: [maturita, dat, web, html, sémantika]
---

# Q: Co přinesl HTML5 oproti HTML4?
A: Zkrácený `<!DOCTYPE html>`, sémantické tagy (header, nav, main, article, section, aside, footer), nativní multimédia (`<audio>`, `<video>`, `<canvas>`), vylepšené formuláře (nové `type` hodnoty s automatickou validací), JavaScript API (localStorage, geolokace).

# Q: Vyjmenuj 7 hlavních sémantických tagů.
A: `<header>`, `<nav>`, `<main>` (1× per stránka), `<article>`, `<section>`, `<aside>`, `<footer>`.

# Q: Jaký je rozdíl mezi `<article>` a `<section>`?
A: **`<article>`** = samostatně smysluplný uzavřený obsah (vytrhneš ho, dává smysl) — blog post, komentář, produktová karta. **`<section>`** = tematická skupina obsahu uvnitř většího kontextu, má vlastní nadpis. Test: *"dává smysl na samostatné stránce?"* Ano → article, Ne → section.

# CLOZE: Element `<{{main}}>` má být na stránce právě jednou — obaluje hlavní unikátní obsah stránky.

# CLOZE: Pro alternativní popis obrázku, který čtečka obrazovky přečte slepému uživateli, slouží atribut `{{alt}}`. Pokud je obrázek dekorativní, použij `alt={{""}}`.

# CLOZE: HTML entita pro znak `<` je `{{&lt;}}`. Pro znak `&` je `{{&amp;}}`. Pro nedělitelnou mezeru `{{&nbsp;}}`.

# MCQ: Vyber pouze blokové elementy.
- !div, p, h1, section, header
- span, a, strong, em
- img, br, input
- (žádné)
> Blokové elementy zabírají celou šířku rodiče a začínají na novém řádku: div, p, h1-h6, section, article, header, footer, main. Inline: span, a, strong, em, img, br.

# MCQ: Který input type validuje formát emailu?
- type="text"
- !type="email"
- type="string"
- type="contact"
> HTML5 přidal nové input types s automatickou validací: email, url, tel, number, date, time, color, range. Browser kontroluje formát.

# MCQ: Co je rozdíl mezi `<strong>` a `<b>`?
- Není rozdíl
- !`<strong>` má sémantický význam (důležitost), `<b>` je jen vizuální tučnost
- `<b>` je deprecated
- `<strong>` je pomalejší
> Sémantické tagy preferuj. `<strong>` čtečka přečte s důrazem, `<b>` jen tučně. Pro maturitu používej `<strong>`/`<em>` místo `<b>`/`<i>`.

# MCQ: Co dělá `target="_blank"` u odkazu?
- Otevře stránku v iframe
- !Otevře v novém okně/tabu prohlížeče
- Stáhne soubor
- Vyvolá download
> `target="_blank"` = nový tab/okno. Bezpečnostně by se měl doplnit `rel="noopener"`, aby nová stránka nemohla manipulovat s rodičovskou.

# FREE: Popiš sémantickou strukturu typické blogové stránky.
> `<header>` se značkou webu a hlavní navigací (`<nav>`). `<main>` obsahuje primární obsah. Uvnitř `<article>` pro každý blog post — má vlastní `<header>` s nadpisem a datem (`<time>`), `<section>` pro části obsahu, případně `<footer>` s autorem. `<aside>` pro sidebar (související články). Globální `<footer>` se copyrightem a kontakty.

# FREE: Proč jsou sémantické tagy důležité?
> 1) **Přístupnost** — čtečky obrazovky chápou strukturu, mohou nabízet skoky mezi sekcemi. 2) **SEO** — Google rozumí, co je hlavní obsah, navigace, sidebar. 3) **Údržba kódu** — programátor po měsíci vidí strukturu, ne `<div>` soup. 4) **Výchozí styly** — sémantické tagy mají rozumné default vzhled.

# FREE: Vyjmenuj povinné meta tagy v `<head>` a vysvětli proč.
> `<meta charset="UTF-8">` — kódování pro správné zobrazení diakritiky. `<meta name="viewport" content="width=device-width, initial-scale=1.0">` — esenciální pro mobilní responzivitu (bez něj zoom-out). `<title>` — záložka v prohlížeči + SEO. Doporučené: `<meta name="description">` (Google ji zobrazuje pod titulkem ve výsledcích).

# CODE: Napiš HTML5 boilerplate.
```html
<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Stránka</title>
</head>
<body>
    <h1>Ahoj svete!</h1>
</body>
</html>
```

# CODE: Napiš formulář pro registraci s validací.
```html
<form action="/register" method="POST">
    <fieldset>
        <legend>Registrace</legend>

        <label for="jmeno">Jméno:</label>
        <input type="text" id="jmeno" name="jmeno" minlength="2" required>

        <label for="email">Email:</label>
        <input type="email" id="email" name="email" required>

        <label for="vek">Věk:</label>
        <input type="number" id="vek" name="vek" min="18" max="120">

        <label>
            <input type="checkbox" name="newsletter">
            Souhlasím s newsletter
        </label>
    </fieldset>
    <button type="submit">Odeslat</button>
</form>
```

# CODE: Napiš `<figure>` s obrázkem a popiskem.
```html
<figure>
    <img src="kytka.jpg" alt="Bílá kopretina">
    <figcaption>Kopretina na letní louce.</figcaption>
</figure>
```

# Q: Kdy použít `<header>` element a kdy `<h1>`?
A: `<header>` = sémantický element pro hlavičku stránky/sekce — obvykle obsahuje logo, nadpis, navigaci. `<h1>` = hlavní nadpis (jeden z header tagů h1-h6). `<header>` je krabice, `<h1>` text v ní. Klidně `<h1>` uvnitř `<header>`.

# Q: Proč potřebuje `<form>` `action` a `method`?
A: `action` = URL, kam se data odešlou. `method` = HTTP metoda (GET data v URL, POST data v body — bezpečnější pro hesla a větší data). Bez `action` se odešle na stejnou URL.

# Q: K čemu slouží `<label for="X">` a co je za podmínka?
A: Sémanticky propojuje popisek s konkrétním inputem (přístupnost, klik na label fokusuje input). `for` musí odpovídat `id` (NE `name`!) inputu. Alternativně lze input obalit do labelu — pak nepotřebuješ for/id.
