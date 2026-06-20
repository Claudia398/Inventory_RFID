using Microsoft.EntityFrameworkCore;
using WebApplication1.DatabaseProvider;
using WebApplication1.DTO;
namespace WebApplication1.Services
{
    public class InventoryService
    {
        private readonly InventoryRfidContext _context;

        public InventoryService(InventoryRfidContext context)
        {
            _context = context;
        }

        //public List<InventoryDTO> GetAll()
        //{
        //    return _context.Inventories
        //        .Select(x => new InventoryDTO
        //        {
        //            Id = x.Id,
        //            Uid = x.Uid,
        //            ItemName = x.NrInventory.Name,
        //            PlacementName = x.Placement !=null? x.Placement.Name:"",
        //            Username = x.User != null ? x.User.Username : "",
        //            Updated = x.Updated
        //        })
        //        .ToList();
        //}


        //31.05.2026

        //public List<InventoryDTO> GetAll()
        //{
        //    var result = _context.NrInventories
        //        .Select(n => new InventoryDTO
        //        {
        //            Id = n.Id,
        //            Uid = n.Rfid,
        //            ItemName = n.Name,

        //            PlacementName = _context.Inventories
        //                .Where(i => i.NrInventoryId == n.Id)
        //                .OrderByDescending(i => i.Updated)
        //                .Select(i => i.Placement != null ? i.Placement.Name : "")
        //                .FirstOrDefault(),

        //            Username = _context.Inventories
        //                .Where(i => i.NrInventoryId == n.Id)
        //                .OrderByDescending(i => i.Updated)
        //                .Select(i => i.User != null ? i.User.Username : "")
        //                .FirstOrDefault(),

        //            Updated = _context.Inventories
        //                .Where(i => i.NrInventoryId == n.Id)
        //                .OrderByDescending(i => i.Updated)
        //                .Select(i => i.Updated)
        //                .FirstOrDefault()
        //        })
        //        .ToList();

        //    foreach (var item in result)
        //    {
        //        item.IsExpired =
        //            item.Updated == null ||
        //            item.Updated.Value.AddMonths(6) < DateTime.Now;

        //        item.Status =
        //            item.IsExpired
        //            ? "NOT SCANNED > 6 MONTHS"
        //            : "OK";
        //    }

        //    return result;
        //}
        //14.06.2026
        public List<InventoryDTO> GetAll()
        {
            var result = _context.NrInventories
                .Select(n => new InventoryDTO
                {
                    Id = n.Id,
                    Uid = n.Rfid,
                    ItemName = n.Name,

                    PlacementName = _context.Inventories
                        .Where(i => i.NrInventoryId == n.Id)
                        .OrderByDescending(i => i.Updated)
                        .Select(i => i.Placement != null ? i.Placement.Name : "")
                        .FirstOrDefault(),

                    Username = _context.Inventories
                        .Where(i => i.NrInventoryId == n.Id)
                        .OrderByDescending(i => i.Updated)
                        .Select(i => i.User != null ? i.User.Username : "")
                        .FirstOrDefault(),

                    Updated = _context.Inventories
                        .Where(i => i.NrInventoryId == n.Id)
                        .OrderByDescending(i => i.Updated)
                        .Select(i => i.Updated)
                        .FirstOrDefault(),
                })
                .ToList();

            foreach (var item in result)
            {
                item.IsExpired =
                    item.Updated == null ||
                    item.Updated.Value.AddMonths(6) < DateTime.Now;

                item.Status =
                    item.IsExpired
                    ? "NOT SCANNED > 6 MONTHS"
                    : "OK";
            }

            return result;
        }
        public List<InventoryDTO> GetByPlacement(int placementId)
        {
            var result = _context.NrInventories
                .Where(n => _context.Inventories.Any(i =>
                    i.NrInventoryId == n.Id &&
                    i.PlacementId == placementId))
                .Select(n => new InventoryDTO
                {
                    Id = n.Id,
                    Uid = n.Rfid,
                    ItemName = n.Name,

                    PlacementName = _context.Inventories
                        .Where(i => i.NrInventoryId == n.Id)
                        .OrderByDescending(i => i.Updated)
                        .Select(i => i.Placement != null ? i.Placement.Name : "")
                        .FirstOrDefault(),

                    Username = _context.Inventories
                        .Where(i => i.NrInventoryId == n.Id)
                        .OrderByDescending(i => i.Updated)
                        .Select(i => i.User != null ? i.User.Username : "")
                        .FirstOrDefault(),

                    Updated = _context.Inventories
                        .Where(i => i.NrInventoryId == n.Id)
                        .OrderByDescending(i => i.Updated)
                        .Select(i => i.Updated)
                        .FirstOrDefault()
                })
                .ToList();

            foreach (var item in result)
            {
                item.IsExpired =
                    item.Updated == null ||
                    item.Updated.Value.AddMonths(6) < DateTime.Now;

                item.Status =
                    item.IsExpired
                    ? "NOT SCANNED > 6 MONTHS"
                    : "OK";
            }

            return result;
        }


        public void UpdateScan(string uid, int placementId)
        {
            var item = _context.Inventories.FirstOrDefault(x => x.Uid == uid);

            if (item != null)
            {
                item.PlacementId = placementId;
                _context.SaveChanges();
            }
        }

        //nou 31.05.2026
        public void PerformInventory(string uid, int placementId, int? userid)
        {
            // 1. Caut RFID-ul
            var nrInventory = _context.NrInventories
                .FirstOrDefault(x => x.Rfid == uid);

            // Daca nu exista => IGNOR
            if (nrInventory == null)
            {
                return;
            }

            // 2. Caut inventory pentru acel obiect
            var inventory = _context.Inventories
                .FirstOrDefault(x => x.NrInventoryId == nrInventory.Id);

            // 3. Prima scanare => creez inventory
            if (inventory == null)
            {
                int newId = 1;

                if (_context.Inventories.Any())
                {
                    newId = _context.Inventories.Max(x => x.Id) + 1;
                }

                inventory = new Inventory
                {
                   
                    Uid = uid,
                    NrInventoryId = nrInventory.Id,
                    PlacementId = placementId,
                    Active = true,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    CreatedById = 1,
                    UserId = userid
                };

                _context.Inventories.Add(inventory);
                _context.SaveChanges();

                return;
            }

            // 4. Exista deja inventory
            bool olderThan24h =
                inventory.Updated == null ||
                inventory.Updated.Value.AddHours(24) < DateTime.Now;

            inventory.PlacementId = placementId;
            inventory.Updated = DateTime.Now;
            inventory.Uid = uid;
            inventory.UserId = userid;

            _context.SaveChanges();

            // 5. Daca au trecut 24h => istoric scanare
            if (olderThan24h)
            {
                _context.ScanRfids.Add(new ScanRfid
                {
                    InventoryId = inventory.Id,
                    PlacementId = placementId
                });

                _context.SaveChanges();
            }
        }
    }
}

    
