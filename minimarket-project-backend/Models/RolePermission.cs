using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class RolePermission
{
    public int Id { get; set; }

    public int IdRole { get; set; }

    public string Entity { get; set; } = null!;

    public bool CanCreate { get; set; }

    public bool CanRead { get; set; }

    public bool CanUpdate { get; set; }

    public bool CanDelete { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;
}
