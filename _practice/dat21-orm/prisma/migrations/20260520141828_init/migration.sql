-- CreateTable
CREATE TABLE "Clanek" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "autorId" INTEGER NOT NULL,
    "titulek" TEXT NOT NULL,
    "obsah" TEXT NOT NULL,
    "vznikl" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "jePublikovany" BOOLEAN NOT NULL DEFAULT false,
    CONSTRAINT "Clanek_autorId_fkey" FOREIGN KEY ("autorId") REFERENCES "Autor" ("id") ON DELETE RESTRICT ON UPDATE CASCADE
);

-- CreateTable
CREATE TABLE "Autor" (
    "jmeno" TEXT NOT NULL,
    "prijmeni" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
);
