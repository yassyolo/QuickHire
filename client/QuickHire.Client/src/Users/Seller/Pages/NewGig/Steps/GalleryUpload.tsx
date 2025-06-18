import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";

interface GalleryUploadProps {
  images: File[];
  onImagesChange: (files: File[]) => void;
  validationErrors?: string[];
  tooltipDescription?: string;
  showTooltip?: boolean;
  onShowTooltip?: () => void;
}

export function GalleryUpload({
  images,
  onImagesChange,
  validationErrors = [],
  tooltipDescription = "Upload up to 5 high-quality images.",
  showTooltip,
  onShowTooltip,
}: GalleryUploadProps) {
  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    let selectedFiles: File[] = [];
    if (e.target instanceof HTMLInputElement && e.target.files) {
      selectedFiles = Array.from(e.target.files);
    }
    onImagesChange([...images, ...selectedFiles]);
  };

  const handleRemoveImage = (index: number) => {
    const updated = images.filter((_, i) => i !== index);
    onImagesChange(updated);
  };

  return (
    <div className="flex flex-col gap-4">
      <FormGroup
        id="gallery-upload"
        label="Gallery Images"
        tooltipDescription={tooltipDescription}
        type="file"
        multiple
        onChange={handleFileChange}
        onShowTooltip={onShowTooltip}
        showTooltip={showTooltip}
        error={validationErrors}
      />

      {images.length > 0 && (
        <div className="flex flex-wrap gap-3">
          {images.map((file, index) => {
            const previewUrl = URL.createObjectURL(file);
            return (
              <div key={index} className="relative group">
                <img
                  src={previewUrl}
                  alt={`Preview ${index + 1}`}
                  className="w-32 h-32 object-cover rounded border"
                />
                <button
                  onClick={() => handleRemoveImage(index)}
                  className="absolute top-1 right-1 bg-red-500 text-white rounded-full px-2 py-0.5 text-xs opacity-80 hover:opacity-100"
                >
                  ✕
                </button>
              </div>
            );
          })}
        </div>
      )}
    </div>
  );
}
