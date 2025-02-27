namespace minimarket_project_backend.Dtos.Category
{
    public class CreateCategoryDTO
    {
        public string Nombre { get; set; } = null!;

        public string? RutaImagen { get; set; }

        public bool Estado { get; set; }
    }
}
