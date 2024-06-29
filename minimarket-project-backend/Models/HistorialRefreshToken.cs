using System;
using System.Collections.Generic;

namespace tienda_project_backend.Models;

public partial class HistorialRefreshToken
{
    public int Id { get; set; }

    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaExpiracion { get; set; }

    public int IdUsuario { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
