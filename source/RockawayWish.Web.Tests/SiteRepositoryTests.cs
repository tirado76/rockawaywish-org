using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RockawayWish.Core.Entities;
using RockawayWish.Web.Repositories;

namespace RockawayWish.Web.Tests
{
    [TestClass]
    public class SiteRepositoryTests
    {
        [TestMethod]
        public void Get()
        {
            SiteRepository siteRepository = new Repositories.SiteRepository();

            // test SiteRepository.WebsiteEntity object
            Assert.IsNotNull(siteRepository.WebsiteEntity);
            Assert.IsNotNull(siteRepository.WebsiteEntity.PagesEntity);
            Assert.IsNotNull(siteRepository.WebsiteEntity.PagesEntity.HomePageEntity);
            Assert.IsNotNull(siteRepository.WebsiteEntity.PagesEntity.HomePageEntity.CarouselEntity);
            Assert.IsNotNull(siteRepository.WebsiteEntity.PagesEntity.HomePageEntity.CarouselEntity.CarouselSlideEntities);
            Assert.IsTrue(siteRepository.WebsiteEntity.PagesEntity.HomePageEntity.CarouselEntity.CarouselSlideEntities.Count > 0);
            foreach (var slide in siteRepository.WebsiteEntity.PagesEntity.HomePageEntity.CarouselEntity.CarouselSlideEntities)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(slide.headerText));
            }

            // test SiteRepository.WebsiteEntity.HomePageVM object
            Assert.IsNotNull(siteRepository.HomePageVM);
            Assert.IsNotNull(siteRepository.HomePageVM.Carousel);
            Assert.IsNotNull(siteRepository.HomePageVM.Carousel.Slides);
            Assert.IsTrue(siteRepository.HomePageVM.Carousel.Slides.Count > 0);
            foreach (var slide in siteRepository.HomePageVM.Carousel.Slides)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(slide.headerText));
            }

            //Assert.IsNotNull(result.EventsVM);
            //Assert.IsNotNull(result.EventVM);

        }
    }
}
