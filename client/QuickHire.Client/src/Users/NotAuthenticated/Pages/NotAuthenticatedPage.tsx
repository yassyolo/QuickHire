import { useEffect, useState } from 'react';
import './NotAuthenticatedPage.css';
import axios from 'axios';
import { ActionButton } from '../../../Shared/Buttons/ActionButton/ActionButton';
import { useNavigate } from 'react-router-dom';
interface NotAuthenticatedPage{
    popularServices: PopularServices[];
    popularTags: string[];
}

interface PopularServices{
    imageUrl: string;
    title: string;
    id: number;
}
export function NotAuthenticatedPage() {
    const [keyword, setKeyword] = useState<string | undefined>(undefined);
    const navigate = useNavigate();
    const [notAuthenticatedPage, setNotAuthenticatedPage] = useState<NotAuthenticatedPage>({
        popularServices: [],
        popularTags: []
    });

    useEffect(() => {
        const fetchNotAuthenticatedPage = async () => {
            try {
                const response = await axios.get<NotAuthenticatedPage>('https://localhost:7267/not-authenticated');
                setNotAuthenticatedPage(response.data);
            } catch (error) {
                console.error('Error fetching not authenticated page data:', error);
            }
        }
        fetchNotAuthenticatedPage();
    }
    , []);



    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value;
        setKeyword(value);

        if (value === '') {
            setKeyword(undefined);
        }
    };


const handleOnSearch = () => {
    const trimmed = keyword?.trim() || '';
    if (!trimmed) return; 
    setKeyword(trimmed);
    navigate(`/login?redirectTo=${encodeURIComponent(`/buyer/search?keyword=${trimmed}`)}`);
};


    return (
            <div className="not-authenticated-page d-flex flex-column">
                  <div className="video-wrapper">
                    <video className="not-authenticated-video" src="https://fiverr-res.cloudinary.com/video/upload/v1/video-attachments/generic_asset/asset/18ad23debdc5ce914d67939eceb5fc27-1738830703211/Desktop%20Header%20new%20version"  autoPlay  loop muted></video>

    <div className="search-conatiner-header-container d-flex flex-column">
        <div className="search-container-header">Our freelancers will take it from here</div>
        <div className="search-container">
          <input id="gig-keyword-input" type="text" className="form-control" value={keyword === undefined ? '' : keyword} onChange={handleInputChange} placeholder='Search for any service...' />
          <button className="search-container-button" onClick={handleOnSearch} aria-label="Search"><i className="fa-solid fa-magnifying-glass"></i></button>    
        </div>
        <div className="tags-row d-flex flex-row">
            {notAuthenticatedPage.popularTags.map((tag, index) => (
                <div key={index} className="tag-row-item" onClick={() => {
                setKeyword(tag);
                handleOnSearch();
                }}>
                {tag}
                </div>
            ))}
        </div>
    </div>
    </div>
    

  <div className="popular-services" style={{padding: "50px 50px"}}>
    <div className="popular-services-header">Popular services</div>
    <div className="popular-services-list">
      {notAuthenticatedPage.popularServices.map((service) => (
        <div key={service.id} className="popular-service-item">
                      <div className="popular-service-title">{service.title}</div>
          <img src={service.imageUrl} alt={service.title} className="popular-service-image" />
        </div>
      ))}
      </div>
  </div>
  <div className="d-flex flex-column" style={{padding: "50px 50px"}}>
    <div className="popular-services-header" style={{marginBottom: '50px'}}>Make it all happen with freelancers</div>
  <div className="maike-it-all-happen-with-freelancers">
    <div className="make-it-all-happen-item d-flex flex-column">
        <i className="bi bi-shuffle"></i>
        <div className="make-it-all-happen-desc">Access a pool of top talent over hundreds of categories</div>
    </div>
    <div className="make-it-all-happen-item d-flex flex-column">
        <i className="bi bi-stack-overflow"></i>
        <div className="make-it-all-happen-desc">Enjoy a simple, easy-to-use matching experience</div>
    </div>
    <div className="make-it-all-happen-item d-flex flex-column">
        <i className="bi bi-piggy-bank"></i>
        <div className="make-it-all-happen-desc">Get quality work done quickly and within budget</div>
    </div>
    <div className="make-it-all-happen-item d-flex flex-column">
<i className="bi bi-lightbulb"></i>       
 <div className="make-it-all-happen-desc">Turn your ideas into reality faster with expert help</div>
    </div>
  </div>
                <ActionButton text={"Join"} onClick={() => navigate("/login")} className={"join-button"} ariaLabel={"Sign up button"}/>
    
  </div>
        
</div>
    );
}