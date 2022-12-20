namespace InventoryManagement.Application.Contracts.Inventory
{
    public class InventoryViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public string Color { get; set; }
        public bool IsAvailable { get; set; }
        public int CurrentCount { get; set; }
        public string CreationDate { get; set; }
    }
}
