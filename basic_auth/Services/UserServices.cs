using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using basic_auth.Models;
using Microsoft.Extensions.Logging;

namespace basic_auth.Services 
{
    public interface IUserService 
    {
        Task<User> Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        private static List<User> TEST_USERS = new List<User> {
            new User { Username = "test", Password = "testpass" }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            _logger.LogInformation($"Username: {username}. Password: {password}");
            var user = TEST_USERS.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null) 
            {
                return null;
            }
            
            return user;
        }
    }
}