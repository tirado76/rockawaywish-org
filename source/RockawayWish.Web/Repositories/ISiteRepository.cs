using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tiradointeractive.Services.Models;

namespace RockawayWish.Web.Repositories
{
    interface ISiteRepository
    {
         Task<SiteModel> Get();

    }
}
