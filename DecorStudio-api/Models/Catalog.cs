namespace DecorStudio_api.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
