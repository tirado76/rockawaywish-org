using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using System.Reflection;

using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.WebApi;

using RockawayWishCMS.Data.Entities;
using RockawayWishCMS.Data.Helpers;
using RockawayWishCMS.Data.Repositories;



namespace RockawayWishCMS.Controllers
{
    public class SiteController : BaseController
    {
        public SiteController()
        {
        }
        /// <summary>
        /// Returns the WebsiteEntity in JSON format
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        //[OutputCache(Duration = CacheDurationLong)]
        public HttpResponseMessage Get()
        {
            WebsiteEntity  entity = new UmbracoRepository().GetWebsiteEntity();
            string json = JsonHelper.JsonSerializer<WebsiteEntity>(entity);
            return new HttpResponseMessage() { Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json") };
        }
    }
}
