---
title: SWI 7 — Šifrování a kódování
description: Kódování (ASCII, UTF-8, Base64), klasické šifry, symetrické (AES) × asymetrické (RSA), Diffie-Hellman, hybrid, hashe (SHA-256, salt, bcrypt), digitální podpis
tags: [maturita, swi, kryptografie, sifrovani, kodovani, hash]
---

# Q: Jaký je rozdíl mezi kódováním a šifrováním?
A: **Kódování** = převod dat pro přenos/zobrazení (ASCII, UTF-8, Base64), **veřejné**, kdokoli dekóduje. **Šifrování** = utajení dat pomocí **klíče** (AES, RSA), zpět jen s klíčem.

# CLOZE: Base64 je {{kódování}}, ne šifrování. AES je {{šifrování}}, ne kódování.

# MCQ: Co je ASCII?
- Šifrovací algoritmus
- !7-bitové kódování s 128 znaky, jen anglická abeceda
- Hashovací funkce
- Šifrovací klíč
> ASCII = American Standard Code for Information Interchange. 7 bitů = 128 znaků. Žádné háčky, azbuka, čínština.

# Q: Co je UTF-8 a proč je dnešní standard?
A: **Mezinárodní kódování s proměnnou délkou 1-4 bajty** na znak. ASCII znaky = 1 bajt (zpětně kompatibilní), čeština 2 bajty, asijské 3 bajty, emoji 4 bajty. Pokrývá 140 000+ znaků.

# Q: Co je Base64 a kde se používá?
A: Kódování **binárních dat do textového formátu** přes 64 znaků `[A-Z, a-z, 0-9, +, /]`. Použití: email přílohy (MIME), data URLs, JWT tokeny, embed obrázků v CSS/HTML.

# MCQ: Je Base64 šifrování?
- Ano, šifruje binární data
- !Ne, je to kódování, kdokoli triviálně dekóduje
- Záleží na klíči
- Ano, ale jen pro emaily
> Base64 nemá klíč. Je to převod binárního obsahu na text. Kdokoli to dekóduje.

# Q: Rozdíl mezi transpozicí a substitucí?
A: **Transpozice** = mění **pořadí** písmen (přesmyčka, "AHOJ" → "JOHA"). **Substituce** = **nahrazuje** písmena jinými ("AHOJ" → "DKRM" Caesar +3).

# Q: Co je Caesarova šifra?
A: Klasická **substituce** s **posunem písmen** o pevný počet. Posun 3: A→D, B→E. Slabiny: jen 25 možných klíčů (brute force za sekundy), frekvenční analýza.

# Q: Co je Vigenèrova šifra?
A: **Polyalfabetická substituce** s heslem. Každé písmeno se posune o jiný počet podle aktuálního písmene hesla. Před počítači silná, dnes prolomená Kasiského zkouškou.

# Q: Co byla Enigma a kdo ji prolomil?
A: **Elektromechanický šifrovací stroj** (Německo, WW2). Substituce přes rotory, 158 kvintilionů kombinací. Prolomil **Alan Turing** ve Bletchley Parku. Začátek éry **počítačové kryptoanalýzy**.

# Q: Co je symetrické šifrování?
A: Obě strany sdílí **JEDEN tajný klíč** pro šifrování i dešifrování. **Rychlé** (GB/s), ale **problém s předáním klíče** přes nezabezpečený kanál.

# Q: 4 algoritmy symetrického šifrování?
A: **AES** (standard, 128/192/256 bitů), **DES** (prolomený, 56 bitů), **3DES** (deprecated), **ChaCha20** (moderní alternativa AES).

# CLOZE: AES-256 má 2{{256}} klíčů. Brute-force by trval déle než věk vesmíru.

# Q: Co je asymetrické šifrování?
A: Každý má **dva klíče**: **veřejný** (sdílí, jím se šifruje) + **soukromý** (tajný, jím se dešifruje). Řeší problém předávání klíčů, ale je 1000-10000× pomalejší.

# MCQ: Šifruješ zprávu pro Boba. Jakým klíčem?
- Svým privátním
- Svým veřejným
- !Bobovým veřejným
- Bobovým privátním
> Šifruješ veřejným klíčem příjemce. On dešifruje svým privátním (který jen on má).

# Q: 3 algoritmy asymetrického šifrování?
A: **RSA** (faktorizace prvočísel, 2048-4096 bitů), **ECC** (eliptické křivky, 256-384 bitů = RSA 3072+), **DSA / EdDSA** (pro digitální podpisy).

# Q: Proč je ECC výhodnější než RSA?
A: **Stejná bezpečnost při kratších klíčích.** 256-bit ECC ≈ 3072-bit RSA. Důsledek: rychlejší, menší certifikáty, lépe pro mobil/IoT.

# Q: Co je Diffie-Hellman a co řeší?
A: Matematický protokol pro **bezpečnou výměnu klíče přes nezabezpečený kanál**. Řeší problém symetrického šifrování: jak bezpečně předat klíč přes síť.

# FREE: Vysvětli Diffie-Hellman přes analogii s mícháním barev.
> 1) Alice a Bob veřejně domluví "základní barvu": žlutá. 2) Alice si vybere tajnou červenou, smíchá žlutá+červená = oranžová, pošle Bobovi (veřejně). 3) Bob si vybere tajnou modrou, smíchá žlutá+modrá = zelená, pošle Alici (veřejně). 4) Alice smíchá zelenou (Bobovou) s červenou (svou tajnou) = hnědá. 5) Bob smíchá oranžovou (Aliciinu) s modrou (svou tajnou) = hnědá. → Oba mají hnědou. Hacker vidí jen směsi, neumí oddělit tajné barvy zpět. Matematicky: modulární umocňování + diskrétní logaritmus.

