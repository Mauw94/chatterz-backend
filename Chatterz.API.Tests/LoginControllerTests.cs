using Chatterz.API.Controllers;
using Chatterz.Domain.Models;
using Chatterz.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Chatterz.HUBS;

namespace Chatterz.API.Tests
{
    [TestClass]
    public class LoginControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IChatroomService> _chatroomServiceMock;
        private readonly Mock<IHubContext<ChatHub>> _hubContextMock;
        private readonly LoginController _loginController;

        public LoginControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _chatroomServiceMock = new Mock<IChatroomService>();
            _hubContextMock = new Mock<IHubContext<ChatHub>>();
            _loginController = new LoginController(
                _userServiceMock.Object,
                _chatroomServiceMock.Object,
                _hubContextMock.Object);
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
    }
}