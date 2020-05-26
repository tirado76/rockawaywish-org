using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using CaptchaMvc.HtmlHelpers;
using InteractiveMembership.Core.Constants.EndPointSettings;
using InteractiveMembership.Data.Providers;

using RockawayWish.Web.ViewModels;

namespace RockawayWish.Web.Controllers
{
    public class ContactUsController : BaseController
    {
        #region Public Methods

        /// <summary>
        /// Contact Us page
        /// </summary>
        /// <returns></returns>
        [Route("contact/us")]
        public ActionResult Index()
        {
            // return vm
            return View(new ContactUsVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(SiteEndPointsConfig.ContactUs)]
        public async Task<ActionResult> Index(ContactUsVM model)
        {
            // check if captcha is valid
            if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                model.Status = 1;
                model.Message = "Captcha is not valid";
                return View(model);
            }
            // check if name is valid
            if (string.IsNullOrEmpty(model.Name))
            {
                model.Status = 1;
                model.Message = "Name is not valid";
                return View(model);
            }

            // check if email is valid
            if (string.IsNullOrEmpty(model.Email))
            {
                model.Status = 1;
                model.Message = "Email is not valid";
                return View(model);
            }

            // check if email is valid
            if (string.IsNullOrEmpty(model.ContactMessage))
            {
                model.Status = 1;
                model.Message = "Message is not valid";
                return View(model);
            }

            // generate email message
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<p>The following user has submitted a question on the WISH of Rockaway website.</p>");
            sb.AppendFormat("<p>Name: {0}</p>", model.Name);
            sb.AppendFormat("<p>Email: {0}</p>", model.Email);
            sb.AppendFormat("<p>Message: {0}</p>", model.ContactMessage);
            sb.AppendLine("<p>---------------------------------------------------------------------------------------------------------------------------</p>");
            sb.AppendFormat("<p><b>This message was sent to you by {0}</b></p>", this.appSettings.MembershipAppSettings.AdminSiteTitle);
            sb.AppendFormat("<img src=\"{0}\" />", this.appSettings.SiteAppSettings.LogoUrl);

            // send email
            var emailResult = await new EmailDataProvider(this.apiKey, this.applicationId, this.appSettings).Send(this.appSettings.SiteAppSettings.SmtpHost,
                this.appSettings.SiteAppSettings.SmtpFromAddress, this.appSettings.MembershipAppSettings.AdminEmail, string.Format(this.appSettings.SiteAppSettings.ContactUsNotificationSubjectToAdmin, this.appSettings.SiteAppSettings.SiteTitle),
                Server.HtmlEncode(sb.ToString()), this.appSettings.SiteAppSettings.SmtpPort, this.appSettings.SiteAppSettings.SmtpUseDefaultCredentials,
                this.appSettings.SiteAppSettings.SmtpEnableSSL, this.appSettings.SiteAppSettings.SmtpUserName,
                this.appSettings.SiteAppSettings.SmtpPassword, this.appSettings.SiteAppSettings.SmtpFromName,model.Name);

            if (emailResult.Status == 0)
                return RedirectPermanent(string.Format("~/{0}", SiteEndPointsConfig.ContactUsComplete));
            else
            {
                model.Status = 1;
                model.Message = "There was a problem sending the message.";
                return View(model);
            }


            return View(model);
        }

        /// <summary>
        /// Contact Us complete page
        /// </summary>
        /// <returns></returns>
        [Route(SiteEndPointsConfig.ContactUsComplete)]
        public ActionResult Complete()
        {

            return View(new ContactUsCompleteVM());
        }
        #endregion
    }
}