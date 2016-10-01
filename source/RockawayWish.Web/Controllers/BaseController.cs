using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

using InteractiveMembership.Core.Constants;
using InteractiveMembership.Core.Models;
using InteractiveMembership.Data.Providers;

namespace RockawayWish.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            RedirectToAction("Index", "Error", new { err = filterContext.Exception.Message });
        }        // GET: Base
        internal Guid UserId
        {
            get
            {
                if (Request.IsAuthenticated)
                {
                    char[] delimitter = { '|' };

                    string[] authTicket = HttpContext.User.Identity.Name.Split(delimitter);

                    if (authTicket.Length > 0)
                    {
                        return new Guid(authTicket[1].ToString());
                    }
                }
                return new Guid();
            }
        }
        internal Guid ApplicationId
        {
            get
            {
                return new Guid(Config.ApplicationId);
            }
        }
        internal EmailModel SendEmail(string toAddress, string subject, string message)
        {
            EmailModel model = new EmailModel();
            try
            {

                SmtpClient smtpClient = new SmtpClient(this.SmtpHost, this.SmtpPort);

                smtpClient.UseDefaultCredentials = this.SmtpUseDefaultCredentials;
                smtpClient.Credentials = new System.Net.NetworkCredential(this.SmtpUserName, this.SmtpPassword);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = this.SmtpEnableSSL;
                smtpClient.Port = this.SmtpPort;
                smtpClient.Timeout = 30000;

                MailMessage mailMessage = new MailMessage();

                //Setting From , To and CC
                mailMessage.From = new MailAddress(this.SmtpFromAddress, this.SmtpFromName);
                mailMessage.To.Add(new MailAddress(toAddress));
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;

                model = new EmailProvider().Send(mailMessage, smtpClient).Result;
            }
            catch (Exception ex)
            {

            }
            return model;
        }
        internal string SmtpHost { get { return ConfigurationManager.AppSettings["SMTP-Host"]; } }
        internal string SmtpUserName { get { return ConfigurationManager.AppSettings["SMTP-UserName"]; } }
        internal string SmtpPassword { get { return ConfigurationManager.AppSettings["SMTP-Password"]; } }
        internal int SmtpPort { get { return int.Parse(ConfigurationManager.AppSettings["SMTP-Port"]); } }
        internal string SmtpFromAddress { get { return ConfigurationManager.AppSettings["SMTP-FromAddress"]; } }
        internal string SmtpFromName { get { return ConfigurationManager.AppSettings["SMTP-FromName"]; } }
        internal bool SmtpEnableSSL { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["SMTP-EnableSSL"]); } }
        internal bool SmtpUseDefaultCredentials { get {  return Convert.ToBoolean(ConfigurationManager.AppSettings["SMTP-UseDefaultCredentials"]); } }
        internal string ResetPasswordEndpoint { get { return ConfigurationManager.AppSettings["ResetPassword-Endpoint"]; } }
        internal string ResetPasswordSubject { get { return ConfigurationManager.AppSettings["ResetPassword-Subject"]; } }
        internal string ResetPasswordContactLink { get { return ConfigurationManager.AppSettings["ResetPassword-ContactLink"]; } }

    }
}