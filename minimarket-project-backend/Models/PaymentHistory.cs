using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class PaymentHistory
{
    public int Id { get; set; }

    public int SaleId { get; set; }

    public DateTime PaymentDate { get; set; }

    public int PaymentMethodId { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}
