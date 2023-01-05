using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.Inventory;

namespace InventoryManagement.Presentation.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryApiController : ControllerBase
    {
        private readonly IInventoryQuery _inventoryQuery;

        public InventoryApiController(IInventoryQuery inventoryQuery)
        {
            _inventoryQuery = inventoryQuery;
        }

        [HttpPost]
        public StockStatusApi GetProducts(IsInStockApi model)
        {
            return _inventoryQuery.CheckStock(model);
        }
    }
}
