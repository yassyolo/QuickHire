import { useEffect, useState } from "react";
import { Tag } from "./Tag";
import "./TagList.css";

export interface TagListProps {
    gigId?: number;
    mainCategoryId?: number;
}

export interface Tag{
    label: string;
}

export function TagList({ mainCategoryId, gigId }: TagListProps) {
    const [tags, setTags] = useState<Tag[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchTags();
    }, []);

    const fetchTags = async () => {
        setLoading(true);
        try {
            const params = new URLSearchParams();
            if (gigId) {
                params.append("GigId", gigId.toString());
            } else if (mainCategoryId) {
                params.append("MainCategoryId", mainCategoryId.toString());
            }
const url = `https://localhost:7267/tags?${params.toString()}`;
            const response = await fetch(url, {
                method: 'GET',
                headers: {
                    'Accept': '*/*',
                },
            });
            if (!response.ok) {
                throw new Error("Failed to fetch categories");
            }

            const result: Tag[] = await response.json();
            setTags(result);
        } catch (error) {
            console.error("Error fetching tags:", error);
        } finally {
            setLoading(false);
        }
    };
    return (
        <div className="tag-list" aria-label="tag-list">
            {loading ? (
                <div className="loading" aria-label="loading">
                    <div id={"loading"} className="loading-text">Loading...</div>
                    </div>
            ) : (
                <div className="tags" aria-label="tags">
                    {tags.map((tag, index) => (
                        <Tag key={index} label={tag.label} />
                    ))}
                </div>
            )}
        </div>
    );
}