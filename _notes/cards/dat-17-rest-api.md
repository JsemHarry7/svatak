---
title: DAT 17 — REST API v ASP.NET Core
description: Controllery, [ApiController], HTTP atributy, routing, parametry, status kódy, EF Core DbContext
tags: [maturita, dat, web, aspnet, rest-api]
---

# Q: Co je Web API v ASP.NET Core?
A: HTTP rozhraní vracející **JSON** (na rozdíl od Razor Pages, které vrací HTML). Postavené na controllerech s action metodami mapovanými na HTTP metody (GET/POST/PUT/DELETE).

# Q: Z čeho má controller dědit pro Web API?
A: **`ControllerBase`** (ne `Controller`). Controller má navíc `View()` pro MVC s views, ControllerBase je lehčí — jen pro API.

# Q: Co dělá atribut `[ApiController]`?
A: Aktivuje API conventions: 1) auto model validation (vrátí 400 při chybě), 2) defaultní binding sources (primitiva z query, objekty z body), 3) problem details formát pro chyby.

# CODE: Kostra REST controlleru
```csharp
[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public MoviesController(ApplicationDbContext db)
    {
        _db = db;
    }
}
```

# Q: Co znamená `[Route("api/[controller]")]`?
A: Definuje base path všech endpointů v kontroleru. `[controller]` je placeholder, který se nahradí jménem třídy **bez "Controller" suffixu** (MoviesController → "movies"). Výsledek: `/api/movies`.

# Q: 4 HTTP atributy pro action metody?
A: **`[HttpGet]`**, **`[HttpPost]`**, **`[HttpPut]`**, **`[HttpDelete]`**. Plus `[HttpPatch]` (částečná úprava) a `[HttpHead]` / `[HttpOptions]`.

# CODE: GET endpoint s ID parametrem
```csharp
[HttpGet("{id}")]
public ActionResult<Movie> GetById(int id)
{
    var movie = _db.Movies.Find(id);
    if (movie == null) return NotFound();
    return movie;
}
```

# CODE: POST endpoint s body
```csharp
[HttpPost]
public ActionResult<Movie> Create(Movie movie)
{
    movie.Votes = 0;
    movie.IsWatched = false;
    _db.Movies.Add(movie);
    _db.SaveChanges();
    return CreatedAtAction(nameof(GetById), new { id = movie.MovieId }, movie);
}
```

# CODE: PUT endpoint
```csharp
[HttpPut("{id}")]
public IActionResult Update(int id, Movie movie)
{
    var existing = _db.Movies.Find(id);
    if (existing == null) return NotFound();

    existing.Title = movie.Title;
    existing.Year = movie.Year;
    _db.SaveChanges();
    return NoContent();
}
```

# CODE: DELETE endpoint
```csharp
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    var movie = _db.Movies.Find(id);
    if (movie == null) return NotFound();

    _db.Movies.Remove(movie);
    _db.SaveChanges();
    return NoContent();
}
```

# CLOZE: Mapping HTTP metod na CRUD: POST = {{Create}}, GET = {{Read}}, PUT = {{Update}}, DELETE = {{Delete}}.

# Q: Jaký status code pro úspěšný POST?
A: **201 Created** + `Location` HTTP header s URL nového resource. Helper: `CreatedAtAction(nameof(GetById), new { id }, movie)`.

# Q: Jaký status code pro úspěšný PUT/DELETE?
A: **204 No Content** — operace proběhla, není co vracet. Helper: `NoContent()`.

# Q: Jaký status code pro úspěšný GET?
A: **200 OK** + data v JSON body. Helper: `Ok(data)`. S `[ApiController]` se `return data` automaticky obalí jako Ok.

# MCQ: Když resource neexistuje (GET /api/movies/999), co vrátit?
- 200 s prázdným tělem
- !404 Not Found přes `NotFound()`
- 500 Internal Server Error
- 204 No Content
> 404 přes `NotFound()` helper. Standardní REST chování pro neexistující resource.

# MCQ: Když klient pošle neplatný JSON v body POST requestu, co se stane s `[ApiController]`?
- 200 OK s null daty
- !Automaticky 400 Bad Request s detaily chyby
- 500 Internal Server Error
- Action se vůbec nezavolá silently
> `[ApiController]` automaticky validuje model. Při chybě vrátí 400 + problem details. Bez `[ApiController]` musíš ručně kontrolovat `ModelState.IsValid`.

# Q: Co dělá `[FromQuery]`?
A: Říká frameworku, že parametr se má vzít **z query stringu** URL. Např. `/api/movies?genre=Horror` → `[FromQuery] string genre`.

# Q: Co dělá `[FromRoute]`?
A: Vezme parametr **z URL path**. Např. `/api/movies/42` s `[Route("{id}")]` → `[FromRoute] int id` (s `[ApiController]` se atribut nemusí psát explicitně).

# Q: Co dělá `[FromBody]`?
A: Vezme parametr **z JSON body** requestu, deserializuje do C# objektu. Typicky pro POST/PUT s komplexními objekty.

