using InternalMessagingSystem.Version1._2.Entities;

namespace InternalMessagingSystem.Version1._2.Interfaces;

/// <summary>
/// Defines the operations that a User Repository should have.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves a user by their id.
    /// </summary>
    /// <param name="id">The id of the user to retrieve.</param>
    /// <returns>The user, if found; otherwise, null.</returns>
    User GetUserById(int id);
        
    /// <summary>
    /// Checks if a user exists by their id.
    /// </summary>
    /// <param name="id">The id of the user to check.</param>
    /// <returns>True if the user exists, false otherwise.</returns>
    bool Exists(int id);

    /// <summary>
    /// Creates a new user with the specified username.
    /// </summary>
    /// <param name="username">The username of the new user.</param>
    /// <returns>The created user.</returns>
    User CreateUser(string username);
}
