import { useCart, Product } from '../context/CartContext';

const DUMMY_PRODUCTS: Product[] = [
  { id: 1, name: 'Klávesnice', price: 900 },
  { id: 2, name: 'Myš', price: 500 },
  { id: 3, name: 'Monitor', price: 3500 },
];

export const ProductList = () => {
  // Získejte funkci dispatch z našeho hooku useCart
  const cartContext = useCart();
  
  const handleAddToCart = (product: Product) => {
    // DOPLŇTE ODESLÁNÍ AKCE
    if (cartContext) {
      // zavolejte dispatch
    }
  };

  return (
    <div className="card">
      <h2>Nabídka produktů</h2>
      {DUMMY_PRODUCTS.map(product => (
        <div key={product.id} className="product">
          <div>
            <strong>{product.name}</strong> - {product.price} Kč
          </div>
          <button onClick={() => handleAddToCart(product)}>
            Přidat do košíku
          </button>
        </div>
      ))}
    </div>
  );
};
