namespace tienda_project_backend.Dtos.Marca
{
    public class UpdateMarcaDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? RutaImagen { get; set; }

        public bool Estado { get; set; }

    }
}
