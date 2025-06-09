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

        if(value === '') {
            setLocalKeyword(undefined);
        }
    }

    const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            event.preventDefault();
            setKeyword(keyword?.trim() || '');
        }
    }

    return(
        <div className="search-by-keyword">
           <input id="keyword-input" type="text" className="form-control" value={keyword === undefined ? '' : keyword} onChange={handleInputChange} onKeyDown={handleKeyPress} placeholder='Search by keyword' />
        </div>
    )

};

