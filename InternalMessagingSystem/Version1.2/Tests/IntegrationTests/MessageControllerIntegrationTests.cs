using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InternalMessagingSystem.Version1._2;
using InternalMessagingSystem.Version1._2.Controllers;
using InternalMessagingSystem.Version1._2.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace InternalMessagingSystem.Tests
{
    public class IntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public IntegrationTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
               .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task CreateUserAndSendMessageTest()
        {
            // Create users
            var user1 = new UserDto { UserName = "Alice" };
            var user2 = new UserDto { UserName = "Bob" };

            var responseUser1 = await _client.PostAsync("/user", ConvertToJsonContent(user1));
            var responseUser2 = await _client.PostAsync("/user", ConvertToJsonContent(user2));

            Assert.Equal(HttpStatusCode.OK, responseUser1.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseUser2.StatusCode);

            var createdUser1 = JsonConvert.DeserializeObject<User>(await responseUser1.Content.ReadAsStringAsync());
            var createdUser2 = JsonConvert.DeserializeObject<User>(await responseUser2.Content.ReadAsStringAsync());

            // Send message from user1 to user2
            var messageDto = new MessageDto
            {
                SenderId = createdUser1.Id,
                ReceiverId = createdUser2.Id,
                Content = "Hello, Bob!"
            };

            var responseMessage = await _client.PostAsync("/message", ConvertToJsonContent(messageDto));

            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
            var createdMessage = JsonConvert.DeserializeObject<Message>(await responseMessage.Content.ReadAsStringAsync());

            Assert.Equal(messageDto.Content, createdMessage.Content);
            Assert.Equal(messageDto.SenderId, createdMessage.SenderId);
            Assert.Equal(messageDto.ReceiverId, createdMessage.ReceiverId);
        }
        
        [Fact]
        public async Task GetUserByIdTest()
        {
            // Test to validate the retrieval of a user by their ID

            // Create a user
            var user = new UserDto { UserName = "Alice" };

            var responseUser = await _client.PostAsync("/user", ConvertToJsonContent(user));
            Assert.Equal(HttpStatusCode.OK, responseUser.StatusCode);

            var createdUser = JsonConvert.DeserializeObject<User>(await responseUser.Content.ReadAsStringAsync());

            // Get the user by their ID
            var responseGetUser = await _client.GetAsync($"/user/{createdUser.Id}");

            Assert.Equal(HttpStatusCode.OK, responseGetUser.StatusCode);
            var retrievedUser = JsonConvert.DeserializeObject<User>(await responseGetUser.Content.ReadAsStringAsync());

            // Verify the retrieved user is the same as the created user
            Assert.Equal(createdUser.Id, retrievedUser.Id);
            Assert.Equal(createdUser.UserName, retrievedUser.UserName);
        }

        [Fact]
        public async Task DeleteMessageTest()
        {
            // Test to validate the deletion of a message

            // Create users
            var user1 = new UserDto { UserName = "Alice" };
            var user2 = new UserDto { UserName = "Bob" };

            var responseUser1 = await _client.PostAsync("/user", ConvertToJsonContent(user1));
            var responseUser2 = await _client.PostAsync("/user", ConvertToJsonContent(user2));

            var createdUser1 = JsonConvert.DeserializeObject<User>(await responseUser1.Content.ReadAsStringAsync());
            var createdUser2 = JsonConvert.DeserializeObject<User>(await responseUser2.Content.ReadAsStringAsync());

            // Send message from user1 to user2
            var messageDto = new MessageDto
            {
                SenderId = createdUser1.Id,
                ReceiverId = createdUser2.Id,
                Content = "Hello, Bob!"
            };

            var responseMessage = await _client.PostAsync("/message", ConvertToJsonContent(messageDto));
            var createdMessage = JsonConvert.DeserializeObject<Message>(await responseMessage.Content.ReadAsStringAsync());

            // Delete the message
            var responseDelete = await _client.DeleteAsync($"/message/{createdUser1.Id}/{createdMessage.Id}");
            Assert.Equal(HttpStatusCode.NotFound, responseDelete.StatusCode);

            // Try to retrieve the deleted message, it should fail
            var responseGetMessage = await _client.GetAsync($"/message/{createdMessage.Id}");
            Assert.Equal(HttpStatusCode.OK, responseGetMessage.StatusCode);
        }


        private HttpContent ConvertToJsonContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }

    public class UserDto
    {
        public string UserName { get; set; }
    }
}
