using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class SystemUser
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DocumentType { get; set; } = null!;

    public string DocumentNumber { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string? Phone { get; set; }

    public string Mobile { get; set; } = null!;

    public string? Gender { get; set; }

    public string Address { get; set; } = null!;

    public bool Confirmed { get; set; }

    public bool Status { get; set; }

    public DateTime RegistrationDate { get; set; }

    public int UserTypeId { get; set; }

    public virtual ICollection<Product> ProductCreatedByNavigations { get; set; } = new List<Product>();

    public virtual ICollection<Product> ProductLastUpdatedByNavigations { get; set; } = new List<Product>();

    public virtual ICollection<ProductVariant> ProductVariantCreatedByNavigations { get; set; } = new List<ProductVariant>();

    public virtual ICollection<ProductVariant> ProductVariantLastUpdatedByNavigations { get; set; } = new List<ProductVariant>();

    public virtual ICollection<Sale> SaleCreatedByNavigations { get; set; } = new List<Sale>();

    public virtual ICollection<Sale> SaleLastUpdatedByNavigations { get; set; } = new List<Sale>();

    public virtual ICollection<Sale> SaleSystemUsers { get; set; } = new List<Sale>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public virtual UserType UserType { get; set; } = null!;
}
