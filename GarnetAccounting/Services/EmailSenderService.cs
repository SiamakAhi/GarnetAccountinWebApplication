using System.Net;
using System.Net.Mail;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Models;

namespace GarnetAccounting.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly AppDbContext _db;
        private string _myEmail;
        public EmailSenderService(AppDbContext db)
        {
            _db = db;
            _myEmail = "supp.tavaertebat@gmail.com";
        }

        public void Sender(string to, string subject, string body)
        {
            var password = "vpfmmbjceakqnwks";
            var myMail = "supp.tavaertebat@gmail.com";
            var mail = new MailMessage(myMail, to);
            var smtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(myMail, "آوا اندیش رستـا");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            smtpServer.Port = 587;
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Credentials = new NetworkCredential(myMail, password);
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);
        }

    }
}
