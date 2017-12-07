using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using tiradointeractive.Services.Models;
using tiradointeractive.Services.Models.ViewModels;

using RockawayWish.Web.Controllers;

namespace RockawayWish.Web.Tests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void HomeController()
        {

            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result);
        }
    }
}
