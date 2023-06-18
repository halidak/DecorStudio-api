using Microsoft.AspNetCore.Identity;

namespace DecorStudio_api.Models
{
    public class User : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public string PhoneNumber { get; set; }
        public string? Image { get; set; }
        public int? StoreId { get; set; }
        public Store Store { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}
