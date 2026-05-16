---
title: DAT 18 — Razor Pages
description: GET/POST handlery, model binding, návratové metody, TempData, routování, pretty URL, validace
tags: [maturita, dat, web, aspnet, razor-pages]
---

# Q: Jaké soubory tvoří jednu Razor Page?
A: **Dvojice souborů**: `.cshtml` (HTML šablona + Razor syntax) a `.cshtml.cs` (PageModel s C# logikou a handlery). Soubory musí mít stejný název.

# Q: Co je PageModel?
A: C# třída v `.cshtml.cs`, dědí z `PageModel`. Drží data stránky (vlastnosti) a handler metody (`OnGet`, `OnPost`...) zpracovávající HTTP requesty.

# CLOZE: Handler `{{OnGet}}` zpracuje GET request (první načtení), handler `{{OnPost}}` zpracuje POST request (odeslání formuláře).

# Q: Co znamená `[BindProperty]`?
A: Atribut nad property v PageModelu. Při POSTu ASP.NET automaticky naplní property hodnotami z formulářových polí (model binding). Jména polí matchne podle property.

# Q: Jak se získá hodnota z URL query stringu (např. `?email=anna@example.com`)?
A: Přes `[BindProperty(SupportsGet = true)]` nad property. Při GETu se property naplní hodnotou z URL.

# CODE: Property pro formulář s validací
```csharp
[BindProperty]
public InputModel Input { get; set; } = new();

public class InputModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required, StringLength(50)]
    public string Name { get; set; } = "";
}
```

# Q: Jak se kontroluje, že formulář prošel validací?
A: V handleru `OnPost()` se kontroluje `ModelState.IsValid`. Pokud `false`, vrátí se `Page()` (re-render s chybovými hláškami). Pokud `true`, pokračuje zpracování.

# CODE: Typický POST handler s validací
```csharp
public IActionResult OnPost()
{
    if (!ModelState.IsValid)
        return Page();

    SaveToDatabase(Input);
    return RedirectToPage("Confirm");
}
```

# MCQ: Co znamená `return Page()` v handleru?
- Stránka přestane existovat
- !Vrátí samotnou stránku (re-render šablony)
- Vrátí prázdnou stránku
- Přesměruje na default
> `Page()` = "zůstaň na této stránce, vyrenderuj šablonu znovu s aktuálními daty". Typicky při chybě validace.

# MCQ: Co znamená `return RedirectToPage("Confirm")`?
- Vrátí HTML stránky Confirm přímo
- !Pošle klientovi HTTP 302, klient zavolá GET /Confirm
- Skopíruje data do Confirm
- Spustí Confirm.OnGet() přímo
> RedirectToPage = HTTP 302. Klient na něj reaguje novým GET requestem. Server volá `ConfirmModel.OnGet()` v rámci toho nového requestu.

# Q: Co je PRG pattern (Post-Redirect-Get)?
A: Pattern: po úspěšném POSTu **vždy redirect**, nikdy přímo re-render. Důvod: pokud uživatel zmáčkne F5 na výsledné stránce, prohlížeč by jinak POST opakoval → duplicitní uložení dat.

# Q: Co je TempData?
A: Slovník dostupný **jeden následující request** (typicky po redirectu). Drží se v cookie / session, automaticky se maže po přečtení. Slouží pro předání dat mezi stránkami v PRG patternu.

# CODE: TempData — uložení a čtení
```csharp
// V RegisterModel.OnPost()
TempData["Email"] = Input.Email;
return RedirectToPage("Confirm");

// V ConfirmModel.OnGet()
public void OnGet()
{
    Email = TempData["Email"] as string;
}
```

# MCQ: TempData podporuje jaké typy dat?
- Pouze string
- !Primitivní typy (string, int, bool...). Pro objekty serializovat na JSON
- Cokoliv, automaticky se serializuje
- Pouze čísla
> TempData je interně cookie/session, podporuje primitiva. Pro objekt → `JsonSerializer.Serialize(obj)` před uložením.

# Q: Jak Razor Pages určuje, která stránka odpovídá URL?
A: **Konvenční routing** — odpovídá struktuře adresářů. `Pages/Register.cshtml` → `/Register`, `Pages/Admin/Users.cshtml` → `/Admin/Users`. File-based routing, podobně jako Next.js.

# Q: Co je pretty URL v Razor Pages?
A: Custom route definovaná přes `@page` direktivu s parametry. Místo `/Detail?id=42` se použije `/Detail/42`.

# CODE: Pretty URL s typovaným parametrem
```cshtml
@page "{id:int}"
@model DetailModel

<h1>Detail produktu @Model.Id</h1>
```

# CLOZE: Volitelný parametr v pretty URL: `@page "{{ {slug?} }}"`.

# Q: Co jsou pojmenované handlery a kdy se používají?
A: Když má stránka více POST akcí (Save, Delete...). Místo `OnPost` se píše `OnPostSave()`, `OnPostDelete()`. V HTML se vybírá tlačítkem `<button asp-page-handler="Save">`.

# CODE: Pojmenované handlery — server + klient
```csharp
public IActionResult OnPostSave() { /* ... */ return RedirectToPage("List"); }
public IActionResult OnPostDelete(int id) { /* ... */ return RedirectToPage("List"); }
```
```html
<button type="submit" asp-page-handler="Save">Uložit</button>
<button type="submit" asp-page-handler="Delete" asp-route-id="42">Smazat</button>
```

# Q: Jak handler dostane parametr z URL?
A: Jako argument metody. Když handler je `OnGet(int id)` a URL je `/Detail?id=42` (nebo `/Detail/42` s pretty URL), framework `id` automaticky předá.

# MCQ: Která návratová metoda se použije pro chybu "stránka neexistuje"?
- Page()
- BadRequest()
- !NotFound()
- RedirectToPage("404")
> `NotFound()` vrátí HTTP 404. Lze i `NotFoundResult` s vlastním obsahem.

# Q: Vyjmenuj 5 nejpoužívanějších návratových metod handleru.
A: **Page()** (re-render), **RedirectToPage("X")** (redirect na jinou stránku), **NotFound()** (404), **BadRequest()** (400), **Content("text")** (plain text).

# Q: Jak se v Razor Pages dělá server-side validace?
A: Atributy nad property v `InputModel` (`[Required]`, `[EmailAddress]`, `[StringLength]`, `[Range]`). V handleru se kontroluje `ModelState.IsValid`, při chybě se vrátí `Page()`.

# Q: Jak se zobrazí validační chyby v HTML?
A: Přes Tag Helper `asp-validation-for`:
```html
<input asp-for="Input.Email" />
<span asp-validation-for="Input.Email" class="text-danger"></span>
```

# Q: Co je `_Layout.cshtml`?
A: Sdílená HTML kostra (header, nav, footer) pro všechny stránky. Jednotlivé stránky se vykreslí v místě `@RenderBody()`. Konvence: layout v `Pages/Shared/_Layout.cshtml`.

# Q: Co je `_ViewStart.cshtml`?
A: Soubor, který se spustí před každou stránkou v daném adresáři. Typicky definuje `Layout = "_Layout"` pro všechny stránky.

# Q: Jak se v Razor Pages řeší CSRF (Cross-Site Request Forgery)?
A: Anti-forgery token se přidává **automaticky** do všech POST formulářů přes `<form method="post">`. Framework při POSTu token ověřuje. Žádná manuální konfigurace.

# Q: Jak se v PageModelu dostane k databázi / službě?
A: Přes **Dependency Injection** v konstruktoru:
```csharp
public RegisterModel(UserDatabase db) { _db = db; }
```
Služba musí být zaregistrovaná v `Program.cs` přes `builder.Services.AddScoped<UserDatabase>()`.

# FREE: Popis flow registračního formuláře od kliknutí na "Registrovat" po zobrazení potvrzovací stránky.
> 1) Klient odešle POST `/Register` s daty formuláře. 2) Kestrel přijme request, middleware ho propustí, routing najde `Pages/Register.cshtml.cs`. 3) DI vytvoří `RegisterModel` (vstříkne závislosti). 4) Model binding naplní `Input` property z formuláře. 5) Validace ([Required] atd.) - pokud selže, `ModelState.IsValid` je false, vrátí se `Page()`. 6) Pokud OK, data se uloží (DB / TempData). 7) `RedirectToPage("Confirm")` vrátí HTTP 302. 8) Klient pošle GET `/Confirm`. 9) `ConfirmModel.OnGet()` čte TempData a renderuje potvrzovací stránku.

# FREE: Vysvětli rozdíl mezi BindProperty pro POST a pro GET.
> Bez `SupportsGet` se property naplní jen při POSTu z formulářových dat. S `SupportsGet = true` se naplní i při GETu z URL query stringu (`?email=anna@example.com`). Typicky se POST varianta používá pro formuláře, GET varianta pro předvyplnění formuláře hodnotou z URL.
