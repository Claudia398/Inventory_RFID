namespace WebApplication1.DTO
{
    public class InventoryDTO
    {
        public int Id { get; set; }
        public string Uid { get; set; }

        public string ItemName { get; set; }
        public string PlacementName { get; set; }
        public string Username { get; set; }

        public DateTime? Updated { get; set; }
    }
}
