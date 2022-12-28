using MyKalaShopQuery.Contracts.Comment;
using MyKalaShopQuery.Contracts.ProductPicture;

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
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }

        public long ProductCategoryId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductCategorySlug { get; set; }

        public double UnitPrice { get; set; }
        public bool IsAvailable { get; set; }

        public int DiscountRate { get; set; }
        public string DiscountEndDate { get; set; }

        public double PriceWithDiscount { get; set; }
        public bool HasDiscount { get; set; }

        public List<ProductPictureQueryView> ProductPictures { get; set; }
        public List<CommentQueryView> ProductComments { get; set; }
    }
}
