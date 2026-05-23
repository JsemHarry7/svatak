// app/blog/[slug]/page.js
// ✅ Server Component s dynamickým parametrem
// [slug] v názvu složky = dynamická část URL

export default function BlogPost({ params }) {
  // params.slug obsahuje hodnotu z URL, např. pro /blog/maturita → "maturita"
  return (
    <div>
      <h1>Článek: {params.slug}</h1>
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
