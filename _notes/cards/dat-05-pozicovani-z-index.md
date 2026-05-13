---
title: DAT 5 — Pozicování + z-index
description: CSS position (static/relative/absolute/fixed/sticky), float, z-index, stacking context
tags: [maturita, dat, web, css, pozicovani, z-index]
---

# Q: Vyjmenuj 5 hodnot CSS `position`.
A: `static` (default — v toku, ignoruje top/left), `relative` (v toku, lze posunout od původní pozice + kotva pro absolute děti), `absolute` (vytržen z toku, kotví se nejbližšímu pozicovanému předkovi), `fixed` (vytržen z toku, kotví se viewportu, drží při scrollu), `sticky` (hybrid — relative dokud nedoraží na hranici, pak fixed).

# Q: Jaký je rozdíl mezi `absolute` a `fixed`?
A: **`absolute`** se pozicuje vůči **nejbližšímu pozicovanému předkovi** (nebo body) — při scrollu se posouvá s obsahem. **`fixed`** se pozicuje vůči **viewportu** — zůstává na stejném místě i při scrollu. Fixed pro modaly, cookie lišty, sticky bary.

# CLOZE: `z-index` funguje **pouze** na prvcích, které mají `position` jinou než `{{static}}` (nebo jsou dětmi flex/grid kontejneru).

# CLOZE: Pro modal vycentrovaný v okně se používá kombinace `position: fixed; top: 50%; left: 50%; transform: {{translate(-50%, -50%)}}`.

# CLOZE: `position: sticky` NEFUNGUJE, pokud nějaký předek má `overflow: {{hidden}}` (nebo auto/scroll). Sticky element pak nemá scroll kontejner pro detekci hranice.

# MCQ: Co je sticky pozicování?
- !Hybrid mezi `relative` a `fixed` — chová se jako relative, dokud scrollování nedosáhne hranice (např. `top: 0`), pak se "přilepí" a chová se jako fixed
- Skryté element
- Sticky cursor
- Zafixovaný v rohu
> Sticky se přilepí na hranici (např. top: 0) a zůstane tam, dokud rodičovský kontejner neopustí viewport. Klasické použití: sticky navigace.

# MCQ: Pro modal v rohu obrazovky, který drží při scrollu, použiješ:
- absolute
- relative
- !fixed
- sticky
> `fixed` se kotví viewportu, nezávisle na scroll pozici. Klasické pro modaly, cookie lišty, "back to top" tlačítko.

# MCQ: Pattern "badge v rohu karty" implementuje:
- Float
- Sticky
- !`position: relative` na rodiči (kartě) + `position: absolute` na badge s `top: 0; right: 0`
- Jen `position: absolute` na badge
> Bez `relative` na rodiči by se absolute badge přimknul k nejbližšímu pozicovanému předkovi nebo body — totální chaos. Klasický pár: relative parent + absolute child.

# MCQ: Co se stane, když element s `transform: scale(1)` má jako děti modaly s vysokým z-index?
- Nic
- Modaly fungují normálně
- !Modaly mohou být skryty pod ostatními elementy — `transform` vytváří stacking context
- Modaly mizí
> Klasická past! `transform` (i `scale(1)` co nic nedělá), `opacity < 1`, `filter` atd. vytvářejí stacking context. Děti jsou "uvězněné" v něm, jejich z-index se porovnává jen uvnitř kontextu.

# MCQ: Co `z-index: 9999` na elementu s `position: static`?
- Maximální priorita
- !Nedělá nic — z-index funguje jen na pozicovaných elementech
- Compile error
- Error v konzoli
> Z-index na `position: static` se ignoruje. Pro vrstvení musí být `relative`, `absolute`, `fixed` nebo `sticky`.

