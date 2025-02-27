using Firebase.Auth.Providers;
using Firebase.Auth;
using Firebase.Storage;
using minimarket_project_backend.Helpers;

namespace minimarket_project_backend.Services.Implementation
{
    public class FirebaseStorageService: IFirebaseStorageService
    {
        private readonly FirebaseAuthClient _authClient;
        private readonly string _storageBucket;
        private const string Email = "juanjo101619@gmail.com";
        private const string Clave = "precioUNO123*";

        public FirebaseStorageService()
        {
            string apiKey = "AIzaSyCr3tren-CVdaBxGWxtlV3fyw-lN5LBRBU";
            string authDomain = "hiperbod-precio-uno-chorrillos.firebaseapp.com";
            _storageBucket = "hiperbod-precio-uno-chorrillos.appspot.com";

            var config = new FirebaseAuthConfig
            {
                ApiKey = apiKey,
                AuthDomain = authDomain,
                Providers = new FirebaseAuthProvider[]
                {
                new EmailProvider()
                }
            };

            _authClient = new FirebaseAuthClient(config);
        }

        private async Task<string> AuthenticateAsync()
        {
            var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(Email, Clave).ConfigureAwait(false);
            return await userCredential.User.GetIdTokenAsync().ConfigureAwait(false);
        }

        public async Task<string> UploadFileAsync(string carpeta, Stream archivo, string nombre, string? imagenAnterior)
        {
            string urlImagen = string.Empty;

            try
            {
                string idToken = await AuthenticateAsync();

                var cancellation = new CancellationTokenSource();

                // Crear la ruta dinámica desglosando la carpeta en múltiples niveles
                //var storageRef = new FirebaseStorage(_storageBucket, new FirebaseStorageOptions
                //{
                //    AuthTokenAsyncFactory = () => Task.FromResult(idToken),
                //    ThrowOnCancel = true
                //});

                //string[] niveles = carpeta.Split('/'); // Dividir la carpeta en niveles
                //foreach (var nivel in niveles)
                //{
                //    storageRef = storageRef.Child(nivel); // Agregar cada nivel como un Child
                //}

                //storageRef = storageRef.Child(nombre); // Agregar el nombre del archivo



                var task = new FirebaseStorage(
                    _storageBucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(idToken),
                        ThrowOnCancel = true
                    })
                    .Child("MINIMARKET")
                    .Child(carpeta)
                    .Child(nombre)
                    .PutAsync(archivo, cancellation.Token);

                urlImagen = await task;

                // Si se subió correctamente y hay imagen anterior, eliminarla
                if (!string.IsNullOrEmpty(urlImagen) && !string.IsNullOrEmpty(imagenAnterior))
                {
                    string fileName = UrlHelper.ExtractFileNameFromUrl(imagenAnterior);
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        await DeleteFileAsync(fileName).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al subir la imagen: {ex.Message}");
            }

            return urlImagen;
        }

        public async Task<bool> DeleteFileAsync(string fullpath)
        {
            try
            {
                string idToken = await AuthenticateAsync();

                var cancellation = new CancellationTokenSource();

                await new FirebaseStorage(
                    _storageBucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(idToken),
                        ThrowOnCancel = true
                    })
                    .Child(fullpath)
                    .DeleteAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al eliminar la imagen: {ex.Message}");
            }

            return false;
        }
    }
}
