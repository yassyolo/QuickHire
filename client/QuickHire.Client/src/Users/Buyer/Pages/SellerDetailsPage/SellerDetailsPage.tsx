import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import axios from "axios";
import "./SellerDetailsPage.css";
import { ContactMe } from "./ContactMe/ContactMe";
import { Gig } from "../FirstPage/FrontPage";
import { SellerPage } from "../../../Seller/Pages/Common/SellerPage";
import { SellerInfoCard } from "../../../Seller/Pages/SellerProfile/SellerProfileCard/SellerInfoCard";
import { SkillsTag } from "../../../Seller/Pages/SellerProfile/Tags/Skills/SkillsTag";
import { CertificationTag } from "../../../Seller/Pages/SellerProfile/Tags/Certification/CertificationTag";
import { EducationTag } from "../../../Seller/Pages/SellerProfile/Tags/Education/EducationTag";
import { PortfolioTag } from "../../../Seller/Pages/SellerProfile/Tags/Portfolio/PortfolioTag";
import { GigCard } from "../../../../Gigs/Common/GigCard/GigCard";
import { RatingDistribution } from "../../../../Orders/Pages/Reviews/RatingDistrbution/RatingDistribution";
import { ReviewsList } from "../../../../Orders/Pages/Reviews/ReviewsLIst/ReviewsList";
import { Portfolio, Skill, UserLanguage } from "../../../Seller/Pages/SellerProfile/SellerProfile";
import { Certification } from "../../../Seller/Pages/SellerProfile/Forms/EditCErtificationModalForm";
import { Education } from "../../../Seller/Pages/SellerProfile/Forms/EditEducationModalForm";



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
    userId: string;
}



export function SellerDetailsPage() {
    const { id } = useParams<{ id: string }>();
    const sellerId = id ? parseInt(id) : null;
    const [sellerDetails, setSellerDetails] = useState<SellerDetails | null>(null);
    const [sellerGigs, setSellerGigs] = useState<Gig[]>([]);

    useEffect(() => {
        const fetchSellerGigs = async () => {
            try {
                const params = new URLSearchParams();
                if (sellerId !== null) {
                    params.append('id', sellerId.toString());
                }
                const url = `https://localhost:7267/seller/gigs?${params.toString()}`;
                const response = await axios.get<Gig[]>(url);
                if (response.status === 200) {
                    setSellerGigs(response.data);
                }
            } catch (error) {
                console.error("Error fetching seller gigs:", error);
            }
        }
        fetchSellerGigs();
        const fetchSellerDetails = async () => {
            try {
                const params = new URLSearchParams();
                if (sellerId !== null) {
                    params.append('id', sellerId.toString());
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

    const setGigLikedSuccess = (liked: boolean, id: number) => {
        setSellerGigs(prevGigs => 
            prevGigs.map(gig => 
                gig.id === id ? { ...gig, liked } : gig
            )
        );
    };
    return(
        <SellerPage>
        <div className="seller-details-page">
            <div className="seller-details-page-top d-flex flex-row justify-content-between">
                <div className="seller-details-dex-skills">
                     <SellerInfoCard sellerDetails={sellerDetails} showActions={false} averageRating={sellerDetails?.averageRating} totalReviews={sellerDetails?.totalReviews} topRated={sellerDetails?.topRated} industry={sellerDetails?.industry}  memberSince={sellerDetails?.memberSince}/>

                     <div className="d-flex flex-column seller-details-dex-skills-item">
                        <div className="seller-details-dex-skills-item-header description">About me</div>
                        <div className="seller-details-dex-skills-item-content description">{sellerDetails?.description || "No description available."}</div>
                    </div>
                    <div className="d-flex flex-column seller-details-dex-skills-item">
                        <div className="seller-details-dex-skills-item-header">Skills</div>
                        <div className="seller-details-dex-skills-item-content-skills">
                            {sellerDetails?.skills.map((skill, index) => (
                                <SkillsTag key={index} skill={skill.name} />
                            ))}
                        </div>
                    </div>

                </div>
                {sellerDetails?.userId && <ContactMe userId={sellerDetails.userId} />}
                
            </div>
{sellerDetails?.certifications && sellerDetails.certifications.length > 0 && (
  <div className="d-flex flex-column seller-details-dex-skills-item">
    <div className="seller-details-dex-skills-item-header">Certifications</div>
    <div className="seller-details-dex-skills-item-content-certification">
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
    <div className="seller-details-dex-skills-item-content-education">
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
    <div className="seller-details-dex-skills-item-content-portfolios">
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
   

           {sellerGigs.length > 0 && (
  <div className="seller-gigs">
    <div className="seller-gigs-header">My Gigs</div>
    <div className="seller-gigs-list">
      {sellerGigs.map((gig) => (
        <GigCard
          key={gig.id}
          gig={gig}
          showSeller={true}
          setLiked={setGigLikedSuccess}
        />
      ))}
    </div>
  </div>)
  }

  <div className="seller-reviews d-flex flex-column">
    <div className="rating-distribution-wrapper">
        <RatingDistribution userId={sellerId ?? undefined} gigId={undefined}/>
    </div>
    <div className="rating-list-wrapper">
            <ReviewsList userId={sellerId ?? undefined} gigId={undefined} />
    </div>
  </div>
        </div>
    </SellerPage>
    )
}