using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using RuiRumos74252.Controllers;
using RuiRumos74252.Data;
using RuiRumos74252.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class UsersControllerTests
    {
        [Fact]
        public void ListUsers_ReturnsViewWithUsers()
        {
            // Arrange
            var users = new List<IdentityUser>
            {
                new IdentityUser { Id = "1", UserName = "user1" },
                new IdentityUser { Id = "2", UserName = "user2" }
            };

            var userManagerMock = MockUserManager(users);

            var controller = new UsersController(null, userManagerMock.Object);

            // Act
            var result = controller.ListUsers() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as List<IdentityUser>;
            Assert.NotNull(model);
            Assert.Equal(users.Count, model.Count);
        }

        private Mock<UserManager<IdentityUser>> MockUserManager(List<IdentityUser> users)
        {
            var userManagerMock = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

            userManagerMock.Setup(m => m.Users)
                .Returns(users.AsQueryable());

            userManagerMock.Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                .Returns((string id) => Task.FromResult(users.FirstOrDefault(u => u.Id == id)));

            userManagerMock.Setup(m => m.UpdateAsync(It.IsAny<IdentityUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            userManagerMock.Setup(m => m.DeleteAsync(It.IsAny<IdentityUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            return userManagerMock;
        }
    }
}

