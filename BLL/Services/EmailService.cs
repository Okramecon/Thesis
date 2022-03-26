using System;
using System.Net;
using System.Net.Mail;
using Common.Exceptions;

namespace BLL.Services
{
    public class EmailService
    {
        public EmailService()
        {
        }

        public void SendEmailConfirmation(string token, string email)
        {
            string fromEmail = "ThesisTicketManager@gmail.com";
            MailMessage mailMessage = new(fromEmail, email, "Confirm your registration", $"<a href=\"thesis.uno/token/{token}\">Confirm</a>")
            {
                BodyEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = true,
            };

            SmtpClient smtpClient = new("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromEmail, "12qw!@QW");
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new InnerException($"Error sending email, exception {ex.Message}", "b6b71274-38c2-471a-8ec8-6e013ef6ea47");
            }
        }
    }
}
