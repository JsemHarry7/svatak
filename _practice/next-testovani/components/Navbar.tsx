import Link from "next/link";
export default function Navbar() {
    return (
        <>
        <nav>
            <Link href="/">Domů</Link>
            <Link href="/dashboard">Dashboard</Link>
            <Link href="/blog/maturita">maturita</Link>
        </nav>
        </>
    )
}