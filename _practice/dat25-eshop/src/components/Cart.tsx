import { useCart } from '../context/CartContext';

export const Cart = () => {
  // Získejte aktuální state a funkci dispatch z hooku useCart
  const cartContext = useCart();

  if (!cartContext) {
    return <p>Načítám...</p>;
  }

  const { state, dispatch } = cartContext;

  return (
    <div className="card">
      <h2>Můj košík ({state.items.length})</h2>
      
      {state.items.length === 0 ? (
        <p>Košík je prázdný.</p>
      ) : (
        <>
          {state.items.map((item, index) => (
            <div key={index} className="cart-item">
              <span>{item.name}</span>
              <span>
                {item.price} Kč
                {/* DOPLŇTE FUNKCI PRO ODEBRÁNÍ POLOŽKY (odeslání akce 'REMOVE_ITEM' podle id) */}
                <button className="remove-btn" style={{marginLeft: '10px'}} onClick={() => {}}>X</button>
              </span>
            </div>
          ))}
          <hr />
          <h3>Celkem: {state.total} Kč</h3>
          
          {/* DOPLŇTE FUNKCI PRO VYSYPÁNÍ KOŠÍKU (odeslání akce 'CLEAR_CART') */}
          <button onClick={() => {}}>
            Vyprázdnit košík
          </button>
        </>
      )}
    </div>
  );
};
