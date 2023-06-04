using Microsoft.AspNetCore.Identity;

namespace DecorStudio_api.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int? ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
