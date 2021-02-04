using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Controllers;
using ToDoList.Database;
using ToDoList.Filters;
using ToDoList.Models;
using ToDoList.Modules;

namespace ToDoList.Test.Filters
{
    [TestFixture]
    class RequiresAuthorizationTest
    {
        [Test]
        public async Task OnActionExecutionAsyncTest()
        {
            // --- TEST SETUP ---------------------------------------

            const string passwordHash = "passwordHash";
            string authToken = "authToken";

            var user = new User()
            {
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
                LoginName = "zekro",
                PasswordHash = passwordHash,
            };

            var claims = new AuthClaims()
            {
                SessionId = Guid.NewGuid(),
                UserId = user.Id,
            };

            var dbMock = new Mock<IContext>();
            dbMock.Setup(c => c.Users.FindAsync(user.Id)).ReturnsAsync(user);

            var authMock = new Mock<IAuthorization>();
            authMock.Setup(a => a.ValidateAuth(authToken)).Returns(claims);

            var httpContextMock = new Mock<HttpContext>();
            var noToken = "";
            httpContextMock.Setup(c => c.Request.Cookies.TryGetValue(Constants.SESSION_COOKIE_NAME, out noToken)).Returns(false);

            var contextMock = new ActionContext(
                httpContextMock.Object,
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>());

            var actionContextMock = new Mock<ActionExecutingContext>(
                contextMock,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<ControllerBase>());

            var nextMock = new Mock<ActionExecutionDelegate>();

            RequiresAuthorization GetFilter() => new RequiresAuthorization(dbMock.Object, authMock.Object);

            // --- TEST PROCEDURE -----------------------------------

            Assert.ThrowsAsync<Exception>(() => 
                GetFilter().OnActionExecutionAsync(actionContextMock.Object, () => null));

            actionContextMock.Setup(c => c.Controller).Returns(new AuthorizedControllerBase());
            await GetFilter().OnActionExecutionAsync(actionContextMock.Object, () => null);
            actionContextMock.VerifySet(c => c.Result = It.IsAny<UnauthorizedResult>());

            var invalidToken = "invalid_token";
            httpContextMock.Setup(c => c.Request.Cookies.TryGetValue(Constants.SESSION_COOKIE_NAME, out invalidToken)).Returns(true);
            await GetFilter().OnActionExecutionAsync(actionContextMock.Object, () => null);
            actionContextMock.VerifySet(c => c.Result = It.IsAny<UnauthorizedResult>());

            var controller = new AuthorizedControllerBase();
            actionContextMock.Setup(c => c.Controller).Returns(controller);
            httpContextMock.Setup(c => c.Request.Cookies.TryGetValue(Constants.SESSION_COOKIE_NAME, out authToken)).Returns(true);
            await GetFilter().OnActionExecutionAsync(actionContextMock.Object, nextMock.Object);
            nextMock.Verify(m => m.Invoke(), Times.AtLeastOnce());
            Assert.AreSame(claims, controller.AuthClaims);
            Assert.AreSame(user, controller.AuthorizedUser);
        }
    }
}
