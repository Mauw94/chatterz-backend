using Chatterz.API.Controllers;
using Chatterz.API.Manages.Interfaces;
using Chatterz.Domain.DTO;
using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Chatterz.API.Tests
{
    [TestClass]
    public class LoginControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ISignalRManager> _signalRManager;
        private readonly LoginController _loginController;

        public LoginControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _signalRManager = new Mock<ISignalRManager>();

            _loginController = new LoginController(
                _userServiceMock.Object,
                _signalRManager.Object
                );
        }

        [TestMethod]
        public async Task Test_Login_Returns_LoggedIn_Users()
        {
            // arrange
            var userLoginInfo = new UserLoginDto()
            {
                UserName = "john",
                Password = "123"
            };

            // act
            var result = await _loginController.Login(userLoginInfo);

            // asert
            _userServiceMock.Verify(s => s.Login(userLoginInfo.UserName, userLoginInfo.Password), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<User>));
        }

        [TestMethod]
        public async Task Logout_Logs_User_Out()
        {
            // arrange

            var userId = 1;
            var connectionId = Guid.NewGuid().ToString();
            var testUser = new User()
            {
                Id = 1,
                ChatroomId = 1,
                ConnectionId = connectionId,
                IsLoggedIn = true,
                UserName = "Test",
                Password = "123"
            };
            _userServiceMock.Setup(u => u.GetAsync(userId)).ReturnsAsync(testUser);

            // act
            var result = await _loginController.Logout(userId, connectionId);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
    }
}