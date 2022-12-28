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
                    "مدیریت سیستم تخفیفات", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.AccessToDiscountManagement, "دسترسی به منو سیستم مدیریت تخفیفات")
                    }
                },
                {
                    "تخفیفات مشتریان", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.AccessToCustomerDiscounts, "دسترسی به منو تخفیفات مشتریان"),
                        new PermissionDto(DiscountPermissions.ListCustomerDiscounts, "لیست کردن تخفیفات مشتریان"),
                        new PermissionDto(DiscountPermissions.SearchCustomerDiscounts, "جستوجو در تخفیفات مشتریان"),
                        new PermissionDto(DiscountPermissions.CreateCustomerDiscount, "ایجاد تخفیف مشتری"),
                        new PermissionDto(DiscountPermissions.EditCustomerDiscount, "ویرایش تخفیف مشتری")
                    }
                },
                {
                    "تخفیفات همکاران", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.AccessToColleagueDiscounts, "دسترسی به منو تخفیفات همکاران"),
                        new PermissionDto(DiscountPermissions.ListColleagueDiscounts, "لیست کردن تخفیفات همکاران"),
                        new PermissionDto(DiscountPermissions.SearchColleagueDiscounts, "جستوجو در تخفیفات همکاران"),
                        new PermissionDto(DiscountPermissions.CreateColleagueDiscount, "ایجاد تخفیف همکاری"),
                        new PermissionDto(DiscountPermissions.EditColleagueDiscount, "ویرایش تخفیف همکاری"),
                        new PermissionDto(DiscountPermissions.DeleteColleagueDiscount, "حذف تخفیف همکاری"),
                        new PermissionDto(DiscountPermissions.RestoreColleagueDiscount, "بازگردانی تخفیف همکاری")
                    }
                },
            };
        }
    }
}
