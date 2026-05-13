---
title: SWI 14 — Návrh obsahového webu
description: Workflow tvorby webu, UX, UI, SEO, responzivita, viewport
tags: [maturita, swi, web, ux, ui, seo, responsivita]
---

# Q: Co je obsahový web a v čem se liší od e-shopu?
A: Obsahový web předává hodnotný obsah (vzdělávat, informovat). E-shop prodává produkty. Měří se jinak: obsahový web měří návštěvnost a engagement, e-shop konverze a obrat.

# Q: Vyjmenuj fáze workflow tvorby webu.
A: 1) Analýza (cílová skupina, cíle, konkurence). 2) Informační architektura (sitemap). 3) Wireframy + prototypy (UX). 4) Grafický návrh (UI). 5) Implementace (HTML/CSS/JS). 6) Testování. 7) Nasazení a údržba.

# Q: Jaký je rozdíl mezi UX a UI?
A: UX (User Experience) = jak web **funguje** (struktura, navigace, user flow, performance). UI (User Interface) = jak web **vypadá** (barvy, fonty, layout, animace). UX je o chování, UI o vzhledu.

# CLOZE: {{Wireframe}} je návrh rozložení prvků bez grafiky (UX fáze). {{Mock-up}} (nebo UI design) je finální vzhled s barvami a fonty.

# CLOZE: {{Mobile-first}} přístup začíná CSS pro mobil a přidává styly pro větší přes `min-width` media queries. {{Desktop-first}} začíná desktop a používá `max-width` pro mobil.

# CLOZE: {{Lighthouse}} je nástroj v Chrome DevTools, který hodnotí stránku na škále 0–100 v kategoriích Performance, Accessibility, Best Practices a SEO.

# MCQ: Co je SEO?
- !Search Engine Optimization — úprava stránek pro lepší dohledatelnost v organických výsledcích vyhledávačů
- Secure Encrypted Output
- Server Engine Operation
- Style Editor Online
> SEO = optimalizace pro vyhledávače. Cíl: dostat se na první příčky Google/Seznam pro klíčová slova.

# MCQ: Které z následujících NEJVÍCE ovlivňuje on-page SEO?
- Barva pozadí
- !`<title>`, meta description, sémantické HTML, alt obrázků, rychlost načítání
- Verze prohlížeče
- Počet návštěvníků
> On-page SEO faktory: title tag, meta description, hierarchie nadpisů, sémantické tagy (article, nav), alt atributy, sémantické URL, Core Web Vitals (rychlost).

# MCQ: Proč je dnes mobile-first preferovaný přístup?
- Mobil je rychlejší
- !Většina internetového trafficu jde z mobilů + Google používá mobile-first indexing od 2018
- Desktop je zastaralý
- Browsery mobile-first podporují lépe
> Google **mobile-first indexing** od 2018 znamená, že Google indexuje mobilní verzi stránky jako primární pro hodnocení — i pro desktop dotaz. Plus >50 % trafficu z mobilů.

# MCQ: Co dělá `<meta name="viewport" content="width=device-width, initial-scale=1.0">`?
- !Říká mobilnímu browseru, aby šířka odpovídala šířce zařízení a výchozí zoom byl 1:1
- Nastaví viewport pro desktop
- Definuje barvu pozadí
- Vypne responzivitu
> Bez tohoto meta tagu mobil zobrazí stránku v defaultní šířce (~980 px) zoom-out. Esenciální pro responzivitu.

# FREE: Vyjmenuj UX faktory.
> Struktura stránek, navigace, rozvržení prvků (layout), responzivní design, user flow (cesta uživatele k cíli), doba načítání (performance), přístupnost (accessibility). UX je o tom, jak rychle a bez frustrace uživatel udělá to, co chce.

# FREE: Vyjmenuj UI faktory.
> Paleta barev, typografie (font, hierarchie velikostí), layout, tlačítka, ikony, formuláře, vizuální hierarchie (velikost, kontrast), konzistence stylu, animace a přechody, grafické prvky. UI je o tom, jak stránka vypadá.

# FREE: Popiš, co ovlivňuje SEO.
> **On-page**: `<title>`, `<meta name="description">`, sémantické HTML (h1, article, nav), `alt` u obrázků, sémantické URL (`/clanek-o-x` lepší než `/p?id=42`), rychlost načítání (Core Web Vitals), responzivita, HTTPS (Google ranking signál). **Off-page**: kvalitní backlinks z jiných stránek, social signals, domain authority. **Technické**: sitemap.xml, robots.txt, canonical URL.

# FREE: Vysvětli druhy viewportů.
> Technicky: **Layout viewport** = šířka, kterou CSS používá pro layout. **Visual viewport** = co uživatel vidí (může být menší při zoom). **Ideal viewport** = device-width (pro responzivní design). Podle zařízení: mobil do 600 px, tablet 768–1024 px, desktop 1280+, velké displeje 1920+.

# FREE: Vysvětli rozdíl mezi mobile-first a desktop-first v CSS.
> Mobile-first: default styly pro mobil, media query `@media (min-width: 768px)` přidává styly pro větší zařízení. Desktop-first: default pro desktop, `@media (max-width: 767px)` upravuje pro mobil. Mobile-first je moderní praxe — lepší performance (mobil dostane méně CSS), respektuje Google mobile-first indexing.

# Q: Vyjmenuj 3 příklady obsahových webů.
A: Zpravodajské portály (iDNES.cz, BBC News), vzdělávací platformy (Khan Academy, Coursera), blogy (Medium, dev.to), Wikipedia.

# Q: K čemu slouží Lighthouse?
A: Nástroj v Chrome DevTools pro audit stránky. Hodnotí na škále 0–100 v 4 kategoriích: Performance, Accessibility, Best Practices, SEO. Pomáhá identifikovat konkrétní problémy s návrhy oprav.

# Q: Co je Core Web Vitals?
A: Sada Google metrik pro UX (LCP — Largest Contentful Paint, FID — First Input Delay, CLS — Cumulative Layout Shift). Ovlivňují SEO ranking — Google penalizuje stránky se špatnými skóry.

# Q: Jaké formáty obrázků jsou moderní a proč?
A: **WebP** a **AVIF** — moderní formáty s lepší kompresí než JPEG/PNG. Menší soubory = rychlejší načítání = lepší SEO i UX. Browsery podporují fallback přes `<picture>` element.
