using System.Text.Json.Serialization;

namespace tienda_project_backend.Dtos.Producto
{
    public class ProductoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? RutaImagen { get; set; }

        public int Stock { get; set; }

        public decimal Precio { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string Marca { get; set; } = null!;

        public string Categoria { get; set; } = null!;

        //public int IdMarca { get; set; }

        //public int IdCategoria { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();

        //public virtual Categoria Categoria { get; set; } = null!;

        //public virtual Marca Marca { get; set; } = null!;
    }
}
