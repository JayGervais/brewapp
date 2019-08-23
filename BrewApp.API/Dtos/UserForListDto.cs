using System;

namespace BrewApp.API.Dtos
{
    public class UserForListDto
    {
        public int User_Id { get; set; }
        public string Username { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string City { get; set; }
        public string Prov { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
    }
}