namespace _0_Framework.Application.Email
{
    public interface IEmailService
    {
        void SendEmail(string title, string emailTemplateName, string destinationName, string destinationAddress);
    }
}