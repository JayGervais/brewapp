using BrewApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BrewApp.API.Data
{
    // data context assists with binding user data
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}