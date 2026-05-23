import type { Ukol } from "../types";
import TodoForm from "../components/TodoForm";
import TodoList from "../components/TodoList";

type HomeProps = {
    ukoly: Ukol[];
    onPridej: (text: string) => void;
    onSmaz: (id: number) => void;
    onHotovo: (id: number) => void;
};

export default function Home({ ukoly, onPridej, onSmaz, onHotovo }: HomeProps) {
    return (
        <>
            <h2>Můj TodoList</h2>
            <TodoForm onPridej={onPridej} />
            <TodoList ukoly={ukoly} onSmaz={onSmaz} onHotovo={onHotovo} />
        </>
    );
}
