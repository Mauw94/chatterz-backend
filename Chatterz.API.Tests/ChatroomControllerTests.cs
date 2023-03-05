using Chatterz.API.Controllers;
using Chatterz.API.Manages.Interfaces;
using Chatterz.Domain.DTO;
using Chatterz.Domain.Models;
using Chatterz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Chatterz.API.Tests;

[TestClass]
public class ChatroomControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<ISignalRManager> _signalRManager;
    private readonly Mock<IChatroomService> _chatroomServiceMock;
    private readonly Mock<IService<ChatMessage>> _chatMessageServiceMock;
    private readonly ChatroomController _chatroomController;

    public ChatroomControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _signalRManager = new Mock<ISignalRManager>();
        _chatMessageServiceMock = new Mock<IService<ChatMessage>>();
        _chatroomServiceMock = new Mock<IChatroomService>();
        _chatroomController = new ChatroomController(
            _userServiceMock.Object,
            _chatroomServiceMock.Object,
            _signalRManager.Object,
            _chatMessageServiceMock.Object);
    }

    [TestMethod]
    public async Task Join_Chatroom_Returns_Ok()
    {
        // arrange
        var connectionId = Guid.NewGuid().ToString();
        var chatroomJoinDto = GetChatroomDto(connectionId);
        var testUser = GetTestUser(connectionId);

        _userServiceMock.Setup(u => u.GetAsync(1)).ReturnsAsync(testUser);

        // act
        var result = await _chatroomController.Join(chatroomJoinDto);

        // assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ActionResult));
    }

    [TestMethod]
    public async Task Leave_Chatroom_Returns_Ok()
    {
        // arrange
        var connectionId = Guid.NewGuid().ToString();
        var chatroomJoinDto = GetChatroomDto(connectionId);
        var testUser = GetTestUser(connectionId);

        _userServiceMock.Setup(u => u.GetAsync(1)).ReturnsAsync(testUser);

        // act
        var result = await _chatroomController.Leave(chatroomJoinDto);

        // assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ActionResult));
    }

    [TestMethod]
    public async Task Create_Chatroom_Returns_Ok_With_Generated_Id()
    {
        // arrange

        // act
        var result = await _chatroomController.Create();

        // assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ActionResult<int>));
    }

    private ChatroomJoinDto GetChatroomDto(string connectionId)
    {
        return new ChatroomJoinDto()
        {
            ChatroomId = 1,
            ConnectionId = connectionId,
            UserId = 1
        };
    }

    private User GetTestUser(string connectionId)
    {
        return new User()
        {
            Id = 1,
            ChatroomId = 1,
            ConnectionId = connectionId,
            IsLoggedIn = true,
            UserName = "Test",
            Password = "123",
        };
    }
}