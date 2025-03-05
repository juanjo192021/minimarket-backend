namespace minimarket_project_backend.Services
{
    public interface IImageManagerService
    {
        public Task<string?> UploadImageAsync(IFormFile? fileImage, string storageFolder, string? existingImageUrl = null);
    }
}
