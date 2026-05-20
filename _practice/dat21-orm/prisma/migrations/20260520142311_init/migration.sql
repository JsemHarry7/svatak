/*
  Warnings:

  - A unique constraint covering the columns `[email]` on the table `Autor` will be added. If there are existing duplicate values, this will fail.

*/
-- CreateIndex
CREATE UNIQUE INDEX "Autor_email_key" ON "Autor"("email");
