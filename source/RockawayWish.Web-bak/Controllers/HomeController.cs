﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


using RockawayWish.Web.Helpers;
using RockawayWish.Web.Repositories;

namespace RockawayWish.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var homePageVM = _SiteVM.HomePageVM;
            // Stop Caching in IE
            //HttpContext.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            // Stop Caching in Firefox
            //HttpContext.Response.Cache.SetNoStore();

            // get set site model
            //var siteModel = new SiteRepository().Get().ConfigureAwait(false);

            return View(homePageVM);
        }

        [Route("about-wish")]
        public ActionResult About()
        {

            return View();
        }

    }
}