using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.DTOs.Country
{
    public class UpdateCountryDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
