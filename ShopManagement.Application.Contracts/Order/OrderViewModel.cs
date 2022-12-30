namespace ShopManagement.Application.Contracts.Order
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public double TotalPrice { get; set; }
        public int TotalDiscount { get; set; }
        public double TotalDiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; }
        public string IssueTrackingNo { get; set; }
        public long RefId { get; set; }
        public string CreationDate { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCanceled { get; set; }
    }
}
