using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ProductImageUrl { get; set; }

    public bool Status { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int CreatedBy { get; set; }

    public int? LastUpdatedBy { get; set; }

    public int BrandId { get; set; }

    public int CategoryId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual SystemUser CreatedByNavigation { get; set; } = null!;

    public virtual SystemUser? LastUpdatedByNavigation { get; set; }

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
