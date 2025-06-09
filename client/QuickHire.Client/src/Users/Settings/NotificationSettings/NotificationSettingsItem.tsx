import React, { useState } from "react";
import './NotificationSettingsItem.css';

interface NotificationSettingsItemProps {
  label: string;
onChange: (label: string, checked: boolean) => void;
}

export function NotificationSettingsItem({ label, onChange }: NotificationSettingsItemProps) {
   const [checked, setChecked] = useState(false);

  const handleNotificationItemChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const isChecked = event.target.checked;
    setChecked(isChecked);
    onChange(label, isChecked);
  };


  return (
    <div className="notification-settings-item">
      <label style={{ cursor: "pointer" }}>
        <input type="checkbox" checked={checked} onChange={handleNotificationItemChange}/>
        {label}
      </label>
    </div>
  );
}
