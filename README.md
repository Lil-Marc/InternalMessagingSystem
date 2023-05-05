# Internal Messaging System Version1.1📱

The Internal Messaging System is a simple yet effective system that allows users to send, receive, and delete messages. It features a `User` class to define users, a `Message` class to define messages, a `MessageStore` class to store and manage messages, and a `MessageService` class to provide an interface for users to interact with the system.

## Preface 🔥

After completing all the features I didn't think it was a complex project, so I did an iteration to make the project as readable and scalable as I could, and I will briefly explain my main improvements and thoughts on the project

### Code Comments（readable）
I add more detailed comments to sub-code to help other developers understand my code more easily. This includes classes, methods and possibly complex sections

### Encapsulation（scalable）
I did a version iteration after I finished implementing all the features and I created a MessageStore
And I moved `_messageStorage` and `_messageIdCounter` to this new class

The MessageService will use the MessageStore to manage the message storage instead of directly managing `_messageStorage`  and `_messageIdCounter`. 
The advantage of this is that if we want to change the message storage implementation later (for example, storing messages in the database instead of in memory),
we only need to modify the MessageStore and not the MessageService.


#### Here I draw on the ideas of several design patterns
1：Dependency Injection: In this pattern, an object (in this case MessageService) does not directly create the object it depends on (in this case MessageStore), 
but receives these dependencies through constructors, methods, or properties. Dependency injection makes code more flexible, easier to test, and reduces coupling between codes.

2: Single Responsibility Principle (SOLID): This is one of the SOLID design principles which suggests that each class should have only one reason for change.
Here, we separate the responsibilities of MessageService (handling the sending and receiving of messages) and MessageStore (managing the storage of messages) to make the responsibilities of each class clearer.

3:Layered Design: In this design pattern, software is organized into multiple layers,
each layer providing services to the layer above it and using the services of the layer below it. 
In this case, MessageService is the business logic layer, which uses the data access layer services provided by MessageStore.

4:Strategy Pattern: If we decide to use MessageStore as an interface instead of a concrete class, we can replace different implementations of MessageStore as needed (for example, a version that uses memory, and a version that uses the database).
This is an application of the policy pattern, allowing we to change the algorithm or policy at runtime as needed.

### Exception Handling❎
I add more detailed exception handling to the  `MessageService` methods to better handle potential error situations, such as a preliminary determination of whether the id is reasonable

### Code scalablility
Consider adding some additional properties to the Message class, such as a DateTime timestamp, to keep track of when messages are sent, and to sort incoming messages by time, this is a very simple improvement but it is an inspiration for my subsequent improvements


## Usage


### User👨

The `User` class is used to represent users in the system. It has two properties: `Id` and `UserName`.

```
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
}
```

### Message📨

The `Message` class is used to represent messages in the system. It has five properties: `Id`, `SenderId`, `ReceiverId`, `Content`, and `Timestamp`.

```
public class Message
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}
```

### MessageStore

The `MessageStore` class stores and manages messages. It provides methods to send a message, get messages for a user, delete a message for a user, and get messages for a user sorted by timestamp.

```

public class MessageStore
{
    // Methods:
    // SendMessage(User sender, User receiver, string content)
    // SendMessage(User sender, User receiver, Message message)
    // GetMessages(User user)
    // DeleteMessage(User user, int messageId)
    // GetMessagesSortedByTimestamp(User user)
}
```

### MessageService

The `MessageService` class provides an interface for users to interact with the system. It uses a `MessageStore` to manage messages and provides methods to send a message, get messages for a user, delete a message for a user, and get messages for a user sorted by timestamp.

```

public class MessageService
{
    // Methods:
    // SendMessage(User sender, User receiver, string content)
    // GetMessages(User user)
    // DeleteMessage(User user, int messageId)
    // GetMessagesSortedByTimestamp(User user)
}
```

## Testing

The system also includes a set of unit tests to verify its functionality. For example, here is a test that verifies the `GetMessagesSortedByTimestamp` method:

```

[Fact]
public void GetMessagesSortedByTimestamp_ReturnsMessagesInOrder()
{
    // Arrange

    // Act

    // Assert
}
```
