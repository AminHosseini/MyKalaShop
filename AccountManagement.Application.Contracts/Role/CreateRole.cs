using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Role
{
    public class CreateRole
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }
    }
}
