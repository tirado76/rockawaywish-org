using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using InteractiveMembership.Core.Constants;

namespace RockawayWish.Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
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
    }
}