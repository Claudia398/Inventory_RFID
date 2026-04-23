using WebApplication1.DatabaseProvider;
using WebApplication1.DTO;

namespace WebApplication1.Services
{
    public class CostCenterService
    {
        private readonly InventoryRfidContext _context;

        public CostCenterService(InventoryRfidContext context)
        {
            _context = context;
        }

        public List<CostCenterDTO> GetAll()
        {
            return _context.CostCenters
                .Select(c => new CostCenterDTO
                {
                    Id = c.Id,
                    Center = c.Center
                })
                .ToList();
        }

        public void Add(CostCenterDTO dto)
        {
            var entity = new CostCenter
            {
                Center = dto.Center
            };

            _context.CostCenters.Add(entity);
            _context.SaveChanges();
        }

        public CostCenterDTO? GetById(int id)
        {
            return _context.CostCenters
                .Select(c => new CostCenterDTO
                {
                    Id = c.Id,
                    Center = c.Center
                })
                .FirstOrDefault(c => c.Id == id);
        }

        public void Update(CostCenterDTO dto)
        {
            var entity = _context.CostCenters.FirstOrDefault(x => x.Id == dto.Id);

            if (entity != null)
            {
                entity.Center = dto.Center;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var item = _context.CostCenters.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.CostCenters.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
