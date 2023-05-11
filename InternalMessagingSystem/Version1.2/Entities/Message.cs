using Newtonsoft.Json;

namespace InternalMessagingSystem.Version1._2.Entities;

public class Message
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }

    [JsonConstructor]
    public Message(int senderId, int receiverId, string content, DateTime timestamp)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content;
        Timestamp = timestamp;
    }
    
    public Message()
    {

    }
}