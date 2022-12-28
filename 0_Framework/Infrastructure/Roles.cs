namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Administrator = "1";
        public const string SystemUser = "2";

        public static string GetRoleBy(long roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "مدیر سیسنم";
                case 2:
                    return "کاربر سیسنم";
                default:
                    return "";
            }
        }
    }
}
