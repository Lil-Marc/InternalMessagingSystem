using InternalMessagingSystem.Version1._2.Controllers;
using InternalMessagingSystem.Version1._2.Entities;
using InternalMessagingSystem.Version1._2.UseCases;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InternalMessagingSystem.Version1._2.Tests.UnitTests;

public class MessageControllerTests
{
    private readonly MessageController _messageController;
    private readonly Mock<IMessageService> _messageServiceMock;

    public MessageControllerTests()
    {
        _messageServiceMock = new Mock<IMessageService>();
        _messageController = new MessageController(_messageServiceMock.Object);
    }

    [Fact]
    public void SendMessage_ReturnsOkResult()
    {
        // Arrange
        _messageServiceMock.Setup(m => m.SendMessage(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Message(1, 2, "Test message", DateTime.Now));

        // Act
        var result = _messageController.SendMessage(new MessageDto {SenderId = 1, ReceiverId = 2, Content = "Test message"});

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<Message>(okResult.Value);
        Assert.Equal(1, returnValue.SenderId);
        Assert.Equal(2, returnValue.ReceiverId);
        Assert.Equal("Test message", returnValue.Content);
    }
    
    [Fact]
    public void GetMessages_ReturnsOkResult()
    {
        // Arrange
        _messageServiceMock.Setup(m => m.GetMessages(It.IsAny<int>()))
            .Returns(new List<Message>
            {
                new Message(1, 3, "Test message 1", DateTime.Now),
                new Message(2, 3, "Test message 2", DateTime.Now)
            });

        // Act
        var result = _messageController.GetMessages(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<Message>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
    
    [Fact]
    public void DeleteMessage_ValidData_MethodCalled()
    {
        // Arrange
        var userId = 1;
        var messageId = 1;
    
        _messageServiceMock.Setup(m => m.DeleteMessage(userId, messageId));
    
        var controller = new MessageController(_messageServiceMock.Object);

        // Act
        controller.DeleteMessage(userId, messageId);

        // Assert
        _messageServiceMock.Verify(m => m.DeleteMessage(userId, messageId), Times.Once);
    }

    
    [Fact]
    public void GetMessagesSortedByTimestamp_ReturnsOkResult()
    {
        // Arrange
        _messageServiceMock.Setup(m => m.GetMessagesSortedByTimestamp(It.IsAny<int>()))
            .Returns(new List<Message>
            {
                new Message(1, 2, "Test message 1", DateTime.Now.AddHours(-1)),
                new Message(2, 1, "Test message 2", DateTime.Now)
            });

        // Act
        var result = _messageController.GetMessagesSortedByTimestamp(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<Message>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
        Assert.True(returnValue[0].Timestamp < returnValue[1].Timestamp);
    }
    
    
}