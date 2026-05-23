import { Link } from "react-router-dom";
import type { Ukol } from "../types";

type UkolProps = {
  ukoly: Ukol[];
};

export default function Statistiky({ ukoly }: UkolProps) {
    const hotove = ukoly.filter((e) => e.hotovo).length;
    const neHotove = ukoly.filter((e) => !e.hotovo).length
    const celkemUkolu = ukoly.length
    let procentoHotovych;
    if (ukoly.length === 0) {
      procentoHotovych = "Přidej úkol pro statistiku!";
    } else {
       procentoHotovych = `${Math.round((ukoly.filter((e) => e.hotovo).length / ukoly.length)*100)}%`;
    }
  return (
    <>
      <p>Počet hotových úkolů: {hotove}</p>
      <p>Počet nehotových úkolů: {neHotove}</p>
      <p>Celkový počet úkolů: {celkemUkolu}</p>
      <p>Procento hotových: {procentoHotovych}</p>
      <Link to="/">Zpět domů</Link>
    </>
  );
}
