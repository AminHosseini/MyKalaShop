using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class CreateInventory
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public double UnitPrice { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Color { get; set; }
    }
}
