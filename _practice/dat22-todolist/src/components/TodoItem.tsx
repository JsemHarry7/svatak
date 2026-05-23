import type { Ukol } from "../types";
import { Routes, Route, Link } from "react-router-dom";

type TodoItemProps = {
    ukol: Ukol;
    onSmaz: (id: number) => void;
    onHotovo: (id: number) => void;
};

export default function TodoItem({ ukol, onSmaz, onHotovo }: TodoItemProps) {
    return (
        <li>
            <input type="checkbox" checked={ukol.hotovo} onChange={() => onHotovo(ukol.id)} />
            <Link to ={`/ukol/${ukol.id}`}><span>{ukol.text}</span></Link>
            <button onClick={() =>  onSmaz(ukol.id)}>Smazat</button>
        </li>
    );
}
