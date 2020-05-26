using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RockawayWish.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // sign user out and destroy access token
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();

            string errMsg = string.Empty;
            if (ex != null)
            {
                // log error

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
