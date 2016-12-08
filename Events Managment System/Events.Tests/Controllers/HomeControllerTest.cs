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
using Events.Models;
using Microsoft.AspNet.Identity;

namespace Events.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private IEventsService service;
        private Mock<IEventsService> mockRepository;
        private HomeController homeController;
        List<Event> events;
        List<Event> eventsIncorrect;
        ApplicationUser dummyUser;

        [TestInitialize]
        public void Initialize()
        {
            this.mockRepository = new Mock<IEventsService>();
            this.service = mockRepository.Object;
            this.homeController = new HomeController(service);
            this.dummyUser = new ApplicationUser() { UserName = "t@t.com", Email = "t@t.com", FullName = "Admin" };
            events = new List<Event>() {
                new Event()
                {
                    Id = 1,
                    Title = "Test Event1",
                    StartDate = DateTime.Now,
                    Duration = TimeSpan.FromHours(10.00),
                    Author = dummyUser,
                    Description = "some description bla bla",
                    Location = "BG",
                },
            };

            eventsIncorrect = new List<Event>() {
                new Event()
                {
                    Id = 1,
                    Title = "Test Event1",
                    Description = "some description bla bla",
                    Location = "BG",
                },
            };
        }

        [TestMethod]
        public void TestIndexViewModel()
        {
            ViewResult result = this.homeController.Index() as ViewResult;
            
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [TestMethod]
        public void TestIndexViewDataWithDummyData()
        {
            IQueryable<Event> mockedEvents = events.AsQueryable();
            this.mockRepository.Setup(t => t.GetPublicEvents()).Returns(mockedEvents);
            ViewResult result = this.homeController.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void TestIndexWithoutData()
        {
            IQueryable<Event> mockedEvents = null;
            this.mockRepository.Setup(t => t.GetPublicEvents()).Returns(mockedEvents);
            ViewResult result = this.homeController.Index() as ViewResult;
            
            Assert.IsNull(result.Model);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestIndexViewDataWithIncorrectData()
        {
            IQueryable<Event> incorrectMockedEvents = eventsIncorrect.AsQueryable();
            this.mockRepository.Setup(t => t.GetPublicEvents()).Returns(incorrectMockedEvents);
            ViewResult result = this.homeController.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            HomeController controller = new HomeController(service);
            ViewResult result = controller.About() as ViewResult;
            
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "About");
        }

        [TestMethod]
        public void Contact()
        {
            HomeController controller = new HomeController(service);
            ViewResult result = controller.Contact() as ViewResult;
            
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Contact");
        }
    }
}
