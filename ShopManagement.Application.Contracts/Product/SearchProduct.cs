namespace InventoryManagement.Application.Contracts.Product
{
    public class SearchProduct
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long ProductCategoryId { get; set; }
    }
}
