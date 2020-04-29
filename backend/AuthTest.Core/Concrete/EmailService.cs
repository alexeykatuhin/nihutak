using AuthTest.Core.Abstract;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AuthTest.Core.Concrete
{
    public class EmailService : IEmailService
    {
        private const string smtpServer = "smtp.mail.ru";
        private const int smtpPort = 587;
        private const string sendFrom = "alexeykatuhin@mail.ru";
        private const string password = "Head4372!";
    
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {

                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress(sendFrom, "nihutak");
                // кому отправляем
                MailAddress to = new MailAddress(email);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = subject;
                // текст письма
                m.Body = message;
                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                // логин и пароль
                smtp.Credentials = new NetworkCredential(sendFrom, password);
                smtp.EnableSsl = true;
                smtp.Send(m);
            }
            catch (Exception)
            {

            }
        }
    }
}
