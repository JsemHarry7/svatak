import { createContext, useContext } from "react";
import type { Ukol, UkolAction } from "./types";

type UkolyContextType = {
    ukoly: Ukol[];
    dispatch: React.Dispatch<UkolAction>;
};

export const UkolyContext = createContext<UkolyContextType | null>(null);

export function useUkoly() {
    const ctx = useContext(UkolyContext);
    if (!ctx) {
        throw new Error("useUkoly must be inside <UkolyContext.Provider>");
    }
    return ctx;
}
