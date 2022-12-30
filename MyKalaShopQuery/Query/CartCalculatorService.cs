using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Infrastructure.EFCore.Data;
using MyKalaShopQuery.Contracts;
using ShopManagement.Application.Contracts.Order;

namespace MyKalaShopQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;

        public CartCalculatorService(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsDeleted)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            foreach (var item in cartItems)
            {
                if (_authHelper.CurrentAccountRole() == Roles.Administrator)
                {
                    var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
                    if (colleagueDiscount != null)
                        item.DiscountRate = colleagueDiscount.DiscountRate;
                }
                else
                {
                    var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
                    if (customerDiscount != null)
                        item.DiscountRate = customerDiscount.DiscountRate;
                }

                var priceTimesCount = item.UnitPrice * item.Count;
                item.DiscountAmount = Math.Round(item.UnitPrice * item.DiscountRate) / 100;
                item.ItemPayAmount = priceTimesCount - item.DiscountAmount;

                cart.Add(item);
            }

            return cart;
        }
    }
}
