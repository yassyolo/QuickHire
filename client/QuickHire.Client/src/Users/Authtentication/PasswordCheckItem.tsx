import './PasswordCheckItem.css';
interface PasswordCheckItemProps {
  isValid: boolean;
  message: string;
}

export function PasswordCheckItem({ isValid , message}: PasswordCheckItemProps) {

  return (
    <div className={`password-check-item d-flex flex-row ${isValid ? 'valid' : 'invalid'}`}>
      <div className="check-circle">
        <i className="bi bi-check-lg"></i>
      </div>
      <div className="message">{message}</div>
    </div>
    
  );
}