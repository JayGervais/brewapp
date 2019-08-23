using System.Collections.Generic;
using System.Threading.Tasks;
using BrewApp.API.Models;

namespace BrewApp.API.Data
{
    public interface IBrewRepository 
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
    }
}