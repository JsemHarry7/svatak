---
title: DAT 19 — Tag Helpers a formuláře
description: asp-for, asp-items, asp-validation-for, asp-page-handler, asp-route, validační atributy, klient vs server validace
tags: [maturita, dat, web, aspnet, tag-helpers]
---

# Q: Co jsou Tag Helpers v ASP.NET Core?
A: Speciální HTML atributy s prefixem `asp-*`, které server-side rozšiřují HTML o data z PageModelu. Razor parser je při renderování přepíše na standardní HTML (name, id, value, data-val-*). Klient nikdy nevidí `asp-*`.

# Q: Kde se Tag Helpers aktivují?
A: V `_ViewImports.cshtml` direktivou `@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`. Default scaffold to už má, není třeba psát.

# CLOZE: `<input asp-for="Input.Email" />` Razor vyrenderuje jako `<input name="{{Input.Email}}" id="{{Input_Email}}" value="..." />`.

# Q: Co všechno přidá `asp-for` do `<input>`?
A: **name** (pro model binding), **id** (label-input pair), **value** (předvyplnění z property), **type** (odvozené z C# typu / atributů), **data-val-*** atributy (pro klientskou validaci).

# MCQ: Property `[EmailAddress] public string Email` s `<input asp-for="Input.Email" />` vyrenderuje atribut `type=`...
- text
- string
- !email
- input
> Razor parser čte atributy property a odvozuje vhodný HTML input type. `[EmailAddress]` → `type="email"`.

# Q: Jak `<label asp-for>` funguje?
A: Vyrenderuje `<label for="Input_X">`, automaticky napojí label na odpovídající input se stejným id. Text labelu se vezme z atributu `[Display(Name = "...")]` na property, jinak default property jméno.

# CODE: Dropdown s Tag Helpers
```html
<select asp-for="Input.Course"
        asp-items="@(new SelectList(Model.Courses))"
        class="form-select">
    <option value="">-- Zvolte kurz --</option>
</select>
```

# Q: Rozdíl mezi `asp-for` a `asp-items` v `<select>`?
A: **`asp-for`** = kam uložit zvolenou hodnotu (binding). **`asp-items`** = co nabídnout v dropdownu (kolekce možností typicky v `new SelectList(...)`).

# Q: Jak SelectList přijme objekty (ne jen stringy)?
A: `new SelectList(kolekce, "ValueProp", "TextProp")`. Např. `new SelectList(Model.Courses, "Id", "Name")` — value bude Id, zobrazený text bude Name.

# Q: Co je `<span asp-validation-for="Input.Email">`?
A: Tag Helper pro zobrazení **chybové hlášky** pro konkrétní property. Po POSTu se naplní textem z ModelState chyby pro Input.Email. Pokud nejsou chyby, je prázdný.

# Q: Co je `<div asp-validation-summary="All">`?
A: Tag Helper pro **souhrnný výpis VŠECH validačních chyb**. Hodnoty: `All` (všechno), `ModelOnly` (jen model-level), `None`.

# Q: Vyjmenuj 5 nejpoužívanějších validačních atributů.
A: **[Required]**, **[EmailAddress]**, **[StringLength(max, MinimumLength)]**, **[Range(min, max)]**, **[RegularExpression(pattern)]**. Plus [Compare], [Phone], [Url].

# CODE: InputModel s validacemi
```csharp
public class InputModel
{
    [Required(ErrorMessage = "Jméno je povinné")]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Range(18, 120)]
    public int Age { get; set; }
}
```

# Q: Jak handler ověří, že formulář prošel validací?
A: Kontroluje `ModelState.IsValid` v handleru. `true` = všechny validační atributy prošly, lze pokračovat. `false` = vrátit `Page()` s chybami.

# CODE: POST handler s validací
```csharp
public IActionResult OnPost()
{
    if (!ModelState.IsValid)
        return Page();

    _db.Add(Input);
    return RedirectToPage("List");
}
```

# Q: Co je klientská validace v Razor Pages?
A: JavaScript validace, která kontroluje formulář před odesláním na server (bez round-tripu). Čte `data-val-*` atributy, používá jQuery Validation. Aktivuje se přidáním `<partial name="_ValidationScriptsPartial" />` v `@section Scripts`.

# MCQ: Klientská validace stačí pro bezpečnost?
- Ano, JS běží v prohlížeči
- Ano, pokud má všechny atributy
- !Ne, je to jen UX vrstva; útočník ji obejde
- Ne, je pomalá
> Klient může JS vypnout, manipulovat s DOM, posílat requesty přes Postman/curl. Server-side validace MUSÍ proběhnout vždy — jen ona je bezpečnost.

# Q: Co je pojmenovaný handler?
A: Metoda `OnPostXxx()` (nebo `OnGetXxx()`) v PageModelu s vlastním jménem. Umožňuje mít víc akcí na jedné stránce — např. Save, Delete, Approve.

# CODE: Pojmenovaný handler + HTML tlačítko
```csharp
public IActionResult OnPostDelete(int id)
{
    _db.Remove(id);
    return RedirectToPage();
}
```
```html
<button type="submit" asp-page-handler="Delete" asp-route-id="@item.Id">Smazat</button>
```

# Q: Co dělá `asp-page-handler="Delete"`?
A: Říká frameworku, že tento submit má vyvolat handler `OnPostDelete()`, ne default `OnPost()`.

# Q: Co dělá `asp-route-id="42"`?
A: Přidá parametr `id=42` do URL (jako query string `?id=42` nebo pretty URL `/page/42`, podle routing pravidel). Handler ho zachytí přes parametr metody (`OnPostDelete(int id)`).

# CLOZE: Odkaz na detail záznamu: `<a asp-page="/Detail" {{asp-route-id="@item.Id"}}>Detail</a>`.

# CODE: Tabulka s Detail/Delete tlačítky
```html
@foreach (var item in Model.Items)
{
    <tr>
        <td>@item.Name</td>
        <td>
            <form method="post" class="d-inline">
                <button type="submit" asp-page-handler="Detail" asp-route-id="@item.Id" class="btn btn-info">Detail</button>
                <button type="submit" asp-page-handler="Delete" asp-route-id="@item.Id" class="btn btn-danger">Smazat</button>
            </form>
        </td>
    </tr>
}
```

# Q: Rozdíl mezi `asp-page` a `asp-action`?
A: **`asp-page`** = link na Razor Page. **`asp-action`** + **`asp-controller`** = link na MVC controller akci. Nemíchat.

# MCQ: V Razor Pages aplikaci se odkaz na jinou stránku píše přes...
- href="/X" ručně
- !asp-page="/X"
- @Url.Page("X")
- @Html.Link("X")
> `asp-page` je idiomatic Tag Helper. Framework vyrenderuje správnou URL podle routing pravidel.

# Q: Co se stane, když má input atribut `data-val-required="text"` (auto-přidaný Tag Helperem) a uživatel nechá pole prázdné?
A: **Bez** `_ValidationScriptsPartial` — nic, formulář se odešle, server zachytí chybu (ModelState.IsValid = false). **S** partial — jQuery validate hned po blur/submit zobrazí klientskou chybu.

# Q: Co je `[Display(Name = "...")]` atribut a co dělá?
A: Atribut nad property, který určuje **lokalizované jméno** pro UI. `<label asp-for="X">` ho vezme jako text labelu místo C# property jména. Bez něj by label byl třeba "Email", s `[Display(Name = "E-mailová adresa")]` by byl plný český text.

# MCQ: `asp-antiforgery="false"` v `<form>`...
- Zapne CSRF ochranu
- !Vypne CSRF token (default je on, ne vypínat bez důvodu)
- Zruší validaci
- Vypne POST
> CSRF token je default v ASP.NET Razor Pages forms. Vypnutí = bezpečnostní díra (Cross-Site Request Forgery útoky).

# FREE: Vysvětli, co se přesně stane v HTML při renderování `<input asp-for="Input.Email" />` pokud Email má `[Required, EmailAddress]`.
> Razor parser přečte property typ (string) a atributy. Vyrenderuje: `<input type="email" name="Input.Email" id="Input_Email" value="@Model.Input.Email" data-val="true" data-val-required="The Email field is required." data-val-email="The Email field is not a valid e-mail address." />`. Klient pak vidí tohle čisté HTML — `asp-for` zmizí. Data-val-* atributy umožní jQuery Validation klientskou validaci, name/id propojí input s server-side bindingem.

# FREE: Popis flow pojmenovaného handleru: uživatel klikne "Smazat" → server → zpět.
> 1) Uživatel klikne `<button type="submit" asp-page-handler="Delete" asp-route-id="42">`. 2) Browser pošle POST na URL `/List?handler=Delete&id=42`. 3) Routing najde `ListModel`, framework vidí `handler=Delete` → volá metodu `OnPostDelete(int id)` místo default `OnPost()`. 4) Handler smaže záznam s id=42 v DB. 5) Vrátí `RedirectToPage()` — refresh stejné stránky. 6) Klient pošle GET `/List`. 7) OnGet načte aktuální seznam (bez smazaného záznamu) a vyrenderuje stránku.
