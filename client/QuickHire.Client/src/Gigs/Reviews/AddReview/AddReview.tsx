import { useState } from "react";
import axios from "axios";
import "./AddReview.css";
import { FormLabel } from "../../../Shared/Forms/FormLabel/FormLabel";
import { FormGroup } from "../../../Shared/Forms/FormGroup/FormGroup";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { StarRating } from "../../../Shared/Star/Star";
import { useParams } from "react-router-dom";



export function AddReview() {
    const {id} = useParams<{ id: string }>();
    const [rating, setRating] = useState(0);
    const [comment, setComment] = useState("");
    const [showCommentTooltip, setShowCommentTooltip] = useState(false);
    const [validationErrors, setValidationErrors] = useState<{ Comment?: string[] }>({});
    const [showRatingTooltip, setShowRatingTooltip] = useState(false);

    const handleRatingChange = (newRating: number) => setRating(newRating);

    const handleCommentChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setComment(event.target.value);

    const handleShowCommentTooltip = () => {
        setShowCommentTooltip(true);
        setTimeout(() => { setShowCommentTooltip(false);}, 2000);
    };

    const handleShowRatingTooltip = () => {
        setShowRatingTooltip(true);
        setTimeout(() => setShowRatingTooltip(false), 2000);
    };

    const handleAddReview = async () => {
        setValidationErrors({});

        try {
            const url = "https://localhost:7267/orders/reviews";
            const response = await axios.post(url, {
                OrderId: Number(id),
                Rating: rating,
                Comment: comment
            })
            if (response.status === 200) {
                //ToDo
            }
        } catch (error : unknown) {
            if (axios.isAxiosError(error) && error.response && error.response.status === 400) {
                setValidationErrors({
                    Comment: error.response.data.errors?.Comment || []
                });
            }
        }
    };



    return(
        <div className="add-rating-form">
            <div className="add-rating-form-title">Tell us about your experience</div>
            <FormLabel id={"add-rating"} label={"Rate your experience"} tooltipDescription={"Select a star rating to reflect your overall experience — 1 being poor and 5 being excellent."} onShowTooltip={handleShowRatingTooltip} showTooltip={showRatingTooltip} ariaDescribedBy={"rating-help"} />           
            <StarRating rating={rating} onRatingChange={handleRatingChange} />           
            <FormGroup id={"rating-comment"} label={"Your comment is valuable"} tooltipDescription={"Write a thoughtful review that explains what you liked or didn’t like. Be specific and helpful for others."} type={"textarea"} value={comment} onChange={handleCommentChange} placeholder={"Enter Comment"} ariaDescribedBy={"comment-help"} onShowTooltip={handleShowCommentTooltip} showTooltip={showCommentTooltip} error={validationErrors.Comment || []} />  
            <div className="add-rating-button">
              <ActionButton text="Continue" onClick={handleAddReview} className="continue-button" ariaLabel="Continue Button"/>
            </div>                 
        </div>
    );
}