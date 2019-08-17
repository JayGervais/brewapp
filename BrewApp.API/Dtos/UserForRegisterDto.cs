using System.ComponentModel.DataAnnotations;

namespace BrewApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Password more than 4 characters")]
        public string Password { get; set; }
    }
}