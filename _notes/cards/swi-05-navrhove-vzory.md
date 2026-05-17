---
title: SWI 5 — Návrhové vzory
description: GoF 23 vzorů, 3 kategorie (vytvářecí/strukturální/behaviorální), Singleton, Observer, Factory Method, Command
tags: [maturita, swi, oop, design-patterns]
---

# Q: Co je návrhový vzor?
A: **Opakovaně použitelné řešení často se opakujícího problému** v OOP návrhu. Není to hotová knihovna ani kód — je to **způsob strukturování tříd a vztahů**.

# Q: Kdo definoval 23 návrhových vzorů a kdy?
A: **Gang of Four (GoF)** — Gamma, Helm, Johnson, Vlissides. Kniha *Design Patterns: Elements of Reusable Object-Oriented Software* (1994).

# Q: Tři kategorie GoF vzorů?
A: **Vytvářecí (creational)** = jak vytvářet objekty (Singleton, Factory Method, Builder). **Strukturální (structural)** = jak skládat objekty (Adapter, Composite, Facade). **Behaviorální (behavioral)** = komunikace + odpovědnost (Observer, Command, Strategy).

# CLOZE: Vytvářecí = {{jak vytvářet objekty}}. Strukturální = {{jak skládat objekty do celků}}. Behaviorální = {{komunikace a odpovědnost}}.

# MCQ: Singleton patří do kategorie...
- !Vytvářecí
- Strukturální
- Behaviorální
- Žádné
> Řeší, jak vytvořit (a omezit) instanci → vytvářecí.

# MCQ: Observer patří do kategorie...
- Vytvářecí
- Strukturální
- !Behaviorální
- Žádné
> Řeší komunikaci mezi Publisher a Subscribery → behaviorální.

# Q: Co je Singleton?
A: Vzor, který zaručí, že třída má **právě jednu instanci** v celé aplikaci + poskytuje **globální přístup**.

# Q: Tři klíčové prvky Singleton implementace?
A: **Privátní konstruktor** (nikdo zvenku nemůže `new`), **statická metoda** `getInstance()` (vrátí instanci, při prvním volání ji vytvoří), **statické pole** drží referenci.

# CODE: Singleton v C#
```csharp
class Logger
{
    private static Logger instance;
    private Logger() {}    // nikdo zvenku nemůže new
    public static Logger getInstance()
    {
        if (instance == null)
            instance = new Logger();   // lazy init
        return instance;
    }
}
```

# Q: Typická použití Singletonu?
A: **Logger** (jeden zápis do log souboru), **konfigurace** aplikace, **cache**, GameManager v Unity.

# Q: Co je lazy initialization u Singletonu?
A: Instance se vytvoří **až při prvním volání** `getInstance()`, ne při startu aplikace. Šetří paměť pokud se nikdy nepoužije.

# Q: Proč je Singleton často označován za anti-pattern?
A: Je to **globální proměnná v hávu OOP**. Vytváří **skryté závislosti** (třída A používá B přes Singleton, ale není to v rozhraní), **špatně se testuje** (nejde mockovat, drží stav mezi testy), **problémy ve vícevláknovém prostředí**.

# Q: Jaký je problém Singletonu ve vícevláknovém prostředí?
A: Dvě vlákna mohou současně projít `if (instance == null)` (oba uvidí null) → **vytvoří dvě instance**. Řešení: `synchronized`, **double-checked locking**, **enum implementace**, nebo C# `Lazy<T>`.

# Q: Co je Observer pattern?
A: Definuje **vztah 1:N** — když Publisher změní stav, všichni Subscriberi jsou **automaticky informováni** přes metodu `update()`.

# Q: 3 klíčové metody Publisher v Observer pattern?
A: **`Subscribe(observer)`** (přidá Subscribera), **`Unsubscribe(observer)`** (odebere), **`Notify(message)`** (zavolá `update()` na všech Subscriberech).

# Q: Co je loose coupling u Observer pattern?
A: Publisher a Subscriber se znají **jen přes rozhraní**, ne konkrétní typy. Publisher netuší, jakou konkrétní třídu Subscriber má — jen že má metodu `update()`.

# Q: Typická použití Observer?
A: **GUI eventy** (button click → handler), **C# `event` / `delegate`**, JavaScript `addEventListener`, **reaktivní knihovny** (RxJS), **MVC** (View pozoruje Model), **pub/sub messaging** (Kafka, MQTT).

# Q: Hlavní nevýhoda Observer pattern?
A: **Memory leaks** — pokud se Observer zapomene odhlásit, Publisher ho drží v seznamu → GC ho neuvolní. Plus neřízené pořadí notifikací + těžké debugovat kaskádu updatů.

