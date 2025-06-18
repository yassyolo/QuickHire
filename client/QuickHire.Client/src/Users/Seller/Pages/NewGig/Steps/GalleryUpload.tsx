import { useRef } from "react";

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

}: GalleryUploadProps) {
  const inputRef = useRef<HTMLInputElement>(null);
  const MAX_IMAGES = 5;

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) {
      const selectedFiles = Array.from(e.target.files);
      const totalFiles = [...images, ...selectedFiles].slice(0, MAX_IMAGES);
      onImagesChange(totalFiles);

      // Reset input so the same file can be re-selected
      if (inputRef.current) {
        inputRef.current.value = '';
      }
    }
  };

  const handleRemoveImage = (index: number) => {
    const updated = images.filter((_, i) => i !== index);
    onImagesChange(updated);
  };

  return (
    <div className="flex flex-col gap-4">
      

      <input
        id="gallery-upload"
        type="file"
        accept="image/*"
        multiple
        onChange={handleFileChange}
        ref={inputRef}
        className="block border border-gray-300 rounded px-3 py-2 w-full text-sm file:mr-4 file:py-2 file:px-4 file:rounded-full file:border-0 file:text-sm file:bg-blue-50 file:text-blue-700 hover:file:bg-blue-100"
      />

      {validationErrors.length > 0 && (
        <div className="text-red-500 text-sm">{validationErrors[0]}</div>
      )}

      {images.length > 0 && (
        <div className="flex flex-wrap gap-3">
          {images.map((file, index) => {
            const previewUrl = URL.createObjectURL(file);
            return (
              <div
                key={index}
                className="relative"
                style={{
                  width: "450px",
                  height: "300px",
                  borderRadius: "8px",
                  overflow: "hidden",
                  boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
                  border: "1px solid #ccc",
                  margin: "5px",
                }}
              >
                <img
                  src={previewUrl}
                  alt={`Preview ${index + 1}`}
                  style={{ borderRadius: "8px" , objectFit: "cover" }}
                />
                <button
                  onClick={() => handleRemoveImage(index)}
                  className="absolute top-1 right-1 bg-red-500 text-white rounded-full px-2 py-0.5 text-xs opacity-80 hover:opacity-100"
                  aria-label="Remove image"
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
