using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class InventoryOperation : EntityBase
    {
        public int Count { get; private set; }
        public bool Added { get; private set; }
        public int CurrentCount { get; private set; }
        public long OperatorId { get; private set; }
        public string Description { get; private set; }
        public long OrderId { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }

        public InventoryOperation(bool added, int count, int currentCount, long operatorId, string description, long orderId, long inventoryId)
        {
            Added = added;
            Count = count;
            CurrentCount = currentCount;
            OperatorId = operatorId;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;
        }
    }
}
