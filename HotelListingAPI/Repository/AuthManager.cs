using AutoMapper;
using HotelListingAPI.Data;
using HotelListingAPI.DTOs.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListingAPI.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;
        private readonly IConfiguration configuration;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<AuthResponseDTO> Login(LoginDTO logindto)
        {
                 var user = await userManager.FindByEmailAsync(logindto.Email);
                bool isValid = await userManager.CheckPasswordAsync(user, logindto.Password);
                if (user == null || isValid == false)
                {
                    return null;
                }
       
            var token = await GenerateToKEN(user);
            return new AuthResponseDTO
            {
                Token = token,
                UserId=user.Id
            };
        }

        public async Task<IEnumerable<IdentityError>> RegisterUser(ApiUserDTO userdto)
        {
            var user = mapper.Map<ApiUser>(userdto);
            user.UserName = userdto.Email;

            var result = await userManager.CreateAsync(user,userdto.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
        }

        public async Task<string> GenerateToKEN(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var role = await userManager.GetRolesAsync(user);
            var roleClaim = role.Select(x => new Claim(ClaimTypes.Role,x)).ToList();
            var userClaim = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("Uid",user.Id)
            }.Union(userClaim).Union(roleClaim);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
