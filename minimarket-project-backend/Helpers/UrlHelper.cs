namespace minimarket_project_backend.Helpers
{
    public class UrlHelper
    {
        public static string ExtractFileNameFromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return string.Empty;

            try
            {
                var uri = new Uri(url);

                // Extraer la parte después de "/o/"
                string[] parts = uri.AbsolutePath.Split(new[] { "/o/" }, StringSplitOptions.None);
                if (parts.Length < 2)
                    return string.Empty;

                // Decodificar la URL
                string filePath = System.Web.HttpUtility.UrlDecode(parts[1]);

                // Eliminar parámetros de la query string (por si acaso)
                return filePath.Split('?')[0];
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
