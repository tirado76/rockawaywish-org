using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Reflection;

using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.WebApi;

using RockawayWishCMS.Data.Repositories;


namespace RockawayWishCMS.Controllers
{

    public class BaseController : UmbracoApiController
    {
        #region Constructor
        public BaseController()
        {
            _UmbracoRepository = new UmbracoRepository();
            GetWebsiteEntity();
        }
        #endregion

        #region Overrides
        #endregion

        #region Private Properties
        internal UmbracoRepository _UmbracoRepository;
        #endregion

        #region Internal Properties
        internal const int CacheDurationShort = 120;
        internal const int CacheDurationMedium = 0;
        internal const int CacheDurationLong = 36000;
        #endregion

        #region Private Methods

        #endregion

        //[OutputCache(Duration = CacheDurationLong)]
        internal void GetWebsiteEntity()
        {
             _UmbracoRepository.GetWebsiteEntity();


        }



    }
}
