using System;
using System.Collections.Generic;

namespace WebApplication1.DatabaseProvider;

public partial class NrInventory
{
    public int Id { get; set; }

    public string Serial { get; set; } = null!;

    public int? CostCenterId { get; set; }

    public string? Name { get; set; }

    public string? Rfid { get; set; }

    public virtual CostCenter? CostCenter { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<NrSubInventory> NrSubInventories { get; set; } = new List<NrSubInventory>();
}
