import { useState } from "react";
import './SearchGig.css';
import { useNavigate } from "react-router-dom";

export interface SearchByKeywordProps{
    setKeyword(keyword: string| undefined): void;
}

export function SearchGig({setKeyword} : SearchByKeywordProps) {
    const [keyword, setLocalKeyword] = useState<string | undefined>(undefined);
    const navigate = useNavigate();

    const handleInputChange = (event : React.ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value;
        setLocalKeyword(value);

        if(value === '') {
            setLocalKeyword(undefined);
        }
    }

     const handleOnSearch = () => {
        const trimmed = keyword?.trim() || '';
        setKeyword(trimmed);
        navigate(`/buyer/search?keyword=${encodeURIComponent(trimmed)}`);
    };

    return(
        <div className="search-gig-by-keyword">
           <input id="gig-keyword-input" type="text" className="form-control" value={keyword === undefined ? '' : keyword} onChange={handleInputChange} placeholder='What service are you looking today?' />
           <button className="search-button-k" onClick={handleOnSearch} aria-label="Search"><i className="fa-solid fa-magnifying-glass"></i></button>
        </div>
    )

};

