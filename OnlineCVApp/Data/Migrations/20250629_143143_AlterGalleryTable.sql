-- Add new columns
ALTER TABLE GalleryImages
ADD ImageUrl NVARCHAR(MAX) NOT NULL DEFAULT '',
    PublicId NVARCHAR(255) NOT NULL DEFAULT '',
    CreatedAt DATETIME DEFAULT GETDATE();

-- Drop the old columns
ALTER TABLE GalleryImages
DROP COLUMN ImageData;

-- Rename the table
EXEC sp_rename 'GalleryImages', 'CloudinaryGallery';
