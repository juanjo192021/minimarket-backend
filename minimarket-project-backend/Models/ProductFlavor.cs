using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class ProductFlavor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
