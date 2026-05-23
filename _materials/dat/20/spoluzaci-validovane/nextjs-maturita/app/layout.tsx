// app/layout.tsx
// Toto je Root Layout – obaluje všechny stránky aplikace
// Je to Server Component (žádné "use client")

import type { Metadata } from "next";
import Navbar from "../components/Navbar";

export const metadata: Metadata = {
  title: "Next.js Maturita Demo",
};

export default function RootLayout({ children }: { children: React.ReactNode }): React.JSX.Element {
  return (
    <html lang="cs">
      <body>
        <Navbar />
        <main style={{ padding: "2rem" }}>{children}</main>
      </body>
    </html>
  );
}
