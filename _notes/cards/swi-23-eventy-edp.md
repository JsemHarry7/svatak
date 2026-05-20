---
title: SWI 23 — Událostmi řízené programování
description: EDP paradigma, Publisher/Subscriber/Event, Observer × Pub-Sub, delegát + event v C#, addEventListener v JS, memory leak, bubbling/delegation
tags: [maturita, swi, edp, eventy, observer, csharp, javascript]
---

# Q: Co je EDP (Event-Driven Programming)?
A: Programovací paradigma, kde **tok programu neurčuje lineární kód, ale události** — kliky, signály, network requests, timery. Místo "udělej 1, 2, 3" aplikace **čeká a reaguje na vstupy**.

# CLOZE: Imperativní paradigma — {{kód}} ovládá tok. EDP — {{události}} ovládají tok.

# Q: 3 klíčové role v EDP?
A: **Publisher** (objekt, kde událost vzniká — tlačítko, časovač), **Subscriber** (objekt, který chce reagovat, má Event Handler), **Event** (most mezi nimi — seznam handlerů).

# Q: Co je Publisher v EDP?
A: Objekt, ve kterém **událost vzniká** (tlačítko, časovač, senzor). **Neví, kdo poslouchá** — je mu jedno, jestli reaguje 1 subscriber nebo 100 nebo nikdo. Definuje událost + vystřelí ji.

# Q: Co je Subscriber v EDP?
A: Objekt, který chce **reagovat na událost**. Přihlásí se (subscribe) k Publisheru a obsahuje **Event Handler** — metodu, která se vykoná při události.

# Q: Co je Event Handler?
A: **Metoda Subscribera**, která se vykoná, když přijde událost. V .NET má konvenční signaturu `void Handler(object sender, EventArgs e)`.

# Q: Co je loose coupling v EDP?
A: **Publisher a Subscriber se neznají přímo.** Tlačítko neví, co se má stát po kliku; posluchač neví, kdo událost vystřelil. Komunikují přes rozhraní events.

# Q: 3 výhody loose coupling?
A: **Modularita** (přidávat/odebírat subscribery bez změny publishera), **znovupoužitelnost** (publisher je univerzální), **testovatelnost** (snadné mockování).

# Q: Hlavní návrhový vzor v EDP?
A: **Observer pattern** (GoF, behaviorální). Subject (Publisher) drží **seznam observerů** a sám je notifikuje voláním `notify()`. Komunikace **1:N přímá**.

# Q: Rozdíl Observer × Pub-Sub?
A: **Observer:** Subject drží **přímou referenci** na observery, 1:N. **Pub-Sub:** Publisher nemá referenci, posílá přes **prostředníka (broker/event bus)**, subscribers se přihlašují k **topic**, N:M. Typicky Kafka/RabbitMQ.

# MCQ: Komunikace mezi mikroslužbami přes Kafka — který pattern?
- Observer
- !Pub-Sub
- Singleton
- Factory Method
> Kafka je message broker, mikroslužby publishují a subscriberi se přihlašují k topicům — Pub-Sub. Observer by byl pro přímou komunikaci uvnitř aplikace.

# Q: Co je delegát v C#?
A: **Předpis pro tvar metody** (parametry + návratový typ). Funguje jako **typesafe ukazatel na funkci** — metody s daným podpisem se do něj dají uložit.

# CODE: Delegát + event v C#
```csharp
public delegate void VideoEncodedEventHandler(object source, EventArgs args);

public class VideoEncoder
{
    public event EventHandler<VideoEventArgs> VideoEncoded;

    protected virtual void OnVideoEncoded(string filename)
    {
        VideoEncoded?.Invoke(this, new VideoEventArgs { Filename = filename });
    }
}
```

# Q: Co dělá `event` klíčové slovo v C#?
A: **Zapouzdří delegát** — mimo třídu lze jen `+=` (subscribe) a `-=` (unsubscribe). Nelze událost vyvolat zvenku ani přepsat seznam handlerů na null. Bez `event` by kdokoli mohl manipulovat se seznamem.

# CLOZE: V C# subscribe = {{+=}}, unsubscribe = {{-=}}.

# Q: K čemu `?.Invoke` při vyvolání eventu?
A: **Null-conditional operator** — pokud event je null (nikdo neposlouchá), volání se přeskočí. Bez něj by `Invoke(...)` na null hodil `NullReferenceException`.

# Q: Konvenční signatura event handleru v .NET?
A: `void Handler(object sender, EventArgs e)`. **`sender`** = Publisher, **`e`** = data o události (EventArgs nebo dědič s vlastními properties).

# CODE: addEventListener v JS
```javascript
const button = document.getElementById("submit");

const handler = (e) => console.log("klik", e.clientX);
button.addEventListener("click", handler);   // subscribe
button.removeEventListener("click", handler); // unsubscribe (stejná reference!)
```

# Q: Klasický chyták s `removeEventListener`?
A: Musí dostat **přesně stejnou referenci**, jakou dostalo `addEventListener`. **Anonymní funkce nelze odebrat** — dvě anonymní funkce se stejným textem jsou různé objekty:
```javascript
button.addEventListener("click", () => console.log("klik"));
button.removeEventListener("click", () => console.log("klik"));  // ❌ nepomůže
```

