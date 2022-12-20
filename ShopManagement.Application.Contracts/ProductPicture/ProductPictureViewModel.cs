namespace InventoryManagement.Application.Contracts.ProductPicture
{
    public class ProductPictureViewModel
    {
        public long Id { get; set; }
        public string PicturePath { get; set; }
        public string ProductName { get; set; }
        public string CreationDate { get; set; }
        public long ProductId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
