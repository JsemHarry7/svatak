import { prisma } from "../lib/prisma.ts"

await prisma.clanek.deleteMany();
await prisma.autor.deleteMany();

const novyAutor = await prisma.autor.create({
    data: {
        email: "honza.hyxa@email.com",
        jmeno: "Tomáš",
        prijmeni: "Novotný"
    }
})
console.log("byl vytvořen autor jménem:",novyAutor.jmeno,"s id:",novyAutor.id)

const novyClanek = await prisma.clanek.create({
    data: {
        titulek: "prvni clanek",
        autor: {connect: { id: novyAutor.id }},
        obsah: "prd",
    }
})
console.log("byl vytvořen článek jménem:",novyClanek.titulek,"s id:",novyClanek.id)

const clankySeJmenyAutoru = await prisma.clanek.findMany({
    include: { autor: true },
})
console.log(clankySeJmenyAutoru)

const clanekPodleId = await prisma.clanek.findUnique( {where: {id: novyClanek.id}})
console.log(clanekPodleId)

const aktualizovatCLanek = await prisma.clanek.update( {where: {id: novyClanek.id},
    data: {
        obsah:"tohle je nyni seriozni obsah",
        jePublikovany: true
    }
})
console.log(aktualizovatCLanek)
const smazatClanek = await prisma.clanek.delete( {where: {id: novyClanek.id}})
console.log(smazatClanek)

const kontorla = await prisma.clanek.findUnique( {where: {id: novyClanek.id}})
console.log(kontorla)