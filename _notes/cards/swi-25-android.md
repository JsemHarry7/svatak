---
title: SWI 25 — Architektura a komponenty Android aplikací
description: Lístek: Architektura | Kernel, ART | 4 komponenty | Sandbox, Manifest, práva | Intent | Compose, MVVM, Lifecycle
tags: [maturita, swi, android]
---

# Q: Jakou má Android architekturu? Vyjmenuj 5 vrstev zdola nahoru.
A: **Zásobník vrstev:** 1) **Linux Kernel** (procesy, paměť, drivery, UID security). 2) **HAL** (Hardware Abstraction Layer — rozhraní HW). 3) **Android Runtime + Native Libraries** (ART + OpenGL/SQLite/WebKit). 4) **Application Framework** (Java/Kotlin API). 5) **Applications** (systémové + third-party). Každá vrstva poskytuje služby té nad ní.

# Q: Co je Linux Kernel v Androidu?
A: **Základ celé platformy.** Spravuje paměť, procesy, drivery hardwaru. Mírně upravený mainline Linux (přidává **Binder IPC**, wakelocks). Stará se o **bezpečnost na úrovni procesů** — každá aplikace dostane vlastní UID (základ sandboxu).

# Q: Co je ART a co nahradil?
A: **Android Runtime** — VM Androidu. **Nahradil Dalvik VM** v Androidu 5.0 Lollipop (2014). Spouští bytecode aplikací ve formátu **DEX** (Dalvik Executable, .dex).

# Q: Rozdíl ART vs Dalvik?
A: **Dalvik** = JIT (kompilace za běhu, rychlejší instalace, pomalejší start). **ART** = **hybrid AOT + JIT** (pomalejší instalace, rychlejší start). Od Android 7.0 **profile-guided AOT** — sleduje časté části kódu a v klidu (nabíjení) je překompiluje AOT. Best of both.

# Q: 4 základní komponenty Android aplikace?
A: 1) **Activity** (jedna obrazovka s UI). 2) **Service** (background bez UI — hudba, navigace). 3) **Broadcast Receiver** (reaguje na události — wifi, baterie). 4) **Content Provider** (sdílí data s jinými appkami přes URI — Contacts, Media). Všechny **registrované v manifestu**.

# CLOZE: **Service** typy: **{{Foreground}}** (povinná notifikace), **{{Background}}** (omezené od Android 8), **{{Bound}}** (jiná komponenta se připojí přes IPC).

# Q: Co je sandbox v Androidu?
A: **Izolace každé aplikace přes vlastní Linux UID.** Vlastní proces, vlastní paměť, vlastní filesystem (`/data/data/com.app.x/`). App A nemůže číst data App B. **Bezpečnost + crash izolace** + vynucené sdílení přes oficiální mechanismy (Content Provider, Intent, IPC).

# Q: Co je AndroidManifest.xml a co definuje?
A: **Povinný konfigurační soubor.** Bez registrace v manifestu komponenta **neexistuje** — OS ji nemůže spustit. Definuje: **všechny 4 komponenty**, **permissions**, `minSdkVersion`/`targetSdkVersion`, ikonu/název/theme, **intent filters**, `exported` flag (povinné od Android 12).

# Q: Kategorie Android permissions?
A: **Normal** (auto při instalaci — INTERNET, VIBRATE). **Dangerous** (runtime, user schvaluje za běhu — CAMERA, READ_CONTACTS, LOCATION). **Signature** (jen aplikace se stejným podpisem). **Special** (Settings — SYSTEM_ALERT_WINDOW). **Runtime permissions** od Android 6.0 (2015) — dangerous se schvalují **v okamžiku potřeby**, ne při instalaci.

# Q: Co je Intent?
A: **Zpráva**, která spouští komponentu nebo přenáší data. Slovo = "záměr". Dva typy: **explicitní** (znám cíl) × **implicitní** (systém vybere). **Intent Filter** v manifestu deklaruje, jaké intenty komponenta umí zpracovat.

