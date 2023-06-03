namespace DecorStudio_api.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public double Size { get; set; }
        public int NumberOfEmployees { get; set; }
        public List<Catalog> Catalogs { get; set; }
    }
}
