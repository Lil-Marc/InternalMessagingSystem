using InternalMessagingSystem.Version1._1.Models;
using InternalMessagingSystem.Version1._1.Services;

namespace InternalMessagingSystem.Version1._1.Services;

public interface IMessageService
{
    void SendMessage(User sender, User receiver, string content);
    List<Message> GetMessages(User user);
    void DeleteMessage(User user, int messageId);
    List<Message> GetMessagesSortedByTimestamp(User user);
}