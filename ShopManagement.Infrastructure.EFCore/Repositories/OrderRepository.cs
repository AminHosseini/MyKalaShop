using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Infrastructure.EFCore.Data;

namespace ShopManagement.Infrastructure.EFCore.Repositories
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;

        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public List<OrderItemViewModel> GetOrderItems(long orderId)
        {
            var order = _context.Orders.Include(x => x.OrderItems).FirstOrDefault(x => x.Id == orderId);
            var products = _context.Products.Select(x => new { x.Id, x.Name }).ToList();

            var orderItems = order.OrderItems.Select(x => new OrderItemViewModel()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                OrderId = x.OrderId,
                Count = x.Count,
                DiscountRate = x.DiscountRate,
                UnitPrice = x.UnitPrice
            }).ToList();

            foreach (var item in orderItems)
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId);

                if (product != null)
                    item.ProductName = product.Name;
            }

            return orderItems;
        }

        public List<OrderViewModel> Search(SearchOrders model)
        {
            var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.FullName }).ToList();

            var query = _context.Orders.Select(x => new OrderViewModel()
            {
                Id = x.Id,
                AccountId = x.AccountId,
                TotalPrice = x.TotalPrice,
                PayAmount = x.PayAmount,
                TotalDiscountAmount = x.TotalDiscountAmount,
                PaymentMethodId = x.PaymentMethod,
                RefId = x.RefId,
                IssueTrackingNo = x.IssueTrackingNo,
                IsConfirmed = x.IsConfirmed,
                IsCanceled = x.IsCanceled,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (model.PaymentMethod != 0)
                query = query.Where(x => x.PaymentMethodId == model.PaymentMethod);

            if (model.SearchCanceled)
                query = query.Where(x => x.IsCanceled);

            var orders = query.ToList();

            orders.ForEach(order =>
                order.AccountName = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.FullName);

            orders.ForEach(Order =>
                Order.PaymentMethod = PaymentMethod.GetPaymentMethodById(Order.PaymentMethodId)?.Name);

            return orders;
        }
    }
}
