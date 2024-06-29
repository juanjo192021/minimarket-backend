using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tienda_project_backend.Models;

public partial class Provincia
{
    public string Id { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string IdDepartamento { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();

    public virtual Departamento Departamento { get; set; } = null!;
}
