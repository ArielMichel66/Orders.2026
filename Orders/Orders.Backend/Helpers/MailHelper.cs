using Orders.Shared.Responses;
using System.Net.Mail;

namespace Orders.Backend.Helpers;

public class MailHelper : IMailHelper
{
    public ActionResponse<string> SendMail(string toName, string toEmail, string subject, string body)
    {
        try
        {
            using (SmtpClient SmtpServer = new SmtpClient("smtp.Gmail.com"))
            {
                MailMessage mail = new()
                {
                    From = new MailAddress("sipegoctes@gmail.com")
                };
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = body;
                mail.Body = htmlBody;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("sipegoctes@gmail.com", "kdxrwytuxbsvdmki");
                SmtpServer.EnableSsl = true;
                try
                {
                    SmtpServer.Send(mail);
                }
                catch (Exception ex)
                {
                    string erroremail = "ERROR: " + ex.Message;
                }
            }
            return new ActionResponse<string> { WasSuccess = true };
        }
        catch (Exception ex)
        {
            return new ActionResponse<string>
            {
                WasSuccess = false,
                Message = ex.Message,
            };
        }
    }
}