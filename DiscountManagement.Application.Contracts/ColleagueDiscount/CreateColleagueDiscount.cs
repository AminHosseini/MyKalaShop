using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class CreateColleagueDiscount
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int DiscountRate { get; set; }
    }
}
