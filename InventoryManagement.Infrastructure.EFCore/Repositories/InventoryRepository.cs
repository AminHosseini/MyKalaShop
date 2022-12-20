using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Infrastructure.EFCore.Data;

namespace InventoryManagement.Infrastructure.EFCore.Repositories
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditInventory GetDetails(long id)
        {
            return _context.Inventory.Select(x => new EditInventory()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                Color = x.Color
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryOperationsLog> GetLog(long inventoryId)
        {
            var inventory = _context.Inventory
                .Include(x => x.Operations).FirstOrDefault(x => x.Id == inventoryId);

            return inventory.Operations.Select(x => new InventoryOperationsLog()
            {
                Id = x.Id,
                Count = x.Count,
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                Added = x.Added,
                OperatorId = x.OperatorId,
                OperatorName = "فعلا خالی",
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();
        }

        public List<InventoryViewModel> Search(SearchInventory model)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.Inventory.Select(x => new InventoryViewModel()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                IsAvailable = x.IsAvailable,
                Color = x.Color,
                CreationDate = x.CreationDate.ToFarsi(),
                CurrentCount = x.CalculateCurrentCount()
            });

            if (model.ProductId != 0)
                query = query.Where(x => x.ProductId == model.ProductId);

            if (model.IsAvailable)
                query = query.Where(x => !x.IsAvailable);

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            inventory.ForEach(item =>
                item.ProductName = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return inventory;
        }
    }
}
