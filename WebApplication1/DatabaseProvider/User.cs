using System;
using System.Collections.Generic;

namespace WebApplication1.DatabaseProvider;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public int? RoleId { get; set; }

    public virtual ICollection<Inventory> InventoryCreatedBies { get; set; } = new List<Inventory>();

    public virtual ICollection<Inventory> InventoryUsers { get; set; } = new List<Inventory>();

    public virtual Role? Role { get; set; }
}
