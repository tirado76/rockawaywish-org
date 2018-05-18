using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

using RockawayWish.Web.Helpers;

namespace RockawayWish.Web.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        const string _SiteApiEndpoint = "https://services.tiradointeractive.com/sites/rockawaywish-org";
        const string _UmbracoApiEndpoint = "https://rockawaywishcms-local.tiradointeractive.com/umbraco/api/site/get";
        readonly Uri _baseUri;
        readonly IDictionary<string, string> _headers;

        public SiteRepository()
        {
            _baseUri = new Uri(_SiteApiEndpoint);
            _headers = new Dictionary<string, string>();

        }

    }
}