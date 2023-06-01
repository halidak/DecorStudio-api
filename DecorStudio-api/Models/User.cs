using Microsoft.AspNetCore.Identity;

namespace DecorStudio_api.Models
{
    public class User : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}
