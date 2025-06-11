import { createContext, useContext, useState } from "react";

interface SearchContextProps {
    keyword: string | undefined;
    setKeyword: (keyword: string | undefined) => void;
}

const SearchContext = createContext<SearchContextProps | undefined>(undefined);

export const useSearch = () => {
    const context = useContext(SearchContext);
    if (!context) {
        throw new Error("useSearch must be used within a SearchProvider");
    }
    return context;
}

interface SearchProviderProps {
    children: React.ReactNode;
}

export function SearchProvider ({ children } : SearchProviderProps) {
    const [keyword, setKeyword] = useState<string | undefined>(undefined);

    return (
        <SearchContext.Provider value={{ keyword, setKeyword }}>
            {children}
        </SearchContext.Provider>
    );
};

