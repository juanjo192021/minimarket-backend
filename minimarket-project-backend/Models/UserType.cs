using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class UserType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SystemUser> SystemUsers { get; set; } = new List<SystemUser>();
}
