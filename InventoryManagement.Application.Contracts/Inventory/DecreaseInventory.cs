namespace InventoryManagement.Application.Contracts.Inventory
{
    public class DecreaseInventory
    {
        public long Id { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }

        public DecreaseInventory()
        {
        }
        public DecreaseInventory(int count, string description, long orderId, long productId)
        {
            Count = count;
            Description = description;
            OrderId = orderId;
            ProductId = productId;
        }
    }
}
