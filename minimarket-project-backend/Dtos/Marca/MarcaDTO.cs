namespace tienda_project_backend.Dtos.Marca
{
    public class MarcaDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? RutaImagen { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
