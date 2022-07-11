using DanishBread.GetRunKao.Config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace DanishBread.GetRunKao
{
public class MailHelper
{
        private readonly MailConfig _config;

        public MailHelper(IOptions<MailConfig> config)
        {
            this._config = config.Value;
        }

        public void SendMail(string msg)
        {
            MailAddress mailAddress = new MailAddress(_config.Address);
            MailMessage mailMessage = new MailMessage();

            foreach (var item in _config.ReceiveList)
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
            smtp.Credentials = new System.Net.NetworkCredential(_config.Address, _config.Password);
            smtp.Host = _config.Host;
            smtp.Send(mailMessage);
        }
    }
}
