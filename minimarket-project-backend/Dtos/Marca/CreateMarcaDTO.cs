namespace tienda_project_backend.Dtos.Marca
{
    public class CreateMarcaDTO
    {
        public string Nombre { get; set; } = null!;

        public string? RutaImagen { get; set; }

        public bool Estado { get; set; }
    }
}
