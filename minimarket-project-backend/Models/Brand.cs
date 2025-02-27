using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? BrandImageUrl { get; set; }

    public bool Status { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
