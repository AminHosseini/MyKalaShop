namespace AccountManagement.Domain.AccountAgg
{
    public class Permission
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public int Code { get; private set; }
        public long AccountId { get; private set; }
        public Account Account { get; private set; }

        public Permission(int code)
        {
            Code = code;
        }
    }
}
