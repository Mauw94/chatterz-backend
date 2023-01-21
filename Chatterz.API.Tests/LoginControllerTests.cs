using Chatterz.API.Controllers;
using Chatterz.API.InMemoryDb;
using Chatterz.Domain;
using Chatterz.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Chatterz.API.Tests
{
    [TestClass]
    public class LoginControllerTests
    {
        private readonly LoginController _loginController;
        private readonly IUsersDb _usersDb;

        public LoginControllerTests()
        {
            _usersDb = new UsersDb();
            _loginController = new LoginController(_usersDb);
        }

        [TestMethod]
        public void Test_Login_Returns_LoggedIn_Users()
        {
            // arrange
            var userLoginInfo = new UserLoginDto()
            {
                UserName = "john",
                Password = "123"
            };

            // act
            var result = _loginController.Login(userLoginInfo);

            // asert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<User>));
        }
    }
}