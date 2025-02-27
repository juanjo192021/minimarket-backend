using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class Region
{
    public int Id { get; set; }

    public string RegionCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Province> Provinces { get; set; } = new List<Province>();
}
