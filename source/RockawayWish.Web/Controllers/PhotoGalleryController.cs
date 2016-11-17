using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Security;

using RockawayWish.Web.Models;

using InteractiveMembership.Core.Constants;
using InteractiveMembership.Core.Enums;
using InteractiveMembership.Core.Models;
using InteractiveMembership.Core.Models.Instagram;
using InteractiveMembership.Data.Providers;

namespace RockawayWish.Web.Controllers
{
    public class PhotoGalleryController : BaseController
    {
        private SocialProvider _Provider = new SocialProvider();
        private List<FeedItem> _InstagramList = new List<FeedItem>();

        [Route("photos/instagram")]
        public async Task<ActionResult> Instagram()
        {
            PhotoGalleryVM vm = new PhotoGalleryVM();
            var result = await _Provider.GetInstagramList(new Guid(Config.ApplicationId), this.InstagramAccessToken);

            // convert result data to object
            _InstagramList = (List<FeedItem>)result;

            if (result != null && result.Count() > 0)
            {
                vm.InstagramList = _InstagramList;
            }
            return View(vm);

        }

    }
}