# Q: Co je hybridní šifrování?
A: **Kombinace symetrického a asymetrického.** Asymetrické (DH/RSA) jen pro **výměnu klíče**, pak rychlé symetrické (AES) pro **data**. Spojuje rychlost a bezpečnost.

# Q: Konkrétní příklad hybridního šifrování v praxi?
A: **HTTPS** (TLS handshake). 1) Klient/server si přes DH nebo ECDHE domluví sdílený AES klíč. 2) Klient ověří serverový certifikát přes CA. 3) Od té chvíle všechny HTTP požadavky šifrovány AES-256. 🔒 v prohlížeči.

# Q: Co je digitální podpis?
A: **Obrácený směr asymetrického šifrování.** Šifruješ (podepisuješ) **hash** zprávy **svým PRIVÁTNÍM** klíčem. Ověřujícímu **VEŘEJNÝ** klíč. Garantuje autenticitu (kdo poslal) + integritu (nezměněno).

# CODE: Postup podepsání a ověření
```
PODPIS (Alice):
1. hash = SHA-256(zpráva)
2. podpis = encrypt_RSA(hash, Aliciin_privátní_klíč)
3. pošle: zpráva + podpis

OVĚŘENÍ (Bob):
1. hash_A = decrypt_RSA(podpis, Aliciin_veřejný_klíč)
2. hash_B = SHA-256(přijatá_zpráva)
3. pokud hash_A == hash_B → podpis platí
```

# CLOZE: U šifrování: šifruj {{veřejným}}, dešifruj {{privátním}}. U podpisu: podepiš {{privátním}}, ověř {{veřejným}}.

# Q: Co je hash a jaké jsou jeho vlastnosti?
A: **Jednosměrná funkce** převádějící vstup na **otisk pevné délky**. 5 vlastností: **jednosměrnost** (zpět nelze), **determinismus** (stejný vstup → stejný hash), **avalanche effect** (drobná změna → úplně jiný hash), **fixed-length output**, **collision resistance**.

# Q: 3 současné hashovací algoritmy + 2 prolomené?
A: **Standardní:** SHA-256 (standard), SHA-3 (nová generace), BLAKE2/3 (moderní rychlé). **Prolomené:** MD5 (2004), SHA-1 (2017).

# Q: Co je salt a proč se používá?
A: **Náhodný řetězec přidaný k heslu PŘED hashováním.** Brání **rainbow table** útokům (předem spočítaný hash běžných hesel). Každý uživatel má **jiný salt**, ukládá se v DB **otevřený** (vedle hashe).

# MCQ: Pro hashování hesel je správné použít...
- MD5
- SHA-1
- SHA-256
- !bcrypt, scrypt, Argon2
> MD5/SHA-1 prolomené. SHA-256 je **moc rychlé** (GPU = miliardy hashů/s = brute force). bcrypt/scrypt/Argon2 jsou ZÁMĚRNĚ pomalé (stovky ms) a memory-hard.

# Q: Proč pro hesla NEPOUŽÍVAT SHA-256?
A: SHA-256 je **moc rychlé** — moderní GPU počítá **miliardy hashů za sekundu**. I se saltem může útočník zkusit miliony hesel za vteřinu. Pro hesla potřebujeme **pomalé** funkce: bcrypt (~250 ms), scrypt, Argon2 (~100 ms + GB paměti).

# Q: Co je HMAC?
A: **Hash-based Message Authentication Code** — hash **kombinovaný s tajným klíčem**. Ověřuje **autenticitu** (kdo poslal, jen ten kdo zná klíč) + **integritu** (nezměněno). Použití: **JWT podpisy** (HS256 = HMAC-SHA256), AWS API podpisy, Stripe webhooks.

# Q: Typická použití hash funkcí?
A: 1) **Ukládání hesel** (jen hash, ne plaintext). 2) **Integrita souborů** (checksum staženého .iso). 3) **Digitální podpisy** (podepisuje se hash). 4) **Blockchain** (každý blok obsahuje hash předchozího). 5) **Deduplikace** dat (stejný hash = stejná data).

# FREE: Vysvětli, jak HTTPS používá hybridní šifrování.
> 1) Klient pošle TLS ClientHello (podporuju TLS 1.3, AES-256, RSA). 2) Server odpoví ServerHello + pošle certifikát s veřejným klíčem. 3) Klient ověří certifikát proti důvěryhodným CA v OS/prohlížeči. 4) DH (nebo ECDHE) výměna — klient a server si přes modulární umocňování nezávisle vypočítají stejný sdílený AES klíč, hacker mezi nimi to z odposlechu nesvede. 5) Od té chvíle všechny HTTP požadavky šifrovány AES-256 (rychlé). 🔒 v prohlížeči = úspěšný handshake.

# FREE: Vysvětli postup ukládání hesla v databázi (best practice).
> 1) Při **registraci**: vygeneruj náhodný **salt** (např. 16 bajtů, jiný pro každého uživatele). 2) Spočítej hash = bcrypt(heslo + salt) s cost faktorem 12 (nebo Argon2 s parametry). 3) Ulož do DB: `username`, `salt` (otevřeně), `hash`. 4) Při **přihlášení**: načti uložený salt podle username. 5) Spočítej hash zadaného hesla se stejným saltem. 6) **Porovnej s uloženým hashem** (constant-time comparison kvůli timing attacks). 7) Shoda → přihlásit, jinak odmítnout. NIKDY: SHA-256, MD5, plaintext, bez saltu.