# FREE: Vyjmenuj 5 hodnot position a popiš každou krátce.
> **`static`** (default) — v normálním toku, ignoruje top/left/z-index. **`relative`** — v toku, lze posunout od původní pozice přes top/right/bottom/left, slouží jako kotva pro absolute děti. **`absolute`** — vytržen z toku, ostatní se chovají, jako by neexistoval, pozicuje se vůči nejbližšímu pozicovanému předkovi. **`fixed`** — vytržen z toku, pozicuje se vůči viewportu, drží při scrollu. **`sticky`** — hybrid: relative do hranice, pak fixed.

# FREE: Vysvětli stacking context a proč na něm záleží.
> Stacking context je izolovaná oblast pro porovnání z-index. Z-index funguje **jen mezi sourozenci ve stejném contextu**. Prvek s z-index 9999 uvnitř kontextu A se může schovat **pod** prvek s z-index 1 z kontextu B, pokud je B v hierarchii výše. Stacking context vzniká u `position` + `z-index`, `opacity < 1`, `transform`, `filter`, `isolation: isolate`. Pravidlo: *"z-index porovnává sourozence ve stejném contextu"*.

# FREE: Popiš past s `position: sticky` a `overflow: hidden`.
> `position: sticky` funguje vůči nejbližšímu **scrolling kontejneru** (default = viewport). `overflow: hidden` (nebo `auto`, `scroll`) na předkovi vytvoří **nový scrolling kontejner** — i kdyby tam reálně nebyl scroll. Sticky element teď cílí na tento kontext, který nescrolluje → sticky se nikdy "neaktivuje". Řešení: odstranit overflow z předka nebo posunout sticky výš v DOM hierarchii.

# FREE: K čemu sloužil `float` historicky a k čemu dnes?
> Historicky pro layout (sloupce stránek). Dnes nahrazen Flexboxem a Gridem. Float se používá už jen pro **obtékání textu kolem obrázku** (jako v časopise) — `float: left` zarovná obrázek vlevo, text obteče vpravo. Klasický problém s float: rodič "zhroutí výšku", pokud obsahuje jen floating prvky. Řešení: clearfix nebo `overflow: hidden`/`display: flow-root` na rodiči.

# CODE: Centrovaný modal přes fixed + transform.
```css
.modal {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    z-index: 1000;
}
.overlay {
    position: fixed;
    inset: 0;                            /* shorthand pro top/right/bottom/left: 0 */
    background: rgba(0, 0, 0, 0.5);
    z-index: 999;
}
```

# CODE: Sticky header.
```css
header {
    position: sticky;
    top: 0;
    background: white;
    z-index: 10;
}
```

# CODE: Karta s badge v rohu (relative + absolute).
```css
.card {
    position: relative;
}
.badge {
    position: absolute;
    top: 10px;
    right: 10px;
    background: red;
    color: white;
}
```

# CODE: Float obrázek s textem obtékaný vlevo.
```css
.float-image {
    float: left;
    width: 200px;
    margin-right: 1rem;
    margin-bottom: 0.5rem;
}
```

# Q: Co je `inset` v CSS?
A: Moderní shorthand pro `top + right + bottom + left`. `inset: 0` = všechny 0 = element roztáhne přes celý kontejner. Často s `position: fixed`/`absolute` pro overlay/modal backdrop.

# Q: Proč `transform: translate(-50%, -50%)` v centrování modal?
A: `top: 50%; left: 50%` posune **levý horní roh** prvku do středu viewportu, ne celý prvek. `transform: translate(-50%, -50%)` posune prvek o **polovinu jeho vlastní velikosti** zpět — teď je střed prvku ve středu viewportu, nezávisle na rozměrech.

# Q: Doporučená hierarchie z-index hodnot?
A: Background overlays: 1–10. Sticky bars/headers: 10–100. Dropdowns: 100–1000. Modals: 1000+. Notifications/toasts: 10000+. Není to standard, ale **vyhneš se kolizím** a kódová báze je čitelná.

# Q: Co dělá `clear: both`?
A: Zruší obtékání kolem plovoucích prvků. Element se posune **pod** floating prvky vlevo i vpravo. Klasické řešení: clearfix pattern (`::after { content: ""; display: block; clear: both; }`) pro zachování výšky rodiče floating dětí.
