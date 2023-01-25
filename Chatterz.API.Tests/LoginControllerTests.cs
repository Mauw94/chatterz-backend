using Chatterz.API.Controllers;
using Chatterz.Domain.Models;
using Chatterz.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Chatterz.Services.Interfaces;

namespace Chatterz.API.Tests
{
    [TestClass]
    public class LoginControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly LoginController _loginController;

        public LoginControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _loginController = new LoginController(_userServiceMock.Object);
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
        public async Task Test_Logout_LogsUserOut()
        {
            // arrange
            var id = 1;

            // act
            var res = await _loginController.Logout(id);

            // assert
            _userServiceMock.Verify(s => s.Logout(id), Times.Once);
            Assert.IsInstanceOfType(res, typeof(OkResult));
        }
    }
}