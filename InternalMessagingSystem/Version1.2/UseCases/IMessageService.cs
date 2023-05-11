using InternalMessagingSystem.Version1._2.Entities;

namespace InternalMessagingSystem.Version1._2.UseCases;

public interface IMessageService
{
    // Methods of sending messages
    Message SendMessage(int senderId, int receiverId, string content);

    // Get all messages from the user
    List<Message> GetMessages(int userId);

    // Delete a specific message
    void DeleteMessage(int userId, int messageId);

    // Get all messages from users sorted by timestamp
    List<Message> GetMessagesSortedByTimestamp(int userId);
}