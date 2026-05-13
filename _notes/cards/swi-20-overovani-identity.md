---
title: SWI 20 — Ověřování identity
description: Identifikace × autentizace × autorizace, hesla, 2FA, JWT, OAuth 2.0, OpenID Connect, passwordless
tags: [maturita, swi, web, auth, security]
---

# Q: Tři pilíře ověřování — identifikace × autentizace × autorizace?
A: **Identifikace** = uživatel tvrdí, kdo je (e-mail). **Autentizace** = potvrzení důkazem (heslo). **Autorizace** = co smí dělat. Pořadí vždy: identifikace → autentizace → autorizace.

# Q: Rozdíl autentizace × autorizace?
A: Autentizace = *kdo jsi* (heslo, kód). Autorizace = *co smíš* (oprávnění ke zdrojům). Autentizace vždy PŘED autorizací.

# Q: HTTP kód 401 × 403 — jaký je rozdíl v kontextu auth?
A: **401 Unauthorized** = chyba autentizace ("nevíme, kdo jsi"). **403 Forbidden** = chyba autorizace ("víme, kdo jsi, ale nesmíš sem").

# CLOZE: Tři kategorie autentizačních faktorů: něco co {{víš}} (heslo), něco co {{máš}} (telefon, token), něco co {{jsi}} (biometrie).

# Q: Co je 2FA a proč se používá?
A: Dvoufaktorová autentizace — kombinace 2 nezávislých faktorů (typicky heslo + kód z mobilu). I když útočník zná heslo, bez druhého faktoru se nedostane dovnitř.

# MCQ: Jaký je rozdíl mezi 2FA a MFA?
- Není rozdíl
- !2FA = právě 2 faktory, MFA = 2+ faktorů
- 2FA je starší pojem
- MFA znamená "mobilní" autentizace
> 2FA ⊂ MFA. Když systém vyžaduje heslo + SMS + Face ID, je to 3FA neboli MFA.

# Q: Proč server NESMÍ ukládat hesla v plaintextu?
A: Při úniku databáze by útočník měl všechna hesla. Místo plaintextu se hesla hashují pomalou funkcí (bcrypt, Argon2) + saltem. Hash je jednosměrný — heslo z něj nevrátíš.

# Q: Co je salt a proč se používá při hashování hesel?
A: Náhodný řetězec přidaný k heslu před hashováním. Brání **rainbow table útokům** — útočník nemůže předem vypočítat hashe běžných hesel, protože každé heslo má unikátní salt.

# MCQ: Která hashovací funkce se HODÍ pro hashování hesel?
- MD5
- SHA-1
- SHA-256
- !bcrypt / Argon2
> MD5/SHA-1 jsou rozbité. SHA-256 je rychlá, což je BAD pro hesla (útočník zkouší miliardy za sekundu). bcrypt/Argon2 jsou ZÁMĚRNĚ pomalé.

# Q: Co je TOTP a kde se používá?
A: **Time-based One-Time Password.** Authenticator app (Google Authenticator) generuje 6-místný kód každých 30 s podle sdíleného tajemství. Druhý faktor v 2FA — bezpečnější než SMS (odolný proti SIM swap).

# MCQ: Který 2FA faktor je NEJSLABŠÍ?
- TOTP aplikace
- !SMS kód
- Hardwarový klíč (YubiKey)
- Biometrie
> SMS je zranitelná na SIM swap útok a odposlech. NIST od 2016 nedoporučuje SMS jako druhý faktor pro citlivé účty.

# Q: Co je token a proč se používá místo opakovaného posílání hesla?
A: Dočasný digitální klíč vydaný serverem po přihlášení. Heslo se pošle jen **jednou** (login), token se posílá při každém dalším requestu. Token má omezenou platnost, lze ho odvolat, server může být stateless.

# Q: Co je JWT a z čeho se skládá?
A: **JSON Web Token.** Skládá se ze 3 částí oddělených tečkami: **header** (algoritmus), **payload** (data o uživateli, expirace), **signature** (kryptografický podpis serverem).

# CLOZE: JWT formát: {{header}}.{{payload}}.{{signature}}, oddělené tečkami.

# MCQ: JWT payload je...
- Šifrovaný, nikdo ho nepřečte
- !Base64-encoded, čte ho kdokoli, ale nelze ho zfalšovat bez serverového klíče
- Hashovaný
- V plaintextu
> JWT NENÍ šifrovaný. Kdokoli si payload dekóduje na jwt.io. Hodnota je v podpisu — útočník nemůže payload změnit, aniž by se rozbil podpis.

# Q: Kam se v HTTP requestu posílá JWT?
A: Do hlavičky `Authorization: Bearer <token>`. NIKDY ne do URL (URL se loguje, ukládá do historie).

