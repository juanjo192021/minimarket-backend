namespace minimarket_project_backend.Services.Implementation
{
    public class ImageManagerService: IImageManagerService
    {
        private readonly IFirebaseStorageService _firebaseStorageService;

        public ImageManagerService(IFirebaseStorageService firebaseStorageService)
        {
            _firebaseStorageService = firebaseStorageService;
        }

        public async Task<string?> UploadImageAsync(IFormFile? fileImage, string storageFolder, string? existingImageUrl = null)
        {
            if (fileImage == null) return existingImageUrl; // Retorna la URL existente si no hay imagen nueva

            using var stream = fileImage.OpenReadStream();
            string fileName = $"{Guid.NewGuid()}_{fileImage.FileName}";

            return await _firebaseStorageService.UploadFileAsync(storageFolder, stream, fileName, existingImageUrl);
        }
    }
}
