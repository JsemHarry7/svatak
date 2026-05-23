export type Ukol = {
  id: number;
  text: string;
  hotovo: boolean;
};

export type UkolAction =
  | { type: "ADD"; text: string }
  | { type: "DELETE"; id: number }
  | { type: "TOGGLE"; id: number }
  | { type: "INIT"; ukoly: Ukol[] };