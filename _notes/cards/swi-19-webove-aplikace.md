---
title: SWI 19 — Webové aplikace
description: SPA, MPA, request-response, HTTP(S), návratové kódy, metody HTTP
tags: [maturita, swi, web, http, spa, mpa]
---

# Q: Co je SPA?
A: Single Page Application — načte se jedna HTML, obsah mění JS přes API. React, Vue, Angular.

# Q: Co je MPA?
A: Multi Page Application — každá akce = nový request, server vrátí novou HTML. PHP, ASP.NET Razor.

# Q: Výhody SPA?
A: Rychlá odezva UI, moderní UX, méně dat (jen JSON).

# Q: Nevýhody SPA?
A: Horší SEO, pomalé první načtení, závislost na JS.

# Q: Výhody MPA?
A: Lepší SEO, jednodušší, funguje bez JS.

# CLOZE: HTTP běží na portu {{80}}, HTTPS na portu {{443}}.

# CLOZE: HTTPS = HTTP + {{TLS}} (dříve SSL) — šifrování + ověření identity serveru.

# CLOZE: HTTP je {{nestavový}} protokol — server si nepamatuje předchozí requesty.

# CLOZE: Nestavovost se řeší přes {{cookies}}, {{sessions}} a {{tokeny}} (JWT).

# Q: Co obsahuje HTTP request?
A: URL, metodu, hlavičky (headers), tělo (body).

# Q: Co obsahuje HTTP response?
A: Stavový kód, hlavičky, body s daty.

# MCQ: Která HTTP metoda je pro čtení dat?
- POST
- !GET
- DELETE
- PATCH
> GET — idempotent, bez body, pro načítání dat.

# MCQ: Která HTTP metoda vytváří nový resource?
- GET
- !POST
- PUT
- HEAD
> POST — ne idempotent, s body. PUT replace celý, PATCH částečně.

# MCQ: Co znamená 401?
- Server error
- Stránka nenalezena
- !Chybí autentizace (Unauthorized)
- Permanent redirect
> 401 = Unauthorized (chybí login). 403 = Forbidden (přihlášen, ale bez práv).

# MCQ: Co znamená 404?
- Server error
- !Resource nenalezen
- Bad request
- OK
> 404 Not Found — typický příklad 4xx (Client Error).

# MCQ: Co znamená 500?
- Stránka nenalezena
- !Internal Server Error — chyba serveru
- Unauthorized
- OK
> 5xx = chyby serveru. 500 typicky bug v kódu, 503 přetížení/údržba.

# MCQ: Která metoda je idempotent?
- POST
- !GET, PUT, DELETE
- Jen POST
- Žádná
> Idempotent = opakované volání = stejný výsledek. POST není (opakováním vznikne víc resources).

# CLOZE: Stavové kódy: 2xx = {{úspěch}}, 3xx = {{přesměrování}}, 4xx = {{chyba klienta}}, 5xx = {{chyba serveru}}.

# Q: Co je PWA?
A: Progressive Web App — webová aplikace s vlastnostmi mobilní app: install, offline, push notifications.

# Q: Co je Service Worker?
A: JS běžící na pozadí browseru, zachytává requesty, cachuje, umožňuje offline. Základ PWA.

# Q: Co je WebAssembly?
A: Binární formát, spouští C/C++/Rust kód v browseru s nativní rychlostí.

# Q: Co je WebSocket?
A: Alternativa HTTP — trvalé obousměrné spojení. Real-time chat, hry, notifikace.

# CODE: Napiš HTTP GET request raw.
```http
GET /api/users/42 HTTP/1.1
Host: api.example.com
Accept: application/json
Authorization: Bearer eyJhbGc...
```

# CODE: Napiš HTTP 200 response s JSON.
```http
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 45

{"id": 42, "name": "Anna"}
```

# CODE: Napiš JavaScript fetch GET.
```javascript
fetch('/api/users/42')
  .then(r => r.json())
  .then(data => console.log(data));
```
