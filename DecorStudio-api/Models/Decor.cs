namespace DecorStudio_api.Models
{
    public class Decor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public byte[]? Image { get; set; }
    }
}
