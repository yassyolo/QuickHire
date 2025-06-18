import { ImageCarrousel } from "../../../../../Shared/Images/ImageCarrousel/ImageCarrousel";
import "./GigInfo.css";

interface GigInfoProps {
    description: string;
    title: string;
    imageUrls: string[];
    ordersInQueue: number;
    gigMetadata: GigMetadata[];
}

interface GigMetadata {
    title: string;
    items: string[];
}

export function GigInfo({ description, title, imageUrls, ordersInQueue, gigMetadata }: GigInfoProps) {
    return (
        <div className="gig-info d-flex flex-column">
            <h1 className="gig-title">{title}</h1>
                        <div className="orders-in-queue">Orders in Queue: {ordersInQueue}</div>
                                    <ImageCarrousel images={imageUrls ?? []}/>

                        <div className="gig-info-header">About this gig</div>
            <div className="gig-description">{description}</div>
            <div className="gig-info-divider"></div>
            <div className="gig-metadata d-flex flex-row justify-content-between">
                {gigMetadata.map((meta, index) => (
                    <div key={index} className="metadata-section d-flex flex-column">
                        <div className="metadata-title">{meta.title}</div>
                        <div className="metadata-items list-unstyled">
                            {meta.items.map((item, itemIndex) => (
                                <li className="metadata-item" key={itemIndex}>{item}</li>
                            ))}
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}