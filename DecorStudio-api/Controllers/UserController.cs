using DecorStudio_api.DTOs;
using DecorStudio_api.Models;
using DecorStudio_api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DecorStudio_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService userService;
        private readonly UserManager<User> userManager;

        public UserController(UserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto user)
        {
            try
            {
                var res = await userService.Register(user);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            try
            {
                var res = await userService.Login(user);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-user-by-email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var res = await userService.CheckUsername(email);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //update user   
        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateDto user)
        {
            try
            {
                var res = await userService.UpdateUser(id, user);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get user by id
        [HttpGet("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var res = await userService.GetUser(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //delete user
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var res = await userService.DeleteUser(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //change password
        [HttpPut("change-password/{id}")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] UserChangePasswordDto user)
        {
            try
            {
                var res = await userService.ChangePassword(id, user);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            try
            {
                var res = await userService.Verify(token, userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("request-password-reset")]
        public async Task<IActionResult> ForgotPassword([FromBody] EmailDto email)
        {
            try
            {
                var res = await userService.RequestPasswordReset(email);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        {
            try
            {
                var res = await userService.ResetPassword(resetPassword);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all-employees/{id}")]
        public async Task<IActionResult> GetAllEmployees(int id)
        {
            try
            {
                var res = await userService.GetEmployees(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
