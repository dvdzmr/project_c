using Frontend.Controllers;
using Frontend.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using AspNetCoreHero.ToastNotification.Abstractions;
using Moq;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.Security.Claims;



namespace FrontendTest
{
    public class UnitTest1
    {
        public ILogger<HomeController> Tmp()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<HomeController>();
            return logger;
        }
        public ControllerContext GetFakeAdminUser()
        {
            var fakeIdentity = new GenericIdentity("TestRanger1@chengeta.com");
            var fakeUser = new ClaimsPrincipal(fakeIdentity);
            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = fakeUser
                }
            };
            return context;
        }

        [Fact]
        public void ViewShowMainTest()
        {
            var logger = Tmp();
            var notyf = new Mock<INotyfService>();

            var controller = new HomeController(logger, notyf.Object);

            var context = GetFakeAdminUser();
            controller.ControllerContext = context;

            var result = controller.Main() as ViewResult;
            Assert.NotNull(result);
            var product = result?.ViewData.Model;
            var list = product as List<MapItems>;
            Assert.Equal(5,list.Count());
        }
        [Fact]
        public void PartialviewEventsTest()
        {
            var logger = Tmp();
            var notyf = new Mock<INotyfService>();

            var controller = new HomeController(logger, notyf.Object);

            var context = GetFakeAdminUser();
            controller.ControllerContext = context;

            var result = controller.Events();
            Assert.NotNull(result);
        }
        [Fact]
        public void ControllerGetEventTest()
        {
            var logger = Tmp();
            var notyf = new Mock<INotyfService>();

            var controller = new HomeController(logger, notyf.Object);

            var context = GetFakeAdminUser();
            controller.ControllerContext = context;

            var result = controller.GetEvents(5);
            Assert.True(result.Count() == 5);
        }
        [Fact]
        public async void ControllerGetDataTest()
        {
            var logger = Tmp();
            var notyf = new Mock<INotyfService>();

            var controller = new HomeController(logger, notyf.Object);

            var context = GetFakeAdminUser();
            controller.ControllerContext = context;

            var result = await controller.GetData(0);
            Assert.NotNull(result);
        }
        [Fact]
        public async void ControllerGetNotificationTest()
        {
            var logger = Tmp();
            var notyf = new Mock<INotyfService>();

            var controller = new HomeController(logger, notyf.Object);

            var context = GetFakeAdminUser();
            controller.ControllerContext = context;

            var result = await controller.GiveNotification();
            Assert.NotNull(result);
        }

        [Fact]
        public async void ControllerGetViewDataTest()
        {
            var logger = Tmp();
            var notyf = new Mock<INotyfService>();

            var controller = new HomeController(logger, notyf.Object);

            var context = GetFakeAdminUser();
            controller.ControllerContext = context;

            var result = await controller.getviewdata();
            Assert.NotNull(result);
        }
        [Fact]
        public async void ControllerPushStatusTest()
        {
            var logger = Tmp();
            var notyf = new Mock<INotyfService>();

            var controller = new HomeController(logger, notyf.Object);

            var context = GetFakeAdminUser();
            controller.ControllerContext = context;

            var result = await controller.PushStatus(1, "Completed");
            Assert.NotNull(result);
        }
        


    }
}