using System;
using System.Collections.Generic;

namespace WebApplication1.DatabaseProvider;

public partial class ScanRfid
{
    public int Id { get; set; }

    public int InventoryId { get; set; }

    public int? PlacementId { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual Placement? Placement { get; set; }
}
