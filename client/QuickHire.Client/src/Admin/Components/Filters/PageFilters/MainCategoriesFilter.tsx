import { SearchById } from "../Inputs/SearchById/SearchById";
import { SearchByKeyword } from "../Inputs/SearchByKeyword/SearchByKeyword";

interface CategoriesFilterProps {
    setId(id: number | undefined): void;
    setKeyword(keyword: string): void;
}

export function MainCategoriesFilter({setId, setKeyword} : CategoriesFilterProps) {
    return(
        <div aria-label="Main-categories-filter" className="page-filter-section">
            <SearchById setId={setId} />
            <SearchByKeyword setKeyword={setKeyword}/>
        </div>
    )
}

