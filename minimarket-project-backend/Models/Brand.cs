using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace minimarket_project_backend.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? BrandImageUrl { get; set; }

    public bool Status { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
