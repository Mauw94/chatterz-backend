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
    private readonly Mock<IChatMessageService> _chatMessageServiceMock;
    private readonly ChatroomController _chatroomController;

    public ChatroomControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _signalRManager = new Mock<ISignalRManager>();
        _chatMessageServiceMock = new Mock<IChatMessageService>();
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
        Assert.IsInstanceOfType(result, typeof(OkResult));
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
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task Create_Chatroom_Returns_Ok_With_Generated_Id_Calls_Service_GetAllWithUsers_AddChatroomAsync()
    {
        // arrange

        // act
        var result = await _chatroomController.Create();

        // assert
        _chatroomServiceMock.Verify(s => s.GetAllWithUsers(), Times.Once);
        _chatroomServiceMock.Verify(s => s.AddChatroomAsync(), Times.Once);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ActionResult<int>));
    }

    [TestMethod]
    public async Task Remove_Chatroom_Returns_Ok_Calls_Service_RemoveChatroom()
    {
        // arrange
        var chatroomId = 1;

        // act
        var result = await _chatroomController.Remove(1);

        // assert
        _chatroomServiceMock.Verify(s => s.RemoveChatroom(chatroomId), Times.Once);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task GetAll_Returns_Ok_Calls_Service_GetAllWithUsers()
    {
        // arrange

        // act
        var result = await _chatroomController.GetAll();

        // assert
        _chatroomServiceMock.Verify(s => s.GetAllWithUsers(), Times.Once);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ActionResult<List<Chatroom>>));
    }

    [TestMethod]
    public async Task GetChatHistory_Returns_Ok_Calls_Service_GetChatHistory()
    {
        // arrange
        var chatroomId = 1;

        // act
        var result = await _chatroomController.GetChatHistory(chatroomId);

        // assert
        _chatMessageServiceMock.Verify(s => s.GetChatHistory(chatroomId), Times.Once);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ActionResult<List<ChatMessage>>));
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