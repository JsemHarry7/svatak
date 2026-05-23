Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$basePath = Get-Location
$transcriptPath = Join-Path $basePath "docs-log.txt"
Start-Transcript -Path $transcriptPath -Append
$nodeToolsPath = Join-Path ${env:ProgramFiles} "nodejs"
$nodeCandidates = @(
    (Join-Path $nodeToolsPath "node.exe")
)
$corepackCandidates = @(
    (Join-Path $nodeToolsPath "corepack.cmd")
)
$gitCandidates = @(
    (Join-Path ${env:ProgramFiles} "Git\cmd\git.exe")
)

$gitPath = (Get-Command git -ErrorAction SilentlyContinue | Select-Object -First 1).Source
if (-not $gitPath) {
    $gitPath = Join-Path ${env:ProgramFiles} "Git\cmd\git.exe"
}
if (-not (Test-Path $gitPath)) {
    throw "Nástroj git není dostupný. Nejprve spusťte _scripts/install.ps1 nebo nainstalujte Git."
}

# === PDF dokumentace ===

$docsToDownload = @(
    @{ name = "asp.net"; folder = "_docs/asp.net"; file = "aspnetcore-docs.pdf"; url = "https://learn.microsoft.com/pdf?url=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Ftoc.json%3Fview%3Daspnetcore-10.0" },
    @{ name = "csharp"; folder = "_docs/csharp"; file = "csharp-docs.pdf"; url = "https://learn.microsoft.com/pdf?url=https%3A%2F%2Flearn.microsoft.com%2Fen-us%2Fdotnet%2Fcsharp%2Ftoc.json" }
)

foreach ($doc in $docsToDownload) {
    $targetFolder = Join-Path $basePath $doc.folder
    if (-not (Test-Path $targetFolder)) {
        New-Item -ItemType Directory -Path $targetFolder -Force | Out-Null
    }

    $targetFile = Join-Path $targetFolder $doc.file
    if (-not (Test-Path $targetFile)) {
        Write-Host "Stahuji dokumentaci $($doc.name)..."
        Invoke-WebRequest -Uri $doc.url -OutFile $targetFile -UseBasicParsing
        Write-Host "Dokumentace $($doc.name) uložena do: $targetFile"
    } else {
        Write-Host "Dokumentace $($doc.name) již existuje. Přeskakuji stahování."
    }
}

# === Klonování repozitářů dokumentace ===

$reactPath = Join-Path $basePath "_docs/react.dev"
if (-not (Test-Path $reactPath)) {
    Write-Host "Klonuji repozitář react.dev..."
    & $gitPath clone https://github.com/reactjs/react.dev.git $reactPath --depth=1
} else {
    Write-Host "Repozitář react.dev už existuje. Přeskakuji klonování."
}
Push-Location $reactPath
try {
    Write-Host "react.dev: yarn install"
    & yarn install
} finally {
    Pop-Location
}

$prismaPath = Join-Path $basePath "_docs/prisma.docs"
if (-not (Test-Path $prismaPath)) {
    Write-Host "Klonuji repozitář prisma.docs..."
    & $gitPath -c core.longPaths=true clone --no-checkout https://github.com/prisma/web.git $prismaPath --depth=1
} else {
    Write-Host "Repozitář prisma.docs už existuje. Přeskakuji klonování."
}
Push-Location $prismaPath
try {
    Write-Host "prisma.docs: checkout working tree"
    & $gitPath checkout HEAD -- . ':(exclude)apps/blog/public/**'
    Write-Host "prisma.docs: pnpm install"
    & corepack pnpm install
} finally {
    Pop-Location
}

$nextPath = Join-Path $basePath "_docs/nextjs.docs"
if (-not (Test-Path $nextPath)) {
    Write-Host "Klonuji repozitář nextjs.docs..."
    & $gitPath clone https://github.com/vercel/next.js.git $nextPath --depth=1
} else {
    Write-Host "Repozitář nextjs.docs už existuje. Přeskakuji klonování."
}

$vitePath = Join-Path $basePath "_docs/vite.docs"
if (-not (Test-Path $vitePath)) {
    Write-Host "Klonuji repozitář vite.docs..."
    & $gitPath clone https://github.com/vitejs/vite.git $vitePath --depth=1
} else {
    Write-Host "Repozitář vite.docs už existuje. Přeskakuji klonování."
}
Push-Location $vitePath
try {
    Write-Host "vite.docs: pnpm install"
    & corepack pnpm install

    $viteNodeVersionText = & node --version
    if ($LASTEXITCODE -eq 0 -and $viteNodeVersionText) {
        $viteNodeVersion = [Version]$viteNodeVersionText.TrimStart('v')
        if ($viteNodeVersion -lt [Version]"20.19") {
            Write-Warning "Build vite.docs vyžaduje Node.js 20.19+. Aktualizujte Node.js přes _scripts/install.ps1."
        } else {
            Write-Host "vite.docs: pnpm docs-build"
            & corepack pnpm docs-build
        }
    } else {
        Write-Warning "Nepodařilo se zjistit verzi Node.js pro vite.docs."
    }
} finally {
    Pop-Location
}

