using _0_Framework.Domain;
using InventoryManagement.Domain.ProductAgg;

namespace InventoryManagement.Domain.ProductPictureAgg
{
    public class ProductPicture : EntityBase
    {
        public bool IsDeleted { get; private set; }
        public string PicturePath { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public long ProductId { get; private set; }
        public Product Product { get; private set; }

        public ProductPicture(string picturePath, string pictureAlt, string pictureTitle, long productId)
        {
            IsDeleted = false;
            PicturePath = picturePath;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
        }

        public void Edit(string picturePath, string pictureAlt, string pictureTitle, long productId)
        {
            if (!string.IsNullOrWhiteSpace(picturePath))
                PicturePath = picturePath;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
