﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

using InteractiveMembership.Core.Constants;
using InteractiveMembership.Core.Models;
using InteractiveMembership.Data.Providers;

using RockawayWish.Web.Models;
using RockawayWish.Web.Repositories;

namespace RockawayWish.Web.Controllers
{
    public class BaseController : Controller
    {
        internal SiteRepository _SiteVM;

        public BaseController()
        {
            _SiteVM = new SiteRepository();
        }

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
                        return new Guid(authTicket[1].ToString());
                }
                return new Guid();
            }
        }
        internal string UserFullName
        {
            get
            {
                if (Request.IsAuthenticated)
                {
                    char[] delimitter = { '|' };
                    string[] authTicket = HttpContext.User.Identity.Name.Split(delimitter);
                    if (authTicket.Length > 0)
                        return authTicket[0].ToString();
                }
                return string.Empty;
            }
        }
        internal string UserEmail
        {
            get
            {
                if (Request.IsAuthenticated)
                {
                    char[] delimitter = { '|' };
                    string[] authTicket = HttpContext.User.Identity.Name.Split(delimitter);
                    if (authTicket.Length > 0)
                        return authTicket[3].ToString();
                }
                return string.Empty;
            }
        }
        internal bool UserIsActive
        {
            get
            {
                if (Request.IsAuthenticated)
                {
                    char[] delimitter = { '|' };
                    string[] authTicket = HttpContext.User.Identity.Name.Split(delimitter);
                    if (authTicket.Length > 0)
                        return Convert.ToBoolean(authTicket[4].ToString());
                }
                return false;
            }
        }
        internal bool UserIsUser
        {
            get
            {
                if (Request.IsAuthenticated)
                {
                    char[] delimitter = { '|' };
                    string[] authTicket = HttpContext.User.Identity.Name.Split(delimitter);
                    if (authTicket.Length > 0)
                        return Convert.ToBoolean(authTicket[5].ToString());
                }
                return false;
            }
        }
        internal bool UserIsAdmin
        {
            get
            {
                if (Request.IsAuthenticated)
                {
                    char[] delimitter = { '|' };
                    string[] authTicket = HttpContext.User.Identity.Name.Split(delimitter);
                    if (authTicket.Length > 0)
                        return Convert.ToBoolean(authTicket[6].ToString());
                }
                return false;
            }
        }
        internal bool UserIsSuperAdmin
        {
            get
            {
                if (Request.IsAuthenticated)
                {
                    char[] delimitter = { '|' };
                    string[] authTicket = HttpContext.User.Identity.Name.Split(delimitter);
                    if (authTicket.Length > 0)
                        return Convert.ToBoolean(authTicket[7].ToString());
                }
                return false;
            }
        }
        internal bool UserIsDonator
        {
            get
            {
                if (Request.IsAuthenticated)
                {
                    char[] delimitter = { '|' };
                    string[] authTicket = HttpContext.User.Identity.Name.Split(delimitter);
                    if (authTicket.Length > 0)
                        return Convert.ToBoolean(authTicket[8].ToString());
                }
                return false;
            }
        }
        internal enum AuthTicketElements
        {
            FullName = 0,
            UserId = 1,
            Email = 3,
            IsActive = 4,
            IsUser = 5,
            IsAdmin = 6,
            IsSuperAdmin = 7,
            IsDonator = 8
        }
        internal Guid ApplicationId
        {
            get
            {
                return new Guid(SiteConfig.ApplicationId);
            }
        }
        internal EmailModel SendEmail(string toAddress, string toName, string subject, string message)
        {
            EmailModel model = new EmailModel();
            try
            {

                model = new EmailProvider().Send(new Guid(SiteConfig.ApplicationId),
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
            catch
            {

            }
            return model;
        }
        internal string MembershipAuditEmail { get { return ConfigurationManager.AppSettings["Membership-Audit-Email"]; } }
        internal string MembershipAuditName { get { return ConfigurationManager.AppSettings["Membership-Audit-Name"]; } }
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
        internal string ContactUsUrl{ get { return ConfigurationManager.AppSettings["ContactUs-Url"]; } }
        internal string ContactUsEmail { get { return ConfigurationManager.AppSettings["ContactUs-Email"]; } }
        internal string PayPalDonateButtonId { get { return ConfigurationManager.AppSettings["PayPal-Donate-Button-Id"]; } }
        internal Guid DonationPaymentId { get { return new Guid(ConfigurationManager.AppSettings["Donation-PaymentId"]); } }
        internal string InstagramAccessToken { get { return ConfigurationManager.AppSettings["Instagram-Access-Token"]; } }
 
    }
}