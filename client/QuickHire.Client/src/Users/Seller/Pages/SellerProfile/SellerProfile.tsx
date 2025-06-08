import axios from "../../../../axiosInstance";
import {  useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { DetailsComponent } from '../../Components/Details/Common/Forms/DetailsContainer';
import { SkillsTag } from './Tags/Skills/SkillsTag';
import './SellerProfile.css';
import { AddOrEditDetailsModal } from '../../Components/Details/Common/EditOrDelete/AddOrEditDetailsModal';
import { DescriptionTag } from './Tags/Description/DescriptionTag';
import { EditDescriptionModalForm } from '../../Components/Details/Common/Edit/EditDescriptionModalForm';
import { CertificationTag } from './Tags/Certification/CertificationTag';
import { EducationTag } from './Tags/Education/EducationTag';
import { Certification, EditCertificationModalForm } from '../../Components/Details/Common/Edit/EditCErtificationModalForm';
import { EditEducationModalForm, Education } from '../../Components/Details/Common/Edit/EditEducationModalForm';
import { EditSkillsModalForm } from '../../Components/Details/Common/Edit/EditSkillModalForm';
import { ActionButton } from '../../../../Shared/Buttons/ActionButton/ActionButton';
import { PortfolioTag } from './Tags/Portfolio/PortfolioTag';
import { EditProjectPortfolioModalForm } from '../../Components/Details/Common/Edit/EditPortfolioModalForm';
import { SellerInfoCard } from './SellerProfileCard/SellerInfoCard';
import { SellerPage } from '../Common/SellerPage';

interface SellerDetails{
    profilePictureUrl: string;
    fullName: string;
    country: string;
    username: string;
    languages: UserLanguage[];
    description: string;
    skills: Skill[];
    certifications: Certification[];
    education: Education[];
    portfolios: Portfolio[];
}

export interface UserLanguage{
    languageId: number;
    languageName: string;
}

export interface Skill {
    id: number;
    name: string;
}

export interface Portfolio{
    id: number;
    title: string;
    description: string;
    imageUrl: string;
    mainCategoryId: number;
mainCategoryName: string;}

export function SellerProfile() {
    const [sellerDetails, setSellerDetails] = useState<SellerDetails | null>(null);
    const [showAddSkillsModal, setShowAddSkillsModal] = useState<boolean>(false);
    const [showAddDescriptionModal, setShowAddDescriptionModal] = useState<boolean>(false);
    const [showAddEducationModal, setShowAddEducationModal] = useState<boolean>(false);
    const [showAddCertificationModal, setShowAddCertificationModal] = useState<boolean>(false);
    const [showEditDescriptionModal, setShowEditDescriptionModal] = useState<boolean>(false);
    const [showEditSkillsModal, setShowEditSkillsModal] = useState<boolean>(false);
    const [showEditCertificationModal, setShowEditCertificationModal] = useState<boolean>(false);
    const [showEditLanguagesModal, setShowEditLanguagesModal] = useState<boolean>(false);
    const [showEditPortfolioModal, setShowEditPortfolioModal] = useState<boolean>(false);
    const [showAddPortfolioModal, setShowAddPortfolioModal] = useState<boolean>(false);

    const handleShowEditPortfolioModalVisibility = () => {
        setShowEditPortfolioModal(!showEditPortfolioModal);
    }
    const handleAddPortfolioModalVisibility = () => {
        setShowAddPortfolioModal(!showAddPortfolioModal);
    }

    const handleOnEditLanguagesButtonClick = () => {
        setShowEditLanguagesModal(!showEditLanguagesModal);
    }

    useEffect(() => {
        const fetchSellerDetails = async () => {
            try {
                const response = await axios.get<SellerDetails>('https://localhost:7267/seller/profile');
                if (response.status === 200) {
                    setSellerDetails(response.data);
                    
                }
            } catch (error) {
                console.error("Error fetching seller details:", error);
            }
        }
        fetchSellerDetails();
    }, []);


const handleOnSkillsAddModalShow = () => {
    setShowAddSkillsModal(true);
};

    const handleOnEditDescriptionSuccess = (newDescription: string) => {
        if (sellerDetails) {
            setSellerDetails({
                ...sellerDetails,
                description: newDescription
            });
        }
        setShowEditDescriptionModal(false);
    };

const onEditSkillsSuccess = (skills: Skill[]) => {
    if (sellerDetails) {
        setSellerDetails({
            ...sellerDetails,
            skills: skills
        });
    }
    setShowEditSkillsModal(false);
};

const handleOnEditCertification = (certifications: Certification[]) => {
    if (sellerDetails) {
        setSellerDetails({
            ...sellerDetails,
            certifications: certifications
        });
    }
    setShowEditDescriptionModal(false);
}

const handleOnEditLanguagesSuccess = (languages: UserLanguage[]) => {
    if (sellerDetails) {
        setSellerDetails({
            ...sellerDetails,
            languages: languages
        });
    }
    setShowEditLanguagesModal(false);
}

const handleOnEditEducation = (education: Education[]) => {
    if (sellerDetails) {
        setSellerDetails({
            ...sellerDetails,
            education: education
        });
    }
    setShowEditDescriptionModal(false);
}

const handleOnEditPortfolioSucess = (portfolios: Portfolio[]) => {
    if (sellerDetails) {
        setSellerDetails({
            ...sellerDetails,
            portfolios: portfolios
        });
    }
    setShowEditPortfolioModal(false);
}
    return(
       <SellerPage>
         <div className="seller-details-page d-flex flex-column">
            <div className="seller-details-top">
                
<SellerInfoCard onClose={() => setShowEditLanguagesModal(false)}
                    sellerDetails={sellerDetails}
                    showEditLanguagesModal={showEditLanguagesModal}
                    handleOnEditLanguagesButtonClick={handleOnEditLanguagesButtonClick}
                    handleOnEditLanguagesSuccess={handleOnEditLanguagesSuccess} showActions={true}/>
                <div className="gigs-quick-link">
                    <div className="quick-links-title">Quick Links </div>
                    <Link to="/seller/gigs" className="quick-link"><i className="bi bi-archive"></i> Gigs</Link>
                </div>
            </div>
            <>
                        <div className="seller-portfolio-container">
                            <div className="seller-portfolio-title">Portfolios</div>
                
                            {!sellerDetails?.portfolios && <div className="seller-portfolio-no-content">Add portfolio projects to showcase you skills</div>}
                            {sellerDetails?.portfolios && <div className="seller-portfolios-content">{
                                sellerDetails.portfolios.map((portfolio, index) => (
                                    <PortfolioTag key={index} title={portfolio.title} description={portfolio.description} imageUrl={portfolio.imageUrl} mainCategoryName={portfolio.mainCategoryName} />
                                ))
                            }
                            </div>}
                
                            {sellerDetails?.portfolios ? (
                                <ActionButton text={`+ Edit portfolios`}
                                    onClick={handleShowEditPortfolioModalVisibility} className="edit-seller-details"
                                    ariaLabel="Edit Seller Details Button"/>
                            ) : (
                                <ActionButton
                                    text={`+ Add Portfolios`} onClick={handleAddPortfolioModalVisibility}
                                    className="add-seller-details" ariaLabel="Add Seller Details Button"/>
                            )}
                
                
                        </div>
                            {showEditPortfolioModal && <AddOrEditDetailsModal title={'Portfolio'} onClose={() => setShowEditPortfolioModal(false)} show={showEditPortfolioModal}><EditProjectPortfolioModalForm initialData={sellerDetails?.portfolios ?? []} onEditSuccess={handleOnEditPortfolioSucess} /></AddOrEditDetailsModal>}
                            {showAddPortfolioModal && <AddOrEditDetailsModal title={'Portfolio'} onClose={() => setShowAddPortfolioModal(false)} show={showAddPortfolioModal}><EditProjectPortfolioModalForm initialData={sellerDetails?.portfolios ?? []} onEditSuccess={handleOnEditPortfolioSucess} /></AddOrEditDetailsModal>}
                        </>
            <div className="seller-details-list">

                        
                <DetailsComponent 
             onAddModalShow={() => setShowAddDescriptionModal(true)}
            addModal={<AddOrEditDetailsModal title={'About me'} onClose={() => setShowAddDescriptionModal(false)} show={showAddDescriptionModal}><EditDescriptionModalForm initialDescription={sellerDetails?.description ?? ''} onEditSuccess={handleOnEditDescriptionSuccess}/></AddOrEditDetailsModal>} 
             title={'About me'} noContent={"Tell your buyers a bit about yourself. Share your experience, interests, or anything that showcases your professionalism."} 
            editModal={<AddOrEditDetailsModal title={'About me'} onClose={() => setShowEditDescriptionModal(false)} show={showEditDescriptionModal}><EditDescriptionModalForm initialDescription={sellerDetails?.description ?? ''} onEditSuccess={handleOnEditDescriptionSuccess}/></AddOrEditDetailsModal>}
              onEditModalShow={() => setShowEditDescriptionModal(true)}>                                
                {sellerDetails?.description && <DescriptionTag description={sellerDetails.description}/>}
            </DetailsComponent>

                <DetailsComponent 
                onAddModalShow={handleOnSkillsAddModalShow} 
                addModal={<AddOrEditDetailsModal title={'Skills and expertise'} onClose={() => setShowAddSkillsModal(false)} show={showAddSkillsModal}><EditSkillsModalForm onSuccess={onEditSkillsSuccess} initialSkills={sellerDetails?.skills ?? []} /></AddOrEditDetailsModal>} 
                title={'Skills and expertise'} noContent={"Let your buyers know your skills. Skills gained through your previous jobs, hobbies or even everyday life."}
                 editModal={<AddOrEditDetailsModal title={'Skills and expertise'} onClose={() => setShowEditSkillsModal(false)} show={showEditSkillsModal}><EditSkillsModalForm onSuccess={onEditSkillsSuccess} initialSkills={sellerDetails?.skills ?? []} /></AddOrEditDetailsModal>}
                  onEditModalShow={() => setShowEditSkillsModal(true)}>
                                   
                                     {sellerDetails?.skills && sellerDetails.skills.length > 0 ? (
                                        sellerDetails.skills.map((skill, index) => (
                                            <SkillsTag key={index} skill={skill.name} />
                                        ))
                                    ) : (null)}
                </DetailsComponent>

            <DetailsComponent
                title={'Certifications'}
                noContent={"Add any certifications you have earned that are relevant to your skills and expertise."}
                onAddModalShow={() => setShowAddCertificationModal(true)}
                addModal={<AddOrEditDetailsModal title={'Certification'} onClose={() => setShowAddCertificationModal(false)} show={showAddCertificationModal}><EditCertificationModalForm onEditSuccess={handleOnEditCertification} initialCertifications={sellerDetails?.certifications ?? []} /></AddOrEditDetailsModal>}
                editModal={<AddOrEditDetailsModal title={'Certification'} onClose={() => setShowEditCertificationModal(false)} show={showEditCertificationModal}><EditCertificationModalForm onEditSuccess={handleOnEditCertification} initialCertifications={sellerDetails?.certifications ?? []} /></AddOrEditDetailsModal>}
                onEditModalShow={() => setShowEditCertificationModal(true)}
            >
                {sellerDetails?.certifications && sellerDetails.certifications.length > 0 ? (
                    sellerDetails.certifications.map((certification, index) => (
                        <CertificationTag key={index} id={certification.id} certification={certification.certification} issuer={certification.issuer} date={certification.date}                            />
                    ))
                ) : (null)}
            </DetailsComponent>

            <DetailsComponent
                title={'Education'}
                noContent={"Add your educational background, including degrees, certifications, and relevant courses."}
                onAddModalShow={() => setShowAddEducationModal(true)}
                addModal={<AddOrEditDetailsModal title={'Education'} onClose={() => setShowAddEducationModal(false)} show={showAddEducationModal}><EditEducationModalForm onSuccess={handleOnEditEducation} existing={sellerDetails?.education ?? []} /></AddOrEditDetailsModal>}
                editModal={<AddOrEditDetailsModal title={'Education'} onClose={() => setShowEditDescriptionModal(false)} show={showEditDescriptionModal}><EditEducationModalForm onSuccess={handleOnEditEducation} existing={sellerDetails?.education ?? []} /></AddOrEditDetailsModal>}
                onEditModalShow={() => setShowEditDescriptionModal(false)}
            >
                {sellerDetails?.education && sellerDetails.education.length > 0 ? (
                    sellerDetails.education.map((education, index) => (
                        <EducationTag key={index} degree={education.degree} institution={education.institution} major={education.major} endYear={education.endYear}                        />))
                ) : (null)}
            </DetailsComponent>
            </div>

        </div>
       </SellerPage>
    )
}
