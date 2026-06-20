using WebApplication1.DatabaseProvider;
using WebApplication1.DTO;

namespace WebApplication1.Services
{
    public class NrSubInventoryService
    {
        private readonly InventoryRfidContext _context;

        public NrSubInventoryService(InventoryRfidContext context)
        {
            _context = context;
        }

        public List<NrSubInventoryDTO> GetAll()
        {
            return _context.NrSubInventories
                .Select(x => new NrSubInventoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    //01.06.2026

            InventorySerial = x.NrInventory.Serial,
                    InventoryName = x.NrInventory.Name
                })
                .ToList();
        }

        public NrSubInventoryDTO? GetById(int id)
        {
            return _context.NrSubInventories
                .Select(x => new NrSubInventoryDTO
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public int Add(NrSubInventoryDTO dto)
        {
            var entity = new NrSubInventory
            {
                Name = dto.Name
            };

           var nrSubInv = _context.NrSubInventories.Add(entity);
            _context.SaveChanges();
            return nrSubInv.Entity.Id;
        }

        public void Update(NrSubInventoryDTO dto)
        {
            var entity = _context.NrSubInventories.FirstOrDefault(x => x.Id == dto.Id);

            if (entity != null)
            {
                entity.Name = dto.Name;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            // ✅ corect pentru 1-la-mulți
            var isUsed = _context.NrSubInventories
                .Any(x => x.Id == id);

            if (isUsed)
            {
                throw new Exception("Nu poți șterge acest SubInventory deoarece este folosit.");
            }

            var item = _context.NrSubInventories.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                _context.NrSubInventories.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}