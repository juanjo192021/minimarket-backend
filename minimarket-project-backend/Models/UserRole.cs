using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public int SystemUserId { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual SystemUser SystemUser { get; set; } = null!;
}
