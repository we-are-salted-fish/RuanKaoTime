using DanishBread.GetRunKao.Config;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace DanishBread.GetRunKao
{
    public class MailHelper
    {

        public static void SendMail(MailConfig config, string msg)
        {
            MailAddress mailAddress = new MailAddress(config.Address);
            MailMessage mailMessage = new MailMessage();

            foreach (var item in config.ReceiveList)
            {
                if (!string.IsNullOrEmpty(item))
                    mailMessage.To.Add(item.ToString());
            }
            
            mailMessage.From = mailAddress;
            mailMessage.Subject = "FuckWayne";
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.Body = msg;
            mailMessage.BodyEncoding = Encoding.Default;
            mailMessage.Priority = MailPriority.High;
            mailMessage.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential(config.Address, config.Password);
            smtp.Host = config.Host;
            smtp.Send(mailMessage);
        }
    }
}
