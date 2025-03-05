﻿using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class RoleMenu
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int MenuId { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
