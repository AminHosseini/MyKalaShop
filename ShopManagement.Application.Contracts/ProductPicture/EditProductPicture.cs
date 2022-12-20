using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.ProductPicture
{
    public class EditProductPicture
    {
        public long Id { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.FileSizeOverExceeded)]
        [FileExtentionLimitation(new string[] { ".png", ".jpg", ".jpeg", ".PNG", ".JPG", ".JPEG" }, ErrorMessage = ValidationMessage.WrongFileFormat)]
        public IFormFile? PicturePath { get; set; }

        public string? PictureAlt { get; set; }
        public string? PictureTitle { get; set; }
    }
}
