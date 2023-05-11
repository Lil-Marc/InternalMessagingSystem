using InternalMessagingSystem.Version1._2.Entities;
using InternalMessagingSystem.Version1._2.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace InternalMessagingSystem.Version1._2.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The user with the specified ID, or an error message if the operation fails.</returns>
    [HttpGet("{userId}")]
    public ActionResult<User> GetUserById(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Invalid user id.");
        }
        try
        {
            var user = _userService.GetUserById(userId);
            Console.WriteLine(user);
            return Ok(user);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while finding the user.");
        }
    }
    
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="userDto">The data of the user to create.</param>
    /// <returns>The created user, or an error message if the operation fails.</returns>
    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] UserDto userDto)
    {
        if (string.IsNullOrEmpty(userDto.UserName))
        {
            return BadRequest("Invalid username.");
        }
        try
        {
            var user = _userService.CreateUser(userDto.UserName);
            return Ok(user);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred while creating a user. Details: {e.ToString()}");
        }
    }

}
/// <summary>
/// Data Transfer Object for user creation.
/// </summary>
public class UserDto    
{
    public string UserName { get; set; }
}