# Q: Co je Factory Method?
A: Vzor, kdy třída **deleguje vytváření instancí na své podtřídy**. Klient pracuje s abstraktním rozhraním, podtřída rozhodne, jaký konkrétní typ vrátí.

# CODE: Factory Method
```csharp
abstract class Dialog
{
    public void Render()
    {
        Button b = CreateButton();   // Factory Method
        b.Render();
    }
    protected abstract Button CreateButton();
}
class WindowsDialog : Dialog
{
    protected override Button CreateButton() => new WindowsButton();
}
class MacDialog : Dialog
{
    protected override Button CreateButton() => new MacButton();
}
```

# Q: Typická použití Factory Method?
A: **Cross-platform UI** (jeden kód, různé widgety podle OS), **parsery** vybírané podle vstupu (XML/JSON/YAML), různé typy **dokumentů** v editoru, **ORM** EntityManager.

# Q: Hlavní výhoda Factory Method?
A: **Open/Closed Principle** — přidání nového typu = **přidat podtřídu**, ne měnit existující kód. Klient závisí jen na rozhraní, ne na konkrétních třídách.

# Q: Co je Command pattern?
A: Vzor, který **zabalí akci jako objekt** s metodou `Execute()` (často i `Undo()`). Umožní akci uložit do **fronty**, **logovat**, **vrátit zpět**, **zopakovat**.

# Q: 5 typických rolí v Command pattern?
A: **Command** (rozhraní s Execute/Undo), **ConcreteCommand** (konkrétní akce), **Receiver** (kdo akci provede), **Invoker** (spouštěč, např. tlačítko), **Client** (vytváří Command).

# Q: Typická použití Command?
A: **Undo/Redo** (Ctrl+Z) v Photoshop/IDE/Word, **makra** (sekvence commandů), **GUI tlačítka** (stejná akce z víc míst), **queueing/scheduling**, **audit log**, **network requesty**.

# Q: Hlavní výhoda Command pro implementaci Undo?
A: Každý Command si **pamatuje svůj stav** (backup před akcí). Stack commandů = historie. Undo = volá `Undo()` na vrchním commandu ze stacku.

# MCQ: Pokud má aplikace potřebovat Undo/Redo, který vzor použít?
- Singleton
- Observer
- Factory Method
- !Command
> Command zabalí akci jako objekt s Execute/Undo. Stack commandů = historie operací.

# MCQ: Pro propojení Model a View, aby se View aktualizoval při změně dat...
- Singleton
- !Observer
- Factory Method
- Command
> View se přihlásí jako Observer modelu. Při změně modelu se View automaticky aktualizuje.

# CODE: C# event = vestavěný Observer
```csharp
class Publisher
{
    public event Action<string> OnMessage;
    public void DoSomething()
    {
        OnMessage?.Invoke("Něco se stalo");
    }
}
// Subscriber
publisher.OnMessage += msg => Console.WriteLine(msg);
```

# Q: Rozdíl mezi Singleton a static class?
A: **Singleton** je instance — lze ji předat, mockovat, implementovat interface. **Static class** = jen statické metody, nelze instanciovat. Singleton flexibilnější, static jednodušší.

# Q: Co je SOLID a jaké principy patří mezi SOLID?
A: 5 principů OOP: **S**ingle Responsibility, **O**pen/Closed, **L**iskov Substitution, **I**nterface Segregation, **D**ependency Inversion. Návrhové vzory typicky některé z nich podporují.

# Q: Co je MVC?
A: **Model-View-Controller** — **architektonický vzor** (ne GoF). Odděluje **data** (Model), **UI** (View), **logiku zpracování** (Controller). Použito v ASP.NET MVC, ne Razor Pages.

# FREE: Jmenuj 3 typická použití každého ze 4 hlavních vzorů.
> **Singleton:** logger, konfigurace, cache. **Observer:** GUI eventy, MVC (View↔Model), pub/sub (Kafka). **Factory Method:** cross-platform UI, parsery vybírané podle vstupu, ORM. **Command:** undo/redo, makra, queueing/audit log.

# FREE: Proč jsou návrhové vzory užitečné a kdy je nepoužívat?
> **Užitečné:** ověřená řešení (neřešíš od nuly), společný slovník v týmu, podporují SOLID, zlepšují udržovatelnost a rozšiřitelnost. **Nepoužívat když:** je to overengineering (vzor přidaný "protože je elegantní"), jednoduchá funkce stačí, nemáš zkušenost s OOP (vyšší vstupní bariéra). Moderní jazyky (Kotlin, Python, JS) některé vzory řeší vestavěnými prostředky (lambdy, first-class funkce) bez celé struktury tříd.
