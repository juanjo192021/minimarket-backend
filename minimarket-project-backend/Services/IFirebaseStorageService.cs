namespace minimarket_project_backend.Services
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadFileAsync(string carpeta,Stream archivo, string nombre, string PrevImage);
        Task<bool> DeleteFileAsync(string nombre);
    }
}
