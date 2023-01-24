using Chatterz.API.Controllers;
using Chatterz.Domain.Models;
using Chatterz.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Chatterz.API.Tests
{
    [TestClass]
    public class LoginControllerTests
    {
        private readonly LoginController _loginController;

        public LoginControllerTests()
        {
            // _loginController = new LoginController(_usersDb);
        }

        [TestMethod]
        public void Test_Login_Returns_LoggedIn_Users()
        {
            // TODO: mock service and inject in controller
            // check if methods were called etc

            // arrange
            // var userLoginInfo = new UserLoginDto()
            // {
            //     UserName = "john",
            //     Password = "123"
            // };

            // // act
            // var result = _loginController.Login(userLoginInfo);

            // // asert
            // Assert.IsNotNull(result);
            // Assert.IsInstanceOfType(result, typeof(ActionResult<User>));
        }
    }
}