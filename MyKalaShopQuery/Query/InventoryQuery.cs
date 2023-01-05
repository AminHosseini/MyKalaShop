using InventoryManagement.Infrastructure.EFCore.Data;
using MyKalaShopQuery.Contracts.Inventory;
using ShopManagement.Infrastructure.EFCore.Data;

namespace MyKalaShopQuery.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;

        public InventoryQuery(ShopContext shopContext, InventoryContext inventoryContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
        }

        public StockStatusApi CheckStock(IsInStockApi model)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == model.ProductId);

            if (inventory == null || inventory.CalculateCurrentCount() < model.Count)
            {
                var product = _shopContext.Products.Select(x => new { x.Id, x.Name }).FirstOrDefault(x => x.Id == model.ProductId);

                return new StockStatusApi()
                {
                    IsInStock = false,
                    ProductName = product.Name
                };
            }

            return new StockStatusApi()
            {
                IsInStock = true
            };
        }
    }
}