# Q: Co je memory leak v EDP a jak vzniká?
A: **Publisher drží referenci na Subscribera** přes event handler. GC vidí živou referenci → Subscriber **nemůže být uvolněn**, i když ho aplikace už nepotřebuje. Vznik při **zapomenutém unsubscribe**.

# Q: Jak řešit memory leak v EDP?
A: **Vždy `unsubscribe`** když Subscriber končí (`-=` v C#, `removeEventListener` v JS). V moderním JS lze použít **AbortController** pro cleanup více listenerů naráz.

# CODE: AbortController (moderní JS cleanup)
```javascript
const controller = new AbortController();
const signal = controller.signal;

window.addEventListener("resize", handler1, { signal });
window.addEventListener("scroll", handler2, { signal });

controller.abort();   // zruší obě naráz
```

# Q: Co je bubbling v DOM?
A: **Event prochází od cíle směrem nahoru** k rodičovským elementům. Klik na `<button>` postupně proběhne na button → rodič `<div>` → další rodič → ... → document. Default chování.

# Q: Co je capturing v DOM?
A: **Opačná fáze bubblingu** — event prochází od kořene k cíli (shora dolů), pak teprve bublá zpět. Aktivuje se třetím parametrem `addEventListener(..., true)`. V praxi zřídka.

# Q: 3 fáze DOM eventu?
A: **1. Capturing** (document → ... → cíl). **2. Target** (cíl zpracuje handler). **3. Bubbling** (cíl → ... → document).

# Q: Co je `e.stopPropagation()` × `e.preventDefault()`?
A: **`stopPropagation`** = zastaví **bubbling** nahoru. **`preventDefault`** = zruší **default chování** (klik na link, submit form, scroll na anchor).

# Q: Co je event delegation a kdy je výhodné?
A: Místo listeneru na **každý prvek seznamu**, dáš ho na **rodiče** a zjistíš cíl přes `e.target`. **Výhody:** 1 listener bez ohledu na počet, funguje i pro **dynamicky přidané** elementy.

# CODE: Event delegation
```javascript
// ❌ Špatně: listener na každém li
items.forEach(li => li.addEventListener("click", handler));

// ✓ Lépe: jeden listener na rodiči
todoList.addEventListener("click", (e) => {
    if (e.target.tagName === "LI") {
        console.log(e.target.textContent);
    }
});
```

# Q: Custom Events v JS?
A: Vlastní eventy (nejen builtin click/scroll). Vytvoření: `new CustomEvent("mojeudalost", { detail: {...} })`. Vyvolání: `element.dispatchEvent(event)`. Subscribe: `addEventListener("mojeudalost", handler)`.

# Q: Node.js EventEmitter — 4 hlavní metody?
A: **`.on(event, handler)`** subscribe, **`.once(event, handler)`** subscribe jen na první emit, **`.off(event, handler)`** unsubscribe, **`.emit(event, ...args)`** vystřelí událost.

# Q: React Synthetic Events — co jsou?
A: **Wrapper nad DOM eventy** od Reactu. Důvod: konzistence napříč prohlížeči + event delegation pod kapotou. CamelCase (`onClick`), hodnota je funkce (`onClick={handler}`).

# Q: Co je Event Loop?
A: JS mechanismus pro asynchronní zpracování bez blokování. Smyčka: 1) vykonej sync kód z Call Stack. 2) Call Stack prázdný → vykonej **všechny microtasks** (Promise.then). 3) Vykonej **jeden macrotask** (setTimeout). 4) Render. 5) Opakuj.

# Q: Microtask × Macrotask priorita?
A: **Microtask vždy předbíhá macrotask.** Promise `.then()` má prioritu před `setTimeout(..., 0)`. Microtasks: `Promise.then`, `queueMicrotask`, `MutationObserver`. Macrotasks: `setTimeout`, DOM eventy, I/O.

# FREE: Popis celý flow kliku na tlačítko v EDP.
> 1) Uživatel klikne na `<button>`. 2) Browser vytvoří **MouseEvent** objekt s informacemi (souřadnice, target). 3) **Capturing fáze**: event prochází od `document` dolů k buttonu (handlery s `useCapture=true`). 4) **Target fáze**: handlery navázané přímo na button se spustí. 5) **Bubbling fáze**: event bublá nahoru přes rodiče (default handlery). 6) Pokud handler zavolá `e.stopPropagation()`, další fáze se zastaví. 7) Pokud zavolá `e.preventDefault()`, zruší se default browser chování (např. form submit). 8) Po skončení všech handlerů event končí.

# FREE: Vysvětli rozdíl mezi C# event keyword a obyčejným delegátem.
> Obyčejný delegát je veřejná proměnná — kdokoli mimo třídu může přepsat seznam (`= null`), vyvolat (`Invoke()`), nebo nahradit (`= newHandler`). To je bezpečnostní problém: třída ztrácí kontrolu nad svými událostmi. **`event` klíčové slovo** zapouzdří delegát — mimo třídu lze JEN `+=` (přidat handler) a `-=` (odebrat handler). Vyvolat lze jen uvnitř třídy. Tím se zaručí, že událost je opravdu vlastnictvím Publishera a Subscriberi se jen "připojují".
