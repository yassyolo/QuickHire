import { ChangeEvent, useState } from 'react';
import './SearchById.css'; 

interface SearchByIdProps {
    setId: (id: number | undefined) => void;
}

export function SearchById({setId} : SearchByIdProps) {
    const [id, setLocalId] = useState<string | undefined>(undefined);
    const [isValid, setIsValid] = useState<boolean>(true);

    const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value;
        setLocalId(value);

        if(value === '') {
            setIsValid(true);
            setId(undefined);
        }
    }

    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            event.preventDefault();           
            if (/^\d+$/.test(id ?? '')) {
                const numericValue = Number(id);
                setIsValid(true);
                setId(numericValue);
            } else {
                setIsValid(false);
            }
        }
    }

   return(
    <div className="search-by-id">
        <div className="validation-input">
         <input id="id-input" aria-invalid={!isValid} type="text" className={`form-control ${isValid ? '' : 'invalid'}`} value={id === undefined ? '' : id} onChange={handleInputChange} onKeyDown={handleKeyDown} placeholder='Enter ID'/>
         <div className={`invalid-feedback d-block ${isValid ? 'invisible' : ''}`}> Not a valid number.</div>
        </div>
    </div>
   );
};

