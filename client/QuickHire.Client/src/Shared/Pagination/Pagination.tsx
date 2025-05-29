import { useEffect } from 'react';
import './Pagination.css';

export interface PaginationProps{
    totalPages: number;
    currentPage: number;
    onPageChange: (page: number) => void;
}

export function Pagination({ totalPages, currentPage, onPageChange } : PaginationProps) {
    const pages = [];
    for (let i = 1; i <= totalPages; i++) {
      pages.push(i);
    }  
    
    const handleBackPage = () => {
        if (currentPage > 1) {
            onPageChange(currentPage - 1);
        }
    }

    const handleNextPage = () => {
        if (currentPage < totalPages) {
            onPageChange(currentPage + 1);
        }
    }

    useEffect(() => {
        const handleKeyDown = (event: KeyboardEvent) => {
            if (event.key === 'ArrowLeft' && currentPage > 1) {
                onPageChange(currentPage - 1);
            }
            if (event.key === 'ArrowRight' && currentPage < totalPages) {
                onPageChange(currentPage + 1);
            }
        }
        window.addEventListener('keydown', handleKeyDown);

        return () => window.removeEventListener('keydown', handleKeyDown);
    }, [currentPage, totalPages, onPageChange]);

    return (
        <div className="pagination" aria-label="Pagination">
            <button key={currentPage-1} className="pagination-button" disabled={currentPage === 1} onClick={handleBackPage}> &lt;</button>
            {pages.map((page) => (<div key={page} className={`page-item ${currentPage === page ? 'active' : ''}`} aria-current={currentPage === page ? 'page' : undefined}>{page}</div>))}
            <button key={currentPage+1} className="pagination-button" disabled={currentPage === totalPages} onClick={handleNextPage}> &gt;</button>
        </div>
    )
}