# MCQ: Default binding source pro `int id` v `Update(int id, Movie movie)` s `[HttpPut("{id}")]`?
- FromBody
- !FromRoute (matchne placeholder v route)
- FromQuery
- FromForm
> Pokud jméno parametru matchne placeholder v route (`{id}` ↔ `int id`), framework použije FromRoute. Pro Movie pak FromBody (komplexní typ).

# Q: Pořadí routes — proč musí `[HttpGet("unwatched")]` být před `[HttpGet("{id}")]`?
A: Framework routes čte v pořadí, jak jsou definované. Pokud by `{id}` bylo dřív, request `/unwatched` by se pokusil interpretovat jako `id="unwatched"` (a selhal s 400). Bezpečné: **literály před parametry**.

# Q: Jak controller dostane DbContext?
A: **Dependency Injection v konstruktoru**. V Program.cs: `builder.Services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlite(...))`. Framework při requestu vytvoří instanci (Scoped lifetime) a dodá do konstruktoru.

# CODE: EF Core základní operace
```csharp
var all = _db.Movies.ToList();
var one = _db.Movies.Find(42);
var filtered = _db.Movies.Where(m => m.Genre == "Horror").ToList();
_db.Movies.Add(newMovie);
_db.Movies.Remove(movie);
_db.SaveChanges();   // commitne změny do DB
```

# Q: Proč musí být `_db.SaveChanges()` po Add/Update/Remove?
A: EF Core sleduje změny v paměti (change tracker), ale **nezapíše je do DB** dokud nezavolá `SaveChanges()`. Umožňuje to seskupit více operací do jedné transakce. Bez SaveChanges změny zmizí po skončení requestu.

# Q: Co je rozdíl mezi `SaveChanges()` a `SaveChangesAsync()`?
A: **Sync** blokuje vlákno serveru dokud DB nedokončí zápis. **Async** uvolní vlákno (await) a server může mezitím obsluhovat jiné requesty. Pro produkční API použít async.

# CODE: Async action method
```csharp
[HttpGet]
public async Task<ActionResult<List<Movie>>> GetAll()
{
    var movies = await _db.Movies.ToListAsync();
    return movies;
}
```

# Q: Co je `CreatedAtAction`?
A: Helper pro POST handlery vracející 201 Created + Location header s URL nového resource. Volání: `CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie)`. Klient z Location vyčte URL detailu nového záznamu.

# Q: Co dělá `dotnet ef migrations add <Name>`?
A: Vytvoří **C# soubor v Migrations/** popisující změnu DB schématu (CREATE TABLE, ADD COLUMN...). Migrace ještě není aplikovaná — to udělá `dotnet ef database update`.

# CLOZE: V Program.cs pro Web API: `builder.Services.{{AddControllers}}()`. Pro Razor Pages: `builder.Services.{{AddRazorPages}}()`.

# MCQ: Rozdíl mezi Web API a Razor Pages?
- Web API běží na Kestrelu, Razor Pages na IIS
- !Web API vrací JSON přes controllery, Razor Pages vrací HTML přes PageModely
- Web API je rychlejší
- Razor Pages je deprecated
> Klíčový rozdíl: výstupní formát (JSON × HTML) + struktura (controllery + action metody × PageModel + handlery).

# MCQ: Pro mobilní aplikaci komunikující se serverem se hodí spíš...
- Razor Pages
- !Web API
- Blazor Server
- MVC s views
> Mobilní app potřebuje JSON endpointy, ne HTML. Web API je idiomatic.

# FREE: Popiš celý flow GET requestu /api/movies/42 v ASP.NET Core Web API.
> 1) Klient pošle HTTP GET na /api/movies/42. 2) Kestrel přijme, middleware pipeline (UseHttpsRedirection, UseRouting, UseAuthorization). 3) Routing pomocí atributů: `[Route("api/[controller]")]` na MoviesController + `[HttpGet("{id}")]` na GetById → match. 4) DI vytvoří MoviesController, vstříkne ApplicationDbContext. 5) Framework parsuje `42` z URL jako parametr `int id`. 6) Action `GetById(42)` se zavolá. 7) `_db.Movies.Find(42)` se zeptá DB. 8) Pokud null → return NotFound() = 404. Pokud najde → return movie. 9) Framework objekt serializuje do JSON (camelCase). 10) Response 200 + JSON body → middleware → Kestrel → klient.

# FREE: Vysvětli rozdíl mezi `Ok()`, `NoContent()`, `Created()` a `NotFound()` jako návratová metoda akce.
> `Ok(data)` = 200 OK + data v JSON body (úspěšný GET). `NoContent()` = 204 No Content, prázdné body (úspěšný PUT/DELETE — operace proběhla, nic vracet). `Created(uri, data)` nebo `CreatedAtAction(...)` = 201 Created + data + Location header (úspěšný POST, klient z Location vyčte URL nového resource). `NotFound()` = 404 (resource neexistuje, např. GET /api/movies/999).
