using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrewApp.API.helpers;
using BrewApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewApp.API.Data
{
    // repository async method class for updating user and website data
    public class BrewingRepository : IBrewRepository
    {
        // generate datacontext object
        private readonly DataContext _context;
        public BrewingRepository(DataContext context)
        {
            _context = context;
        }

        // add new entity (used for user) to list
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        // delete entity from list
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Like> GetLike(int user_Id, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync(u => u.LikerId == user_Id && u.LikeeId == recipientId);
        }

        // generate main photo context for user
        public async Task<Photo> GetMainPhotoForUser(int user_Id)
        {
            return await _context.Photos.Where(u => u.User_Id == user_Id).FirstOrDefaultAsync(p => p.IsMain);
        }

        // get single photo from user
        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        // get single user from id
        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.User_Id == id);
            return user;
        }

        // pagedlist method for getting users and paginating view
        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.Include(p => p.Photos)
                .OrderByDescending(u => u.LastActive).AsQueryable();

            users = users.Where(u => u.User_Id != userParams.User_Id);

            if (userParams.Likers)
            {
                var userLikers = await GetUserLikes(userParams.User_Id, userParams.Likers);
                users = users.Where(u => userLikers.Contains(u.User_Id));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUserLikes(userParams.User_Id, userParams.Likers);
                users = users.Where(u => userLikees.Contains(u.User_Id));
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserLikes(int id, bool likers)
        {
            var user = await _context.Users
                .Include(x => x.Likers)
                .Include(x => x.Likees)
                .FirstOrDefaultAsync(u => u.User_Id == id);

                if (likers)
                {
                    return user.Likers.Where(u => u.LikeeId == id).Select(i => i.LikerId);
                }
                else
                {
                    return user.Likees.Where(u => u.LikerId == id).Select(i => i.LikeeId);
                }          
        }

        // save changes
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}