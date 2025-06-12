import { useState } from "react";
import './SearchByKeyword.css';

export interface SearchByKeywordProps{
    setKeyword(keyword: string| undefined): void;
}

export function SearchByKeyword({setKeyword} : SearchByKeywordProps) {
    const [keyword, setLocalKeyword] = useState<string | undefined>(undefined);

    const handleInputChange = (event : React.ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value;
        setLocalKeyword(value);

        if(value.trim() === '') {
            setLocalKeyword(undefined);
        }
    }

    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            event.preventDefault();

            const trimmed = (keyword ?? '').trim();

            if (trimmed === '') {
                setKeyword(undefined);
                setLocalKeyword('');
            } else {
                setKeyword(trimmed);
            }
        }
    };

    return(
        <div className="search-by-keyword">
           <input id="keyword-input" type="text" className="form-control" value={keyword === undefined ? '' : keyword} onChange={handleInputChange} onKeyDown={handleKeyDown} placeholder='Search by keyword' />
        </div>
    )

};

