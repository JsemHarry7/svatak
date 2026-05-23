import { useState, useEffect, useReducer } from "react";
import { Routes, Route, Link } from "react-router-dom";
import type { Ukol, UkolAction } from "./types";
import Home from "./pages/Home";
import About from "./pages/About";
import NotFound from "./pages/NotFound";
import UkolDetail from "./pages/UkolDetail";
import Statistiky from "./pages/Statistiky";

const STORAGE_KEY: string = "todolist-data";

function ukolyReducer(state: Ukol[], action: UkolAction): Ukol[] {
  switch (action.type) {
    case "ADD":
      return [...state, { id: Date.now(), text: action.text, hotovo: false }];
    case "TOGGLE":
      return state.map((x) =>
        x.id === action.id ? { ...x, hotovo: !x.hotovo } : x,
      );
    case "DELETE":
      return state.filter((x) => x.id !== action.id);
    case "INIT":
      return action.ukoly;
    default:
      return state;
  }
}

export default function App() {
  const [ukoly, dispatch] = useReducer(ukolyReducer, []);
  const [loaded, setLoaded] = useState<boolean>(false);

  const pridatUkol = (text: string) => {
    dispatch({ type: "ADD", text });
  };

  const smazatUkol = (id: number) => {
    dispatch({ type: "DELETE", id });
  };

  const prepnoutHotovo = (id: number) => {
    dispatch({ type: "TOGGLE", id });
  };

  useEffect(() => {
    const saved: string | null = localStorage.getItem(STORAGE_KEY);
    if (saved) {
      try {
        const parsed: Ukol[] = JSON.parse(saved);
        dispatch({ type: "INIT", ukoly: parsed });
      } catch {
        // nic
      }
    }
    setLoaded(true);
  }, []);

  useEffect(() => {
    if (!loaded) return;
    localStorage.setItem(STORAGE_KEY, JSON.stringify(ukoly));
  }, [ukoly, loaded]);

  useEffect(() => {
    document.title = `Můj TodoList: ${ukoly.filter((u) => u.hotovo).length}/${ukoly.length}`;
  }, [ukoly]);

  return (
    <div>
      <nav
        style={{
          borderBottom: "1px solid #ccc",
          padding: "1rem",
          marginBottom: "1rem",
        }}
      >
        <Link to="/" style={{ marginRight: "1rem" }}>
          Domů
        </Link>
        <Link to="/about" style={{ marginRight: "1rem" }}>
          O aplikaci
        </Link>
        <Link to="/statistiky">Statistiky</Link>
      </nav>

      <Routes>
        <Route
          path="/"
          element={
            <Home
              ukoly={ukoly}
              onPridej={pridatUkol}
              onSmaz={smazatUkol}
              onHotovo={prepnoutHotovo}
            />
          }
        />
        <Route path="/about" element={<About />} />
        <Route
          path="/ukol/:id"
          element={
            <UkolDetail
              ukoly={ukoly}
              onSmaz={smazatUkol}
              onHotovo={prepnoutHotovo}
            />
          }
        />
        <Route path="/statistiky" element={<Statistiky ukoly={ukoly} />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </div>
  );
}
