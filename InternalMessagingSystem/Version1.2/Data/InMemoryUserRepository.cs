using InternalMessagingSystem.Version1._2.Entities;
using InternalMessagingSystem.Version1._2.Interfaces;

namespace InternalMessagingSystem.Version1._2.Data;

/// <summary>
/// An in-memory implementation of the IUserRepository interface.
/// </summary>
public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<int, User> users = new();
    
    public User GetUserById(int id)
    {
        if (users.TryGetValue(id, out var user))
        {
            return user;
        }
        else
        {
            throw new ArgumentException("User not found", nameof(id));
        }
    }
    
    public bool Exists(int id)
    {
        return users.ContainsKey(id);
    }
    
    public User CreateUser(string username)
    {
        var newUser = new User {Id = users.Count + 1, UserName = username};
        users.Add(newUser.Id, newUser);
        return newUser;
    }
}