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
                    "انبارها", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermissions.AccessToInventoryManagement, "دسترسی به منو سیستم انبارداری"),
                        new PermissionDto(InventoryPermissions.ListInventory, "لیست کردن انبارها"),
                        new PermissionDto(InventoryPermissions.SearchInventory, "جستوجو در انبارها"),
                        new PermissionDto(InventoryPermissions.CreateInventory, "ایجاد انبار"),
                        new PermissionDto(InventoryPermissions.EditInventory, "ویرایش انبار"),
                        new PermissionDto(InventoryPermissions.IncreaseInventory, "افزایش تعداد در انبار"),
                        new PermissionDto(InventoryPermissions.DecreaseInventory, "کاهش تعداد در انبار"),
                        new PermissionDto(InventoryPermissions.GetInventoryOperationsLog, "گرفتن لاگ عملیات های انبار")
                    }
                },
            };
        }
    }
}
