namespace minimarket_project_backend.Services
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadFileAsync(Stream archivo, string nombre);
    }
}
