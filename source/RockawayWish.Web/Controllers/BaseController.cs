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
        internal EmailModel SendEmail(string toAddress, string toName, string subject, string message)
        {
            EmailModel model = new EmailModel();
            try
            {

                model = new EmailProvider().Send(new Guid(Config.ApplicationId),
                    this.SmtpHost,
                    this.SmtpFromAddress,
                    toAddress,
                    subject,
                    Server.HtmlEncode(message),
                    this.SmtpPort,
                    this.SmtpUseDefaultCredentials,
                    this.SmtpEnableSSL,
                    this.SmtpUserName,
                    this.SmtpPassword,
                    this.SmtpFromName,
                    string.Empty).Result;
            }
            catch (Exception ex)
            {

            }
            return model;
        }
        internal string MembershipAdminUrl { get { return ConfigurationManager.AppSettings["Membership-Admin-Url"]; } }
        internal string MembershipAdminEmail { get { return ConfigurationManager.AppSettings["Membership-Admin-Email"]; } }
        internal string MembershipAdminName { get { return ConfigurationManager.AppSettings["Membership-Admin-Name"]; } }
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