
using InteractiveMembership.Core.ViewModels;

namespace RockawayWish.Web.ViewModels
{
    public class HomeVM : BaseVM
    {
        #region Constructor
        public HomeVM()
        {
            Carousel = new CarouselVM();
        }
        #endregion

        #region Public Properties
        public CarouselVM Carousel { get; set; }
        #endregion

    }
}