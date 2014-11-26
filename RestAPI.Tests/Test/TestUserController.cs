using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestAPI.Users;
using System.Web.Http.Results;
using RestAPI.Models;
using System.Net;

namespace RestAPI.Tests.Test
{
    [TestClass]
    public class TestUserController
    {
        [TestMethod]
        public void PostUser_ShouldReturnSameUser()
        {
            var controller = new UserController(new TestRestAPIContext());

            var item = GetDemoUser();

            var result =
                controller.PostUser(item) as CreatedAtRouteNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Name, item.Name);
        }

        [TestMethod]
        public void PutUser_ShouldReturnStatusCode()
        {
            var controller = new UserController(new TestRestAPIContext());

            var item = GetDemoUser();

            var result = controller.PutUser(item.Id, item) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void PutUser_ShouldFail_WhenDifferentID()
        {
            var controller = new UserController(new TestRestAPIContext());

            var badresult = controller.PutUser(999, GetDemoUser());
            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetUser_ShouldReturnUserWithSameID()
        {
            var context = new TestRestAPIContext();
            context.Users.Add(GetDemoUser());

            var controller = new UserController(context);
            var result = controller.GetUser(3) as OkNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.Id);
        }

        [TestMethod]
        public void GetUsers_ShouldReturnAllUsers()
        {
            var context = new TestRestAPIContext();
            context.Users.Add(new User { Id = 1, Name = "Demo1", Surname = "DemoApellido1", Password = "1234", Active = true, Email = "mail1@dominio.com" });
            context.Users.Add(new User { Id = 2, Name = "Demo2", Surname = "DemoApellido2", Password = "12345", Active = false, Email = "mail2@dominio.com" });
            context.Users.Add(new User { Id = 3, Name = "Demo3", Surname = "DemoApellido3", Password = "123456", Email = "mail3@dominio.com" });

            var controller = new UserController(context);
            var result = controller.GetUsers() as TestUserDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void DeleteUser_ShouldReturnOK()
        {
            var context = new TestRestAPIContext();
            var item = GetDemoUser();
            context.Users.Add(item);

            var controller = new UserController(context);
            var result = controller.DeleteUser(3) as OkNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.Id, result.Content.Id);
        }

        User GetDemoUser()
        {
            return new User() { Id = 3, Name = "Demo3", Surname = "DemoApellido 3" };
        }
    }
}
