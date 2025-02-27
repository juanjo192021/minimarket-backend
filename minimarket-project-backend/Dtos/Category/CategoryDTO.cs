namespace minimarket_project_backend.Dtos.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? RutaImagen { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
