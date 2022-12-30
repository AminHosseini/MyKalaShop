namespace ShopManagement.Application.Contracts.Order
{
    public class SearchOrders
    {
        public bool SearchCanceled { get; set; }
        public int PaymentMethod { get; set; }
    }
}
