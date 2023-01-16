//using Frontend.Controllers;
//using Frontend.Models;
//using Frontend.Areas.Identity.Pages.Account;
//using Frontend.Data;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.VisualStudio;
//using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
//using AspNetCoreHero.ToastNotification.Abstractions;
//using Moq;
//using AspNetCoreHero.ToastNotification;
//using AspNetCoreHero.ToastNotification.Extensions;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Principal;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.InMemory;

//namespace FrontendTest
//{
//    public class UnitTestAccountSystem
//    {
//        public ILogger<HomeController> Tmp()
//        {
//            var serviceProvider = new ServiceCollection()
//                .AddLogging()
//                .BuildServiceProvider();
//            var factory = serviceProvider.GetService<ILoggerFactory>();
//            var logger = factory.CreateLogger<HomeController>();
//            return logger;
//        }
//            public ControllerContext GetFakeAdminUser()
//        {
//            var fakeIdentity = new GenericIdentity("TestRanger1@chengeta.com");
//            var fakeUser = new ClaimsPrincipal(fakeIdentity);
//            var context = new ControllerContext
//            {
//                HttpContext = new DefaultHttpContext
//                {
//                    User = fakeUser
//                }
//            };
//            return context;
//        }
//        [Fact]
//        public void LoginTest()
//        {
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase(databaseName: "Test")
//                .Options;
//            using (var context = new ApplicationDbContext(options))
//            {
//                context.Testsets.Add(new Testset { id = "098b2034 - 250b - 435f - 951f - 896e5941d056", Name = "Employee", NormalizedName = "EMPLOYEE", ConcurrencyStamp = "8c403fe2 - 1a16 - 49c6 - 8738 - 7ce325f57138"});
//                context.Testsets.Add(new Testset { id = "3164ec42-26e3-4365-9e0d-f2dcea3e4913", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "30d50acc-36a2-4d4f-a780-35b770c57a77" });
//                context.SaveChanges();
//            }
//            using (var context = new ApplicationDbContext(options))
//            {
//                TmpRepository tmpRepository = new TmpRepository(context);
//                var testsets = tmpRepository.GetAll(); 
//                Assert.Equal(2, testsets.Count());
//            }
//        }
//    }
//}
