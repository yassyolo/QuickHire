import { useEffect } from 'react';
import './NoPagesPagination.css';

export interface PaginationProps {
  totalPages: number;
  currentPage: number;
  onPageChange: (page: number) => void;
}

export function NoPagesPagination({ totalPages, currentPage, onPageChange }: PaginationProps) {
  const handleBackPage = () => {
    if (currentPage > 1) {
      onPageChange(currentPage - 1);
    }
  };

  const handleNextPage = () => {
    if (currentPage < totalPages) {
      onPageChange(currentPage + 1);
    }
  };

  useEffect(() => {
    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.key === 'ArrowLeft' && currentPage > 1) {
        onPageChange(currentPage - 1);
      }
      if (event.key === 'ArrowRight' && currentPage < totalPages) {
        onPageChange(currentPage + 1);
      }
    };
    window.addEventListener('keydown', handleKeyDown);
    return () => window.removeEventListener('keydown', handleKeyDown);
  }, [currentPage, totalPages, onPageChange]);

  return (
    <div className="no-pages-pagination" aria-label="Pagination">
      <button className="no-pages-pagination-button" disabled={currentPage === 1} onClick={handleBackPage} aria-label="Previous page" > &lt; </button>
      <button className="no-pages-pagination-button" disabled={currentPage === totalPages} onClick={handleNextPage} aria-label="Next page" > &gt; </button>
    </div>
  );
}
