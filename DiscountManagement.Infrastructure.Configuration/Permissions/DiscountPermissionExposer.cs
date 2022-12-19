using _0_Framework.Infrastructure;

namespace DiscountManagement.Infrastructure.Configuration.Permissions
{
    public class DiscountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "سیستم_تخفیفات", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.AccessToDiscountManagement, "دسترسی_به_سیستم_مدیریت_تخفیفات")
                    }
                },
                {
                    "تخفیفات_مشتری", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.ListCustomerDiscounts, "لیست_تخفیفات_مشتری"),
                        new PermissionDto(DiscountPermissions.SearchCustomerDiscounts, "جستوجو_تخفیفات_مشتری"),
                        new PermissionDto(DiscountPermissions.CreateCustomerDiscount, "ایجاد_تخفیف_مشتری"),
                        new PermissionDto(DiscountPermissions.EditCustomerDiscount, "ویرایش_تخفیف_مشتری")
                    }
                },
            };
        }
    }
}
