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

        public List<InventoryDTO> GetAll()
        {
            return _context.Inventories
                .Select(x => new InventoryDTO
                {
                    Id = x.Id,
                    Uid = x.Uid,
                    ItemName = x.NrInventory.Name,
                    PlacementName = x.Placement.Name,
                    Username = x.User.Username,
                    Updated = x.Updated
                })
                .ToList();
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
    }
}