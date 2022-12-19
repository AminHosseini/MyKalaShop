using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Code { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.FileSizeOverExceeded)]
        [FileExtentionLimitation(new string[] { ".png", ".jpg", ".jpeg", ".PNG", ".JPG", ".JPEG" }, ErrorMessage = ValidationMessage.WrongFileFormat)]
        public IFormFile? PicturePath { get; set; }

        public string? PictureAlt { get; set; }
        public string? PictureTitle { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }

        public string? Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

        public string? MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductCategoryId { get; set; }
    }
}
