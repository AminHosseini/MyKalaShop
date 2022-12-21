using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contracts.Article
{
    public class CreateArticle
    {

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Title { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PublishDate { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.FileSizeOverExceeded)]
        [FileExtentionLimitation(new string[] { ".png", ".jpg", ".jpeg", ".PNG", ".JPG", ".JPEG" }, ErrorMessage = ValidationMessage.WrongFileFormat)]
        public IFormFile? PicturePath { get; set; }

        public string? PictureAlt { get; set; }
        public string? PictureTitle { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? Keywords { get; set; }
        public string? MetaDescription { get; set; }
        public string? CanonicalAddress { get; set; }
        public long ArticleCategoryId { get; set; }
    }
}