# Q: Co znamená "stateless" v kontextu tokenu vs session?
A: **Session** = stateful — server si pamatuje, kdo je přihlášen (v DB/cache). **Token (JWT)** = stateless — všechna data o uživateli jsou v tokenu, server nic neukládá. Stateless = lepší škálovatelnost, vhodné pro REST.

# Q: Co je OAuth 2.0?
A: Standardizovaný protokol pro **delegovaný přístup**. Umožňuje aplikaci získat omezený přístup k datům uživatele v jiné službě, aniž by se dozvěděla heslo. *Příklad: aplikace získá přístup k Google Photos bez znalosti Google hesla.*

# MCQ: OAuth 2.0 řeší...
- Autentizaci (kdo jsi)
- !Autorizaci (co smíš)
- Šifrování
- Hashování hesel
> OAuth = autorizační protokol. Pro autentizaci slouží OpenID Connect (vrstva NAD OAuth).

# Q: Čtyři aktéři v OAuth 2.0 flow?
A: **Resource Owner** (uživatel), **Client** (aplikace), **Authorization Server** (Google), **Resource Server** (Google Photos API).

# Q: Co je OpenID Connect?
A: Vrstva nad OAuth 2.0, která přidává **autentizaci** (identitu uživatele). Vrátí aplikaci **ID Token** (JWT) s daty o uživateli: jméno, e-mail, foto.

# CLOZE: Mantra: OAuth říká {{co smíš}}, OpenID Connect říká {{kdo jsi}}.

# Q: Co je sociální přihlášení a jaké protokoly využívá?
A: *"Přihlásit se přes Google/Facebook/Apple"* — využívá **OAuth 2.0** (autorizace) + **OpenID Connect** (autentizace). Uživatel se přihlašuje na Google, aplikace dostane jen token + základní data.

# Q: Výhody sociálního přihlášení?
A: Pohodlné (jeden klik), žádné nové heslo, rychlá registrace, vyšší bezpečnost (Google má lepší zabezpečení než malé weby).

# Q: Nevýhody sociálního přihlášení?
A: Závislost na poskytovateli (smazaný účet = ztráta všeho), platforma vidí kam se přihlašuješ, vendor lock-in, soukromí.

# Q: Co je passwordless přihlašování?
A: Trend úplně odstranit hesla. Místo nich kryptografické klíče (FIDO2, Passkeys) uložené v zařízení, odemykané biometrií. Klient drží privátní klíč, server veřejný — při přihlášení klient podepisuje výzvu.

# Q: Co jsou Passkeys?
A: Uživatelsky přívětivá implementace FIDO2. Kryptografické klíče synchronizované mezi zařízeními uživatele (iCloud Keychain, Google Password Manager, Microsoft Account). Heslo nikdy neexistuje.

# Q: Co je refresh token?
A: Dlouhodobý token (~30 dní) vedle krátkodobého access tokenu (~15 min). Když access vyprší, klient ho vymění za nový pomocí refresh tokenu — bez nutnosti znovu zadávat heslo.

# MCQ: Kam je BEZPEČNĚJŠÍ ukládat JWT v prohlížeči?
- localStorage
- !httpOnly cookie
- sessionStorage
- URL query string
> JS NEMÁ přístup k httpOnly cookies, takže XSS útok nemůže ukrást token. localStorage je dosažitelný pro JS = zranitelnost.

# Q: Proč autentizace bez HTTPS nedává smysl?
A: HTTP posílá heslo / token v plaintextu — kdokoli na trase (Wi-Fi, ISP) ho odposlechne. HTTPS šifruje obsah requestu TLS protokolem. Autentizace přes HTTP = ekvivalent posílat heslo na pohlednici.

# Q: Co je SSO (Single Sign-On)?
A: Jedno přihlášení = přístup k mnoha aplikacím. Typicky ve firmě (firemní e-mail = jeden login pro intranet, cloud, GitHub). Postavené nad OIDC nebo SAML.

# CODE: JWT decoded příklad (3 části)
```
Header:    { "alg": "HS256", "typ": "JWT" }
Payload:   { "sub": "42", "name": "Anna", "role": "admin", "exp": 1735689600 }
Signature: HMACSHA256(base64(header) + "." + base64(payload), secret)
```

# CODE: HTTP request s JWT
```
GET /api/profile HTTP/1.1
Host: example.com
Authorization: Bearer eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiI0MiJ9.X8sdf...
```

# FREE: Vysvětli OAuth 2.0 flow na příkladu "Aplikace na úpravu fotek chce přístup ke Google Photos".
> 1) Aplikace pošle uživatele na Google login. 2) Uživatel se přihlásí na Google. 3) Google se zeptá: "povolíš aplikaci přístup k fotkám?". 4) Uživatel potvrdí. 5) Google vydá aplikaci access token s omezeným scope (jen čtení fotek). 6) Aplikace volá Google Photos API s tokenem. Aplikace NIKDY nevidí Google heslo.
