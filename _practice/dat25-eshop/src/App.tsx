import { CartProvider } from './context/CartContext';
import { ProductList } from './components/ProductList';
import { Cart } from './components/Cart';

function App() {
  return (
    <>
      <div style={{ textAlign: 'center', marginBottom: '30px' }}>
        <h1>E-shop v TypeScriptu: Správa stavu ({'useContext'} + {'useReducer'})</h1>
      </div>

      <div className="app-container">
        <ProductList />
        <Cart />
      </div>
    </>
  );
}

export default App;
