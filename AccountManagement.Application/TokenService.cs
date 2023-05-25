using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Application
{
    public class TokenService : ITokenService
    {
        public string Token { get; set; } = string.Empty;

        public string Get()
        {
            return Token;
        }

        public void Set(string token)
        {
            Token = token;
        }
    }
}
