namespace minimarket_project_backend.Dtos.Categoria
{
    public class CreateCategoriaDTO
    {
        public string Nombre { get; set; } = null!;

        public string? RutaImagen { get; set; }

        public bool Estado { get; set; }
    }
}
