import { useParams, useNavigate, Link } from "react-router-dom";
import type { Ukol } from "../types";

type UkolDetailProps = {
    ukoly: Ukol[];
    onSmaz: (id: number) => void;
    onHotovo: (id: number) => void;
};

export default function UkolDetail({ ukoly, onSmaz, onHotovo }: UkolDetailProps) {
    const { id } = useParams();
    const nav = useNavigate();

    const ukol = ukoly.find((u) => u.id === Number(id));

    if (!ukol) {
        return (
            <>
                <h2>Úkol nenalezen</h2>
                <p>Úkol s ID {id} neexistuje.</p>
                <Link to="/">Zpět na seznam</Link>
            </>
        );
    }

    const smazatAVratit = () => {
        onSmaz(ukol.id);
        nav("/");
    };

    return (
        <>
            <h2>Detail úkolu #{ukol.id}</h2>
            <p><strong>Text:</strong> {ukol.text}</p>
            <p><strong>Stav:</strong> {ukol.hotovo ? "✅ hotovo" : "⏳ čeká"}</p>

            <button onClick={() => onHotovo(ukol.id)}>
                {ukol.hotovo ? "Označit jako nehotové" : "Označit jako hotové"}
            </button>
            <button onClick={smazatAVratit} style={{ marginLeft: "0.5rem" }}>
                Smazat a vrátit
            </button>
            <button onClick={() => nav(-1)} style={{ marginLeft: "0.5rem" }}>
                Zpět
            </button>
        </>
    );
}
