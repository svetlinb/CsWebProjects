using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Events;
using Events.Controllers;
using Events.Services;
using Moq;
using System.ComponentModel;

namespace Events.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        IEventsService service;
        public HomeControllerTest()
        {
            Mock<IEventsService> mockView = new Mock<IEventsService>();
            service = mockView.Object;
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(service);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(service);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(service);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
