---
title: SWI 13 — Internet
description: Internet, internetové zdroje, URL, MIME, DNS, domény, IP
tags: [maturita, swi, web, internet, dns, url]
---

# Q: Co je internet?
A: Celosvětová síť propojených počítačových sítí, které komunikují pomocí rodiny protokolů TCP/IP. Vznikl z amerického projektu ARPANET v 60.–70. letech.

# Q: Jaký je rozdíl mezi internetem a webem?
A: Internet je **infrastruktura** (síť počítačů + TCP/IP protokoly). Web (WWW) je **jedna ze služeb** na internetu — používá HTTP/HTTPS. Další služby: e-mail (SMTP), FTP, DNS, SSH.

# CLOZE: URL se skládá z těchto částí (v pořadí): {{protokol}}://{{doména}}:{{port}}/{{path}}?{{query}}#{{fragment}}.

# CLOZE: V URL platí: query (`?`) se posílá na {{server}}, fragment (`#`) zůstává v {{prohlížeči}} (server ho nikdy nedostane).

# CLOZE: Default port pro HTTP je {{80}}, pro HTTPS je {{443}}.

# MCQ: Co dělá DNS?
- Šifruje webovou komunikaci
- !Překládá doménové jméno na IP adresu
- Hostuje webové stránky
- Spravuje uživatelské účty
> Domain Name System = překlad doménového jména (např. google.com) na IP adresu (např. 142.250.187.132). Počítače komunikují přes IP, lidé pamatují jména.

# MCQ: Kde se deklaruje MIME typ obsahu?
- V příponě souboru
- !Server ho posílá v HTTP hlavičce `Content-Type`
- V meta tagu HTML
- V doménovém jménu
> MIME typ posílá SERVER v HTTP response header `Content-Type` (např. `image/png`, `text/html`). Browser podle něj rozhoduje, jak obsah zobrazit — řídí se MIME, ne příponou.

# MCQ: Co je TLD v doméně `www.google.com`?
- www
- google
- !com
- .com (s tečkou)
> TLD = Top Level Domain = nejvyšší úroveň, čte se zprava. V `google.com` je TLD `com`, SLD `google`, subdoména `www`.

# MCQ: Jaký je rozdíl mezi absolutní a relativní URL?
- !Absolutní obsahuje celou cestu včetně protokolu a domény. Relativní je vztažená k aktuálnímu dokumentu.
- Absolutní je rychlejší
- Relativní funguje jen v Chrome
- Není rozdíl
> Absolutní: `https://example.com/img/logo.png`. Relativní: `img/logo.png` (vůči aktuálnímu dokumentu) nebo `/img/logo.png` (od root domény). Relativní je přenositelnější mezi dev/staging/prod.

# FREE: Popiš anatomii URL na příkladu `https://api.example.com:8080/v1/users?id=42#profile`.
> `https` = protokol. `api` = subdoména. `example` = SLD (Second Level Domain). `com` = TLD (Top Level Domain). `8080` = port (volitelný, default by byl 443 pro https). `/v1/users` = cesta (path). `?id=42` = query string (data pro server). `#profile` = fragment/kotva (pozice v dokumentu, zůstává v prohlížeči).

# FREE: Vysvětli, jak funguje DNS lookup.
> 1) Uživatel zadá doménu v prohlížeči. 2) Browser se zeptá DNS resolveru (typicky u ISP nebo veřejný jako 8.8.8.8). 3) Resolver postupně dotazuje hierarchii: Root DNS (kdo má TLD .com) → TLD servery (kdo má example.com) → Authoritative server (jakou má IP). 4) Vrátí IP, browser se připojí. 5) Vše se cachuje (s TTL Time To Live).

# FREE: Vyjmenuj typy domén podle účelu.
> Národní (ccTLD) — Country Code, určují stát: `.cz`, `.de`, `.sk`. Generické (gTLD) — obecné: `.com`, `.org`, `.net`, `.edu`, `.gov`. Nové TLD (od 2014) — moderní: `.tech`, `.shop`, `.app`, `.dev`, `.io`. Speciální (vyhrazené): `.gov` pro vládu USA, `.edu` pro vzdělávání, `.mil` pro armádu.

# Q: Co znamená IPv4 a IPv6?
A: IPv4 = 4 bajty, formát `192.168.1.1`, ~4.3 miliardy adres (vyčerpáno). IPv6 = 16 bajtů, hexadecimální formát `2001:0db8:...`, prakticky neomezeno. IPv6 nahrazuje IPv4 postupně.

# Q: Co se stane, když server pošle .html soubor s `Content-Type: text/plain`?
A: Browser zobrazí HTML kód jako **plain text** (uvidíš `<html><body>` jako text místo render). Browser se řídí MIME typem z HTTP hlavičky, ne příponou souboru.

# Q: Vyjmenuj 5 běžných MIME typů a jejich přípony.
A: `.html` → `text/html`, `.css` → `text/css`, `.js` → `application/javascript`, `.json` → `application/json`, `.jpg` → `image/jpeg`, `.png` → `image/png`, `.pdf` → `application/pdf`.

# Q: K čemu slouží fragment v URL (např. `#kontakt`)?
A: Posun na konkrétní část stránky (element s daným `id`). Fragment **zůstává v prohlížeči**, server ho nikdy nedostane. Použití: navigační kotvy v rámci stránky, SPA routing.

# CODE: Napiš HTML odkazy s různými typy URL.
```html
<!-- Absolutní -->
<a href="https://example.com/about">O nás</a>

<!-- Relativní k aktuální stránce -->
<a href="about.html">O nás</a>

<!-- Relativní k root domény -->
<a href="/about">O nás</a>

<!-- Bez protokolu (dědí HTTP/HTTPS) -->
<script src="//cdn.example.com/lib.js"></script>

<!-- Pouze fragment -->
<a href="#kontakt">Skoč na kontakt</a>
```

# CODE: Napiš HTTP odpověď s MIME typem.
```http
HTTP/1.1 200 OK
Content-Type: text/html; charset=UTF-8
Content-Length: 1234

<!DOCTYPE html>
<html>...
```

# Q: Co jsou TCP a UDP?
A: Oba transportní protokoly. **TCP** (Transmission Control Protocol) — spolehlivý přenos s kontrolou doručení a znovuzasláním ztracených paketů. **UDP** (User Datagram) — rychlejší, ale bez záruky doručení (streaming, online hry, DNS query).
