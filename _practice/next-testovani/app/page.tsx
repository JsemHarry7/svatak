import Link from "next/link"

export default function Home() {
  return (
    <>
      <h1>Hello Next.js</h1>
      <Link href="/dashboard">Dashboard</Link>
      <Link href="/blog/prvni-clanek">Clanek</Link>
    </>
  );
}
