---
title: SWI 8 — Kryptosystémy
description: 4 vlastnosti (důvěrnost/integrita/autenticita/nepopiratelnost), AES/DES/RSA/ECC, módy bloků, TLS handshake, PKI, certifikáty, útoky, PQC
tags: [maturita, swi, kryptografie, tls, pki, certifikaty]
---

# Q: Co je kryptosystém?
A: **Ucelený soubor algoritmů, klíčů a postupů** pro bezpečnou komunikaci.

# Q: 4 vlastnosti, které moderní kryptosystém zajišťuje?
A: **Důvěrnost** (data nikdo cizí nepřečte — šifrování), **integrita** (data nebyla cestou změněna — hash/MAC/podpis), **autenticita** (známe identitu protistrany — certifikáty/podpis), **nepopiratelnost** (odesílatel nemůže popřít odeslání — digitální podpis).

# CLOZE: Důvěrnost = {{šifrování}}. Integrita = {{hash, MAC, digitální podpis}}. Autenticita = {{certifikáty, digitální podpis}}. Nepopiratelnost = {{digitální podpis}}.

# Q: Co je Kerckhoffsův princip?
A: **Bezpečnost kryptosystému závisí JEN na utajení klíče, ne algoritmu** (1883). Moderní kryptografie tomu věří — AES, RSA jsou veřejné, ale bez klíče k ničemu. Opak ("security through obscurity") je past.

# Q: Co je rozdíl kryptografie × kryptoanalýza × kryptologie?
A: **Kryptografie** = věda o zabezpečení komunikace. **Kryptoanalýza** = věda o prolamování šifer. **Kryptologie** = obojí dohromady.

# Q: Co je AES?
A: **Advanced Encryption Standard** (NIST, 2001) — dnešní standard symetrického šifrování. Klíče **128/192/256 bitů**, bloková šifra (blok 128 bitů). Použití: HTTPS, WPA2/3, BitLocker, FileVault.

# Q: Co byl DES a proč se nepoužívá?
A: **Data Encryption Standard** (1977) — první moderní symetrická šifra. Klíč **56 bitů** = **prolomený brute-force** (1997, 96 dní). Dnes minuty. **Deprecated.**

# Q: Co je 3DES?
A: **DES aplikovaný 3×** za sebou s různými klíči (168 bitů celkem). Vznikl jako přechod mezi DES a AES. **Deprecated 2023.**

# Q: Co je ChaCha20?
A: Moderní symetrická šifra (2008), 256-bit klíč. **Rychlejší v software než AES** (AES potřebuje HW akceleraci). Používá ji TLS.

# Q: 4 módy blokových šifer?
A: **ECB** (Electronic Codebook — špatný), **CBC** (Cipher Block Chaining — OK ale bez autentizace), **CTR** (Counter — paralelizovatelný), **GCM** (Galois/Counter — moderní standard s autentizací).

# MCQ: Proč nepoužívat ECB mód?
- Je pomalý
- !Stejné bloky vstupu se zašifrují stejně → vzory v datech viditelné ("ECB tučňák")
- Není standardizovaný
- Nepodporuje AES
> ECB šifruje každý blok nezávisle. Když má vstup opakující se vzory, výstup je má taky. Slavný příklad: zašifrovaný obrázek tučňáka je stále rozpoznatelný.

# Q: Co je AES-GCM a proč je moderní standard?
A: AES v **Galois/Counter Mode** — kombinuje CTR mód + **autentizaci integrity** v jednom kroku. Tzv. **authenticated encryption** — šifruje + zaručuje, že data nebyla změněna. Dnes default v TLS 1.3.

# Q: Na čem matematicky stojí RSA?
A: **Obtížnost faktorizace velkých čísel.** Vynásobit dvě prvočísla je snadné, **rozložit součin zpět** je extrémně těžké. Klíče typicky 2048-4096 bitů.

# Q: Na čem stojí ECC (Elliptic Curve Cryptography)?
A: **"Logaritmus" na eliptických křivkách** je obtížný. Násobení bodů je snadné, opačný směr ne. Výhoda: stejná bezpečnost při kratších klíčích (256-bit ECC ≈ 3072-bit RSA).

