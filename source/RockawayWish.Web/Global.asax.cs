using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

using InteractiveMembership.Core.Enums;
using InteractiveMembership.Data.Providers;

using RockawayWish.Web.Controllers;

namespace RockawayWish.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Removing the X-AspNetMvc-Version HTTP Header
            MvcHandler.DisableMvcResponseHeader = true;
        }
        protected void Session_End(object sender, EventArgs e)
        {
            // sign user out and destroy access token
            try{

                if (Request.IsAuthenticated)
                {
                    Guid appId = new BaseController().ApplicationId;
                    Guid userId = new BaseController().UserId;

                    // sign user out
                    FormsAuthentication.SignOut();

                    // delete token
                    var requestToken = new UsersProvider().DeleteToken(appId, userId, (int)TokenType.SiteAccess);
                    if (requestToken.Status == 0)
                    {
                    }

                }


                // sign user out
            }
            catch {};
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();

            string errMsg = string.Empty;
            if (ex != null)
            {
                // log error
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<p>An error occurred</p>");
                sb.AppendFormat("<p>{0}</p>", ex.Message);
                var result = new BaseController().SendEmail("richie@tiradointeractive.com", "Richard Tirado", "An error occurred", sb.ToString());

                HttpException httpError = ex as HttpException;

                if (httpError != null)
                {
                    // get error code
                    int errorCode = httpError.GetHttpCode();

                    // check if 404 error
                    // if so, redirect to 404 page
                    if (errorCode == 404)
                    {
                        Response.Clear();
                        Response.Status = "404 Page Not Found";
                        Response.StatusCode = 404;
                        HttpContext.Current.Response.Redirect("~/Error/PageNotFound");
                        return;
                    }
                    else
                    {
                        // somehow log the error
                        // exception is null
                        errMsg = Server.UrlEncode("Server Error Code: " + errorCode);
                        Response.Redirect("~/Error/?err=" + errMsg);
                    }
                }
                else
                {
                    errMsg = Server.UrlEncode("Not a server error: " + ex.Message);
                    // exception is null
                    Response.Redirect("~/Error/?err=" + errMsg);
                }
            }
            else
            {
                // exception is null
                errMsg = Server.UrlEncode("Unable to detect error.");
                Response.Redirect("~/Error/?err=" + errMsg);
            }
        }
    }
}
