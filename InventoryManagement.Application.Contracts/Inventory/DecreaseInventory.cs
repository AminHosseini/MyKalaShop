namespace InventoryManagement.Application.Contracts.Inventory
{
    public class DecreaseInventory
    {
        public long Id { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
    }
}
