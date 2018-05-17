using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockawayWishCMS.Data.Entities
{
    public class WebsiteEntity
    {
        public WebsiteEntity()
        {
            PagesEntity = new PagesEntity();
        }
        public string domain { get; set; }
        public string title { get; set; }
        public PagesEntity PagesEntity { get; set; }
    }
}
