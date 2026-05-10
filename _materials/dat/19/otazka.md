### Výběr kurzu (`Index`)

- Na titulní stránce se zobrazuje `<select>` pro výběr kurzu.
- Po výběru kurzu je uživatel přesměrován na stránku pro přihlášení.
- ❗ Chybí správné Tag Helpery (`asp-for`, `asp-items`) pro zobrazení seznamu kurzů.

### Přihlašovací formulář (`Subscribe`)

- Na této stránce se uživatel přihlásí pomocí jména a e-mailu.
- Formulář musí dále předávat i informaci o zvoleném kurzu.
- Úspěšné odeslání uloží přihlášku do databáze a přesměruje na seznam.
- ❗ Je třeba doplnit `asp-*` atributy a validační atributy v `InputModel`, aby fungovalo ověření dat na straně serveru i klienta.

### Seznam přihlášek (`List`)

- Zobrazí [se všechny] odeslané přihlášky.
- U [každého záznamu jsou] dvě tlačítka:
  - [Detail — slouží] na zobrazení údajů
  - [Smazat — odstraní z]áznam přes příslušný handler
- ❗ [Je třeba doplnit `asp-page-handler` pr]o routování na handlery `Detail` a `D[elete]`.