using System;
using System.Collections.Generic;

namespace WebApplication1.DatabaseProvider;

public partial class Placement
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<ScanRfid> ScanRfids { get; set; } = new List<ScanRfid>();
}
