import { useState } from "react";

export function useTooltip() : [boolean, () => void] {
    const [show, setShow] = useState(false);
    const trigger = () => {
        setShow(true);
        setTimeout(() => {setShow(false);}, 2000);
    }

    return [show, trigger];
}