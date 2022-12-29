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
                    "مدیریت سیستم", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.AccessToAdminDashboard, "دسترسی به داشبورد مدیریت سیستم")
                    }
                },
                {
                    "مدیریت سیستم فروشگاه", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.AccessToShopManagement, "دسترسی به منو سیستم مدیریت فروشگاه")
                    }
                },
                {
                    "گروه محصولات", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.AccessToProductCategories, "دسترسی به منو گروه محصولات"),
                        new PermissionDto(ShopPermissions.ListProductCategories, "لیست کردن گروه محصولات"),
                        new PermissionDto(ShopPermissions.SearchProductCategories, "جستوجو در گروه محصولات"),
                        new PermissionDto(ShopPermissions.CreateProductCategory, "ایجاد گروه محصولی"),
                        new PermissionDto(ShopPermissions.EditProductCategory, "ویرایش گروه محصولی")
                    }
                },
                {
                    "محصولات", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.AccessToProducts, "دسترسی به منو محصولات"),
                        new PermissionDto(ShopPermissions.ListProducts, "لیست کردن محصولات"),
                        new PermissionDto(ShopPermissions.SearchProducts, "جستوجو در محصولات"),
                        new PermissionDto(ShopPermissions.CreateProduct, "ایجاد محصول"),
                        new PermissionDto(ShopPermissions.EditProduct, "ویرایش محصول")
                    }
                },
                {
                    "عکس محصولات", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.AccessToProductPictures, "دسترسی به منو عکس های محصولات"),
                        new PermissionDto(ShopPermissions.ListProductPictures, "لیست کردن عکس های محصولات"),
                        new PermissionDto(ShopPermissions.SearchProductPictures, "جستوجو در عکس های محصولات"),
                        new PermissionDto(ShopPermissions.CreateProductPicture, "ایجاد عکس محصول"),
                        new PermissionDto(ShopPermissions.EditProductPicture, "ویرایش عکس محصول"),
                        new PermissionDto(ShopPermissions.DeleteProductPicture, "حذف عکس محصول"),
                        new PermissionDto(ShopPermissions.RestoreProductPicture, "بازگردانی عکس محصول")
                    }
                },
                {
                    "اسلایدها", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.AccessToSlides, "دسترسی به منو اسلایدها"),
                        new PermissionDto(ShopPermissions.ListSlides, "لیست کردن اسلایدها"),
                        new PermissionDto(ShopPermissions.CreateSlide, "ایجاد اسلاید"),
                        new PermissionDto(ShopPermissions.EditSlide, "ویرایش اسلاید"),
                        new PermissionDto(ShopPermissions.DeleteSlide, "حذف اسلاید"),
                        new PermissionDto(ShopPermissions.RestoreSlide, "بازگردانی اسلاید")
                    }
                },
            };
        }
    }
}
