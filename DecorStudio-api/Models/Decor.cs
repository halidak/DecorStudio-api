namespace DecorStudio_api.Models
{
    public class Decor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string? Image { get; set; }
        public List<Warehouse_Decor> Warehouse_Decors { get; set; }
        public List<Catalog_Decor> Catalog_Decors { get; set; }
        public List<Decor_Reservation> Decor_Reservations { get; set; }
    }
}
