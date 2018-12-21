using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace common
{
    /// <summary>
    /// functions that are essential to all applications
    /// </summary>
    public static class SharedMethod
    {
        static SharedMethod()
        { }

        public static bool SendEmail(string to, string from, string subject, string body, bool isHtml = false)
        {
            try
            {
                if (from.IndexOf('@') == -1) from = from + "@acmewidget.com";

                var msg = new MailMessage();
                msg.From = new MailAddress(from);
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = isHtml;

                foreach (string a in to.Split(';'))
                {
                    if (a.Trim() != "")
                    {
                        if (a.IndexOf('@') == -1)
                            msg.To.Add(a + "@acmewidget.com");
                        else
                            msg.To.Add(a);
                    }
                }

                using (var srv = new SmtpClient("smtpgateway.acmenetwork.com"))
                {
                    srv.Send(msg);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}