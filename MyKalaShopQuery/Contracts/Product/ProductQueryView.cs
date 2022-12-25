namespace MyKalaShopQuery.Contracts.Product
{
    public class ProductQueryView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string PicturePath { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }

        public long ProductCategoryId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductCategorySlug { get; set; }

        public double UnitPrice { get; set; }
        public bool IsAvailable { get; set; }

        public int DiscountRate { get; set; }
        public string DiscountEndDate { get; set; }

        public double PriceWithDiscount { get; set; }
        public bool HasDiscount { get; set; }
    }
}