$reactCheatsheetPath = Join-Path $basePath "_docs/react.cheatsheet"
if (-not (Test-Path $reactCheatsheetPath)) {
    Write-Host "Klonuji repozitář react.cheatsheet..."
    & $gitPath clone https://github.com/typescript-cheatsheets/react.git $reactCheatsheetPath --depth=1
} else {
    Write-Host "Repozitář react.cheatsheet už existuje. Přeskakuji klonování."
}

$typescriptPath = Join-Path $basePath "_docs/typescript.docs"
if (-not (Test-Path $typescriptPath)) {
    Write-Host "Klonuji repozitář typescript.docs..."
    & $gitPath clone https://github.com/microsoft/TypeScript-Website.git $typescriptPath --depth=1
} else {
    Write-Host "Repozitář typescript.docs už existuje. Přeskakuji klonování."
}

Write-Host "`nOffline dokumentaci Vite najdete po sestavení ve složce _docs/vite.docs/docs/.vitepress/dist."
Write-Host "Bootstrap a MDN dokumentaci najdete v Zeal aplikaci (docset Bootstrap_5 a HTML/CSS)."

# === Zeal docsets ===
# Zeal ukládá docsets do %LOCALAPPDATA%\Zeal\Zeal\docsets\.
# Docsets jsou staženy z Kapeli CDN (stejný zdroj jako Dash na macOS).
# Pokrývají technologie použité v otázkách 01-25 (HTML/CSS, JS/TS, React, Bootstrap, Node.js, SQLite).
$zealDocsetPath = Join-Path ${env:LOCALAPPDATA} "Zeal\Zeal\docsets"

if (-not (Test-Path $zealDocsetPath)) {
    Write-Warning "Složka Zeal docsets ($zealDocsetPath) neexistuje. Je Zeal nainstalovaný? Spusťte nejprve _scripts/install.ps1."
} else {
    $zealDocsets = @(
        @{ DisplayName = "HTML";       Slug = "HTML";         DocsetFolder = "HTML.docset" },
        @{ DisplayName = "CSS";        Slug = "CSS";          DocsetFolder = "CSS.docset" },
        @{ DisplayName = "JavaScript"; Slug = "JavaScript";   DocsetFolder = "JavaScript.docset" },
        @{ DisplayName = "TypeScript"; Slug = "TypeScript";   DocsetFolder = "TypeScript.docset" },
        @{ DisplayName = "React";      Slug = "React";        DocsetFolder = "React.docset" },
        @{ DisplayName = "Bootstrap 5"; Slug = "Bootstrap_5"; DocsetFolder = "Bootstrap_5.docset" },
        @{ DisplayName = "Node.js";    Slug = "NodeJS";       DocsetFolder = "NodeJS.docset" },
        @{ DisplayName = "SQLite";     Slug = "SQLite";       DocsetFolder = "SQLite.docset" }
    )

    foreach ($ds in $zealDocsets) {
        $targetDocset = Join-Path $zealDocsetPath $ds.DocsetFolder
        if (Test-Path $targetDocset) {
            Write-Host "Zeal docset $($ds.DisplayName) již existuje. Přeskakuji."
            continue
        }

        Write-Host "Stahuji Zeal docset: $($ds.DisplayName)..."
        $tgzFile = Join-Path ${env:TEMP} "$($ds.Slug).tgz"
        try {
            Invoke-WebRequest -Uri "https://kapeli.com/feeds/$($ds.Slug).tgz" -OutFile $tgzFile -UseBasicParsing
            # tar.exe je součástí Windows 10+ (bsdtar)
            & tar.exe -xzf $tgzFile -C $zealDocsetPath
            Write-Host "Docset $($ds.DisplayName) nainstalován."
        } finally {
            if (Test-Path $tgzFile) { Remove-Item $tgzFile -Force }
        }
    }
    Write-Host "Zeal docsets jsou k dispozici v Zeal aplikaci."
}

Write-Host "`nDokumentace byla úspěšně stažena a připravena k použití."
Stop-Transcript