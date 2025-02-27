using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class SaleDetail
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal HistoricalUnitPrice { get; set; }

    public decimal? TotalPrice { get; set; }

    public int SaleId { get; set; }

    public int ProductVariantId { get; set; }

    public virtual ProductVariant ProductVariant { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}
