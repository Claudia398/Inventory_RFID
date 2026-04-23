namespace WebApplication1.DTO
{
    public class NrInventoryDTO
    {
        public int Id { get; set; }

        public string Serial { get; set; }

        public string Name { get; set; }

        public int? CostCenterId { get; set; }

        public string CostCenterName { get; set; }


        public List<NrSubInventoryDTO> SubInventory { get; set; } = new();
    }
}