using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.DTOs.Users
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Password length should be minimum {2} and maximum {1}", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
