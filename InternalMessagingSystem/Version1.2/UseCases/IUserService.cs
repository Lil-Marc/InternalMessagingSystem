using InternalMessagingSystem.Version1._2.Entities;

namespace InternalMessagingSystem.Version1._2.UseCases;

public interface IUserService
{
    User GetUserById(int id);
    
    User CreateUser(string username); 
}