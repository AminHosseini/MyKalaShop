namespace AccountManagement.Infrastructure.Configuration.Permissions
{
    public static class AccountPermissions
    {
        public const int AccessToAccountManagement = 600;

        public const int AccessToAccounts = 610;
        public const int ListAccounts = 611;
        public const int SearchAccounts = 612;
        public const int CreateAccount = 613;
        public const int EditAccount = 614;
        public const int ChangeAccountPassword = 615;
        public const int SpecifyAccountPermissions = 616;

        public const int AccessToRoles = 620;
        public const int ListRoles = 621;
        public const int CreateRole = 622;
        public const int EditRole = 623;
    }
}
