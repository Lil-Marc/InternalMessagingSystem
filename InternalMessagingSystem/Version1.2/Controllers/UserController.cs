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
public class UserDto    
{
    public string UserName { get; set; }
}