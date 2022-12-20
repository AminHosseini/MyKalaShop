namespace InventoryManagement.Application.Contracts.Inventory
{
    public class SearchInventory
    {
        public long ProductId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
