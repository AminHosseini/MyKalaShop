using _0_Framework.Domain;
using InventoryManagement.Domain.ProductCategoryAgg;
using InventoryManagement.Domain.ProductPictureAgg;

namespace InventoryManagement.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string PicturePath { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Keywords { get; private set; }
        public string Slug { get; private set; }
        public string MetaDescription { get; private set; }
        public long ProductCategoryId { get; private set; }
        public ProductCategory ProductCategory { get; private set; }
        public List<ProductPicture> ProductPictures { get; private set; }

        public Product(string name, string code, string picturePath,
            string pictureAlt, string pictureTitle, string shortDescription,
            string description, string keywords, string slug, string metaDescription,
            long productCategoryId)
        {
            Name = name;
            Code = code;
            PicturePath = picturePath;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ShortDescription = shortDescription;
            Description = description;
            Keywords = keywords;
            Slug = slug;
            MetaDescription = metaDescription;
            ProductCategoryId = productCategoryId;
            ProductPictures = new List<ProductPicture>();
        }

        public void Edit(string name, string code, string picturePath,
            string pictureAlt, string pictureTitle, string shortDescription,
            string description, string keywords, string slug, string metaDescription,
            long productCategoryId)
        {
            Name = name;
            Code = code;
            if (!string.IsNullOrWhiteSpace(picturePath))
                PicturePath = picturePath;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ShortDescription = shortDescription;
            Description = description;
            Keywords = keywords;
            Slug = slug;
            MetaDescription = metaDescription;
            ProductCategoryId = productCategoryId;
        }
    }
}
