using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string FullName { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [DataType(DataType.Password)]
        [Compare("RePassword", ErrorMessage = ValidationMessage.PasswordsNotMatched)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = ValidationMessage.PasswordsNotMatched)]
        public string RePassword { get; set; }

        public IFormFile? PicturePath { get; set; }
        public string? UserName { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public long RoleId { get; set; }
    }
}
