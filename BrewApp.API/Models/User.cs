using System.ComponentModel.DataAnnotations;
namespace BrewApp.API.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Prov { get; set; }
        public string Country { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}