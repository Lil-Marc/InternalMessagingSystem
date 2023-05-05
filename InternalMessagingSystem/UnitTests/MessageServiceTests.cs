using InternalMessagingSystem.Services;
using Xunit;

namespace InternalMessagingSystem.UnitTests;

public class MessageServiceTests
{
    private readonly MessageService _messageService;
    private readonly User _sender;
    private readonly User _receiver;
    
    public MessageServiceTests()
    {
        var messageStore = new MessageStore();
        _messageService = new MessageService(messageStore);
        _sender = new User { Id = 1, UserName = "Sender" };
        _receiver = new User { Id = 2, UserName = "Receiver" };
    }
    
    // This test checks that when SendMessage is called, it adds a message to the receiver's messages.
    [Fact]
    public void SendMessage_WhenCalled_AddsMessageToReceiverMessages()
    {
        _messageService.SendMessage(_sender, _receiver, "Hello!");

        var messages = _messageService.GetMessages(_receiver);
        Assert.Single(messages);
        Assert.Equal("Hello!", messages[0].Content);
        Assert.Equal(_sender.Id, messages[0].SenderId);
    }
    
    // This test checks that GetMessages returns the receiver's messages.
    [Fact]
    public void GetMessages_WhenCalled_ReturnsReceiverMessages()
    {
        _messageService.SendMessage(_sender, _receiver, "Hello!");

        var messages = _messageService.GetMessages(_receiver);
        Assert.Single(messages);
        Assert.Equal("Hello!", messages[0].Content);
        Assert.Equal(_sender.Id, messages[0].SenderId);
    }

    // This test checks that DeleteMessage removes the message from the receiver's messages.
    [Fact]
    public void DeleteMessage_WhenCalled_RemovesMessageFromReceiverMessages()
    {
        // Arrange
        _messageService.SendMessage(_sender, _receiver, "Hello!");
        var messages = _messageService.GetMessages(_receiver);
        var messageId = messages[0].Id;

        // Act
        _messageService.DeleteMessage(_receiver, messageId);

        // Assert
        messages = _messageService.GetMessages(_receiver);
        Assert.Empty(messages);  // Verify that the message list is now empty
    }
    
    // This test verifies that when DeleteMessage is called with a user that does not exist,
    // it throws an ArgumentException with an appropriate error message.
    [Fact]
    public void DeleteMessage_WhenUserNotFound_ThrowsArgumentException()
    {
        // Arrange
        User nonExistentUser = new User { Id = 2 };
        int nonExistentMessageId = 2;

        // Act and Assert
        ArgumentException ex = Assert.Throws<ArgumentException>(() => _messageService.DeleteMessage(nonExistentUser, nonExistentMessageId));
        Assert.Equal("User not found.", ex.Message);
    }

    // This test checks that GetMessagesSortedByTimestamp returns messages in order of timestamp.
    [Fact]
    public void GetMessagesSortedByTimestamp_ReturnsMessagesInOrder()
    {
        // Arrange
        var receiver = new User { Id = 2, UserName = "Receiver" };
        var sender   = new User { Id = 1, UserName = "Sender" };
        var messageStore = new MessageStore();
        var messageService = new MessageService(messageStore);

        // Add some messages with timestamps
        var now = DateTime.UtcNow;
        
        // Simulate sending messages with different timestamps
        var msg1 = new Message { Id = 1, Content = "Hello!", Timestamp = now };
        var msg2 = new Message { Id = 2, Content = "Hi!", Timestamp = now.AddSeconds(1) };
        var msg3 = new Message { Id = 3, Content = "How are you?", Timestamp = now.AddSeconds(2) };
        var msg4 = new Message { Id = 4, Content = "I'm good, thanks.", Timestamp = now.AddSeconds(3) };

        // Simulate adding messages to the receiver's list
        messageStore.SendMessage(sender,receiver, msg1);
        messageStore.SendMessage(sender,receiver, msg2);
        messageStore.SendMessage(sender,receiver, msg3);
        messageStore.SendMessage(sender,receiver, msg4);

        // Act
        var sortedMessages = messageService.GetMessagesSortedByTimestamp(receiver);

        // Assert
        Assert.Equal(4, sortedMessages.Count);

        Assert.Equal("Hello!", sortedMessages[0].Content);
        Assert.Equal(sender.Id, sortedMessages[0].SenderId);
        Assert.Equal(receiver.Id, sortedMessages[0].ReceiverId);
        Assert.Equal(now, sortedMessages[0].Timestamp);

        Assert.Equal("Hi!", sortedMessages[1].Content);
        Assert.Equal(sender.Id, sortedMessages[1].SenderId);
        Assert.Equal(receiver.Id, sortedMessages[1].ReceiverId);
        Assert.Equal(now.AddSeconds(1), sortedMessages[1].Timestamp);

        Assert.Equal("How are you?", sortedMessages[2].Content);
        Assert.Equal(sender.Id, sortedMessages[2].SenderId);
        Assert.Equal(receiver.Id, sortedMessages[2].ReceiverId);
        Assert.Equal(now.AddSeconds(2), sortedMessages[2].Timestamp);

        Assert.Equal("I'm good, thanks.", sortedMessages[3].Content);
        Assert.Equal(sender.Id, sortedMessages[3].SenderId);
        Assert.Equal(receiver.Id, sortedMessages[3].ReceiverId);
        Assert.Equal(now.AddSeconds(3), sortedMessages[3].Timestamp);
    }
}