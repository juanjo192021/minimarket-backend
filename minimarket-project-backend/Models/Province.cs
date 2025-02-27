using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class Province
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RegionId { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual Region Region { get; set; } = null!;
}
