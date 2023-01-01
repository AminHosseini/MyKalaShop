namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountService
    {
        (string mobile, DateTime codeGenerationTime, int code) Get();
        void Set(string mobile, DateTime codeGenerationTime, int code);
    }
}
