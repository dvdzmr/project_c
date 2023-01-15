using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FrontendTest
{
    public class IntergrationTestFrontend
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        
        public IntergrationTestFrontend(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Theory]
        [InlineData("/")]
        [InlineData("/Home")]
        [InlineData("/Home/Main")]
        [InlineData("/Home/Events")]
        [InlineData("/Home/ViewDetailsPartial")]
        [InlineData("/Home/Index")]
        [InlineData("/Home/Privacy")]
        [InlineData("/ErrorNoAcces/NoAcces")]
        [InlineData("/Identity/Account/Login")]
        [InlineData("/Identity/Account/Register")]
        [InlineData("/Identity/Account/ForgotPassword")]
        [InlineData("/Identity/Account/ResendEmailConfirmation")]
        [InlineData("/Identity/Account/Manage")]
        [InlineData("/Identity/Account/Manage/Email")]
        [InlineData("/Identity/Account/Manage/ChangePassword")]
        [InlineData("/Identity/Account/Manage/TwoFactorAuthentication")]
        [InlineData("/Identity/Account/Manage/PersonalData")]
        public async Task Get_EndpointsReturnSuccesAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Home/Main")]
        [InlineData("/Home/ViewDetailsPartial")]
        [InlineData("/Identity/Account/Register")]
        public async Task Get_SecurePageRedirectsAnUnauthenticatedUser(string url)
        {
            // Arrange
            var client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
            // Act
            var response = await client.GetAsync(url);
            
            //Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("/ErrorNoAcces/NoAcces", 
                response.Headers.Location.OriginalString);
        }
    }
}
