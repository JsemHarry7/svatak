// app/blog/[slug]/page.tsx
// ✅ Server Component s dynamickým parametrem
// [slug] v názvu složky = dynamická část URL

// Typ pro props dynamické stránky
interface BlogPostProps {
  params: Promise<{ slug: string }>;
}

export default async function BlogPost({ params }: BlogPostProps): Promise<React.JSX.Element> {
  // V Next.js 15+ je params Promise, proto musíme použít await
  const { slug } = await params;

  return (
    <div>
      <h1>Článek: {slug}</h1>
      <p>
        Tato stránka je Server Component. Parametr <code>slug</code> pochází
        přímo z URL adresy a je dostupný přes prop <code>params</code>.
      </p>
      <p>
        Zkus změnit URL na <code>/blog/cokoliv</code> a sleduj, co se stane.
      </p>
    </div>
  );
}
