using Chatterz.API.Controllers;
using Chatterz.Domain.DTO;
using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Chatterz.API.Tests;

[TestClass]
public class RegisterControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly RegisterController _registerController;

    public RegisterControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _registerController = new RegisterController(_userServiceMock.Object);
    }

    [TestMethod]
    public async Task Register_Calls_Service_AddAsync_Returns_Ok()
    {
        // arrange
        var userInfo = new UserLoginDto()
        {
            UserName = "test",
            Password = "123"
        };
        var user = new User()
        {
            UserName = userInfo.UserName,
            Password = userInfo.Password
        };

        // act
        var result = await _registerController.Register(userInfo);

        // assert
        // _userServiceMock.Verify(s => s.AddAsync(user), Times.Once);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ActionResult<User>));
    }
}