using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string FullName { get; private set; }
        public string UserName { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }
        public string PicturePath { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }
        public List<Permission> Permissions { get; private set; }

        protected Account()
        {
        }

        public Account(string fullname, string username, string mobile, string password, string picturePath, long roleId)
        {
            FullName = fullname;
            UserName = username;
            Mobile = mobile;
            Password = password;
            PicturePath = picturePath;
            RoleId = roleId;
            Permissions = new List<Permission>();
        }

        public void Edit(string fullname, string username, string mobile, string picturePath, long roleId)
        {
            FullName = fullname;
            UserName = username;
            Mobile = mobile;
            if (!string.IsNullOrWhiteSpace(picturePath))
                PicturePath = picturePath;
            RoleId = roleId;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void SpecifyPermissions(List<Permission> permissions)
        {
            Permissions = permissions;
        }
    }
}
