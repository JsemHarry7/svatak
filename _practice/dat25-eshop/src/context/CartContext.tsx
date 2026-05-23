import { ReactNode } from "react";

export type Product = {
  id: number;
  name: string;
  price: number;
};

export type CartState = {
  items: Product[];
  total: number;
};

export type CartAction =
  | { type: "ADD_ITEM"; payload: Product }
  | { type: "REMOVE_ITEM"; payload: number } // payload je index prvku v poli
  | { type: "CLEAR_CART" };

export type CartContextType = {
  state: CartState;
  dispatch: React.Dispatch<CartAction>;
} | null;

// 1. ZDE VYTVOŘTE CONTEXT S PŘÍSLUŠNÝM TYPEM
export const CartContext = createContext<CartContextType>(null);

// 2. ZDE VYTVOŘTE REDUCER (použijte připravené typy CartState a CartAction)
const cartReducer = (state: CartState, action: CartAction): CartState => {
  switch (action.type) {
    case
    default:
      return state;
  }
};

const initialState: CartState = {
  items: [],
  total: 0,
};

type CartProviderProps = {
  children: ReactNode;
};

// 3. VYTVOŘTE PROVIDER KOMPONENTU
export const CartProvider = ({ children }: CartProviderProps) => {
  // Použijte useReducer s vaším cartReducer a initialState

  return (
    // Vraťte state a dispatch pomocí CartContext.Provider
    <>{children}</> /* TOTO ZMĚŇTE */
  );
};

// Pomocný hook, který můžete použít v komponentách
export const useCart = () => {
  // použití useContext a navrácení hodnoty contextu
  return {
    state: { items: [], total: 0 },
    dispatch: () => {},
  } as unknown as CartContextType; // TOTO ZMĚŇTE
};
