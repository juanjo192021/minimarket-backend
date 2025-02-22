using Firebase.Auth.Providers;
using Firebase.Auth;
using Firebase.Storage;

namespace minimarket_project_backend.Services.Implementation
{
    public class FirebaseStorageService: IFirebaseStorageService
    {
        private readonly FirebaseAuthClient _authClient;
        private readonly string _storageBucket;

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

        public async Task<string> UploadFileAsync(Stream archivo, string nombre)
        {
            string urlImagen = string.Empty;

            string email = "juanjo101619@gmail.com";
            string clave = "precioUNO123*";

            try
            {
                // Autenticación
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, clave);
                string idToken = await userCredential.User.GetIdTokenAsync();

                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    _storageBucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(idToken),
                        ThrowOnCancel = true
                    })
                    .Child("MINIMARKET")
                    .Child(nombre)
                    .PutAsync(archivo, cancellation.Token);

                urlImagen = await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al subir la imagen: {ex.Message}");
                urlImagen = string.Empty;
            }

            return urlImagen;
        }
    }
}
