namespace tienda_project_backend.Dtos.Categoria
{
    public class CategoriaDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? RutaImagen { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
