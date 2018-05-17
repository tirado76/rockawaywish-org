using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockawayWishCMS.Data.Entities
{
    public class PagesEntity
    {

        public PagesEntity()
        {
            HomePageEntity = new HomePageEntity();
        }
        public HomePageEntity HomePageEntity { get; set; }

    }
    public class BasePagesEntity
    {
        public string pageTitle { get; set; }
        public string headerText { get; set; }
        public string metaKeywords { get; set; }
        public string metaDescription { get; set; }


    }
}
