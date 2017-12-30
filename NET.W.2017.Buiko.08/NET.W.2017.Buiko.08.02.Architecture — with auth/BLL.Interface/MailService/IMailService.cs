using System.Threading.Tasks;

namespace BLL.Interface.MailService
{
    public interface IMailService
    {
        void SendMail(MailData mailData);

        Task SendMailAsync(MailData mailData);
    }
}
