using DecorStudio_api.DTOs;
using DecorStudio_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DecorStudio_api.Services
{
    public class UserService
    {
        private readonly UserManager<User> userManager;
        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Register(UserRegisterDto user)
        {
            var u = await userManager.FindByNameAsync(user.UserName);
            if (u != null)
            {
                throw new Exception("User already exists");
            }
            User us = new User
            {
                UserName = user.UserName,
                Email = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleId = user.RoleId,
                PhoneNumber = user.PhoneNumber,
                StoreId = user.StoreId
            };
            var result = await userManager.CreateAsync(us, user.Password);
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
                };

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                    );
                var toReturn = new JwtSecurityTokenHandler().WriteToken(token);
                var obj = new
                {
                    expires = DateTime.Now.AddHours(2),
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
        //proveri da li je username zauzet
        public async Task<bool> CheckUsername(string username)
        {
            var u = await userManager.FindByNameAsync(username);
            if (u == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //public async Task<User> GetUser(string email)
        //{
        //    return await userManager.FindByEmailAsync(email);
        //}

        //update user
        public async Task<User> UpdateUser(string userId, UserUpdateDto user)
        {
            var u = await userManager.FindByIdAsync(userId);
            if (u == null)
            {
                throw new Exception("User not found");
            }
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.PhoneNumber = user.PhoneNumber;
            u.Image = user.Image;
            var result = await userManager.UpdateAsync(u);
            if (result.Succeeded)
            {
                return u;
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }

        //get user by id
        public async Task<User> GetUser(string userId)
        {
            var u = await userManager.FindByIdAsync(userId);
            if (u == null)
            {
                throw new Exception("User not found");
            }
            return u;
        }

    }
}
