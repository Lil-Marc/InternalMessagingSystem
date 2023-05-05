namespace InternalMessagingSystem.Services;

public interface IMessageService
{
    void SendMessage(User sender, User receiver, string content);
    List<Message> GetMessages(User user);
    void DeleteMessage(User user, int messageId);
    List<Message> GetMessagesSortedByTimestamp(User user);
}