# Q: Co je Forward Secrecy (PFS)?
A: **Perfect Forward Secrecy** — i kdyby útočník v budoucnu získal soukromý klíč serveru, **nemůže dešifrovat staré odposlechnuté komunikace**. Princip: efemérní (jednorázové) DH klíče per spojení.

# CLOZE: TLS 1.3 vyžaduje PFS {{povinně}}. TLS 1.2 ho má {{volitelně}} (přes ECDHE cipher suites).

# Q: Co je digitální certifikát?
A: **Elektronicky podepsaný veřejný klíč** s metadata o vlastníkovi (identita, doba platnosti, použití). Vydává ho **Certifikační autorita (CA)**.

# Q: Co obsahuje certifikát?
A: **Veřejný klíč** vlastníka, **identitu** (jméno/doména), **dobu platnosti** (90 dní u Let's Encrypt), **vydávající CA + její podpis**, **použití** (server auth, code signing, email).

# Q: Co je Chain of trust v PKI?
A: Hierarchie CA: **Root CA** (v OS/prohlížeči preinstalovaný, samopodepsaný) → **Intermediate CA** → **End-entity certifikát** (např. terpino.cz). Prohlížeč ověří identitu projitím řetězce až k důvěryhodné Root CA.

# Q: 4 hlavní CA?
A: **Let's Encrypt** (zdarma, dominantní, 90 dní platnost), **DigiCert** (placená, EV), **Sectigo** (Comodo), **PostSignum** (česká kvalifikovaná).

# Q: Co je self-signed certifikát?
A: Certifikát **podepsaný sám sebou**, neuznaný žádnou CA. Použití: dev na localhostu, interní systémy, IoT. Prohlížeč zobrazí varování ("Nelze ověřit identitu").

# Q: Jaký je rozdíl SSL × TLS?
A: **SSL** = starý, deprecated (verze 1.0/2.0/3.0, poslední z 1996). **TLS** = moderní nástupce (1.0/1.1/1.2/1.3). Reálně se používá výhradně TLS, **"SSL" žije jen v marketingu** ("SSL certifikát" = vlastně TLS cert).

# Q: Aktuální stav TLS verzí?
A: **TLS 1.3** (2018) = moderní standard, doporučený. **TLS 1.2** stále široce používaný. **TLS 1.0/1.1** deprecated od 2020. **SSL 2.0/3.0** deprecated 2011/2015.

# CODE: Cipher suite formát (TLS 1.2)
```
TLS_AES_256_GCM_SHA384
│   │       │   │      │
│   │       │   │      └── Hash pro KDF (SHA-384)
│   │       │   └────────── Mód operace (GCM)
│   │       └────────────── Délka klíče (256 bitů)
│   └────────────────────── Šifra (AES)
└────────────────────────── Protokol
```

# Q: Hlavní kroky TLS 1.2 handshake?
A: 1) **ClientHello** (TLS verze, šifry). 2) **ServerHello** (vybraná cipher suite). 3) **Certifikát** od serveru → klient ověří CA. 4) **Výměna klíčů** (RSA nebo ECDHE) → sdílený symetrický klíč. 5) **Finished** zprávy. 6) **Aplikační data** šifrována AES-GCM.

# Q: Co zjednodušil TLS 1.3 oproti 1.2?
A: 1) Odstranil starší slabé algoritmy (RSA key exchange, CBC mode). 2) **Povinný PFS** přes (EC)DHE. 3) **1-RTT** handshake (key share v ClientHello). 4) **0-RTT resumption** pro známé servery.

# Q: 7 klasických útoků na kryptosystémy?
A: **Brute force**, **dictionary**, **rainbow tables**, **MITM** (Man-in-the-Middle), **replay attack**, **padding oracle**, **side channel**.

# Q: Co je MITM útok a jak se proti němu chránit?
A: **Man-in-the-Middle** — útočník se vloží mezi klienta a server jako proxy, vidí a může měnit komunikaci. **Obrana: certifikáty** (server prokáže identitu přes cert podepsaný CA) + **HSTS** (vynucená HTTPS).

# Q: Co je replay attack a jak se chránit?
A: Útočník **zachytí legitimní zprávu** (např. autorizaci platby) a později ji **znovu pošle**. **Obrana: nonce** (one-time number), **timestamps**, **sekvenční čísla**.

