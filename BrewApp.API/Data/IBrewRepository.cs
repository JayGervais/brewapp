using System.Collections.Generic;
using System.Threading.Tasks;
using BrewApp.API.helpers;
using BrewApp.API.Models;

namespace BrewApp.API.Data
{
    public interface IBrewRepository 
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForUser(int user_Id);
    }
}