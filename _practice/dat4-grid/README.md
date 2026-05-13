# Praktická maturitní otázka: CSS Grid a tvorba layoutu

## Instrukce

K dispozici máte soubor `index.html` s připravenou strukturou a základními třídami. HTML kód nijak neupravujte. Veškerou práci provádějte v souboru `styles.css` ve vyznačených sekcích. Pro tvorbu layoutu musíte použít technologii CSS Grid.

## Požadavky na výsledný layout

Celý obsah stránky (header, panely, obsah, footer) musí být zabalen v jednom hlavním grid kontejneru, který zabírá minimálně 100% výšky okna prohlížeče. Mezi všemi jednotlivými bloky layoutu musí být mezera (mezera v mřížce) 20px.

### Desktopová verze (výchozí stav)

Vytvořte klasické rozložení, které se skládá z headeru, třísloupcového středu a footeru:

* **Header** Táhne se přes celou šířku stránky úplně nahoře. Její výška se přizpůsobí obsahu.
* **Levý postranní panel (Navigace):** Nachází se pod hlavičkou vlevo a má pevnou šířku 250px.
* **Hlavní obsah:** Nachází se uprostřed mezi levým a pravým panelem. Musí být flexibilní a zabírat veškeré zbývající volné místo na šířku i na výšku.
* **Pravý postranní panel (Reklama):** Nachází se pod hlavičkou vpravo a má pevnou šířku 200px.
* **Footer** Táhne se přes celou šířku stránky úplně dole, pod panely a hlavním obsahem. Její výška se přizpůsobí obsahu.

<img width="1919" height="906" alt="Image" src="https://github.com/user-attachments/assets/32cfbe85-f298-4425-a375-c3f6636b0be1" />

### Mobilní verze

Rozložení se musí inteligentně přeskládat na zařízeních s šířkou obrazovky 768px a menší:

* Celý layout se změní na jeden jediný sloupec, který zabírá 100 % dostupné šířky.
* Jednotlivé bloky se poskládají pod sebe v tomto pořadí:
    1. Hlavička
    2. Levý postranní panel (Navigace)
    3. Hlavní obsah
    4. Pravý postranní panel (Reklama)
    5. Patička
 
<img width="608" height="906" alt="Image" src="https://github.com/user-attachments/assets/5ccff47b-e36a-4886-a87f-aac7fdb018b7" />
