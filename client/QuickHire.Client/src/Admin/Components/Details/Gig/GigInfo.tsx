import { TagList } from "../../../../Gigs/Tags/TagList";
import "./GigInfo.css";
import { FAQList } from "../../../../Shared/FAQ/FAQList/FAQList";
import { ComparePackagesTable } from "../../../../Gigs/PaymentPlan/PaymentPlan";

export interface GigDetailsProps {
    id: number;
}

export function GigDetails({ id }: GigDetailsProps) {

    return (
        <div className="gig-details-container" aria-label="gig-details">
            <div className="button-details d-flex d-row justify-content-between">
                <div className="gig-details d-flex flex-column">
                    <ComparePackagesTable plans={[]}/>
                    
                    <TagList gigId={id} />
                    <div className="faqs-gig-details">
                      <FAQList gigId={id} showActions={false} title={"Gig"}/> 
                    </div>
                </div>
            </div>     
            
        </div>
    );
}