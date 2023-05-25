namespace AccountManagement.Application.Contracts.Account
{
    public interface ITokenService
    {
        string Get();
        void Set(string token);
    }
}
