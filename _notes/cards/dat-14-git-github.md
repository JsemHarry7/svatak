---
title: DAT 14 — Git a GitHub
description: Verzovací systémy, Git příkazy, branches, GitHub, merge conflict, .gitignore
tags: [maturita, dat, prg, git, github, vcs]
---

# Q: Co je Git a kdo ho vytvořil?
A: **Distribuovaný verzovací systém** vytvořený **Linusem Torvaldsem v roce 2005** pro vývoj Linux Kernelu. Slouží ke správě verzí kódu, týmové spolupráci a možnosti vrátit se k předchozím stavům.

# Q: Jaký je rozdíl mezi Gitem a GitHubem?
A: **Git** = nástroj na lokálním PC (verzovací systém). **GitHub** = webová služba pro hosting Git repozitářů + features (PR, Issues, Actions). Git může fungovat bez GitHubu, GitHub bez Gitu nemá smysl. Alternativy: GitLab, Bitbucket.

# CLOZE: 3 vrstvy stavu souboru v Gitu: {{Working directory}} (kde píšeš kód) → {{Staging area}} (`git add`) → {{Repository}} (`git commit`).

# CLOZE: Git je {{distribuovaný}} VCS — každý vývojář má kompletní historii lokálně. SVN je {{centralizovaný}} — jeden server, ostatní jen kopie.

# CLOZE: Default branch v moderním Gitu/GitHubu je {{main}} (od 2020). Starší repa mají {{master}}.

# CLOZE: Skrytá složka, ve které Git drží celou historii repa, se jmenuje `{{.git/}}`.

# MCQ: Co dělá `git add` a co `git commit`?
- !`git add` přesune změny z working directory do staging area. `git commit` vezme staged a vytvoří checkpoint v historii s zprávou.
- Add je commit, commit je push
- Add nahraje na GitHub
- Není rozdíl
> Tři vrstvy: working → staging → repo. Mantra: *"add do stagingu, commit do repa, push na remote"*.

# MCQ: Co dělá `.gitignore` u už trackovaného souboru?
- Smaže ho z repa
- !Nic — `.gitignore` filtruje jen untracked. Trackovaný soubor zůstává trackovaný.
- Vyhodí chybu
- Skryje ho
> Klasická past! `.env` jednou commitnutý zůstává v historii **navždy**. Pro odebrání: `git rm --cached soubor` + commit. Pak ho .gitignore začne filtrovat.

# MCQ: Co je merge conflict?
- !Když dva lidé změnili stejný řádek souboru, Git neví, kterou verzi vybrat. Řeší se manuálně přes markery `<<<<<<<`, `=======`, `>>>>>>>`.
- Když je server offline
- Když chybí push
- Když je velký soubor
> Při manuálním řešení smažeš markery, ponecháš požadovanou verzi (nebo kombinaci), `git add` + commit dokončí merge. Lze zrušit: `git merge --abort`.

# MCQ: Jaký je rozdíl mezi `git fetch` a `git pull`?
- !`git fetch` stáhne jen info ze serveru (bez merge). `git pull` = fetch + automatický merge.
- Není rozdíl
- Fetch je pro mobil
- Pull je rychlejší
> Fetch bezpečnější — můžeš si nejdřív projít, co přibylo, a pak teprve merge. Pull rovnou aplikuje.

# MCQ: Jaký je rozdíl mezi `git revert` a `git reset --hard`?
- !`git revert <hash>` vytvoří NOVÝ commit, který ruší starý — historie zachovaná, bezpečné. `git reset --hard <hash>` přepíše historii, smaže novější commity — destruktivní.
- Není rozdíl
- Revert je pomalejší
- Reset je deprecated
> Reset --hard je destruktivní — použít jen lokálně **před pushem**. Po push použij vždy revert, jinak rozbíjíš historii ostatním.

# FREE: Popiš workflow vytvoření repa a prvního commitu.
> 1) `mkdir projekt && cd projekt` — vytvoř složku. 2) `git init` — inicializuj repo (vznikne `.git/`). 3) `git config --global user.name "..." / user.email "..."` (pokud poprvé). 4) Vytvoř soubory. 5) `git status` — uvidíš untracked. 6) `git add .` (přidá vše respektující .gitignore) nebo `git add <soubor>`. 7) `git commit -m "Initial commit"`. 8) `git log` — uvidíš commit s hashem, autorem, datem.

