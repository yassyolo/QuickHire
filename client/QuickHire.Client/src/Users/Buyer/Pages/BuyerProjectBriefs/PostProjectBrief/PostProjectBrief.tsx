import { useState } from "react";
import './PostProjectBrief.css'
import { SellerPage } from "../../../../Seller/Pages/Common/SellerPage";
import { PageTitle } from "../../../../../Shared/PageItems/PageTitle/PageTitle";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { PostProjectBriefForm } from "./Form/PostProjectBriefForm";

export function PostProjectBrief() {
    const [showProjectBriefForm, setShowProjectBriefForm] = useState(false);

    const handleProjectBriefVisibility = () => {
        setShowProjectBriefForm(!showProjectBriefForm);
    }
    return (
        (
            !showProjectBriefForm ?
                (
                    <SellerPage>
                        <PageTitle title="Post project brief" description="Describe your project requirements and connect with qualified professionals to get the job done." breadcrumbs={[{ label: <i className="bi bi-house-door"></i>, to: "/buyer/profile" },
                            { label: "Post a project brief" }]} />
                        <div className="brief-welcome-container">
                            <h2 className="brief-welcome-title">Welcome! Ready to bring your idea to life?</h2>
                            <p className="brief-welcome-description">
                                Let us know what you're working on. The more details you provide, the easier it is to connect you with the right experts.
                                Whether it’s a one-off task or a complex, long-term project — this is where your vision starts to take shape.
                            </p>
                            <ul className="brief-welcome-list">
                                <li className="brief-welcome-list-item"><strong><i className="bi bi-check-all"></i>Define your goals</strong> — What are you trying to achieve?</li>
                                <li className="brief-welcome-list-item"><strong><i className="bi bi-check-all"></i>Set clear expectations</strong> — Share your must-haves and nice-to-haves</li>
                                <li className="brief-welcome-list-item"><strong><i className="bi bi-check-all"></i>Outline your budget & timeline</strong> — Be realistic and transparent</li>
                            </ul>

                            <ActionButton text={
                                <>
                                    Continue <i className="bi bi-arrow-right"></i>
                                </>
                            } onClick={handleProjectBriefVisibility} className="post-project-brief-button" ariaLabel="Post Project Brief Button" />
                        </div>
                    </SellerPage>
                )
                : ( <PostProjectBriefForm onBack={handleProjectBriefVisibility} />)
        )
    );
}