using System.Threading.Tasks;

namespace ReTwitter.Services.External.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
