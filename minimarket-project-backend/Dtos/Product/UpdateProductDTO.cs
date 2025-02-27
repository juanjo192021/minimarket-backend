namespace minimarket_project_backend.Dtos.Product
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? RutaImagen { get; set; }

        public int Stock { get; set; }

        public decimal Precio { get; set; }

        public bool Estado { get; set; }

        public int IdMarca { get; set; }

        public int IdCategoria { get; set; }
    }
}
