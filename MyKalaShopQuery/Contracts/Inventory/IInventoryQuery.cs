namespace MyKalaShopQuery.Contracts.Inventory
{
    public interface IInventoryQuery
    {
        StockStatusApi CheckStock(IsInStockApi model);
    }
}
