using System;

namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Administrator = "1";
        public const string SystemUser = "2";
        public const string ContentUploader = "3";
        public const string Colleague = "6";

        public static string GetRoleBy(long roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "مدیر سیسنم";
                case 3:
                    return "محتوا گذار";
                case 6:
                    return "کاربر همکار";
                default:
                    return "";
            }
        }
    }
}
