using System.Net;
using System.Net.Mail;
using System.Text;
using GarnetAccounting.Areas.Support.Dtos;

namespace GarnetAccounting.Classes
{

    public static class EmailSender
    {
        public static void Send(string to, string subject, string body)
        {
            var password = "aonx jurj gitu rhma";
            var myMail = "avaandish.sup@gmail.com";
            var mail = new MailMessage(myMail, to);
            var smtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(myMail, "آوا اندیش رستـا");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            smtpServer.Port = 587;
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Credentials = new NetworkCredential(myMail, password);
            smtpServer.EnableSsl = true;
            smtpServer.Timeout = 1000000;
            smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpServer.Send(mail);
        }

        public static string CreateUserMessageForSupport(UserSendSmsDto smsDto)
        {
            string template = @"
    <!DOCTYPE html>
    <html lang='fa' dir='rtl'>
    <head>
        <meta charset='UTF-8'>
        <title>پشتیبانی سامانه مودیان</title>
    </head>
    <body style='font-family: Tahoma, sans-serif; background-color: #f4f4f4; margin: 0; padding: 20px; direction: rtl; text-align: right;'>
        <div style='max-width: 90%; margin: 0 auto; background-color: #ffffff; border-radius: 5px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); padding: 20px; line-height: 1.6;'>
            <div style='background-color: #007BFF; color: white; padding: 10px; border-top-left-radius: 5px; border-top-right-radius: 5px; font-size: 18px;'>
                پشتیبانی سامانه مودیان
            </div>
            <div style='padding: 10px;'>
                <p>
                    <span style='font-weight: bold; margin:0; margin-right: 10px;'>دپارتمان:</span> {{Department}}
                </p>
                <p>
                    <span style='font-weight: bold; margin:0; margin-right: 10px;'>موضوع:</span> {{Subject}}
                </p>
                <p>
                    <span style='font-weight: bold; margin:0; margin-right: 10px;'>نام مشتری:</span> {{SenderCustomerName}}
                </p>
                <p>
                    <span style='font-weight: bold; margin:0; margin-right: 10px;'>فرستنده:</span> {{SenderName}}
                </p>
                <p>
                    <span style='font-weight: bold; margin:0; margin-right: 10px;'>شماره تماس فرستنده:</span> {{PhoneNumber}}
                </p>
                <p>
                    <span style='font-weight: bold; margin:0; margin-right: 10px;'>نام کاربری:</span> {{SenderUserId}}
                </p>
                <p>
                    <span style='font-weight: bold; margin:0; margin-right: 10px;'>زمان ارسال:</span> {{Date}} ساعت {{Time}}
                </p>
                <hr>
                <p style='padding: 20px; margin-top: 15px; background: whitesmoke; border-radius:8px;'>
                    <span style='font-weight: bold; margin-right: 15px;'>پیام:</span> 
                    </br>  {{Message}}
                </p>
                </hr>
               <div style='display:flex; flex-wrap:wrap; flex-direction:column;'> 
                 <a href='tel:{{PhoneNumber}}' style='text-decoration:none; font-weight: bold; padding:10px 20px; margin:10px; background-color:#ff2c54; color:#fff; border-radius:5px; text-align:center;' > تماس با فرستنده </a>
                  <a href='sms:{{PhoneNumber}}?body=کاربر گرامی {{SenderName}}%0Aضمن سپاس از ارتباط شما با واحد پشتیبانی گارنِت%0Aبا تشکر' style='text-decoration:none; font-weight: bold; padding:10px 20px; margin:10px; background-color:#0094ff; color:#000; border-radius:5px; text-align:center;' > ارسال پیام برای فرستنده </a>
            </div>
            </div>
        </div>
    </body>
    </html>";

            string body = template
                .Replace("{{Department}}", smsDto.Department)
                .Replace("{{PhoneNumber}}", smsDto.PhoneNumber)
                .Replace("{{Subject}}", smsDto.Subject)
                .Replace("{{Message}}", smsDto.Message.Replace("\n", "<br>"))
                .Replace("{{SenderUserId}}", smsDto.SenderUserId ?? "-")
                .Replace("{{SenderName}}", smsDto.SenderName ?? "-")
                .Replace("{{SenderCustomerName}}", smsDto.SenderCustomerName ?? "-")
                .Replace("{{Date}}", smsDto.Date.ToString("yyyy/MM/dd"))
                .Replace("{{Time}}", smsDto.Time.ToString("HH:mm"));

            return body;
        }
    }

}

