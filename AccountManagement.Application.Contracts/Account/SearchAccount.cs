namespace AccountManagement.Application.Contracts.Account
{
    public class SearchAccount
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Mobile { get; set; }
        public long RoleId { get; set; }
    }
}
