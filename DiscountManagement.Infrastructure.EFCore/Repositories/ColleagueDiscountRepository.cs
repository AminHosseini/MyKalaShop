using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Data;
using InventoryManagement.Infrastructure.EFCore.Data;

namespace DiscountManagement.Infrastructure.EFCore.Repositories
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public ColleagueDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _context.ColleagueDiscounts.Select(x => new EditColleagueDiscount()
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(SearchColleagueDiscount model)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                IsDeleted = x.IsDeleted,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (model.ProductId != 0)
                query = query.Where(x => x.ProductId == model.ProductId);

            var colleagueDiscounts = query.OrderByDescending(x => x.Id).ToList();

            colleagueDiscounts.ForEach(discount =>
                discount.ProductName = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);

            return colleagueDiscounts;
        }
    }
}
