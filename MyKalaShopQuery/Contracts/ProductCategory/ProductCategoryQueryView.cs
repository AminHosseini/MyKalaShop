using MyKalaShopQuery.Contracts.Product;

namespace MyKalaShopQuery.Contracts.ProductCategory
{
    public class ProductCategoryQueryView
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string PicturePath { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
    }
}
