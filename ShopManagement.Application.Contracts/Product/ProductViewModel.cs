namespace InventoryManagement.Application.Contracts.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string PicturePath { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long ProductCategoryId { get; set; }
        public string ProductCategory { get; set; }
        public string CreationDate { get; set; }
    }
}
