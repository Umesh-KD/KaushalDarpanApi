using DocumentFormat.OpenXml.Wordprocessing;
using Kaushal_Darpan.Core.Helper;
using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace Kaushal_Darpan.Api.Email
{
    public interface IEmailService
    {
        Task SendEmail(string Body, string ToEmail, string subject);
        Task SendEmail(string Body, string ToEmail, string subject, string filepath);
    }
    public class EmailService : IEmailService
    {
        public string SMTPServerUrl;
        public int SMTPServerPort;
        public string SMTPServerEmail;
        public string SMTPServerLoginName;  
        public string SMTPServerPassword;
        public bool IsLiveServer;
        public bool EnableSsl;
        public bool UseDefaultCredentials;



        public EmailService()
        {
            SMTPServerUrl = ConfigurationHelper.SMTPHost;
            SMTPServerPort = ConfigurationHelper.SMTPPort;
            SMTPServerEmail = ConfigurationHelper.SMTPEmail;
            SMTPServerLoginName = ConfigurationHelper.SMTPUsername;
            SMTPServerPassword = ConfigurationHelper.SMTPPassword;
            IsLiveServer = ConfigurationHelper.IsLiveServer;
            EnableSsl = ConfigurationHelper.EnableSsl;
            UseDefaultCredentials = ConfigurationHelper.UseDefaultCredentials;
        }

        public async Task SendEmail(string Body, string ToEmail, string subject)
        {
            try
            {
                if (IsLiveServer)
                {
                    var sMTPServerUrl = SMTPServerUrl;
                    int sMTPServerPort = SMTPServerPort;
                    var username = SMTPServerLoginName;
                    var email = SMTPServerEmail;
                    string password = SMTPServerPassword;
                    MailMessage msg = new MailMessage();
                    msg.Subject = subject;
                    msg.From = new MailAddress(email, username);
                    msg.To.Add(new MailAddress(ToEmail));
                    msg.IsBodyHtml = true;
                    msg.Body = Body;

                    //System.Net.Mail.Attachment attachment;
                    //attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
                    //msg.Attachments.Add(attachment);

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = sMTPServerUrl;
                    smtpClient.Port = sMTPServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
                    await smtpClient.SendMailAsync(msg);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public async Task SendEmail(string Body, string ToEmail, string subject, string filepath)
        {
            try
            {
                if (IsLiveServer)
                {
                    var sMTPServerUrl = SMTPServerUrl;
                    int sMTPServerPort = SMTPServerPort;
                    var username = SMTPServerLoginName;
                    var email = SMTPServerEmail;
                    string password = SMTPServerPassword;
                    MailMessage msg = new MailMessage();
                    msg.Subject = subject;
                    msg.From = new MailAddress(email, username);
                    msg.To.Add(new MailAddress(ToEmail));
                    msg.IsBodyHtml = true;
                    msg.Body = Body;

                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(filepath);
                    msg.Attachments.Add(attachment);

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = sMTPServerUrl;
                    smtpClient.Port = sMTPServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
                    await smtpClient.SendMailAsync(msg);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }


}
