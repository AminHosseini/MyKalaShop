using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public int Code { get; set; }

        public string? Message { get; set; }
    }
}
