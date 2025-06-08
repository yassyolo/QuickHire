import { useEffect, useState } from "react";
import axios from "axios";
import { useTooltip } from "../../../../../../Shared/Tooltip/Tooltip";
import { AddDetailsModal } from "../Add/AddDetailsForm";
import { DetailsModalButtons } from "../EditOrDelete/DetailsModalButtons";
import { NewAddedLanguage } from "../Add/NewAddedItems/Language/NewAddedLanguage";
import { SelectDropdown } from "../../../../../../Shared/Select/SelectDropdown";

export interface Language {
  id: number;
  name: string;
}

export interface UserLanguage {
  languageId: number;
  languageName: string;
}

interface EditLanguageModalFormProps {
  initialLanguages: UserLanguage[];
  onEditSuccess: (updatedLanguages: UserLanguage[]) => void;
}

export function EditLanguageModalForm({
  initialLanguages,
  onEditSuccess,
}: EditLanguageModalFormProps) {
  const [newLanguages, setNewLanguages] = useState<UserLanguage[]>([]);
  const [availableLanguages, setAvailableLanguages] = useState<Language[]>([]);
  const [selectedLanguageId, setSelectedLanguageId] = useState<number | null>(null);
  const [langToEditId, setLangToEditId] = useState<number | null>(null);

  const [showLanguageTooltip, handleShowLanguageTooltip] = useTooltip();

  const fetchAvailableLanguages = async () => {
    try {
      const response = await axios.get<Language[]>("https://localhost:7267/languages/populate");
      setAvailableLanguages(response.data);
    } catch (error) {
      console.error("Error fetching available languages:", error);
    }
  };

  useEffect(() => {
    fetchAvailableLanguages();
    setNewLanguages(initialLanguages);
  }, [initialLanguages]);

  const clearForm = () => {
    setSelectedLanguageId(null);
    setLangToEditId(null);
  };

  const onAddOrUpdate = () => {
    const selectedLanguage = availableLanguages.find(l => l.id === selectedLanguageId);
    if (!selectedLanguage) return;

    const updatedLang: UserLanguage = {
      languageId: selectedLanguage.id,
      languageName: selectedLanguage.name,
    };

    if (langToEditId !== null) {
      setNewLanguages(prev =>
        prev.map(lang =>
          lang.languageId === langToEditId ? updatedLang : lang
        )
      );
    } else {
      const alreadyExists = newLanguages.some(l => l.languageId === selectedLanguage.id);
      if (!alreadyExists) {
        setNewLanguages(prev => [...prev, updatedLang]);
      }
    }

    clearForm();
  };

  const handleRemove = (lang: UserLanguage) => {
    setNewLanguages(prev => prev.filter(l => l.languageId !== lang.languageId));
    if (langToEditId === lang.languageId) clearForm();
  };

  const handleSave = async () => {
    try {
      await axios.put("https://localhost:7267/user/languages", {
        Languages: newLanguages.map(lang => ({
          LanguageId: lang.languageId,
        })),
      });

      onEditSuccess(newLanguages);
      clearForm();
    } catch (error) {
      console.error("Error saving languages:", error);
    }
  };

  return (
    <div className="add-form-wrapper">
      <AddDetailsModal onSave={onAddOrUpdate}>
        <SelectDropdown
          id="language"
          label="Language"
          options={availableLanguages}
          value={selectedLanguageId === null ? undefined : selectedLanguageId}
          onChange={(value) => setSelectedLanguageId(value === undefined ? null : value)}
          getOptionLabel={(opt) => opt.name}
          getOptionValue={(opt) => opt.id}
          tooltipDescription="Choose a language you speak."
          showTooltip={showLanguageTooltip}
          onShowTooltip={handleShowLanguageTooltip}
          ariaDescribedBy="language-help"
        />
      </AddDetailsModal>

      {newLanguages.map((lang) => (
        <NewAddedLanguage
          key={lang.languageId}
          languageName={lang.languageName}
          onDelete={() => handleRemove(lang)}
          onEdit={() => handleRemove(lang)}
        />
      ))}

      <div className="description-buttons">
        <DetailsModalButtons onSave={handleSave} onClear={clearForm} />
      </div>
    </div>
  );
}