# Q: Co je padding oracle útok?
A: Útok na **špatně implementovaný CBC mód**. Útočník iterativně mění padding a podle odpovědi serveru vyvodí plaintext. **Obrana: autentizované šifrování** (AES-GCM místo CBC).

# Q: Co je side channel útok?
A: Útočník neměří kryptografii samotnou, ale **vedlejší kanály**: spotřebu energie, čas operací, EM záření, zvuk. **Obrana: konstantní časová implementace** (každá operace trvá stejně dlouho bez ohledu na vstup).

# Q: Co je E2EE?
A: **End-to-End Encryption** — data vidí **jen odesílatel a příjemce, ani server uprostřed ne**. Klíče jen u koncových uživatelů. **Signal, WhatsApp, iMessage, ProtonMail**.

# Q: Co je Shorův algoritmus a proč je hrozba?
A: **Kvantový algoritmus** (1994), který na **dostatečně silném kvantovém počítači** prolomí RSA a ECC (faktorizace + diskrétní logaritmus) **ve zlomku sekundy**. Aktuálně kvantové počítače existují, ale **nejsou dostatečně silné** (stovky qubitů, potřebujeme miliony).

# Q: Co je post-quantum kryptografie?
A: Algoritmy **odolné vůči kvantovým útokům**. Založené na jiných matematických problémech (lattice, hash, code-based). **NIST 2024** standardizoval: **CRYSTALS-Kyber** (key encapsulation), **CRYSTALS-Dilithium** (podpisy), **SPHINCS+** (hash-based).

# Q: Které symetrické šifry zůstávají kvantově odolné?
A: **AES-256** a **SHA-256/SHA-3** zůstávají bezpečné. Groverův algoritmus jen půlí efektivní velikost klíče (AES-256 = "AES-128" v kvantové éře, což je pořád bezpečné).

# Q: Co je "harvest now, decrypt later" útok?
A: Státy a velké hráče **sbírají dnes zašifrovaný traffic** a **čekají na kvantové počítače**, pak ho dešifrují. Proto se s PQC pospíchá — dnešní data uložená protivníkem budou v budoucnu prolomená, pokud nepoužíváme PQC.

# FREE: Vysvětli TLS 1.2 handshake krok za krokem.
> 1) **ClientHello:** klient pošle "podporuju TLS 1.2, AES-256-GCM, ECDHE-RSA, ..." + náhodné číslo. 2) **ServerHello:** server vybere cipher suite + pošle své náhodné číslo. 3) **Certificate:** server pošle svůj certifikát s veřejným klíčem (a celý chain of trust). 4) **Klient ověří certifikát** proti důvěryhodným CA v OS/prohlížeči — kontrola podpisu, doby platnosti, doménového jména. 5) **Key Exchange:** přes RSA (klient zašifruje pre-master secret veřejným klíčem serveru) nebo přes ECDHE (Elliptic Curve Diffie-Hellman Ephemeral pro forward secrecy). 6) Oba spočítají **sdílený symetrický klíč** z náhodných čísel + pre-master. 7) **Finished:** oba pošlou zašifrovanou zprávu potvrzující úspěch. 8) **Aplikační data:** od této chvíle všechny HTTP požadavky šifrovány AES-256-GCM sdíleným klíčem.

# FREE: Proč je AES-GCM moderní standard a co poskytuje navíc oproti AES-CBC?
> **AES-CBC** šifruje, ale **negarantuje integritu** — pokud útočník flipne bit ciphertextu, dešifrování vrátí jiný plaintext, ale **server neví, že byla data změněna**. Pro integritu by se musel přidat samostatný MAC (HMAC) — náchylné k chybám (padding oracle útoky). **AES-GCM** kombinuje **šifrování (CTR mód) + autentizaci** v jednom kroku — output má jak ciphertext, tak **autentizační tag** (16 bajtů). Při dešifrování server ověří tag — pokud nesedí, **odmítne dešifrování**. Jednodušší pro programátora, odolnější vůči útokům, paralelizovatelnější. Proto dnes default v TLS 1.3, IPsec, Signal Protocol.
