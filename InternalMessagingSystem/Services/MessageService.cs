namespace InternalMessagingSystem.Services;

public class MessageService : IMessageService
{
    private readonly MessageStore _messageStore;

    public MessageService(MessageStore messageStore)
    {
        _messageStore = messageStore;
    }

    /// <summary>
    /// Sends a message from one user to another.
    /// </summary>
    /// <param name="sender">The user who sends the message.</param>
    /// <param name="receiver">The user who will receive the message.</param>
    /// <param name="content">The content of the message.</param>
    /// /// <exception cref="ArgumentException">Thrown when sender or receiver is null or their IDs are not valid.</exception>
    public void SendMessage(User sender, User receiver, string content)
    {
        if (sender == null)
        {
            throw new ArgumentException("Sender cannot be null.", nameof(sender));
        }

        if (receiver == null)
        {
            throw new ArgumentException("Receiver cannot be null.", nameof(receiver));
        }

        if (sender.Id <= 0)
        {
            throw new ArgumentException("Sender ID is not valid.", nameof(sender));
        }

        if (receiver.Id <= 0)
        {
            throw new ArgumentException("Receiver ID is not valid.", nameof(receiver));
        }
        
        _messageStore.SendMessage(sender, receiver, content);;
    }

    /// <summary>
    /// Gets the list of messages for a user.
    /// </summary>
    /// <param name="user">The user to get the messages for.</param>
    /// <returns>A list of messages, or an empty list if the user has no messages.</returns>
    /// /// <exception cref="ArgumentException">Thrown when user is null or its ID is not valid.</exception>
    public List<Message> GetMessages(User user)
    {
        if (user == null)
        {
            throw new ArgumentException("User cannot be null.", nameof(user));
        }

        if (user.Id <= 0)
        {
            throw new ArgumentException("User ID is not valid.", nameof(user));
        }

        return _messageStore.GetMessages(user);
    }

    /// <summary>
    /// Deletes a message for a user.
    /// </summary>
    /// <param name="user">The user to delete the message for.</param>
    /// <param name="messageId">The id of the message to delete.</param>
    /// <exception cref="ArgumentException">Thrown when user is null or its ID is not valid, or when the message ID is not valid or the message is not found.</exception>
    public void DeleteMessage(User user, int messageId)
    {
        if (user == null)
        {
            throw new ArgumentException("User cannot be null.", nameof(user));
        }

        if (user.Id <= 0)
        {
            throw new ArgumentException("User ID is not valid.", nameof(user));
        }

        if (messageId <= 0)
        {
            throw new ArgumentException("Message ID is not valid.", nameof(messageId));
        }
        
        _messageStore.DeleteMessage(user, messageId);
    }
    
    /// <summary>
    /// Gets a list of messages for a user sorted by timestamp.
    /// </summary>
    /// <param name="user">The user to get the messages for.</param>
    /// <returns>A list of messages, sorted by timestamp.</returns>
    /// <exception cref="ArgumentException">Thrown when user is null or its ID is not valid.</exception>
    public List<Message> GetMessagesSortedByTimestamp(User user)
    {
        if (user == null)
        {
            throw new ArgumentException("User cannot be null.", nameof(user));
        }

        if (user.Id <= 0)
        {
            throw new ArgumentException("User ID is not valid.", nameof(user));
        }
        return _messageStore.GetMessagesSortedByTimestamp(user);
    }
}
