using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class ProductVariant
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int? PresentationId { get; set; }

    public int? FlavorId { get; set; }

    public string Sku { get; set; } = null!;

    public string BarCode { get; set; } = null!;

    public string? Description { get; set; }

    public string? ProductVariantImageUrl { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int CreatedBy { get; set; }

    public int? LastUpdatedBy { get; set; }

    public virtual SystemUser CreatedByNavigation { get; set; } = null!;

    public virtual ProductFlavor? Flavor { get; set; }

    public virtual SystemUser? LastUpdatedByNavigation { get; set; }

    public virtual ProductPresentation? Presentation { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
