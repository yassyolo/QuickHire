import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import axios from "../../../../../axiosInstance";
import { Portfolio, Skill, UserLanguage } from "../../../../../Users/Seller/Pages/SellerProfile/SellerProfile";
import { Certification } from "../../../../../Users/Seller/Pages/SellerProfile/Forms/EditCErtificationModalForm";
import { Education } from "../../../../../Users/Seller/Pages/SellerProfile/Forms/EditEducationModalForm";
import { SellerInfoCard } from "../../../../../Users/Seller/Pages/SellerProfile/SellerProfileCard/SellerInfoCard";
import { SkillsTag } from "../../../../../Users/Seller/Pages/SellerProfile/Tags/Skills/SkillsTag";
import { CertificationTag } from "../../../../../Users/Seller/Pages/SellerProfile/Tags/Certification/CertificationTag";
import { EducationTag } from "../../../../../Users/Seller/Pages/SellerProfile/Tags/Education/EducationTag";
import { PortfolioTag } from "../../../../../Users/Seller/Pages/SellerProfile/Tags/Portfolio/PortfolioTag";
import "../../../../../Users/Buyer/SellerDetails/SellerDetailsPage.css";


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
    averageRating: number;
    topRated: boolean;
    industry: string;
    memberSince: string;
    totalReviews: number;
}


export function UserInfo() {
    const { id } = useParams<{ id: string }>();
    const [sellerDetails, setSellerDetails] = useState<SellerDetails | null>(null);

    useEffect(() => {
                const fetchSellerDetails = async () => {
            try {
                const params = new URLSearchParams();
                if( id) {
                  params.append('userId', id);
                }
                const url = `https://localhost:7267/gigs/seller?${params.toString()}`;
                const response = await axios.get<SellerDetails>(url);
                if (response.status === 200) {
                    setSellerDetails(response.data);
                    
                }
            } catch (error) {
                console.error("Error fetching seller details:", error);
            }
        }
        fetchSellerDetails();
    }, []);

   
    return(
        <div className="seller-details-page" style={{padding: '0'}}>
            <div className="seller-details-page-top d-flex flex-row justify-content-between">
                <div className="seller-details-dex-skills">
                    <div className="d-flex flex-row">
                         <SellerInfoCard sellerDetails={sellerDetails} showActions={false} averageRating={sellerDetails?.averageRating} totalReviews={sellerDetails?.totalReviews} topRated={sellerDetails?.topRated} industry={sellerDetails?.industry}  memberSince={sellerDetails?.memberSince}/>

                     <div className="d-flex flex-column seller-details-dex-skills-item" >
                        <div className="seller-details-dex-skills-item-header description">About me</div>
                        <div className="seller-details-dex-skills-item-content description">{sellerDetails?.description || "No description available."}</div>
                    </div>
                    </div>
                    
                    {sellerDetails?.skills && sellerDetails.skills.length > 0 &&
                    <div className="d-flex flex-column seller-details-dex-skills-item" style={{marginTop: '20px'}}>
                        <div className="seller-details-dex-skills-item-header">Skills</div>
                        <div className="seller-details-dex-skills-item-content-skills">
                            {sellerDetails?.skills.map((skill, index) => (
                                <SkillsTag key={index} skill={skill.name} />
                            ))}
                        </div>
                    </div> }
                    

                </div>               
                
            </div>
{sellerDetails?.certifications && sellerDetails.certifications.length > 0 && (
  <div className="d-flex flex-column seller-details-dex-skills-item">
    <div className="seller-details-dex-skills-item-header">Certifications</div>
    <div className="seller-details-dex-skills-item-content-certification" style={{display: 'grid', gridTemplateColumns: 'repeat(2, 1fr)', gap: '10px'}}>
      {sellerDetails.certifications.map((certification, index) => (
        <CertificationTag
          key={index}
          id={certification.id}
          certification={certification.certification}
          issuer={certification.issuer}
          date={certification.date}
        />
      ))}
    </div>
  </div>
)}

{sellerDetails?.education && sellerDetails.education.length > 0 && (
  <div className="d-flex flex-column seller-details-dex-skills-item">
    <div className="seller-details-dex-skills-item-header">Education</div>
    <div className="seller-details-dex-skills-item-content-education" style={{display: 'grid', gridTemplateColumns: 'repeat(2, 1fr)', gap: '10px'}}>
      {sellerDetails.education.map((education, index) => (
        <EducationTag
          key={index}
          degree={education.degree}
          institution={education.institution}
          major={education.major}
          endYear={education.endYear}
        />
      ))}
    </div>
  </div>
)}

{(sellerDetails?.portfolios?.length ?? 0) > 0 && (
  <div className="d-flex flex-column seller-details-dex-skills-item">
    <div className="seller-details-dex-skills-item-header">Portfolios</div>
    <div className="seller-details-dex-skills-item-content-portfolios" style={{display: 'grid', gridTemplateColumns: 'repeat(2, 1fr)', gap: '10px'}}>
      {sellerDetails?.portfolios.map((portfolio, index) => (
        <PortfolioTag
          key={index}
          title={portfolio.title}
          description={portfolio.description}
          imageUrl={portfolio.imageUrl}
          mainCategoryName={portfolio.mainCategoryName}
        />
      ))}
    </div>
  </div>
)}
                
 
        </div>
    )
}