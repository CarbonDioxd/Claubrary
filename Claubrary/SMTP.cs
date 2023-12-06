using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Claubrary
{
    internal static class SMTP
    {
        public static SmtpClient Client { get; set; } = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("viraylogan@gmail.com", "ooyq ejge wjsh exgn"),
            EnableSsl = true,
        };

        public static bool SendEmail(string from, string recipient, string subject, string body)
        {
            try
            {
                Client.Send(from, recipient, subject, body);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
    }
}
