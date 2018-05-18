using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockawayWish.Web.Models
{
    public class HomePageVM : BasePageVM
    {
        public HomePageVM()
        {
            Carousel = new CarouselVM();
        }

        public CarouselVM Carousel { get; set; }
    }

    public class CarouselVM
    {
        public CarouselVM()
        {
            Slides = new List<CarouselSlideVM>();
        }

        public List<CarouselSlideVM> Slides { get; set; }

    }
    public class CarouselSlideVM
    {

        public string headerText { get; set; }
        public string subheaderText { get; set; }
        public string sliderImage { get; set; }
        public string buttonText { get; set; }
        public bool showButton { get; set; }
        public bool buttonALinkToAURL { get; set; }
        public string buttonLinkURL { get; set; }
        public bool buttonLinkOpenNewWindow { get; set; }
        public bool buttonLinkToAFile { get; set; }
        public string buttonFileDownload { get; set; }
        public string headerTextCSSClass { get; set; }
        public string subHeaderTextCSSClass { get; set; }
        public string buttonCSSClass { get; set; }
        public string sliderImageCSSClass { get; set; }

    }
}