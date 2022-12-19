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
                        new PermissionDto(ShopPermissions.CreateProductCategory, "ایجاد_گروه_محصولی"),
                        new PermissionDto(ShopPermissions.EditProductCategory, "ویرایش_گروه_محصولی")
                    }
                },
                {
                    "محصولات", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProducts, "لیست_محصولات"),
                        new PermissionDto(ShopPermissions.SearchProducts, "جستوجو_محصولات"),
                        new PermissionDto(ShopPermissions.CreateProduct, "ایجاد_محصولات"),
                        new PermissionDto(ShopPermissions.EditProduct, "ویرایش_محصولات")
                    }
                },
                {
                    "عکس_محصولات", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProductPictures, "لیست_عکس_محصولات"),
                        new PermissionDto(ShopPermissions.SearchProductPictures, "جستوجو_عکس_محصولات"),
                        new PermissionDto(ShopPermissions.CreateProductPicture, "ایجاد_عکس_محصول"),
                        new PermissionDto(ShopPermissions.EditProductPicture, "ویرایش_عکس_محصول")
                    }
                },
                {
                    "اسلایدها", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListSlides, "لیست_اسلایدها"),
                        new PermissionDto(ShopPermissions.CreateSlide, "ایجاد_اسلاید"),
                        new PermissionDto(ShopPermissions.EditSlide, "ویرایش_اسلاید")
                    }
                },
            };
        }
    }
}
