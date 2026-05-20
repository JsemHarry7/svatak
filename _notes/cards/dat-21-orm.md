---
title: DAT 21 — ORM
description: ORM základ, Prisma 7 setup, schéma, migrace, Prisma Client operace, EF Core, code-first × DB-first
tags: [maturita, dat, orm, prisma, ef-core]
---

# Q: Co je ORM?
A: **Object-Relational Mapper** — vrstva mezi aplikačním kódem a relační DB. Místo SQL pracuješ s **objekty a metodami**, ORM je přeloží na SQL za tebe.

# Q: 4 výhody ORM oproti raw SQL?
A: **1) Typová bezpečnost** (chyby při kompilaci), **2) Přenositelnost mezi DB** (ORM abstrahuje dialekty), **3) Automatické migrace**, **4) Refactoring** (IDE najde všechny použití).

# Q: Code-first × DB-first?
A: **Code-first** = schéma v kódu, migrace generuje DB. Pravda v kódu. **DB-first** = DB existuje, ORM z ní generuje modely. Pravda v DB. **Code-first běžnější.**

# Q: 4 hlavní ORM nástroje + jejich jazyk?
A: **Prisma** (TypeScript/JS), **Drizzle** (TypeScript), **Entity Framework Core** (C#), **Hibernate** (Java), **SQLAlchemy** (Python).

# CLOZE: V Next.js typicky {{Prisma}}, v ASP.NET Core typicky {{Entity Framework Core}}.

# Q: Co je migrace?
A: **Verzování změn DB schématu**, jako git verzuje kód. ORM porovná aktuální schéma s předchozím, vygeneruje SQL (CREATE TABLE, ALTER TABLE), aplikuje na DB.

# Q: 5 klíčových změn v Prismě 7 oproti 6?
A: **1) `prisma.config.ts`** (URL přesunuto sem). **2) Driver adapter** povinný. **3) Generator `prisma-client`** (ne -js), output mimo node_modules. **4) ESM only** (`"type": "module"`). **5) WebAssembly engine** místo Rust.

# Q: Co je driver adapter v Prismě 7?
A: **Most mezi Prisma Client a JS DB driverem.** Prisma 7 odstranila Rust engine, klient běží čistě v JS/WASM. Adapter zajišťuje připojení k DB. Příklady: `@prisma/adapter-better-sqlite3`, `@prisma/adapter-pg`.

# Q: Proč jsou v Prismě 7 nutné `prisma.config.ts`?
A: Connection URL se přesunula ze `schema.prisma` do `prisma.config.ts`. CLI (`migrate dev`, `studio`) čte URL odtud. Plus umožňuje různé URL pro migrace vs runtime.

# CODE: prisma.config.ts (Prisma 7)
```typescript
import "dotenv/config";
import { defineConfig } from "prisma/config";

export default defineConfig({
  schema: "prisma/schema.prisma",
  migrations: { path: "prisma/migrations" },
  datasource: {
    url: process.env["DATABASE_URL"],
  },
});
```

# CODE: schema.prisma (Prisma 7)
```
generator client {
    provider = "prisma-client"        // ne prisma-client-js!
    output   = "../app/generated/prisma"
}

datasource db {
    provider = "sqlite"               // url NENÍ tady
}

model Post {
    id        Int      @id @default(autoincrement())
    title     String
    published Boolean  @default(false)
    author    Author   @relation(fields: [authorId], references: [id])
    authorId  Int
}
```

# Q: 6 nejčastějších atributů sloupce v Prismě?
A: **`@id`** (PK), **`@default(autoincrement())`** (auto-increment), **`@default(now())`** (aktuální čas), **`@unique`** (UNIQUE), **`@relation(fields, references)`** (FK), **`@updatedAt`** (auto-update na změnu).

# Q: 4 hlavní migrace příkazy?
A: **`prisma migrate dev --name init`** (vytvoří + aplikuje, dev), **`prisma migrate deploy`** (produkce, bez promptu), **`prisma migrate reset`** (smaž vše a reaplikuj), **`prisma db push`** (rychlý prototyping bez migrace).

# Q: Co dělá `migrate dev`?
A: 1) Načte URL z `prisma.config.ts`. 2) Porovná aktuální schéma s předchozím. 3) Vygeneruje SQL do `prisma/migrations/`. 4) Aplikuje na DB. 5) Regeneruje Prisma Client do `app/generated/prisma/`.

# CODE: Prisma Client - sdílená instance
```typescript
// lib/prisma.ts
import { PrismaBetterSqlite3 } from "@prisma/adapter-better-sqlite3";
import { PrismaClient } from "@/app/generated/prisma/client";

const globalForPrisma = globalThis as unknown as { prisma?: PrismaClient };

function createPrismaClient() {
  const adapter = new PrismaBetterSqlite3({ url: process.env.DATABASE_URL! });
  return new PrismaClient({ adapter });
}

export const prisma = globalForPrisma.prisma ?? createPrismaClient();
if (process.env.NODE_ENV !== "production") globalForPrisma.prisma = prisma;
```

# Q: Proč sdílená instance Prisma Client v dev?
A: Next.js hot reload reloaduje moduly při změně. Bez sdílené instance by `new PrismaClient()` byl volaný při každém reloadu, vyčerpal by connection pool. Pattern `globalThis.prisma ??` zaručí jednu instanci.

# Q: Rozdíl `findMany` × `findUnique`?
A: **`findMany`** = pole záznamů (může být prázdné). **`findUnique`** = jeden záznam nebo null. Vyžaduje unique sloupec (PK/UNIQUE). Vždy null check.

