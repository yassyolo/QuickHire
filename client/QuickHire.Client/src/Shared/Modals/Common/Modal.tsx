export interface ModalProps {
    children: React.ReactNode;
    className?: string;
}

export function Modal({ children, className }: ModalProps) {
    return (
        <div className="modal-overlay" aria-modal="true" role="dialog" aria-labelledby="modal-title">
            {className ? (
                <div className={`${className}`} role="document" aria-label="Modal Content" aria-describedby="modal-description">
                    {children}
                </div>
            ) : (
                <div className="modal-content" role="document" aria-label="Modal Content" aria-describedby="modal-description">
                    {children}
                </div>
            )}
        </div>
    );
}