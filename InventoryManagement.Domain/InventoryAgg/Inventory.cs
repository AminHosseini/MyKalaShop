using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public int UnitPrice { get; private set; }
        public string Color { get; private set; }
        public bool IsAvailable { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }

        public Inventory(long productId, int unitPrice, string color)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            Color = color;
            IsAvailable = false;
            Operations = new List<InventoryOperation>();
        }

        public void Edit(long productId, int unitPrice, string color)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            Color = color;
        }

        public int CalculateCurrentCount()
        {
            var added = Operations.Where(x => x.Added).Sum(x => x.Count);
            var removed = Operations.Where(x => !x.Added).Sum(x => x.Count);
            return added - removed;
        }

        public void Increase(int count, long operatorId, string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InventoryOperation(true, count, currentCount, operatorId, description, 0, Id);
            Operations.Add(operation);
            IsAvailable = currentCount > 0;
        }

        public void Decrease(int count, long operatorId, string description, long orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(false, count, currentCount, operatorId, description, orderId, Id);
            Operations.Add(operation);
            IsAvailable = currentCount > 0;
        }
    }
}
