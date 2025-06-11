import { useEffect, useState } from 'react';
import { Checkbox } from '../../../Shared/Dropdowns/Common/Checkbox/Checkbox';
import { Dropdown } from '../../../Shared/Dropdowns/Common/Dropdown/Dropdown';
import axios from '../../../axiosInstance';
import { Item } from '../../../Shared/Dropdowns/Common/PopulateDropdown';
import './MessageFilterDropdown.css';

interface MessageFilterDropdownProps {
  selectedOrderStatusIds: number[];
  filterByCustomOffer: boolean;
  filterByStarred: boolean;
  onApply: (filters: {
    selectedOrderStatusIds: number[]; 
    filterByCustomOffer: boolean;
    filterByStarred: boolean;
  }) => void;
}

export function MessageFilterDropdown({
  selectedOrderStatusIds,
  filterByCustomOffer,
  filterByStarred,
  onApply,
}: MessageFilterDropdownProps) {
  const [internalOrderStatus, setInternalOrderStatus] = useState<number[]>(selectedOrderStatusIds);
  const [orderStatusOptions, setOrderStatusOptions] = useState<Item[]>([]);
  const [customOffer, setCustomOffer] = useState<boolean>(filterByCustomOffer);
  const [starred, setStarred] = useState<boolean>(filterByStarred);

  useEffect(() => {
    const fetchOrderStatusOptions = async () => {
      try {
        const url = 'https://localhost:7267/admin/filters/order-status';
        const response = await axios.get<Item[]>(url);
        setOrderStatusOptions(response.data);
      } catch (error) {
        console.error('Error fetching order status options:', error);
      }
    };

    fetchOrderStatusOptions();
  }, []);

  const toggleOrderStatus = (id: number) => {
    setInternalOrderStatus((prev) =>
      prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id]
    );
  };

  const handleClearAll = () => {
    setInternalOrderStatus([]);
    setCustomOffer(false);
    setStarred(false);
  };

  const handleApply = () => {
    onApply({
      selectedOrderStatusIds: internalOrderStatus, 
      filterByCustomOffer: customOffer,
      filterByStarred: starred,
    });
  };

  return (
    <Dropdown onClearAll={handleClearAll} onApply={handleApply}>
      <div className="filter-item">
        <div className="filter-title">Order Status</div>
        <div className="order-status-list">
            {orderStatusOptions
          .filter((status) => typeof status.id === 'number')
          .map((status) => (
            <Checkbox
              key={status.id}
              id={status.id as number}
              label={status.name}
              isSelected={internalOrderStatus.includes(status.id as number)}
              onChange={toggleOrderStatus}
            />
        ))}
        </div>
        
      </div>
      <div className="filter-item">
        <div className="filter-title">Other Filters</div>
        <Checkbox
          id="customOffer"
          label="Has Custom Offer"
          isSelected={customOffer}
          onChange={() => setCustomOffer((prev) => !prev)}
        />
        <Checkbox
          id="starred"
          label="Starred"
          isSelected={starred}
          onChange={() => setStarred((prev) => !prev)}
        />
      </div>
    </Dropdown>
  );
}
