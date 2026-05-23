import { Link } from "react-router-dom";

export default function NotFound() {
    return (
        <>
            <h2>404 — Stránka nenalezena</h2>
            <p>Tato URL neexistuje.</p>
            <Link to="/">Zpět na hlavní stránku</Link>
        </>
    );
}
