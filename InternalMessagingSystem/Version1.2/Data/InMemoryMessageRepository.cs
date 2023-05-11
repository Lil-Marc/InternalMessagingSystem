using InternalMessagingSystem.Version1._2.Entities;
using InternalMessagingSystem.Version1._2.Interfaces;

namespace InternalMessagingSystem.Version1._2.Data;

/// <summary>
/// An in-memory implementation of the IMessageRepository interface.
/// </summary>
public class InMemoryMessageRepository : IMessageRepository
{
    private readonly Dictionary<int, List<Message>> messages = new();
    private int currentMessageId = 1;

    public void Save(Message message)
    {
        message.Id = currentMessageId++;
        if (!messages.ContainsKey(message.ReceiverId))
        {
            messages[message.ReceiverId] = new List<Message>();
        }

        messages[message.ReceiverId].Add(message);
    }

    public List<Message> GetMessagesByUser(int userId)
    {
        return messages.ContainsKey(userId) ? messages[userId] : new List<Message>();
    }

    public void Delete(int userId, int messageId)
    {
        if (messages.ContainsKey(userId))
        {
            var message = messages[userId].FirstOrDefault(m => m.Id == messageId);
            if (message != null)
            {
                messages[userId].Remove(message);
            }
            else
            {
                throw new ArgumentException("Message not found");
            }
        }
        else
        {
            throw new ArgumentException("User not found");
        }
    }

}