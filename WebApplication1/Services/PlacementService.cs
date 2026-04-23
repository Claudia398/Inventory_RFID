using WebApplication1.DatabaseProvider;
using WebApplication1.DTO;


namespace WebApplication1.Services
{
    public class PlacementService
    {
        private readonly InventoryRfidContext _context;

        public PlacementService(InventoryRfidContext context)
        {
            _context = context;
        }

        public List<PlacementDTO> GetAll()
        {
            return _context.Placements
                .Select(p => new PlacementDTO
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();
        }


        public PlacementDTO? GetById(int id)
        {
            return _context.Placements
                .Select(p => new PlacementDTO
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .FirstOrDefault(p => p.Id == id);
        }

        public void Add(PlacementDTO dto)
        {
            var entity = new Placement
            {
                Name = dto.Name
            };

            _context.Placements.Add(entity);
            _context.SaveChanges();
        }

        public void Update(PlacementDTO dto)
        {
            var entity = _context.Placements.FirstOrDefault(x => x.Id == dto.Id);

            if (entity != null)
            {
                entity.Name = dto.Name;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var item = _context.Placements.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Placements.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}