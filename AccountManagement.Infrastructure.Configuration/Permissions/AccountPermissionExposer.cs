using _0_Framework.Infrastructure;

namespace AccountManagement.Infrastructure.Configuration.Permissions
{
    public class AccountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    " مدیریت سیستم کاربران", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermissions.AccessToAccountManagement, "دسترسی به منو سیستم کاربران")
                    }
                },
                {
                    "کاربران", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermissions.AccessToAccounts, "دسترسی به منو کاربران"),
                        new PermissionDto(AccountPermissions.ListAccounts, "دیدن لیست کاربران"),
                        new PermissionDto(AccountPermissions.SearchAccounts, "جستوجو در کاربران"),
                        new PermissionDto(AccountPermissions.CreateAccount, "ایجاد کاربر"),
                        new PermissionDto(AccountPermissions.EditAccount, "ویرایش کاربر"),
                        new PermissionDto(AccountPermissions.SpecifyAccountPermissions, "دادن دسترسی ها به کاربر")
                    }
                },
                {
                    "نقش ها", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermissions.AccessToRoles, "دسترسی به منو نقش ها"),
                        new PermissionDto(AccountPermissions.ListRoles, "لیست کردن نقش ها"),
                        new PermissionDto(AccountPermissions.CreateRole, "ایجاد نقش"),
                        new PermissionDto(AccountPermissions.EditRole, "ویرایش نقش")
                    }
                },
            };
        }
    }
}
