using Microsoft.EntityFrameworkCore;
using WebApplication1.DatabaseProvider;
using WebApplication1.DTO;

namespace WebApplication1.Services
{
    public class NrInventoryService
    {
        private readonly InventoryRfidContext _context;

        public NrInventoryService(InventoryRfidContext context)
        {
            _context = context;
        }

        public List<NrInventoryDTO> GetAll()
        {
            return _context.NrInventories
                .Include(a => a.NrSubInventories)
                .Select(x => new NrInventoryDTO
                {
                    Id = x.Id,
                    Serial = x.Serial,
                    Name = x.Name,
                    CostCenterId = x.CostCenterId,
                    CostCenterName = x.CostCenter.Center,
                    SubInventory = x.NrSubInventories.Select(a => new NrSubInventoryDTO()
                    {
                        Id = a.Id,
                        Name = a.Name
                    }).ToList()
                })
                .ToList();
        }

          
        public NrInventoryDTO GetById(int id)
        {
            var entity = _context.NrInventories
                .Include(x => x.NrSubInventories)
                .FirstOrDefault(x => x.Id == id);

            if (entity == null) return null;

            return new NrInventoryDTO
            {
                Id = entity.Id,
                Serial = entity.Serial,
                Name = entity.Name,
                CostCenterId = entity.CostCenterId,

                SubInventory = entity.NrSubInventories
                    .Select(x => new NrSubInventoryDTO
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList()
            };
        }

        public void Add(NrInventoryDTO dto)
        {
            var entity = new NrInventory
            {
                Serial = dto.Serial,
                Name = dto.Name,
                CostCenterId = dto.CostCenterId,
            };

            foreach (var subinv in dto.SubInventory)
            {
                var subinvEntity = new NrSubInventory
                {
                    Name = subinv.Name
                };

                entity.NrSubInventories.Add(subinvEntity);
            }

            _context.NrInventories.Add(entity);
            _context.SaveChanges();
        }


        //nou
        public void Update(NrInventoryDTO model)
        {
            var entity = _context.NrInventories.Include(a => a.NrSubInventories).FirstOrDefault(x => x.Id == model.Id);

            if (entity == null) return;

            entity.Serial = model.Serial;
            entity.Name = model.Name;
            entity.CostCenterId = model.CostCenterId;

            // Ștergere vechi
            var existing = _context.NrSubInventories
                .Where(x => x.NrInventory.Id == model.Id)
                .ToList();





            _context.NrSubInventories.RemoveRange(existing);

            // Adaugare noi
            foreach (var sub in model.SubInventory)
            {
                _context.NrSubInventories.Add(new NrSubInventory
                {
                    Name = sub.Name,
                    NrInventoryId = model.Id
                });
            }

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.NrInventories.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                _context.NrInventories.Remove(item);
                _context.SaveChanges();
            }
        }




        internal void AssignRFIDToInventory(int id, string rFID)
        {
            var NrInventory = _context.NrInventories.Find(id);
            if(NrInventory != default)
            {
                NrInventory.Rfid = rFID;
                _context.SaveChanges();
            }
        }
    }
}