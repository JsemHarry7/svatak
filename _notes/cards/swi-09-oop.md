---
title: SWI 9 — Objektové programování
description: OOP, třídy, objekty, 3 pilíře, dědičnost, polymorfismus, abstrakce, interface, generika
tags: [maturita, swi, prg, oop, csharp]
---

# Q: Co je třída a co je objekt?
A: Třída je **šablona** (popis), definuje, jak objekt vypadá a co umí. Objekt (instance) je konkrétní výtvor podle třídy, vytvořený přes `new`. Analogie: třída = plán domu, objekt = postavený dům.

# Q: Vyjmenuj 3 pilíře OOP.
A: Zapouzdření (encapsulation), dědičnost (inheritance), polymorfismus.

# Q: Co je zapouzdření?
A: Skrytí vnitřních dat objektu a vystavení jen toho, co je bezpečné, přes vnější rozhraní. Realizováno přes modifikátory přístupu (private, protected, public). V C# pattern: private field + public property s validací.

# Q: Co je dědičnost?
A: Schopnost vytvořit novou třídu na základě existující — potomek získá vlastnosti a metody předka. V C# přes `class Student : Clovek`. C# dovoluje jen jednu třídu rodiče, ale více interfaces.

# CLOZE: Polymorfismus má dvě formy: {{přetěžování}} (overloading — víc metod stejného jména s jinými parametry) a {{override}} (potomek přepíše chování metody předka).

# CLOZE: V C# se přetížení podle instance realizuje klíčovými slovy {{virtual}} u předka a {{override}} u potomka.

# CLOZE: Klíčové slovo {{static}} označuje člen patřící třídě, ne instanci. Volá se přes jméno třídy bez `new`.

# MCQ: Které z následujících platí pro overloading vs override?
- !Overloading je rozhodnut při překladu (compile-time), override za běhu (runtime)
- Override je rozhodnut při překladu, overloading za běhu
- Obojí je rozhodnuto za běhu
- Obojí je rozhodnuto při překladu
> Klasická past! Overloading: kompilátor vidí typy argumentů a vybere variantu při překladu (static polymorphism). Override: skutečný typ objektu se zjistí až za běhu (dynamic polymorphism).

# MCQ: Co se vypíše?
```cs
Clovek osoba = new Student();
osoba.Pozdrav();
```
Předpoklady: `Pozdrav` je virtual v Clovek a override v Student.
- "Ahoj!" (z Cloveka)
- !"Čau, jsem student!" (ze Studenta)
- Compile error
- Runtime error
> Virtual + override = runtime polymorphism. I když je proměnná typu Clovek, skutečný typ za referencí je Student, a Pozdrav je virtual/override, takže se volá metoda Studenta.

# MCQ: Jaký je rozdíl mezi field a property v C#?
- Není žádný, jsou totéž
- !Property má get/set akcesory, lze validovat při zápisu/čtení; field je přímý přístup
- Property je vždy soukromá, field veřejná
- Field je rychlejší
> Property je get/set wrapper, který může obsahovat validační logiku. Field je obyčejná proměnná v třídě. C# konvence: preferuj property nad public field.

# MCQ: Kdy zvolíš abstraktní třídu a kdy interface?
- Vždy interface
- !Abstraktní třída pro "JE TO X" (sdílený základ + případné hotové metody). Interface pro "UMÍ TO X" (capability)
- Abstraktní třída pro malé objekty, interface pro velké
- Není rozdíl
> Abstraktní třída říká *co to JE* (Pes je Zvíře). Interface říká *co to UMÍ* (Pes umí Plavat). Třída může dědit max z 1 abstraktní třídy, ale implementovat víc interfaces.

# FREE: Vysvětli polymorfismus a jeho 2 formy.
> Polymorfismus = schopnost objektů chovat se různě podle situace nebo typu. Dvě formy: 1) **Overloading (přetěžování)** — víc metod stejného jména, ale jiných parametrů: `Vypis(int)` × `Vypis(string)`. Kompilátor vybere variantu při překladu (compile-time, static polymorphism). 2) **Override** — potomek změní chování metody předka, vyžaduje `virtual` u předka a `override` u potomka. Rozhodnutí se dělá za běhu podle skutečného typu objektu (runtime, dynamic polymorphism).

# FREE: Vysvětli rozdíl mezi abstraktní třídou a interface.
> Abstraktní třída = nehotová třída, nelze z ní vytvořit instanci. Slouží jako společný základ. Může obsahovat hotové metody (s tělem) i abstraktní (bez těla, potomek musí dopsat). Interface = smlouva, říká CO třída umí, ne JAK. Tradičně bez kódu, jen seznam metod. Třída dědí max z 1 abstraktní třídy, ale implementuje víc interfaces. Mantra: *"abstraktní = co to JE, interface = co to UMÍ"*.

# FREE: Vysvětli generika v C# a uveď příklad.
> Generika `<T>` = šablona pro typy. Umožňuje napsat metodu nebo třídu univerzálně pro různé datové typy, konkrétní typ se doplní až při použití. Příklad: `List<int>` pro čísla, `List<string>` pro řetězce — stejná logika, různé typy. Výhody: type safety (kompilátor zachytí špatný typ), žádný boxing/unboxing (rychlejší než ArrayList s object), reusability.

# FREE: Popiš typický pattern enkapsulace v C# (private field + property).
> V C# se data skrývají do private fieldu, navenek se vystaví přes public property s get/set akcesory, které mohou obsahovat validační logiku. Tím se zabrání, aby cizí kód nastavil neplatné hodnoty (např. záporný věk).

# CODE: V C# napiš abstraktní třídu Zvire s abstraktní metodou Zvuk a třídu Pes, která ji dědí.
```cs
abstract class Zvire {
    public string Jmeno { get; set; }
    public void Spi() => Console.WriteLine($"{Jmeno} spí.");
    public abstract void Zvuk();    // bez těla, potomek musí dopsat
}

class Pes : Zvire {
    public override void Zvuk() => Console.WriteLine("Haf!");
}
```

# CODE: V C# napiš interface IPohyb a třídu Auto, která ho implementuje.
```cs
interface IPohyb {
    void Jed();
    int RychlostKmh { get; }
}

class Auto : IPohyb {
    public int RychlostKmh { get; set; }
    public void Jed() => Console.WriteLine("Jedu " + RychlostKmh + " km/h");
}
```

# CODE: V C# napiš generickou třídu Krabice<T>.
```cs
class Krabice<T> {
    public T Obsah { get; set; }
    public void Vypis() => Console.WriteLine(Obsah);
}

Krabice<int> k = new Krabice<int> { Obsah = 42 };
Krabice<string> s = new Krabice<string> { Obsah = "ahoj" };
```

# Q: Co je `static` a uveď příklad volání.
A: `static` označuje člen patřící **třídě**, ne instanci. Volá se přes jméno třídy bez `new`. Příklad: `Math.Sqrt(16)`, `Console.WriteLine("...")`, `int.Parse("42")` — všechno statické metody.

# Q: Dědí potomek konstruktor předka?
A: Ne. Konstruktor se v C# **nedědí** — potomek si musí napsat svůj. Může ho ale zavolat přes `: base(...)` v hlavičce, aby inicializoval předka.

# Q: Co je `sealed` třída?
A: Třída, ze které **nelze dědit**. Použití: bezpečnost (chceš zaručit, že nikdo logiku nepřepíše) a performance (kompilátor může lépe optimalizovat).
