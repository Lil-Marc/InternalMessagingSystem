using InternalMessagingSystem.Version1._2.Entities;
using InternalMessagingSystem.Version1._2.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace InternalMessagingSystem.Version1._2.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }
    
    
    /// <summary>
    /// The constructor for the MessageController class. It takes an IMessageService as a parameter, 
    /// which will be used to perform operations on messages.
    /// </summary>
    /// <param name="messageService">The IMessageService to use for message operations.</param>
    [HttpPost]
    public ActionResult<Message> SendMessage([FromBody] MessageDto messageDto)
    {
        if (messageDto.SenderId <= 0 || messageDto.ReceiverId <= 0 || string.IsNullOrEmpty(messageDto.Content))
        {
            return BadRequest("Invalid message data.");
        }

        try
        {
            var message = _messageService.SendMessage(messageDto.SenderId, messageDto.ReceiverId, messageDto.Content);
            return Ok(message);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while sending the message.");
        }
    }
    
    /// <summary>
    /// Handles HTTP GET requests to retrieve all messages for a specific user.
    /// </summary>
    /// <param name="userId">The id of the user to get messages for.</param>
    /// <returns>A list of messages for the user or an error message if the operation fails.</returns>
    [HttpGet("{userId}")]
    public ActionResult<List<Message>> GetMessages(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Invalid user id.");
        }

        try
        {
            var messages = _messageService.GetMessages(userId);
            return Ok(messages);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while getting the messages.");
        }
    }

    /// <summary>
    /// Handles HTTP DELETE requests to delete a specific message for a user.
    /// </summary>
    /// <param name="userId">The id of the user to delete the message for.</param>
    /// <param name="messageId">The id of the message to delete.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpDelete("{userId}/{messageId}")]
    public IActionResult DeleteMessage(int userId, int messageId)
    {
        if (userId <= 0 || messageId <= 0)
        {
            return BadRequest("Invalid user id or message id.");
        }

        try
        {
            _messageService.DeleteMessage(userId, messageId);
            return NoContent();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while deleting the message.");
        }
    }
    
    /// <summary>
    /// Handles HTTP GET requests to retrieve all messages for a specific user, sorted by timestamp.
    /// </summary>
    /// <param name="userId">The id of the user to retrieve the messages for.</param>
    /// <returns>A list of messages, sorted by timestamp, for the user or an error message if the operation fails.</returns>
    [HttpGet("{userId}/sorted")]
    public ActionResult<List<Message>> GetMessagesSortedByTimestamp(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Invalid user id.");
        }

        try
        {
            var messages = _messageService.GetMessagesSortedByTimestamp(userId);
            return Ok(messages);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while getting the sorted messages.");
        }
    }
}

/// <summary>
/// Data Transfer Object for messages.
/// </summary>
public class MessageDto
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Content { get; set; }
}
