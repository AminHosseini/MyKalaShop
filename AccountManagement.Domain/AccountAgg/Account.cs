using _0_Framework.Domain;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string FullName { get; private set; }
        public string UserName { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }
        public string PicturePath { get; private set; }

        protected Account()
        {
        }

        public Account(string fullname, string username, string mobile, string password, string picturePath)
        {
            FullName = fullname;
            UserName = username;
            Mobile = mobile;
            Password = password;
            PicturePath = picturePath;
        }

        public void Edit(string fullname, string username, string mobile, string picturePath)
        {
            FullName = fullname;
            UserName = username;
            Mobile = mobile;
            if (!string.IsNullOrWhiteSpace(picturePath))
                PicturePath = picturePath;
        }

        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }
    }
}
