namespace DecorStudio_api.Models
{
    public class Decor_Reservation
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int DecorId { get; set; }
        public Decor Decor { get; set; }
    }
}
