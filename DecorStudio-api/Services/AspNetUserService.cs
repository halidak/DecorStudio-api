using DecorStudio_api.DTOs;
using DecorStudio_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DecorStudio_api.Services
{
    public class AspNetUserService
    {
        private readonly UserManager<User> userManager;
        public AspNetUserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Register(UserRegisterDto user)
        {
            User u = new User
            {
                UserName = user.UserName,
                Email = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
            };
            var result = await userManager.CreateAsync(u, user.Password);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<object> Login(UserLoginDto user)
        {
            var u = await userManager.FindByNameAsync(user.UserName);
            if (u == null)
            {
                throw new Exception("User do not exists");
            }

            if (await userManager.CheckPasswordAsync(u, user.Password))
            {
                var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("78fUjkyzfLz56gTq"));
                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                //new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString())
            };

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                    );
                var toReturn = new JwtSecurityTokenHandler().WriteToken(token);
                var obj = new
                {
                    expires = DateTime.Now.AddHours(1),
                    token = toReturn,
                    user = u
                };
                return obj;
            }
            else
            {
                throw new Exception("Username and password not match");
            }
        }
    }
}
