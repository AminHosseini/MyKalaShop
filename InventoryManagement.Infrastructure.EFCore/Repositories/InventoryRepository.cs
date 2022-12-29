using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Data;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore.Data;

namespace InventoryManagement.Infrastructure.EFCore.Repositories
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext,
            AccountContext accountContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public EditInventory GetDetails(long id)
        {
            return _context.Inventory.Select(x => new EditInventory()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryOperationsLog> GetLog(long inventoryId)
        {
            var inventory = _context.Inventory
                .Include(x => x.Operations).FirstOrDefault(x => x.Id == inventoryId);

            var operations = inventory.Operations.Select(x => new InventoryOperationsLog()
            {
                Id = x.Id,
                Count = x.Count,
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                Added = x.Added,
                OperatorId = x.OperatorId,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();

            foreach (var operation in operations)
            {
                operation.OperatorName = _accountContext.Accounts.FirstOrDefault(x => x.Id == operation.OperatorId)?.FullName;
            }

            return operations;
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
