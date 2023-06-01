using System.ComponentModel.DataAnnotations;

namespace DecorStudio_api.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}
