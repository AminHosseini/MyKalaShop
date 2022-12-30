using _0_Framework.Application;
using _0_Framework.Application.Sms;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _repository;
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IShopInventoryAcl _shopInventoryAcl;
        private readonly ISmsService _smsService;
        private readonly IShopAccountAcl _shopAccountAcl;

        public OrderApplication(IOrderRepository repository, IConfiguration configuration,
            IShopInventoryAcl shopInventoryAcl, IAuthHelper authHelper, ISmsService smsService, 
            IShopAccountAcl shopAccountAcl)
        {
            _repository = repository;
            _configuration = configuration;
            _shopInventoryAcl = shopInventoryAcl;
            _authHelper = authHelper;
            _smsService = smsService;
            _shopAccountAcl = shopAccountAcl;
        }

        public List<OrderItemViewModel> GetOrderItems(long orderId)
        {
            return _repository.GetOrderItems(orderId);
        }

        public long PlaceOrder(Cart cart)
        {
            var accountId = _authHelper.CurrentAccountId();
            var order = new Order(accountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount, cart.PaymentMethod);

            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DiscountRate);
                order.Add(orderItem);
            }

            _repository.Create(order);
            _repository.Save();

            return order.Id;
        }

        public List<OrderViewModel> Search(SearchOrders model)
        {
            return _repository.Search(model);
        }

        public double GetAmountById(long id)
        {
            var order = _repository.Get(id);
            if (order != null)
                return order.PayAmount;

            return 0;
        }

        public OperationResult PaymentSucceeded(long orderId, long refId)
        {
            var operationResult = new OperationResult();

            var order = _repository.Get(orderId);
            order.PaymentSucceeded(refId);

            if (!_shopInventoryAcl.RemoveFromInventory(order.OrderItems))
                return operationResult.Failed(ValidationMessage.OperationFailed);

            _repository.Save();

            var account = _shopAccountAcl.GetAccountInfo(order.AccountId);
            _smsService.Send(account.Mobile, $"خریدار محترم {account.FullName} عزیز. سفارش شما با شماره پیگیری {order.IssueTrackingNo} ثبت و به زودی ارسال خواهد شد.");

            return operationResult.Succeeded();
        }

        public string SetIssueTrackingNo(long orderId)
        {
            var order = _repository.Get(orderId);

            if (order != null)
            {
                var symbol = _configuration.GetSection("symbol").Value;
                var issueTrackingNo = CodeGenerator.Generate(symbol);
                order.SetIssueTrackingNo(issueTrackingNo);
                _repository.Save();
                return issueTrackingNo;
            }

            return "";
        }

        public void Cancel(long id)
        {
            var order = _repository.Get(id);
            order.Cancel();
            _repository.Save();
        }
    }
}
