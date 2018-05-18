using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockawayWish.Core.Entities
{
    public class HomePageEntity
    {
        public HomePageEntity()
        {
            CarouselEntity = new CarouselEntity();
        }
        public string pageTitle { get; set; }
        public string headerText { get; set; }
        public string metaKeywords { get; set; }
        public string metaDescription { get; set; }

        public CarouselEntity CarouselEntity { get; set; }
    }
}
