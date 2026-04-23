using System;
using System.Collections.Generic;

namespace WebApplication1.DatabaseProvider;

public partial class Inventory
{
    public int Id { get; set; }

    public string? Uid { get; set; }

    public int NrInventoryId { get; set; }

    public int? PlacementId { get; set; }

    public int? UserId { get; set; }

    public string? Comment { get; set; }

    public bool Active { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    public int CreatedById { get; set; }

    public virtual User CreatedBy { get; set; } = null!;

    public virtual NrInventory NrInventory { get; set; } = null!;

    public virtual Placement? Placement { get; set; }

    public virtual ICollection<ScanRfid> ScanRfids { get; set; } = new List<ScanRfid>();

    public virtual User? User { get; set; }
}
