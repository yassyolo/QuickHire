import axios from '../../../../../axiosInstance';
import { useEffect, useState } from 'react';
import { BrowsingHistoryRowItem } from './BrowsingHistoryRowItem';
import { useNavigate } from 'react-router-dom';
import './BrowsingHistoryRow.css';
interface BrowsingHistoryRowItem{
    id: number;
    gigId: number;
    title: string;
    imageUrl: string;
    liked: boolean;
}


export function BrowsingHistoryRow() {
    const [items, setItems] = useState<BrowsingHistoryRowItem[]>([]);
    const navigate = useNavigate();

    const fetchBrowsingHistoryRowItems = async () => {
        try {
            const response = await axios.get<BrowsingHistoryRowItem[]>(`https://localhost:7267/buyers/browsing-history/row`);
            setItems(response.data);
        } catch (error) {
            console.error("Error fetching browsing history row items:", error);
            return [];
        }
    }

    const handleClearBrowsingHistory = async () => {
        try {
            await axios.delete(`https://localhost:7267/buyers/browsing-history`);
            setItems([]);
        } catch (error) {
            console.error("Error clearing browsing history:", error);
        }
    };

    const handleSeeAllBrowsingHistory = () => {
        navigate('/buyer/browsing-history');
    }

    useEffect(() => {
        fetchBrowsingHistoryRowItems();
    }, []);

    const handleOnsetLiked = (liked: boolean, id: number) => {
        console.log("Setting liked for item with id:", id, "to", liked);
        setItems((prevItems) =>
            prevItems.map((item) =>
                item.id === id ? { ...item, liked } : item
            )
        );
    }

    return (
        items.length > 0 ? (
            <div className="browsing-history-row d-flex flex-column">
                <div className="header-buttons d-flex flex-row justify-content-between align-items-center">
                    <div className="browsing-history-row-header">Your Browsing History</div>
                    <div className="browsing-history-buttons d-flex flex-row justify-content-between align-items-center">
                        <button className="browsing-history-button" onClick={handleClearBrowsingHistory}>Clear All</button>
                        <div className="browsing-history-button-divider"></div>
                        <button className="browsing-history-button" onClick={handleSeeAllBrowsingHistory}>See All</button>
                    </div>
                </div>
                <div className="browsing-history-row-items">
                    {items.map((item) => (
                        <BrowsingHistoryRowItem key={item.id} id={item.id} gigId={item.gigId} title={item.title} imageUrl={item.imageUrl} liked={item.liked} setLiked={handleOnsetLiked}/>
                    ))}
                </div>
            </div>
        ) : null
    );
}