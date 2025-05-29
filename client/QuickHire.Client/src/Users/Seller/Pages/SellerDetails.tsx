//import axios from 'axios';
import {  useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { DetailsComponent } from '../Components/Details/Common/Forms/DetailsContainer';
import { SkillsTag } from '../Components/Details/Common/Tags/SkillsTag';
import './SellerDetails.css';
import { AddSkillsModalForm } from '../Components/Details/Common/Add/AddSkillsModalForm';
import { AddOrEditDetailsModal } from '../Components/Details/Common/EditOrDelete/AddOrEditDetailsModal';
import { DescriptionTag } from '../Components/Details/Common/Tags/DescriptionTag';
import { AddDescriptionModalForm } from '../Components/Details/Common/Add/AddDescriptionModalForm';

interface SellerDetails{
    profilePictureUrl: string;
    fullName: string;
    country: string;
    username: string;
    languages: string[];
    description: string;
    skills: string[];
}

export function SellerDetails() {
    const [sellerDetails, setSellerDetails] = useState<SellerDetails | null>(null);
    const [showAddSkillsModal, setShowAddSkillsModal] = useState<boolean>(false);
    const [showAddDescriptionModal, setShowAddDescriptionModal] = useState<boolean>(false);

    const handleOnAddNewDescription = (newDescription: string) => {
        if (sellerDetails) {
            setSellerDetails({
                ...sellerDetails,
                description: newDescription
            });
        }
        setShowAddDescriptionModal(false);
    };


    /*useEffect(() => {
        const fetchSellerDetails = async () => {
            try {
                const response = await axios.get<SellerDetails>('https://localhost:7267/users/seller/details');
                if (response.status === 200) {
                    setSellerDetails(response.data);
                    
                }
            } catch (error) {
                console.error("Error fetching seller details:", error);
            }
        }
        fetchSellerDetails();
    }, []);*/

       useEffect(() => {
            /*const mockSellerDetails: SellerDetails = {
    profilePictureUrl: "",
    fullName: "John Doe",
    country: "United States",
    username: "johndoe123",
    description: "Experienced web developer with a passion for creating dynamic and user-friendly applications.",
    languages: ["English", "Spanish", "French"],
    skills: ["React", "TypeScript", "Node.js", "CSS"]
};*/
        // Instead of fetching from API, directly set the mock data
        setSellerDetails(null);
    }, []);

    const handleOnAddNewSkills = (newSkills: string[]) => {
        if (sellerDetails) {
            setSellerDetails({
                ...sellerDetails,
                skills: [...(sellerDetails.skills || []), ...newSkills]
            });
        }
        setShowAddSkillsModal(false);
    };
const handleOnSkillsAddModalShow = () => {
    setShowAddSkillsModal(true);
    setShowAddDescriptionModal(false);
};
    return(
        <div className="seller-details-page d-flex flex-column">
            <div className="seller-details-top">
                <div className="seller-info">

                <img src={sellerDetails?.profilePictureUrl} alt="Profile" className="profile-picture" />
                <div className="names-country-languages d-flex flex-column">
                    <div className="username-full-name d-flex flex-row">
                        <Link to="/buyer/settings" className="seller-full-name">{sellerDetails?.fullName}</Link>
                        <div className="seller-username"> @{sellerDetails?.username}</div>
                    </div>

                <div className="country-languages d-flex flex-row">
                        <div className="seller-country"><i className="bi bi-geo-alt"></i>{sellerDetails?.country}</div>
                        {sellerDetails?.languages && sellerDetails?.languages.length > 0 && (
                            sellerDetails.languages.map((language, index) => (
                                <span key={index} className="language-badge">
                                    <i className="bi bi-chat"></i> {language}
                                </span>
                            ))
                        )}
                </div>
                </div>
                </div>

                <div className="gigs-quick-link">
                    <div className="quick-links-title">Quick Links </div>
                    <Link to="/seller/gigs" className="quick-link"><i className="bi bi-archive"></i> Gigs</Link>
                </div>
            </div>
            <DetailsComponent onAddModalShow={() => setShowAddDescriptionModal(true)} addModal={<AddOrEditDetailsModal title={'About me'} onClose={() => setShowAddDescriptionModal(false)} show={showAddDescriptionModal}><AddDescriptionModalForm onSuccess={handleOnAddNewDescription}/></AddOrEditDetailsModal>} title={'About me'}   noContent={"Tell your buyers a bit about yourself. Share your experience, interests, or anything that showcases your professionalism."}
>
                                   
                                    {sellerDetails?.description && <DescriptionTag description={sellerDetails.description}/>}
                </DetailsComponent>

                <DetailsComponent onAddModalShow={handleOnSkillsAddModalShow} addModal={<AddOrEditDetailsModal title={'Skills and expertise'} onClose={() => setShowAddSkillsModal(false)} show={showAddSkillsModal}><AddSkillsModalForm onSuccess={handleOnAddNewSkills}/></AddOrEditDetailsModal>} title={'Skills and expertise'} noContent={"Let your buyers know your skills. Skills gained through your previous jobs, hobbies or even everyday life."}>
                                   
                                     {sellerDetails?.skills && sellerDetails.skills.length > 0 ?
                                        (sellerDetails.skills.map((skill, index) => (
                                            <SkillsTag key={`skill-${index}`} skill={skill}/>
                                        ))) : null}
                </DetailsComponent>

        </div>
    )
}
