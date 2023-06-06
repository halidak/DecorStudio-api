namespace DecorStudio_api.DTOs
{
    public class ReservationDto
    {
        public string UserId { get; set; }
        public DateTime ReservationDate { get; set; }
        public List<int> DecorIds { get; set; }
    }
}
