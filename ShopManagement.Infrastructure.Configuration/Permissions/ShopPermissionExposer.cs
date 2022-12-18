using _0_Framework.Infrastructure;

namespace ShopManagement.Infrastructure.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "فروشگاه", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.AccessToShopManagement, "دسترسی_به_سیستم_مدیریت_فروشگاه")
                    }
                },
                {
                    "گروه_محصولی", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProductCategories, "لیست_گروه_محصولی"),
                        new PermissionDto(ShopPermissions.SearchProductCategories, "جستوجو_گروه_محصولی"),
                        new PermissionDto(ShopPermissions.CreateProductCategory, "ساخت_گروه_محصولی"),
                        new PermissionDto(ShopPermissions.EditProductCategory, "ویرایش_گروه_محصولی")
                    }
                },
            };
        }
    }
}
