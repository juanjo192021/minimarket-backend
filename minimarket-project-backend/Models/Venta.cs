using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tienda_project_backend.Models;

public partial class Venta
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public string IdTransaccion { get; set; } = null!;

    public string TipoComprobante { get; set; } = null!;

    public string NumeroComprobante { get; set; } = null!;

    public string IdDistrito { get; set; } = null!;

    public string? Direccion { get; set; }

    public DateTime FechaVenta { get; set; }

    public string Contacto { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Celular { get; set; } = null!;

    public decimal MontoTotal { get; set; }

    [JsonIgnore]
    public virtual ICollection<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Distrito Distrito { get; set; } = null!;
}
