using InternalMessagingSystem.Version1._1.Models;

namespace InternalMessagingSystem.Version1._1.Services;

/// <summary>
/// A class to manage the storage of messages in a dictionary.
/// The advantage of this is that if we want to change the message store implementation later
/// (for example, to store messages in the database instead of in memory), we only need to modify the MessageStore and not the MessageService.
/// </summary>
public class MessageStore
{
    /*
     A dictionary to store the messages, the key is the receiver's user id
     and the value is a list of messages for that user.
     */
    private readonly Dictionary<int, List<Message>> _messageStorage = new Dictionary<int, List<Message>>();
    
    // A counter to assign unique ids to the messages.
    private int _messageIdCounter = 1;

    /// <summary>
    /// Sends a message from one user to another.
    /// </summary>
    /// <param name="sender">The user who sends the message.</param>
    /// <param name="receiver">The user who will receive the message.</param>
    /// <param name="content">The content of the message.</param>
    public Message SendMessage(User sender, User receiver, string content)
    {
        var message = new Message
        {
            Id = _messageIdCounter++,
            SenderId = sender.Id,
            ReceiverId = receiver.Id,
            Content = content,
            Timestamp = DateTime.Now 
        };

        // Store message for the receiver
        if (_messageStorage.ContainsKey(receiver.Id))
        {
            _messageStorage[receiver.Id].Add(message);
        }
        else
        {
            _messageStorage[receiver.Id] = new List<Message> { message };
        }
        

        return message;
    }
    
    public Message SendMessage(User sender, User receiver, Message message)
    {
        // Update the properties of the message based on the sender and receiver
        message.Id = _messageIdCounter++;
        message.SenderId = sender.Id;
        message.ReceiverId = receiver.Id;

        // Store message for the receiver
        if (_messageStorage.ContainsKey(receiver.Id))
        {
            _messageStorage[receiver.Id].Add(message);
        }
        else
        {
            _messageStorage[receiver.Id] = new List<Message> { message };
        }

        return message;
    }
    
    /// <summary>
    /// Gets the list of messages for a user.
    /// </summary>
    /// <param name="user">The user to get the messages for.</param>
    /// <returns>A list of messages, or an empty list if the user has no messages.</returns>
    public List<Message> GetMessages(User user)
    {
        if (_messageStorage.ContainsKey(user.Id))
        {
            return _messageStorage[user.Id];
        }
        else
        {
            return new List<Message>();
        }
    }
    
    /// <summary>
    /// Deletes a message for a user.
    /// </summary>
    /// <param name="user">The user to delete the message for.</param>
    /// <param name="messageId">The id of the message to delete.</param>
    public void DeleteMessage(User user, int messageId)
    {
        if (_messageStorage.ContainsKey(user.Id))
        {
            var messages = _messageStorage[user.Id];
            var messageToRemove = messages.FirstOrDefault(m => m.Id == messageId);

            if (messageToRemove != null)
            {
                messages.Remove(messageToRemove);
            }
            else
            {
                throw new ArgumentException("Message not found.");
            }
        }
        else
        {
            throw new ArgumentException("User not found.");
        }
    }
    
    /// <summary>
    /// Gets a list of messages for a user sorted by timestamp.
    /// </summary>
    /// <param name="user">The user to get the messages for.</param>
    /// <returns>A list of messages, sorted by timestamp.</returns>
    public List<Message> GetMessagesSortedByTimestamp(User user)
    {
        if (!_messageStorage.ContainsKey(user.Id))
        {
            return new List<Message>();
        }

        var messages = _messageStorage[user.Id];
        // Filter messages based on the user being the receiver
        return messages.Where(m => m.ReceiverId == user.Id).OrderBy(m => m.Timestamp).ToList();
    }
}
