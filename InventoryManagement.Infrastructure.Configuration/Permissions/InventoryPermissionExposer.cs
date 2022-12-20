using _0_Framework.Infrastructure;

namespace InventoryManagement.Infrastructure.Configuration.Permissions
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "انبار", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermissions.AccessToInventoryManagement, "دسترسی_به_سیستم_انبارداری"),
                        new PermissionDto(InventoryPermissions.ListInventory, "لیست_انبار"),
                        new PermissionDto(InventoryPermissions.SearchInventory, "جستوجو_انبار"),
                        new PermissionDto(InventoryPermissions.CreateInventory, "ایجاد_انبار"),
                        new PermissionDto(InventoryPermissions.EditInventory, "ویرایش_انبار"),
                        new PermissionDto(InventoryPermissions.IncreaseInventory, "افزایش_انبار"),
                        new PermissionDto(InventoryPermissions.DecreaseInventory, "کاهش_انبار"),
                        new PermissionDto(InventoryPermissions.GetInventoryOperationsLog, "لاگ_عملیاتهای_انبار")
                    }
                },
            };
        }
    }
}
