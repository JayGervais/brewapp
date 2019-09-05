using System.Threading.Tasks;
using BrewApp.API.Models;

namespace BrewApp.API.Data
{
    // interface for registration, login, and checking if user exists
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string email, string password);
         Task<bool> UserExists(string email);
    }
}