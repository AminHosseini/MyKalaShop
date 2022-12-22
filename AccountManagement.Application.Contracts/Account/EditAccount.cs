using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccount
    {
        public long Id { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string FullName { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Mobile { get; set; }

        public IFormFile? PicturePath { get; set; }
        public string? UserName { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public long RoleId { get; set; }
    }
}
