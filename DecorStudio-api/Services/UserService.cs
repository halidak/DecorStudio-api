using DecorStudio_api.DTOs;
using DecorStudio_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DecorStudio_api.Services
{
    public class UserService
    {
        private readonly UserManager<User> userManager;
        private IConfiguration configuration;
        private readonly AppDbContext context;
        public UserService(UserManager<User> userManager, IConfiguration configuration, AppDbContext context)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.context = context;
        }

        public async Task<bool> Register(UserRegisterDto user)
        {
            var u = await userManager.FindByNameAsync(user.UserName);
            if (u != null)
            {
                throw new Exception("User already exists");
            }
            User us = new User();
            if (user.StoreId != 0)
            {
                us = new User
                {
                    UserName = user.UserName,
                    Email = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RoleId = user.RoleId,
                    PhoneNumber = user.PhoneNumber,
                    StoreId = user.StoreId
                };
            }
            else if (user.StoreId == 0)
            {
                us = new User
                {
                    UserName = user.UserName,
                    Email = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RoleId = user.RoleId,
                    PhoneNumber = user.PhoneNumber,
                    StoreId = null
                };

            }
                var result = await userManager.CreateAsync(us, user.Password);
                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(us);
                    var encodedToken = HttpUtility.UrlEncode(token); // Enkodiraj token
                var htmlContent = $"<h1>Welcome to Decor Studio</h1>" +
                    $"<h3>Please click " +
                 $"<a href=\"{configuration.GetSection("ClientAppUrl").Value}/verify/{us.UserName}/{us.SecurityStamp}\">here</a>" +
                 $" to confirm your account</h3>";
                    SendEmail(us, "Verify your account", htmlContent).Wait();
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

            if (!u.IsEmailConfirmed)
            {
                throw new Exception("Email is not confirmed");
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

        //confirm email
        public async Task<bool> Verify(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                if (user.SecurityStamp == token)
                {
                    user.IsEmailConfirmed = true;
                    await context.SaveChangesAsync();
                    await userManager.UpdateAsync(user);
                    return true;
                }
                else
                {
                    throw new Exception("bad request");
                }
            }
            else 
            {   
                throw new Exception("not found"); 
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

        //delete user
        public async Task<bool> DeleteUser(string userId)
        {
            var u = await userManager.FindByIdAsync(userId);
            if (u == null)
            {
                throw new Exception("User not found");
            }
            var result = await userManager.DeleteAsync(u);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }

        //change password
        public async Task<bool> ChangePassword(string userId, UserChangePasswordDto user)
        {
            var u = await userManager.FindByIdAsync(userId);
            if (u == null)
            {
                throw new Exception("User not found");
            }
            var result = await userManager.ChangePasswordAsync(u, user.OldPassword, user.NewPassword);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }
        public async Task<bool> RequestPasswordReset(EmailDto email)
        {
            var user = await userManager.FindByEmailAsync(email.Email);
            if (user == null)
            {
               throw new Exception("user do not exist");
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token); // Enkodiraj token

            var htmlContent = $"<h1>Welcome to Decor Studio</h1>" +
                    $"<h3>Please click " +
                 $"<a href=\"{configuration.GetSection("ClientAppUrl").Value}/new-password/{user.UserName}/{encodedToken}\">here</a>" +
                 $" to confirm your account</h3>";
            SendEmail(user, "Verify your account", htmlContent).Wait();

            // Send the resetLink to the user's email address using an email service

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await userManager.FindByNameAsync(resetPasswordDto.UserName);
            if (user == null)
            {
                throw new Exception("user do not exist");
            }
            var result = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }


        public async Task SendEmail(User u, string subject, string htmlContent)
        {
            //var apiKey = Environment.GetEnvironmentVariable("EmailKey");
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("halida.karisik6@gmail.com", "DecorStudio");
            var to = new EmailAddress(u.Email, "DecorStudio User");
            var plainTextContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

    }
}
