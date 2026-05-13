---
title: SWI 21 — RESTful
description: REST, Web API, AJAX, JSON vs XML, CRUD, HTTP metody, kódy
tags: [maturita, swi, web, rest, api, json]
---

# Q: Co je REST?
A: Architektonický styl pro API — HTTP + zdroje (URL) + stateless + jednotné rozhraní. Definoval Roy Fielding (2000).

# Q: Co je Web API?
A: Rozhraní pro komunikaci aplikací (klient ↔ server), typicky přes HTTP.

# Q: Rozdíl Web API × REST API?
A: Web API = obecný pojem. REST API = Web API následující REST principy.

# CLOZE: REST pracuje se zdroji identifikovanými přes {{URL}} a používá {{HTTP}} metody.

# CLOZE: V REST je server {{stateless}} — nepamatuje si stav klienta, každý request obsahuje vše potřebné.

# CLOZE: HTTP metody mapují na CRUD: POST = {{Create}}, GET = {{Read}}, PUT/PATCH = {{Update}}, DELETE = {{Delete}}.

# Q: Standardní REST URL pro získání uživatele s id 42?
A: GET /api/users/42

# Q: Standardní REST URL pro vytvoření nového uživatele?
A: POST /api/users (data v body, ne v URL)

# MCQ: Jaký je rozdíl mezi PUT a PATCH?
- Není rozdíl
- !PUT replace celý resource, PATCH částečná úprava
- PUT je idempotent, PATCH ne (toto je důsledek, ne hlavní rozdíl)
- PATCH je rychlejší
> PUT pošle kompletní nová data (chybějící pole se vymažou). PATCH pošle jen pole ke změně.

# MCQ: Která metoda NENÍ idempotent?
- GET
- PUT
- DELETE
- !POST
> POST opakováním vytvoří víc resources. GET/PUT/DELETE vrátí stejný stav.

# MCQ: Co znamená 201 Created v REST?
- Server se restartoval
- !Resource byl úspěšně vytvořen (po POST)
- Připojení vytvořeno
- 201 neexistuje
> 201 = úspěch POST, vrátí nově vytvořený resource a `Location` header s URL.

# MCQ: Co znamená 422 Unprocessable Entity?
- Server přetížen
- !Sémantická validace selhala (např. neplatný formát emailu)
- Stránka neexistuje
- CORS error
> 422 = data jsou syntakticky správná (validní JSON), ale sémanticky neplatná. 400 vs 422: 400 špatný formát, 422 špatný význam.

# Q: Co je JSON?
A: JavaScript Object Notation — lehký textový formát pro výměnu dat. Dnes standard pro REST.

# Q: JSON datové typy?
A: string, number, boolean, null, array, object.

# Q: Hlavní rozdíl JSON × XML?
A: JSON lehký, čitelný, blízký JS. XML "ukecaný", ale podporuje atributy a schemata. JSON pro REST, XML pro legacy/SOAP.

# CLOZE: JSON klíče musí být v {{uvozovkách}} (na rozdíl od JavaScript object literálu).

# Q: Co je AJAX?
A: Asynchronous JavaScript and XML — technika asynchronního volání API z prohlížeče bez reloadu stránky.

# Q: Dnešní moderní AJAX se píše přes co?
A: Fetch API (Promise-based) nebo `XMLHttpRequest` (starší, callback-based).

# Q: Proč "X" v AJAX, když dnes používáme JSON?
A: Historický název z roku ~2005 (Gmail). Dnes JSON, ale zkratka zůstala.

# MCQ: Co je CORS?
- Chyba serveru
- !Cross-Origin Resource Sharing — bezpečnostní pravidlo prohlížeče proti volání API z jiné domény
- Šifrovací protokol
- HTTP metoda
> Browser blokuje JS volání API na jiné doméně, dokud server nepošle `Access-Control-Allow-Origin` header.

# Q: Co je preflight request?
A: OPTIONS request, který browser pošle před skutečným PUT/DELETE, aby zjistil, zda server CORS dovolí.

# Q: Kde se posílá auth token v REST?
A: V hlavičce `Authorization: Bearer eyJhbGc...` — NIKDY v URL (URL se loguje).

# CODE: Fetch GET request s JSON odpovědí.
```javascript
fetch('/api/users/42')
  .then(r => r.json())
  .then(data => console.log(data));
```

# CODE: Fetch POST s JSON body.
```javascript
fetch('/api/users', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ name: 'Anna', email: 'a@x.cz' })
});
```

# CODE: Async/await fetch.
```javascript
async function loadUser(id) {
    const r = await fetch(`/api/users/${id}`);
    if (!r.ok) throw new Error(`HTTP ${r.status}`);
    return await r.json();
}
```

# CODE: REST API typické endpointy.
```
GET    /api/products         seznam
GET    /api/products/42      jeden
POST   /api/products         vytvořit
PUT    /api/products/42      replace
PATCH  /api/products/42      částečná
DELETE /api/products/42      smazat
```

# CODE: JSON objekt.
```json
{
    "id": 42,
    "name": "Anna",
    "skills": ["C#", "React"],
    "active": true,
    "manager": null
}
```

# Q: Co je rozdíl mezi REST a GraphQL?
A: REST = pevné endpointy (URL + metoda). GraphQL = jeden endpoint, klient určuje, jaká data dostane (úspora over-fetching).

# Q: REST pravidla pojmenování URL?
A: Plurál pro kolekce (/users), id přes path (/users/42), sloveso v HTTP metodě (NE /getUsers), lowercase + pomlčky.
