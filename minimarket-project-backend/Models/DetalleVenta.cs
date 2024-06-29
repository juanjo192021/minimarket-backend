using System;
using System.Collections.Generic;

namespace tienda_project_backend.Models;

public partial class DetalleVenta
{
    public int Id { get; set; }

    public int Cantidad { get; set; }

    public decimal Subtotal { get; set; }

    public decimal PrecioUnitarioHistorico { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Venta Venta { get; set; } = null!;
}
