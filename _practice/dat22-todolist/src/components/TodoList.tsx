import type { Ukol } from "../types";
import TodoItem from "./TodoItem";

type TodoListProps = {
    ukoly: Ukol[];
    onSmaz: (id: number) => void;
    onHotovo: (id: number) => void;
};

export default function TodoList({ ukoly, onSmaz, onHotovo }: TodoListProps) {
    return (
        <ul>
            {ukoly.map(u => (
                <TodoItem key={u.id} ukol={u} onSmaz={onSmaz} onHotovo={onHotovo} />
            ))}
        </ul>
    );
}
