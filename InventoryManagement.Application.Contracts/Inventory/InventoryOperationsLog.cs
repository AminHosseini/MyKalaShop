namespace InventoryManagement.Application.Contracts.Inventory
{
    public class InventoryOperationsLog
    {
        public long Id { get; set; }
        public int Count { get; set; }
        public string CreationDate { get; set; }
        public int CurrentCount { get; set; }
        public long OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string Description { get; set; }
        public bool Added { get; set; }
    }
}
