using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailTest
{
    class Program
    {
        public async Task Send()
        {
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("monin.jose@neudesic.com", "Password.1");
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                string userState = "test message1";
                var mail = new MailMessage("monin.jose@neudesic.com", "monin.jose@neudesic.com");
                mail.Subject = "Test Mail";
                mail.Body = "Test Mail message";
                //client.SendMailAsync(mail);
                await client.SendMailAsync(mail);
                //client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void Send(MailAddress toAddress, string subject, string body, bool priority)
        {
            //SendEmail(toAddress, subject, body, priority);
            //Task.Factory.StartNew(() => SendEmail(toAddress, subject, body, priority), TaskCreationOptions.LongRunning);

        }

        private void SendEmail(string toAddress, string subject, string body, bool priority)
        {
            MailAddress fromAddress = new MailAddress("monin.jose@neudesic.com");
            string serverName = "smtp-mail.outlook.com";
            int port = 587;
            string userName = "monin.jose@neudesic.com";
            string password = "Password.1";

            var message = new MailMessage(fromAddress, new MailAddress(toAddress));

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            message.HeadersEncoding = Encoding.UTF8;
            message.SubjectEncoding = Encoding.UTF8;
            message.BodyEncoding = Encoding.UTF8;
            if (priority) message.Priority = MailPriority.High;

            Thread.Sleep(1000);

            SmtpClient client = new SmtpClient(serverName, port);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;

            NetworkCredential smtpUserInfo = new NetworkCredential(userName, password);
            client.Credentials = smtpUserInfo;

            client.Send(message);

            client.Dispose();
            message.Dispose();
        }

        static void Main(string[] args)
        {

            /*Program pr = new Program();
            ThreadStart ts = delegate
            {
                pr.SendEmail("monin.jose@neudesic.com", "Test Mail", "Test mail message", true);
            };
            new Thread(ts).Start();
            int i = 10;*/
            //await email.Send();
            /*SmtpClient smtpClient = new SmtpClient("mail.neudesic.com", 25);

            smtpClient.Credentials = new System.Net.NetworkCredential("monin.jose@neudesic.com", "Password.1");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("hcm@neudesc.com", "HCM");
            mail.To.Add(new MailAddress("monin.jose@neudesic.com"));
            mail.IsBodyHtml = true;
            mail.Body = "Test Mail";
            smtpClient.Send(mail);*/

            //MailMessage msg = new MailMessage();
            ////Add your email address to the recipients
            //msg.To.Add("monin.jose@neudesic.com");
            ////Configure the address we are sending the mail from
            //MailAddress address = new MailAddress("monin.jose@neudesic.com");
            //msg.From = address;
            //msg.Subject = "anything";
            //msg.Body = "anything";

            ////Configure an SmtpClient to send the mail.
            //SmtpClient client = new SmtpClient();
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = false;
            //client.Host = "smtp.gmail.com";
            //client.Port = 25;

            ////Setup credentials to login to our sender email address ("UserName", "Password")
            //NetworkCredential credentials = new NetworkCredential("monin.jose@neudesic.com", "Password.1");
            //client.UseDefaultCredentials = true;
            //client.Credentials = credentials;

            ////Send the msg
            //client.Send(msg);

            //SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            //client.Port = 587;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //System.Net.NetworkCredential credentials =
            //    new System.Net.NetworkCredential("monin.jose@neudesic.com", "Password.1");
            //client.EnableSsl = true;
            //client.Credentials = credentials;

            //try
            //{
            //    string userState = "test message1";
            //    var mail = new MailMessage("monin.jose@neudesic.com", "monin.jose@neudesic.com");
            //    mail.Subject = "Test Mail";
            //    mail.Body = "Test Mail message";
            //    //client.SendMailAsync(mail);
            //    client.Send(mail);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    throw ex;
            //}

        }
    }
}
