// components/Clock.tsx
// ❗ "use client" je NUTNÉ – komponenta používá useState a useEffect
// Tyto hooky fungují jen v prohlížeči (Client Component)

"use client";

import { useState, useEffect } from "react";

export default function Clock(): React.JSX.Element {
  const [time, setTime] = useState<string | null>(null);

  useEffect(() => {
    // Nastavíme čas hned po načtení komponenty
    setTime(new Date().toLocaleTimeString("cs-CZ"));

    // Každou sekundu aktualizujeme čas
    const interval: ReturnType<typeof setInterval> = setInterval(() => {
      setTime(new Date().toLocaleTimeString("cs-CZ"));
    }, 1000);

    // Cleanup – zruší interval při odebrání komponenty z DOMu
    return () => clearInterval(interval);
  }, []); // [] = spustí se jen jednou po načtení

  return (
    <div style={{ border: "2px solid #333", padding: "1rem", display: "inline-block", borderRadius: "8px" }}>
      <p>🕐 Aktuální čas:</p>
      <strong style={{ fontSize: "2rem" }}>{time ?? "Načítám..."}</strong>
      <p style={{ fontSize: "0.8rem", color: "#666" }}>
        Toto je Client Component – aktualizuje se každou sekundu v prohlížeči.
      </p>
    </div>
  );
}