# FREE: Popiš branching workflow.
> 1) `git switch -c feature-name` — vytvoř a přepni se. 2) Edituj kód. 3) `git add . && git commit -m "..."` — commit na branchi. 4) `git switch main` — zpět na hlavní. 5) `git merge feature-name` — sloučí změny. 6) Při konfliktu: vyřeš manuálně, `git add` + commit. 7) `git branch -d feature-name` — smaže (jen pokud je už mergnutá).

# FREE: Co patří do `.gitignore` a proč?
> Soubory, které nechceš verzovat. **Hesla a secrets**: `.env`, `*.key`, `secrets.json`. **Build artefakty**: `node_modules/`, `bin/`, `obj/`, `dist/`. **IDE konfigurace**: `.vscode/`, `.idea/`. **OS soubory**: `.DS_Store`, `Thumbs.db`. **Logy**: `*.log`. Hesla NIKDY do Gitu — historie je trvalá, i po smazání zůstávají v history.

# FREE: Co je Pull Request a jak funguje?
> Žádost o začlenění tvé branche/forku do hlavního projektu. Mechanika: 1) Vytvoř branch s opravou/featurou. 2) Push branch na GitHub. 3) Otevři PR — popíšeš změny. 4) Ostatní **review**ují kód, komentují, navrhují změny. 5) Schválení/odmítnutí. 6) Merge do main. Základní mechanika open-source vývoje + týmové spolupráce.

# CODE: Init + první commit.
```bash
mkdir muj-projekt
cd muj-projekt
git init                                # vznikne .git/

echo "# Můj Projekt" > README.md
git status                              # uvidíš README.md jako untracked
git add README.md
git commit -m "Initial commit"
git log                                 # uvidíš jeden commit
```

# CODE: Branching workflow.
```bash
git switch -c feature-login             # nová branch + přepni
# ...edituj kód...
git add .
git commit -m "Implementace přihlášení"

git switch main                         # zpět na main
git merge feature-login                 # sloučí změny
git branch -d feature-login             # smaž branch
```

# CODE: Push na GitHub poprvé.
```bash
git remote add origin https://github.com/user/projekt.git
git branch -M main                      # přejmenuj na main (pokud máš master)
git push -u origin main                 # první push s nastavením upstreamu

# Další pushe pak jen
git push
```

# CODE: Vyřešení merge conflictu.
```bash
git merge feature-branch
# Output: CONFLICT (content): Merge conflict in README.md

# Otevři soubor, najdi markery:
# <<<<<<< HEAD
# tvoje verze
# =======
# verze z druhé branche
# >>>>>>> feature-branch

# Smaž markery, ponech požadovanou verzi
git add README.md
git commit -m "Resolve merge conflict"
```

# CODE: .gitignore příklad pro .NET projekt.
```
# secrets
.env
*.key

# build artefakty
bin/
obj/
node_modules/

# IDE
.vscode/
.idea/

# OS
.DS_Store
Thumbs.db
```

# Q: Co je `git remote add origin <url>`?
A: Propojí lokální Git repo se vzdáleným repozitářem na URL. `origin` je konvenční jméno (lze libovolné). Pak `git push` posílá na origin, `git pull` stahuje z origin.

# Q: Jaký je rozdíl mezi fast-forward merge a merge commit?
A: **Fast-forward** — když hlavní branch neměl od feature-branch žádné nové commity, Git **jen posune ukazatel** kupředu. Žádný nový merge commit, lineární historie. **Merge commit** — když oba branche měly nové commity (divergovaly), Git vytvoří **nový commit** se dvěma rodiči. Y-struktura v `git log --graph`.

# Q: Co je `git stash`?
A: Dočasně **schová rozdělané změny**, aby ses mohl přepnout branch bez commitu. `git stash` — uloží. `git stash pop` — obnoví na aktuální branch. Klasické pro "rychlou pauzu" práce.

# Q: Co je rozdíl mezi Working directory, Staging area a Repository?
A: **Working directory** = soubory, které právě editujeme (skutečné soubory na disku). **Staging area (index)** = "přípravná zóna" — soubory připravené pro další commit (`git add` přidá sem). **Repository** = uložená historie s commit hashy. `git commit` vezme ze stagingu do repa.

# Q: Co dělá `git checkout` × `git switch`?
A: `git checkout` je starší, **multifunkční** příkaz — přepíná branch, odhazuje změny v souborech, vytváří branch. `git switch` (od Git 2.23) je **přesněji jen pro přepínání branchí** — méně zmatku. `git switch <name>` = přepnout, `git switch -c <name>` = vytvoř a přepnout.
