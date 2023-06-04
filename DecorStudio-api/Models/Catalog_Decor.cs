namespace DecorStudio_api.Models
{
    public class Catalog_Decor
    {
        public int Id { get; set; }
        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }
        public int DecorId { get; set; }
        public Decor Decor { get; set; }
    }
}
