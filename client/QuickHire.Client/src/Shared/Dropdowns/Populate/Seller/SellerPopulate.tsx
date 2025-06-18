import { useEffect, useState } from 'react';
import './SellerPopulate.css';
import axios from '../../../../axiosInstance';
import { Item } from '../../Common/PopulateDropdown';
import { Language } from '../../../../Users/Seller/Pages/SellerProfile/Forms/EditLanguageModalForm';
import { Dropdown } from '../../Common/Dropdown/Dropdown';
import { Checkbox } from '../../Common/Checkbox/Checkbox';

interface SellerFilterDropdownProps {
  selectedCountryIds: number[];
  selectedLanguageIds: number[];
  onApply: (filters: {
    selectedCountryIds: number[];
    selectedLanguageIds: number[];
  }) => void;
}

export function SellerPopulateDropdown({
  selectedCountryIds,
  selectedLanguageIds,
  onApply,
}: SellerFilterDropdownProps) {
  const [internalCountryIds, setInternalCountryIds] = useState<number[]>(selectedCountryIds);
  const [internalLanguageIds, setInternalLanguageIds] = useState<number[]>(selectedLanguageIds);
  const [countryOptions, setCountryOptions] = useState<Item<number>[]>([]);
  const [languageOptions, setLanguageOptions] = useState<Language[]>([]);

  useEffect(() => {
    const fetchCountries = async () => {
      try {
        const url = 'https://localhost:7267/admin/filters/countries';
        const response = await axios.get<Item<number>[]>(url);
        setCountryOptions(response.data.map(item => ({
          ...item,
          id: typeof item.id === 'string' ? Number(item.id) : item.id,
        })));
      } catch (error) {
        console.error('Error fetching countries:', error);
      }
    };

    const fetchLanguages = async () => {
      try {
        const res = await axios.get<Language[]>('https://localhost:7267/languages/populate');
        setLanguageOptions(res.data);
      } catch (error) {
        console.error('Error fetching languages:', error);
      }
    };

    fetchCountries();
    fetchLanguages();
  }, []);

  const toggleCountry = (id: number) => {
    setInternalCountryIds(prev =>
      prev.includes(id) ? prev.filter(x => x !== id) : [...prev, id]
    );
  };

  const toggleLanguage = (id: number) => {
    setInternalLanguageIds(prev =>
      prev.includes(id) ? prev.filter(x => x !== id) : [...prev, id]
    );
  };

  const handleClearAll = () => {
    setInternalCountryIds([]);
    setInternalLanguageIds([]);
  };

  const handleApply = () => {
    onApply({
      selectedCountryIds: internalCountryIds,
      selectedLanguageIds: internalLanguageIds,
    });
  };

  return (
    <Dropdown onClearAll={handleClearAll} onApply={handleApply}>
      <div className="filter-item">
        <div className="filter-title">Seller lives in</div>
        <div className="checkbox-list">
          {countryOptions.map(country => (
            country.id !== undefined && (
              <Checkbox
                key={country.id}
                id={country.id}
                label={country.name}
                isSelected={internalCountryIds.includes(country.id)}
                onChange={toggleCountry}
              />
            )
          ))}
        </div>
      </div>
      <div className="divider"></div>

      <div className="filter-item">
        <div className="filter-title">Seller speaks</div>
        <div className="checkbox-list">
          {languageOptions.map(language => (
            <Checkbox
              key={language.id}
              id={language.id}
              label={language.name}
              isSelected={internalLanguageIds.includes(language.id)}
              onChange={toggleLanguage}
            />
          ))}
        </div>
      </div>
    </Dropdown>
  );
}
