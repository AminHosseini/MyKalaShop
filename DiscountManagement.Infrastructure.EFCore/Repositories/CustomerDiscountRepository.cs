using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Data;
using InventoryManagement.Infrastructure.EFCore.Data;

namespace DiscountManagement.Infrastructure.EFCore.Repositories
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount()
            {
                Id = id,
                DiscountRate = x.DiscountRate,
                Reason = x.Reason,
                ProductId = x.ProductId,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi()
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(SearchCustomerDiscount model)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name });

            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel()
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                Reason = x.Reason,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                CreationDate = x.CreationDate.ToFarsi(),
                ProductId = x.ProductId,
                StartDateGr = x.StartDate,
                EndDateGr = x.EndDate
            });

            if (!string.IsNullOrWhiteSpace(model.StartDate))
                query = query.Where(x => x.StartDateGr <= model.StartDate.ToGeorgianDateTime());

            if (!string.IsNullOrWhiteSpace(model.EndDate))
                query = query.Where(x => x.EndDateGr >= model.EndDate.ToGeorgianDateTime());

            if (model.ProductId != 0)
                query = query.Where(x => x.ProductId == model.ProductId);

            var customerDiscounts = query.OrderByDescending(x => x.Id).ToList();

            customerDiscounts.ForEach(discount =>
                discount.ProductName = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);

            return customerDiscounts;
        }
    }
}
