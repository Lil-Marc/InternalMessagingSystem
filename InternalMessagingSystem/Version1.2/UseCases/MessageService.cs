using InternalMessagingSystem.Version1._2.Entities;
using InternalMessagingSystem.Version1._2.Interfaces;

namespace InternalMessagingSystem.Version1._2.UseCases;

public class MessageService : IMessageService
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageRepository _messageRepository;

    public MessageService(IUserRepository userRepository, IMessageRepository messageRepository)
    {
        _userRepository = userRepository;
        _messageRepository = messageRepository;
    }
    /// <summary>
    /// Sends a message from one user to another.
    /// </summary>
    /// <param name="senderId">The id of the user sending the message.</param>
    /// <param name="receiverId">The id of the user receiving the message.</param>
    /// <param name="content">The content of the message.</param>
    /// <returns>The sent message.</returns>
    public Message SendMessage(int senderId, int receiverId, string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException("Content cannot be null or empty", nameof(content));
        }

        // Ensure both sender and receiver exist
        if (!_userRepository.Exists(senderId) || !_userRepository.Exists(receiverId))
        {
            throw new ArgumentException("Either sender or receiver does not exist");
        }

        var message = new Message(senderId, receiverId, content, DateTime.UtcNow);
        _messageRepository.Save(message);

        return message;
    }
    
    /// <summary>
    /// Retrieves all messages for a specific user.
    /// </summary>
    /// <param name="userId">The id of the user to retrieve the messages for.</param>
    /// <returns>A list of messages.</returns>
    public List<Message> GetMessages(int userId)
    {
        if (!_userRepository.Exists(userId))
        {
            throw new ArgumentException("User does not exist");
        }

        return _messageRepository.GetMessagesByUser(userId);
    }
    
    /// <summary>
    /// Deletes a specific message for a user.
    /// </summary>
    /// <param name="userId">The id of the user to delete the message for.</param>
    /// <param name="messageId">The id of the message to delete.</param>
    public void DeleteMessage(int userId, int messageId)
    {
        if (!_userRepository.Exists(userId))
        {
            throw new ArgumentException("User does not exist");
        }

        _messageRepository.Delete(userId, messageId);
    }

    /// <summary>
    /// Retrieves all messages for a specific user, sorted by timestamp.
    /// </summary>
    /// <param name="userId">The id of the user to retrieve the messages for.</param>
    /// <returns>A list of messages, sorted by timestamp.</returns>
    public List<Message> GetMessagesSortedByTimestamp(int userId)
    {
        var messages = GetMessages(userId);
        return messages.OrderBy(m => m.Timestamp).ToList();
    }
}