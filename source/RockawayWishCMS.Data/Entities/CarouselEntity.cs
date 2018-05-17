using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockawayWishCMS.Data.Entities
{
    public class CarouselEntity
    {
        public CarouselEntity()
        {
            //CarouselSlideEntities = new List<CarouselSlideEntity>();
        }
        //public List<CarouselSlideEntity> CarouselSlideEntities { get; set; }
    }
    public class CarouselSlideEntity
    {
        public string headerText { get; set; }
        public string subheaderText { get; set; }
        public string sliderImage { get; set; }
        public string buttonText { get; set; }
        public string showButton { get; set; }
        public string buttonALinkToAURL { get; set; }
        public string buttonLinkURL { get; set; }
        public string buttonLinkOpenNewWindow { get; set; }
        public string buttonLinkToAFile { get; set; }
        public string buttonFileDownload { get; set; }
        public string headerTextCSSClass { get; set; }
        public string subHeaderTextCSSClass { get; set; }
        public string buttonCSSClass { get; set; }
        public string sliderImageCSSClass { get; set; }

    }
}
