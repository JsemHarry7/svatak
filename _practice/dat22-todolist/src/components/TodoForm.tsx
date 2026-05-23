import { useEffect, useRef, useState, useId } from "react";

type TodoFormProps = {
  onPridej: (text: string) => void;
};

export default function TodoForm({ onPridej }: TodoFormProps) {
  const [text, setText] = useState("");
  const inputRef = useRef<HTMLInputElement>(null);
  const inputId: string = useId();

  useEffect(() => {
    if (inputRef.current) {
      inputRef.current.focus();
    }
  }, []);

  const submit = () => {
    const trimmed = text.trim();
    if (trimmed === "") return;
    onPridej(trimmed);
    setText("");
  };

  return (
    <form>
      <label htmlFor={inputId}>Nový úkol: </label>
      <input
        type="text"
        ref={inputRef}
        value={text}
        id={inputId}
        onChange={(e) => setText(e.target.value)}
        placeholder="Nový úkol..."
      />
      <button type="button" onClick={submit}>
        Přidat
      </button>
    </form>
  );
}
