import { SearchById } from "../../../Shared/Forms/SearchInputs/SearchById/SearchById";
import { SearchByKeyword } from "../../../Shared/Forms/SearchInputs/SearchByKeyword/SearchByKeyword";

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

