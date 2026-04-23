using System;
using System.Collections.Generic;

namespace WebApplication1.DatabaseProvider;

public partial class CostCenter
{
    public int Id { get; set; }

    public string Center { get; set; } = null!;

    public virtual ICollection<NrInventory> NrInventories { get; set; } = new List<NrInventory>();
}
