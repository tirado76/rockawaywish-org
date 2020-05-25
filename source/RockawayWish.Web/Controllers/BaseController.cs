using System;
using System.Web.Mvc;

using InteractiveMembership.Core.Constants;

namespace RockawayWish.Web.Controllers
{
    public class BaseController : AsyncController
    {
        #region Constructor
        public BaseController()
        {
            appSettings = new AppSettingsConfig();
            applicationId = appSettings.SiteAppSettings.ApplicationId;
            apiKey = new Guid(appSettings.SiteAppSettings.ApiKey);
        }
        #endregion
        #region Internal Properties
        internal AppSettingsConfig appSettings;
        internal Guid applicationId;
        internal Guid apiKey;
        #endregion
        #region Public Methods
        #endregion

    }
}