using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.ServiceLayer
{
    public static class EmailServices
    {
        public static void Send(string to, string from, string subject, string body)
        {

            try
            {
                //string to = "nitikesh.jalan2@gmail.com";
                //to = "shivam441@gmail.com";

                MailMessage message = new MailMessage();
                message.From = new MailAddress("donotreply@prototype.com");

                message.To.Add(new MailAddress(to));

                message.Subject = subject;

                message.Body = body;

                //message.IsBodyHtml = true;

                // finaly send the email:
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";

                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("shivam441@gmail.com", "Sshhiivfaam717");

                smtp.EnableSsl = true;
                smtp.Send(message);

            }
            catch
            {
                throw;
            }

        }

    }
}
