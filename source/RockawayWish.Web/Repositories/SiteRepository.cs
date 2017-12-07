using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

using tiradointeractive.Services.Models;
using tiradointeractive.Services.Models.ViewModels;

using RockawayWish.Web.Helpers;

namespace RockawayWish.Web.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        const string _SiteApiEndpoint = "https://services.tiradointeractive.com/sites/rockawaywish-org"; 
        readonly Uri _baseUri;
        readonly IDictionary<string, string> _headers;

        public SiteRepository()
        {
            _baseUri = new Uri(_SiteApiEndpoint);
            _headers = new Dictionary<string, string>();

        }
        public async Task<SiteModel> Get()
        {
            SiteModel siteModel = new SiteModel();


            try
            {
                var url = new Uri(_SiteApiEndpoint);
                var result = await new HTTPHelper().SendRequestAsync<SiteModel>(url);

                if (result != null)
                {
                }
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }

            
            //var result = HTTPHelper.GetAsync(new Di)

            //await Task.Delay(0);
            return siteModel;

        }
    }
}