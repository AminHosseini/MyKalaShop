using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Application
{
    public class AccountService : IAccountService
    {
        public string Mobile { get; set; }
        public DateTime CodeGenerationTime { get; set; }
        public int Code { get; set; }

        public (string mobile, DateTime codeGenerationTime, int code) Get()
        {
            return (Mobile, CodeGenerationTime, Code);
        }

        public void Set(string mobile, DateTime codeGenerationTime, int code)
        {
            Mobile = mobile;
            CodeGenerationTime = codeGenerationTime;
            Code = code;
        }
    }
}
