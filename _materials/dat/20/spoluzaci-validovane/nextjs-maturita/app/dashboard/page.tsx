// app/dashboard/page.tsx
// ✅ Server Component – samotná stránka nepotřebuje "use client"
// Interaktivní část (hodiny) je oddělena do Client Componenty

import Clock from "../../components/Clock";

export default function DashboardPage(): React.JSX.Element {
  return (
    <div>
      <h1>Dashboard</h1>
      <p>Tato stránka kombinuje Server Component se Client Componentou.</p>

      {/* Clock je Client Component – může používat useState a useEffect */}
      <Clock />
    </div>
  );
}
