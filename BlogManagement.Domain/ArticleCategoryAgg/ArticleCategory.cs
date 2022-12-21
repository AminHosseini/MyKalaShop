using _0_Framework.Domain;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public string PicturePath { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Description { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }
        public int DisplayOrder { get; private set; }

        public ArticleCategory(string name, string slug, string picturePath, string pictureAlt,
            string pictureTitle, string description, string keywords, string metaDescription,
            string canonicalAddress, int displayOrder)
        {
            Name = name;
            Slug = slug;
            PicturePath = picturePath;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Description = description;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            DisplayOrder = displayOrder;
        }

        public void Edit(string name, string slug, string picturePath, string pictureAlt,
            string pictureTitle, string description, string keywords, string metaDescription,
            string canonicalAddress, int displayOrder)
        {
            Name = name;
            Slug = slug;
            if (!string.IsNullOrWhiteSpace(picturePath))
                PicturePath = picturePath;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Description = description;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            DisplayOrder = displayOrder;
        }
    }
}
