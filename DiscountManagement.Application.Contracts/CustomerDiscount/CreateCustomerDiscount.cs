using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class CreateCustomerDiscount
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int DiscountRate { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string StartDate { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string EndDate { get; set; }

        public string? Reason { get; set; }
    }
}
