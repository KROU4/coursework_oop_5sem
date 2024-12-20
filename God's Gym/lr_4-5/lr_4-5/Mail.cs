using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace lr_4_5
{
    internal class Mail
    {
        public static MailMessage CreateMail(string name, string sender, string receiver,  string subject, string body)
        {
            var from = new MailAddress(sender, name);
            var to = new MailAddress(receiver);
            var mail = new MailMessage(from, to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            return mail;
        }

        public static void SendMail(string host, int smtpport, string sender, string pass, MailMessage mail)
        {
            SmtpClient smtp = new SmtpClient(host, smtpport);
            smtp.Credentials = new NetworkCredential(sender, pass);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        } 
    }
}
