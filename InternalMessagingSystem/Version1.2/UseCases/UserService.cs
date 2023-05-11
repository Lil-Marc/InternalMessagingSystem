using InternalMessagingSystem.Version1._2.Entities;
using InternalMessagingSystem.Version1._2.Interfaces;

namespace InternalMessagingSystem.Version1._2.UseCases;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetUserById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("User does not exist");
        }

        var user = _userRepository.GetUserById(id);
        return user;
    }

    public User CreateUser(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("Username cannot be null or empty", nameof(username));
        }
        
        var user = _userRepository.CreateUser(username);
        
        return user;
    }
}