# Q: Explicitní vs implicitní Intent?
A: **Explicitní** = znám konkrétní cílovou komponentu (typicky vlastní Activity): `Intent(this, DetailActivity::class.java)`. **Implicitní** = deklaruji **záměr**, systém najde vhodnou app: `Intent(ACTION_VIEW, Uri.parse("https://..."))`. Pokud více kandidátů → **Intent Chooser** (dialog s výběrem).

# Q: Co je Jetpack Compose?
A: **Moderní deklarativní UI framework** pro Android (stable od 2021). Místo XML píšeš Kotlin funkce s anotací **`@Composable`**. **Reactive recomposition** — když se mění State (`mutableStateOf`), znovu vyrenderuje **jen komponenty čtoucí ten state**. **Paralela:** ekvivalent React komponent (`useState` ≈ `remember { mutableStateOf() }`, lifting state up ≈ state hoisting).

# Q: Compose vs XML Views?
A: **Compose** = deklarativní, pouze Kotlin, reactive recomposition, **málo boilerplate**. **XML Views** (legacy) = imperativní, XML soubory + Kotlin/Java, LiveData manual updates, hodně boilerplate (`findViewById`, adapters).

# Q: Co je MVVM a proč v Androidu?
A: **Model-View-ViewModel** — Google-doporučená architektura. **View** (Activity/Compose) observuje state z **ViewModel**, ViewModel volá **Model** (Repository → API/DB/cache). **Proč ne MVC:** ViewModel **přežije rotaci** (Activity zničena, ViewModel zůstává), oddělení concerns, testovatelnost bez Android frameworku.

# Q: Activity Lifecycle — 6 hlavních callbacks?
A: 1) **`onCreate()`** (vytvoří se → init UI, ViewModel). 2) **`onStart()`** (viditelná). 3) **`onResume()`** (interakce → start kamery/GPS). 4) **`onPause()`** (jiná Activity přijde → uložit stav). 5) **`onStop()`** (zcela skryta → stop těžké op). 6) **`onDestroy()`** (ničí se → uvolnit zdroje).

# Q: Co se stane s Activity při rotaci telefonu?
A: **Úplně se zničí a vytvoří znovu:** `onPause → onStop → onDestroy → onCreate → onStart → onResume`. **ViewModel přežije** tuto destrukci (drží UI state napříč konfiguračními změnami) — proto se používá místo ukládání stavu v Activity.

# FREE: 30s úvod k SWI 25 (mluvíš jako první větu komise).
> "Android je open-source mobilní operační systém spravovaný Googlem, postavený na Linuxovém kernelu. Architektonicky je to **zásobník vrstev** — Linux Kernel, HAL, Android Runtime s nativními knihovnami, Application Framework a samotné aplikace. Každá aplikace běží v **sandboxu** pro izolaci a bezpečnost a skládá se ze **čtyř základních komponent: Activity, Service, Broadcast Receiver a Content Provider**, registrovaných v povinném souboru AndroidManifest.xml."

# FREE: Klasické chytáky komise k SWI 25 — drill body.
> 1) **ART je hybrid AOT+JIT**, Dalvik byl jen JIT. ART nahradil Dalvik v Android 5.0 (2014). 2) **Rotace zničí Activity** (onDestroy → onCreate), proto ViewModel. 3) **Manifest = povinný config**, bez registrace komponenta neexistuje. 4) **Dangerous permissions** (CAMERA, LOCATION, CONTACTS) se schvalují **za běhu**, ne při instalaci (od Android 6.0). 5) **Implicitní Intent** = systém vybere vhodnou app + Intent Chooser. **Explicitní** = znám konkrétní cíl. 6) **Sandbox = vlastní Linux UID per app** + vlastní proces + vlastní filesystem. 7) **Compose paralela s Reactem:** `@Composable` = funkce, `mutableStateOf` ≈ `useState`, state hoisting ≈ lifting state up.
