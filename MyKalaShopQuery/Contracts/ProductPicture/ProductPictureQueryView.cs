namespace MyKalaShopQuery.Contracts.ProductPicture
{
    public class ProductPictureQueryView
    {
        public long ProductId { get; set; }
        public string PicturePath { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
    }
}
