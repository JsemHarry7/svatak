---
title: SWI 22 — ASP.NET
description: Typy aplikací, Kestrel + middleware, Razor Pages, handlery, Razor syntax, DI + lifetime, appsettings.json
tags: [maturita, swi, web, aspnet, csharp]
---

# Q: Co je ASP.NET Core?
A: Multiplatformní framework od Microsoftu pro tvorbu webových aplikací a API v C#. Běží na Windows/Linux/Mac, má vlastní HTTP server Kestrel a zabudovanou Dependency Injection.

# Q: Čtyři hlavní typy aplikací v ASP.NET Core?
A: **Razor Pages** (page-based weby s HTML), **MVC** (controller-based, tradiční), **Web API** (REST endpointy v JSON), **Blazor** (SPA framework v C#).

# MCQ: Pro jednoduchý CRUD formulářový web v ASP.NET je nejvhodnější...
- Web API
- !Razor Pages
- Blazor WebAssembly
- MVC
> Razor Pages = page-based, ideální pro CRUD a formuláře. Vše související se stránkou je v jednom file-pair.

# MCQ: Pro REST API obsluhující mobilní aplikaci je nejvhodnější...
- Razor Pages
- !Web API (nebo Minimal API)
- Blazor Server
- MVC s view
> Web API vrací JSON, žádný HTML render. Minimal API je odlehčená varianta bez controllerů.

# Q: Co je Kestrel?
A: HTTP server zabudovaný v ASP.NET Core. Aplikace běží jako samostatný proces (`.exe`) s vlastním Kestrelem — nepotřebuje externí webserver. V produkci bývá za reverzní proxy.

# Q: Co je reverzní proxy a proč se používá před Kestrelem?
A: Server stojící mezi klientem a aplikační instancí. Klient komunikuje s proxy (typicky IIS/NGINX), proxy předává request Kestrelu. Řeší TLS terminaci, statické soubory, load balancing. **Chrání server, ne klienta** (klasická proxy chrání klienta).

# Q: Co je middleware v ASP.NET Core?
A: Komponenta v řetězci zpracování requestu. Každý request prochází sérií middleware (UseHttpsRedirection, UseStaticFiles, UseRouting, UseAuthorization, MapRazorPages...) → na konci endpoint → response.

# CLOZE: Request prochází ASP.NET Core takto: Request → {{Middleware 1}} → {{Middleware 2}} → ... → {{Endpoint}} → Response.

# Q: Co jsou Razor Pages?
A: Page-based přístup v ASP.NET — každá stránka má **dva soubory**: `.cshtml` (HTML šablona + Razor syntax) a `.cshtml.cs` (PageModel s C# logikou). Routing je konvenční podle umístění v `Pages/`.

# Q: Soubor `Pages/Admin/Users.cshtml` má jakou URL?
A: `/Admin/Users`. Routing v Razor Pages je konvenční — odráží strukturu složek.

# Q: Co je PageModel?
A: Třída v `.cshtml.cs` souboru. Drží data stránky (vlastnosti) + handler metody (`OnGet`, `OnPost`...) zpracovávající HTTP requesty.

# Q: Co je handler v Razor Pages?
A: Veřejná metoda v PageModelu zpracovávající konkrétní HTTP request. Konvence: `OnGet()` pro GET, `OnPost()` pro POST. Async varianty: `OnGetAsync()`, `OnPostAsync()`.

# Q: Co jsou pojmenované handlery a kdy se používají?
A: Když má stránka více POST akcí (např. Save a Delete). Místo `OnPost` se píše `OnPostSave()` a `OnPostDelete()`. V HTML pak `<button asp-page-handler="Save">`.

# MCQ: Které návratové typy může handler vracet?
- !Page(), RedirectToPage(), NotFound(), BadRequest()
- Pouze void
- Pouze IActionResult
- Pouze string s HTML
> Handler vrací `IActionResult` — Page() (zůstat na stránce), RedirectToPage() (HTTP 302), NotFound() (404), BadRequest() (400). Lze i `void` — automaticky renderuje stránku.

# Q: Jak handler dostane parametry z URL?
A: Jako argumenty metody. `public IActionResult OnGet(int id)` → request `/Detail?id=42` přiřadí `id = 42` automaticky.

# Q: Co je Razor syntax?
A: Šablonovací jazyk pro vkládání C# do HTML. Přepínání mezi HTML a C# přes znak `@`. Umožňuje dynamicky generovat HTML podle dat.

# CLOZE: Inline Razor výraz: `<h1>{{@Model.Name}}</h1>`. Blok C# kódu: `{{@{ var x = 5; }}}`.

# CODE: Razor řídicí konstrukce — výpis seznamu
```cshtml
@if (Model.Items.Any())
{
    <ul>
    @foreach (var item in Model.Items)
    {
        <li>@item.Name — @item.Price Kč</li>
    }
    </ul>
}
else
{
    <p>Žádné položky.</p>
}
```

# Q: Co je `_Layout.cshtml`?
A: Sdílená šablona definující rám stránek (header, nav, footer). Jednotlivé stránky se vykreslí v místě `@RenderBody()`. Pattern jako master page nebo layout v ostatních frameworcích.

# Q: Co je služba v ASP.NET Core?
A: Třída obsahující business logiku aplikace (např. `OrderService`, `EmailSender`). Registruje se v DI kontejneru v `Program.cs`. PageModely / kontrolery ji dostávají automaticky přes konstruktor.

# Q: Co je Dependency Injection?
A: Návrhový vzor, kdy si třída nevytváří závislosti sama (`new XxxService()`), ale dostává je zvenčí přes konstruktor. ASP.NET Core má DI kontejner zabudovaný — výrazně zvyšuje testovatelnost a vyměnitelnost implementace.

# CODE: DI v konstruktoru PageModelu
```csharp
public class IndexModel : PageModel
{
    private readonly OrderService _orders;

    public IndexModel(OrderService orders)  // DI dodá automaticky
    {
        _orders = orders;
    }

    public void OnGet()
    {
        var list = _orders.GetAll();
    }
}
```

# CODE: Registrace služby v Program.cs
```csharp
builder.Services.AddScoped<OrderService>();
builder.Services.AddSingleton<CacheService>();
builder.Services.AddTransient<EmailSender>();
```

# Q: Tři lifetime typy služeb a kdy je použít?
A: **Singleton** = 1 instance pro celou aplikaci (cache, config, logging). **Scoped** = 1 instance na 1 HTTP request (DbContext, business services). **Transient** = pokaždé nová instance (lehké stateless služby, mailery).

# CLOZE: Mantra: {{Singleton}} žije navždy, {{Scoped}} žije s requestem, {{Transient}} žije jen na voláním.

# MCQ: Pro DbContext (Entity Framework) je správný lifetime...
- Singleton
- !Scoped
- Transient
- AddDbContext sám rozhodne
> DbContext je Scoped — jedna instance na request, sdílená všemi komponentami v daném requestu. Singleton by způsoboval race conditions, Transient by zbytečně vytvářel připojení.

# Q: Co je `appsettings.json`?
A: Centrální konfigurační soubor v rootu projektu. Obsahuje connection stringy, API klíče, URLs, log levels. Mění se bez nutnosti rekompilace.

# Q: Co je `appsettings.Development.json` × `appsettings.Production.json`?
A: Per-prostředí varianty appsettings.json. Framework načte správnou podle aktuálního prostředí (proměnná `ASPNETCORE_ENVIRONMENT`). V devu třeba lokální DB, v prodzu cloud DB.

# Q: Kam patří citlivé sekrety (API klíče, hesla k DB)?
A: **NIKDY do appsettings.json** (commitlo by se do gitu). V devu **User Secrets** (`dotnet user-secrets`), v produkci **environment variables** nebo **Azure Key Vault** / Hashicorp Vault.

# CODE: Čtení konfigurace přes DI
```csharp
public class IndexModel : PageModel
{
    private readonly IConfiguration _config;

    public IndexModel(IConfiguration config) { _config = config; }

    public void OnGet()
    {
        var conn = _config.GetConnectionString("Default");
        var apiKey = _config["ExternalApi:Key"];
    }
}
```

# MCQ: Razor Pages × MVC — hlavní rozdíl?
- Razor Pages podporují async, MVC ne
- !Razor Pages = page-based (file-pair na stránku), MVC = controller-based (akce ve sdílených složkách)
- MVC neumí Razor syntax
- Razor Pages je deprecated
> Razor Pages sjednocují logiku a view do file-pair na stránku. MVC odděluje controllery, views, modely. Pro CRUD/formuláře Razor Pages, pro velké multi-action aplikace MVC.

# MCQ: Pro statický web bez DB a logiky bychom v ASP.NET Core použili...
- Razor Pages
- MVC
- !Minimal API + statické soubory / žádný framework
- Blazor Server
> Pro čistě statický web je ASP.NET overkill. Minimal API + UseStaticFiles, případně jen statický hosting (S3, Netlify).

# FREE: Vysvětli cestu HTTP requestu od klienta přes ASP.NET Core až po response.
> 1) Klient pošle request. 2) Pokud je před Kestrelem reverzní proxy (NGINX/IIS), proxy terminuje TLS a předá request Kestrelu. 3) Kestrel přijme request a předá ho ASP.NET Core middleware pipeline. 4) Request projde sérií middleware (HTTPS redirect, static files, routing, authentication, authorization). 5) Routing vybere konkrétní endpoint (Razor Page / controller akce / Minimal API). 6) Handler endpoint zpracuje (zavolá služby přes DI, načte data, vrátí IActionResult). 7) Response prochází pipeline zpět (filtry, headers) a Kestrel ji pošle klientovi.

# Q: Co je `[BindProperty]`?
A: Atribut nad property v PageModelu. Při POST requestu automaticky naplní property hodnotami z formuláře (model binding). Bez něj by se musely parametry přijímat ručně.

# Q: Co je `IActionResult`?
A: Návratový typ handleru / akce v ASP.NET. Reprezentuje výsledek zpracování HTTP requestu. Konkrétní implementace: `PageResult`, `RedirectToPageResult`, `NotFoundResult`, `JsonResult`, `FileResult`...
