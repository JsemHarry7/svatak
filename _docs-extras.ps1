# Pokračování docs.ps1 — jen git clony, bez yarn/pnpm buildů
Set-StrictMode -Version Latest
$ErrorActionPreference = "Continue"   # nechce failnout u prvního errr

$basePath = "C:\_dev\_svatak"
$gitPath = (Get-Command git -ErrorAction SilentlyContinue).Source
if (-not $gitPath) { $gitPath = "C:\Program Files\Git\cmd\git.exe" }

$clones = @(
    @{ name = "nextjs.docs";       url = "https://github.com/vercel/next.js.git" },
    @{ name = "react.cheatsheet";  url = "https://github.com/typescript-cheatsheets/react.git" },
    @{ name = "typescript.docs";   url = "https://github.com/microsoft/TypeScript-Website.git" },
    @{ name = "vite.docs";         url = "https://github.com/vitejs/vite.git" }
)

foreach ($c in $clones) {
    $target = Join-Path $basePath "_docs\$($c.name)"
    if (Test-Path $target) {
        Write-Host "$($c.name): již existuje, skip"
        continue
    }
    Write-Host "Klonuji $($c.name)..."
    & $gitPath clone $c.url $target --depth=1
}

# Prisma docs s exclude blogu (longPaths)
$prismaPath = Join-Path $basePath "_docs\prisma.docs"
if (-not (Test-Path $prismaPath)) {
    Write-Host "Klonuji prisma.docs..."
    & $gitPath -c core.longPaths=true clone --no-checkout https://github.com/prisma/web.git $prismaPath --depth=1
    Push-Location $prismaPath
    & $gitPath checkout HEAD -- . ':(exclude)apps/blog/public/**'
    Pop-Location
}

Write-Host "Hotovo."
