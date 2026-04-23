using System;
using System.Collections.Generic;

namespace WebApplication1.DatabaseProvider;

public partial class NrSubInventory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int NrInventoryId { get; set; }

    public virtual NrInventory NrInventory { get; set; } = null!;
}
