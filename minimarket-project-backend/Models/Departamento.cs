using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tienda_project_backend.Models;

public partial class Departamento
{
    public string Id { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Provincia> Provincias { get; set; } = new List<Provincia>();
}
