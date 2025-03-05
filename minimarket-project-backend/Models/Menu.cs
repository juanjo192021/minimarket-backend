using System;
using System.Collections.Generic;

namespace minimarket_project_backend.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int? ParentMenuId { get; set; }

    public virtual ICollection<Menu> InverseParentMenu { get; set; } = new List<Menu>();

    public virtual Menu? ParentMenu { get; set; }

    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
