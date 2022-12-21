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
                    "دسترسی_به_سیستم_کاربران", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermissions.AccessToAccountManagement, "دسترسی_به_سیستم_مدیریت_کاربران")
                    }
                },
                {
                    "کاربران", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermissions.ListAccounts, "لیست_کاربران"),
                        new PermissionDto(AccountPermissions.SearchAccounts, "جستوجو_کاربران"),
                        new PermissionDto(AccountPermissions.CreateAccount, "ایجاد_کاربر"),
                        new PermissionDto(AccountPermissions.EditAccount, "ویرایش_کاربر")
                    }
                },
            };
        }
    }
}
