import { useState } from "react";
import axios from "../../../axiosInstance";
import "./AddReview.css";
import { FormLabel } from "../../../Shared/Forms/FormLabel/FormLabel";
import { FormGroup } from "../../../Shared/Forms/FormGroup/FormGroup";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { StarRating } from "../Reviews/Star/Star";
import { useNavigate, useParams } from "react-router-dom";
import { isAxiosError } from "axios";
import { useAuth } from "../../../AuthContext";



export function AddReview() {
    const {user} = useAuth();
    const navigate = useNavigate();
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
            await axios.post(url, {
                OrderId: Number(id),
                Rating: rating,
                Comment: comment
            })
                if(user?.mode === "buyer"){
                    navigate(`/buyers/orders/${id}`);
                }
                else if(user?.mode === "seller"){
                    navigate(`/sellers/orders/${id}`);
                }
          
        } catch (error : unknown) {
            if (isAxiosError(error) && error.response && error.response.status === 400) {
                setValidationErrors({
                    Comment: error.response.data.errors?.Comment || []
                });
            }
        }
    };



    return(
        <div className="add-rating-form" style={{ width: "100%" }}>
            <FormLabel id={"add-rating"} label={"Rate your experience"} tooltipDescription={"Select a star rating to reflect your overall experience — 1 being poor and 5 being excellent."} onShowTooltip={handleShowRatingTooltip} showTooltip={showRatingTooltip} ariaDescribedBy={"rating-help"} />           
            <StarRating rating={rating} onRatingChange={handleRatingChange} />           
            <FormGroup id={"rating-comment"} label={"Your comment is valuable"} tooltipDescription={"Write a thoughtful review that explains what you liked or didn’t like. Be specific and helpful for others."} type={"textarea"} value={comment} onChange={handleCommentChange} placeholder={"Enter Comment"} ariaDescribedBy={"comment-help"} onShowTooltip={handleShowCommentTooltip} showTooltip={showCommentTooltip} error={validationErrors.Comment || []} />  
            <div className="add-rating-button">
              <ActionButton text="Continue" onClick={handleAddReview} className="continue-button" ariaLabel="Continue Button"/>
            </div>                 
        </div>
    );
}