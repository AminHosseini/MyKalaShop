using _0_Framework.Domain;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount : EntityBase
    {
        public int DiscountRate { get; private set; }
        public string Reason { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public long ProductId { get; private set; }

        public CustomerDiscount(int discountRate, string reason, DateTime startDate, DateTime endDate, long productId)
        {
            DiscountRate = discountRate;
            Reason = reason;
            StartDate = startDate;
            EndDate = endDate;
            ProductId = productId;
        }

        public void Edit(int discountRate, string reason, DateTime startDate, DateTime endDate, long productId)
        {
            DiscountRate = discountRate;
            Reason = reason;
            StartDate = startDate;
            EndDate = endDate;
            ProductId = productId;
        }
    }
}
