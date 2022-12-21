using _0_Framework.Domain;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Domain.ArticleAgg
{
    public class Article : EntityBase
    {
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string PicturePath { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }
        public long ArticleCategoryId { get; private set; }
        public ArticleCategory ArticleCategory { get; private set; }

        public Article(string title, string slug, DateTime publishDate, string picturePath,
            string pictureAlt, string pictureTitle, string shortDescription, string description,
            string keywords, string metaDescription, string canonicalAddress, long articleCategoryId)
        {
            Title = title;
            Slug = slug;
            PublishDate = publishDate;
            PicturePath = picturePath;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ShortDescription = shortDescription;
            Description = description;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            ArticleCategoryId = articleCategoryId;
        }

        public void Edit(string title, string slug, DateTime publishDate, string picturePath,
            string pictureAlt, string pictureTitle, string shortDescription, string description,
            string keywords, string metaDescription, string canonicalAddress, long articleCategoryId)
        {
            Title = title;
            Slug = slug;
            PublishDate = publishDate;
            if (!string.IsNullOrWhiteSpace(picturePath))
                PicturePath = picturePath;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ShortDescription = shortDescription;
            Description = description;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            ArticleCategoryId = articleCategoryId;
        }
    }
}
