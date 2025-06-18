export interface ModalActionsProps {
    id: string;
    children: React.ReactNode; 
}

export function ModalActions({ id, children }: ModalActionsProps) {
    return (
        <div className="modal-actions" id={id} aria-label="modal-actions">{children}</div>
    );
}