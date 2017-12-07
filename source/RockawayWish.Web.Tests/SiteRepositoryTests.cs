using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using tiradointeractive.Services.Models;
using RockawayWish.Web.Repositories;

namespace RockawayWish.Web.Tests
{
    [TestClass]
    public class SiteRepositoryTests
    {
        [TestMethod]
        public void Get()
        {
            var result = new SiteRepository().Get().Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.HomeVM);
            Assert.IsNotNull(result.EventsVM);
            Assert.IsNotNull(result.EventVM);

        }
    }
}
