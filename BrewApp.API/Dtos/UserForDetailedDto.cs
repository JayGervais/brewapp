using System;
using System.Collections.Generic;
using BrewApp.API.Models;

namespace BrewApp.API.Dtos
{
    public class UserForDetailedDto
    {
        public int User_Id { get; set; }
        public string Username { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Prov { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime Created { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotosForDetailsDto> Photos { get; set; }
    }
}