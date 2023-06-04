namespace DecorStudio_api.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Decor_Reservation> Decor_Reservations { get; set; }
        public List<Appointment> Appointments { get; set; }

    }
}
