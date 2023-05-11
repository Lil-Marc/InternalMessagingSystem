using InternalMessagingSystem.Version1._2.Entities;

namespace InternalMessagingSystem.Version1._2.Interfaces;

public interface IMessageRepository
{
    /// <summary>
    /// Saves a new message.
    /// </summary>
    /// <param name="message">The message to save.</param>
    void Save(Message message);

    /// <summary>
    /// Retrieves all messages for a specific user.
    /// </summary>
    /// <param name="userId">The id of the user to retrieve the messages for.</param>
    /// <returns>A list of messages.</returns>
    List<Message> GetMessagesByUser(int userId);

    /// <summary>
    /// Deletes a specific message for a user.
    /// </summary>
    /// <param name="userId">The id of the user to delete the message for.</param>
    /// <param name="messageId">The id of the message to delete.</param>
    void Delete(int userId, int messageId);
}