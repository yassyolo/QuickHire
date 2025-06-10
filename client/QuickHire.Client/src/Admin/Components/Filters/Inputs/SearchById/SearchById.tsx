import { ChangeEvent, useState } from 'react';
import './SearchById.css';

interface SearchByIdProps {
    setId: (id: number | undefined) => void;
}

export function SearchById({ setId }: SearchByIdProps) {
    const [inputValue, setInputValue] = useState<string>('');
    const [isValid, setIsValid] = useState<boolean>(true);

    const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value;
        setInputValue(value);
        setIsValid(true); // Reset validation feedback

        if (value.trim() === '') {
            setId(undefined);
        }
    };

    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            event.preventDefault();
            const trimmed = inputValue.trim();

            if (trimmed === '') {
                setIsValid(false);
                setId(undefined);
                return;
            }

            if (/^\d+$/.test(trimmed)) {
                setIsValid(true);
                setId(Number(trimmed));
            } else {
                setIsValid(false);
                setId(undefined);
            }
        }
    };

    return (
        <div className="search-by-id">
            <div className="validation-input">
                <input
                    id="id-input"
                    type="text"
                    inputMode="numeric"
                    pattern="\d*"
                    className={`form-control ${isValid ? '' : 'invalid'}`}
                    value={inputValue}
                    onChange={handleInputChange}
                    onKeyDown={handleKeyDown}
                    placeholder="Enter ID"
                    aria-invalid={!isValid}
                />
                <div className={`invalid-feedback d-block ${isValid ? 'invisible' : ''}`}>
                    Please enter a valid numeric ID.
                </div>
            </div>
        </div>
    );
}
