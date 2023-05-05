# Internal Messaging System Version1.1📱

The Internal Messaging System is a simple yet effective system for enabling users to send, receive, and delete messages. It features a `User` class for defining users, a `Message` class for defining messages, a `MessageStore` class for storing and managing messages, and a `MessageService` class to provide an interface for users to interact with the system.

## Preface 🔥

After implementing all the features, I undertook an iteration to make the project as readable and scalable as possible. Below, I will briefly explain my main improvements and the thought processes behind them.

### Code Comments (Readability) 📝

To facilitate the understanding of my code by other developers, I have added detailed comments to the classes, methods, and potentially complex sections of my project. Moreover, all variable names follow the camelCase naming convention, enhancing the readability and consistency throughout the codebase.

### Encapsulation (Scalability) 🚀

After implementing all the features, I iterated on the version and created a `MessageStore` class. I moved `_messageStorage` and `_messageIdCounter` to this new class. Now, `MessageService` uses `MessageStore` to manage the message storage instead of directly managing `_messageStorage` and `_messageIdCounter`. This means that if we wish to alter the message storage implementation later (e.g., storing messages in a database instead of in-memory), we only need to modify `MessageStore`, not `MessageService`.

#### Below, I outline the design patterns that guided my thinking:

1: Dependency Injection: This design pattern involves an object receiving its dependencies through constructors, methods, or properties, instead of directly creating them. In this project, `MessageService` doesn't directly create its dependency on `MessageStore`. Instead, it receives this dependency through its constructor. Dependency injection enhances code flexibility, simplifies testing, and reduces code coupling.

2: Single Responsibility Principle (SOLID): This principle states that each class should only have one reason to change. In this project, I separated the responsibilities of `MessageService` (managing the sending and receiving of messages) and `MessageStore` (managing the storage of messages), which clarifies the responsibilities of each class.

3: Layered Design: In this design pattern, software is organized into multiple layers. Each layer provides services to the layer above it and uses the services of the layer below it. In this project, `MessageService` acts as the business logic layer and utilizes the data access layer services provided by `MessageStore`.

4: Strategy Pattern: By treating `MessageStore` as an interface instead of a concrete class, we gain the flexibility to replace different `MessageStore` implementations as required. For instance, we can have a version that stores messages in-memory and another that uses a database. This is an application of the Strategy pattern, which allows us to change the algorithm or policy at runtime.

### Exception Handling ❎

I have incorporated detailed exception handling into `MessageService` methods to better manage potential error situations, such as making a preliminary determination of whether an id is reasonable.

### Code Scalability 💡

I considered adding some additional properties to the `Message` class, such as a DateTime timestamp, to track when messages are sent and to sort incoming messages by time. While this is a simple improvement, it served as an inspiration for my subsequent improvements.

## Usage 🛠

### User👨

The `User` class represents users in the system. It has two properties: `Id` and `UserName`.

```
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
}
```

### Message📨

The `Message` class represents messages in the system. It has five properties: `Id`, `SenderId`, `ReceiverId`, `Content`, and `Timestamp`.

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

### MessageStore 📚

The `MessageStore` class is responsible for storing and managing messages. It offers methods to send a message, retrieve messages for a user, delete a message for a user, and retrieve messages for a user sorted by timestamp.

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

### MessageService 📡

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

## Testing 🔬

The system also includes a set of unit tests to verify its functionality.Here I use Xunit framework to test, For example, here is a test that verifies the `GetMessagesSortedByTimestamp` method:

```
[Fact]
public void GetMessagesSortedByTimestamp_ReturnsMessagesInOrder()
{
    // Arrange

    // Act

    // Assert
}
```