# CODE: findMany s filtry, JOIN, řazením
```typescript
const posts = await prisma.post.findMany({
  where: {
    published: true,
    author: { name: "axo" },                  // přes relaci
    title: { contains: "Next" },              // SQL LIKE
  },
  include: { author: true },                  // JOIN
  orderBy: { createdAt: "desc" },
  take: 10,
  skip: 20,
});
```

# Q: Rozdíl `include` × `select`?
A: **`include`** = přidej relaci k defaultu (všechny scalar fields + tahle relace). **`select`** = určuje **co vrátit** (limit fieldů, jen vyjmenované).

# Q: Vytvoření záznamu s propojením přes existující relaci?
A: `prisma.post.create({ data: { title: "X", author: { connect: { id: 1 } } } })`. **`connect`** propojí na **existující** záznam.

# Q: Co je `upsert`?
A: **Update nebo Create** — pokud záznam s `where` existuje, update; jinak create. `prisma.post.upsert({ where, update, create })`.

# Q: Jak vypadá 1:N v Prisma schématu?
A: 
```
model Author { posts Post[] }
model Post {
    author   Author @relation(fields: [authorId], references: [id])
    authorId Int
}
```

# Q: Jak vypadá 1:1 v Prisma schématu?
A: **FK + `@unique`** (UNIQUE dělá z 1:N → 1:1):
```
model Profile {
    user   User @relation(fields: [userId], references: [id])
    userId Int  @unique
}
```

# Q: Jak vypadá N:M v Prismě?
A: **Implicit** — Prisma sám vytvoří vazební tabulku:
```
model Post { tags Tag[] }
model Tag  { posts Post[] }
```

# Q: Co je Prisma Studio?
A: **GUI prohlížeč DB** v prohlížeči. `npx prisma studio` na `localhost:5555`. Procházení, editace, mazání záznamů **bez SQL**. Skvělé pro debug.

# Q: 4 ekvivalent v EF Core × Prisma?
A: **DbContext** ↔ **Prisma Client**. **Entity třídy** ↔ **`model` v schema.prisma**. **LINQ `.Where()`** ↔ **Prisma `where: {}`**. **`Include()`** ↔ **`include: {}`**.

# CODE: EF Core LINQ dotaz
```csharp
var posts = await db.Posts
    .Where(p => p.Published)
    .Include(p => p.Author)
    .OrderByDescending(p => p.CreatedAt)
    .ToListAsync();
```

# Q: Migrace v EF Core?
A: **`dotnet ef migrations add InitialCreate`** (vytvoří migraci), **`dotnet ef database update`** (aplikuje). Visual Studio CLI: `Add-Migration`, `Update-Database`.

# Q: Co je N+1 problém v ORM?
A: **Antipattern**: místo 1 dotazu s JOIN provedeš 1 + N dotazů. Příklad: 100 postů + pro každý zvlášť autora = 101 dotazů. **Řešení:** `include` (Prisma) / `Include()` (EF Core) → 1 dotaz s JOIN.

# Q: Co je `$queryRaw` v Prismě?
A: **Escape hatch pro raw SQL.** Pro 10 % komplexních dotazů, kde ORM API není dostatečné (komplexní agregace, raportní dotazy, DB-specific funkce). Příklad: `await prisma.$queryRaw\`SELECT ... FROM ...\``.

# MCQ: V Prismě 7 importuješ PrismaClient odkud?
- `'@prisma/client'`
- `'./node_modules/@prisma/client'`
- !`'../generated/prisma/client'` (nebo kde máš output)
- `'prisma'`
> Prisma 7 generuje Client **mimo node_modules**, defaultně do `output` cesty (typicky `app/generated/prisma/`). Import z původní cesty `@prisma/client` selže.

# FREE: Popis kompletní flow setupu Prisma 7 aplikace od npm install do prvního dotazu.
> 1) `npm install prisma --save-dev`, `npm install @prisma/client`, `npm install @prisma/adapter-better-sqlite3 better-sqlite3 dotenv`. 2) V `package.json` přidat `"type": "module"`. 3) `npx prisma init` — vytvoří `prisma/schema.prisma` + `.env`. 4) Vytvořit `prisma.config.ts` v rootu s `defineConfig({ schema, migrations, datasource: { url: process.env.DATABASE_URL } })`. 5) Upravit `.env` s `DATABASE_URL="file:./dev.db"`. 6) Definovat modely v `schema.prisma` s `generator client { provider = "prisma-client", output = "../app/generated/prisma" }` a `datasource db { provider = "sqlite" }`. 7) `npx prisma migrate dev --name init` — vytvoří DB + tabulky + generated client. 8) Vytvořit `lib/prisma.ts` s adapterem a sdílenou instancí. 9) V kódu `import { prisma } from "@/lib/prisma"` a `await prisma.post.findMany()`.

# FREE: Vysvětli rozdíl Prisma vs EF Core pro programátora, který zná obojí.
> **Prisma** je deklarativní s vlastním DSL (`schema.prisma`), generuje typed klient. Query syntax je metoda-chaining s plně typovanými objekty (`prisma.post.findMany({ where: {...}, include: {...} })`). Filozofie "zjednoduš ORM". Lepší pro rychlý prototyping v TS/JS. **EF Core** používá C# třídy + DataAnnotations nebo Fluent API pro definici, query přes **LINQ** (declarative C# syntax). Filozofie "plný ORM se vším". Lepší pro .NET enterprise s komplexními entity modely. **Společné:** code-first migrace, eager loading přes include/Include, sdílené koncepty (PK, FK, vazby). **Rozdíl:** Prisma je explicitní (vždy psát `include`), EF Core má **lazy loading** (default načte relaci až při přístupu). Pro maturitu praxe: Prisma s Next.js.
