import { useEffect, useState } from "react";
import { IconButton } from "../../Shared/Buttons/IconButton/IconButton";
import { FormGroup } from "../../Shared/Forms/FormGroup/FormGroup";
import "./SendCustomOfferModal.css";
import axios from "../../axiosInstance";
import { isAxiosError } from "axios";
import { ModalActions } from "../../Shared/Modals/Common/ModalActions";
import { ActionButton } from "../../Shared/Buttons/ActionButton/ActionButton";
import { CustomOfferPayloadModel, useAuth } from "../../AuthContext";

interface ChooseFromGigs
 {
    id: number;
    title: string;
    imageUrl: string;
}

interface Inclusives {
    id: number;
    name: string;
    value: string;
}

interface CustomOfferReturnModel {
    text: string;
    conversationId: number;
    payload: CustomOfferPayloadModel;
}

export function SendCustomOfferModal({
    id,
    onClose,
    onSendCustomOfferSuccess
}: {
    id: number;
    onClose: () => void;
    onSendCustomOfferSuccess: (id: number) => void;
}) {
    const auth = useAuth();
    const [showGigsChoice, setShowGigsChoice] = useState<boolean>(true);
    const [chosenGig, setChosenGig] = useState<ChooseFromGigs | null>(null);
    const[populatedGigs, setPopulatedGigs] = useState<ChooseFromGigs[]>([]);
    const [chosenInclusives, setChosenInclusives] = useState<Inclusives[]>([]);
    const [populatedInclusives, setPopulatedInclusives] = useState<Inclusives[]>([]);

    const [description, setDescription] = useState<string>("");
    const [deliveryTime, setDeliveryTime] = useState<number>(1);
    const [revisions, setRevisions] = useState<number>(1);
    const [total, setTotal] = useState<number>(0);

    const [validationErrors, setValidationErrors] = useState<Record<string, string[]>>({});
    const [showDescriptionTooltip, setShowDescriptionTooltip] = useState<boolean>(false);

    useEffect(() => {
        const fetchChooseFromGigs = async () => {
            try {
                const response = await axios.get<ChooseFromGigs[]>(`https://localhost:7267/seller/choose-from-gigs`);
                if (response.status !== 200) {
                    throw new Error(`Failed to fetch gigs, status code: ${response.status}`);
                }
                setPopulatedGigs(response.data);
            } catch (error) {
                console.error("Error fetching choose from gigs:", error);
            }
        };
        fetchChooseFromGigs();

        const fetchInclusives = async () => {
            try {
                const response = await axios.get<Inclusives[]>(`https://localhost:7267/seller/choose-from-inclusives`);
                if (response.status !== 200) {
                    throw new Error(`Failed to fetch inclusives, status code: ${response.status}`);
                }
                setPopulatedInclusives(response.data);
            } catch (error) {
                console.error("Error fetching inclusives:", error);
            }
        };
        fetchInclusives();
    }, [id]);

    const handleGigChoice = (gig: ChooseFromGigs) => {
        setChosenGig(gig);
        setShowGigsChoice(false);
    };

    const handleInclusiveChange = (
  e: React.ChangeEvent<HTMLInputElement>,
  id: number,
  name: string
) => {
  const { checked } = e.target;
  setChosenInclusives((prev) => {
    if (checked) {
      return [...prev, { id, name, value: name }];
    } else {
      return prev.filter((inclusive) => inclusive.id !== id);
    }
  });
};

    const handleSendOffer = async () => {
    if (!chosenGig) return;

    const payload = {
    projectBriefId: id,
    gigId: chosenGig.id,
    description,
    deliveryTime,
    total,
    inclusivesIds: chosenInclusives.map((x) => x.id),
};

console.log(payload);

try {
    const response = await axios.post(
        "https://localhost:7267/seller/custom-offer",
        payload,
        {
            headers: {
                "Content-Type": "application/json", 
            },
        }
    );
        
        const offer = response.data as CustomOfferReturnModel;
        onClose();

        if (auth.signalRConnection) {
             await auth.signalRConnection.invoke(
                    "SendMessage",
                    offer.text,                            
                    offer.conversationId,                  
                    null,                                  
                    JSON.stringify(offer.payload),         
                    1, 
                    null                       
                );
        }

        onSendCustomOfferSuccess(id);

    } catch (error) {
        if (isAxiosError(error) && error.response?.status === 400) {
            console.error("Validation Errors:", error.response.data.errors);
            setValidationErrors(error.response.data.errors || {});
        } else {
            console.error("Error sending custom offer:", error);
        }
    }
};


    return (
        <div className="custom-offer-modal-overlay">
            <div className="custom-offer-modal d-flex flex-row justify-content-between">
                {showGigsChoice ? (
                    <div className="choose-from-gigs">
                        <div className="custom-offer-header">Choose from Gigs</div>
                        <ul className="choose-gigs-list">
                            {populatedGigs.map((gig) => (
                                <li key={gig.id} className="choose-gig-item" onClick={() => handleGigChoice(gig)}>
                                    <img src={gig.imageUrl} alt={gig.title} className="choose-gig-image" />
                                    <span className="choose-gig-title">{gig.title}</span>
                                </li>
                            ))}
                        </ul>
                        
                    </div>
                ) : (
                    <div className="custom-offer-form">
                        <div className="custom-offer-header">Create a custom single-payment offer</div>

                        {chosenGig && (
                            <div className="choose-gig-item">
                                <img src={chosenGig.imageUrl} alt={chosenGig.title} className="new-chose-gig-image" />
                                <span className="choose-gig-title">{chosenGig.title}</span>
                            </div>
                        )}
                        <div className="description-offer">
                            
                        <FormGroup
                            id={"description"}
                            label={"Description"}
                            tooltipDescription={"Write a short, catchy description that grabs attention."}
                            type={"text"}
                            value={description}
                            onChange={(e) => setDescription(e.target.value)}
                            placeholder={"Enter Description"}
                            ariaDescribedBy={"description-help"}
                            onShowTooltip={() => setShowDescriptionTooltip(!showDescriptionTooltip)}
                            showTooltip={showDescriptionTooltip}
                            error={validationErrors.Description || []}
                        />
                        </div>

                        <div className="d-flex flex-row gap-3">
                            <FormGroup
                                id={"delivery-time"}
                                label={"Delivery Time (days)"}
                                tooltipDescription={"How many days to deliver the offer?"}
                                type={"number"}
                                value={deliveryTime}
                                onChange={(e) => setDeliveryTime(parseInt(e.target.value))}
                                placeholder={"e.g. 3"}
                                ariaDescribedBy={"delivery-help"}
                                error={validationErrors.DeliveryTime || []}
                            />
                            <FormGroup
                                id={"revisions"}
                                label={"Revisions"}
                                tooltipDescription={"How many revisions do you allow?"}
                                type={"number"}
                                value={revisions}
                                onChange={(e) => setRevisions(parseInt(e.target.value))}
                                placeholder={"e.g. 2"}
                                ariaDescribedBy={"revision-help"}
                                error={validationErrors.Revisions || []}
                            />
                            <FormGroup
                                id={"total"}
                                label={"Total Price ($)"}
                                tooltipDescription={"Enter the total amount you will charge."}
                                type={"number"}
                                value={total}
                                onChange={(e) => setTotal(parseFloat(e.target.value))}
                                placeholder={"e.g. 150"}
                                ariaDescribedBy={"total-help"}
                                error={validationErrors.Total || []}
                            />
                        </div>

                        <div className="custom-offer-inclusives">
                            <div className="custom-offer-inclusives-header">Inclusives</div>
                            <ul className="inclusives-list">
                                {populatedInclusives.map((inclusive) => (
                                    <li key={inclusive.id} className="inclusive-item">
                                        <input
                                            type="checkbox"
                                            id={`inclusive-${inclusive.id}`}
                                            value={inclusive.value}
                                            onChange={(e) => handleInclusiveChange(e, inclusive.id, inclusive.name)}
                                        />
                                        <label htmlFor={`inclusive-${inclusive.id}`}>{inclusive.name}</label>
                                    </li>
                                ))}
                            </ul>
                        </div>
<ModalActions id={"deactivate-main-category-actions"}>
                <ActionButton text={"Back"} onClick={() => setShowGigsChoice(true)} className={"back-button"} ariaLabel={"Back Button"} />
                <ActionButton text={"Send offer"} onClick={handleSendOffer} className={"send-offer-button"} ariaLabel={"Continue Button"} />
            </ModalActions> 

                    </div>
                )}
                                <IconButton icon={<i className="bi bi-x"></i>} onClick={onClose} className={"close-button"} ariaLabel={"Close gig preview"} />

            </div>
        </div>
    );
}
