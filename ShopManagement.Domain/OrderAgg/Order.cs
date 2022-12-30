using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public long AccountId { get; private set; }
        public double TotalPrice { get; private set; }
        public double TotalDiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public int PaymentMethod { get; private set; }
        public string IssueTrackingNo { get; private set; }
        public long RefId { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public Order(long accountId, double totalPrice, double totalDiscountAmount,
            double payAmount, int paymentMethod)
        {
            AccountId = accountId;
            TotalPrice = totalPrice;
            TotalDiscountAmount = totalDiscountAmount;
            PayAmount = payAmount;
            PaymentMethod = paymentMethod;
            RefId = 0;
            IsConfirmed = false;
            IsCanceled = false;
            OrderItems = new List<OrderItem>();
        }

        public void PaymentSucceeded(long refId)
        {
            IsConfirmed = true;
            IsCanceled = false;

            if (refId != 0)
                RefId = refId;
        }

        public void SetIssueTrackingNo(string issueTrackingNo)
        {
            IssueTrackingNo = issueTrackingNo;
        }

        public void Cancel()
        {
            IsCanceled = true;
            IsConfirmed = false;
        }

        public void Add(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
        }
    }
}
