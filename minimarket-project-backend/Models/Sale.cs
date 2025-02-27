using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class Sale
{
    public int Id { get; set; }

    public int SystemUserId { get; set; }

    public string TransactionId { get; set; } = null!;

    public string InvoiceType { get; set; } = null!;

    public string InvoiceNumber { get; set; } = null!;

    public int DistrictId { get; set; }

    public string Address { get; set; } = null!;

    public DateTime SaleDate { get; set; }

    public string Contact { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? LastUpdateDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? LastUpdatedBy { get; set; }

    public virtual SystemUser? CreatedByNavigation { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual SystemUser? LastUpdatedByNavigation { get; set; }

    public virtual ICollection<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    public virtual SystemUser SystemUser { get; set; } = null!;
}
