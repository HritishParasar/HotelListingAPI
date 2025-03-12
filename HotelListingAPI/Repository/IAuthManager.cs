using HotelListingAPI.DTOs.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.Repository
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> RegisterUser(ApiUserDTO userdto);
        Task<AuthResponseDTO> Login(LoginDTO logindto);
    }
}
